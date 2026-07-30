using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000043 RID: 67
	internal sealed class XPathSortComparer : IComparer<SortKey>
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00006D95 File Offset: 0x00004F95
		public XPathSortComparer(int size)
		{
			if (size <= 0)
			{
				size = 3;
			}
			this.expressions = new Query[size];
			this.comparers = new IComparer[size];
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006DBC File Offset: 0x00004FBC
		public XPathSortComparer()
			: this(3)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public void AddSort(Query evalQuery, IComparer comparer)
		{
			if (this.numSorts == this.expressions.Length)
			{
				Query[] array = new Query[this.numSorts * 2];
				IComparer[] array2 = new IComparer[this.numSorts * 2];
				for (int i = 0; i < this.numSorts; i++)
				{
					array[i] = this.expressions[i];
					array2[i] = this.comparers[i];
				}
				this.expressions = array;
				this.comparers = array2;
			}
			if (evalQuery.StaticType == XPathResultType.NodeSet || evalQuery.StaticType == XPathResultType.Any)
			{
				evalQuery = new StringFunctions(Function.FunctionType.FuncString, new Query[] { evalQuery });
			}
			this.expressions[this.numSorts] = evalQuery;
			this.comparers[this.numSorts] = comparer;
			this.numSorts++;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006E84 File Offset: 0x00005084
		public int NumSorts
		{
			get
			{
				return this.numSorts;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006E8C File Offset: 0x0000508C
		public Query Expression(int i)
		{
			return this.expressions[i];
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006E98 File Offset: 0x00005098
		int IComparer<SortKey>.Compare(SortKey x, SortKey y)
		{
			for (int i = 0; i < x.NumKeys; i++)
			{
				int num = this.comparers[i].Compare(x[i], y[i]);
				if (num != 0)
				{
					return num;
				}
			}
			return x.OriginalPosition - y.OriginalPosition;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006EE8 File Offset: 0x000050E8
		internal XPathSortComparer Clone()
		{
			XPathSortComparer xpathSortComparer = new XPathSortComparer(this.numSorts);
			for (int i = 0; i < this.numSorts; i++)
			{
				xpathSortComparer.comparers[i] = this.comparers[i];
				xpathSortComparer.expressions[i] = (Query)this.expressions[i].Clone();
			}
			xpathSortComparer.numSorts = this.numSorts;
			return xpathSortComparer;
		}

		// Token: 0x040000FD RID: 253
		private const int minSize = 3;

		// Token: 0x040000FE RID: 254
		private Query[] expressions;

		// Token: 0x040000FF RID: 255
		private IComparer[] comparers;

		// Token: 0x04000100 RID: 256
		private int numSorts;
	}
}
