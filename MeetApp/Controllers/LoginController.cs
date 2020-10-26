using MeetApp.Actions;
using MeetApp.Models;
using ServiceTemplate.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MeetApp.Controllers
{

    [ValidateModel]
    [GenericExceptionFilter]
    public class LoginController : ApiController
    {
        public async Task<LoginOutput> Login(LoginInput parameters)
        {
            return await SessionManagement.Login(parameters);
        }

        public LogoutOutput LogOut(LogoutInput parameters)
        {
            return SessionManagement.Logout(parameters);
        }

        public CheckTokenOutput Check(CheckTokenInput Parameters)
        {
            var userToken = SessionManagement.getUserToken(Parameters.Token);


            if (userToken != null)

            {
                return new CheckTokenOutput() { Type = 1, Message = "" };
                // user var demektir, true gönderebilirsin.
            }
            else
            {
                return new CheckTokenOutput() { Type = 0, Message = "" };
            }
            }

    }
}
