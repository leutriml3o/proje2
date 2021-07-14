using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.Models
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        public void SetSessionUser(string username, string email)
        {
            //Sessiona Kullanıcın username ve mail adresini atar.
            HttpContextAccessor.HttpContext.Session.SetString("username", username);
            HttpContextAccessor.HttpContext.Session.SetString("email", email);
        }

        public string GetSessionUsername()
        {
            //Sessiondan Kullanıcın username adresini okur
            return HttpContextAccessor.HttpContext.User.Identity.Name;
        }
        public string GetSessionMail()
        {
            //Sessiondan Kullanıcın Mail adresini okur
            return HttpContextAccessor.HttpContext.Session.GetString("email");
        }

        public void SessionClear()
        {
            HttpContextAccessor.HttpContext.Session.Remove("username");
            HttpContextAccessor.HttpContext.Session.Remove("email");
        }
    }
}
