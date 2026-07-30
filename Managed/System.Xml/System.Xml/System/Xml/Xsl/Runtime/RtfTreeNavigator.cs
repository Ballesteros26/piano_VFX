using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D5 RID: 1493
	internal sealed class RtfTreeNavigator : RtfNavigator
	{
		// Token: 0x06003B08 RID: 15112 RVA: 0x0014CF48 File Offset: 0x0014B148
		public RtfTreeNavigator(XmlEventCache events, XmlNameTable nameTable)
		{
			this.events = events;
			this.constr = new NavigatorConstructor();
			this.nameTable = nameTable;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x0014CF69 File Offset: 0x0014B169
		public RtfTreeNavigator(RtfTreeNavigator that)
		{
			this.events = that.events;
			this.constr = that.constr;
			this.nameTable = that.nameTable;
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x0014CF95 File Offset: 0x0014B195
		public override void CopyToWriter(XmlWriter writer)
		{
			this.events.EventsToWriter(writer);
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x0014CFA3 File Offset: 0x0014B1A3
		public override XPathNavigator ToNavigator()
		{
			return this.constr.GetNavigator(this.events, this.nameTable);
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x0014CFBC File Offset: 0x0014B1BC
		public override string Value
		{
			get
			{
				return this.events.EventsToString();
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x0014CFC9 File Offset: 0x0014B1C9
		public override string BaseURI
		{
			get
			{
				return this.events.BaseUri;
			}
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0014CFD6 File Offset: 0x0014B1D6
		public override XPathNavigator Clone()
		{
			return new RtfTreeNavigator(this);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0014CFE0 File Offset: 0x0014B1E0
		public override bool MoveTo(XPathNavigator other)
		{
			RtfTreeNavigator rtfTreeNavigator = other as RtfTreeNavigator;
			if (rtfTreeNavigator != null)
			{
				this.events = rtfTreeNavigator.events;
				this.constr = rtfTreeNavigator.constr;
				this.nameTable = rtfTreeNavigator.nameTable;
				return true;
			}
			return false;
		}

		// Token: 0x040026AE RID: 9902
		private XmlEventCache events;

		// Token: 0x040026AF RID: 9903
		private NavigatorConstructor constr;

		// Token: 0x040026B0 RID: 9904
		private XmlNameTable nameTable;
	}
}
