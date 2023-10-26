using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
        public IActionResult AddUserToNewsLetter(UserWithNewsLetterDto body)
        {
            if (body == null)
            {
                return Ok(new ApiResponse { Success = false, Message = "The fields are empty!!!" });
            }

            if (!_servicesNewsLetter.IsUserRegisteredWithNewsLetter(body).Result)
            {
                return Ok(new ApiResponse { Success = false, Message = "The user already subscribed to the newsletter!!!" });
            }

            return Ok(new ApiResponse { Success = true, Message = "Subscriber Success" });
        }

        [HttpGet("GetNewsLetterInfo")]
        public ActionResult<bool> GetAllUsersInfo([FromBody] UserWithNewsLetterDto body)
        {

            if (body == null)
            {
                return Ok(new ApiResponse { Success = false, Message = "The fiels are emplty!!!" });
            }
            return Ok(_servicesNewsLetter.GetStatusOfNewsLetter(body));
        }
    }
}
