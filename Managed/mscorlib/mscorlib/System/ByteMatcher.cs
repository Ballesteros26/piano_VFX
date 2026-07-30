using System;
using System.Collections;

namespace System
{
	// Token: 0x02000236 RID: 566
	internal class ByteMatcher
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x000666C6 File Offset: 0x000648C6
		public void AddMapping(TermInfoStrings key, byte[] val)
		{
			if (val.Length == 0)
			{
				return;
			}
			this.map[val] = key;
			this.starts[(int)val[0]] = true;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00002194 File Offset: 0x00000394
		public void Sort()
		{
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000666F8 File Offset: 0x000648F8
		public bool StartsWith(int c)
		{
			return this.starts[c] != null;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00066710 File Offset: 0x00064910
		public TermInfoStrings Match(char[] buffer, int offset, int length, out int used)
		{
			foreach (object obj in this.map.Keys)
			{
				byte[] array = (byte[])obj;
				int num = 0;
				while (num < array.Length && num < length && (char)array[num] == buffer[offset + num])
				{
					if (array.Length - 1 == num)
					{
						used = array.Length;
						return (TermInfoStrings)this.map[array];
					}
					num++;
				}
			}
			used = 0;
			return (TermInfoStrings)(-1);
		}

		// Token: 0x04000D82 RID: 3458
		private Hashtable map = new Hashtable();

		// Token: 0x04000D83 RID: 3459
		private Hashtable starts = new Hashtable();
	}
}
