using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000077 RID: 119
	public static class HDAdditionalReflectionDataExtensions
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0002B4E4 File Offset: 0x000296E4
		public static void RequestRenderNextUpdate(this ReflectionProbe probe)
		{
			HDAdditionalReflectionData component = probe.GetComponent<HDAdditionalReflectionData>();
			if (component != null && !component.Equals(null))
			{
				component.RequestRenderNextUpdate();
			}
		}
	}
}
