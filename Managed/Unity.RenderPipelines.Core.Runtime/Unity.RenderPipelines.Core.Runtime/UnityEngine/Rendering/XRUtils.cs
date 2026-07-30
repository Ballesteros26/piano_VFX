using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000062 RID: 98
	public static class XRUtils
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000C298 File Offset: 0x0000A498
		public static void DrawOcclusionMesh(CommandBuffer cmd, Camera camera, bool stereoEnabled = true)
		{
			if (!XRGraphics.enabled || !camera.stereoEnabled || !stereoEnabled)
			{
				return;
			}
			RectInt rectInt = new RectInt(0, 0, camera.pixelWidth, camera.pixelHeight);
			cmd.DrawOcclusionMesh(rectInt);
		}
	}
}
