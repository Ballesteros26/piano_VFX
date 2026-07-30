using System;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x02000093 RID: 147
	public class StageRuntimeInterface
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public StageRuntimeInterface(Func<bool, GameObject> AddGameObject, Func<Camera> GetCamera, Func<Light> GetSunLight)
		{
			this.m_AddGameObject = AddGameObject;
			this.m_GetCamera = GetCamera;
			this.m_GetSunLight = GetSunLight;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		public GameObject AddGameObject(bool persistent = false)
		{
			Func<bool, GameObject> addGameObject = this.m_AddGameObject;
			if (addGameObject == null)
			{
				return null;
			}
			return addGameObject(persistent);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000DDE1 File Offset: 0x0000BFE1
		public Camera camera
		{
			get
			{
				Func<Camera> getCamera = this.m_GetCamera;
				if (getCamera == null)
				{
					return null;
				}
				return getCamera();
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public Light sunLight
		{
			get
			{
				Func<Light> getSunLight = this.m_GetSunLight;
				if (getSunLight == null)
				{
					return null;
				}
				return getSunLight();
			}
		}

		// Token: 0x040001D0 RID: 464
		private Func<bool, GameObject> m_AddGameObject;

		// Token: 0x040001D1 RID: 465
		private Func<Camera> m_GetCamera;

		// Token: 0x040001D2 RID: 466
		private Func<Light> m_GetSunLight;

		// Token: 0x040001D3 RID: 467
		public object SRPData;
	}
}
