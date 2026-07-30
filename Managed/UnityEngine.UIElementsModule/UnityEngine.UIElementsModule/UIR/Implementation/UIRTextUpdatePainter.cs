using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x02000256 RID: 598
	internal class UIRTextUpdatePainter : IStylePainter, IDisposable
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0004D840 File Offset: 0x0004BA40
		public MeshGenerationContext meshGenerationContext { get; }

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004D848 File Offset: 0x0004BA48
		public UIRTextUpdatePainter()
		{
			this.meshGenerationContext = new MeshGenerationContext(this);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0004D860 File Offset: 0x0004BA60
		public void Begin(VisualElement ve, UIRenderDevice device)
		{
			Debug.Assert(ve.renderChainData.usesLegacyText && ve.renderChainData.textEntries.Count > 0);
			this.m_CurrentElement = ve;
			this.m_TextEntryIndex = 0;
			Alloc allocVerts = ve.renderChainData.data.allocVerts;
			NativeSlice<Vertex> nativeSlice = ve.renderChainData.data.allocPage.vertices.cpuData.Slice((int)allocVerts.start, (int)allocVerts.size);
			device.Update(ve.renderChainData.data, ve.renderChainData.data.allocVerts.size, out this.m_MeshDataVerts);
			RenderChainTextEntry renderChainTextEntry = ve.renderChainData.textEntries[0];
			bool flag = ve.renderChainData.textEntries.Count > 1 || renderChainTextEntry.vertexCount != this.m_MeshDataVerts.Length;
			if (flag)
			{
				this.m_MeshDataVerts.CopyFrom(nativeSlice);
			}
			int firstVertex = renderChainTextEntry.firstVertex;
			this.m_XFormClipPages = nativeSlice[firstVertex].xformClipPages;
			this.m_IDsFlags = nativeSlice[firstVertex].idsFlags;
			this.m_OpacityPagesSettingsIndex = nativeSlice[firstVertex].opacityPageSVGSettingIndex;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004D9A0 File Offset: 0x0004BBA0
		public void End()
		{
			Debug.Assert(this.m_TextEntryIndex == this.m_CurrentElement.renderChainData.textEntries.Count);
			this.m_CurrentElement = null;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004D9D0 File Offset: 0x0004BBD0
		public void Dispose()
		{
			bool isCreated = this.m_DudVerts.IsCreated;
			if (isCreated)
			{
				this.m_DudVerts.Dispose();
			}
			bool isCreated2 = this.m_DudIndices.IsCreated;
			if (isCreated2)
			{
				this.m_DudIndices.Dispose();
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000062F3 File Offset: 0x000044F3
		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000062F3 File Offset: 0x000044F3
		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000062F3 File Offset: 0x000044F3
		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004DA14 File Offset: 0x0004BC14
		public VisualElement visualElement
		{
			get
			{
				return this.m_CurrentElement;
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004DA2C File Offset: 0x0004BC2C
		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			bool flag = this.m_DudVerts.Length < vertexCount;
			if (flag)
			{
				bool isCreated = this.m_DudVerts.IsCreated;
				if (isCreated)
				{
					this.m_DudVerts.Dispose();
				}
				this.m_DudVerts = new NativeArray<Vertex>(vertexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			bool flag2 = this.m_DudIndices.Length < indexCount;
			if (flag2)
			{
				bool isCreated2 = this.m_DudIndices.IsCreated;
				if (isCreated2)
				{
					this.m_DudIndices.Dispose();
				}
				this.m_DudIndices = new NativeArray<ushort>(indexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			return new MeshWriteData
			{
				m_Vertices = this.m_DudVerts.Slice(0, vertexCount),
				m_Indices = this.m_DudIndices.Slice(0, indexCount)
			};
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004DAE8 File Offset: 0x0004BCE8
		public void DrawText(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			bool flag = textParams.font == null;
			if (!flag)
			{
				float num = TextNative.ComputeTextScaling(this.m_CurrentElement.worldTransform, pixelsPerPoint);
				TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, num);
				using (NativeArray<TextVertex> vertices = TextNative.GetVertices(textNativeSettings))
				{
					List<RenderChainTextEntry> textEntries = this.m_CurrentElement.renderChainData.textEntries;
					int textEntryIndex = this.m_TextEntryIndex;
					this.m_TextEntryIndex = textEntryIndex + 1;
					RenderChainTextEntry renderChainTextEntry = textEntries[textEntryIndex];
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					MeshBuilder.UpdateText(vertices, offset, this.m_CurrentElement.renderChainData.verticesSpace, this.m_XFormClipPages, this.m_IDsFlags, this.m_OpacityPagesSettingsIndex, this.m_MeshDataVerts.Slice(renderChainTextEntry.firstVertex, renderChainTextEntry.vertexCount));
					renderChainTextEntry.command.state.font = textParams.font.material.mainTexture;
				}
			}
		}

		// Token: 0x0400088D RID: 2189
		private VisualElement m_CurrentElement;

		// Token: 0x0400088E RID: 2190
		private int m_TextEntryIndex;

		// Token: 0x0400088F RID: 2191
		private NativeArray<Vertex> m_DudVerts;

		// Token: 0x04000890 RID: 2192
		private NativeArray<ushort> m_DudIndices;

		// Token: 0x04000891 RID: 2193
		private NativeSlice<Vertex> m_MeshDataVerts;

		// Token: 0x04000892 RID: 2194
		private Color32 m_XFormClipPages;

		// Token: 0x04000893 RID: 2195
		private Color32 m_IDsFlags;

		// Token: 0x04000894 RID: 2196
		private Color32 m_OpacityPagesSettingsIndex;
	}
}
