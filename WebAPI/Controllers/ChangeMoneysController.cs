using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/changemoneys")]
    public class ChangeMoneysController : ControllerBase
    {
        private readonly ChangeMoneyService _changeMoneyService;

        public ChangeMoneysController(ChangeMoneyService changeMoneyService)
        {
            _changeMoneyService = changeMoneyService;
        }
    }
}
