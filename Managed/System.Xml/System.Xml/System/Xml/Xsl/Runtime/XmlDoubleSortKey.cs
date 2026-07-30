using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000620 RID: 1568
	internal class XmlDoubleSortKey : XmlSortKey
	{
		// Token: 0x06003D6B RID: 15723 RVA: 0x00153A70 File Offset: 0x00151C70
		public XmlDoubleSortKey(double value, XmlCollation collation)
		{
			if (double.IsNaN(value))
			{
				this.isNaN = true;
				this.dblVal = ((collation.EmptyGreatest != collation.DescendingOrder) ? double.PositiveInfinity : double.NegativeInfinity);
				return;
			}
			this.dblVal = (collation.DescendingOrder ? (-value) : value);
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x00153AD0 File Offset: 0x00151CD0
		public override int CompareTo(object obj)
		{
			XmlDoubleSortKey xmlDoubleSortKey = obj as XmlDoubleSortKey;
			if (xmlDoubleSortKey == null)
			{
				if (this.isNaN)
				{
					return base.BreakSortingTie(obj as XmlSortKey);
				}
				return base.CompareToEmpty(obj);
			}
			else if (this.dblVal == xmlDoubleSortKey.dblVal)
			{
				if (this.isNaN)
				{
					if (xmlDoubleSortKey.isNaN)
					{
						return base.BreakSortingTie(xmlDoubleSortKey);
					}
					if (this.dblVal != double.NegativeInfinity)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (!xmlDoubleSortKey.isNaN)
					{
						return base.BreakSortingTie(xmlDoubleSortKey);
					}
					if (xmlDoubleSortKey.dblVal != double.NegativeInfinity)
					{
						return -1;
					}
					return 1;
				}
			}
			else
			{
				if (this.dblVal >= xmlDoubleSortKey.dblVal)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x040027D4 RID: 10196
		private double dblVal;

		// Token: 0x040027D5 RID: 10197
		private bool isNaN;
	}
}
