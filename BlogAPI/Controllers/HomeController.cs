using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
    [Route("")]
    public class HomeController: Controller
    {
        public ActionResult Home()
        {
            return Ok("Home Page");
        }
    }
}
