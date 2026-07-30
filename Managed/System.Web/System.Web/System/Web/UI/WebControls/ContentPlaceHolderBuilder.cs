using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200035C RID: 860
	internal class ContentPlaceHolderBuilder : ControlBuilder
	{
		// Token: 0x06001FDA RID: 8154 RVA: 0x00050560 File Offset: 0x0004E760
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			string text = null;
			foreach (object obj in attribs.Keys)
			{
				string text2 = obj as string;
				if (!string.IsNullOrEmpty(text2) && string.Compare(text2, "id", StringComparison.OrdinalIgnoreCase) == 0)
				{
					text = attribs[text2] as string;
					break;
				}
			}
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			MasterPageParser masterPageParser = parser as MasterPageParser;
			if (masterPageParser == null || string.IsNullOrEmpty(text))
			{
				return;
			}
			masterPageParser.AddContentPlaceHolderId(text);
		}
	}
}
