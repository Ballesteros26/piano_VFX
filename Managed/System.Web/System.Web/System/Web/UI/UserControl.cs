using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.ModelBinding;
using System.Web.SessionState;
using Unity;

namespace System.Web.UI
{
	/// <summary>Represents an .ascx file, also known as a user control, requested from a server that hosts an ASP.NET Web application. The file must be called from a Web Forms page or a parser error will occur.</summary>
	// Token: 0x02000241 RID: 577
	[ToolboxItem(false)]
	[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[ParseChildren(true)]
	[ControlBuilder(typeof(UserControlControlBuilder))]
	[DefaultEvent("Load")]
	[Designer("System.Web.UI.Design.UserControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	[DesignerCategory("ASPXCodeBehind")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UserControl : TemplateControl, IAttributeAccessor, IUserControlDesignerAccessor, INamingContainer, IFilterResolutionService, INonBindingContainer
	{
		/// <summary>Gets the <see cref="P:System.Web.HttpContext.Application" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> object for the current Web request.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000407C4 File Offset: 0x0003E9C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpApplicationState Application
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Application;
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x000407E3 File Offset: 0x0003E9E3
		private void EnsureAttributes()
		{
			if (this.attributes == null)
			{
				this.attrBag = new StateBag(true);
				if (base.IsTrackingViewState)
				{
					this.attrBag.TrackViewState();
				}
				this.attributes = new AttributeCollection(this.attrBag);
			}
		}

		/// <summary>Gets a collection of all attribute name and value pairs declared in the user control tag within the .aspx file.</summary>
		/// <returns>An <see cref="T:System.Web.UI.AttributeCollection" /> object that contains all the name and value pairs declared in the user control tag.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0004081D File Offset: 0x0003EA1D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AttributeCollection Attributes
		{
			get
			{
				this.EnsureAttributes();
				return this.attributes;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object that is associated with the application that contains the user control.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.Cache" /> object in which to store the user control's data.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0004082C File Offset: 0x0003EA2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cache Cache
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Cache;
			}
		}

		/// <summary>Gets a reference to a collection of caching parameters for this user control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCachePolicy" /> containing properties that define the caching parameters for this <see cref="T:System.Web.UI.UserControl" />.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0004084C File Offset: 0x0003EA4C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ControlCachePolicy CachePolicy
		{
			get
			{
				BasePartialCachingControl basePartialCachingControl = this.Parent as BasePartialCachingControl;
				if (basePartialCachingControl != null)
				{
					return basePartialCachingControl.CachePolicy;
				}
				if (this.cachePolicy == null)
				{
					this.cachePolicy = new ControlCachePolicy();
				}
				return this.cachePolicy;
			}
		}

		/// <summary>Gets a value indicating whether the user control is being loaded in response to a client postback, or if it is being loaded and accessed for the first time.</summary>
		/// <returns>true if the user control is being loaded in response to a client postback; otherwise, false.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00040888 File Offset: 0x0003EA88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPostBack
		{
			get
			{
				Page page = this.Page;
				return page != null && page.IsPostBack;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpRequest" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000408A8 File Offset: 0x0003EAA8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpRequest Request
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpResponse" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000408C8 File Offset: 0x0003EAC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpResponse Response
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Response;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000408E8 File Offset: 0x0003EAE8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpServerUtility Server
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Server;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current Web request.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.HttpSessionState" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00040908 File Offset: 0x0003EB08
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpSessionState Session
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Session;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</summary>
		/// <returns>The data from the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00040928 File Offset: 0x0003EB28
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TraceContext Trace
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Trace;
			}
		}

		/// <summary>Performs any initialization steps on the user control that are required by RAD designers.</summary>
		// Token: 0x060017C8 RID: 6088 RVA: 0x000378E7 File Offset: 0x00035AE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DesignerInitialize()
		{
			this.InitRecursive(null);
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.UserControl" /> object that has been created declaratively. Since there are some differences between pages and user controls, this method makes sure that the user control is initialized properly.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> object that contains the user control. </param>
		// Token: 0x060017C9 RID: 6089 RVA: 0x00040947 File Offset: 0x0003EB47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeAsUserControl(Page page)
		{
			if (this.initialized)
			{
				return;
			}
			this.Page = page;
			this.InitializeAsUserControlInternal();
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0004095F File Offset: 0x0003EB5F
		internal void InitializeAsUserControlInternal()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			base.WireupAutomaticEvents();
			this.FrameworkInitialize();
		}

		/// <summary>Assigns a virtual file path, either absolute or relative, to a physical file path.</summary>
		/// <returns>The physical path to the file.</returns>
		/// <param name="virtualPath">The virtual file path to map. </param>
		// Token: 0x060017CB RID: 6091 RVA: 0x0004097D File Offset: 0x0003EB7D
		public string MapPath(string virtualPath)
		{
			return this.Request.MapPath(virtualPath, this.TemplateSourceDirectory, true);
		}

		/// <summary>Restores the view-state information from a previous user control request that was saved by the <see cref="M:System.Web.UI.UserControl.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the user control state to be restored. </param>
		// Token: 0x060017CC RID: 6092 RVA: 0x00040994 File Offset: 0x0003EB94
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				base.LoadViewState(pair.First);
				if (pair.Second != null)
				{
					this.EnsureAttributes();
					this.attrBag.LoadViewState(pair.Second);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060017CD RID: 6093 RVA: 0x000409D6 File Offset: 0x0003EBD6
		protected internal override void OnInit(EventArgs e)
		{
			this.InitializeAsUserControl(this.Page);
			base.OnInit(e);
		}

		/// <summary>Saves any user control view-state changes that have occurred since the last page postback.</summary>
		/// <returns>Returns the user control's current view state. If there is no view state associated with the control, it returns null.</returns>
		// Token: 0x060017CE RID: 6094 RVA: 0x000409EC File Offset: 0x0003EBEC
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			if (this.attributes != null)
			{
				obj2 = this.attrBag.SaveViewState();
			}
			if (obj == null && obj2 == null)
			{
				return null;
			}
			return new Pair(obj, obj2);
		}

		/// <summary>Returns the value of the specified user control attribute.</summary>
		/// <returns>The value of the specified user control attribute.</returns>
		/// <param name="name">The name of the attribute to get the value of.</param>
		// Token: 0x060017CF RID: 6095 RVA: 0x00040A25 File Offset: 0x0003EC25
		string IAttributeAccessor.GetAttribute(string name)
		{
			if (this.attributes == null)
			{
				return null;
			}
			return this.attributes[name];
		}

		/// <summary>Sets the value of the specified user control attribute.</summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The value of the attribute to set.</param>
		// Token: 0x060017D0 RID: 6096 RVA: 0x00040A3D File Offset: 0x0003EC3D
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.EnsureAttributes();
			this.Attributes[name] = value;
		}

		/// <summary>Gets or sets the text that appears between the opening and closing tags of a user control.</summary>
		/// <returns>The text that appears between the opening and closing tabs of a user control.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00040A54 File Offset: 0x0003EC54
		// (set) Token: 0x060017D2 RID: 6098 RVA: 0x00040A81 File Offset: 0x0003EC81
		string IUserControlDesignerAccessor.InnerText
		{
			get
			{
				string text = (string)this.ViewState["!DesignTimeInnerText"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["!DesignTimeInnerText"] = value;
			}
		}

		/// <summary>Gets or sets the full tag name of the user control.</summary>
		/// <returns>The full tag name of the user control.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00040A94 File Offset: 0x0003EC94
		// (set) Token: 0x060017D4 RID: 6100 RVA: 0x00040AC1 File Offset: 0x0003ECC1
		string IUserControlDesignerAccessor.TagName
		{
			get
			{
				string text = (string)this.ViewState["!DesignTimeTagName"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["!DesignTimeTagName"] = value;
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		int IFilterResolutionService.CompareFilters(string filter1, string filter2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		bool IFilterResolutionService.EvaluateFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Attempts to update the model instance by using the values from the data-bound control.</summary>
		/// <returns>true if the model instance was updated successfully; otherwise, false.</returns>
		/// <param name="model">The model instance to update.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		// Token: 0x060017D7 RID: 6103 RVA: 0x00040AD4 File Offset: 0x0003ECD4
		public virtual bool TryUpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Attempts to update the model instance using values from the value provider.</summary>
		/// <returns>true if the model instance was updated successfully; otherwise, false.</returns>
		/// <param name="model">The model instance to update.</param>
		/// <param name="valueProvider">A dictionary of values to use to update the model.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		// Token: 0x060017D8 RID: 6104 RVA: 0x00040AF0 File Offset: 0x0003ECF0
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the model instance by using values from the data-bound control.</summary>
		/// <param name="model">The model instance to update.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		/// <exception cref="System.InvalidOperationException">The model instance was not updated successfully.</exception>
		// Token: 0x060017D9 RID: 6105 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void UpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the specified model instance using values from the value provider of the user control.</summary>
		/// <param name="model">The model instance to update.</param>
		/// <param name="valueProvider">A dictionary of values to use to update the model.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		/// <exception cref="System.InvalidOperationException">The model instance was not updated successfully.</exception>
		// Token: 0x060017DA RID: 6106 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015FA RID: 5626
		private ControlCachePolicy cachePolicy;

		// Token: 0x040015FB RID: 5627
		private bool initialized;

		// Token: 0x040015FC RID: 5628
		private AttributeCollection attributes;

		// Token: 0x040015FD RID: 5629
		private StateBag attrBag;
	}
}
