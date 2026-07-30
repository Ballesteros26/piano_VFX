using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000064 RID: 100
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class VolumeComponentMenu : Attribute
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000C452 File Offset: 0x0000A652
		public VolumeComponentMenu(string menu)
		{
			this.menu = menu;
		}

		// Token: 0x0400019D RID: 413
		public readonly string menu;
	}
}
