using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000156 RID: 342
	public static class GameObjectExtension
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x0004EDEE File Offset: 0x0004CFEE
		public static HDAdditionalLightData AddHDLight(this GameObject gameObject, HDLightTypeAndShape lightTypeAndShape)
		{
			HDAdditionalLightData hdadditionalLightData = gameObject.AddComponent<HDAdditionalLightData>();
			HDAdditionalLightData.InitDefaultHDAdditionalLightData(hdadditionalLightData);
			hdadditionalLightData.SetLightTypeAndShape(lightTypeAndShape);
			return hdadditionalLightData;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0004EE03 File Offset: 0x0004D003
		public static void RemoveHDLight(this GameObject gameObject)
		{
			Object component = gameObject.GetComponent<Light>();
			CoreUtils.Destroy(gameObject.GetComponent<HDAdditionalLightData>());
			CoreUtils.Destroy(component);
		}
	}
}
