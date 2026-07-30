using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044A RID: 1098
	internal sealed class WizardLayoutContainer : WebControl
	{
		// Token: 0x060032F7 RID: 13047 RVA: 0x0002F7AE File Offset: 0x0002D9AE
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
