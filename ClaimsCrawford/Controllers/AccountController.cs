using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogicLayer;
using Models.Interface;
using System.IO;
using System.Text;
using Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Project.Controllers
{

    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IBLL _logic;

        public AccountController(IBLL BLL)
        {
            _logic = BLL; // using baisc IOC for maintainability and possibly unit testing at a later date.
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/API/Accounts/Login")]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                // Don't return any information for people who might be fishing for login related data.
                // Doesn't account for reponse time fishing.
                ApplicationUser user;
                if (model.UserName == string.Empty || model.Password == string.Empty)
                {
                    return NotFound("Login failed");
                }
                else if (_logic.AuthenticateUser(model.UserName, model.Password, out user))
                {
                    return Ok(new { UserModel = user, DataModel = _logic.GetLossTypeDateForUser(user.UserID) });
                }
                return NotFound("Login failed");
            }
            catch (Exception)
            {
                //log("Login failed or fetching data failed.,e) // ideally we'd capture this to a log file. Possibly not ideal for the user to see this.
                return NotFound("Login failed or fetching data failed.");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/API/IsAlive")]
        public IActionResult Alive()
        {
            return Ok(true);
        }

    }
}
