using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SiteManagement.Web.Controllers
{
    public class DuesController : ApiControllerBase
    {
        [HttpGet]
        public async GetDuesInformation()
        {
            return await Mediator.Send(new GetDuesInformation());
        }
    }
}