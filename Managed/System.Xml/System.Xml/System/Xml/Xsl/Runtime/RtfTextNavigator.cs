using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D6 RID: 1494
	internal sealed class RtfTextNavigator : RtfNavigator
	{
		// Token: 0x06003B10 RID: 15120 RVA: 0x0014D01E File Offset: 0x0014B21E
		public RtfTextNavigator(string text, string baseUri)
		{
			this.text = text;
			this.baseUri = baseUri;
			this.constr = new NavigatorConstructor();
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x0014D03F File Offset: 0x0014B23F
		public RtfTextNavigator(RtfTextNavigator that)
		{
			this.text = that.text;
			this.baseUri = that.baseUri;
			this.constr = that.constr;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x0014D06B File Offset: 0x0014B26B
		public override void CopyToWriter(XmlWriter writer)
		{
			writer.WriteString(this.Value);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x0014D079 File Offset: 0x0014B279
		public override XPathNavigator ToNavigator()
		{
			return this.constr.GetNavigator(this.text, this.baseUri, new NameTable());
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x0014D097 File Offset: 0x0014B297
		public override string Value
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x0014D09F File Offset: 0x0014B29F
		public override string BaseURI
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x0014D0A7 File Offset: 0x0014B2A7
		public override XPathNavigator Clone()
		{
			return new RtfTextNavigator(this);
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0014D0B0 File Offset: 0x0014B2B0
		public override bool MoveTo(XPathNavigator other)
		{
			RtfTextNavigator rtfTextNavigator = other as RtfTextNavigator;
			if (rtfTextNavigator != null)
			{
				this.text = rtfTextNavigator.text;
				this.baseUri = rtfTextNavigator.baseUri;
				this.constr = rtfTextNavigator.constr;
				return true;
			}
			return false;
		}

		// Token: 0x040026B1 RID: 9905
		private string text;

		// Token: 0x040026B2 RID: 9906
		private string baseUri;

		// Token: 0x040026B3 RID: 9907
		private NavigatorConstructor constr;
	}
}
