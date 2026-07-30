using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Unity;

namespace System.Web.UI.Design
{
	/// <summary>Provides a base control designer class for extending the design-mode behavior of a Web server control.</summary>
	// Token: 0x0200005A RID: 90
	[MonoTODO]
	public class ControlDesigner : HtmlControlDesigner
	{
		/// <summary>Provides a simple rectangular placeholder representation that displays the type and ID of the control.</summary>
		/// <returns>A string that contains design-time HTML markup providing basic information about the control.</returns>
		// Token: 0x060002C3 RID: 707 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected string CreatePlaceHolderDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides a simple rectangular placeholder representation that displays the type and ID of the control, and also additional specified instructions or information.</summary>
		/// <returns>A string that contains design-time HTML markup providing information about the control.</returns>
		/// <param name="instruction">A string that contains text and markup to add to the HTML output. </param>
		// Token: 0x060002C4 RID: 708 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected string CreatePlaceHolderDesignTimeHtml(string instruction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup that is used to represent the control at design time.</summary>
		/// <returns>The HTML markup used to represent the control at design time.</returns>
		// Token: 0x060002C5 RID: 709 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an appropriate resource provider factory, depending on the globalization settings in the configuration file for the site.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> object, if any are defined in the configuration file; otherwise, null.</returns>
		/// <param name="serviceProvider">An <see cref="T:System.IServiceProvider" /> object that can retrieve global and local services.</param>
		// Token: 0x060002C6 RID: 710 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public static DesignTimeResourceProviderFactory GetDesignTimeResourceProviderFactory(IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves an object that contains the design-time markup for the content and regions of the specified control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ViewRendering" /> object.</returns>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> object.</param>
		// Token: 0x060002C7 RID: 711 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public static ViewRendering GetViewRendering(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves an object that contains the design-time markup for the content and regions of the associated control for the specified control designer.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ViewRendering" /> object.</returns>
		/// <param name="designer">A control designer that derives from <see cref="T:System.Web.UI.Design.ControlDesigner" />.</param>
		// Token: 0x060002C8 RID: 712 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public static ViewRendering GetViewRendering(ControlDesigner designer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates HTML markup to display a specified error message at design time.</summary>
		/// <returns>An HTML markup string that contains the specified error message.</returns>
		/// <param name="errorMessage">The error message to include in the generated HTML markup.</param>
		// Token: 0x060002C9 RID: 713 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected string CreateErrorDesignTimeHtml(string errorMessage)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates the HTML markup to display a specified exception error message at design time.</summary>
		/// <returns>HTML markup that contains the specified <paramref name="errorMessage" /> and <paramref name="e" />.</returns>
		/// <param name="errorMessage">The error message to include in the generated HTML string.</param>
		/// <param name="e">The exception to include in the generated HTML string.</param>
		// Token: 0x060002CA RID: 714 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected string CreateErrorDesignTimeHtml(string errorMessage, Exception e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a copy of the associated control for viewing or rendering on the design surface.</summary>
		/// <returns>A Web server control.</returns>
		// Token: 0x060002CB RID: 715 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual Control CreateViewControl()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the coordinates of the rectangle representing the boundaries for the control as displayed on the design surface.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> object representing the boundaries for the control on the design surface relative to the control, not the document.</returns>
		// Token: 0x060002CC RID: 716 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public Rectangle GetBounds()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup to display the control and populates the collection with the current control designer regions.</summary>
		/// <returns>The design-time HTML markup for the associated control, including all control designer regions.</returns>
		/// <param name="regions">A collection of control designer regions for the associated control.</param>
		// Token: 0x060002CD RID: 717 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the content for an editable region of the design-time view of the associated control.</summary>
		/// <returns>The persisted content for the region, if the control designer supports editable regions; otherwise, an empty string ("").</returns>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> object to get content for.</param>
		// Token: 0x060002CE RID: 718 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Specifies the content for an editable region of the control at design time. </summary>
		/// <param name="region">An editable design region contained within the control.</param>
		/// <param name="content">The content to assign for the editable design region.</param>
		// Token: 0x060002CF RID: 719 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the persistable inner HTML markup of the control at design time.</summary>
		/// <returns>A string representing the persistable inner HTML markup for the associated control. The default is null.</returns>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual string GetPersistenceContent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves an object that contains the design-time markup for the content and regions of the associated control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ViewRendering" /> object.</returns>
		// Token: 0x060002D1 RID: 721 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public ViewRendering GetViewRendering()
		{
			throw new NotImplementedException();
		}

