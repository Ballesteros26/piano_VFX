using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A3 RID: 419
	internal class UIRRepaintUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		public UIRRepaintUpdater()
		{
			base.panelChanged += new Action<BaseVisualElementPanel>(this.OnPanelChanged);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0002BDDD File Offset: 0x00029FDD
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRRepaintUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000BA8 RID: 2984 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		// (remove) Token: 0x06000BA9 RID: 2985 RVA: 0x0002BE0C File Offset: 0x0002A00C
		public event Action<RenderChain> BeforeDrawChain
		{
			add
			{
				bool flag = this.renderChain != null;
				if (flag)
				{
					this.renderChain.BeforeDrawChain += value;
				}
			}
			remove
			{
				bool flag = this.renderChain != null;
				if (flag)
				{
					this.renderChain.BeforeDrawChain -= value;
				}
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002BE34 File Offset: 0x0002A034
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & VersionChangeType.Size) > (VersionChangeType)0;
				bool flag4 = (versionChangeType & VersionChangeType.Overflow) > (VersionChangeType)0;
				bool flag5 = (versionChangeType & VersionChangeType.BorderRadius) > (VersionChangeType)0;
				bool flag6 = (versionChangeType & VersionChangeType.BorderWidth) > (VersionChangeType)0;
				bool flag7 = flag2 || flag3 || flag6;
				if (flag7)
				{
					this.renderChain.UIEOnTransformOrSizeChanged(ve, flag2, flag3 || flag6);
				}
				bool flag8 = flag4 || flag5;
				if (flag8)
				{
					this.renderChain.UIEOnClippingChanged(ve, false);
				}
				bool flag9 = (versionChangeType & VersionChangeType.Opacity) > (VersionChangeType)0;
				if (flag9)
				{
					this.renderChain.UIEOnOpacityChanged(ve);
				}
				bool flag10 = (versionChangeType & VersionChangeType.Repaint) > (VersionChangeType)0;
				if (flag10)
				{
					this.renderChain.UIEOnVisualsChanged(ve, false);
				}
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002BEFC File Offset: 0x0002A0FC
		public override void Update()
		{
			RenderChain renderChain = this.renderChain;
			bool flag = ((renderChain != null) ? renderChain.device : null) == null;
			if (!flag)
			{
				using (UIRRepaintUpdater.s_MarkerDrawChain.Auto())
				{
					this.renderChain.ProcessChanges();
					PanelClearFlags clearFlags = base.panel.clearFlags;
					bool flag2 = clearFlags > PanelClearFlags.None;
					if (flag2)
					{
						GL.Clear((clearFlags & PanelClearFlags.Depth) > PanelClearFlags.None, (clearFlags & PanelClearFlags.Color) > PanelClearFlags.None, Color.clear, 0.99f);
					}
					this.renderChain.Render();
				}
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		internal RenderChain DebugGetRenderChain()
		{
			return this.renderChain;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
		protected virtual RenderChain CreateRenderChain()
		{
			return new RenderChain(base.panel);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002BFD5 File Offset: 0x0002A1D5
		static UIRRepaintUpdater()
		{
			Utility.GraphicsResourcesRecreate += new Action<bool>(UIRRepaintUpdater.OnGraphicsResourcesRecreate);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002C014 File Offset: 0x0002A214
		private static void OnGraphicsResourcesRecreate(bool recreate)
		{
			bool flag = !recreate;
			if (flag)
			{
				UIRenderDevice.PrepareForGfxDeviceRecreate();
			}
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				UIRRepaintUpdater uirrepaintUpdater = keyValuePair.Value.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = ((uirrepaintUpdater != null) ? uirrepaintUpdater.renderChain : null);
				if (recreate)
				{
					if (renderChain != null)
					{
						renderChain.AfterRenderDeviceRelease();
					}
				}
				else if (renderChain != null)
				{
					renderChain.BeforeRenderDeviceRelease();
				}
			}
			bool flag2 = !recreate;
			if (flag2)
			{
				UIRenderDevice.FlushAllPendingDeviceDisposes();
			}
			else
			{
				UIRenderDevice.WrapUpGfxDeviceRecreate();
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		private void OnPanelChanged(BaseVisualElementPanel obj)
		{
			this.DisposeRenderChain();
			bool flag = base.panel != null;
			if (flag)
			{
				this.renderChain = this.CreateRenderChain();
				bool flag2 = base.panel.visualTree != null;
				if (flag2)
				{
					this.renderChain.UIEOnChildAdded(base.panel.visualTree.hierarchy.parent, base.panel.visualTree, (base.panel.visualTree.hierarchy.parent == null) ? 0 : base.panel.visualTree.hierarchy.parent.IndexOf(base.panel.visualTree));
					this.renderChain.UIEOnVisualsChanged(base.panel.visualTree, true);
				}
				base.panel.standardShaderChanged += new Action(this.OnPanelStandardShaderChanged);
				base.panel.standardWorldSpaceShaderChanged += new Action(this.OnPanelStandardWorldSpaceShaderChanged);
				base.panel.hierarchyChanged += this.OnPanelHierarchyChanged;
				this.OnPanelStandardShaderChanged();
				bool flag3 = base.panel.contextType == ContextType.Player;
				if (flag3)
				{
					this.OnPanelStandardWorldSpaceShaderChanged();
				}
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002C1E8 File Offset: 0x0002A3E8
		private void OnPanelHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.renderChain == null || ve.panel == null;
			if (!flag)
			{
				switch (changeType)
				{
				case HierarchyChangeType.Add:
					this.renderChain.UIEOnChildAdded(ve.hierarchy.parent, ve, (ve.hierarchy.parent != null) ? ve.hierarchy.parent.IndexOf(ve) : 0);
					break;
				case HierarchyChangeType.Remove:
					this.renderChain.UIEOnChildRemoving(ve);
					break;
				case HierarchyChangeType.Move:
					this.renderChain.UIEOnChildrenReordered(ve);
					break;
				}
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002C288 File Offset: 0x0002A488
		private void OnPanelStandardShaderChanged()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Shader shader = base.panel.standardShader;
				bool flag2 = shader == null;
				if (flag2)
				{
					shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Debug.Assert(shader != null, "Failed to load UIElements default shader");
					bool flag3 = shader != null;
					if (flag3)
					{
						shader.hideFlags |= HideFlags.DontSaveInEditor;
					}
				}
				this.renderChain.defaultShader = shader;
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002C304 File Offset: 0x0002A504
		private void OnPanelStandardWorldSpaceShaderChanged()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Shader shader = base.panel.standardWorldSpaceShader;
				bool flag2 = shader == null;
				if (flag2)
				{
					shader = Shader.Find(UIRUtility.k_DefaultWorldSpaceShaderName);
					Debug.Assert(shader != null, "Failed to load UIElements default world-space shader");
					bool flag3 = shader != null;
					if (flag3)
					{
						shader.hideFlags |= HideFlags.DontSaveInEditor;
					}
				}
				this.renderChain.defaultWorldSpaceShader = shader;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002C380 File Offset: 0x0002A580
		private void ResetAllElementsDataRecursive(VisualElement ve)
		{
			ve.renderChainData = default(RenderChainVEData);
			int i = ve.hierarchy.childCount - 1;
			while (i >= 0)
			{
				this.ResetAllElementsDataRecursive(ve.hierarchy[i--]);
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		private void DisposeRenderChain()
		{
			bool flag = this.renderChain != null;
			if (flag)
			{
				IPanel panel = this.renderChain.panel;
				this.renderChain.Dispose();
				this.renderChain = null;
				bool flag2 = panel != null;
				if (flag2)
				{
					base.panel.hierarchyChanged -= this.OnPanelHierarchyChanged;
					base.panel.standardShaderChanged -= new Action(this.OnPanelStandardShaderChanged);
					this.ResetAllElementsDataRecursive(panel.visualTree);
				}
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0002C457 File Offset: 0x0002A657
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0002C45F File Offset: 0x0002A65F
		private protected bool disposed { protected get; private set; }

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002C468 File Offset: 0x0002A668
		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.DisposeRenderChain();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000510 RID: 1296
		internal RenderChain renderChain;

		// Token: 0x04000511 RID: 1297
		private static ProfilerMarker s_MarkerDrawChain = new ProfilerMarker("DrawChain");

		// Token: 0x04000512 RID: 1298
		private static readonly string s_Description = "UIRepaint";

		// Token: 0x04000513 RID: 1299
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRRepaintUpdater.s_Description);
	}
}
