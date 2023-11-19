using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Services.AutoMapper;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesNewsLetter : IServicesNewsLetter<UserWithNewsLetterDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _authorization;
        private ICoffeeShopUserRepository<UserWithNewsLetter> _usersWithNewsLetterRepository;
        private static UserWithNewsLetter userWithNews = new UserWithNewsLetter();

        public ServicesNewsLetter(ICoffeeShopUserRepository<UserWithNewsLetter> usersRepository, IMapper mapper, IAuthentication authorization)
        {
            _usersWithNewsLetterRepository = usersRepository;
            _mapper = mapper;
            _authorization = authorization;
        }

        public IEnumerable<UserWithNewsLetterDto> GetAllUserWithNewsLetter()
        {
            var mapper = MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>.InitializeAutomapper();
            var users = new List<UserWithNewsLetterDto>();
            foreach (var user in _usersWithNewsLetterRepository.GetAll().Result)
            {
                users.Add(mapper.Map<UserWithNewsLetter, UserWithNewsLetterDto>(user));
            }
            return users;
        }

        public async Task<bool> IsUserRegisteredWithNewsLetter(UserWithNewsLetterDto userDto)
        {
            var mapper = MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>.InitializeAutomapper();
            if(!String.IsNullOrEmpty(userDto.Email) )
            {
                userWithNews = mapper.Map(userDto, userWithNews);
                return await _usersWithNewsLetterRepository.Insert(userWithNews);
            }
            return false;
        }

        public bool GetStatusOfNewsLetter(UserWithNewsLetterDto userDto)
        {
            var mapper = MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>.InitializeAutomapper();
            userWithNews = mapper.Map(userDto, userWithNews);
            if (!_usersWithNewsLetterRepository.IsUserExistingInDB(userWithNews))
            {
                return false;
            }
            return true;
        }
    }
}
