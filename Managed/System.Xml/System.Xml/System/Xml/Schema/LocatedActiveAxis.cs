using System;

namespace System.Xml.Schema
{
	// Token: 0x02000391 RID: 913
	internal class LocatedActiveAxis : ActiveAxis
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x000DF7CE File Offset: 0x000DD9CE
		internal int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000DF7D6 File Offset: 0x000DD9D6
		internal LocatedActiveAxis(Asttree astfield, KeySequence ks, int column)
			: base(astfield)
		{
			this.Ks = ks;
			this.column = column;
			this.isMatched = false;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000DF7F4 File Offset: 0x000DD9F4
		internal void Reactivate(KeySequence ks)
		{
			base.Reactivate();
			this.Ks = ks;
		}

		// Token: 0x0400190A RID: 6410
		private int column;

		// Token: 0x0400190B RID: 6411
		internal bool isMatched;

		// Token: 0x0400190C RID: 6412
		internal KeySequence Ks;
	}
}
