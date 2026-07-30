using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020004BB RID: 1211
	[Serializable]
	internal class PathList
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x0008BEDD File Offset: 0x0008A0DD
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0008BEEC File Offset: 0x0008A0EC
		public int GetCookiesCount()
		{
			int num = 0;
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_list.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj;
					num += cookieCollection.Count;
				}
			}
			return num;
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0008BF7C File Offset: 0x0008A17C
		public ICollection Values
		{
			get
			{
				return this.m_list.Values;
			}
		}

		// Token: 0x17000766 RID: 1894
		public object this[string s]
		{
			get
			{
				return this.m_list[s];
			}
			set
			{
				object syncRoot = this.SyncRoot;
				lock (syncRoot)
				{
					this.m_list[s] = value;
				}
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0008BFE0 File Offset: 0x0008A1E0
		public IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x0008BFED File Offset: 0x0008A1ED
		public object SyncRoot
		{
			get
			{
				return this.m_list.SyncRoot;
			}
		}

		// Token: 0x04001FEB RID: 8171
		private SortedList m_list = SortedList.Synchronized(new SortedList(PathList.PathListComparer.StaticInstance));

		// Token: 0x020004BC RID: 1212
		[Serializable]
		private class PathListComparer : IComparer
		{
			// Token: 0x060023D5 RID: 9173 RVA: 0x0008BFFC File Offset: 0x0008A1FC
			int IComparer.Compare(object ol, object or)
			{
				string text = CookieParser.CheckQuoted((string)ol);
				string text2 = CookieParser.CheckQuoted((string)or);
				int length = text.Length;
				int length2 = text2.Length;
				int num = Math.Min(length, length2);
				for (int i = 0; i < num; i++)
				{
					if (text[i] != text2[i])
					{
						return (int)(text[i] - text2[i]);
					}
				}
				return length2 - length;
			}

			// Token: 0x04001FEC RID: 8172
			internal static readonly PathList.PathListComparer StaticInstance = new PathList.PathListComparer();
		}
	}
}
