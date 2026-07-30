using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200005D RID: 93
	public class TMP_UpdateRegistry
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00022F0E File Offset: 0x0002110E
		public static TMP_UpdateRegistry instance
		{
			get
			{
				if (TMP_UpdateRegistry.s_Instance == null)
				{
					TMP_UpdateRegistry.s_Instance = new TMP_UpdateRegistry();
				}
				return TMP_UpdateRegistry.s_Instance;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00022F28 File Offset: 0x00021128
		protected TMP_UpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdateForCanvasRendererObjects;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00022F78 File Offset: 0x00021178
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00022F88 File Offset: 0x00021188
		private bool InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(instanceID))
			{
				return false;
			}
			this.m_LayoutQueueLookup.Add(instanceID);
			this.m_LayoutRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00022FCB File Offset: 0x000211CB
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00022FDC File Offset: 0x000211DC
		private bool InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(instanceID))
			{
				return false;
			}
			this.m_GraphicQueueLookup.Add(instanceID);
			this.m_GraphicRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00023020 File Offset: 0x00021220
		private void PerformUpdateForCanvasRendererObjects()
		{
			for (int i = 0; i < this.m_LayoutRebuildQueue.Count; i++)
			{
				TMP_UpdateRegistry.instance.m_LayoutRebuildQueue[i].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int j = 0; j < this.m_GraphicRebuildQueue.Count; j++)
			{
				TMP_UpdateRegistry.instance.m_GraphicRebuildQueue[j].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000230CD File Offset: 0x000212CD
		private void PerformUpdateForMeshRendererObjects()
		{
			Debug.Log("Perform update of MeshRenderer objects.");
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000230D9 File Offset: 0x000212D9
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			TMP_UpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000230F4 File Offset: 0x000212F4
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			TMP_UpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(instanceID);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0002312C File Offset: 0x0002132C
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			TMP_UpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(instanceID);
		}

		// Token: 0x04000446 RID: 1094
		private static TMP_UpdateRegistry s_Instance;

		// Token: 0x04000447 RID: 1095
		private readonly List<ICanvasElement> m_LayoutRebuildQueue = new List<ICanvasElement>();

		// Token: 0x04000448 RID: 1096
		private HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x04000449 RID: 1097
		private readonly List<ICanvasElement> m_GraphicRebuildQueue = new List<ICanvasElement>();

		// Token: 0x0400044A RID: 1098
		private HashSet<int> m_GraphicQueueLookup = new HashSet<int>();
	}
}
