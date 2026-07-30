using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000622 RID: 1570
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XmlSortKeyAccumulator
	{
		// Token: 0x06003D6E RID: 15726 RVA: 0x00153B85 File Offset: 0x00151D85
		public void Create()
		{
			if (this.keys == null)
			{
				this.keys = new XmlSortKey[64];
			}
			this.pos = 0;
			this.keys[0] = null;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x00153BAC File Offset: 0x00151DAC
		public void AddStringSortKey(XmlCollation collation, string value)
		{
			this.AppendSortKey(collation.CreateSortKey(value));
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x00153BBB File Offset: 0x00151DBB
		public void AddDecimalSortKey(XmlCollation collation, decimal value)
		{
			this.AppendSortKey(new XmlDecimalSortKey(value, collation));
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x00153BCA File Offset: 0x00151DCA
		public void AddIntegerSortKey(XmlCollation collation, long value)
		{
			this.AppendSortKey(new XmlIntegerSortKey(value, collation));
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x00153BD9 File Offset: 0x00151DD9
		public void AddIntSortKey(XmlCollation collation, int value)
		{
			this.AppendSortKey(new XmlIntSortKey(value, collation));
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x00153BE8 File Offset: 0x00151DE8
		public void AddDoubleSortKey(XmlCollation collation, double value)
		{
			this.AppendSortKey(new XmlDoubleSortKey(value, collation));
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00153BF7 File Offset: 0x00151DF7
		public void AddDateTimeSortKey(XmlCollation collation, DateTime value)
		{
			this.AppendSortKey(new XmlDateTimeSortKey(value, collation));
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00153C06 File Offset: 0x00151E06
		public void AddEmptySortKey(XmlCollation collation)
		{
			this.AppendSortKey(new XmlEmptySortKey(collation));
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x00153C14 File Offset: 0x00151E14
		public void FinishSortKeys()
		{
			this.pos++;
			if (this.pos >= this.keys.Length)
			{
				XmlSortKey[] array = new XmlSortKey[this.pos * 2];
				Array.Copy(this.keys, 0, array, 0, this.keys.Length);
				this.keys = array;
			}
			this.keys[this.pos] = null;
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x00153C78 File Offset: 0x00151E78
		private void AppendSortKey(XmlSortKey key)
		{
			key.Priority = this.pos;
			if (this.keys[this.pos] == null)
			{
				this.keys[this.pos] = key;
				return;
			}
			this.keys[this.pos].AddSortKey(key);
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x00153CB8 File Offset: 0x00151EB8
		public Array Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x040027D6 RID: 10198
		private XmlSortKey[] keys;

		// Token: 0x040027D7 RID: 10199
		private int pos;

		// Token: 0x040027D8 RID: 10200
		private const int DefaultSortKeyCount = 64;
	}
}
