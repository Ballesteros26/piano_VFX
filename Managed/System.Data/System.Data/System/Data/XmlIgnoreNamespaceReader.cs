using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000106 RID: 262
	internal sealed class XmlIgnoreNamespaceReader : XmlNodeReader
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x00042FFA File Offset: 0x000411FA
		internal XmlIgnoreNamespaceReader(XmlDocument xdoc, string[] namespacesToIgnore)
			: base(xdoc)
		{
			this._namespacesToIgnore = new List<string>(namespacesToIgnore);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00043010 File Offset: 0x00041210
		public override bool MoveToFirstAttribute()
		{
			return base.MoveToFirstAttribute() && ((!this._namespacesToIgnore.Contains(this.NamespaceURI) && (!(this.NamespaceURI == "http://www.w3.org/XML/1998/namespace") || !(this.LocalName != "lang"))) || this.MoveToNextAttribute());
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00043068 File Offset: 0x00041268
		public override bool MoveToNextAttribute()
		{
			bool flag;
			bool flag2;
			do
			{
				flag = false;
				flag2 = false;
				if (base.MoveToNextAttribute())
				{
					flag = true;
					if (this._namespacesToIgnore.Contains(this.NamespaceURI) || (this.NamespaceURI == "http://www.w3.org/XML/1998/namespace" && this.LocalName != "lang"))
					{
						flag2 = true;
					}
				}
			}
			while (flag2);
			return flag;
		}

		// Token: 0x040008D6 RID: 2262
		private List<string> _namespacesToIgnore;
	}
}
