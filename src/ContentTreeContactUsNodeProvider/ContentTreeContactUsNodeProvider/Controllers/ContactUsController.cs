﻿using System.Web.Mvc;

namespace Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Controllers
{
    public class ContactUsController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult Confirmation()
        {
            return View();
        }

    }
}
