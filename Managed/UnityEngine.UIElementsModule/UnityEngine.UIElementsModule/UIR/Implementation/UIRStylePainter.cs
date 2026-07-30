using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x02000252 RID: 594
	internal class UIRStylePainter : IStylePainter, IDisposable
	{
		// Token: 0x06001188 RID: 4488 RVA: 0x0004BC70 File Offset: 0x00049E70
		private MeshWriteData GetPooledMeshWriteData()
		{
			bool flag = this.m_NextMeshWriteDataPoolItem == this.m_MeshWriteDataPool.Count;
			if (flag)
			{
				this.m_MeshWriteDataPool.Add(new MeshWriteData());
			}
			List<MeshWriteData> meshWriteDataPool = this.m_MeshWriteDataPool;
			int nextMeshWriteDataPoolItem = this.m_NextMeshWriteDataPoolItem;
			this.m_NextMeshWriteDataPoolItem = nextMeshWriteDataPoolItem + 1;
			return meshWriteDataPool[nextMeshWriteDataPoolItem];
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0004BCC8 File Offset: 0x00049EC8
		private MeshWriteData AllocRawVertsIndices(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			this.m_CurrentEntry.vertices = this.m_VertsPool.Alloc(vertexCount);
			this.m_CurrentEntry.indices = this.m_IndicesPool.Alloc(indexCount);
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices);
			return pooledMeshWriteData;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004BD30 File Offset: 0x00049F30
		private MeshWriteData AllocThroughDrawMesh(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			return this.DrawMesh((int)vertexCount, (int)indexCount, allocatorData.texture, allocatorData.material, allocatorData.flags);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004BD5C File Offset: 0x00049F5C
		public UIRStylePainter(RenderChain renderChain)
		{
			this.m_Owner = renderChain;
			this.meshGenerationContext = new MeshGenerationContext(this);
			this.device = renderChain.device;
			this.m_AtlasManager = renderChain.atlasManager;
			this.m_VectorImageManager = renderChain.vectorImageManager;
			this.m_AllocRawVertsIndicesDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocRawVertsIndices);
			this.m_AllocThroughDrawMeshDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocThroughDrawMesh);
			int num = 32;
			this.m_MeshWriteDataPool = new List<MeshWriteData>(num);
			for (int i = 0; i < num; i++)
			{
				this.m_MeshWriteDataPool.Add(new MeshWriteData());
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0004BE40 File Offset: 0x0004A040
		public MeshGenerationContext meshGenerationContext { get; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0004BE48 File Offset: 0x0004A048
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x0004BE50 File Offset: 0x0004A050
		public VisualElement currentElement { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0004BE59 File Offset: 0x0004A059
		public UIRenderDevice device { get; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004BE64 File Offset: 0x0004A064
		public List<UIRStylePainter.Entry> entries
		{
			get
			{
				return this.m_Entries;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004BE7C File Offset: 0x0004A07C
		public UIRStylePainter.ClosingInfo closingInfo
		{
			get
			{
				return this.m_ClosingInfo;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0004BE94 File Offset: 0x0004A094
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x0004BE9C File Offset: 0x0004A09C
		public int totalVertices { get; private set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0004BEA5 File Offset: 0x0004A0A5
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x0004BEAD File Offset: 0x0004A0AD
		public int totalIndices { get; private set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004BEB6 File Offset: 0x0004A0B6
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x0004BEBE File Offset: 0x0004A0BE
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001198 RID: 4504 RVA: 0x0004BEC7 File Offset: 0x0004A0C7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004BEDC File Offset: 0x0004A0DC
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_IndicesPool.Dispose();
					this.m_VertsPool.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004BF20 File Offset: 0x0004A120
		public void Begin()
		{
			this.m_NextMeshWriteDataPoolItem = 0;
			this.m_SVGBackgroundEntryIndex = -1;
			this.currentElement.renderChainData.usesLegacyText = (this.currentElement.renderChainData.usesAtlas = (this.currentElement.renderChainData.disableNudging = false));
			this.currentElement.renderChainData.displacementUVStart = (this.currentElement.renderChainData.displacementUVEnd = 0);
			bool flag = (this.currentElement.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			bool flag2 = flag;
			if (flag2)
			{
				RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
				renderChainCommand.owner = this.currentElement;
				renderChainCommand.type = CommandType.PushView;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.popViewMatrix = true);
			}
			bool flag3 = this.currentElement.hierarchy.parent != null;
			if (flag3)
			{
				this.m_StencilClip = this.currentElement.hierarchy.parent.renderChainData.isStencilClipped;
				this.m_ClipRectID = (flag ? UIRVEShaderInfoAllocator.infiniteClipRect : this.currentElement.hierarchy.parent.renderChainData.clipRectID);
			}
			else
			{
				this.m_StencilClip = false;
				this.m_ClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004C091 File Offset: 0x0004A291
		public void LandClipUnregisterMeshDrawCommand(RenderChainCommand cmd)
		{
			Debug.Assert(this.m_ClosingInfo.needsClosing);
			this.m_ClosingInfo.clipUnregisterDrawCommand = cmd;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0004C0B1 File Offset: 0x0004A2B1
		public void LandClipRegisterMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, int indexOffset)
		{
			Debug.Assert(this.m_ClosingInfo.needsClosing);
			this.m_ClosingInfo.clipperRegisterVertices = vertices;
			this.m_ClosingInfo.clipperRegisterIndices = indices;
			this.m_ClosingInfo.clipperRegisterIndexOffset = indexOffset;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004C0EC File Offset: 0x0004A2EC
		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			bool flag = vertexCount == 0 || indexCount == 0;
			MeshWriteData meshWriteData;
			if (flag)
			{
				pooledMeshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
				meshWriteData = pooledMeshWriteData;
			}
			else
			{
				this.m_CurrentEntry = new UIRStylePainter.Entry
				{
					vertices = this.m_VertsPool.Alloc((uint)vertexCount),
					indices = this.m_IndicesPool.Alloc((uint)indexCount),
					material = material,
					uvIsDisplacement = (flags == MeshGenerationContext.MeshFlags.UVisDisplacement),
					clipRectID = this.m_ClipRectID,
					isStencilClipped = this.m_StencilClip,
					addFlags = VertexFlags.IsSolid
				};
				Debug.Assert(this.m_CurrentEntry.vertices.Length == vertexCount);
				Debug.Assert(this.m_CurrentEntry.indices.Length == indexCount);
				Rect rect = new Rect(0f, 0f, 1f, 1f);
				bool flag2 = flags == MeshGenerationContext.MeshFlags.IsSVGGradients;
				bool flag3 = flags == MeshGenerationContext.MeshFlags.IsCustomSVGGradients;
				bool flag4 = flag2 || flag3;
				if (flag4)
				{
					this.m_CurrentEntry.addFlags = (flag2 ? VertexFlags.IsSVGGradients : VertexFlags.IsCustomSVGGradients);
					bool flag5 = flag3;
					if (flag5)
					{
						this.m_CurrentEntry.custom = texture;
					}
					this.currentElement.renderChainData.usesAtlas = true;
				}
				else
				{
					bool flag6 = texture != null;
					if (flag6)
					{
						RectInt rectInt;
						bool flag7 = this.m_AtlasManager != null && this.m_AtlasManager.TryGetLocation(texture as Texture2D, out rectInt);
						if (flag7)
						{
							this.m_CurrentEntry.addFlags = ((texture.filterMode == FilterMode.Point) ? VertexFlags.IsAtlasTexturedPoint : VertexFlags.IsAtlasTexturedBilinear);
							this.currentElement.renderChainData.usesAtlas = true;
							rect = new Rect((float)rectInt.x, (float)rectInt.y, (float)rectInt.width, (float)rectInt.height);
						}
						else
						{
							this.m_CurrentEntry.addFlags = VertexFlags.IsCustomTextured;
							this.m_CurrentEntry.custom = texture;
						}
					}
				}
				pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices, rect);
				this.m_Entries.Add(this.m_CurrentEntry);
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
				meshWriteData = pooledMeshWriteData;
			}
			return meshWriteData;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004C364 File Offset: 0x0004A564
		public void DrawText(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			bool flag = textParams.font == null;
			if (!flag)
			{
				bool useLegacy = handle.useLegacy;
				if (useLegacy)
				{
					this.DrawTextNative(textParams, handle, pixelsPerPoint);
				}
				else
				{
					this.DrawTextCore(textParams, handle, pixelsPerPoint);
				}
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004C3A4 File Offset: 0x0004A5A4
		private void DrawTextNative(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			float num = TextHandle.ComputeTextScaling(this.currentElement.worldTransform, pixelsPerPoint);
			TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, num);
			using (NativeArray<TextVertex> vertices = TextNative.GetVertices(textNativeSettings))
			{
				bool flag = vertices.Length == 0;
				if (!flag)
				{
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					this.m_CurrentEntry.isTextEntry = true;
					this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
					this.m_CurrentEntry.isStencilClipped = this.m_StencilClip;
					MeshBuilder.MakeText(vertices, offset, new MeshBuilder.AllocMeshData
					{
						alloc = this.m_AllocRawVertsIndicesDelegate
					});
					this.m_CurrentEntry.font = textParams.font.material.mainTexture;
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_CurrentEntry = default(UIRStylePainter.Entry);
					this.currentElement.renderChainData.usesLegacyText = true;
					this.currentElement.renderChainData.disableNudging = true;
				}
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004C508 File Offset: 0x0004A708
		private void DrawTextCore(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			TextInfo textInfo = handle.Update(textParams, pixelsPerPoint);
			for (int i = 0; i < textInfo.materialCount; i++)
			{
				bool flag = textInfo.meshInfo[i].vertexCount == 0;
				if (flag)
				{
					break;
				}
				this.m_CurrentEntry.isTextEntry = true;
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.isStencilClipped = this.m_StencilClip;
				MeshBuilder.MakeText(textInfo.meshInfo[i], textParams.rect.min, new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocRawVertsIndicesDelegate
				});
				this.m_CurrentEntry.font = textInfo.meshInfo[i].material.mainTexture;
				this.m_Entries.Add(this.m_CurrentEntry);
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004C63C File Offset: 0x0004A83C
		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			MeshBuilder.AllocMeshData allocMeshData = new MeshBuilder.AllocMeshData
			{
				alloc = this.m_AllocThroughDrawMeshDelegate,
				texture = rectParams.texture,
				material = rectParams.material
			};
			bool flag = rectParams.vectorImage != null;
			if (flag)
			{
				this.DrawVectorImage(rectParams);
			}
			else
			{
				bool flag2 = rectParams.texture != null;
				if (flag2)
				{
					MeshBuilder.MakeTexturedRect(rectParams, 0f, allocMeshData);
				}
				else
				{
					MeshBuilder.MakeSolidRect(rectParams, 0f, allocMeshData);
				}
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004C6C4 File Offset: 0x0004A8C4
		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
			MeshBuilder.MakeBorder(borderParams, 0f, new MeshBuilder.AllocMeshData
			{
				alloc = this.m_AllocThroughDrawMeshDelegate,
				material = borderParams.material,
				texture = null,
				flags = MeshGenerationContext.MeshFlags.UVisDisplacement
			});
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004C714 File Offset: 0x0004A914
		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
			RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
			renderChainCommand.type = (cullingEnabled ? CommandType.ImmediateCull : CommandType.Immediate);
			renderChainCommand.owner = this.currentElement;
			renderChainCommand.callback = callback;
			this.m_Entries.Add(new UIRStylePainter.Entry
			{
				customCommand = renderChainCommand
			});
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004C76C File Offset: 0x0004A96C
		public VisualElement visualElement
		{
			get
			{
				return this.currentElement;
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004C784 File Offset: 0x0004A984
		public void DrawVisualElementBackground()
		{
			bool flag = this.currentElement.layout.width <= Mathf.Epsilon || this.currentElement.layout.height <= Mathf.Epsilon;
			if (!flag)
			{
				ComputedStyle computedStyle = this.currentElement.computedStyle;
				bool flag2 = computedStyle.backgroundColor != Color.clear;
				if (flag2)
				{
					MeshGenerationContextUtils.RectangleParams rectangleParams = new MeshGenerationContextUtils.RectangleParams
					{
						rect = GUIUtility.AlignRectToDevice(this.currentElement.rect),
						color = computedStyle.backgroundColor.value,
						playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
					};
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out rectangleParams.topLeftRadius, out rectangleParams.bottomLeftRadius, out rectangleParams.topRightRadius, out rectangleParams.bottomRightRadius);
					this.DrawRectangle(rectangleParams);
				}
				Background value = computedStyle.backgroundImage.value;
				bool flag3 = value.texture != null || value.vectorImage != null;
				if (flag3)
				{
					MeshGenerationContextUtils.RectangleParams rectangleParams2 = default(MeshGenerationContextUtils.RectangleParams);
					bool flag4 = value.texture != null;
					if (flag4)
					{
						rectangleParams2 = MeshGenerationContextUtils.RectangleParams.MakeTextured(GUIUtility.AlignRectToDevice(this.currentElement.rect), new Rect(0f, 0f, 1f, 1f), value.texture, computedStyle.unityBackgroundScaleMode.value, this.currentElement.panel.contextType);
					}
					else
					{
						bool flag5 = value.vectorImage != null;
						if (flag5)
						{
							rectangleParams2 = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(GUIUtility.AlignRectToDevice(this.currentElement.rect), new Rect(0f, 0f, 1f, 1f), value.vectorImage, computedStyle.unityBackgroundScaleMode.value, this.currentElement.panel.contextType);
						}
					}
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out rectangleParams2.topLeftRadius, out rectangleParams2.bottomLeftRadius, out rectangleParams2.topRightRadius, out rectangleParams2.bottomRightRadius);
					rectangleParams2.leftSlice = computedStyle.unitySliceLeft.value;
					rectangleParams2.topSlice = computedStyle.unitySliceTop.value;
					rectangleParams2.rightSlice = computedStyle.unitySliceRight.value;
					rectangleParams2.bottomSlice = computedStyle.unitySliceBottom.value;
					bool flag6 = computedStyle.unityBackgroundImageTintColor != Color.clear;
					if (flag6)
					{
						rectangleParams2.color = computedStyle.unityBackgroundImageTintColor.value;
					}
					this.DrawRectangle(rectangleParams2);
				}
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004CA5C File Offset: 0x0004AC5C
		public void DrawVisualElementBorder()
		{
			bool flag = this.currentElement.layout.width >= Mathf.Epsilon && this.currentElement.layout.height >= Mathf.Epsilon;
			if (flag)
			{
				ComputedStyle computedStyle = this.currentElement.computedStyle;
				bool flag2 = (computedStyle.borderLeftColor != Color.clear && computedStyle.borderLeftWidth.value > 0f) || (computedStyle.borderTopColor != Color.clear && computedStyle.borderTopWidth.value > 0f) || (computedStyle.borderRightColor != Color.clear && computedStyle.borderRightWidth.value > 0f) || (computedStyle.borderBottomColor != Color.clear && computedStyle.borderBottomWidth.value > 0f);
				if (flag2)
				{
					MeshGenerationContextUtils.BorderParams borderParams = new MeshGenerationContextUtils.BorderParams
					{
						rect = GUIUtility.AlignRectToDevice(this.currentElement.rect),
						leftColor = computedStyle.borderLeftColor.value,
						topColor = computedStyle.borderTopColor.value,
						rightColor = computedStyle.borderRightColor.value,
						bottomColor = computedStyle.borderBottomColor.value,
						leftWidth = computedStyle.borderLeftWidth.value,
						topWidth = computedStyle.borderTopWidth.value,
						rightWidth = computedStyle.borderRightWidth.value,
						bottomWidth = computedStyle.borderBottomWidth.value,
						playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
					};
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out borderParams.topLeftRadius, out borderParams.bottomLeftRadius, out borderParams.topRightRadius, out borderParams.bottomRightRadius);
					this.DrawBorder(borderParams);
				}
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004CC94 File Offset: 0x0004AE94
		public void ApplyVisualElementClipping()
		{
			bool flag = this.currentElement.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
				renderChainCommand.type = CommandType.PushScissor;
				renderChainCommand.owner = this.currentElement;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.popScissorClip = true);
			}
			else
			{
				bool flag2 = this.currentElement.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					bool flag3 = UIRUtility.IsVectorImageBackground(this.currentElement);
					if (flag3)
					{
						this.GenerateStencilClipEntryForSVGBackground();
					}
					else
					{
						this.GenerateStencilClipEntryForRoundedRectBackground();
					}
				}
			}
			this.m_ClipRectID = this.currentElement.renderChainData.clipRectID;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004CD68 File Offset: 0x0004AF68
		public void DrawVectorImage(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			VectorImage vectorImage = rectParams.vectorImage;
			Debug.Assert(vectorImage != null);
			VertexFlags vertexFlags = ((vectorImage.atlas != null) ? VertexFlags.IsSVGGradients : VertexFlags.IsSolid);
			int num = 0;
			bool flag = vectorImage.atlas != null && this.m_VectorImageManager != null;
			if (flag)
			{
				GradientRemap gradientRemap = this.m_VectorImageManager.AddUser(vectorImage);
				vertexFlags = (gradientRemap.isAtlassed ? VertexFlags.IsSVGGradients : VertexFlags.IsCustomSVGGradients);
				num = gradientRemap.destIndex;
			}
			int count = this.m_Entries.Count;
			MeshGenerationContext.MeshFlags meshFlags = MeshGenerationContext.MeshFlags.None;
			bool flag2 = vertexFlags == VertexFlags.IsSVGGradients;
			if (flag2)
			{
				meshFlags = MeshGenerationContext.MeshFlags.IsSVGGradients;
			}
			else
			{
				bool flag3 = vertexFlags == VertexFlags.IsCustomSVGGradients;
				if (flag3)
				{
					meshFlags = MeshGenerationContext.MeshFlags.IsCustomSVGGradients;
				}
			}
			MeshBuilder.AllocMeshData allocMeshData = new MeshBuilder.AllocMeshData
			{
				alloc = this.m_AllocThroughDrawMeshDelegate,
				texture = ((vertexFlags == VertexFlags.IsCustomSVGGradients) ? vectorImage.atlas : null),
				flags = meshFlags
			};
			int num2;
			int num3;
			MeshBuilder.MakeVectorGraphics(rectParams, num, allocMeshData, out num2, out num3);
			Debug.Assert(count <= this.m_Entries.Count + 1);
			bool flag4 = count != this.m_Entries.Count;
			if (flag4)
			{
				this.m_SVGBackgroundEntryIndex = this.m_Entries.Count - 1;
				bool flag5 = num2 != 0 && num3 != 0;
				if (flag5)
				{
					UIRStylePainter.Entry entry = this.m_Entries[this.m_SVGBackgroundEntryIndex];
					entry.vertices = entry.vertices.Slice(0, num2);
					entry.indices = entry.indices.Slice(0, num3);
					this.m_Entries[this.m_SVGBackgroundEntryIndex] = entry;
				}
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004CF04 File Offset: 0x0004B104
		internal void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.ValidateMeshWriteData();
				this.m_Entries.Clear();
				this.m_VertsPool.SessionDone();
				this.m_IndicesPool.SessionDone();
				this.m_ClosingInfo = default(UIRStylePainter.ClosingInfo);
				this.m_NextMeshWriteDataPoolItem = 0;
				this.currentElement = null;
				this.totalVertices = (this.totalIndices = 0);
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0004CF80 File Offset: 0x0004B180
		private void ValidateMeshWriteData()
		{
			for (int i = 0; i < this.m_NextMeshWriteDataPoolItem; i++)
			{
				MeshWriteData meshWriteData = this.m_MeshWriteDataPool[i];
				bool flag = meshWriteData.vertexCount > 0 && meshWriteData.currentVertex < meshWriteData.vertexCount;
				if (flag)
				{
					Debug.LogError(string.Concat(new object[] { "Not enough vertices written in generateVisualContent callback (asked for ", meshWriteData.vertexCount, " but only wrote ", meshWriteData.currentVertex, ")" }));
					Vertex vertex = meshWriteData.m_Vertices[0];
					while (meshWriteData.currentVertex < meshWriteData.vertexCount)
					{
						meshWriteData.SetNextVertex(vertex);
					}
				}
				bool flag2 = meshWriteData.indexCount > 0 && meshWriteData.currentIndex < meshWriteData.indexCount;
				if (flag2)
				{
					Debug.LogError(string.Concat(new object[] { "Not enough indices written in generateVisualContent callback (asked for ", meshWriteData.indexCount, " but only wrote ", meshWriteData.currentIndex, ")" }));
					while (meshWriteData.currentIndex < meshWriteData.indexCount)
					{
						meshWriteData.SetNextIndex(0);
					}
				}
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004D0D0 File Offset: 0x0004B2D0
		private void GenerateStencilClipEntryForRoundedRectBackground()
		{
			bool flag = this.currentElement.layout.width <= Mathf.Epsilon || this.currentElement.layout.height <= Mathf.Epsilon;
			if (!flag)
			{
				ComputedStyle computedStyle = this.currentElement.computedStyle;
				Vector2 vector;
				Vector2 vector2;
				Vector2 vector3;
				Vector2 vector4;
				MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out vector, out vector2, out vector3, out vector4);
				float value = computedStyle.borderTopWidth.value;
				float value2 = computedStyle.borderLeftWidth.value;
				float value3 = computedStyle.borderBottomWidth.value;
				float value4 = computedStyle.borderRightWidth.value;
				MeshGenerationContextUtils.RectangleParams rectangleParams = new MeshGenerationContextUtils.RectangleParams
				{
					rect = GUIUtility.AlignRectToDevice(this.currentElement.rect),
					color = Color.white,
					topLeftRadius = Vector2.Max(Vector2.zero, vector - new Vector2(value2, value)),
					topRightRadius = Vector2.Max(Vector2.zero, vector3 - new Vector2(value4, value)),
					bottomLeftRadius = Vector2.Max(Vector2.zero, vector2 - new Vector2(value2, value3)),
					bottomRightRadius = Vector2.Max(Vector2.zero, vector4 - new Vector2(value4, value3)),
					playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
				};
				rectangleParams.rect.x = rectangleParams.rect.x + value2;
				rectangleParams.rect.y = rectangleParams.rect.y + value;
				rectangleParams.rect.width = rectangleParams.rect.width - (value2 + value4);
				rectangleParams.rect.height = rectangleParams.rect.height - (value + value3);
				bool flag2 = computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
				if (flag2)
				{
					rectangleParams.rect.x = rectangleParams.rect.x + computedStyle.paddingLeft.value.value;
					rectangleParams.rect.y = rectangleParams.rect.y + computedStyle.paddingTop.value.value;
					rectangleParams.rect.width = rectangleParams.rect.width - (computedStyle.paddingLeft.value.value + computedStyle.paddingRight.value.value);
					rectangleParams.rect.height = rectangleParams.rect.height - (computedStyle.paddingTop.value.value + computedStyle.paddingBottom.value.value);
				}
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.isStencilClipped = this.m_StencilClip;
				this.m_CurrentEntry.isClipRegisterEntry = true;
				MeshBuilder.MakeSolidRect(rectangleParams, 1f, new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocRawVertsIndicesDelegate
				});
				bool flag3 = this.m_CurrentEntry.vertices.Length > 0 && this.m_CurrentEntry.indices.Length > 0;
				if (flag3)
				{
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_StencilClip = true;
					this.m_ClosingInfo.needsClosing = true;
				}
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004D4AC File Offset: 0x0004B6AC
		private void GenerateStencilClipEntryForSVGBackground()
		{
			bool flag = this.m_SVGBackgroundEntryIndex == -1;
			if (!flag)
			{
				UIRStylePainter.Entry entry = this.m_Entries[this.m_SVGBackgroundEntryIndex];
				Debug.Assert(entry.vertices.Length > 0);
				Debug.Assert(entry.indices.Length > 0);
				this.m_StencilClip = true;
				this.m_CurrentEntry.vertices = entry.vertices;
				this.m_CurrentEntry.indices = entry.indices;
				this.m_CurrentEntry.uvIsDisplacement = entry.uvIsDisplacement;
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.isStencilClipped = this.m_StencilClip;
				this.m_CurrentEntry.isClipRegisterEntry = true;
				this.m_ClosingInfo.needsClosing = true;
				int length = this.m_CurrentEntry.vertices.Length;
				NativeSlice<Vertex> nativeSlice = this.m_VertsPool.Alloc((uint)length);
				for (int i = 0; i < length; i++)
				{
					Vertex vertex = this.m_CurrentEntry.vertices[i];
					vertex.position.z = 1f;
					nativeSlice[i] = vertex;
				}
				this.m_CurrentEntry.vertices = nativeSlice;
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_Entries.Add(this.m_CurrentEntry);
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x04000861 RID: 2145
		private RenderChain m_Owner;

		// Token: 0x04000862 RID: 2146
		private List<UIRStylePainter.Entry> m_Entries = new List<UIRStylePainter.Entry>();

		// Token: 0x04000863 RID: 2147
		private UIRAtlasManager m_AtlasManager;

		// Token: 0x04000864 RID: 2148
		private VectorImageManager m_VectorImageManager;

		// Token: 0x04000865 RID: 2149
		private UIRStylePainter.Entry m_CurrentEntry;

		// Token: 0x04000866 RID: 2150
		private UIRStylePainter.ClosingInfo m_ClosingInfo;

		// Token: 0x04000867 RID: 2151
		private bool m_StencilClip = false;

		// Token: 0x04000868 RID: 2152
		private BMPAlloc m_ClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;

		// Token: 0x04000869 RID: 2153
		private int m_SVGBackgroundEntryIndex = -1;

		// Token: 0x0400086A RID: 2154
		private UIRStylePainter.TempDataAlloc<Vertex> m_VertsPool = new UIRStylePainter.TempDataAlloc<Vertex>(8192);

		// Token: 0x0400086B RID: 2155
		private UIRStylePainter.TempDataAlloc<ushort> m_IndicesPool = new UIRStylePainter.TempDataAlloc<ushort>(16384);

		// Token: 0x0400086C RID: 2156
		private List<MeshWriteData> m_MeshWriteDataPool;

		// Token: 0x0400086D RID: 2157
		private int m_NextMeshWriteDataPoolItem;

		// Token: 0x0400086E RID: 2158
		private MeshBuilder.AllocMeshData.Allocator m_AllocRawVertsIndicesDelegate;

		// Token: 0x0400086F RID: 2159
		private MeshBuilder.AllocMeshData.Allocator m_AllocThroughDrawMeshDelegate;

		// Token: 0x02000253 RID: 595
		internal struct Entry
		{
			// Token: 0x04000876 RID: 2166
			public NativeSlice<Vertex> vertices;

			// Token: 0x04000877 RID: 2167
			public NativeSlice<ushort> indices;

			// Token: 0x04000878 RID: 2168
			public Material material;

			// Token: 0x04000879 RID: 2169
			public Texture custom;

			// Token: 0x0400087A RID: 2170
			public Texture font;

			// Token: 0x0400087B RID: 2171
			public RenderChainCommand customCommand;

			// Token: 0x0400087C RID: 2172
			public BMPAlloc clipRectID;

			// Token: 0x0400087D RID: 2173
			public VertexFlags addFlags;

			// Token: 0x0400087E RID: 2174
			public bool uvIsDisplacement;

			// Token: 0x0400087F RID: 2175
			public bool isTextEntry;

			// Token: 0x04000880 RID: 2176
			public bool isClipRegisterEntry;

			// Token: 0x04000881 RID: 2177
			public bool isStencilClipped;
		}

		// Token: 0x02000254 RID: 596
		internal struct ClosingInfo
		{
			// Token: 0x04000882 RID: 2178
			public bool needsClosing;

			// Token: 0x04000883 RID: 2179
			public bool popViewMatrix;

			// Token: 0x04000884 RID: 2180
			public bool popScissorClip;

			// Token: 0x04000885 RID: 2181
			public RenderChainCommand clipUnregisterDrawCommand;

			// Token: 0x04000886 RID: 2182
			public NativeSlice<Vertex> clipperRegisterVertices;

			// Token: 0x04000887 RID: 2183
			public NativeSlice<ushort> clipperRegisterIndices;

			// Token: 0x04000888 RID: 2184
			public int clipperRegisterIndexOffset;
		}

		// Token: 0x02000255 RID: 597
		internal struct TempDataAlloc<T> : IDisposable where T : struct
		{
			// Token: 0x060011AD RID: 4525 RVA: 0x0004D649 File Offset: 0x0004B849
			public TempDataAlloc(int maxPoolElems)
			{
				this.maxPoolElemCount = maxPoolElems;
				this.pool = default(NativeArray<T>);
				this.excess = new List<NativeArray<T>>();
				this.takenFromPool = 0U;
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x0004D674 File Offset: 0x0004B874
			public void Dispose()
			{
				foreach (NativeArray<T> nativeArray in this.excess)
				{
					nativeArray.Dispose();
				}
				this.excess.Clear();
				bool isCreated = this.pool.IsCreated;
				if (isCreated)
				{
					this.pool.Dispose();
				}
			}

			// Token: 0x060011AF RID: 4527 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
			internal NativeSlice<T> Alloc(uint count)
			{
				bool flag = (ulong)(this.takenFromPool + count) <= (ulong)((long)this.pool.Length);
				NativeSlice<T> nativeSlice2;
				if (flag)
				{
					NativeSlice<T> nativeSlice = this.pool.Slice((int)this.takenFromPool, (int)count);
					this.takenFromPool += count;
					nativeSlice2 = nativeSlice;
				}
				else
				{
					NativeArray<T> nativeArray = new NativeArray<T>((int)count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
					this.excess.Add(nativeArray);
					nativeSlice2 = nativeArray;
				}
				return nativeSlice2;
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x0004D768 File Offset: 0x0004B968
			internal void SessionDone()
			{
				int num = this.pool.Length;
				foreach (NativeArray<T> nativeArray in this.excess)
				{
					bool flag = nativeArray.Length < this.maxPoolElemCount;
					if (flag)
					{
						num += nativeArray.Length;
					}
					nativeArray.Dispose();
				}
				this.excess.Clear();
				bool flag2 = num > this.pool.Length;
				if (flag2)
				{
					bool isCreated = this.pool.IsCreated;
					if (isCreated)
					{
						this.pool.Dispose();
					}
					this.pool = new NativeArray<T>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				this.takenFromPool = 0U;
			}

			// Token: 0x04000889 RID: 2185
			private int maxPoolElemCount;

			// Token: 0x0400088A RID: 2186
			private NativeArray<T> pool;

			// Token: 0x0400088B RID: 2187
			private List<NativeArray<T>> excess;

			// Token: 0x0400088C RID: 2188
			private uint takenFromPool;
		}
	}
}
