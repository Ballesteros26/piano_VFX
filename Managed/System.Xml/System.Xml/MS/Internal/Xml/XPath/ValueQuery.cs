using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000046 RID: 70
	internal abstract class ValueQuery : Query
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00003662 File Offset: 0x00001862
		public ValueQuery()
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007998 File Offset: 0x00005B98
		protected ValueQuery(ValueQuery other)
			: base(other)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00002F50 File Offset: 0x00001150
		public sealed override void Reset()
		{
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000079A1 File Offset: 0x00005BA1
		public sealed override XPathNavigator Current
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000079A1 File Offset: 0x00005BA1
		public sealed override int CurrentPosition
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000079A1 File Offset: 0x00005BA1
		public sealed override int Count
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000079A1 File Offset: 0x00005BA1
		public sealed override XPathNavigator Advance()
		{
			throw XPathException.Create("Expression must evaluate to a node-set.");
		}
	}
}
