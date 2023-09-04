using AutoMapper;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop_WebApi.Services.AutoMapper;
using WebApplication1.DataAccess.Repository;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesAuth : IServices<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _authorization;
        private ICoffeeShopRepository<User> _usersRepository;
        private static User user = new User();

        public ServicesAuth(ICoffeeShopRepository<User> usersRepository, IMapper mapper, IAuthentication authorization)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _authorization = authorization;
        }

        public UserDto GetInfo(AuthenticateRequest loginUser)
        {
            var user = MapperConfig<AuthenticateRequest, User>.InitializeAutomapper().Map<AuthenticateRequest, User>(loginUser);
           
            if (_usersRepository.IsUserExistingInDB(user))
            {
                return MapperConfig<User, UserDto>.InitializeAutomapper().Map<User, UserDto>(user);
            }
            return null;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var mapperUser = MapperConfig<UserDto, User>.InitializeAutomapper();
            var users = new List<UserDto>();
            foreach (var user in _usersRepository.GetAll().Result)
            {
                users.Add(mapperUser.Map<User, UserDto>(user));
            }
            return users;
        }

        public async Task<bool> IsUserRegistered(UserDto userDto)
        {
            var mapperUser = MapperConfig<UserDto, User>.InitializeAutomapper();
            if ((userDto.Role != "User" || userDto.Role != "Admin") && userDto != null)
            {
                user = mapperUser.Map(userDto, user);
                return await _usersRepository.Insert(user);
            }
            return false;
        }
        public AuthenticateResponse? Authenticate(AuthenticateRequest request)
        {
            var response = _authorization.Authorization(request, DateTime.Now.AddDays(1));
            if (response == null)
            {
                return null;
            }
            return response;
        }
    }
}
