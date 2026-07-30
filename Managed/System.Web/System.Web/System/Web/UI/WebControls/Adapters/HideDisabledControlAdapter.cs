using System;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Provides rendering capabilities for the associated Web control to modify the default markup or behavior for a specific browser.</summary>
	// Token: 0x0200045C RID: 1116
	public class HideDisabledControlAdapter : WebControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.HideDisabledControlAdapter" /> class. </summary>
		// Token: 0x060033C0 RID: 13248 RVA: 0x0008A673 File Offset: 0x00088873
		public HideDisabledControlAdapter()
		{
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0008A67B File Offset: 0x0008887B
		internal HideDisabledControlAdapter(WebControl c)
			: base(c)
		{
		}

		/// <summary>Writes the associated Web control to the output stream as HTML.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output. </param>
		// Token: 0x060033C2 RID: 13250 RVA: 0x0008A69F File Offset: 0x0008889F
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.Control.IsEnabled)
			{
				return;
			}
			base.Render(writer);
		}
	}
}
