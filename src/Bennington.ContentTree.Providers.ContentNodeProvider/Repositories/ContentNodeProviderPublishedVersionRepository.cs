﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.Core.Helpers;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Repositories
{
    public interface IContentNodeProviderPublishedVersionRepository
    {
        IQueryable<ContentNodeProviderPublishedVersion> GetAllContentNodeProviderPublishedVersions();
        void Update(ContentNodeProviderPublishedVersion instance);
        void Create(ContentNodeProviderPublishedVersion instance);
        void Delete(ContentNodeProviderPublishedVersion instance);
    }

    public class ContentNodeProviderPublishedVersionRepository : IContentNodeProviderPublishedVersionRepository
    {
        private readonly IDatabaseRetriever databaseRetriever;
        private readonly IGetPathToDataDirectoryService getPathToDataDirectoryService;

        public ContentNodeProviderPublishedVersionRepository(IDatabaseRetriever databaseRetriever,
                                                            IGetPathToDataDirectoryService getPathToDataDirectoryService)
        {
            this.getPathToDataDirectoryService = getPathToDataDirectoryService;
            this.databaseRetriever = databaseRetriever;
        }

        public IQueryable<ContentNodeProviderPublishedVersion> GetAllContentNodeProviderPublishedVersions()
        {
            var db = databaseRetriever.GetDatabase();
            var list = new List<ContentNodeProviderPublishedVersion>();
            list.AddRange(db.ContentNodeProviderPublishedVersions.All().Cast<ContentNodeProviderPublishedVersion>());
            return list.AsQueryable();
        }

        public void Update(ContentNodeProviderPublishedVersion instance)
        {
            var db = databaseRetriever.GetDatabase();
            db.ContentNodeProviderPublishedVersions.UpdateByPageId(instance);
            TouchLegacyFilestorePathToInvalidateAnyCachesThatAreListeningForChanges();
        }

        public void Create(ContentNodeProviderPublishedVersion instance)
        {
            var db = databaseRetriever.GetDatabase();
            if (instance.LastModifyDate == DateTime.MinValue) instance.LastModifyDate = new DateTime(1753, 1, 1);
            db.ContentNodeProviderPublishedVersions.Insert(instance);
            TouchLegacyFilestorePathToInvalidateAnyCachesThatAreListeningForChanges();
        }

        public void Delete(ContentNodeProviderPublishedVersion instance)
        {
            var db = databaseRetriever.GetDatabase();
            db.ContentNodeProviderPublishedVersions.Delete(PageId: instance.PageId);
            TouchLegacyFilestorePathToInvalidateAnyCachesThatAreListeningForChanges();
        }

        private void TouchLegacyFilestorePathToInvalidateAnyCachesThatAreListeningForChanges()
        {
            var path = string.Format("{0}ContentNodeProviderPublishedVersions.xml", getPathToDataDirectoryService.GetPathToDirectory());

            if (!File.Exists(path))
            {
                using (var fileStream = File.Create(path))
                {
                }
            }

            using (var writer = File.AppendText(path))
            {
                writer.WriteLine(string.Empty);
            }
        }
    }
}