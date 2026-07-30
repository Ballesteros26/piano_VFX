using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200034F RID: 847
	internal class ChildTable : Table
	{
		// Token: 0x06001F61 RID: 8033 RVA: 0x0004F7DB File Offset: 0x0004D9DB
		public ChildTable(Control parent)
		{
			this.parent = parent;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0004F7EA File Offset: 0x0004D9EA
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.ID == null)
			{
				writer.AddAttribute("id", this.parent.ClientID);
			}
		}

		// Token: 0x04001887 RID: 6279
		private Control parent;
	}
}
