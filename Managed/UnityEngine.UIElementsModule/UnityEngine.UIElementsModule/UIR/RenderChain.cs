using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.UIElements.UIR.Implementation;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000217 RID: 535
	internal class RenderChain : IDisposable
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003AF94 File Offset: 0x00039194
		internal RenderChainCommand firstCommand
		{
			get
			{
				return this.m_FirstCommand;
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003AFAC File Offset: 0x000391AC
		static RenderChain()
		{
			Utility.RegisterIntermediateRenderers += new Action<Camera>(RenderChain.OnRegisterIntermediateRenderers);
			Utility.RenderNodeExecute += new Action<IntPtr>(RenderChain.OnRenderNodeExecute);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003B050 File Offset: 0x00039250
		public RenderChain(IPanel panel)
		{
			UIRAtlasManager uiratlasManager = new UIRAtlasManager(RenderTextureFormat.ARGB32, FilterMode.Bilinear, 64, 64);
			VectorImageManager vectorImageManager = new VectorImageManager(uiratlasManager);
			this.Constructor(panel, new UIRenderDevice(0U, 0U), uiratlasManager, vectorImageManager);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003B0C0 File Offset: 0x000392C0
		protected RenderChain(IPanel panel, UIRenderDevice device, UIRAtlasManager atlasManager, VectorImageManager vectorImageManager)
		{
			this.Constructor(panel, device, atlasManager, vectorImageManager);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0003B118 File Offset: 0x00039318
		private void Constructor(IPanel panelObj, UIRenderDevice deviceObj, UIRAtlasManager atlasMan, VectorImageManager vectorImageMan)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			this.m_DirtyTracker.heads = new List<VisualElement>(8);
			this.m_DirtyTracker.tails = new List<VisualElement>(8);
			this.m_DirtyTracker.minDepths = new int[4];
			this.m_DirtyTracker.maxDepths = new int[4];
			this.m_DirtyTracker.Reset();
			bool flag = this.m_RenderNodesData.Count < 1;
			if (flag)
			{
				this.m_RenderNodesData.Add(new RenderChain.RenderNodeData
				{
					matPropBlock = new MaterialPropertyBlock()
				});
			}
			this.panel = panelObj;
			this.device = deviceObj;
			this.atlasManager = atlasMan;
			this.vectorImageManager = vectorImageMan;
			this.shaderInfoAllocator.Construct();
			this.painter = new UIRStylePainter(this);
			Font.textureRebuilt += new Action<Font>(this.OnFontReset);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003B208 File Offset: 0x00039408
		private void Destructor()
		{
			bool flag = this.m_StaticIndex >= 0;
			if (flag)
			{
				RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
			}
			this.m_StaticIndex = -1;
			UIRUtility.Destroy(this.m_DefaultMat);
			UIRUtility.Destroy(this.m_DefaultWorldSpaceMat);
			this.m_DefaultMat = (this.m_DefaultWorldSpaceMat = null);
			Font.textureRebuilt -= new Action<Font>(this.OnFontReset);
			UIRStylePainter painter = this.painter;
			if (painter != null)
			{
				painter.Dispose();
			}
			UIRTextUpdatePainter textUpdatePainter = this.m_TextUpdatePainter;
			if (textUpdatePainter != null)
			{
				textUpdatePainter.Dispose();
			}
			UIRAtlasManager atlasManager = this.atlasManager;
			if (atlasManager != null)
			{
				atlasManager.Dispose();
			}
			VectorImageManager vectorImageManager = this.vectorImageManager;
			if (vectorImageManager != null)
			{
				vectorImageManager.Dispose();
			}
			this.shaderInfoAllocator.Dispose();
			UIRenderDevice device = this.device;
			if (device != null)
			{
				device.Dispose();
			}
			this.painter = null;
			this.m_TextUpdatePainter = null;
			this.atlasManager = null;
			this.shaderInfoAllocator = default(UIRVEShaderInfoAllocator);
			this.device = null;
			this.m_ActiveRenderNodes = 0;
			this.m_RenderNodesData.Clear();
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0003B317 File Offset: 0x00039517
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0003B31F File Offset: 0x0003951F
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600102C RID: 4140 RVA: 0x0003B328 File Offset: 0x00039528
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003B33C File Offset: 0x0003953C
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Destructor();
				}
				this.disposed = true;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003B36C File Offset: 0x0003956C
		internal ChainBuilderStats stats
		{
			get
			{
				return this.m_Stats;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003B384 File Offset: 0x00039584
		public void ProcessChanges()
		{
			this.m_Stats = default(ChainBuilderStats);
			this.m_Stats.elementsAdded = this.m_Stats.elementsAdded + this.m_StatsElementsAdded;
			this.m_Stats.elementsRemoved = this.m_Stats.elementsRemoved + this.m_StatsElementsRemoved;
			this.m_StatsElementsAdded = (this.m_StatsElementsRemoved = 0U);
			bool isReleased = this.shaderInfoAllocator.isReleased;
			if (isReleased)
			{
				this.RecreateDevice();
			}
			bool flag = this.m_DrawInCameras && this.m_StaticIndex < 0;
			if (flag)
			{
				this.m_StaticIndex = RenderChain.RenderChainStaticIndexAllocator.AllocateIndex(this);
			}
			else
			{
				bool flag2 = !this.m_DrawInCameras && this.m_StaticIndex >= 0;
				if (flag2)
				{
					RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
					this.m_StaticIndex = -1;
				}
			}
			bool flag3 = RenderChain.OnPreRender != null;
			if (flag3)
			{
				RenderChain.OnPreRender.Invoke();
			}
			bool flag4 = false;
			UIRAtlasManager atlasManager = this.atlasManager;
			bool flag5 = atlasManager != null && atlasManager.RequiresReset();
			if (flag5)
			{
				this.atlasManager.Reset();
				flag4 = true;
			}
			VectorImageManager vectorImageManager = this.vectorImageManager;
			bool flag6 = vectorImageManager != null && vectorImageManager.RequiresReset();
			if (flag6)
			{
				this.vectorImageManager.Reset();
				flag4 = true;
			}
			bool flag7 = flag4;
			if (flag7)
			{
				this.RepaintAtlassedElements();
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			int num = 0;
			RenderDataDirtyTypes renderDataDirtyTypes = RenderDataDirtyTypes.Clipping | RenderDataDirtyTypes.ClippingHierarchy;
			RenderDataDirtyTypes renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int i = this.m_DirtyTracker.minDepths[num]; i <= this.m_DirtyTracker.maxDepths[num]; i++)
			{
				VisualElement visualElement = this.m_DirtyTracker.heads[i];
				while (visualElement != null)
				{
					VisualElement nextDirty = visualElement.renderChainData.nextDirty;
					bool flag8 = (visualElement.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag8)
					{
						bool flag9 = visualElement.renderChainData.isInChain && visualElement.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag9)
						{
							RenderEvents.ProcessOnClippingChanged(this, visualElement, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement, renderDataDirtyTypes2);
					}
					visualElement = nextDirty;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 1;
			renderDataDirtyTypes = RenderDataDirtyTypes.Opacity;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int j = this.m_DirtyTracker.minDepths[num]; j <= this.m_DirtyTracker.maxDepths[num]; j++)
			{
				VisualElement visualElement2 = this.m_DirtyTracker.heads[j];
				while (visualElement2 != null)
				{
					VisualElement nextDirty2 = visualElement2.renderChainData.nextDirty;
					bool flag10 = (visualElement2.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag10)
					{
						bool flag11 = visualElement2.renderChainData.isInChain && visualElement2.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag11)
						{
							RenderEvents.ProcessOnOpacityChanged(this, visualElement2, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement2, renderDataDirtyTypes2);
					}
					visualElement2 = nextDirty2;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 2;
			renderDataDirtyTypes = RenderDataDirtyTypes.Transform | RenderDataDirtyTypes.ClipRectSize;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int k = this.m_DirtyTracker.minDepths[num]; k <= this.m_DirtyTracker.maxDepths[num]; k++)
			{
				VisualElement visualElement3 = this.m_DirtyTracker.heads[k];
				while (visualElement3 != null)
				{
					VisualElement nextDirty3 = visualElement3.renderChainData.nextDirty;
					bool flag12 = (visualElement3.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag12)
					{
						bool flag13 = visualElement3.renderChainData.isInChain && visualElement3.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag13)
						{
							RenderEvents.ProcessOnTransformOrSizeChanged(this, visualElement3, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement3, renderDataDirtyTypes2);
					}
					visualElement3 = nextDirty3;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_BlockDirtyRegistration = true;
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 3;
			renderDataDirtyTypes = RenderDataDirtyTypes.Visuals | RenderDataDirtyTypes.VisualsHierarchy;
			renderDataDirtyTypes2 = ~renderDataDirtyTypes;
			for (int l = this.m_DirtyTracker.minDepths[num]; l <= this.m_DirtyTracker.maxDepths[num]; l++)
			{
				VisualElement visualElement4 = this.m_DirtyTracker.heads[l];
				while (visualElement4 != null)
				{
					VisualElement nextDirty4 = visualElement4.renderChainData.nextDirty;
					bool flag14 = (visualElement4.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag14)
					{
						bool flag15 = visualElement4.renderChainData.isInChain && visualElement4.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag15)
						{
							RenderEvents.ProcessOnVisualsChanged(this, visualElement4, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement4, renderDataDirtyTypes2);
					}
					visualElement4 = nextDirty4;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_BlockDirtyRegistration = false;
			this.m_DirtyTracker.Reset();
			this.ProcessTextRegen(true);
			bool fontWasReset = this.m_FontWasReset;
			if (fontWasReset)
			{
				for (int m = 0; m < 2; m++)
				{
					bool flag16 = !this.m_FontWasReset;
					if (flag16)
					{
						break;
					}
					this.m_FontWasReset = false;
					this.ProcessTextRegen(false);
				}
			}
			UIRAtlasManager atlasManager2 = this.atlasManager;
			if (atlasManager2 != null)
			{
				atlasManager2.Commit();
			}
			VectorImageManager vectorImageManager2 = this.vectorImageManager;
			if (vectorImageManager2 != null)
			{
				vectorImageManager2.Commit();
			}
			this.shaderInfoAllocator.IssuePendingAtlasBlits();
			UIRenderDevice device = this.device;
			if (device != null)
			{
				device.OnFrameRenderingBegin();
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003B9B4 File Offset: 0x00039BB4
		public void Render()
		{
			bool flag = this.BeforeDrawChain != null;
			if (flag)
			{
				this.BeforeDrawChain.Invoke(this);
			}
			Exception ex = null;
			bool flag2 = this.m_FirstCommand != null;
			if (flag2)
			{
				bool flag3 = !this.m_DrawInCameras;
				if (flag3)
				{
					Rect layout = this.panel.visualTree.layout;
					Material standardMaterial = this.GetStandardMaterial();
					if (standardMaterial != null)
					{
						standardMaterial.SetPass(0);
					}
					Matrix4x4 matrix4x = ProjectionUtils.Ortho(layout.xMin, layout.xMax, layout.yMax, layout.yMin, -0.001f, 1.001f);
					GL.LoadProjectionMatrix(matrix4x);
					GL.modelview = Matrix4x4.identity;
					UIRenderDevice device = this.device;
					RenderChainCommand firstCommand = this.m_FirstCommand;
					Material material = standardMaterial;
					Material material2 = standardMaterial;
					UIRAtlasManager atlasManager = this.atlasManager;
					Texture texture = ((atlasManager != null) ? atlasManager.atlas : null);
					VectorImageManager vectorImageManager = this.vectorImageManager;
					device.EvaluateChain(firstCommand, material, material2, texture, (vectorImageManager != null) ? vectorImageManager.atlas : null, this.shaderInfoAllocator.atlas, (this.panel as BaseVisualElementPanel).scaledPixelsPerPoint, this.shaderInfoAllocator.transformConstants, this.shaderInfoAllocator.clipRectConstants, this.m_RenderNodesData[0].matPropBlock, true, ref ex);
				}
			}
			bool flag4 = ex != null;
			if (!flag4)
			{
				bool drawStats = this.drawStats;
				if (drawStats)
				{
					this.DrawStats();
				}
				return;
			}
			bool flag5 = GUIUtility.IsExitGUIException(ex);
			if (flag5)
			{
				throw ex;
			}
			throw new ImmediateModeException(ex);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003BB20 File Offset: 0x00039D20
		private void ProcessTextRegen(bool timeSliced)
		{
			bool flag = (timeSliced && this.m_DirtyTextRemaining == 0) || this.m_TextElementCount == 0;
			if (!flag)
			{
				bool flag2 = this.m_TextUpdatePainter == null;
				if (flag2)
				{
					this.m_TextUpdatePainter = new UIRTextUpdatePainter();
				}
				VisualElement visualElement = this.m_FirstTextElement;
				this.m_DirtyTextStartIndex = (timeSliced ? (this.m_DirtyTextStartIndex % this.m_TextElementCount) : 0);
				for (int i = 0; i < this.m_DirtyTextStartIndex; i++)
				{
					visualElement = visualElement.renderChainData.nextText;
				}
				bool flag3 = visualElement == null;
				if (flag3)
				{
					visualElement = this.m_FirstTextElement;
				}
				int num = (timeSliced ? Math.Min(50, this.m_DirtyTextRemaining) : this.m_TextElementCount);
				for (int j = 0; j < num; j++)
				{
					RenderEvents.ProcessRegenText(this, visualElement, this.m_TextUpdatePainter, this.device, ref this.m_Stats);
					visualElement = visualElement.renderChainData.nextText;
					this.m_DirtyTextStartIndex++;
					bool flag4 = visualElement == null;
					if (flag4)
					{
						visualElement = this.m_FirstTextElement;
						this.m_DirtyTextStartIndex = 0;
					}
				}
				this.m_DirtyTextRemaining = Math.Max(0, this.m_DirtyTextRemaining - num);
				bool flag5 = this.m_DirtyTextRemaining > 0;
				if (flag5)
				{
					BaseVisualElementPanel baseVisualElementPanel = this.panel as BaseVisualElementPanel;
					if (baseVisualElementPanel != null)
					{
						baseVisualElementPanel.OnVersionChanged(this.m_FirstTextElement, VersionChangeType.Transform);
					}
				}
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001032 RID: 4146 RVA: 0x0003BC84 File Offset: 0x00039E84
		// (remove) Token: 0x06001033 RID: 4147 RVA: 0x0003BCBC File Offset: 0x00039EBC
		[field: DebuggerBrowsable(0)]
		public event Action<RenderChain> BeforeDrawChain;

		// Token: 0x06001034 RID: 4148 RVA: 0x0003BCF4 File Offset: 0x00039EF4
		public void UIEOnChildAdded(VisualElement parent, VisualElement ve, int index)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be added to an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			bool flag = parent != null && !parent.renderChainData.isInChain;
			if (!flag)
			{
				uint num = RenderEvents.DepthFirstOnChildAdded(this, parent, ve, index, true);
				Debug.Assert(ve.renderChainData.isInChain);
				Debug.Assert(ve.panel == this.panel);
				this.UIEOnClippingChanged(ve, true);
				this.UIEOnOpacityChanged(ve);
				this.UIEOnVisualsChanged(ve, true);
				this.m_StatsElementsAdded += num;
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003BD8C File Offset: 0x00039F8C
		public void UIEOnChildrenReordered(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be moved under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				RenderEvents.DepthFirstOnChildRemoving(this, ve.hierarchy[i]);
			}
			for (int j = 0; j < childCount; j++)
			{
				RenderEvents.DepthFirstOnChildAdded(this, ve, ve.hierarchy[j], j, false);
			}
			this.UIEOnClippingChanged(ve, true);
			this.UIEOnVisualsChanged(ve, true);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003BE2C File Offset: 0x0003A02C
		public void UIEOnChildRemoving(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be removed from an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			this.m_StatsElementsRemoved += RenderEvents.DepthFirstOnChildRemoving(this, ve);
			Debug.Assert(!ve.renderChainData.isInChain);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0003BE77 File Offset: 0x0003A077
		public void StopTrackingGroupTransformElement(VisualElement ve)
		{
			this.m_LastGroupTransformElementScale.Remove(ve);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0003BE88 File Offset: 0x0003A088
		public void UIEOnClippingChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change clipping state under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Clipping | (hierarchical ? RenderDataDirtyTypes.ClippingHierarchy : RenderDataDirtyTypes.None), 0);
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003BED4 File Offset: 0x0003A0D4
		public void UIEOnOpacityChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change opacity under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Opacity, 1);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003BF18 File Offset: 0x0003A118
		public void UIEOnTransformOrSizeChanged(VisualElement ve, bool transformChanged, bool clipRectSizeChanged)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change size or transform under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				RenderDataDirtyTypes renderDataDirtyTypes = (transformChanged ? RenderDataDirtyTypes.Transform : RenderDataDirtyTypes.None) | (clipRectSizeChanged ? RenderDataDirtyTypes.ClipRectSize : RenderDataDirtyTypes.None);
				this.m_DirtyTracker.RegisterDirty(ve, renderDataDirtyTypes, 2);
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003BF6C File Offset: 0x0003A16C
		public void UIEOnVisualsChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot be marked for dirty repaint under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Visuals | (hierarchical ? RenderDataDirtyTypes.VisualsHierarchy : RenderDataDirtyTypes.None), 3);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003BFB9 File Offset: 0x0003A1B9
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x0003BFC1 File Offset: 0x0003A1C1
		internal IPanel panel { get; private set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003BFCA File Offset: 0x0003A1CA
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x0003BFD2 File Offset: 0x0003A1D2
		internal UIRenderDevice device { get; private set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0003BFDB File Offset: 0x0003A1DB
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0003BFE3 File Offset: 0x0003A1E3
		internal UIRAtlasManager atlasManager { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0003BFEC File Offset: 0x0003A1EC
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x0003BFF4 File Offset: 0x0003A1F4
		internal VectorImageManager vectorImageManager { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003BFFD File Offset: 0x0003A1FD
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0003C005 File Offset: 0x0003A205
		internal UIRStylePainter painter { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0003C00E File Offset: 0x0003A20E
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0003C016 File Offset: 0x0003A216
		internal bool drawStats { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0003C020 File Offset: 0x0003A220
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0003C038 File Offset: 0x0003A238
		internal bool drawInCameras
		{
			get
			{
				return this.m_DrawInCameras;
			}
			set
			{
				bool flag = this.m_DrawInCameras != value;
				if (flag)
				{
					this.m_DrawInCameras = value;
					bool flag2 = this.panel.visualTree != null;
					if (flag2)
					{
						this.UIEOnClippingChanged(this.panel.visualTree, true);
					}
				}
				bool flag3 = this.m_DrawInCameras && this.m_StaticIndex < 0;
				if (flag3)
				{
					this.m_StaticIndex = RenderChain.RenderChainStaticIndexAllocator.AllocateIndex(this);
				}
				else
				{
					bool flag4 = !this.m_DrawInCameras && this.m_StaticIndex >= 0;
					if (flag4)
					{
						RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
						this.m_StaticIndex = -1;
					}
				}
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x0003C0F4 File Offset: 0x0003A2F4
		internal Shader defaultShader
		{
			get
			{
				return this.m_DefaultShader;
			}
			set
			{
				bool flag = this.m_DefaultShader == value;
				if (!flag)
				{
					this.m_DefaultShader = value;
					UIRUtility.Destroy(this.m_DefaultMat);
					this.m_DefaultMat = null;
				}
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0003C130 File Offset: 0x0003A330
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0003C148 File Offset: 0x0003A348
		internal Shader defaultWorldSpaceShader
		{
			get
			{
				return this.m_DefaultWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_DefaultWorldSpaceShader == value;
				if (!flag)
				{
					this.m_DefaultWorldSpaceShader = value;
					UIRUtility.Destroy(this.m_DefaultWorldSpaceMat);
					this.m_DefaultWorldSpaceMat = null;
				}
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003C184 File Offset: 0x0003A384
		internal Material GetStandardMaterial()
		{
			bool flag = this.m_DefaultMat == null && this.m_DefaultShader != null;
			if (flag)
			{
				this.m_DefaultMat = new Material(this.m_DefaultShader);
				this.m_DefaultMat.hideFlags |= HideFlags.DontSaveInEditor;
			}
			return this.m_DefaultMat;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
		internal Material GetStandardWorldSpaceMaterial()
		{
			bool flag = this.m_DefaultWorldSpaceMat == null && this.m_DefaultWorldSpaceShader != null;
			if (flag)
			{
				this.m_DefaultWorldSpaceMat = new Material(this.m_DefaultWorldSpaceShader);
				this.m_DefaultWorldSpaceMat.hideFlags |= HideFlags.DontSaveInEditor;
			}
			return this.m_DefaultWorldSpaceMat;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003C244 File Offset: 0x0003A444
		internal void EnsureFitsDepth(int depth)
		{
			this.m_DirtyTracker.EnsureFits(depth);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003C254 File Offset: 0x0003A454
		internal void ChildWillBeRemoved(VisualElement ve)
		{
			bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
			if (flag)
			{
				this.m_DirtyTracker.ClearDirty(ve, ~ve.renderChainData.dirtiedValues);
			}
			Debug.Assert(ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None);
			Debug.Assert(ve.renderChainData.prevDirty == null);
			Debug.Assert(ve.renderChainData.nextDirty == null);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003C2CC File Offset: 0x0003A4CC
		internal RenderChainCommand AllocCommand()
		{
			RenderChainCommand renderChainCommand = this.m_CommandPool.Get();
			renderChainCommand.Reset();
			return renderChainCommand;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003C2F4 File Offset: 0x0003A4F4
		internal void FreeCommand(RenderChainCommand cmd)
		{
			bool flag = cmd.state.material != null;
			if (flag)
			{
				this.m_CustomMaterialCommands--;
			}
			cmd.Reset();
			this.m_CommandPool.Return(cmd);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003C33C File Offset: 0x0003A53C
		internal void OnRenderCommandAdded(RenderChainCommand command)
		{
			bool flag = command.prev == null;
			if (flag)
			{
				this.m_FirstCommand = command;
			}
			bool flag2 = command.state.material != null;
			if (flag2)
			{
				this.m_CustomMaterialCommands++;
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0003C384 File Offset: 0x0003A584
		internal void OnRenderCommandsRemoved(RenderChainCommand firstCommand, RenderChainCommand lastCommand)
		{
			bool flag = firstCommand.prev == null;
			if (flag)
			{
				this.m_FirstCommand = lastCommand.next;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		internal void AddTextElement(VisualElement ve)
		{
			bool flag = this.m_FirstTextElement != null;
			if (flag)
			{
				this.m_FirstTextElement.renderChainData.prevText = ve;
				ve.renderChainData.nextText = this.m_FirstTextElement;
			}
			this.m_FirstTextElement = ve;
			this.m_TextElementCount++;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003C400 File Offset: 0x0003A600
		internal void RemoveTextElement(VisualElement ve)
		{
			bool flag = ve.renderChainData.prevText != null;
			if (flag)
			{
				ve.renderChainData.prevText.renderChainData.nextText = ve.renderChainData.nextText;
			}
			bool flag2 = ve.renderChainData.nextText != null;
			if (flag2)
			{
				ve.renderChainData.nextText.renderChainData.prevText = ve.renderChainData.prevText;
			}
			bool flag3 = this.m_FirstTextElement == ve;
			if (flag3)
			{
				this.m_FirstTextElement = ve.renderChainData.nextText;
			}
			ve.renderChainData.prevText = (ve.renderChainData.nextText = null);
			this.m_TextElementCount--;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003C4B8 File Offset: 0x0003A6B8
		internal void OnGroupTransformElementChangedTransform(VisualElement ve)
		{
			Vector2 vector;
			bool flag = !this.m_LastGroupTransformElementScale.TryGetValue(ve, ref vector) || ve.worldTransform.m00 != vector.x || ve.worldTransform.m11 != vector.y;
			if (flag)
			{
				this.m_DirtyTextRemaining = this.m_TextElementCount;
				this.m_LastGroupTransformElementScale[ve] = new Vector2(ve.worldTransform.m00, ve.worldTransform.m11);
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003C53C File Offset: 0x0003A73C
		internal void BeforeRenderDeviceRelease()
		{
			Debug.Assert(this.device != null);
			Debug.Assert(this.m_RenderDeviceRestoreInfo.panel == null);
			Debug.Assert(this.m_RenderDeviceRestoreInfo.root == null);
			this.m_RenderDeviceRestoreInfo.panel = this.panel;
			RenderChainCommand firstCommand = this.m_FirstCommand;
			this.m_RenderDeviceRestoreInfo.root = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null);
			this.m_RenderDeviceRestoreInfo.hasAtlasMan = this.atlasManager != null;
			this.m_RenderDeviceRestoreInfo.hasVectorImageMan = this.vectorImageManager != null;
			bool flag = this.m_RenderDeviceRestoreInfo.root != null;
			if (flag)
			{
				this.UIEOnChildRemoving(this.m_RenderDeviceRestoreInfo.root);
			}
			this.Destructor();
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003C608 File Offset: 0x0003A808
		internal void AfterRenderDeviceRelease()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			Debug.Assert(this.device == null);
			IPanel panel = this.m_RenderDeviceRestoreInfo.panel;
			VisualElement root = this.m_RenderDeviceRestoreInfo.root;
			UIRenderDevice uirenderDevice = new UIRenderDevice(0U, 0U);
			UIRAtlasManager uiratlasManager = (this.m_RenderDeviceRestoreInfo.hasAtlasMan ? new UIRAtlasManager(RenderTextureFormat.ARGB32, FilterMode.Bilinear, 64, 64) : null);
			VectorImageManager vectorImageManager = (this.m_RenderDeviceRestoreInfo.hasVectorImageMan ? new VectorImageManager(uiratlasManager) : null);
			this.m_RenderDeviceRestoreInfo = default(RenderChain.RenderDeviceRestoreInfo);
			this.Constructor(panel, uirenderDevice, uiratlasManager, vectorImageManager);
			bool flag = root != null;
			if (flag)
			{
				Debug.Assert(root.panel == panel);
				this.UIEOnChildAdded(root.parent, root, (root.hierarchy.parent == null) ? 0 : root.hierarchy.parent.IndexOf(this.panel.visualTree));
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003C700 File Offset: 0x0003A900
		internal void RecreateDevice()
		{
			this.BeforeRenderDeviceRelease();
			this.AfterRenderDeviceRelease();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003C714 File Offset: 0x0003A914
		private unsafe static RenderChain.RenderNodeData AccessRenderNodeData(IntPtr obj)
		{
			int* ptr = (int*)obj.ToPointer();
			RenderChain renderChain = RenderChain.RenderChainStaticIndexAllocator.AccessIndex(*ptr);
			return renderChain.m_RenderNodesData[ptr[1]];
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003C748 File Offset: 0x0003A948
		private static void OnRenderNodeExecute(IntPtr obj)
		{
			RenderChain.RenderNodeData renderNodeData = RenderChain.AccessRenderNodeData(obj);
			Exception ex = null;
			renderNodeData.device.EvaluateChain(renderNodeData.firstCommand, renderNodeData.initialMaterial, renderNodeData.standardMaterial, renderNodeData.atlas, renderNodeData.vectorAtlas, renderNodeData.shaderInfoAtlas, renderNodeData.dpiScale, renderNodeData.transformConstants, renderNodeData.clipRectConstants, renderNodeData.matPropBlock, false, ref ex);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		private static void OnRegisterIntermediateRenderers(Camera camera)
		{
			int num = 0;
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				Panel value = keyValuePair.Value;
				UIRRepaintUpdater uirrepaintUpdater = value.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = ((uirrepaintUpdater != null) ? uirrepaintUpdater.renderChain : null);
				bool flag = renderChain == null || renderChain.m_StaticIndex < 0 || renderChain.m_FirstCommand == null;
				if (!flag)
				{
					RuntimePanel runtimePanel = (RuntimePanel)value;
					Material standardWorldSpaceMaterial = renderChain.GetStandardWorldSpaceMaterial();
					RenderChain.RenderNodeData renderNodeData = default(RenderChain.RenderNodeData);
					renderNodeData.device = renderChain.device;
					renderNodeData.standardMaterial = standardWorldSpaceMaterial;
					UIRAtlasManager atlasManager = renderChain.atlasManager;
					renderNodeData.atlas = ((atlasManager != null) ? atlasManager.atlas : null);
					VectorImageManager vectorImageManager = renderChain.vectorImageManager;
					renderNodeData.vectorAtlas = ((vectorImageManager != null) ? vectorImageManager.atlas : null);
					renderNodeData.shaderInfoAtlas = renderChain.shaderInfoAllocator.atlas;
					renderNodeData.dpiScale = runtimePanel.scaledPixelsPerPoint;
					renderNodeData.transformConstants = renderChain.shaderInfoAllocator.transformConstants;
					renderNodeData.clipRectConstants = renderChain.shaderInfoAllocator.clipRectConstants;
					bool flag2 = renderChain.m_CustomMaterialCommands == 0;
					if (flag2)
					{
						renderNodeData.initialMaterial = standardWorldSpaceMaterial;
						renderNodeData.firstCommand = renderChain.m_FirstCommand;
						RenderChain.OnRegisterIntermediateRendererMat(runtimePanel, renderChain, ref renderNodeData, camera, num++);
					}
					else
					{
						Material material = null;
						RenderChainCommand renderChainCommand = renderChain.m_FirstCommand;
						RenderChainCommand renderChainCommand2 = renderChainCommand;
						while (renderChainCommand != null)
						{
							bool flag3 = renderChainCommand.type > CommandType.Draw;
							if (flag3)
							{
								renderChainCommand = renderChainCommand.next;
							}
							else
							{
								Material material2 = ((renderChainCommand.state.material == null) ? standardWorldSpaceMaterial : renderChainCommand.state.material);
								bool flag4 = material2 != material;
								if (flag4)
								{
									bool flag5 = material != null;
									if (flag5)
									{
										renderNodeData.initialMaterial = material;
										renderNodeData.firstCommand = renderChainCommand2;
										RenderChain.OnRegisterIntermediateRendererMat(runtimePanel, renderChain, ref renderNodeData, camera, num++);
										renderChainCommand2 = renderChainCommand;
									}
									material = material2;
								}
								renderChainCommand = renderChainCommand.next;
							}
						}
						bool flag6 = renderChainCommand2 != null;
						if (flag6)
						{
							renderNodeData.initialMaterial = material;
							renderNodeData.firstCommand = renderChainCommand2;
							RenderChain.OnRegisterIntermediateRendererMat(runtimePanel, renderChain, ref renderNodeData, camera, num++);
						}
					}
				}
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003C9F4 File Offset: 0x0003ABF4
		private unsafe static void OnRegisterIntermediateRendererMat(RuntimePanel rtp, RenderChain renderChain, ref RenderChain.RenderNodeData rnd, Camera camera, int sameDistanceSortPriority)
		{
			int activeRenderNodes = renderChain.m_ActiveRenderNodes;
			renderChain.m_ActiveRenderNodes = activeRenderNodes + 1;
			int num = activeRenderNodes;
			bool flag = num < renderChain.m_RenderNodesData.Count;
			if (flag)
			{
				RenderChain.RenderNodeData renderNodeData = renderChain.m_RenderNodesData[num];
				rnd.matPropBlock = renderNodeData.matPropBlock;
				renderChain.m_RenderNodesData[num] = rnd;
			}
			else
			{
				rnd.matPropBlock = new MaterialPropertyBlock();
				num = renderChain.m_RenderNodesData.Count;
				renderChain.m_RenderNodesData.Add(rnd);
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = renderChain.m_StaticIndex;
			ptr[1] = num;
			Utility.RegisterIntermediateRenderer(camera, rnd.initialMaterial, rtp.panelToWorld, new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue)), 3, 0, false, sameDistanceSortPriority, (ulong)((long)camera.cullingMask), 2, new IntPtr((void*)ptr), 8);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003CADC File Offset: 0x0003ACDC
		private void RepaintAtlassedElements()
		{
			RenderChainCommand firstCommand = this.m_FirstCommand;
			for (VisualElement visualElement = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); visualElement != null; visualElement = visualElement.renderChainData.next)
			{
				bool usesAtlas = visualElement.renderChainData.usesAtlas;
				if (usesAtlas)
				{
					this.UIEOnVisualsChanged(visualElement, false);
				}
			}
			this.UIEOnOpacityChanged(this.panel.visualTree);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003CB44 File Offset: 0x0003AD44
		private void OnFontReset(Font font)
		{
			this.m_FontWasReset = true;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003CB50 File Offset: 0x0003AD50
		private void DrawStats()
		{
			bool flag = this.device != null;
			float num = 12f;
			Rect rect = new Rect(30f, 60f, 1000f, 100f);
			GUI.Box(new Rect(20f, 40f, 200f, (float)(flag ? 380 : 256)), "UIElements Draw Stats");
			GUI.Label(rect, "Elements added\t: " + this.m_Stats.elementsAdded);
			rect.y += num;
			GUI.Label(rect, "Elements removed\t: " + this.m_Stats.elementsRemoved);
			rect.y += num;
			GUI.Label(rect, "Mesh allocs allocated\t: " + this.m_Stats.newMeshAllocations);
			rect.y += num;
			GUI.Label(rect, "Mesh allocs updated\t: " + this.m_Stats.updatedMeshAllocations);
			rect.y += num;
			GUI.Label(rect, "Clip update roots\t: " + this.m_Stats.recursiveClipUpdates);
			rect.y += num;
			GUI.Label(rect, "Clip update total\t: " + this.m_Stats.recursiveClipUpdatesExpanded);
			rect.y += num;
			GUI.Label(rect, "Opacity update roots\t: " + this.m_Stats.recursiveOpacityUpdates);
			rect.y += num;
			GUI.Label(rect, "Opacity update total\t: " + this.m_Stats.recursiveOpacityUpdatesExpanded);
			rect.y += num;
			GUI.Label(rect, "Xform update roots\t: " + this.m_Stats.recursiveTransformUpdates);
			rect.y += num;
			GUI.Label(rect, "Xform update total\t: " + this.m_Stats.recursiveTransformUpdatesExpanded);
			rect.y += num;
			GUI.Label(rect, "Xformed by bone\t: " + this.m_Stats.boneTransformed);
			rect.y += num;
			GUI.Label(rect, "Xformed by skipping\t: " + this.m_Stats.skipTransformed);
			rect.y += num;
			GUI.Label(rect, "Xformed by nudging\t: " + this.m_Stats.nudgeTransformed);
			rect.y += num;
			GUI.Label(rect, "Xformed by repaint\t: " + this.m_Stats.visualUpdateTransformed);
			rect.y += num;
			GUI.Label(rect, "Visual update roots\t: " + this.m_Stats.recursiveVisualUpdates);
			rect.y += num;
			GUI.Label(rect, "Visual update total\t: " + this.m_Stats.recursiveVisualUpdatesExpanded);
			rect.y += num;
			GUI.Label(rect, "Visual update flats\t: " + this.m_Stats.nonRecursiveVisualUpdates);
			rect.y += num;
			GUI.Label(rect, "Dirty processed\t: " + this.m_Stats.dirtyProcessed);
			rect.y += num;
			GUI.Label(rect, "Group-xform updates\t: " + this.m_Stats.groupTransformElementsChanged);
			rect.y += num;
			GUI.Label(rect, "Text regens\t: " + this.m_Stats.textUpdates);
			rect.y += num;
			bool flag2 = !flag;
			if (!flag2)
			{
				rect.y += num;
				UIRenderDevice.DrawStatistics drawStatistics = this.device.GatherDrawStatistics();
				GUI.Label(rect, "Frame index\t: " + drawStatistics.currentFrameIndex);
				rect.y += num;
				GUI.Label(rect, "Command count\t: " + drawStatistics.commandCount);
				rect.y += num;
				GUI.Label(rect, "Draw commands\t: " + drawStatistics.drawCommandCount);
				rect.y += num;
				GUI.Label(rect, "Draw ranges\t: " + drawStatistics.drawRangeCount);
				rect.y += num;
				GUI.Label(rect, "Draw range calls\t: " + drawStatistics.drawRangeCallCount);
				rect.y += num;
				GUI.Label(rect, "Material sets\t: " + drawStatistics.materialSetCount);
				rect.y += num;
				GUI.Label(rect, "Immediate draws\t: " + drawStatistics.immediateDraws);
				rect.y += num;
				GUI.Label(rect, "Total triangles\t: " + drawStatistics.totalIndices / 3U);
				rect.y += num;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003D11C File Offset: 0x0003B31C
		private static VisualElement GetFirstElementInPanel(VisualElement ve)
		{
			for (;;)
			{
				bool flag;
				if (ve != null)
				{
					VisualElement prev = ve.renderChainData.prev;
					flag = prev != null && prev.renderChainData.isInChain;
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					break;
				}
				ve = ve.renderChainData.prev;
			}
			return ve;
		}

		// Token: 0x040006CF RID: 1743
		private RenderChainCommand m_FirstCommand;

		// Token: 0x040006D0 RID: 1744
		private RenderChain.DepthOrderedDirtyTracking m_DirtyTracker;

		// Token: 0x040006D1 RID: 1745
		private Pool<RenderChainCommand> m_CommandPool = new Pool<RenderChainCommand>();

		// Token: 0x040006D2 RID: 1746
		private List<RenderChain.RenderNodeData> m_RenderNodesData = new List<RenderChain.RenderNodeData>();

		// Token: 0x040006D3 RID: 1747
		private Shader m_DefaultShader;

		// Token: 0x040006D4 RID: 1748
		private Shader m_DefaultWorldSpaceShader;

		// Token: 0x040006D5 RID: 1749
		private Material m_DefaultMat;

		// Token: 0x040006D6 RID: 1750
		private Material m_DefaultWorldSpaceMat;

		// Token: 0x040006D7 RID: 1751
		private bool m_BlockDirtyRegistration;

		// Token: 0x040006D8 RID: 1752
		private bool m_DrawInCameras;

		// Token: 0x040006D9 RID: 1753
		private int m_StaticIndex = -1;

		// Token: 0x040006DA RID: 1754
		private int m_ActiveRenderNodes = 0;

		// Token: 0x040006DB RID: 1755
		private int m_CustomMaterialCommands = 0;

		// Token: 0x040006DC RID: 1756
		private ChainBuilderStats m_Stats;

		// Token: 0x040006DD RID: 1757
		private uint m_StatsElementsAdded;

		// Token: 0x040006DE RID: 1758
		private uint m_StatsElementsRemoved;

		// Token: 0x040006DF RID: 1759
		private VisualElement m_FirstTextElement;

		// Token: 0x040006E0 RID: 1760
		private UIRTextUpdatePainter m_TextUpdatePainter;

		// Token: 0x040006E1 RID: 1761
		private int m_TextElementCount;

		// Token: 0x040006E2 RID: 1762
		private int m_DirtyTextStartIndex;

		// Token: 0x040006E3 RID: 1763
		private int m_DirtyTextRemaining;

		// Token: 0x040006E4 RID: 1764
		private bool m_FontWasReset;

		// Token: 0x040006E5 RID: 1765
		private Dictionary<VisualElement, Vector2> m_LastGroupTransformElementScale = new Dictionary<VisualElement, Vector2>();

		// Token: 0x040006E6 RID: 1766
		private static ProfilerMarker s_MarkerProcess = new ProfilerMarker("RenderChain.Process");

		// Token: 0x040006E7 RID: 1767
		private static ProfilerMarker s_MarkerRender = new ProfilerMarker("RenderChain.Draw");

		// Token: 0x040006E8 RID: 1768
		private static ProfilerMarker s_MarkerClipProcessing = new ProfilerMarker("RenderChain.UpdateClips");

		// Token: 0x040006E9 RID: 1769
		private static ProfilerMarker s_MarkerOpacityProcessing = new ProfilerMarker("RenderChain.UpdateOpacity");

		// Token: 0x040006EA RID: 1770
		private static ProfilerMarker s_MarkerTransformProcessing = new ProfilerMarker("RenderChain.UpdateTransforms");

		// Token: 0x040006EB RID: 1771
		private static ProfilerMarker s_MarkerVisualsProcessing = new ProfilerMarker("RenderChain.UpdateVisuals");

		// Token: 0x040006EC RID: 1772
		private static ProfilerMarker s_MarkerTextRegen = new ProfilerMarker("RenderChain.RegenText");

		// Token: 0x040006EE RID: 1774
		internal static Action OnPreRender = null;

		// Token: 0x040006F4 RID: 1780
		internal UIRVEShaderInfoAllocator shaderInfoAllocator;

		// Token: 0x040006F7 RID: 1783
		private RenderChain.RenderDeviceRestoreInfo m_RenderDeviceRestoreInfo;

		// Token: 0x02000218 RID: 536
		private struct DepthOrderedDirtyTracking
		{
			// Token: 0x06001064 RID: 4196 RVA: 0x0003D164 File Offset: 0x0003B364
			public void EnsureFits(int maxDepth)
			{
				while (this.heads.Count <= maxDepth)
				{
					this.heads.Add(null);
					this.tails.Add(null);
				}
			}

			// Token: 0x06001065 RID: 4197 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
			public void RegisterDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypes, int dirtyTypeClassIndex)
			{
				Debug.Assert(dirtyTypes > RenderDataDirtyTypes.None);
				int hierarchyDepth = ve.renderChainData.hierarchyDepth;
				this.minDepths[dirtyTypeClassIndex] = ((hierarchyDepth < this.minDepths[dirtyTypeClassIndex]) ? hierarchyDepth : this.minDepths[dirtyTypeClassIndex]);
				this.maxDepths[dirtyTypeClassIndex] = ((hierarchyDepth > this.maxDepths[dirtyTypeClassIndex]) ? hierarchyDepth : this.maxDepths[dirtyTypeClassIndex]);
				bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
				if (flag)
				{
					ve.renderChainData.dirtiedValues = ve.renderChainData.dirtiedValues | dirtyTypes;
				}
				else
				{
					ve.renderChainData.dirtiedValues = dirtyTypes;
					bool flag2 = this.tails[hierarchyDepth] != null;
					if (flag2)
					{
						this.tails[hierarchyDepth].renderChainData.nextDirty = ve;
						ve.renderChainData.prevDirty = this.tails[hierarchyDepth];
						this.tails[hierarchyDepth] = ve;
					}
					else
					{
						List<VisualElement> list = this.heads;
						int num = hierarchyDepth;
						this.tails[hierarchyDepth] = ve;
						list[num] = ve;
					}
				}
			}

			// Token: 0x06001066 RID: 4198 RVA: 0x0003D2AC File Offset: 0x0003B4AC
			public void ClearDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypesInverse)
			{
				Debug.Assert(ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None);
				ve.renderChainData.dirtiedValues = ve.renderChainData.dirtiedValues & dirtyTypesInverse;
				bool flag = ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None;
				if (flag)
				{
					bool flag2 = ve.renderChainData.prevDirty != null;
					if (flag2)
					{
						ve.renderChainData.prevDirty.renderChainData.nextDirty = ve.renderChainData.nextDirty;
					}
					bool flag3 = ve.renderChainData.nextDirty != null;
					if (flag3)
					{
						ve.renderChainData.nextDirty.renderChainData.prevDirty = ve.renderChainData.prevDirty;
					}
					bool flag4 = this.tails[ve.renderChainData.hierarchyDepth] == ve;
					if (flag4)
					{
						Debug.Assert(ve.renderChainData.nextDirty == null);
						this.tails[ve.renderChainData.hierarchyDepth] = ve.renderChainData.prevDirty;
					}
					bool flag5 = this.heads[ve.renderChainData.hierarchyDepth] == ve;
					if (flag5)
					{
						Debug.Assert(ve.renderChainData.prevDirty == null);
						this.heads[ve.renderChainData.hierarchyDepth] = ve.renderChainData.nextDirty;
					}
					ve.renderChainData.prevDirty = (ve.renderChainData.nextDirty = null);
				}
			}

			// Token: 0x06001067 RID: 4199 RVA: 0x0003D424 File Offset: 0x0003B624
			public void Reset()
			{
				for (int i = 0; i < this.minDepths.Length; i++)
				{
					this.minDepths[i] = int.MaxValue;
					this.maxDepths[i] = int.MinValue;
				}
			}

			// Token: 0x040006F8 RID: 1784
			public List<VisualElement> heads;

			// Token: 0x040006F9 RID: 1785
			public List<VisualElement> tails;

			// Token: 0x040006FA RID: 1786
			public int[] minDepths;

			// Token: 0x040006FB RID: 1787
			public int[] maxDepths;

			// Token: 0x040006FC RID: 1788
			public uint dirtyID;
		}

		// Token: 0x02000219 RID: 537
		private struct RenderChainStaticIndexAllocator
		{
			// Token: 0x06001068 RID: 4200 RVA: 0x0003D468 File Offset: 0x0003B668
			public static int AllocateIndex(RenderChain renderChain)
			{
				int num = RenderChain.RenderChainStaticIndexAllocator.renderChains.IndexOf(null);
				bool flag = num >= 0;
				if (flag)
				{
					RenderChain.RenderChainStaticIndexAllocator.renderChains[num] = renderChain;
				}
				else
				{
					num = RenderChain.RenderChainStaticIndexAllocator.renderChains.Count;
					RenderChain.RenderChainStaticIndexAllocator.renderChains.Add(renderChain);
				}
				return num;
			}

			// Token: 0x06001069 RID: 4201 RVA: 0x0003D4BA File Offset: 0x0003B6BA
			public static void FreeIndex(int index)
			{
				RenderChain.RenderChainStaticIndexAllocator.renderChains[index] = null;
			}

			// Token: 0x0600106A RID: 4202 RVA: 0x0003D4CC File Offset: 0x0003B6CC
			public static RenderChain AccessIndex(int index)
			{
				return RenderChain.RenderChainStaticIndexAllocator.renderChains[index];
			}

			// Token: 0x040006FD RID: 1789
			private static List<RenderChain> renderChains = new List<RenderChain>(4);
		}

		// Token: 0x0200021A RID: 538
		private struct RenderNodeData
		{
			// Token: 0x040006FE RID: 1790
			public Material standardMaterial;

			// Token: 0x040006FF RID: 1791
			public Material initialMaterial;

			// Token: 0x04000700 RID: 1792
			public MaterialPropertyBlock matPropBlock;

			// Token: 0x04000701 RID: 1793
			public RenderChainCommand firstCommand;

			// Token: 0x04000702 RID: 1794
			public UIRenderDevice device;

			// Token: 0x04000703 RID: 1795
			public Texture atlas;

			// Token: 0x04000704 RID: 1796
			public Texture vectorAtlas;

			// Token: 0x04000705 RID: 1797
			public Texture shaderInfoAtlas;

			// Token: 0x04000706 RID: 1798
			public float dpiScale;

			// Token: 0x04000707 RID: 1799
			public NativeSlice<Transform3x4> transformConstants;

			// Token: 0x04000708 RID: 1800
			public NativeSlice<Vector4> clipRectConstants;
		}

		// Token: 0x0200021B RID: 539
		private struct RenderDeviceRestoreInfo
		{
			// Token: 0x04000709 RID: 1801
			public IPanel panel;

			// Token: 0x0400070A RID: 1802
			public VisualElement root;

			// Token: 0x0400070B RID: 1803
			public bool hasAtlasMan;

			// Token: 0x0400070C RID: 1804
			public bool hasVectorImageMan;
		}
	}
}
