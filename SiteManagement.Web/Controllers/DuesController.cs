using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Application.DuesInformation.Queries.GetDuesInformation;

namespace SiteManagement.Web.Controllers
{
    public class DuesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<DuesInformationVm>> GetDuesInformation()
        {
            return await Mediator.Send(new GetDuesInformationQuery());
        }
    }
}