using System;

namespace UnityEngine
{
	// Token: 0x02000196 RID: 406
	[AttributeUsage(4, AllowMultiple = false, Inherited = false)]
	public sealed class CreateAssetMenuAttribute : Attribute
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0001F360 File Offset: 0x0001D560
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0001F368 File Offset: 0x0001D568
		public string menuName { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0001F371 File Offset: 0x0001D571
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x0001F379 File Offset: 0x0001D579
		public string fileName { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0001F382 File Offset: 0x0001D582
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x0001F38A File Offset: 0x0001D58A
		public int order { get; set; }
	}
}
