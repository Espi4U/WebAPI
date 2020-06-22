using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/invitekeys")]
    public class InviteKeysController : ControllerBase
    {
        private readonly InviteKeyService _inviteKeyService;

        public InviteKeysController(InviteKeyService inviteKeyService)
        {
            _inviteKeyService = inviteKeyService;
        }

        [Route("add_invite_key"), HttpPost]
        public BaseResponse AddInviteKey([FromBody]InviteKeyRequest request) =>
            _inviteKeyService.AddInviteKey(request);
    }
}
