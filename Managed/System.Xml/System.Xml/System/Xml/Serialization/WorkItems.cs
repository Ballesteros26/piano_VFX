using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200033D RID: 829
	internal class WorkItems
	{
		// Token: 0x17000683 RID: 1667
		internal ImportStructWorkItem this[int index]
		{
			get
			{
				return (ImportStructWorkItem)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x000AEEF4 File Offset: 0x000AD0F4
		internal int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000AEF01 File Offset: 0x000AD101
		internal void Add(ImportStructWorkItem item)
		{
			this.list.Add(item);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000AEF10 File Offset: 0x000AD110
		internal bool Contains(StructMapping mapping)
		{
			return this.IndexOf(mapping) >= 0;
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000AEF20 File Offset: 0x000AD120
		internal int IndexOf(StructMapping mapping)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Mapping == mapping)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000AEF50 File Offset: 0x000AD150
		internal void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x04001761 RID: 5985
		private ArrayList list = new ArrayList();
	}
}
