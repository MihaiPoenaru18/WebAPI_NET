using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

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
        public ActionResult<string> AddUserToNewsLetter(UserWithNewsLetterDto body)
        {
            try
            {
                if (body.Email == null && body.Name == null)
                {
                    return BadRequest("The fields are empty!!!" );
                }
                var x = _servicesNewsLetter.IsUserRegisteredWithNewsLetter(body).Result;
                if (!x)
                {
                    return BadRequest("The user already subscribed to the newsletter!!!" );
                }

                return Ok("Subscriber Success" );
            }
            catch (Exception ex)
            {
                Log.Error("NewsLetterController -> AddUserToNewsLette() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }

        }

        //[HttpGet("GetNewsLetterInfo")]
        //public ActionResult<bool> GetAllUsersInfo([FromBody] UserWithNewsLetterDto body)
        //{

        //    if (body == null)
        //    {
        //        return Ok(new ApiResponse { Success = false, Message = "The fiels are emplty!!!" });
        //    }
        //    return Ok(_servicesNewsLetter.GetStatusOfNewsLetter(body));
        //}
    }
}
