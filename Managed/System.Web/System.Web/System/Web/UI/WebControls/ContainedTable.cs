using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000358 RID: 856
	internal class ContainedTable : Table
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x00050463 File Offset: 0x0004E663
		public ContainedTable(WebControl container)
		{
			this._container = container;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00050472 File Offset: 0x0004E672
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.ControlStyle.CopyFrom(this._container.ControlStyle);
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this._container.ClientID);
		}

		// Token: 0x0400188B RID: 6283
		private WebControl _container;
	}
}
