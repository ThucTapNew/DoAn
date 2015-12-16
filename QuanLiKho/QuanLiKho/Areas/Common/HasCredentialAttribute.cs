using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiKho.Areas.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[Common.USER_SESSION];
            List<string> privillegalLevels = this.GetCredentialByLoggedUser(session.UserName);
            if (privillegalLevels.Contains(this.RoleID))
            {
                return true;
            }
            else return false;
        }
        private List<string> GetCredentialByLoggedUser(string user)
        {
            var credential = (List<string>)HttpContext.Current.Session[Common.SESSION_CREDENTIAL];
            return credential ;
        }
    }
}