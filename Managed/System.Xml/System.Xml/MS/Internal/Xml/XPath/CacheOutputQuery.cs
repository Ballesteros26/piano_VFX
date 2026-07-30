using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000011 RID: 17
	internal abstract class CacheOutputQuery : Query
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002C48 File Offset: 0x00000E48
		public CacheOutputQuery(Query input)
		{
			this.input = input;
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C69 File Offset: 0x00000E69
		protected CacheOutputQuery(CacheOutputQuery other)
			: base(other)
		{
			this.input = Query.Clone(other.input);
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002979 File Offset: 0x00000B79
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public override void SetXsltContext(XsltContext context)
		{
			this.input.SetXsltContext(context);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002CAE File Offset: 0x00000EAE
		public override object Evaluate(XPathNodeIterator context)
		{
			this.outputBuffer.Clear();
			this.count = 0;
			return this.input.Evaluate(context);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public override XPathNavigator Advance()
		{
			if (this.count < this.outputBuffer.Count)
			{
				List<XPathNavigator> list = this.outputBuffer;
				int count = this.count;
				this.count = count + 1;
				return list[count];
			}
			return null;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D0E File Offset: 0x00000F0E
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this.outputBuffer[this.count - 1];
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000029F5 File Offset: 0x00000BF5
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D2D File Offset: 0x00000F2D
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002A0A File Offset: 0x00000C0A
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D3A File Offset: 0x00000F3A
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.input.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x0400006C RID: 108
		internal Query input;

		// Token: 0x0400006D RID: 109
		protected List<XPathNavigator> outputBuffer;
	}
}
