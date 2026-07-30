using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000084 RID: 132
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Reflection-Proxy-Volume.html")]
	[AddComponentMenu("Rendering/Reflection Proxy Volume")]
	public class ReflectionProxyVolumeComponent : MonoBehaviour
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0002D970 File Offset: 0x0002BB70
		public ProxyVolume proxyVolume
		{
			get
			{
				return this.m_ProxyVolume;
			}
		}

		// Token: 0x04000578 RID: 1400
		[SerializeField]
		private ProxyVolume m_ProxyVolume = new ProxyVolume();
	}
}
