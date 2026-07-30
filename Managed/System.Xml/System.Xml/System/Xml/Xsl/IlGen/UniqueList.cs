using System;
using System.Collections.Generic;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000662 RID: 1634
	internal class UniqueList<T>
	{
		// Token: 0x060041CE RID: 16846 RVA: 0x0015FBB0 File Offset: 0x0015DDB0
		public int Add(T value)
		{
			int num;
			if (!this.lookup.ContainsKey(value))
			{
				num = this.list.Count;
				this.lookup.Add(value, num);
				this.list.Add(value);
			}
			else
			{
				num = this.lookup[value];
			}
			return num;
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x0015FC00 File Offset: 0x0015DE00
		public T[] ToArray()
		{
			return this.list.ToArray();
		}

		// Token: 0x04002A36 RID: 10806
		private Dictionary<T, int> lookup = new Dictionary<T, int>();

		// Token: 0x04002A37 RID: 10807
		private List<T> list = new List<T>();
	}
}
