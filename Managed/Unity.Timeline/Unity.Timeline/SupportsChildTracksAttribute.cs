using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal class SupportsChildTracksAttribute : Attribute
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00009357 File Offset: 0x00007557
		public SupportsChildTracksAttribute(Type childType = null, int levels = 2147483647)
		{
			this.childType = childType;
			this.levels = levels;
		}

		// Token: 0x040000ED RID: 237
		public readonly Type childType;

		// Token: 0x040000EE RID: 238
		public readonly int levels;
	}
}
