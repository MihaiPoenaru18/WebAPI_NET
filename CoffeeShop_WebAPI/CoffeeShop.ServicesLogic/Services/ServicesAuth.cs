using AutoMapper;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop_WebApi.Services.AutoMapper;
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
        private ICoffeeShopUserRepository<User> _usersRepository;
        private ICoffeeShopUserRepository<UserWithNewsLetter> _usersWithNewsLetterRepository;
        private static User user = new User();


        public ServicesAuth(ICoffeeShopUserRepository<User> usersRepository, IMapper mapper, IAuthentication authorization, ICoffeeShopUserRepository<UserWithNewsLetter> usersWithNewsLetterRepository)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _authorization = authorization;
            _usersWithNewsLetterRepository = usersWithNewsLetterRepository;
        }

        public UserDto GetInfo(AuthenticateRequest loginUser)
        {
            var user = MapperConfig<AuthenticateRequest, User>.InitializeAutomapper().Map<AuthenticateRequest, User>(loginUser);
            try
            {
                if (_usersRepository.IsUserExistingInDB(user))
                {
                    return MapperConfig<User, UserDto>.InitializeAutomapper().Map<User, UserDto>(user);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth  -> GetInfo() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            try
            {
                var users = _usersRepository.GetAll().Result;
                var userWithNewsLetters = _usersWithNewsLetterRepository.GetAll().Result;
                if (users != null || userWithNewsLetters != null)
                {
                    var usersFromDb = (from user in users
                                       join userWithNewsLetter in userWithNewsLetters
                                       on user.IdUserNewsLetter equals userWithNewsLetter.Id
                                       select new UserDto
                                       {
                                           Email = user.Email,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           Role = user.Role,
                                           Password = user.Password,
                                           NewsLetter = new UserWithNewsLetterDto()
                                           {
                                               Email = user.Email,
                                               Name = userWithNewsLetter.Name,
                                               IsActived = userWithNewsLetter.IsNewsLetterActive
                                           }
                                       }).ToList();

                    return usersFromDb;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth  -> GetAllUsers() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public async Task<bool> IsUserRegistered(UserDto userDto)
        {
            try
            {
                var mapperUser = MapperConfig<UserDto, User>.InitializeAutomapper();
                if ((userDto.Role != "User" || userDto.Role != "Admin") && userDto != null)
                {
                    user = mapperUser.Map(userDto, user);
                    return await _usersRepository.Insert(user);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth  -> IsUserRegistered() -> Exception => {@ex.Message}", ex.Message);
            }
            return false;
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest request)
        {
            try
            {
                var response = _authorization.Authorization(request, DateTime.Now.AddDays(7));
                if (response != null)
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ServicesAuth  -> Authenticate() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }
    }
}
