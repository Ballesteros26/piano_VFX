using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000F RID: 15
	internal abstract class CacheAxisQuery : BaseAxisQuery
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002934 File Offset: 0x00000B34
		public CacheAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002953 File Offset: 0x00000B53
		protected CacheAxisQuery(CacheAxisQuery other)
			: base(other)
		{
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002979 File Offset: 0x00000B79
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002982 File Offset: 0x00000B82
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			this.outputBuffer.Clear();
			return this;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002998 File Offset: 0x00000B98
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029D6 File Offset: 0x00000BD6
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029F5 File Offset: 0x00000BF5
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000029FD File Offset: 0x00000BFD
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002A0A File Offset: 0x00000C0A
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04000067 RID: 103
		protected List<XPathNavigator> outputBuffer;
	}
}
