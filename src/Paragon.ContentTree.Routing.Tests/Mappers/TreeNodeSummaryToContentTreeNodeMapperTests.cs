﻿using AutoMapperAssist;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.Routing.Mappers;

namespace Paragon.ContentTree.Routing.Tests.Mappers
{
	[TestClass]
	public class TreeNodeSummaryToContentTreeNodeMapperTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Assert_config_is_valid()
		{
			mocker.Resolve<TreeNodeSummaryToContentTreeNodeMapper>().AssertConfigurationIsValid();
		}
	}
}