		/// <summary>Invalidates the whole area of the control that is displayed on the design surface and signals the control designer to redraw the control.</summary>
		// Token: 0x060002D2 RID: 722 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Invalidates the specified area of the control that is displayed on the design surface and signals the control designer to redraw the control.</summary>
		/// <param name="rectangle">A <see cref="T:System.Drawing.Rectangle" /> object that represents the area to invalidate, relative to the upper-left corner of the control. </param>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate(Rectangle rectangle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Wraps a series of changes into a transaction, using the specified parameters that can be rolled back as a unit with the undo functionality of the design host.</summary>
		/// <param name="component">The control associated with the control designer.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.Design.TransactedChangeCallback" /> object representing a function to call in the control designer as part of the transaction.</param>
		/// <param name="context">An object that contains the argument for callback.</param>
		/// <param name="description">A description of the effect of allowing the transaction to complete, which is used by the design host to give the user an opportunity to cancel the transaction.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.-or-<paramref name="callback" /> is null.</exception>
		// Token: 0x060002D4 RID: 724 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void InvokeTransactedChange(IComponent component, TransactedChangeCallback callback, object context, string description)
		{
			throw new NotImplementedException();
		}

		/// <summary>Wraps a series of changes into a transaction, using the specified parameters that can be rolled back as a unit with the undo functionality of the design host.</summary>
		/// <param name="component">The control associated with the control designer.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.Design.TransactedChangeCallback" /> object representing a function to call in the control designer as part of the transaction.</param>
		/// <param name="context">An object that contains the argument for callback.</param>
		/// <param name="description">A description of the effect of allowing the transaction to complete, which is used by the design host to give the user an opportunity to cancel the transaction.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> object (typically, either an <see cref="T:System.ComponentModel.EventDescriptor" /> or a <see cref="T:System.ComponentModel.PropertyDescriptor" /> object) that describes the member of the associated control that is being invoked as part of the transaction.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.-or-<paramref name="callback" /> is null.</exception>
		// Token: 0x060002D5 RID: 725 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void InvokeTransactedChange(IComponent component, TransactedChangeCallback callback, object context, string description, MemberDescriptor member)
		{
			throw new NotImplementedException();
		}

		/// <summary>Wraps a series of changes into a transaction, using the specified parameters that can be rolled back as a unit with the undo functionality of the design host.</summary>
		/// <param name="serviceProvider">An <see cref="T:System.IServiceProvider" /> object representing the design host that provides control designer services for the associated control.</param>
		/// <param name="component">The control associated with the control designer.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.Design.TransactedChangeCallback" /> object representing a function to call in the control designer as part of the transaction.</param>
		/// <param name="context">An object that contains the argument for callback.</param>
		/// <param name="description">A description of the effect of allowing the transaction to complete, which is used by the design host to give the user an opportunity to cancel the transaction.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> object (typically either an <see cref="T:System.ComponentModel.EventDescriptor" /> or a <see cref="T:System.ComponentModel.PropertyDescriptor" /> object) that describes the member of the associated control that is being invoked as part of the transaction.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.-or-<paramref name="callback" /> is null.-or-<paramref name="serviceProvider" /> is null.</exception>
		// Token: 0x060002D6 RID: 726 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void InvokeTransactedChange(IServiceProvider serviceProvider, IComponent component, TransactedChangeCallback callback, object context, string description, MemberDescriptor member)
		{
			throw new NotImplementedException();
		}

