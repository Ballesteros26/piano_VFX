using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000039 RID: 57
	public class MousePositionDebug
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000779C File Offset: 0x0000599C
		public static MousePositionDebug instance
		{
			get
			{
				if (MousePositionDebug.s_Instance == null)
				{
					MousePositionDebug.s_Instance = new MousePositionDebug();
				}
				return MousePositionDebug.s_Instance;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00002788 File Offset: 0x00000988
		public void Build()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00002788 File Offset: 0x00000988
		public void Cleanup()
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000077B4 File Offset: 0x000059B4
		public Vector2 GetMousePosition(float ScreenHeight, bool sceneView)
		{
			return Input.mousePosition;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000077C0 File Offset: 0x000059C0
		public Vector2 GetMouseClickPosition(float ScreenHeight)
		{
			return Vector2.zero;
		}

		// Token: 0x040000FD RID: 253
		private static MousePositionDebug s_Instance;
	}
}
