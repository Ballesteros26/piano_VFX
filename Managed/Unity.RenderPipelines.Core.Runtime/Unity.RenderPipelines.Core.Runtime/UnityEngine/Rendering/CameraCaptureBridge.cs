using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000057 RID: 87
	public static class CameraCaptureBridge
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A124 File Offset: 0x00008324
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A12B File Offset: 0x0000832B
		public static bool enabled
		{
			get
			{
				return CameraCaptureBridge._enabled;
			}
			set
			{
				CameraCaptureBridge._enabled = value;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A134 File Offset: 0x00008334
		public static IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> GetCaptureActions(Camera camera)
		{
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			if (!CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet))
			{
				return null;
			}
			return hashSet.GetEnumerator();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A160 File Offset: 0x00008360
		public static void AddCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet);
			if (hashSet == null)
			{
				hashSet = new HashSet<Action<RenderTargetIdentifier, CommandBuffer>>();
				CameraCaptureBridge.actionDict.Add(camera, hashSet);
			}
			hashSet.Add(action);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A198 File Offset: 0x00008398
		public static void RemoveCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			if (camera == null)
			{
				return;
			}
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			if (CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet))
			{
				hashSet.Remove(action);
			}
		}

		// Token: 0x04000171 RID: 369
		private static Dictionary<Camera, HashSet<Action<RenderTargetIdentifier, CommandBuffer>>> actionDict = new Dictionary<Camera, HashSet<Action<RenderTargetIdentifier, CommandBuffer>>>();

		// Token: 0x04000172 RID: 370
		private static bool _enabled;
	}
}
