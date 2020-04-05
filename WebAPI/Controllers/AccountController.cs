using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json;
using Shared.Models.Responses.PersonsResponses;
using WebAPI.Models.APIModels;
using WebAPI.Services;
using Shared.Models.Requests.BaseRequests;
using Shared.Models.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly PersonService _personService;

        public AccountController(PersonService personService)
        {
            _personService = personService;
        }

        [Route("login"), HttpPost]
        public LoginResponse Login([FromBody]LoginRequest request)
        {
            return GetToken(request);
        }

        public LoginResponse GetToken(LoginRequest request)
        {
            var response = new LoginResponse();

            var identity = GetIdentity(request.Login, request.Password, request.PINCode);
            if (identity == default)
            {
                response.BaseIsSuccess = false;
                response.BaseMessage = "User not found";
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            response.Token = encodedJwt;
            response.PersonId = Convert.ToInt32(identity.Name);

            return response;
        }

        private ClaimsIdentity GetIdentity(string login, string passwordHash, int pinCode)
        {
            PersonResponse response = _personService.GetPersonByUserData(login, passwordHash, pinCode);
            if (response.Person != default)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, response.Person.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, response.Person.Role)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
