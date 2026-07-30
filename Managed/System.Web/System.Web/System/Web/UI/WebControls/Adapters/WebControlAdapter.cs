using System;
using System.Web.UI.Adapters;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Customizes rendering for the Web control to which the control adapter is attached, to modify the default markup or behavior for specific browsers.</summary>
	// Token: 0x0200045F RID: 1119
	public class WebControlAdapter : ControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.WebControlAdapter" /> class.</summary>
		// Token: 0x060033D4 RID: 13268 RVA: 0x00045B2A File Offset: 0x00043D2A
		public WebControlAdapter()
		{
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x00045B32 File Offset: 0x00043D32
		internal WebControlAdapter(WebControl wc)
			: base(wc)
		{
		}

		/// <summary>Generates the target-specific markup for the control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x060033D6 RID: 13270 RVA: 0x0008A731 File Offset: 0x00088931
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		/// <summary>Creates the beginning tag for the Web control in the markup that is transmitted to the target browser.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x060033D7 RID: 13271 RVA: 0x0008A748 File Offset: 0x00088948
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.Control.RenderBeginTag(writer);
		}

		/// <summary>Generates the target-specific inner markup for the Web control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x060033D8 RID: 13272 RVA: 0x0008A756 File Offset: 0x00088956
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			this.Control.RenderContents(writer);
		}

		/// <summary>Creates the ending tag for the Web control in the markup that is transmitted to the target browser.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x060033D9 RID: 13273 RVA: 0x0008A764 File Offset: 0x00088964
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			this.Control.RenderEndTag(writer);
		}

		/// <summary>Gets a reference to the Web control to which this control adapter is attached.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebControl" /> to which this <see cref="T:System.Web.UI.WebControls.Adapters.WebControlAdapter" /> is attached.</returns>
		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060033DA RID: 13274 RVA: 0x0008A772 File Offset: 0x00088972
		protected new WebControl Control
		{
			get
			{
				return (WebControl)this.control;
			}
		}

		/// <summary>Gets a value indicating whether the Web control and all its parent controls are enabled.</summary>
		/// <returns>true if the associated <see cref="T:System.Web.UI.WebControls.WebControl" /> and all its parent controls are enabled; otherwise, false.</returns>
		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x0008A77F File Offset: 0x0008897F
		protected bool IsEnabled
		{
			get
			{
				return this.Control.IsEnabled;
			}
		}
	}
}
