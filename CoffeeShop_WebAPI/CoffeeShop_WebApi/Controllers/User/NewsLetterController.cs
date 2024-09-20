using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoffeeShop_WebApi.Controllers.User
{
    [Route("api/")]
    [ApiController]
    public class NewsLetterController : ControllerBase
    {
        private readonly IServicesNewsLetter<UserWithNewsLetterDto> _servicesNewsLetter;

        public NewsLetterController(IServicesNewsLetter<UserWithNewsLetterDto> servicesNewsLetter)
        {
            _servicesNewsLetter = servicesNewsLetter;
        }

        [HttpPost("AddUserToNewsLetter")]
        public async Task<ActionResult<string>> AddUserToNewsLetter(UserWithNewsLetterDto body)
        {
            if (string.IsNullOrWhiteSpace(body.Email) || string.IsNullOrWhiteSpace(body.Name))
            {
                return BadRequest("The fields are empty!!!");
            }

            try
            {
                var isAlreadySubscribed = await _servicesNewsLetter.IsUserRegisteredWithNewsLetter(body);
                if (isAlreadySubscribed)
                {
                    return BadRequest("The user already subscribed to the newsletter!!!");
                }

                return Ok("Subscriber Success");
            }
            catch (Exception ex)
            {
                Log.Error("Error adding user to newsletter: {Message}", ex.Message);
                return BadRequest("An error occurred while processing the request.");
            }
        }
    }
}
