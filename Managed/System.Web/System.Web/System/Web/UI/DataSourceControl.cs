using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Serves as the base class for controls that represent data sources to data-bound controls.</summary>
	// Token: 0x020001C4 RID: 452
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[NonVisualControl]
	[Bindable(false)]
	[Designer("System.Web.UI.Design.DataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class DataSourceControl : Control, IDataSource, IListSource
	{
		/// <summary>Applies the style properties that are defined in the page style sheet to the control. </summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> containing the control.</param>
		// Token: 0x06001257 RID: 4695 RVA: 0x00003A1F File Offset: 0x00001C1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::System.MonoTODO("Not implemented")]
		public override void ApplyStyleSheetSkin(Page page)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x06001258 RID: 4696 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter. </summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found.</param>
		// Token: 0x06001259 RID: 4697 RVA: 0x00032A5C File Offset: 0x00030C5C
		[global::System.MonoTODO("why override?")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to call the <see cref="M:System.Web.UI.DataSourceControl.Focus" /> method.</exception>
		// Token: 0x0600125A RID: 4698 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> to retrieve. In data source controls that support only one view, such as <see cref="T:System.Web.UI.WebControls.SqlDataSource" />, this parameter is ignored. </param>
		// Token: 0x0600125B RID: 4699
		protected abstract DataSourceView GetView(string viewName);

		/// <summary>Gets the named <see cref="T:System.Web.UI.DataSourceView" /> object associated with the <see cref="T:System.Web.UI.DataSourceControl" /> control. Some data source controls support only one view, while others support more than one.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> to retrieve. In data source controls that support only one view, such as <see cref="T:System.Web.UI.WebControls.SqlDataSource" />, this parameter is ignored.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="viewName" /> is null. </exception>
		// Token: 0x0600125C RID: 4700 RVA: 0x00032A65 File Offset: 0x00030C65
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		/// <summary>Gets a collection of names, representing the list of <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		// Token: 0x0600125D RID: 4701 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual ICollection GetViewNames()
		{
			return null;
		}

		/// <summary>Gets a collection of names, representing the list of <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		// Token: 0x0600125E RID: 4702 RVA: 0x00032A6E File Offset: 0x00030C6E
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		/// <summary>Gets a list of data source controls that can be used as sources of lists of data.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
		// Token: 0x0600125F RID: 4703 RVA: 0x00032A76 File Offset: 0x00030C76
		IList IListSource.GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		/// <summary>Determines if the server control contains any child controls. </summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x06001260 RID: 4704 RVA: 0x00032A7E File Offset: 0x00030C7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06001261 RID: 4705 RVA: 0x00032A88 File Offset: 0x00030C88
		protected virtual void RaiseDataSourceChangedEvent(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataSourceControl.dataSourceChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled. </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
		// Token: 0x06001262 RID: 4706 RVA: 0x00032AB6 File Offset: 0x00030CB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		/// <summary>Gets the server control identifier generated by ASP.NET.</summary>
		/// <returns>The server control identifier generated by ASP.NET.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00032ABF File Offset: 0x00030CBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls for a specified server control in the UI hierarchy.</summary>
		/// <returns>The collection of child controls for the specified server control.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00032AC7 File Offset: 0x00030CC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets a value indicating whether this control supports themes.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.DataSourceControl.EnableTheming" /> property. </exception>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00003A01 File Offset: 0x00001C01
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the skin to apply to the <see cref="T:System.Web.UI.DataSourceControl" /> control.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.DataSourceControl.SkinID" /> property. </exception>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00032ACF File Offset: 0x00030CCF
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00032AD7 File Offset: 0x00030CD7
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		/// <summary>Indicates whether the data source control is associated with one or more lists of data.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.DataSourceControl" /> is associated with one or more <see cref="T:System.Web.UI.DataSourceView" /> objects; otherwise, false.</returns>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00032AE0 File Offset: 0x00030CE0
		bool IListSource.ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visually displayed.</summary>
		/// <returns>Always false.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.DataSourceControl.Visible" /> property. </exception>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue(false)]
		public override bool Visible
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Occurs when a data source control has changed in a way that affects data-bound controls. </summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600126C RID: 4716 RVA: 0x00032AE8 File Offset: 0x00030CE8
		// (remove) Token: 0x0600126D RID: 4717 RVA: 0x00032AFB File Offset: 0x00030CFB
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataSourceControl.dataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataSourceControl.dataSourceChanged, value);
			}
		}

		// Token: 0x0400141F RID: 5151
		private static object dataSourceChanged = new object();
	}
}
