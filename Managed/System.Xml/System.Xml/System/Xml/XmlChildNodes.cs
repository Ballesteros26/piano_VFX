using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x0200021A RID: 538
	internal class XmlChildNodes : XmlNodeList
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x00072D9B File Offset: 0x00070F9B
		public XmlChildNodes(XmlNode container)
		{
			this.container = container;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00072DAC File Offset: 0x00070FAC
		public override XmlNode Item(int i)
		{
			if (i < 0)
			{
				return null;
			}
			XmlNode xmlNode = this.container.FirstChild;
			while (xmlNode != null)
			{
				if (i == 0)
				{
					return xmlNode;
				}
				xmlNode = xmlNode.NextSibling;
				i--;
			}
			return null;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00072DE4 File Offset: 0x00070FE4
		public override int Count
		{
			get
			{
				int num = 0;
				for (XmlNode xmlNode = this.container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00072E10 File Offset: 0x00071010
		public override IEnumerator GetEnumerator()
		{
			if (this.container.FirstChild == null)
			{
				return XmlDocument.EmptyEnumerator;
			}
			return new XmlChildEnumerator(this.container);
		}

		// Token: 0x04000D85 RID: 3461
		private XmlNode container;
	}
}
