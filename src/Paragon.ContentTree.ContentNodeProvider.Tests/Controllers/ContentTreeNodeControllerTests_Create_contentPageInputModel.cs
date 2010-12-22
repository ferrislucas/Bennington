﻿using System.Web.Mvc;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerTests_Create_contentPageInputModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sets_ContentItemId_property_of_inputModel_to_Index_if_blank()
		{
			var contentPageInputMOdel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.Create(contentPageInputMOdel);

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.CreateTreeNodeAndReturnTreeNodeId(It.Is<ContentTreeNodeInputModel>(b => b.ContentItemId == "Index")), Times.Once());
		}

		[TestMethod]
		public void Returns_RedirectResult_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Create(contentTreeNodeInputModel);

			Assert.IsInstanceOfType(result, typeof(RedirectResult));
		}

		[TestMethod]
		public void Returns_view_model_with_input_model_set_to_same_input_model_that_was_passed_in_when_ModelState_is_invalid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.ModelState.AddModelError("key", "error");
			var result = contentTreeNodeController.Create(contentTreeNodeInputModel);

			Assert.AreEqual(contentTreeNodeInputModel, ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).ContentTreeNodeInputModel);
		}

		[TestMethod]
		public void Calls_Create_method_of_IContentTreeNodeContext_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
			                            	{
			                            		ParentTreeNodeId = "2",
			                            	};
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Create(contentTreeNodeInputModel);

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.CreateTreeNodeAndReturnTreeNodeId(contentTreeNodeInputModel), Times.Once());
		}

		[TestMethod]
		public void Does_not_call_Create_method_of_IContentTreeNodeContext_when_ModelState_is_invalid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
			{
				ParentTreeNodeId = "2",
			};
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.ModelState.AddModelError("key", "error");
			var result = contentTreeNodeController.Create(contentTreeNodeInputModel);

			mocker.GetMock<IContentTreeNodeContext>().Verify(a => a.CreateTreeNodeAndReturnTreeNodeId(contentTreeNodeInputModel), Times.Never());
		}

		[TestMethod]
		public void Sets_view_model_action_to_create_when_ModelState_is_not_valid()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.ModelState.AddModelError("key", "error");
			var result = contentTreeNodeController.Create(new ContentTreeNodeInputModel());

			Assert.AreEqual("Create", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).Action);
		}

		[TestMethod]
		public void Sends_CreatePageCommand_command_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
														};

			mocker.Resolve<ContentTreeNodeController>().Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<CreatePageCommand>()), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_command_with_correct_Body_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
															Content = "content"
														};

			mocker.Resolve<ContentTreeNodeController>().Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Body == contentTreeNodeInputModel.Content)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_command_with_correct_HeaderText_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
															Content = "content",
															Name = "header text"
														};

			mocker.Resolve<ContentTreeNodeController>().Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.HeaderText == contentTreeNodeInputModel.Name)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_command_with_correct_Sequence_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
															Content = "content",
															Name = "header text",
															Sequence = 100,
														};

			mocker.Resolve<ContentTreeNodeController>().Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Sequence == contentTreeNodeInputModel.Sequence)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_command_with_correct_UrlSegment_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
															Content = "content",
															Name = "header text",
															Sequence = 100,
															UrlSegment = "url segment"
														};

			mocker.Resolve<ContentTreeNodeController>().Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.UrlSegment == contentTreeNodeInputModel.UrlSegment)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_command_with_correct_ParentId_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
															Content = "content",
															Name = "header text",
															Sequence = 100,
															UrlSegment = "url segment",
														};

			mocker.Resolve<ContentTreeNodeController>().Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.ParentId == contentTreeNodeInputModel.ParentTreeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Does_not_send_CreatePageCommand_command_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															ParentTreeNodeId = "2",
														};

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.ModelState.AddModelError("key", "error");
			contentTreeNodeController.Create(contentTreeNodeInputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<CreatePageCommand>()), Times.Never());
		}
	}
}
