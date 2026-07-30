using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Provides a base class for data source controls that represent hierarchical data.</summary>
	// Token: 0x020001D3 RID: 467
	[Bindable(false)]
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[Designer("System.Web.UI.Design.HierarchicalDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[NonVisualControl]
	public abstract class HierarchicalDataSourceControl : Control, IHierarchicalDataSource
	{
		/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
		// Token: 0x060012EB RID: 4843
		protected abstract HierarchicalDataSourceView GetHierarchicalView(string viewPath);

		/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
		// Token: 0x060012EC RID: 4844 RVA: 0x00033602 File Offset: 0x00031802
		HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
		{
			return this.GetHierarchicalView(viewPath);
		}

		/// <summary>Gets a value indicating whether this control supports themes.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.EnableTheming" /> property. </exception>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x00003A01 File Offset: 0x00001C01
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
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

		/// <summary>Gets or sets the skin to apply to the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" /> in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.SkinID" /> property. </exception>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x00003A01 File Offset: 0x00001C01
		[DefaultValue("")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visually displayed.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.Visible" /> property. </exception>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x00003A01 File Offset: 0x00001C01
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.EmptyControlCollection" /> that prevents any child controls from being added.</returns>
		// Token: 0x060012F3 RID: 4851 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found.</param>
		// Token: 0x060012F4 RID: 4852 RVA: 0x0003360B File Offset: 0x0003180B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			if (id == this.ID)
			{
				return this;
			}
			return null;
		}

		/// <summary>Determines if the server control contains any child controls.</summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x060012F5 RID: 4853 RVA: 0x00008A69 File Offset: 0x00006C69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return false;
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to call the <see cref="M:System.Web.UI.HierarchicalDataSourceControl.Focus" /> method.</exception>
		// Token: 0x060012F6 RID: 4854 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> has changed in some way that affects data-bound controls.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060012F7 RID: 4855 RVA: 0x0003361E File Offset: 0x0003181E
		// (remove) Token: 0x060012F8 RID: 4856 RVA: 0x00033631 File Offset: 0x00031831
		event EventHandler IHierarchicalDataSource.DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(HierarchicalDataSourceControl.dataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(HierarchicalDataSourceControl.dataSourceChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.IHierarchicalDataSource.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060012F9 RID: 4857 RVA: 0x00033644 File Offset: 0x00031844
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[HierarchicalDataSourceControl.dataSourceChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <param name="writer">The HTML text writer.</param>
		// Token: 0x060012FA RID: 4858 RVA: 0x0000393A File Offset: 0x00001B3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
		}

		// Token: 0x0400143D RID: 5181
		private static object dataSourceChanged = new object();
	}
}