		/// <summary>Uses the provided resource writer to persist the localizable properties of the associated control to a resource in the design host.</summary>
		/// <param name="resourceWriter">An object derived from the <see cref="T:System.Web.UI.Design.IDesignTimeResourceWriter" /> object that is used to write resources into the design-time response stream.</param>
		// Token: 0x060002D7 RID: 727 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Localize(IDesignTimeResourceWriter resourceWriter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when a predefined, automatic formatting scheme has been applied to the associated control.</summary>
		/// <param name="appliedAutoFormat">A <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object that defines a style.</param>
		// Token: 0x060002D8 RID: 728 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnAutoFormatApplied(DesignerAutoFormat appliedAutoFormat)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event for the associated control. </summary>
		/// <param name="sender">The object that is the source of the event.</param>
		/// <param name="ce">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> object that contains the event data.</param>
		// Token: 0x060002D9 RID: 729 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnComponentChanging(object sender, ComponentChangingEventArgs ce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the control designer draws the associated control on the design surface, if the <see cref="F:System.Web.UI.Design.ViewFlags.CustomPaint" /> value is true.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> object that specifies the graphics and rectangle boundaries used to draw the control.</param>
		// Token: 0x060002DA RID: 730 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void OnPaint(PaintEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers internal data in a cloned control.</summary>
		/// <param name="original">The control associated with the control designer.</param>
		/// <param name="clone">The cloned copy of the associated control.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="original" /> is null.-or-<paramref name="clone" /> is null.</exception>
		// Token: 0x060002DB RID: 731 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void RegisterClone(object original, object clone)
		{
			throw new NotImplementedException();
		}

