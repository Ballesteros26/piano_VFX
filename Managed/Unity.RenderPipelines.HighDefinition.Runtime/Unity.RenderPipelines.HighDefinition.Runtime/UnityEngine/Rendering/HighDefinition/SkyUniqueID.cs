using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000164 RID: 356
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SkyUniqueID : Attribute
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x00052566 File Offset: 0x00050766
		public SkyUniqueID(int uniqueID)
		{
			this.uniqueID = uniqueID;
		}

		// Token: 0x04000FE5 RID: 4069
		internal readonly int uniqueID;
	}
}
