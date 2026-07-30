using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000088 RID: 136
	[MeansImplicitUse]
	public sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00002059 File Offset: 0x00000259
		public PublicAPIAttribute()
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004145 File Offset: 0x00002345
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00004157 File Offset: 0x00002357
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000415F File Offset: 0x0000235F
		[NotNull]
		public string Comment { get; private set; }
	}
}
