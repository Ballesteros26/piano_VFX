using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a business object that provides data to data-bound controls in multitier Web application architectures.</summary>
	// Token: 0x020003DE RID: 990
	[PersistChildren(false)]
	[DefaultEvent("Selecting")]
	[DefaultProperty("TypeName")]
	[Designer("System.Web.UI.Design.WebControls.ObjectDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true)]
	[ToolboxBitmap("bitmap file goes here")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSource : DataSourceControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class.</summary>
		// Token: 0x06002A9F RID: 10911 RVA: 0x000711D4 File Offset: 0x0006F3D4
		public ObjectDataSource()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class with the specified type name and data retrieval method name.</summary>
		/// <param name="typeName">The name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> works with. </param>
		/// <param name="selectMethod">The name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> invokes to retrieve data. </param>
		// Token: 0x06002AA0 RID: 10912 RVA: 0x000711DC File Offset: 0x0006F3DC
		public ObjectDataSource(string typeName, string selectMethod)
		{
			this.SelectMethod = selectMethod;
			this.TypeName = typeName;
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x000711F2 File Offset: 0x0006F3F2
		private ObjectDataSourceView DefaultView
		{
			get
			{
				if (this.defaultView == null)
				{
					this.defaultView = new ObjectDataSourceView(this, ObjectDataSource.emptyNames[0], this.Context);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.defaultView).TrackViewState();
					}
				}
				return this.defaultView;
			}
		}

		/// <summary>Occurs when a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation has completed.</summary>
		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06002AA2 RID: 10914 RVA: 0x0007122E File Offset: 0x0006F42E
		// (remove) Token: 0x06002AA3 RID: 10915 RVA: 0x0007123C File Offset: 0x0006F43C
		public event ObjectDataSourceStatusEventHandler Deleted
		{
			add
			{
				this.DefaultView.Deleted += value;
			}
			remove
			{
				this.DefaultView.Deleted -= value;
			}
		}

		/// <summary>Occurs before a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation.</summary>
		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06002AA4 RID: 10916 RVA: 0x0007124A File Offset: 0x0006F44A
		// (remove) Token: 0x06002AA5 RID: 10917 RVA: 0x00071258 File Offset: 0x0006F458
		public event ObjectDataSourceMethodEventHandler Deleting
		{
			add
			{
				this.DefaultView.Deleting += value;
			}
			remove
			{
				this.DefaultView.Deleting -= value;
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06002AA6 RID: 10918 RVA: 0x00071266 File Offset: 0x0006F466
		// (remove) Token: 0x06002AA7 RID: 10919 RVA: 0x00071274 File Offset: 0x0006F474
		public event ObjectDataSourceFilteringEventHandler Filtering
		{
			add
			{
				this.DefaultView.Filtering += value;
			}
			remove
			{
				this.DefaultView.Filtering -= value;
			}
		}

		/// <summary>Occurs when an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" /> operation has completed.</summary>
		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06002AA8 RID: 10920 RVA: 0x00071282 File Offset: 0x0006F482
		// (remove) Token: 0x06002AA9 RID: 10921 RVA: 0x00071290 File Offset: 0x0006F490
		public event ObjectDataSourceStatusEventHandler Inserted
		{
			add
			{
				this.DefaultView.Inserted += value;
			}
			remove
			{
				this.DefaultView.Inserted -= value;
			}
		}

		/// <summary>Occurs before an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" /> operation.</summary>
		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06002AAA RID: 10922 RVA: 0x0007129E File Offset: 0x0006F49E
		// (remove) Token: 0x06002AAB RID: 10923 RVA: 0x000712AC File Offset: 0x0006F4AC
		public event ObjectDataSourceMethodEventHandler Inserting
		{
			add
			{
				this.DefaultView.Inserting += value;
			}
			remove
			{
				this.DefaultView.Inserting -= value;
			}
		}

		/// <summary>Occurs after the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is created.</summary>
		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06002AAC RID: 10924 RVA: 0x000712BA File Offset: 0x0006F4BA
		// (remove) Token: 0x06002AAD RID: 10925 RVA: 0x000712C8 File Offset: 0x0006F4C8
		public event ObjectDataSourceObjectEventHandler ObjectCreated
		{
			add
			{
				this.DefaultView.ObjectCreated += value;
			}
			remove
			{
				this.DefaultView.ObjectCreated -= value;
			}
		}

		/// <summary>Occurs before the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is created.</summary>
		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06002AAE RID: 10926 RVA: 0x000712D6 File Offset: 0x0006F4D6
		// (remove) Token: 0x06002AAF RID: 10927 RVA: 0x000712E4 File Offset: 0x0006F4E4
		public event ObjectDataSourceObjectEventHandler ObjectCreating
		{
			add
			{
				this.DefaultView.ObjectCreating += value;
			}
			remove
			{
				this.DefaultView.ObjectCreating -= value;
			}
		}

		/// <summary>Occurs before the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is discarded.</summary>
		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06002AB0 RID: 10928 RVA: 0x000712F2 File Offset: 0x0006F4F2
		// (remove) Token: 0x06002AB1 RID: 10929 RVA: 0x00071300 File Offset: 0x0006F500
		public event ObjectDataSourceDisposingEventHandler ObjectDisposing
		{
			add
			{
				this.DefaultView.ObjectDisposing += value;
			}
			remove
			{
				this.DefaultView.ObjectDisposing -= value;
			}
		}

		/// <summary>Occurs when a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> operation has completed.</summary>
		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06002AB2 RID: 10930 RVA: 0x0007130E File Offset: 0x0006F50E
		// (remove) Token: 0x06002AB3 RID: 10931 RVA: 0x0007131C File Offset: 0x0006F51C
		public event ObjectDataSourceStatusEventHandler Selected
		{
			add
			{
				this.DefaultView.Selected += value;
			}
			remove
			{
				this.DefaultView.Selected -= value;
			}
		}

		/// <summary>Occurs before a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> operation.</summary>
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06002AB4 RID: 10932 RVA: 0x0007132A File Offset: 0x0006F52A
		// (remove) Token: 0x06002AB5 RID: 10933 RVA: 0x00071338 File Offset: 0x0006F538
		public event ObjectDataSourceSelectingEventHandler Selecting
		{
			add
			{
				this.DefaultView.Selecting += value;
			}
			remove
			{
				this.DefaultView.Selecting -= value;
			}
		}

		/// <summary>Occurs when an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> operation has completed.</summary>
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06002AB6 RID: 10934 RVA: 0x00071346 File Offset: 0x0006F546
		// (remove) Token: 0x06002AB7 RID: 10935 RVA: 0x00071354 File Offset: 0x0006F554
		public event ObjectDataSourceStatusEventHandler Updated
		{
			add
			{
				this.DefaultView.Updated += value;
			}
			remove
			{
				this.DefaultView.Updated -= value;
			}
		}

		/// <summary>Occurs before an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> operation.</summary>
		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06002AB8 RID: 10936 RVA: 0x00071362 File Offset: 0x0006F562
		// (remove) Token: 0x06002AB9 RID: 10937 RVA: 0x00071370 File Offset: 0x0006F570
		public event ObjectDataSourceMethodEventHandler Updating
		{
			add
			{
				this.DefaultView.Updating += value;
			}
			remove
			{
				this.DefaultView.Updating -= value;
			}
		}

		/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data that is retrieved by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</summary>
		/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> caches the results of a <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property invocation. The default is 0. The value cannot be negative.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x0007137E File Offset: 0x0006F57E
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x00071386 File Offset: 0x0006F586
		[DefaultValue(0)]
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		public virtual int CacheDuration
		{
			get
			{
				return this.cacheDuration;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "The duration must be non-negative");
				}
				this.cacheDuration = value;
			}
		}

		/// <summary>Gets or sets the cache expiration behavior that, when combined with the duration, describes the behavior of the cache that the data source control uses.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default is <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" />.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x000713A3 File Offset: 0x0006F5A3
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x000713AB File Offset: 0x0006F5AB
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this.cacheExpirationPolicy;
			}
			set
			{
				this.cacheExpirationPolicy = value;
			}
		}

		/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects that are created by the data source control.</summary>
		/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000713B4 File Offset: 0x0006F5B4
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x000713CA File Offset: 0x0006F5CA
		[DefaultValue("")]
		public virtual string CacheKeyDependency
		{
			get
			{
				if (this.cacheKeyDependency == null)
				{
					return string.Empty;
				}
				return this.cacheKeyDependency;
			}
			set
			{
				this.cacheKeyDependency = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether or not just the new values are passed to the Update method or both the old and new values are passed to the Update method.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" />.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000713D3 File Offset: 0x0006F5D3
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x000713E0 File Offset: 0x0006F5E0
		[WebCategory("Data")]
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.DefaultView.ConflictDetection;
			}
			set
			{
				this.DefaultView.ConflictDetection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> values that are passed to an update, insert, or delete operation are automatically converted from null to the <see cref="F:System.DBNull.Value" /> value by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
		/// <returns>true, if any null values in <see cref="T:System.Web.UI.WebControls.Parameter" /> objects passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control are automatically converted to <see cref="F:System.DBNull.Value" /> values; otherwise, false. The default is false.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000713EE File Offset: 0x0006F5EE
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x000713FB File Offset: 0x0006F5FB
		[DefaultValue(false)]
		public bool ConvertNullToDBNull
		{
			get
			{
				return this.DefaultView.ConvertNullToDBNull;
			}
			set
			{
				this.DefaultView.ConvertNullToDBNull = value;
			}
		}

		/// <summary>Gets or sets the name of a class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses for a parameter in an update, insert, or delete data operation, instead of passing individual values from the data-bound control.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> can use as a parameter for an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" />, <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />, or a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation. The default is an empty string ("").</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x00071409 File Offset: 0x0006F609
		// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x00071416 File Offset: 0x0006F616
		[DefaultValue("")]
		[WebCategory("Data")]
		public string DataObjectTypeName
		{
			get
			{
				return this.DefaultView.DataObjectTypeName;
			}
			set
			{
				this.DefaultView.DataObjectTypeName = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to delete data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to delete data. The default is an empty string ("").</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x00071424 File Offset: 0x0006F624
		// (set) Token: 0x06002AC7 RID: 10951 RVA: 0x00071431 File Offset: 0x0006F631
		[DefaultValue("")]
		[WebCategory("Data")]
		public string DeleteMethod
		{
			get
			{
				return this.DefaultView.DeleteMethod;
			}
			set
			{
				this.DefaultView.DeleteMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> method.</returns>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x0007143F File Offset: 0x0006F63F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.DefaultView.DeleteParameters;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control has data caching enabled.</summary>
		/// <returns>true if data caching is enabled for the data source control; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSource.EnableCaching" /> property is set to true when the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property returns a <see cref="T:System.Data.Common.DbDataReader" />.</exception>
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x0007144C File Offset: 0x0006F64C
		// (set) Token: 0x06002ACA RID: 10954 RVA: 0x00071454 File Offset: 0x0006F654
		[DefaultValue(false)]
		public virtual bool EnableCaching
		{
			get
			{
				return this.enableCaching;
			}
			set
			{
				this.enableCaching = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the data source control supports paging through the set of data that it retrieves.</summary>
		/// <returns>true if the data source control supports paging through the data it retrieves; otherwise, false.</returns>
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x0007145D File Offset: 0x0006F65D
		// (set) Token: 0x06002ACC RID: 10956 RVA: 0x0007146A File Offset: 0x0006F66A
		[WebCategory("Paging")]
		[DefaultValue(false)]
		public bool EnablePaging
		{
			get
			{
				return this.DefaultView.EnablePaging;
			}
			set
			{
				this.DefaultView.EnablePaging = value;
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property is called.</summary>
		/// <returns>A string that represents a filtering expression that is applied when data is retrieved by using the method or function identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method does not return a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />. </exception>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x00071478 File Offset: 0x0006F678
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x00071485 File Offset: 0x0006F685
		[WebCategory("Data")]
		[DefaultValue("")]
		public string FilterExpression
		{
			get
			{
				return this.DefaultView.FilterExpression;
			}
			set
			{
				this.DefaultView.FilterExpression = value;
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">You set the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property and the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method does not return a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />. </exception>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x00071493 File Offset: 0x0006F693
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[WebCategory("Data")]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.DefaultView.FilterParameters;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to insert data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to insert data. The default is an empty string ("").</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000714A0 File Offset: 0x0006F6A0
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x000714AD File Offset: 0x0006F6AD
		[DefaultValue("")]
		[WebCategory("Data")]
		public string InsertMethod
		{
			get
			{
				return this.DefaultView.InsertMethod;
			}
			set
			{
				this.DefaultView.InsertMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x000714BB File Offset: 0x0006F6BB
		[MergableProperty(false)]
		[WebCategory("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.DefaultView.InsertParameters;
			}
		}

		/// <summary>Gets or sets the name of the business object data retrieval method parameter that is used to indicate the number of records to retrieve for data source paging support.</summary>
		/// <returns>The name of the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> parameter that is used to indicate the number of records to retrieve. The default is "maximumRows".</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000714C8 File Offset: 0x0006F6C8
		// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x000714D5 File Offset: 0x0006F6D5
		[DefaultValue("maximumRows")]
		[WebCategory("Paging")]
		public string MaximumRowsParameterName
		{
			get
			{
				return this.DefaultView.MaximumRowsParameterName;
			}
			set
			{
				this.DefaultView.MaximumRowsParameterName = value;
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of the parameters for original values that are passed to the Delete or Update methods.</summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> or key parameters passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> methods. The default is "{0}", which means the parameter name is the field name.</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000714E3 File Offset: 0x0006F6E3
		// (set) Token: 0x06002AD6 RID: 10966 RVA: 0x000714F0 File Offset: 0x0006F6F0
		[WebCategory("Data")]
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				return this.DefaultView.OldValuesParameterFormatString;
			}
			set
			{
				this.DefaultView.OldValuesParameterFormatString = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to retrieve a row count.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to retrieve a row count. The method must return an integer (<see cref="T:System.Int32" />). The default is an empty string ("").</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000714FE File Offset: 0x0006F6FE
		// (set) Token: 0x06002AD8 RID: 10968 RVA: 0x0007150B File Offset: 0x0006F70B
		[WebCategory("Paging")]
		[DefaultValue("")]
		public string SelectCountMethod
		{
			get
			{
				return this.DefaultView.SelectCountMethod;
			}
			set
			{
				this.DefaultView.SelectCountMethod = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to retrieve data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to retrieve data. The default is an empty string ("").</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x00071519 File Offset: 0x0006F719
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x00071526 File Offset: 0x0006F726
		[WebCategory("Data")]
		[DefaultValue("")]
		public string SelectMethod
		{
			get
			{
				return this.DefaultView.SelectMethod;
			}
			set
			{
				this.DefaultView.SelectMethod = value;
			}
		}

		/// <summary>Gets a collection of parameters that are used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</summary>
		/// <returns>A collection of parameters that are used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x00071534 File Offset: 0x0006F734
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.DefaultView.SelectParameters;
			}
		}

		/// <summary>Gets or sets the name of the business object that the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> parameter used to specify a sort expression for data source sorting support.</summary>
		/// <returns>The name of the method parameter used to indicate the parameter which is used to sort the data. The default is an empty string.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x00071541 File Offset: 0x0006F741
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x0007154E File Offset: 0x0006F74E
		[DefaultValue("")]
		[WebCategory("Data")]
		public string SortParameterName
		{
			get
			{
				return this.DefaultView.SortParameterName;
			}
			set
			{
				this.DefaultView.SortParameterName = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited string that indicates which databases and tables to use for the Microsoft SQL Server cache dependency.</summary>
		/// <returns>A string that indicates which databases and tables to use for the SQL Server cache dependency.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0007155C File Offset: 0x0006F75C
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x00071572 File Offset: 0x0006F772
		[global::System.MonoTODO("SQLServer specific")]
		[DefaultValue("")]
		public virtual string SqlCacheDependency
		{
			get
			{
				if (this.sqlCacheDependency == null)
				{
					return string.Empty;
				}
				return this.sqlCacheDependency;
			}
			set
			{
				this.sqlCacheDependency = value;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the value of the identifier of the first record to retrieve for data source paging support.</summary>
		/// <returns>The name of the business object method parameter used to indicate the first record to retrieve. The parameter must return an integer value. The default is "startRowIndex".</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x0007157B File Offset: 0x0006F77B
		// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x00071588 File Offset: 0x0006F788
		[DefaultValue("startRowIndex")]
		[WebCategory("Paging")]
		public string StartRowIndexParameterName
		{
			get
			{
				return this.DefaultView.StartRowIndexParameterName;
			}
			set
			{
				this.DefaultView.StartRowIndexParameterName = value;
			}
		}

		/// <summary>Gets or sets the name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object represents.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> represents. The default is an empty string ("").</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x00071596 File Offset: 0x0006F796
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x000715A3 File Offset: 0x0006F7A3
		[DefaultValue("")]
		[WebCategory("Data")]
		public string TypeName
		{
			get
			{
				return this.DefaultView.TypeName;
			}
			set
			{
				this.DefaultView.TypeName = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to update data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to update data. The default is an empty string.</returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000715B1 File Offset: 0x0006F7B1
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x000715BE File Offset: 0x0006F7BE
		[DefaultValue("")]
		[WebCategory("Data")]
		public string UpdateMethod
		{
			get
			{
				return this.DefaultView.UpdateMethod;
			}
			set
			{
				this.DefaultView.UpdateMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000715CC File Offset: 0x0006F7CC
		[WebCategory("Data")]
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.DefaultView.UpdateParameters;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000715D9 File Offset: 0x0006F7D9
		internal DataSourceCacheManager Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new DataSourceCacheManager(this.CacheDuration, this.CacheKeyDependency, this.CacheExpirationPolicy, this, this.Context);
				}
				return this.cache;
			}
		}

		/// <summary>Retrieves the named data source view that is associated with the data source control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> named DefaultView that is associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		/// <param name="viewName">The name of the view to retrieve. Because the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> supports only one view, <paramref name="viewName" /> is ignored. </param>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="viewName" /> is null or something other than DefaultView. </exception>
		// Token: 0x06002AE8 RID: 10984 RVA: 0x0007160D File Offset: 0x0006F80D
		protected override DataSourceView GetView(string viewName)
		{
			if (viewName == null)
			{
				throw new ArgumentException("viewName");
			}
			return this.DefaultView;
		}

		/// <summary>Retrieves a collection of names representing the list of view objects that are associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		// Token: 0x06002AE9 RID: 10985 RVA: 0x00071623 File Offset: 0x0006F823
		protected override ICollection GetViewNames()
		{
			return ObjectDataSource.emptyNames;
		}

		/// <summary>Retrieves data from the underlying data storage by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property with the parameters in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		// Token: 0x06002AEA RID: 10986 RVA: 0x0007162A File Offset: 0x0006F82A
		public IEnumerable Select()
		{
			return this.DefaultView.Select(DataSourceSelectArguments.Empty);
		}

		/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property and any parameters that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows updated in the underlying data storage.</returns>
		// Token: 0x06002AEB RID: 10987 RVA: 0x0007163C File Offset: 0x0006F83C
		public int Update()
		{
			Hashtable hashtable = new Hashtable();
			return this.DefaultView.Update(hashtable, hashtable, null);
		}

		/// <summary>Performs a delete operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> property with any parameters that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows deleted from the underlying data storage, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs.AffectedRows" /> property of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> is set in the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> event; otherwise, -1.</returns>
		// Token: 0x06002AEC RID: 10988 RVA: 0x00071660 File Offset: 0x0006F860
		public int Delete()
		{
			Hashtable hashtable = new Hashtable();
			return this.DefaultView.Delete(hashtable, null);
		}

		/// <summary>Performs an insert operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property and any parameters in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows inserted into the underlying data storage.</returns>
		// Token: 0x06002AED RID: 10989 RVA: 0x00071680 File Offset: 0x0006F880
		public int Insert()
		{
			Hashtable hashtable = new Hashtable();
			return this.DefaultView.Insert(hashtable);
		}

		/// <summary>Adds a <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler to the page that contains the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06002AEE RID: 10990 RVA: 0x0007169F File Offset: 0x0006F89F
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000716B8 File Offset: 0x0006F8B8
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.FilterParameters.UpdateValues(this.Context, this);
			this.SelectParameters.UpdateValues(this.Context, this);
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control. </summary>
		/// <param name="savedState">An object that contains the saved view state values for the control. </param>
		// Token: 0x06002AF0 RID: 10992 RVA: 0x000716E0 File Offset: 0x0006F8E0
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				((IStateManager)this.DefaultView).LoadViewState(null);
				return;
			}
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			((IStateManager)this.DefaultView).LoadViewState(pair.Second);
		}

		/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
		/// <returns>Returns the server control's current view state; otherwise, returns null, if there is no view state associated with the control.</returns>
		// Token: 0x06002AF1 RID: 10993 RVA: 0x00071728 File Offset: 0x0006F928
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((IStateManager)this.DefaultView).SaveViewState();
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x06002AF2 RID: 10994 RVA: 0x00071757 File Offset: 0x0006F957
		protected override void TrackViewState()
		{
			((IStateManager)this.DefaultView).TrackViewState();
		}

		/// <summary>Gets a or sets a value that indicates what culture information is used when converting string values to actual property types in order to construct an object of the type indicated by <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DataObjectTypeName" />.</summary>
		/// <returns>The culture information. The default value is <see cref="F:System.Web.UI.WebControls.ParsingCulture.Invariant" />.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x0007177C File Offset: 0x0006F97C
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ParsingCulture ParsingCulture
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ParsingCulture.Invariant;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001AE8 RID: 6888
		private static readonly string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001AE9 RID: 6889
		private ObjectDataSourceView defaultView;

		// Token: 0x04001AEA RID: 6890
		private int cacheDuration;

		// Token: 0x04001AEB RID: 6891
		private bool enableCaching;

		// Token: 0x04001AEC RID: 6892
		private string cacheKeyDependency;

		// Token: 0x04001AED RID: 6893
		private string sqlCacheDependency;

		// Token: 0x04001AEE RID: 6894
		private DataSourceCacheManager cache;

		// Token: 0x04001AEF RID: 6895
		private DataSourceCacheExpiry cacheExpirationPolicy;
	}
}
