using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B5 RID: 949
	internal interface IMenuRenderer
	{
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060026E5 RID: 9957
		HtmlTextWriterTag Tag { get; }

		// Token: 0x060026E6 RID: 9958
		void AddAttributesToRender(HtmlTextWriter writer);

		// Token: 0x060026E7 RID: 9959
		void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script);

		// Token: 0x060026E8 RID: 9960
		void RenderBeginTag(HtmlTextWriter writer, string skipLinkText);

		// Token: 0x060026E9 RID: 9961
		void RenderEndTag(HtmlTextWriter writer);

		// Token: 0x060026EA RID: 9962
		void RenderContents(HtmlTextWriter writer);

		// Token: 0x060026EB RID: 9963
		void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem);

		// Token: 0x060026EC RID: 9964
		void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

		// Token: 0x060026ED RID: 9965
		void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast);

		// Token: 0x060026EE RID: 9966
		void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

		// Token: 0x060026EF RID: 9967
		void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool notLast, bool isFirst);

		// Token: 0x060026F0 RID: 9968
		bool IsDynamicItem(MenuItem item);

		// Token: 0x060026F1 RID: 9969
		bool IsDynamicItem(Menu owner, MenuItem item);
	}
}
