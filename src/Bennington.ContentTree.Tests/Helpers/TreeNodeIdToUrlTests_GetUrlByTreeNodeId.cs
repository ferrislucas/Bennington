﻿using AutoMoq;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bennington.ContentTree.Tests.Helpers
{
	[TestClass]
	public class TreeNodeIdToUrlTests_GetUrlByTreeNodeId
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Returns_null_when_id_is_not_found()
		{
			mocker.GetMock<IContentTree>().Setup(a => a.GetById("1"))
				.Returns(new ContentTreeNode()
				{
					Id = "1",
					UrlSegment = "segment1",
                    ParentTreeNodeId = ContentTree.RootNodeId
				});

			var url = mocker.Resolve<TreeNodeIdToUrl>().GetUrlByTreeNodeId("2");

			Assert.IsNull(url);
		}

		[TestMethod]
		public void Returns_correct_url_for_first_level_node()
		{
			mocker.GetMock<IContentTree>().Setup(a => a.GetById("1"))
				.Returns(new ContentTreeNode()
				{
					Id = "1",
					UrlSegment = "segment1",
					ParentTreeNodeId = ContentTree.RootNodeId
				});

			var url = mocker.Resolve<TreeNodeIdToUrl>().GetUrlByTreeNodeId("1");

			Assert.AreEqual("/segment1", url);
		}

		[TestMethod]
		public void Returns_correct_url_for_second_level_node()
		{
			mocker.GetMock<IContentTree>().Setup(a => a.GetById("1"))
				.Returns(new ContentTreeNode()
				         			{
				         				Id = "1",
										UrlSegment = "segment1",
										ParentTreeNodeId = ContentTree.RootNodeId
				         			});
			mocker.GetMock<IContentTree>().Setup(a => a.GetById("2"))
				.Returns(new ContentTreeNode()
				         			{
				         				Id = "2",
										UrlSegment = "segment2",
										ParentTreeNodeId = "1",
				         			} 
							);

			var url = mocker.Resolve<TreeNodeIdToUrl>().GetUrlByTreeNodeId("2");

			Assert.AreEqual("/segment1/segment2", url);
		}
	}
}
