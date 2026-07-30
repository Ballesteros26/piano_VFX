using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.Web.UI.Design
{
	/// <summary>Provides a base class for the design-time functionality of a Web Forms page and allows access to and manipulation of components and controls that are contained within the Web Forms page at design time. </summary>
	// Token: 0x020000BB RID: 187
	public abstract class WebFormsRootDesigner : IRootDesigner, IDesigner, IDisposable, IDesignerFilter
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x000095D8 File Offset: 0x000077D8
		~WebFormsRootDesigner()
		{
		}

		/// <summary>Occurs when the designer completes loading the Web Forms page.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000565 RID: 1381 RVA: 0x00009600 File Offset: 0x00007800
		// (remove) Token: 0x06000566 RID: 1382 RVA: 0x00009638 File Offset: 0x00007838
		public event EventHandler LoadComplete;

		/// <summary>When overridden in a derived class, gets the URL at which the Web Forms page is located. </summary>
		/// <returns>The URL at which the Web Forms page is located; otherwise, null, if the Web Forms page has no associated URL.</returns>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000567 RID: 1383
		public abstract string DocumentUrl { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the designer view is locked.</summary>
		/// <returns>true, if the designer view is locked; otherwise, false.</returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000568 RID: 1384
		public abstract bool IsDesignerViewLocked { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the Web Forms page is still loading.</summary>
		/// <returns>true, if the Web Forms page is loading; otherwise, false.</returns>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000569 RID: 1385
		public abstract bool IsLoading { get; }

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Web.UI.Design.WebFormsReferenceManager" /> object that has information about the current Web Forms page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.WebFormsReferenceManager" /> containing information about the current Web Forms page.</returns>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600056A RID: 1386
		public abstract WebFormsReferenceManager ReferenceManager { get; }

		/// <summary>When overridden in a derived class, adds a client script element to the current Web Forms page.</summary>
		/// <param name="scriptItem">A <see cref="T:System.Web.UI.Design.ClientScriptItem" /> to add to the Web Forms page.</param>
		// Token: 0x0600056B RID: 1387
		public abstract void AddClientScriptToDocument(ClientScriptItem scriptItem);

		/// <summary>When overridden in a derived class, adds a Web server control to the Web Forms page.</summary>
		/// <returns>The ID of the control that was added.</returns>
		/// <param name="newControl">The control to add to the Web Forms page.</param>
		/// <param name="referenceControl">The control relative to which <paramref name="newControl" /> is added.</param>
		/// <param name="location">A <see cref="T:System.Web.UI.Design.ControlLocation" /> value that indicates the location for <paramref name="newControl" /> relative to <paramref name="referenceControl" />.</param>
		// Token: 0x0600056C RID: 1388
		public abstract string AddControlToDocument(Control newControl, Control referenceControl, ControlLocation location);

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.Web.UI.Design.ClientScriptItemCollection" />  object that contains all client script items that are on the page.</summary>
		/// <returns>An object that contains all client script items that are on the page.</returns>
		// Token: 0x0600056D RID: 1389
		public abstract ClientScriptItemCollection GetClientScriptsInDocument();

		/// <summary>When overridden in a derived class, returns both the current design-time view and the HTML markup for the specified control.</summary>
		/// <param name="control">The control to provide the view and tag for.</param>
		/// <param name="view">When the <see cref="M:System.Web.UI.Design.WebFormsRootDesigner.GetControlViewAndTag(System.Web.UI.Control,System.Web.UI.Design.IControlDesignerView@,System.Web.UI.Design.IControlDesignerTag@)" /> method returns, <paramref name="view" /> contains an IControlDesignerView object that provides access to the visual representation and content of a control at design time. <paramref name="view" /> is passed uninitialized.</param>
		/// <param name="tag">When the <see cref="M:System.Web.UI.Design.WebFormsRootDesigner.GetControlViewAndTag(System.Web.UI.Control,System.Web.UI.Design.IControlDesignerView@,System.Web.UI.Design.IControlDesignerTag@)" /> method returns, <paramref name="tag" /> contains an IControlDesignerTag object that provides access to the HTML element for the control's control designer. <paramref name="tag" /> is passed uninitialized.</param>
		// Token: 0x0600056E RID: 1390
		protected internal abstract void GetControlViewAndTag(Control control, out IControlDesignerView view, out IControlDesignerTag tag);

		/// <summary>Removes the specified client script from the document at design time.</summary>
		/// <param name="clientScriptId">The identifier for the previously registered client script.</param>
		// Token: 0x0600056F RID: 1391
		public abstract void RemoveClientScriptFromDocument(string clientScriptId);

		/// <summary>When overridden in a derived class, removes the specified control from the Web Forms page.</summary>
		/// <param name="control">The control to remove from the Web Forms page.</param>
		// Token: 0x06000570 RID: 1392
		public abstract void RemoveControlFromDocument(Control control);

		/// <summary>Gets or sets the component that this designer is designing.</summary>
		/// <returns>The component managed by the designer.</returns>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual IComponent Component
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the culture information for the current thread.</summary>
		/// <returns>The culture information for the current thread.</returns>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public CultureInfo CurrentCulture
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an array of technologies that the designer component can support for its display.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected ViewTechnology[] SupportedTechnologies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the design-time verbs that are supported by the designer.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects supported by the designer; otherwise, null, if the component has no verbs.</returns>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected DesignerVerbCollection Verbs
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a design-time <see cref="T:System.ComponentModel.Design.DesignerActionService" /> object.</summary>
		/// <returns>A design-time designer action service object.</returns>
		/// <param name="serviceProvider">A design host, such as Visual Studio 2005, cast as an <see cref="T:System.IServiceProvider" />.</param>
		// Token: 0x06000576 RID: 1398 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual DesignerActionService CreateDesignerActionService(IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Web.UI.IUrlResolutionService" /> that resolves relative URLs.</summary>
		/// <returns>An object that resolves relative URLs.</returns>
		// Token: 0x06000577 RID: 1399 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual IUrlResolutionService CreateUrlResolutionService()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000578 RID: 1400 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates empty HTML markup for a control at design time.</summary>
		/// <returns>HTML markup for an empty control.</returns>
		/// <param name="control">The control to generate HTML markup for.</param>
		// Token: 0x06000579 RID: 1401 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual string GenerateEmptyDesignTimeHtml(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates HTML markup that is used to display an error message at design time by using the specified control, exception, and message.</summary>
		/// <returns>HTML markup for a control and exception information.</returns>
		/// <param name="control">The control that raised the exception.-or- null.</param>
		/// <param name="e">The exception. -or-null.</param>
		/// <param name="errorMessage">A message to add to the exception message.-or- An empty string ("").</param>
		// Token: 0x0600057A RID: 1402 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual string GenerateErrorDesignTimeHtml(Control control, Exception e, string errorMessage)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the requested service.</summary>
		/// <returns>The requested service; otherwise, null, if the service cannot be resolved.</returns>
		/// <param name="serviceType">The type of service to retrieve.</param>
		// Token: 0x0600057B RID: 1403 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected internal virtual object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a view object that is determined by the provided <see cref="T:System.ComponentModel.Design.ViewTechnology" /> object.</summary>
		/// <returns>An object containing the current view of the component.</returns>
		/// <param name="viewTechnology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> obtained from the <see cref="P:System.Web.UI.Design.WebFormsRootDesigner.SupportedTechnologies" /> property.</param>
		// Token: 0x0600057C RID: 1404 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected object GetView(ViewTechnology viewTechnology)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> object using the specified component.</summary>
		/// <param name="component">The component that this designer is designing.</param>
		// Token: 0x0600057D RID: 1405 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Design.WebFormsRootDesigner.LoadComplete" /> event when the Web Forms page is completely loaded.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" />.</param>
		// Token: 0x0600057E RID: 1406 RVA: 0x0000966D File Offset: 0x0000786D
		[MonoTODO]
		protected virtual void OnLoadComplete(EventArgs e)
		{
			if (this.LoadComplete != null)
			{
				this.LoadComplete(this, e);
			}
		}

		/// <summary>Allows a designer to change or remove items from the set of attributes that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
		/// <param name="attributes">The attributes for the class of the component.</param>
		// Token: 0x0600057F RID: 1407 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void PostFilterAttributes(IDictionary attributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows a designer to change or remove items from the set of events that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
		/// <param name="events">The events for the class of the component.</param>
		// Token: 0x06000580 RID: 1408 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void PostFilterEvents(IDictionary events)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows a designer to change or remove items from the set of properties that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
		/// <param name="properties">The properties for the class of the component.</param>
		// Token: 0x06000581 RID: 1409 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void PostFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows a designer to add to the set of attributes that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
		/// <param name="attributes">The attributes for the class of the component.</param>
		// Token: 0x06000582 RID: 1410 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void PreFilterAttributes(IDictionary attributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows a designer to add items to the set of events that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
		/// <param name="events">The events for the class of the component.</param>
		// Token: 0x06000583 RID: 1411 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void PreFilterEvents(IDictionary events)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows a designer to add items to the set of properties that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
		/// <param name="properties">The properties for the class of the component.</param>
		// Token: 0x06000584 RID: 1412 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a relative URL into a fully qualified URL.</summary>
		/// <returns>A fully qualified URL resolved from <paramref name="relativeUrl" />.</returns>
		/// <param name="relativeUrl">A relative URL for a resource on the site.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="relativeUrl" /> is null.</exception>
		// Token: 0x06000585 RID: 1413 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string ResolveUrl(string relativeUrl)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the ID property of the specified control with the specified string.</summary>
		/// <param name="control">The control on which to set the ID.</param>
		/// <param name="id">The string to set as the ID for the control.</param>
		// Token: 0x06000586 RID: 1414 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SetControlID(Control control, string id)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the design-time verbs that are supported by the designer. For a description of this member, see <see cref="P:System.ComponentModel.Design.IDesigner.Verbs" />.</summary>
		/// <returns>The design-time verbs that are supported by the designer.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00009684 File Offset: 0x00007884
		[MonoTODO]
		DesignerVerbCollection IDesigner.Verbs
		{
			get
			{
				return this.Verbs;
			}
		}

		/// <summary>Gets an array of technologies that the designer component can support for its display. For a description of this member, see <see cref="P:System.ComponentModel.Design.IRootDesigner.SupportedTechnologies" />.</summary>
		/// <returns>An array of technologies that the designer component can support for its display.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000968C File Offset: 0x0000788C
		[MonoTODO]
		ViewTechnology[] IRootDesigner.SupportedTechnologies
		{
			get
			{
				return this.SupportedTechnologies;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesigner.DoDefaultAction" />.</summary>
		// Token: 0x06000589 RID: 1417 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesigner.DoDefaultAction()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterAttributes(System.Collections.IDictionary)" />.</summary>
		/// <param name="attributes">The attribute objects for the class of the component.</param>
		// Token: 0x0600058A RID: 1418 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterEvents(System.Collections.IDictionary)" />.</summary>
		/// <param name="events">The event descriptor objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
		// Token: 0x0600058B RID: 1419 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesignerFilter.PostFilterEvents(IDictionary events)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterProperties(System.Collections.IDictionary)" />.</summary>
		/// <param name="properties">The property descriptor objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
		// Token: 0x0600058C RID: 1420 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesignerFilter.PostFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterAttributes(System.Collections.IDictionary)" />.</summary>
		/// <param name="attributes">The attribute objects for the class of the component.</param>
		// Token: 0x0600058D RID: 1421 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterEvents(System.Collections.IDictionary)" />.</summary>
		/// <param name="events">The event descriptor objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
		// Token: 0x0600058E RID: 1422 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesignerFilter.PreFilterEvents(IDictionary events)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterProperties(System.Collections.IDictionary)" />.</summary>
		/// <param name="properties">The property descriptor objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
		// Token: 0x0600058F RID: 1423 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IDesignerFilter.PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a view object for the specified view technology. For a description of this member, see <see cref="M:System.ComponentModel.Design.IRootDesigner.GetView(System.ComponentModel.Design.ViewTechnology)" />.</summary>
		/// <returns>The view object for the specified view technology.</returns>
		/// <param name="viewTechnology"> The view technology.</param>
		// Token: 0x06000590 RID: 1424 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		object IRootDesigner.GetView(ViewTechnology viewTechnology)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.IDisposable.Dispose" />.</summary>
		// Token: 0x06000591 RID: 1425 RVA: 0x00009694 File Offset: 0x00007894
		[MonoTODO]
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}
	}
}
