using System;
using System.ComponentModel;

namespace System.Web.UI.Adapters
{
	/// <summary>Customizes rendering for the derived control to which the adapter is attached, to modify the default markup or behavior for specific browsers, and is the base class from which all control adapters inherit.</summary>
	// Token: 0x0200027A RID: 634
	public abstract class ControlAdapter
	{
		// Token: 0x06001A36 RID: 6710 RVA: 0x000459C1 File Offset: 0x00043BC1
		internal ControlAdapter(Control c)
		{
			this.control = c;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> class.</summary>
		// Token: 0x06001A37 RID: 6711 RVA: 0x00002050 File Offset: 0x00000250
		protected ControlAdapter()
		{
		}

		/// <summary>Gets a reference to the browser capabilities of the client making the current HTTP request.</summary>
		/// <returns>An <see cref="T:System.Web.HttpBrowserCapabilities" /> specifying client browser and markup capabilities.</returns>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x000459D0 File Offset: 0x00043BD0
		protected HttpBrowserCapabilities Browser
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.Request.Browser;
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the control to which this control adapter is attached.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> to which this <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> is attached.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x000459F4 File Offset: 0x00043BF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets a reference to the page where the control associated with this adapter resides.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Page" /> that provides access to the page instance where the associated control is situated.</returns>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000459FC File Offset: 0x00043BFC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected Page Page
		{
			get
			{
				Control control = this.Control;
				if (control != null)
				{
					return control.Page;
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the page adapter for the page where the associated control resides.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Adapters.PageAdapter" /> for the page where the control associated with the current <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> is situated.</returns>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00045A1C File Offset: 0x00043C1C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected PageAdapter PageAdapter
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.PageAdapter;
				}
				return null;
			}
		}

		/// <summary>Called prior to the rendering of a control. In a derived adapter class, generates opening tags that are required by a specific target but not needed by HTML browsers.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x06001A3C RID: 6716 RVA: 0x00045A3B File Offset: 0x00043C3B
		protected internal virtual void BeginRender(HtmlTextWriter writer)
		{
			writer.BeginRender();
		}

		/// <summary>Creates the target-specific child controls for a composite control.</summary>
		// Token: 0x06001A3D RID: 6717 RVA: 0x00045A44 File Offset: 0x00043C44
		protected internal virtual void CreateChildControls()
		{
			Control control = this.Control;
			if (control != null)
			{
				control.CreateChildControls();
			}
		}

		/// <summary>Called after the rendering of a control. In a derived adapter class, generates closing tags that are required by a specific target but not needed by HTML browsers.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x06001A3E RID: 6718 RVA: 0x00045A61 File Offset: 0x00043C61
		protected internal virtual void EndRender(HtmlTextWriter writer)
		{
			writer.EndRender();
		}

		/// <summary>Loads adapter control state information that was saved by <see cref="M:System.Web.UI.Adapters.ControlAdapter.SaveAdapterControlState" /> during a previous request to the page where the control associated with this control adapter resides.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />. </param>
		// Token: 0x06001A3F RID: 6719 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void LoadAdapterControlState(object state)
		{
		}

		/// <summary>Loads adapter view state information that was saved by <see cref="M:System.Web.UI.Adapters.ControlAdapter.SaveAdapterViewState" /> during a previous request to the page where the control associated with this control adapter resides.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the adapter view state information as a <see cref="T:System.Web.UI.StateBag" />. </param>
		// Token: 0x06001A40 RID: 6720 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal virtual void LoadAdapterViewState(object state)
		{
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnInit(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A41 RID: 6721 RVA: 0x00045A6C File Offset: 0x00043C6C
		protected internal virtual void OnInit(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnInit(e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A42 RID: 6722 RVA: 0x00045A8C File Offset: 0x00043C8C
		protected internal virtual void OnLoad(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnLoad(e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A43 RID: 6723 RVA: 0x00045AAC File Offset: 0x00043CAC
		protected internal virtual void OnPreRender(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnPreRender(e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnUnload(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A44 RID: 6724 RVA: 0x00045ACC File Offset: 0x00043CCC
		protected internal virtual void OnUnload(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnUnload(e);
			}
		}

		/// <summary>Generates the target-specific markup for the control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to use to render the target-specific output. </param>
		// Token: 0x06001A45 RID: 6725 RVA: 0x00045AEC File Offset: 0x00043CEC
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.Render(writer);
			}
		}

		/// <summary>Generates the target-specific markup for the child controls in a composite control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to use to render the target-specific output. </param>
		// Token: 0x06001A46 RID: 6726 RVA: 0x00045B0C File Offset: 0x00043D0C
		protected internal virtual void RenderChildren(HtmlTextWriter writer)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.RenderChildren(writer);
			}
		}

		/// <summary>Saves control state information for the control adapter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />. </returns>
		// Token: 0x06001A47 RID: 6727 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected internal virtual object SaveAdapterControlState()
		{
			return null;
		}

		/// <summary>Saves view state information for the control adapter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the adapter view state information as a <see cref="T:System.Web.UI.StateBag" />. </returns>
		// Token: 0x06001A48 RID: 6728 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected internal virtual object SaveAdapterViewState()
		{
			return null;
		}

		// Token: 0x0400164D RID: 5709
		internal Control control;
	}
}
