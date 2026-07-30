using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200035A RID: 858
	internal class ContentBuilderInternal : TemplateBuilder
	{
		// Token: 0x06001FD6 RID: 8150 RVA: 0x00050510 File Offset: 0x0004E710
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			this.placeHolderID = attribs["ContentPlaceHolderID"] as string;
			if (string.IsNullOrEmpty(this.placeHolderID))
			{
				throw new HttpException("Missing required 'ContentPlaceHolderID' attribute");
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00050550 File Offset: 0x0004E750
		public string ContentPlaceHolderID
		{
			get
			{
				return this.placeHolderID;
			}
		}

		// Token: 0x0400188C RID: 6284
		private string placeHolderID;
	}
}
