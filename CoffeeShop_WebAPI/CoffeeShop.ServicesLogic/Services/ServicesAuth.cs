using AutoMapper;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Serilog;
using CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels;
using System.Linq;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesAuth : IServicesAuth<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _authorization;
        private readonly ICoffeeShopUserRepository<User> _usersRepository;
        private readonly ICoffeeShopUserRepository<UserWithNewsLetter> _usersWithNewsLetterRepository;

        public ServicesAuth(ICoffeeShopUserRepository<User> usersRepository, IMapper mapper, IAuthentication authorization, ICoffeeShopUserRepository<UserWithNewsLetter> usersWithNewsLetterRepository)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _authorization = authorization;
            _usersWithNewsLetterRepository = usersWithNewsLetterRepository;
        }

        public async Task<UserDto> GetInfo(AuthenticateRequest loginUser)
        {
            try
            {
                var user = _usersRepository.GetUserByEmail(loginUser.Email);
                if (user != null)
                {
                    return _mapper.Map<UserDto>(user);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth -> GetInfo() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            try
            {
                var users = await _usersRepository.GetAll();
                var userWithNewsLetters = await _usersWithNewsLetterRepository.GetAll();

                var userDtos = users.Join(
                    userWithNewsLetters,
                    user => user.IdUserNewsLetter,
                    newsletter => newsletter.Id,
                    (user, newsletter) => new { User = user, Newsletter = newsletter }
                ).Select(joined => _mapper.Map<UserDto>(joined.User, opts =>
                {
                    opts.AfterMap((src, dest) =>
                    {
                        dest.NewsLetter = _mapper.Map<UserWithNewsLetterDto>(joined.Newsletter);
                    });
                }));

                return userDtos.ToList();
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth -> GetAllUsers() -> Exception => {@ex.Message}", ex.Message);
            }
            return Enumerable.Empty<UserDto>();
        }

        public async Task<bool> IsUserRegistered(UserDto userDto)
        {
            try
            {
                if (userDto != null && (userDto.Role == "User" || userDto.Role == "Admin"))
                {
                    var user = _mapper.Map<User>(userDto);
                    return await _usersRepository.Insert(user);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth -> IsUserRegistered() -> Exception => {@ex.Message}", ex.Message);
            }
            return false;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            try
            {
                var response = _authorization.Authorization(request, DateTime.Now.AddDays(7));
                return response;
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth -> Authenticate() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }
    }
}