		/// <summary>Specifies the content for an editable region in the design-time view of the control.</summary>
		/// <param name="region">An editable design region contained within the design-time view of the control.</param>
		/// <param name="content">The content to assign for the editable design region.</param>
		// Token: 0x060002DC RID: 732 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected void SetRegionContent(EditableDesignerRegion region, string content)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup to represent a Web server control at design time that will have no visual representation at run time.</summary>
		/// <returns>The HTML markup used to represent a control at design time that would otherwise have no visual representation. The default is a rectangle that contains the type and ID of the component.</returns>
		// Token: 0x060002DD RID: 733 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual string GetEmptyDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup that provides information about the specified exception. </summary>
		/// <returns>The design-time HTML markup for the specified exception.</returns>
		/// <param name="e">The exception that occurred. </param>
		// Token: 0x060002DE RID: 734 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual string GetErrorDesignTimeHtml(Exception e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the persistable inner HTML markup of the control.</summary>
		/// <returns>The persistable inner HTML markup of the control.</returns>
		// Token: 0x060002DF RID: 735 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use GetPersistenceContent() instead")]
		[MonoTODO]
		public virtual string GetPersistInnerHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the control designer and loads the specified component.</summary>
		/// <param name="component">The control being designed. </param>
		// Token: 0x060002E0 RID: 736 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a value indicating whether the specified property on the associated control is data-bound.</summary>
		/// <returns>true, if the property is data-bound; otherwise, false.</returns>
		/// <param name="propName">The property to test for data binding. </param>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use DataBindings.Contains(string) instead")]
		[MonoTODO]
		public bool IsPropertyBound(string propName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the data-binding collection changes.</summary>
		/// <param name="propName">The property to test for changes in its bindings collection. </param>
		// Token: 0x060002E2 RID: 738 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use DataBindings.Changed event instead")]
		protected override void OnBindingsCollectionChanged(string propName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the design host when the user clicks the associated control at design time.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.Design.DesignerRegionMouseEventArgs" /> object that specifies the location and, possibly, the control designer region that the user clicked.</param>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void OnClick(DesignerRegionMouseEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the associated control changes.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="ce">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> object that contains the event data. </param>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when the associated Web server control has been resized in the design host at design time.</summary>
		// Token: 0x060002E5 RID: 741 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use OnComponentChanged() instead")]
		[MonoTODO]
		protected virtual void OnControlResize()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds properties to or removes properties from the Properties grid in a design host at design time or provides new design-time properties that might correspond to properties on the associated control.</summary>
		/// <param name="properties">The properties for the class of the component. </param>
		// Token: 0x060002E6 RID: 742 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Design.ControlDesigner.OnControlResize" /> event.</summary>
		// Token: 0x060002E7 RID: 743 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use OnComponentChanged() instead")]
		[MonoTODO]
		public void RaiseResizeEvent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the design-time HTML markup for the associated Web server control by calling the <see cref="Overload:System.Web.UI.Design.ControlDesigner.GetDesignTimeHtml" /> method.</summary>
		// Token: 0x060002E8 RID: 744 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void UpdateDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the control can be resized in the design-time environment.</summary>
		/// <returns>true, if the control can be resized; otherwise, false.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool AllowResize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the view-control object for the control designer.</summary>
		/// <returns>null.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("It is documented as not in use anymore", true)]
		protected object DesignTimeElementView
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the design host must finish loading before the <see cref="Overload:System.Web.UI.Design.ControlDesigner.GetDesignTimeHtml" /> method can be called.</summary>
		/// <returns>true, if loading must be complete before the <see cref="Overload:System.Web.UI.Design.ControlDesigner.GetDesignTimeHtml" /> method can be called; otherwise, false. The default is false.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use SetViewFlags(ViewFlags.DesignTimeHtmlRequiresLoadComplete, true)")]
		public virtual bool DesignTimeHtmlRequiresLoadComplete
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the ID string for the control.</summary>
		/// <returns>The ID string for the control.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual string ID
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

		/// <summary>Gets or sets a value indicating whether the Web server control has been marked as changed.</summary>
		/// <returns>true, if the Web server control has changed since it was last persisted or loaded; otherwise, false.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[Obsolete("Use Tag.SetDirty() and Tag.IsDirty instead.")]
		public bool IsDirty
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

		/// <summary>Gets or sets a value indicating whether the properties of the control are read-only at design time.</summary>
		/// <returns>true, if the properties of the control are read-only at design time; otherwise, false.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000234B File Offset: 0x0000054B
		[Obsolete("Use ContainerControlDesigner and EditableDesignerRegion")]
		[MonoTODO]
		public bool ReadOnly
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

		/// <summary>Gets the action list collection for the control designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object that contains the <see cref="T:System.ComponentModel.Design.DesignerActionList" /> items for the control designer.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the collection of predefined automatic formatting schemes to display in the Auto Format dialog box for the associated control at design time. </summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object that contains the predefined schemes for the control.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether data binding is supported by the containing region for the associated control.</summary>
		/// <returns>true, if the container for the control supports data binding; otherwise, false.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual bool DataBindingsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object that is used to persist data for the associated control at design time.</summary>
		/// <returns>A <see cref="P:System.Web.UI.Design.ControlDesigner.DesignerState" /> property that implements <see cref="T:System.Collections.IDictionary" /> and uses the <see cref="T:System.ComponentModel.Design.IComponentDesignerStateService" /> to persist state data for the associated control at design time. </returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected ControlDesignerState DesignerState
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the properties of the associated control are hidden when the control is in template mode.</summary>
		/// <returns>true, if the properties of the associated control are hidden when the control is in template mode; otherwise, false. The default is true.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected internal virtual bool HidePropertiesInTemplateMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the control is in either template viewing or editing mode in the design host. The <see cref="P:System.Web.UI.Design.ControlDesigner.InTemplateMode" /> property is read-only.</summary>
		/// <returns>true, if the control is in either template viewing or editing mode; otherwise, false.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool InTemplateMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the control designer for the Web Forms page that contains the associated control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> object that can be used at design time to access components on the Web Forms page that contains the control.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected WebFormsRootDesigner RootDesigner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object representing the HTML markup element for the associated control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IControlDesignerTag" /> object that represents the HTML markup element for the associated control. </returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected IControlDesignerTag Tag
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Assigns the specified bitwise <see cref="T:System.Web.UI.Design.ViewFlags" /> enumeration to the specified flag value.</summary>
		/// <param name="viewFlags">A <see cref="T:System.Web.UI.Design.ViewFlags" /> value.</param>
		/// <param name="setFlag">true to set the flag, false to remove the flag.</param>
		// Token: 0x060002FA RID: 762 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected void SetViewFlags(ViewFlags viewFlags, bool setFlag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of template groups, each containing one or more template definitions.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects. The default is an empty <see cref="T:System.Web.UI.Design.TemplateGroupCollection" />.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual TemplateGroupCollection TemplateGroups
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a Web server control that can be used for previewing the design-time HTML markup.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> object used by the base class to generate design-time HTML markup.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public Control ViewControl
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether a View control has been created for display on the design surface.</summary>
		/// <returns>true, if a control has been created for viewing on the design surface; otherwise, false.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public virtual bool ViewControlCreated
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the control designer uses a temporary preview control to generate the design-time HTML markup.</summary>
		/// <returns>true, if the control designer uses a temporary copy of the control for design-time preview; otherwise, false, if the control designer uses the <see cref="P:System.ComponentModel.Design.ComponentDesigner.Component" /> property for the control contained in the control designer.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected virtual bool UsePreviewControl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the control is visible at design time.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00008E84 File Offset: 0x00007084
		protected virtual bool Visible
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
