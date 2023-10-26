using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop_WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class NewsLetterController : ControllerBase
    {
        private IServicesNewsLetter<UserWithNewsLetterDto> _servicesNewsLetter;

        public NewsLetterController(IServicesNewsLetter<UserWithNewsLetterDto> servicesNewsLetter)
        {
            _servicesNewsLetter = servicesNewsLetter;
        }
        [HttpPost("AddUserToNewsLetter")]
        public ActionResult AddUserToNewsLetter(UserWithNewsLetterDto body)
        {
            if (body == null)
            {
                return BadRequest("The fiels are emplty!!!");
            }
            if (!_servicesNewsLetter.IsUserRegisteredWithNewsLetter(body).Result)
            {
                return BadRequest("The user already subscriber the newsletter!!!");
            }
            return Ok("Subscriber Success ");
        }
        
        [HttpGet("GetNewsLetterInfo")]
        public ActionResult<bool> GetAllUsersInfo([FromBody] UserWithNewsLetterDto body)
        {

            if (body == null)
            {
                return BadRequest("The fiels are emplty!!!");
            }
            return Ok(_servicesNewsLetter.GetStatusOfNewsLetter(body));
        }
    }
}
