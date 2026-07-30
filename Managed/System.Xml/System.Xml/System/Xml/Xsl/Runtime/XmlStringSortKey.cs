using System;
using System.Globalization;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200061F RID: 1567
	internal class XmlStringSortKey : XmlSortKey
	{
		// Token: 0x06003D68 RID: 15720 RVA: 0x00153962 File Offset: 0x00151B62
		public XmlStringSortKey(SortKey sortKey, bool descendingOrder)
		{
			this.sortKey = sortKey;
			this.descendingOrder = descendingOrder;
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x00153978 File Offset: 0x00151B78
		public XmlStringSortKey(byte[] sortKey, bool descendingOrder)
		{
			this.sortKeyBytes = sortKey;
			this.descendingOrder = descendingOrder;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x00153990 File Offset: 0x00151B90
		public override int CompareTo(object obj)
		{
			XmlStringSortKey xmlStringSortKey = obj as XmlStringSortKey;
			if (xmlStringSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			int num;
			if (this.sortKey != null)
			{
				num = SortKey.Compare(this.sortKey, xmlStringSortKey.sortKey);
			}
			else
			{
				int num2 = ((this.sortKeyBytes.Length < xmlStringSortKey.sortKeyBytes.Length) ? this.sortKeyBytes.Length : xmlStringSortKey.sortKeyBytes.Length);
				for (int i = 0; i < num2; i++)
				{
					if (this.sortKeyBytes[i] < xmlStringSortKey.sortKeyBytes[i])
					{
						num = -1;
						goto IL_00BC;
					}
					if (this.sortKeyBytes[i] > xmlStringSortKey.sortKeyBytes[i])
					{
						num = 1;
						goto IL_00BC;
					}
				}
				if (this.sortKeyBytes.Length < xmlStringSortKey.sortKeyBytes.Length)
				{
					num = -1;
				}
				else if (this.sortKeyBytes.Length > xmlStringSortKey.sortKeyBytes.Length)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			IL_00BC:
			if (num == 0)
			{
				return base.BreakSortingTie(xmlStringSortKey);
			}
			if (!this.descendingOrder)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x040027D1 RID: 10193
		private SortKey sortKey;

		// Token: 0x040027D2 RID: 10194
		private byte[] sortKeyBytes;

		// Token: 0x040027D3 RID: 10195
		private bool descendingOrder;
	}
}
