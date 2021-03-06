﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Controllers
{
    public class ContentTreeController : Controller
    {
        private readonly Func<IContentTreeNodeDisplayViewModelBuilder> contentTreeNodeDisplayViewModelBuilder;
        private readonly Func<IContentTreeNodeDisplayMetaViewModelBuilder> contentTreeNodeDisplayMetaViewModelBuilder;

        public ContentTreeController(Func<IContentTreeNodeDisplayViewModelBuilder> contentTreeNodeDisplayViewModelBuilder,
                                    Func<IContentTreeNodeDisplayMetaViewModelBuilder> contentTreeNodeDisplayMetaViewModelBuilder)
        {
            this.contentTreeNodeDisplayMetaViewModelBuilder = contentTreeNodeDisplayMetaViewModelBuilder;
            this.contentTreeNodeDisplayViewModelBuilder = contentTreeNodeDisplayViewModelBuilder;
        }

        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Duration = 28800, VaryByParam = "treeNodeId;actionId", VaryByCustom = "Bennington.ContentTree")]
        public virtual ActionResult Display(string treeNodeId, string actionId)
        {
            return PartialView("Display", contentTreeNodeDisplayViewModelBuilder().BuildViewModel(treeNodeId, actionId));
        }

        public virtual ActionResult DisplayMeta(string treeNodeId, string actionId)
        {
            return PartialView("DisplayMeta", contentTreeNodeDisplayMetaViewModelBuilder().BuildViewModel(treeNodeId, actionId));
        }
    }
}
