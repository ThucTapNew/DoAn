﻿using System.Web.Mvc;

namespace QuanLiKho.Areas.Admin
{
    public class MainAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Main";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Main_default",
                "Main/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}