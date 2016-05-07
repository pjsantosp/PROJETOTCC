using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SISPTD.Ultis
{
    public class NavTabs
    {
        public static string GetTab(ViewContext view, string controleSelecionado, string action = null)
        {
            if (action == null)
            {
                return view.RouteData.Values["controller"].ToString() == controleSelecionado ? "active" : null;
                
            }
            if (view.RouteData.Values["controller"].ToString()== controleSelecionado && view.RouteData.Values["action"].ToString()== action)
            {
                return "active";
            }
            return null;
        }
    }
}