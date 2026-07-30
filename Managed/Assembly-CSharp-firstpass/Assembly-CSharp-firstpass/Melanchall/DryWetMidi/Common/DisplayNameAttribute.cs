using System;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C4 RID: 452
	internal sealed class DisplayNameAttribute : Attribute
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00024AD0 File Offset: 0x00022CD0
		public DisplayNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00024ADF File Offset: 0x00022CDF
		public string Name { get; }
	}
}
