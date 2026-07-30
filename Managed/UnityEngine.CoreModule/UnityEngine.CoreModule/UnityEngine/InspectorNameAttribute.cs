using System;

namespace UnityEngine
{
	// Token: 0x02000180 RID: 384
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public class InspectorNameAttribute : PropertyAttribute
	{
		// Token: 0x0600128D RID: 4749 RVA: 0x0001E7AE File Offset: 0x0001C9AE
		public InspectorNameAttribute(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x0400061E RID: 1566
		public readonly string displayName;
	}
}
