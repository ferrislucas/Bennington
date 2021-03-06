﻿using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.ContentTree.TreeManager.Routing
{
	public class RegisterContentTreeSmsRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"ContentTree/{action}",
				new { controller = "TreeManager", action = "Index" }
				);
		}
	}
}