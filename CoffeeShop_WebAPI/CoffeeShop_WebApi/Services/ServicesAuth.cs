using AutoMapper;
using CoffeeShop_WebApi.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.Models;
using CoffeeShop_WebApi.Services.AutoMapper;
using WebApplication1.DataAccess.Repository;

namespace CoffeeShop_WebApi.Services
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
            var findUser = _usersRepository.GetAll().Result.SingleOrDefault(x => x.Email == loginUser.Email);
         
            if (findUser == null)
            {
                return null;
            }
            var s = _mapper.Map<User, UserDto>(findUser);
            return s;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var mapperUser =  MapperConfig.InitializeAutomapper();
            var users = new List<UserDto>();
            foreach(var user in _usersRepository.GetAll().Result)
            {
                users.Add(mapperUser.Map<User, UserDto>(user));
            }
            return users;
        }

        public async Task<bool> IsUserRegistered(UserDto user)
        {
            if (user.Role != "User" || user.Role != "Admin")
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                Guid g = Guid.NewGuid();
                ServicesAuth.user.Id = g;
                ServicesAuth.user = _mapper.Map<User>(user);
                ServicesAuth.user.Password = passwordHash;
                return await _usersRepository.Insert(ServicesAuth.user);
            }
            return false;
        }
        public AuthenticateResponse? Authenticate(AuthenticateRequest request)
        {
            var response = _authorization.Authorization(request,DateTime.Now.AddDays(1));
            if (response == null)
            {
                return null;
            }
            return response;
        }
    }
}
