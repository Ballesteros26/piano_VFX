using System;
using System.Collections;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000654 RID: 1620
	internal sealed class SubstitutionList
	{
		// Token: 0x06004128 RID: 16680 RVA: 0x0015BC96 File Offset: 0x00159E96
		public SubstitutionList()
		{
			this.s = new ArrayList(4);
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x0015BCAA File Offset: 0x00159EAA
		public void AddSubstitutionPair(QilNode find, QilNode replace)
		{
			this.s.Add(find);
			this.s.Add(replace);
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x0015BCC6 File Offset: 0x00159EC6
		public void RemoveLastSubstitutionPair()
		{
			this.s.RemoveRange(this.s.Count - 2, 2);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x0015BCE1 File Offset: 0x00159EE1
		public void RemoveLastNSubstitutionPairs(int n)
		{
			if (n > 0)
			{
				n *= 2;
				this.s.RemoveRange(this.s.Count - n, n);
			}
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x0015BD08 File Offset: 0x00159F08
		public QilNode FindReplacement(QilNode n)
		{
			for (int i = this.s.Count - 2; i >= 0; i -= 2)
			{
				if (this.s[i] == n)
				{
					return (QilNode)this.s[i + 1];
				}
			}
			return null;
		}

		// Token: 0x040028F5 RID: 10485
		private ArrayList s;
	}
}
