using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200008B RID: 139
	[AttributeUsage(2048)]
	public class PathReferenceAttribute : Attribute
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00002059 File Offset: 0x00000259
		public PathReferenceAttribute()
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004168 File Offset: 0x00002368
		public PathReferenceAttribute([PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000417A File Offset: 0x0000237A
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00004182 File Offset: 0x00002382
		[NotNull]
		public string BasePath { get; private set; }
	}
}
