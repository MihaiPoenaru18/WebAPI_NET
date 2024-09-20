using AutoMapper;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Serilog;
using CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesAuth : IServicesAuth<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _authorization;
        private readonly ICoffeeShopUserRepository<User> _userRepository;
        private readonly ICoffeeShopUserRepository<UserWithNewsLetter> _newsletterRepository;

        public ServicesAuth(
            ICoffeeShopUserRepository<User> userRepository,
            IMapper mapper,
            IAuthentication authorization,
            ICoffeeShopUserRepository<UserWithNewsLetter> newsletterRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authorization = authorization;
            _newsletterRepository = newsletterRepository;
        }

        public async Task<UserDto> GetInfo(AuthenticateRequest loginUser)
        {
            if (loginUser == null || string.IsNullOrEmpty(loginUser.Email))
            {
                Log.Warning("GetInfo() called with null or invalid AuthenticateRequest.");
                return null;
            }

            try
            {
                var user = await _userRepository.GetUserByEmail(loginUser.Email);
                return user == null ? null : _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while retrieving user info for email: {Email}", loginUser.Email);
                return null;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAll();
                var newsletters = await _newsletterRepository.GetAll();

                var userDtos = users
                    .GroupJoin(newsletters,
                        user => user.IdUserNewsLetter,
                        newsletter => newsletter.Id,
                        (user, matchingNewsletters) => new
                        {
                            User = user,
                            Newsletter = matchingNewsletters.FirstOrDefault()
                        })
                    .Select(joined => MapUserWithNewsletter(joined.User, joined.Newsletter));

                return userDtos.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while retrieving all users.");
                return Enumerable.Empty<UserDto>();
            }
        }

        public async Task<bool> IsUserRegistered(UserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Role))
            {
                Log.Warning("IsUserRegistered() called with invalid UserDto.");
                return false;
            }

            if (userDto.Role != "User" && userDto.Role != "Admin")
            {
                Log.Warning("Invalid user role: {Role}", userDto.Role);
                return false;
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                return await _userRepository.Insert(user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while registering user.");
                return false;
            }
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            if (request == null)
            {
                Log.Warning("Authenticate() called with null request.");
                return null;
            }

            try
            {
                return await _authorization.Authorization(request, DateTime.Now.AddDays(7));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during authentication.");
                return null;
            }
        }

        private UserDto MapUserWithNewsletter(User user, UserWithNewsLetter newsletter)
        {
            var userDto = _mapper.Map<UserDto>(user);
            if (newsletter != null)
            {
                userDto.NewsLetter = _mapper.Map<UserWithNewsLetterDto>(newsletter);
            }

            return userDto;
        }
    }
}
