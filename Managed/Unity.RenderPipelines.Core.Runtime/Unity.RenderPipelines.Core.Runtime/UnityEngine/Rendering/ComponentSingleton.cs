using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001C RID: 28
	public static class ComponentSingleton<TType> where TType : Component
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000048A4 File Offset: 0x00002AA4
		public static TType instance
		{
			get
			{
				if (ComponentSingleton<TType>.s_Instance == null)
				{
					GameObject gameObject = new GameObject("Default " + typeof(TType));
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					gameObject.SetActive(false);
					ComponentSingleton<TType>.s_Instance = gameObject.AddComponent<TType>();
				}
				return ComponentSingleton<TType>.s_Instance;
			}
		}

		// Token: 0x0400008E RID: 142
		private static TType s_Instance;
	}
}
