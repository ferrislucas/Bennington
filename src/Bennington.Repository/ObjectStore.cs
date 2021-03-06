﻿using System;
using System.Collections.Generic;
using Bennington.Core.Helpers;
using Bennington.Repository.Helpers;

namespace Bennington.Repository
{
	public interface IObjectStore<T>
	{
		IEnumerable<T> GetAll();
		string SaveAndReturnId(T instance);
		T GetById(string id);
		void Delete(string id);
	}

	public class ObjectStore<T> : IObjectStore<T>
	{
		private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;
		private readonly IGetDataPathForType getDataPathForType;
		private readonly IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance;
		private readonly IGuidGetter guidGetter;
		private readonly IFileSystem fileSystem;

		public ObjectStore(IXmlFileSerializationHelper xmlFileSerializationHelper,
							IGetDataPathForType getDataPathForType,
							IGetValueOfIdPropertyForInstance getValueOfIdPropertyForInstance,
							IGuidGetter guidGetter,
							IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
			this.guidGetter = guidGetter;
			this.getValueOfIdPropertyForInstance = getValueOfIdPropertyForInstance;
			this.getDataPathForType = getDataPathForType;
			this.xmlFileSerializationHelper = xmlFileSerializationHelper;
		}

		public IEnumerable<T> GetAll()
		{
			var path = getDataPathForType.GetPathForDataByType(typeof(T));
			if (!fileSystem.DirectoryExists(path)) return new T[] { };

			var set = new List<T>();
			var filePaths = fileSystem.GetFiles(path, "*.xml");
			foreach (var filePath in filePaths)
			{
				set.Add(xmlFileSerializationHelper.DeserializeFromPath<T>(filePath));
			}
			
			return set;
		}

		public string SaveAndReturnId(T instance)
		{
			var idValue = getValueOfIdPropertyForInstance.GetId(instance) ?? guidGetter.GetGuid().ToString();
			var path = string.Format("{0}{1}.xml", getDataPathForType.GetPathForDataByType(typeof(T)), idValue);
			xmlFileSerializationHelper.SerializeToPath(instance, path);
			return idValue;
		}

		public T GetById(string id)
		{
			var path = string.Format("{0}{1}.xml", getDataPathForType.GetPathForDataByType(typeof(T)) , id);
			
			if (!fileSystem.FileExists(path)) 
				return default(T);
			
			return xmlFileSerializationHelper.DeserializeFromPath<T>(path);
		}

		public void Delete(string id)
		{
			var path = getDataPathForType.GetPathForDataByType(typeof(T)) + id + ".xml";
			if (fileSystem.FileExists(path))
				fileSystem.DeleteFile(path);
		}
	}
}
