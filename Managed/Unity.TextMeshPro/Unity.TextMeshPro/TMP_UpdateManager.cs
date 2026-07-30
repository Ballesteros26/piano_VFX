using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200005C RID: 92
	public class TMP_UpdateManager
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00022BEC File Offset: 0x00020DEC
		public static TMP_UpdateManager instance
		{
			get
			{
				if (TMP_UpdateManager.s_Instance == null)
				{
					TMP_UpdateManager.s_Instance = new TMP_UpdateManager();
				}
				return TMP_UpdateManager.s_Instance;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00022C04 File Offset: 0x00020E04
		protected TMP_UpdateManager()
		{
			Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(this.OnCameraPreCull));
			RenderPipelineManager.beginFrameRendering += this.OnBeginFrameRendering;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00022C8A File Offset: 0x00020E8A
		internal static void RegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00022C98 File Offset: 0x00020E98
		private void InternalRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			if (this.m_InternalUpdateLookup.Contains(instanceID))
			{
				return;
			}
			this.m_InternalUpdateLookup.Add(instanceID);
			this.m_InternalUpdateQueue.Add(textObject);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00022CD4 File Offset: 0x00020ED4
		public static void RegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForLayoutRebuild(element);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00022CE4 File Offset: 0x00020EE4
		private bool InternalRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(instanceID))
			{
				return false;
			}
			this.m_LayoutQueueLookup.Add(instanceID);
			this.m_LayoutRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00022D22 File Offset: 0x00020F22
		public static void RegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForGraphicRebuild(element);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00022D30 File Offset: 0x00020F30
		private bool InternalRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(instanceID))
			{
				return false;
			}
			this.m_GraphicQueueLookup.Add(instanceID);
			this.m_GraphicRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00022D6E File Offset: 0x00020F6E
		private void OnBeginFrameRendering(ScriptableRenderContext renderContext, Camera[] cameras)
		{
			this.DoRebuilds();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00022D6E File Offset: 0x00020F6E
		private void OnCameraPreCull(Camera cam)
		{
			this.DoRebuilds();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00022D78 File Offset: 0x00020F78
		private void DoRebuilds()
		{
			for (int i = 0; i < this.m_InternalUpdateQueue.Count; i++)
			{
				this.m_InternalUpdateQueue[i].InternalUpdate();
			}
			for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
			{
				this.m_LayoutRebuildQueue[j].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int k = 0; k < this.m_GraphicRebuildQueue.Count; k++)
			{
				this.m_GraphicRebuildQueue[k].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00022E44 File Offset: 0x00021044
		internal static void UnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00022E51 File Offset: 0x00021051
		public static void UnRegisterTextElementForRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForGraphicRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForLayoutRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(element);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00022E74 File Offset: 0x00021074
		private void InternalUnRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			TMP_UpdateManager.instance.m_GraphicRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(instanceID);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00022EA8 File Offset: 0x000210A8
		private void InternalUnRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			TMP_UpdateManager.instance.m_LayoutRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(instanceID);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00022EDC File Offset: 0x000210DC
		private void InternalUnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			TMP_UpdateManager.instance.m_InternalUpdateQueue.Remove(textObject);
			this.m_InternalUpdateLookup.Remove(instanceID);
		}

		// Token: 0x0400043F RID: 1087
		private static TMP_UpdateManager s_Instance;

		// Token: 0x04000440 RID: 1088
		private readonly List<TMP_Text> m_LayoutRebuildQueue = new List<TMP_Text>();

		// Token: 0x04000441 RID: 1089
		private HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x04000442 RID: 1090
		private readonly List<TMP_Text> m_GraphicRebuildQueue = new List<TMP_Text>();

		// Token: 0x04000443 RID: 1091
		private HashSet<int> m_GraphicQueueLookup = new HashSet<int>();

		// Token: 0x04000444 RID: 1092
		private readonly List<TMP_Text> m_InternalUpdateQueue = new List<TMP_Text>();

		// Token: 0x04000445 RID: 1093
		private HashSet<int> m_InternalUpdateLookup = new HashSet<int>();
	}
}
