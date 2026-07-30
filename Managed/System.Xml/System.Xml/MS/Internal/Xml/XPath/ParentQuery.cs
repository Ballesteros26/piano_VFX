using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000036 RID: 54
	internal sealed class ParentQuery : CacheAxisQuery
	{
		// Token: 0x06000171 RID: 369 RVA: 0x000059BA File Offset: 0x00003BBA
		public ParentQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type)
			: base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000059C7 File Offset: 0x00003BC7
		private ParentQuery(ParentQuery other)
			: base(other)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000059D0 File Offset: 0x00003BD0
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.qyInput.Advance()) != null)
			{
				xpathNavigator = xpathNavigator.Clone();
				if (xpathNavigator.MoveToParent() && this.matches(xpathNavigator))
				{
					base.Insert(this.outputBuffer, xpathNavigator);
				}
			}
			return this;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005A1D File Offset: 0x00003C1D
		public override XPathNodeIterator Clone()
		{
			return new ParentQuery(this);
		}
	}
}
