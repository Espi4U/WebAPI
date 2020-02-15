using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Requests.AuthorizeDataRequests;
using Shared.Models.Responses.AuthorizeDataResponses;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class AuthorizeDataController : ControllerBase
    {
        private readonly FamilyFinanceContext db;
        public AuthorizeDataController(FamilyFinanceContext context)
        {
            db = context;
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody]LoginRequest request)
        //{
        //    if(!db.AuthorizeData.Where(x=>x.Login == request.Login).Any())
        //    {
        //        return NotFound();
        //    }

        //    int currentFamilyId = db.AuthorizeData.Where(x => x.Login == request.Login && x.PasswordHash == request.PasswordHash).FirstOrDefault().FamilyId;
        //    return new LoginResponse
        //    {
        //        FamilyId = currentFamilyId,
        //        PersonId = db.Persons.Where(x => x.PersonalPINCode == request.PersonalPINCode && x.FamilyId == currentFamilyId).FirstOrDefault().Id
        //    };
        //}

        //[HttpPost]
        //[Route("register")]
        //public async Task<ActionResult<LoginResponse>> RegisterAsync([FromBody]LoginRequest request)
        //{
        //    AuthorizeData user = db.AuthorizeData.Where(x => (x.Login == request.Login && x.PasswordHash == request.PasswordHash && x.PersonalPINCode == request.PersonalPINCode)).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return new LoginResponse
        //    {
        //        PersonId = user.PersonId,
        //        FamilyId = db.Persons.Where(x => x.Id == user.PersonId).FirstOrDefault().FamilyId
        //    };
        //}
    }
}
