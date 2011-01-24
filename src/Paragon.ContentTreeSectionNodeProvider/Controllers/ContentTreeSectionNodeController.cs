﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Domain.Commands;
using Paragon.ContentTree.SectionNodeProvider.Context;
using Paragon.ContentTree.SectionNodeProvider.Mappers;
using Paragon.ContentTree.SectionNodeProvider.Models;
using Paragon.ContentTree.SectionNodeProvider.Repositories;
using Paragon.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.SectionNodeProvider.Controllers
{
	public class ContentTreeSectionNodeController : Controller
	{
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;
		private readonly IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper;
		private readonly IContentTreeSectionInputModelToContentTreeSectionNodeMapper contentTreeSectionInputModelToContentTreeSectionNodeMapper;
		private readonly IContentTreeSectionNodeContext contentTreeSectionNodeContext;
		private readonly ICommandBus commandBus;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly IGuidGetter guidGetter;

		public ContentTreeSectionNodeController(IContentTreeSectionNodeRepository contentTreeSectionNodeRepository, 
												IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper, 
												IContentTreeSectionInputModelToContentTreeSectionNodeMapper contentTreeSectionInputModelToContentTreeSectionNodeMapper, 
												IContentTreeSectionNodeContext contentTreeSectionNodeContext,
												ICommandBus commandBus,
												ITreeNodeSummaryContext treeNodeSummaryContext,
												IGuidGetter guidGetter)
		{
			this.guidGetter = guidGetter;
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.commandBus = commandBus;
			this.contentTreeSectionNodeContext = contentTreeSectionNodeContext;
			this.contentTreeSectionInputModelToContentTreeSectionNodeMapper = contentTreeSectionInputModelToContentTreeSectionNodeMapper;
			this.contentTreeSectionNodeToContentTreeSectionInputModelMapper = contentTreeSectionNodeToContentTreeSectionInputModelMapper;
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
		}

		public ActionResult Delete(string treeNodeId)
		{
			commandBus.Send(new DeleteSectionCommand(){ AggregateRootId = new Guid(treeNodeId) });
			contentTreeSectionNodeContext.Delete(treeNodeId);
			return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "ContentTree" }, { "action", "Index" } });
		}

		[HttpPost]
		public ActionResult Create(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeSectionNodeViewModel()
				                      	{
				                      		ContentTreeSectionInputModel = contentTreeSectionInputModel,
											Action = "Create",
				                      	});

			var treeNodeId = treeNodeSummaryContext.Create(contentTreeSectionInputModel.ParentTreeNodeId, typeof(SectionNodeProvider).AssemblyQualifiedName);
			
			commandBus.Send(new CreateSectionCommand()
			                	{
									SectionId = guidGetter.GetGuid().ToString(),
									TreeNodeId = treeNodeId,
			                		DefaultTreeNodeId = contentTreeSectionInputModel.DefaultTreeNodeId,
									Name = contentTreeSectionInputModel.Name,
									ParentTreeNodeId = contentTreeSectionInputModel.ParentTreeNodeId,
									Sequence = contentTreeSectionInputModel.Sequence,
									UrlSegment = contentTreeSectionInputModel.UrlSegment,
			                	});

			if (contentTreeSectionInputModel.Action.ToLower() == "save and exit")
				return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});

			return new RedirectResult(GetRedirectUrlToModifyMethod(contentTreeSectionInputModel));
		}

		public ActionResult Create(string parentTreeNodeId)
		{
			return View("Modify", new ContentTreeSectionNodeViewModel()
			                      	{
										Action = "Create",
			                      		ContentTreeSectionInputModel = new ContentTreeSectionInputModel()
			                      		                        	{
			                      		                        		ParentTreeNodeId = parentTreeNodeId,
			                      		                        	}
			                      	});
		}

		[HttpPost]
		public ActionResult Modify(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeSectionNodeViewModel() { Action = "Modify", ContentTreeSectionInputModel = contentTreeSectionInputModel });

			commandBus.Send(new ModifySectionCommand()
			                	{
									TreeNodeId = contentTreeSectionInputModel.TreeNodeId,
			                		DefaultTreeNodeId = contentTreeSectionInputModel.DefaultTreeNodeId,
									ParentTreeNodeId = contentTreeSectionInputModel.ParentTreeNodeId,
									UrlSegment = contentTreeSectionInputModel.UrlSegment,
									Sequence = contentTreeSectionInputModel.Sequence,
									Name = contentTreeSectionInputModel.Name,
			                	});

			if (contentTreeSectionInputModel.Action != null)
			{
				if (contentTreeSectionInputModel.Action.ToLower() == "save and exit")
					return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});
			}

			return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTreeSectionNode"},
													{"action", "Modify"},
													{ "treeNodeId", contentTreeSectionInputModel == null ? "0" : contentTreeSectionInputModel.TreeNodeId },
			                                 	});
		}

		public ActionResult Modify(string treeNodeId)
		{
			var viewData = ViewData;

			var contentTreeSection = contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == treeNodeId).FirstOrDefault();
			var contentTreeSectionInputModel = contentTreeSection == null ? new ContentTreeSectionInputModel()
			                		                        	{
			                		                        		TreeNodeId = treeNodeId,
			                		                        	} 
										: contentTreeSectionNodeToContentTreeSectionInputModelMapper.CreateInstance(contentTreeSection);

			var viewModel = new ContentTreeSectionNodeViewModel()
			                	{
									Action = "Modify",
			                		ContentTreeSectionInputModel = contentTreeSectionInputModel,
			                	};

			return View("Modify", viewModel);
		}

		private string GetRedirectUrlToModifyMethod(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ContentTreeSectionNode", new { treeNodeId = contentTreeSectionInputModel == null ? "0" : contentTreeSectionInputModel.TreeNodeId });
		}
	}
}
