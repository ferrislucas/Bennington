﻿using System.Collections.Generic;
using System.Linq;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Context
{
	public interface IContentTreeNodeContext
	{
		string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel);
		IEnumerable<ContentTreePageNode> GetContentTreeNodesByTreeId(string nodeId);
	}

	public class ContentTreeNodeContext : IContentTreeNodeContext
	{
        public const string RootNodeId = ContentTree.RootNodeId;
		
		private readonly IContentTreeNodeVersionContext contentTreeNodeVersionContext;
		private readonly IContentTree contentTree;

		public ContentTreeNodeContext(IContentTreeNodeVersionContext contentTreeNodeVersionContext, 
										IContentTree contentTree)
		{
			this.contentTree = contentTree;
			this.contentTreeNodeVersionContext = contentTreeNodeVersionContext;
		}

		public string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			var newTreeNodeId = contentTree.Create(contentTreeNodeInputModel.ParentTreeNodeId, contentTreeNodeInputModel.Type, contentTreeNodeInputModel.ControllerName);
			contentTreeNodeInputModel.TreeNodeId = newTreeNodeId;
			return contentTreeNodeInputModel.TreeNodeId;
		}

		public IEnumerable<ContentTreePageNode> GetContentTreeNodesByTreeId(string treeNodeId)
		{
			var contentTreeNodes = contentTreeNodeVersionContext.GetAllContentTreeNodes().Where(a => a.Id == treeNodeId);
			return contentTreeNodes.ToArray();
		}
	}
}
