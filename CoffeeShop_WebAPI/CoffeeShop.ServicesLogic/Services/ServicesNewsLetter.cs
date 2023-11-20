using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Services.AutoMapper;
using Serilog;

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
            try
            {
                var mapper = MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>.InitializeAutomapper();
                var users = new List<UserWithNewsLetterDto>();
                foreach (var user in _usersWithNewsLetterRepository.GetAll().Result)
                {
                    users.Add(mapper.Map<UserWithNewsLetter, UserWithNewsLetterDto>(user));
                }
                return users;
            }
            catch (Exception ex)
            {
                Log.Information("ServicesNewsLetter  -> GetAllUserWithNewsLetter() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public async Task<bool> IsUserRegisteredWithNewsLetter(UserWithNewsLetterDto userDto)
        {
            try
            {
                var mapper = MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>.InitializeAutomapper();
                if (!String.IsNullOrEmpty(userDto.Email))
                {
                    userWithNews = mapper.Map(userDto, userWithNews);
                    return await _usersWithNewsLetterRepository.Insert(userWithNews);
                }
            }
            catch (Exception ex)
            {
                Log.Information("ServicesAuth  -> GetInfo() -> Exception => {@ex.Message}", ex.Message);
            }
            return false;
        }

        public bool GetStatusOfNewsLetter(UserWithNewsLetterDto userDto)
        {
            try
            {
                var mapper = MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>.InitializeAutomapper();
                userWithNews = mapper.Map(userDto, userWithNews);
                if (!_usersWithNewsLetterRepository.IsUserExistingInDB(userWithNews))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Information("ServicesAuth  -> GetInfo() -> Exception => {@ex.Message}", ex.Message);
            }
            return true;
        }
    }
}
