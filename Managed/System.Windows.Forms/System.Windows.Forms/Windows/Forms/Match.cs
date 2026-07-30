using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200025F RID: 607
	internal class Match
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x00098B4C File Offset: 0x00096D4C
		// (set) Token: 0x060027B9 RID: 10169 RVA: 0x00098B40 File Offset: 0x00096D40
		public string MimeType
		{
			get
			{
				return this.mimeType;
			}
			set
			{
				this.mimeType = value;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x00098B60 File Offset: 0x00096D60
		// (set) Token: 0x060027BB RID: 10171 RVA: 0x00098B54 File Offset: 0x00096D54
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x00098B68 File Offset: 0x00096D68
		public ArrayList Matchlets
		{
			get
			{
				return this.matchlets;
			}
		}

		// Token: 0x040013DC RID: 5084
		private string mimeType;

		// Token: 0x040013DD RID: 5085
		private int priority;

		// Token: 0x040013DE RID: 5086
		private ArrayList matchlets = new ArrayList();
	}
}
