﻿using System.Web;
using System.Web.Mvc;

namespace Laboratorio2_EDD2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
