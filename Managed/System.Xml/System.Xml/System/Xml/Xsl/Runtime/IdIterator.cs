using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000604 RID: 1540
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct IdIterator
	{
		// Token: 0x06003BF8 RID: 15352 RVA: 0x0014FD41 File Offset: 0x0014DF41
		public void Create(XPathNavigator context, string value)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.idrefs = XmlConvert.SplitString(value);
			this.idx = -1;
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x0014FD68 File Offset: 0x0014DF68
		public bool MoveNext()
		{
			for (;;)
			{
				this.idx++;
				if (this.idx >= this.idrefs.Length)
				{
					break;
				}
				if (this.navCurrent.MoveToId(this.idrefs[this.idx]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x0014FDA5 File Offset: 0x0014DFA5
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002772 RID: 10098
		private XPathNavigator navCurrent;

		// Token: 0x04002773 RID: 10099
		private string[] idrefs;

		// Token: 0x04002774 RID: 10100
		private int idx;
	}
}
