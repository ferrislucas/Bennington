﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bennington.Content.Sql.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Bennington")]
	public partial class ContentDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContentActionItem(ContentActionItem instance);
    partial void UpdateContentActionItem(ContentActionItem instance);
    partial void DeleteContentActionItem(ContentActionItem instance);
    partial void InsertContentTypeItem(ContentTypeItem instance);
    partial void UpdateContentTypeItem(ContentTypeItem instance);
    partial void DeleteContentTypeItem(ContentTypeItem instance);
    partial void InsertContentTreeItem(ContentTreeItem instance);
    partial void UpdateContentTreeItem(ContentTreeItem instance);
    partial void DeleteContentTreeItem(ContentTreeItem instance);
    #endregion
		
		public ContentDataContext() : 
				base(global::Bennington.Content.Sql.Properties.Settings.Default.BenningtonConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ContentDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ContentActionItem> ContentActionItems
		{
			get
			{
				return this.GetTable<ContentActionItem>();
			}
		}
		
		public System.Data.Linq.Table<ContentTypeItem> ContentTypeItems
		{
			get
			{
				return this.GetTable<ContentTypeItem>();
			}
		}
		
		public System.Data.Linq.Table<ContentTreeItem> ContentTreeItems
		{
			get
			{
				return this.GetTable<ContentTreeItem>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentActions")]
	public partial class ContentActionItem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ContentActionId;
		
		private int _ContentTypeId;
		
		private string _ContentType;
		
		private string _Action;
		
		private string _DisplayName;
		
		private EntityRef<ContentTypeItem> _ContentTypeItem;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnContentActionIdChanging(int value);
    partial void OnContentActionIdChanged();
    partial void OnContentTypeIdChanging(int value);
    partial void OnContentTypeIdChanged();
    partial void OnContentTypeChanging(string value);
    partial void OnContentTypeChanged();
    partial void OnActionChanging(string value);
    partial void OnActionChanged();
    partial void OnDisplayNameChanging(string value);
    partial void OnDisplayNameChanged();
    #endregion
		
		public ContentActionItem()
		{
			this._ContentTypeItem = default(EntityRef<ContentTypeItem>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentActionId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ContentActionId
		{
			get
			{
				return this._ContentActionId;
			}
			set
			{
				if ((this._ContentActionId != value))
				{
					this.OnContentActionIdChanging(value);
					this.SendPropertyChanging();
					this._ContentActionId = value;
					this.SendPropertyChanged("ContentActionId");
					this.OnContentActionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentTypeId", DbType="Int NOT NULL")]
		public int ContentTypeId
		{
			get
			{
				return this._ContentTypeId;
			}
			set
			{
				if ((this._ContentTypeId != value))
				{
					if (this._ContentTypeItem.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnContentTypeIdChanging(value);
					this.SendPropertyChanging();
					this._ContentTypeId = value;
					this.SendPropertyChanged("ContentTypeId");
					this.OnContentTypeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentType", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string ContentType
		{
			get
			{
				return this._ContentType;
			}
			set
			{
				if ((this._ContentType != value))
				{
					this.OnContentTypeChanging(value);
					this.SendPropertyChanging();
					this._ContentType = value;
					this.SendPropertyChanged("ContentType");
					this.OnContentTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Action", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				if ((this._Action != value))
				{
					this.OnActionChanging(value);
					this.SendPropertyChanging();
					this._Action = value;
					this.SendPropertyChanged("Action");
					this.OnActionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DisplayName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				if ((this._DisplayName != value))
				{
					this.OnDisplayNameChanging(value);
					this.SendPropertyChanging();
					this._DisplayName = value;
					this.SendPropertyChanged("DisplayName");
					this.OnDisplayNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ContentTypeItem_ContentActionItem", Storage="_ContentTypeItem", ThisKey="ContentTypeId", OtherKey="ContentTypeId", IsForeignKey=true)]
		public ContentTypeItem ContentTypeItem
		{
			get
			{
				return this._ContentTypeItem.Entity;
			}
			set
			{
				ContentTypeItem previousValue = this._ContentTypeItem.Entity;
				if (((previousValue != value) 
							|| (this._ContentTypeItem.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ContentTypeItem.Entity = null;
						previousValue.ContentActionItems.Remove(this);
					}
					this._ContentTypeItem.Entity = value;
					if ((value != null))
					{
						value.ContentActionItems.Add(this);
						this._ContentTypeId = value.ContentTypeId;
					}
					else
					{
						this._ContentTypeId = default(int);
					}
					this.SendPropertyChanged("ContentTypeItem");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentTypes")]
	public partial class ContentTypeItem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ContentTypeId;
		
		private string _Type;
		
		private string _ControllerName;
		
		private string _DisplayName;
		
		private EntitySet<ContentActionItem> _ContentActionItems;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnContentTypeIdChanging(int value);
    partial void OnContentTypeIdChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnControllerNameChanging(string value);
    partial void OnControllerNameChanged();
    partial void OnDisplayNameChanging(string value);
    partial void OnDisplayNameChanged();
    #endregion
		
		public ContentTypeItem()
		{
			this._ContentActionItems = new EntitySet<ContentActionItem>(new Action<ContentActionItem>(this.attach_ContentActionItems), new Action<ContentActionItem>(this.detach_ContentActionItems));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentTypeId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ContentTypeId
		{
			get
			{
				return this._ContentTypeId;
			}
			set
			{
				if ((this._ContentTypeId != value))
				{
					this.OnContentTypeIdChanging(value);
					this.SendPropertyChanging();
					this._ContentTypeId = value;
					this.SendPropertyChanged("ContentTypeId");
					this.OnContentTypeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ControllerName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ControllerName
		{
			get
			{
				return this._ControllerName;
			}
			set
			{
				if ((this._ControllerName != value))
				{
					this.OnControllerNameChanging(value);
					this.SendPropertyChanging();
					this._ControllerName = value;
					this.SendPropertyChanged("ControllerName");
					this.OnControllerNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DisplayName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				if ((this._DisplayName != value))
				{
					this.OnDisplayNameChanging(value);
					this.SendPropertyChanging();
					this._DisplayName = value;
					this.SendPropertyChanged("DisplayName");
					this.OnDisplayNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ContentTypeItem_ContentActionItem", Storage="_ContentActionItems", ThisKey="ContentTypeId", OtherKey="ContentTypeId")]
		public EntitySet<ContentActionItem> ContentActionItems
		{
			get
			{
				return this._ContentActionItems;
			}
			set
			{
				this._ContentActionItems.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ContentActionItems(ContentActionItem entity)
		{
			this.SendPropertyChanging();
			entity.ContentTypeItem = this;
		}
		
		private void detach_ContentActionItems(ContentActionItem entity)
		{
			this.SendPropertyChanging();
			entity.ContentTypeItem = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentTree")]
	public partial class ContentTreeItem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Id;
		
		private string _ParentId;
		
		private string _Segment;
		
		private string _Action;
		
		private string _Controller;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(string value);
    partial void OnIdChanged();
    partial void OnParentIdChanging(string value);
    partial void OnParentIdChanged();
    partial void OnSegmentChanging(string value);
    partial void OnSegmentChanged();
    partial void OnActionChanging(string value);
    partial void OnActionChanged();
    partial void OnControllerChanging(string value);
    partial void OnControllerChanged();
    #endregion
		
		public ContentTreeItem()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParentId", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ParentId
		{
			get
			{
				return this._ParentId;
			}
			set
			{
				if ((this._ParentId != value))
				{
					this.OnParentIdChanging(value);
					this.SendPropertyChanging();
					this._ParentId = value;
					this.SendPropertyChanged("ParentId");
					this.OnParentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Segment", DbType="NVarChar(500)")]
		public string Segment
		{
			get
			{
				return this._Segment;
			}
			set
			{
				if ((this._Segment != value))
				{
					this.OnSegmentChanging(value);
					this.SendPropertyChanging();
					this._Segment = value;
					this.SendPropertyChanged("Segment");
					this.OnSegmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Action", DbType="NVarChar(500)")]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				if ((this._Action != value))
				{
					this.OnActionChanging(value);
					this.SendPropertyChanging();
					this._Action = value;
					this.SendPropertyChanged("Action");
					this.OnActionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Controller", DbType="NVarChar(500)")]
		public string Controller
		{
			get
			{
				return this._Controller;
			}
			set
			{
				if ((this._Controller != value))
				{
					this.OnControllerChanging(value);
					this.SendPropertyChanging();
					this._Controller = value;
					this.SendPropertyChanged("Controller");
					this.OnControllerChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
