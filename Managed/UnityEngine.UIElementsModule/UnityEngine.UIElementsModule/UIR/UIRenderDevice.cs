using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200023A RID: 570
	internal class UIRenderDevice : IDisposable
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x00045648 File Offset: 0x00043848
		static UIRenderDevice()
		{
			Utility.EngineUpdate += new Action(UIRenderDevice.OnEngineUpdateGlobal);
			Utility.FlushPendingResources += new Action(UIRenderDevice.OnFlushPendingResources);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0004575C File Offset: 0x0004395C
		public UIRenderDevice(uint initialVertexCapacity = 0U, uint initialIndexCapacity = 0U)
			: this(initialVertexCapacity, initialIndexCapacity, false)
		{
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004576C File Offset: 0x0004396C
		protected UIRenderDevice(uint initialVertexCapacity, uint initialIndexCapacity, bool mockDevice)
		{
			this.m_MockDevice = mockDevice;
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			bool flag = UIRenderDevice.m_ActiveDeviceCount++ == 0;
			if (flag)
			{
				bool flag2 = !UIRenderDevice.m_SubscribedToNotifications && !this.m_MockDevice;
				if (flag2)
				{
					Utility.NotifyOfUIREvents(true);
					UIRenderDevice.m_SubscribedToNotifications = true;
				}
			}
			this.m_NextPageVertexCount = Math.Max(initialVertexCapacity, 2048U);
			this.m_LargeMeshVertexCount = this.m_NextPageVertexCount;
			this.m_IndexToVertexCountRatio = initialIndexCapacity / initialVertexCapacity;
			this.m_IndexToVertexCountRatio = Mathf.Max(this.m_IndexToVertexCountRatio, 2f);
			this.m_DeferredFrees = new List<List<UIRenderDevice.AllocToFree>>(4);
			this.m_Updates = new List<List<UIRenderDevice.AllocToUpdate>>(4);
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_DeferredFrees.Add(new List<UIRenderDevice.AllocToFree>());
				this.m_Updates.Add(new List<UIRenderDevice.AllocToUpdate>());
				num++;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00045880 File Offset: 0x00043A80
		internal static Texture2D whiteTexel
		{
			get
			{
				bool flag = UIRenderDevice.s_WhiteTexel == null;
				if (flag)
				{
					UIRenderDevice.s_WhiteTexel = new Texture2D(1, 1, TextureFormat.RGBA32, false);
					UIRenderDevice.s_WhiteTexel.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_WhiteTexel.filterMode = FilterMode.Bilinear;
					UIRenderDevice.s_WhiteTexel.SetPixel(0, 0, Color.white);
					UIRenderDevice.s_WhiteTexel.Apply(false, true);
				}
				return UIRenderDevice.s_WhiteTexel;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x000458F0 File Offset: 0x00043AF0
		internal static Texture2D defaultShaderInfoTexFloat
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexFloat == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexFloat = new Texture2D(64, 64, TextureFormat.RGBAFloat, false);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y, UIRVEShaderInfoAllocator.identityTransformRow0Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 1, UIRVEShaderInfoAllocator.identityTransformRow1Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 2, UIRVEShaderInfoAllocator.identityTransformRow2Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, UIRVEShaderInfoAllocator.infiniteClipRectTexel.y, UIRVEShaderInfoAllocator.infiniteClipRectValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexFloat;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00045A44 File Offset: 0x00043C44
		internal static Texture2D defaultShaderInfoTexARGB8
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexARGB8 == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = new Texture2D(64, 64, TextureFormat.RGBA32, false);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexARGB8;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00045AD4 File Offset: 0x00043CD4
		internal static bool vertexTexturingIsAvailable
		{
			get
			{
				bool flag = UIRenderDevice.s_VertexTexturingIsAvailable == null;
				if (flag)
				{
					Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					string tag = material.GetTag("UIE_VertexTexturingIsAvailable", false);
					UIRUtility.Destroy(material);
					UIRenderDevice.s_VertexTexturingIsAvailable = new bool?(tag == "1");
				}
				return UIRenderDevice.s_VertexTexturingIsAvailable.Value;
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00045B50 File Offset: 0x00043D50
		private void InitVertexDeclaration()
		{
			VertexAttributeDescriptor[] array = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8, 4, 0)
			};
			this.m_VertexDecl = Utility.GetVertexDeclaration(array);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00045BD4 File Offset: 0x00043DD4
		private void CompleteCreation()
		{
			bool flag = this.m_MockDevice || this.fullyCreated;
			if (!flag)
			{
				this.InitVertexDeclaration();
				this.m_Fences = new uint[4];
				this.m_StandardMatProps = new MaterialPropertyBlock();
				this.m_CommonMatProps = new MaterialPropertyBlock();
				Utility.EngineUpdate += new Action(this.OnEngineUpdate);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00045C34 File Offset: 0x00043E34
		private bool fullyCreated
		{
			get
			{
				return this.m_Fences != null;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00045C4F File Offset: 0x00043E4F
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x00045C57 File Offset: 0x00043E57
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600111B RID: 4379 RVA: 0x00045C60 File Offset: 0x00043E60
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00045C72 File Offset: 0x00043E72
		internal void DisposeImmediate()
		{
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			UIRenderDevice.m_SynchronousFree = true;
			this.Dispose();
			UIRenderDevice.m_SynchronousFree = false;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00045C98 File Offset: 0x00043E98
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				UIRenderDevice.m_ActiveDeviceCount--;
				if (disposing)
				{
					bool fullyCreated = this.fullyCreated;
					if (fullyCreated)
					{
						Utility.EngineUpdate -= new Action(this.OnEngineUpdate);
					}
					UIRenderDevice.DeviceToFree deviceToFree = new UIRenderDevice.DeviceToFree
					{
						handle = (this.m_MockDevice ? 0U : Utility.InsertCPUFence()),
						page = this.m_FirstPage
					};
					bool flag = deviceToFree.handle == 0U;
					if (flag)
					{
						deviceToFree.Dispose();
					}
					else
					{
						UIRenderDevice.m_DeviceFreeQueue.AddLast(deviceToFree);
						bool synchronousFree = UIRenderDevice.m_SynchronousFree;
						if (synchronousFree)
						{
							UIRenderDevice.ProcessDeviceFreeQueue();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00045D58 File Offset: 0x00043F58
		public MeshHandle Allocate(uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			MeshHandle meshHandle = this.m_MeshHandles.Get();
			meshHandle.triangleCount = indexCount / 3U;
			this.Allocate(meshHandle, vertexCount, indexCount, out vertexData, out indexData, false);
			indexOffset = (ushort)meshHandle.allocVerts.start;
			return meshHandle;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00045DA0 File Offset: 0x00043FA0
		public void Update(MeshHandle mesh, uint vertexCount, out NativeSlice<Vertex> vertexData)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
			}
			else
			{
				uint start = mesh.allocVerts.start;
				NativeSlice<ushort> nativeSlice = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)mesh.allocIndices.size);
				NativeSlice<ushort> nativeSlice2;
				ushort num;
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, mesh.allocIndices.size, out vertexData, out nativeSlice2, out num, out allocToUpdate, false);
				int size = (int)mesh.allocIndices.size;
				int num2 = (int)((uint)num - start);
				for (int i = 0; i < size; i++)
				{
					nativeSlice2[i] = (ushort)((int)nativeSlice[i] + num2);
				}
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00045E9C File Offset: 0x0004409C
		public void Update(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			Debug.Assert(mesh.allocIndices.size >= indexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
				indexData = mesh.allocPage.indices.cpuData.Slice((int)mesh.allocIndices.start, (int)indexCount);
				indexOffset = (ushort)mesh.allocVerts.start;
			}
			else
			{
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, indexCount, out vertexData, out indexData, out indexOffset, out allocToUpdate, true);
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00045F60 File Offset: 0x00044160
		private bool TryAllocFromPage(Page page, uint vertexCount, uint indexCount, ref Alloc va, ref Alloc ia, bool shortLived)
		{
			va = page.vertices.allocator.Allocate(vertexCount, shortLived);
			bool flag = va.size > 0U;
			if (flag)
			{
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
				bool flag2 = ia.size > 0U;
				if (flag2)
				{
					return true;
				}
				page.vertices.allocator.Free(va);
				va.size = 0U;
			}
			return false;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00045FEC File Offset: 0x000441EC
		private void Allocate(MeshHandle meshHandle, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, bool shortLived)
		{
			Page page = null;
			Alloc alloc = default(Alloc);
			Alloc alloc2 = default(Alloc);
			bool flag = vertexCount <= this.m_LargeMeshVertexCount;
			if (flag)
			{
				bool flag2 = this.m_FirstPage != null;
				if (flag2)
				{
					page = this.m_FirstPage;
					for (;;)
					{
						bool flag3 = this.TryAllocFromPage(page, vertexCount, indexCount, ref alloc, ref alloc2, shortLived) || page.next == null;
						if (flag3)
						{
							break;
						}
						page = page.next;
					}
				}
				else
				{
					this.CompleteCreation();
				}
				bool flag4 = alloc2.size == 0U;
				if (flag4)
				{
					this.m_NextPageVertexCount <<= 1;
					this.m_NextPageVertexCount = Math.Max(this.m_NextPageVertexCount, vertexCount * 2U);
					this.m_NextPageVertexCount = Math.Min(this.m_NextPageVertexCount, 65536U);
					uint num = (uint)(this.m_NextPageVertexCount * this.m_IndexToVertexCountRatio + 0.5f);
					num = Math.Max(num, indexCount * 2U);
					Debug.Assert(((page != null) ? page.next : null) == null);
					page = new Page(this.m_NextPageVertexCount, num, 4U, this.m_MockDevice);
					page.next = this.m_FirstPage;
					this.m_FirstPage = page;
					alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
					alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
					Debug.Assert(alloc.size > 0U);
					Debug.Assert(alloc2.size > 0U);
				}
			}
			else
			{
				this.CompleteCreation();
				Page page2 = this.m_FirstPage;
				while (page2 != null && page2.next != null)
				{
					page2 = page2.next;
				}
				Page page3 = new Page(vertexCount, indexCount, 4U, this.m_MockDevice);
				bool flag5 = page2 != null;
				if (flag5)
				{
					page2.next = page3;
				}
				else
				{
					this.m_FirstPage = page3;
				}
				page = page3;
				alloc = page3.vertices.allocator.Allocate(vertexCount, shortLived);
				alloc2 = page3.indices.allocator.Allocate(indexCount, shortLived);
			}
			page.vertices.RegisterUpdate(alloc.start, alloc.size);
			page.indices.RegisterUpdate(alloc2.start, alloc2.size);
			vertexData = new NativeSlice<Vertex>(page.vertices.cpuData, (int)alloc.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(page.indices.cpuData, (int)alloc2.start, (int)indexCount);
			meshHandle.allocPage = page;
			meshHandle.allocVerts = alloc;
			meshHandle.allocIndices = alloc2;
			meshHandle.allocTime = this.m_FrameIndex;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00046288 File Offset: 0x00044488
		private void UpdateAfterGPUUsedData(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset, out UIRenderDevice.AllocToUpdate allocToUpdate, bool copyBackIndices)
		{
			UIRenderDevice.AllocToUpdate allocToUpdate2 = default(UIRenderDevice.AllocToUpdate);
			uint nextUpdateID = this.m_NextUpdateID;
			this.m_NextUpdateID = nextUpdateID + 1U;
			allocToUpdate2.id = nextUpdateID;
			allocToUpdate2.allocTime = this.m_FrameIndex;
			allocToUpdate2.meshHandle = mesh;
			allocToUpdate2.copyBackIndices = copyBackIndices;
			allocToUpdate = allocToUpdate2;
			Debug.Assert(this.m_NextUpdateID > 0U);
			bool flag = mesh.updateAllocID == 0U;
			if (flag)
			{
				allocToUpdate.permAllocVerts = mesh.allocVerts;
				allocToUpdate.permAllocIndices = mesh.allocIndices;
				allocToUpdate.permPage = mesh.allocPage;
			}
			else
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate3 = list[num];
				Debug.Assert(allocToUpdate3.id == mesh.updateAllocID);
				allocToUpdate.copyBackIndices |= allocToUpdate3.copyBackIndices;
				allocToUpdate.permAllocVerts = allocToUpdate3.permAllocVerts;
				allocToUpdate.permAllocIndices = allocToUpdate3.permAllocIndices;
				allocToUpdate.permPage = allocToUpdate3.permPage;
				allocToUpdate3.allocTime = uint.MaxValue;
				list[num] = allocToUpdate3;
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
			}
			bool flag2 = this.TryAllocFromPage(mesh.allocPage, vertexCount, indexCount, ref mesh.allocVerts, ref mesh.allocIndices, true);
			if (flag2)
			{
				mesh.allocPage.vertices.RegisterUpdate(mesh.allocVerts.start, mesh.allocVerts.size);
				mesh.allocPage.indices.RegisterUpdate(mesh.allocIndices.start, mesh.allocIndices.size);
			}
			else
			{
				this.Allocate(mesh, vertexCount, indexCount, out vertexData, out indexData, true);
			}
			mesh.triangleCount = indexCount / 3U;
			mesh.updateAllocID = allocToUpdate.id;
			mesh.allocTime = allocToUpdate.allocTime;
			this.m_Updates[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Updates.Count))].Add(allocToUpdate);
			vertexData = new NativeSlice<Vertex>(mesh.allocPage.vertices.cpuData, (int)mesh.allocVerts.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)indexCount);
			indexOffset = (ushort)mesh.allocVerts.start;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00046578 File Offset: 0x00044778
		public void Free(MeshHandle mesh)
		{
			bool flag = mesh.updateAllocID > 0U;
			if (flag)
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate = list[num];
				Debug.Assert(allocToUpdate.id == mesh.updateAllocID);
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocVerts,
					page = allocToUpdate.permPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocIndices,
					page = allocToUpdate.permPage,
					vertices = false
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
				allocToUpdate.allocTime = uint.MaxValue;
				list[num] = allocToUpdate;
			}
			else
			{
				bool flag2 = mesh.allocTime != this.m_FrameIndex;
				if (flag2)
				{
					int num2 = (int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count);
					this.m_DeferredFrees[num2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocVerts,
						page = mesh.allocPage,
						vertices = true
					});
					this.m_DeferredFrees[num2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocIndices,
						page = mesh.allocPage,
						vertices = false
					});
				}
				else
				{
					mesh.allocPage.vertices.allocator.Free(mesh.allocVerts);
					mesh.allocPage.indices.allocator.Free(mesh.allocIndices);
				}
			}
			mesh.allocVerts = default(Alloc);
			mesh.allocIndices = default(Alloc);
			mesh.allocPage = null;
			mesh.updateAllocID = 0U;
			this.m_MeshHandles.Return(mesh);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00046800 File Offset: 0x00044A00
		private static void Set1PixelSizeParameter(DrawParams drawParams, MaterialPropertyBlock props)
		{
			Vector4 vector = default(Vector4);
			RectInt activeViewport = Utility.GetActiveViewport();
			vector.x = 2f / (float)activeViewport.width;
			vector.y = 2f / (float)activeViewport.height;
			Matrix4x4 unityProjectionMatrix = Utility.GetUnityProjectionMatrix();
			Vector3 vector2 = (unityProjectionMatrix * drawParams.view.Peek().transform).inverse.MultiplyVector(new Vector3(vector.x, vector.y));
			vector.z = 1f / (Mathf.Abs(vector2.x) + Mathf.Epsilon);
			vector.w = 1f / (Mathf.Abs(vector2.y) + Mathf.Epsilon);
			props.SetVector(UIRenderDevice.s_1PixelClipInvViewPropID, vector);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000468D4 File Offset: 0x00044AD4
		public void OnFrameRenderingBegin()
		{
			bool flag = !this.m_FrameIndexIncremented;
			if (flag)
			{
				this.AdvanceFrame();
			}
			this.m_FrameIndexIncremented = false;
			this.m_DrawStats = default(UIRenderDevice.DrawStatistics);
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				page.vertices.SendUpdates();
				page.indices.SendUpdates();
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004694C File Offset: 0x00044B4C
		private unsafe static NativeSlice<T> PtrToSlice<T>(void* p, int count) where T : struct
		{
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(p, UnsafeUtility.SizeOf<T>(), count);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004696C File Offset: 0x00044B6C
		public unsafe void EvaluateChain(RenderChainCommand head, Material initialMat, Material defaultMat, Texture atlas, Texture gradientSettings, Texture shaderInfo, float pixelsPerPoint, NativeSlice<Transform3x4> transforms, NativeSlice<Vector4> clipRects, MaterialPropertyBlock stateMatProps, bool allowMaterialChange, ref Exception immediateException)
		{
			Utility.ProfileDrawChainBegin();
			DrawParams drawParams = this.m_DrawParams;
			drawParams.Reset();
			stateMatProps.Clear();
			bool fullyCreated = this.fullyCreated;
			if (fullyCreated)
			{
				bool flag = atlas != null;
				if (flag)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_MainTexPropID, atlas);
				}
				bool flag2 = gradientSettings != null;
				if (flag2)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
				}
				bool flag3 = shaderInfo != null;
				if (flag3)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
				}
				bool flag4 = transforms.Length > 0;
				if (flag4)
				{
					Utility.SetVectorArray<Transform3x4>(this.m_StandardMatProps, UIRenderDevice.s_TransformsPropID, transforms);
				}
				bool flag5 = clipRects.Length > 0;
				if (flag5)
				{
					Utility.SetVectorArray<Vector4>(this.m_StandardMatProps, UIRenderDevice.s_ClipRectsPropID, clipRects);
				}
				UIRenderDevice.Set1PixelSizeParameter(drawParams, this.m_CommonMatProps);
				this.m_CommonMatProps.SetVector(UIRenderDevice.s_ScreenClipRectPropID, drawParams.view.Peek().clipRect);
				Utility.SetPropertyBlock(this.m_StandardMatProps);
				Utility.SetPropertyBlock(this.m_CommonMatProps);
			}
			int num = 1024;
			DrawBufferRange* ptr;
			checked
			{
				ptr = stackalloc DrawBufferRange[unchecked((UIntPtr)num) * (UIntPtr)sizeof(DrawBufferRange)];
			}
			int num2 = num - 1;
			int num3 = 0;
			int num4 = 0;
			DrawBufferRange drawBufferRange = default(DrawBufferRange);
			Page page = null;
			State state = new State
			{
				material = initialMat
			};
			int num5 = -1;
			int num6 = 0;
			while (head != null)
			{
				this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
				this.m_DrawStats.drawCommandCount = this.m_DrawStats.drawCommandCount + ((head.type == CommandType.Draw) ? 1U : 0U);
				bool flag6 = head.type > CommandType.Draw;
				bool flag7 = true;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = !flag6;
				if (flag10)
				{
					Material material = ((head.state.material != null) ? head.state.material : defaultMat);
					flag8 = material != state.material;
					state.material = material;
					bool flag11 = head.state.custom != null;
					if (flag11)
					{
						flag9 |= head.state.custom != state.custom;
						state.custom = head.state.custom;
						stateMatProps.SetTexture(UIRenderDevice.s_CustomTexPropID, head.state.custom);
					}
					bool flag12 = head.state.font != null;
					if (flag12)
					{
						flag9 |= head.state.font != state.font;
						state.font = head.state.font;
						stateMatProps.SetTexture(UIRenderDevice.s_FontTexPropID, head.state.font);
					}
					flag6 = flag9 || flag8 || head.mesh.allocPage != page;
					bool flag13 = !flag6;
					if (flag13)
					{
						flag7 = (long)num5 != (long)((ulong)head.mesh.allocIndices.start + (ulong)((long)head.indexOffset));
					}
				}
				bool flag14 = flag7;
				if (flag14)
				{
					bool flag15 = drawBufferRange.indexCount > 0;
					if (flag15)
					{
						int num7 = (num3 + num4++) & num2;
						ptr[num7] = drawBufferRange;
						bool flag16 = num4 == num;
						if (flag16)
						{
							this.KickRanges(ptr, ref num4, ref num3, num, page);
						}
						drawBufferRange = default(DrawBufferRange);
						this.m_DrawStats.drawRangeCount = this.m_DrawStats.drawRangeCount + 1U;
					}
					bool flag17 = head.type == CommandType.Draw;
					if (flag17)
					{
						drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset);
						drawBufferRange.indexCount = head.indexCount;
						drawBufferRange.vertsReferenced = (int)(head.mesh.allocVerts.start + head.mesh.allocVerts.size);
						drawBufferRange.minIndexVal = (int)head.mesh.allocVerts.start;
						num5 = drawBufferRange.firstIndex + head.indexCount;
						num6 = drawBufferRange.vertsReferenced + drawBufferRange.minIndexVal;
						this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					}
					bool flag18 = flag6;
					if (flag18)
					{
						this.KickRanges(ptr, ref num4, ref num3, num, page);
						bool flag19 = head.type > CommandType.Draw;
						if (flag19)
						{
							bool flag20 = !this.m_MockDevice;
							if (flag20)
							{
								head.ExecuteNonDrawMesh(drawParams, pixelsPerPoint, ref immediateException);
							}
							bool flag21 = head.type == CommandType.Immediate || head.type == CommandType.ImmediateCull;
							if (flag21)
							{
								state.material = null;
								flag8 = false;
								this.m_DrawStats.immediateDraws = this.m_DrawStats.immediateDraws + 1U;
							}
						}
						else
						{
							page = head.mesh.allocPage;
						}
						bool flag22 = flag8 || flag9;
						if (flag22)
						{
							bool flag23 = !this.m_MockDevice;
							if (flag23)
							{
								bool flag24 = flag8;
								if (flag24)
								{
									bool flag25 = !allowMaterialChange;
									if (flag25)
									{
										IL_076E:
										Utility.ProfileDrawChainEnd();
										return;
									}
									state.material.SetPass(0);
									bool flag26 = this.m_StandardMatProps != null;
									if (flag26)
									{
										Utility.SetPropertyBlock(this.m_StandardMatProps);
										Utility.SetPropertyBlock(this.m_CommonMatProps);
									}
									Utility.SetPropertyBlock(stateMatProps);
								}
								else
								{
									bool flag27 = flag9;
									if (flag27)
									{
										Utility.SetPropertyBlock(stateMatProps);
									}
									else
									{
										bool flag28 = this.m_CommonMatProps != null && (head.type == CommandType.PushView || head.type == CommandType.PopView);
										if (flag28)
										{
											UIRenderDevice.Set1PixelSizeParameter(drawParams, this.m_CommonMatProps);
											this.m_CommonMatProps.SetVector(UIRenderDevice.s_ScreenClipRectPropID, drawParams.view.Peek().clipRect);
											Utility.SetPropertyBlock(this.m_CommonMatProps);
										}
									}
								}
							}
							this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1U;
						}
						else
						{
							bool flag29 = head.type == CommandType.PushView || head.type == CommandType.PopView;
							if (flag29)
							{
								bool flag30 = this.m_CommonMatProps != null;
								if (flag30)
								{
									UIRenderDevice.Set1PixelSizeParameter(drawParams, this.m_CommonMatProps);
									this.m_CommonMatProps.SetVector(UIRenderDevice.s_ScreenClipRectPropID, drawParams.view.Peek().clipRect);
									Utility.SetPropertyBlock(this.m_CommonMatProps);
								}
								this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1U;
							}
						}
					}
					head = head.next;
				}
				else
				{
					bool flag31 = drawBufferRange.indexCount == 0;
					if (flag31)
					{
						num5 = (drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset));
					}
					num6 = Math.Max(num6, (int)(head.mesh.allocVerts.size + head.mesh.allocVerts.start));
					drawBufferRange.indexCount += head.indexCount;
					drawBufferRange.minIndexVal = Math.Min(drawBufferRange.minIndexVal, (int)head.mesh.allocVerts.start);
					drawBufferRange.vertsReferenced = num6 - drawBufferRange.minIndexVal;
					num5 += head.indexCount;
					this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					head = head.next;
				}
			}
			bool flag32 = drawBufferRange.indexCount > 0;
			if (flag32)
			{
				int num8 = (num3 + num4++) & num2;
				ptr[num8] = drawBufferRange;
			}
			bool flag33 = num4 > 0;
			if (flag33)
			{
				this.KickRanges(ptr, ref num4, ref num3, num, page);
			}
			this.UpdateFenceValue();
			goto IL_076E;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000470F0 File Offset: 0x000452F0
		private unsafe void UpdateFenceValue()
		{
			bool flag = this.m_Fences != null;
			if (flag)
			{
				uint num = Utility.InsertCPUFence();
				fixed (uint* ptr = &this.m_Fences[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length))])
				{
					uint* ptr2 = ptr;
					bool flag3;
					do
					{
						uint num2 = *ptr2;
						bool flag2 = num - num2 <= 0U;
						if (flag2)
						{
							break;
						}
						int num3 = Interlocked.CompareExchange(ref *(int*)ptr2, (int)num, (int)num2);
						flag3 = (long)num3 == (long)((ulong)num2);
					}
					while (!flag3);
				}
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00047170 File Offset: 0x00045370
		private unsafe void KickRanges(DrawBufferRange* ranges, ref int rangesReady, ref int rangesStart, int rangesCount, Page curPage)
		{
			bool flag = rangesReady > 0;
			if (flag)
			{
				bool flag2 = rangesStart + rangesReady <= rangesCount;
				if (flag2)
				{
					bool flag3 = !this.m_MockDevice;
					if (flag3)
					{
						this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), rangesReady));
					}
					this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 1U;
				}
				else
				{
					int num = rangesCount - rangesStart;
					int num2 = rangesReady - num;
					bool flag4 = !this.m_MockDevice;
					if (flag4)
					{
						this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), num));
						this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)ranges, num2));
					}
					this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 2U;
				}
				rangesStart = (rangesStart + rangesReady) & (rangesCount - 1);
				rangesReady = 0;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00047288 File Offset: 0x00045488
		private unsafe void DrawRanges<I, T>(Utility.GPUBuffer<I> ib, Utility.GPUBuffer<T> vb, NativeSlice<DrawBufferRange> ranges) where I : struct where T : struct
		{
			checked
			{
				IntPtr* ptr = stackalloc IntPtr[unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)];
				*ptr = vb.BufferPointer;
				Utility.DrawRanges(ib.BufferPointer, ptr, 1, new IntPtr(ranges.GetUnsafePtr<DrawBufferRange>()), ranges.Length, this.m_VertexDecl);
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000472D0 File Offset: 0x000454D0
		public void AdvanceFrame()
		{
			this.m_FrameIndex += 1U;
			this.m_FrameIndexIncremented = true;
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			bool flag = this.m_Fences != null;
			if (flag)
			{
				int num = (int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length));
				uint num2 = this.m_Fences[num];
				bool flag2 = num2 != 0U && !Utility.CPUFencePassed(num2);
				if (flag2)
				{
					Utility.WaitForCPUFencePassed(num2);
				}
				this.m_Fences[num] = 0U;
			}
			this.m_NextUpdateID = 1U;
			List<UIRenderDevice.AllocToFree> list = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToFree allocToFree in list)
			{
				bool vertices = allocToFree.vertices;
				if (vertices)
				{
					allocToFree.page.vertices.allocator.Free(allocToFree.alloc);
				}
				else
				{
					allocToFree.page.indices.allocator.Free(allocToFree.alloc);
				}
			}
			list.Clear();
			List<UIRenderDevice.AllocToUpdate> list2 = this.m_Updates[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToUpdate allocToUpdate in list2)
			{
				bool flag3 = allocToUpdate.meshHandle.updateAllocID == allocToUpdate.id && allocToUpdate.meshHandle.allocTime == allocToUpdate.allocTime;
				if (flag3)
				{
					NativeSlice<Vertex> nativeSlice = new NativeSlice<Vertex>(allocToUpdate.meshHandle.allocPage.vertices.cpuData, (int)allocToUpdate.meshHandle.allocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					NativeSlice<Vertex> nativeSlice2 = new NativeSlice<Vertex>(allocToUpdate.permPage.vertices.cpuData, (int)allocToUpdate.permAllocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					nativeSlice2.CopyFrom(nativeSlice);
					allocToUpdate.permPage.vertices.RegisterUpdate(allocToUpdate.permAllocVerts.start, allocToUpdate.meshHandle.allocVerts.size);
					bool copyBackIndices = allocToUpdate.copyBackIndices;
					if (copyBackIndices)
					{
						NativeSlice<ushort> nativeSlice3 = new NativeSlice<ushort>(allocToUpdate.meshHandle.allocPage.indices.cpuData, (int)allocToUpdate.meshHandle.allocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						NativeSlice<ushort> nativeSlice4 = new NativeSlice<ushort>(allocToUpdate.permPage.indices.cpuData, (int)allocToUpdate.permAllocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						int length = nativeSlice4.Length;
						int num3 = (int)(allocToUpdate.permAllocVerts.start - allocToUpdate.meshHandle.allocVerts.start);
						for (int i = 0; i < length; i++)
						{
							nativeSlice4[i] = (ushort)((int)nativeSlice3[i] + num3);
						}
						allocToUpdate.permPage.indices.RegisterUpdate(allocToUpdate.permAllocIndices.start, allocToUpdate.meshHandle.allocIndices.size);
					}
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocVerts,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = true
					});
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocIndices,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = false
					});
					allocToUpdate.meshHandle.allocVerts = allocToUpdate.permAllocVerts;
					allocToUpdate.meshHandle.allocIndices = allocToUpdate.permAllocIndices;
					allocToUpdate.meshHandle.allocPage = allocToUpdate.permPage;
					allocToUpdate.meshHandle.updateAllocID = 0U;
				}
			}
			list2.Clear();
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00047748 File Offset: 0x00045948
		internal static void PrepareForGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount++;
			bool flag = UIRenderDevice.s_WhiteTexel != null;
			if (flag)
			{
				UIRUtility.Destroy(UIRenderDevice.s_WhiteTexel);
				UIRenderDevice.s_WhiteTexel = null;
			}
			bool flag2 = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
			if (flag2)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
				UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
			}
			bool flag3 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
			if (flag3)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
				UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000477C8 File Offset: 0x000459C8
		internal static void WrapUpGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount--;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000477D7 File Offset: 0x000459D7
		internal static void FlushAllPendingDeviceDisposes()
		{
			Utility.SyncRenderThread();
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000477E8 File Offset: 0x000459E8
		internal UIRenderDevice.AllocationStatistics GatherAllocationStatistics()
		{
			UIRenderDevice.AllocationStatistics allocationStatistics = default(UIRenderDevice.AllocationStatistics);
			allocationStatistics.completeInit = this.fullyCreated;
			allocationStatistics.freesDeferred = new int[this.m_DeferredFrees.Count];
			for (int i = 0; i < this.m_DeferredFrees.Count; i++)
			{
				allocationStatistics.freesDeferred[i] = this.m_DeferredFrees[i].Count;
			}
			int num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				num++;
			}
			allocationStatistics.pages = new UIRenderDevice.AllocationStatistics.PageStatistics[num];
			num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				allocationStatistics.pages[num].vertices = page.vertices.allocator.GatherStatistics();
				allocationStatistics.pages[num].indices = page.indices.allocator.GatherStatistics();
				num++;
			}
			return allocationStatistics;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000478F4 File Offset: 0x00045AF4
		internal UIRenderDevice.DrawStatistics GatherDrawStatistics()
		{
			return this.m_DrawStats;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004790C File Offset: 0x00045B0C
		private void OnEngineUpdate()
		{
			this.AdvanceFrame();
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00047918 File Offset: 0x00045B18
		private static void ProcessDeviceFreeQueue()
		{
			bool synchronousFree = UIRenderDevice.m_SynchronousFree;
			if (synchronousFree)
			{
				Utility.SyncRenderThread();
			}
			for (LinkedListNode<UIRenderDevice.DeviceToFree> linkedListNode = UIRenderDevice.m_DeviceFreeQueue.First; linkedListNode != null; linkedListNode = UIRenderDevice.m_DeviceFreeQueue.First)
			{
				bool flag = !Utility.CPUFencePassed(linkedListNode.Value.handle);
				if (flag)
				{
					break;
				}
				linkedListNode.Value.Dispose();
				UIRenderDevice.m_DeviceFreeQueue.RemoveFirst();
			}
			Debug.Assert(!UIRenderDevice.m_SynchronousFree || UIRenderDevice.m_DeviceFreeQueue.Count == 0);
			bool flag2 = UIRenderDevice.m_ActiveDeviceCount == 0 && UIRenderDevice.m_SubscribedToNotifications;
			if (flag2)
			{
				bool flag3 = UIRenderDevice.s_WhiteTexel != null;
				if (flag3)
				{
					UIRUtility.Destroy(UIRenderDevice.s_WhiteTexel);
					UIRenderDevice.s_WhiteTexel = null;
				}
				bool flag4 = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
				if (flag4)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
					UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
				}
				bool flag5 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
				if (flag5)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
				}
				Utility.NotifyOfUIREvents(false);
				UIRenderDevice.m_SubscribedToNotifications = false;
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00047A39 File Offset: 0x00045C39
		private static void OnEngineUpdateGlobal()
		{
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00047A42 File Offset: 0x00045C42
		private static void OnFlushPendingResources()
		{
			UIRenderDevice.m_SynchronousFree = true;
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x040007C2 RID: 1986
		private const uint k_MaxQueuedFrameCount = 4U;

		// Token: 0x040007C3 RID: 1987
		private readonly bool m_MockDevice;

		// Token: 0x040007C4 RID: 1988
		private IntPtr m_VertexDecl;

		// Token: 0x040007C5 RID: 1989
		private Page m_FirstPage;

		// Token: 0x040007C6 RID: 1990
		private uint m_NextPageVertexCount;

		// Token: 0x040007C7 RID: 1991
		private uint m_LargeMeshVertexCount;

		// Token: 0x040007C8 RID: 1992
		private float m_IndexToVertexCountRatio;

		// Token: 0x040007C9 RID: 1993
		private List<List<UIRenderDevice.AllocToFree>> m_DeferredFrees;

		// Token: 0x040007CA RID: 1994
		private List<List<UIRenderDevice.AllocToUpdate>> m_Updates;

		// Token: 0x040007CB RID: 1995
		private uint[] m_Fences;

		// Token: 0x040007CC RID: 1996
		private MaterialPropertyBlock m_StandardMatProps;

		// Token: 0x040007CD RID: 1997
		private MaterialPropertyBlock m_CommonMatProps;

		// Token: 0x040007CE RID: 1998
		private uint m_FrameIndex;

		// Token: 0x040007CF RID: 1999
		private bool m_FrameIndexIncremented;

		// Token: 0x040007D0 RID: 2000
		private uint m_NextUpdateID = 1U;

		// Token: 0x040007D1 RID: 2001
		private UIRenderDevice.DrawStatistics m_DrawStats;

		// Token: 0x040007D2 RID: 2002
		private readonly Pool<MeshHandle> m_MeshHandles = new Pool<MeshHandle>();

		// Token: 0x040007D3 RID: 2003
		private readonly DrawParams m_DrawParams = new DrawParams();

		// Token: 0x040007D4 RID: 2004
		private static LinkedList<UIRenderDevice.DeviceToFree> m_DeviceFreeQueue = new LinkedList<UIRenderDevice.DeviceToFree>();

		// Token: 0x040007D5 RID: 2005
		private static int m_ActiveDeviceCount = 0;

		// Token: 0x040007D6 RID: 2006
		private static bool m_SubscribedToNotifications;

		// Token: 0x040007D7 RID: 2007
		private static bool m_SynchronousFree;

		// Token: 0x040007D8 RID: 2008
		private static readonly int s_MainTexPropID = Shader.PropertyToID("_MainTex");

		// Token: 0x040007D9 RID: 2009
		private static readonly int s_FontTexPropID = Shader.PropertyToID("_FontTex");

		// Token: 0x040007DA RID: 2010
		private static readonly int s_CustomTexPropID = Shader.PropertyToID("_CustomTex");

		// Token: 0x040007DB RID: 2011
		private static readonly int s_1PixelClipInvViewPropID = Shader.PropertyToID("_1PixelClipInvView");

		// Token: 0x040007DC RID: 2012
		private static readonly int s_GradientSettingsTexID = Shader.PropertyToID("_GradientSettingsTex");

		// Token: 0x040007DD RID: 2013
		private static readonly int s_ShaderInfoTexID = Shader.PropertyToID("_ShaderInfoTex");

		// Token: 0x040007DE RID: 2014
		private static readonly int s_ScreenClipRectPropID = Shader.PropertyToID("_ScreenClipRect");

		// Token: 0x040007DF RID: 2015
		private static readonly int s_TransformsPropID = Shader.PropertyToID("_Transforms");

		// Token: 0x040007E0 RID: 2016
		private static readonly int s_ClipRectsPropID = Shader.PropertyToID("_ClipRects");

		// Token: 0x040007E1 RID: 2017
		private static ProfilerMarker s_MarkerAllocate = new ProfilerMarker("UIR.Allocate");

		// Token: 0x040007E2 RID: 2018
		private static ProfilerMarker s_MarkerFree = new ProfilerMarker("UIR.Free");

		// Token: 0x040007E3 RID: 2019
		private static ProfilerMarker s_MarkerAdvanceFrame = new ProfilerMarker("UIR.AdvanceFrame");

		// Token: 0x040007E4 RID: 2020
		private static ProfilerMarker s_MarkerFence = new ProfilerMarker("UIR.WaitOnFence");

		// Token: 0x040007E5 RID: 2021
		private static ProfilerMarker s_MarkerBeforeDraw = new ProfilerMarker("UIR.BeforeDraw");

		// Token: 0x040007E6 RID: 2022
		private static bool? s_VertexTexturingIsAvailable;

		// Token: 0x040007E7 RID: 2023
		private const string k_VertexTexturingIsAvailableTag = "UIE_VertexTexturingIsAvailable";

		// Token: 0x040007E8 RID: 2024
		private const string k_VertexTexturingIsAvailableTrue = "1";

		// Token: 0x040007E9 RID: 2025
		private static Texture2D s_WhiteTexel;

		// Token: 0x040007EA RID: 2026
		private static Texture2D s_DefaultShaderInfoTexFloat;

		// Token: 0x040007EB RID: 2027
		private static Texture2D s_DefaultShaderInfoTexARGB8;

		// Token: 0x0200023B RID: 571
		private struct AllocToUpdate
		{
			// Token: 0x040007ED RID: 2029
			public uint id;

			// Token: 0x040007EE RID: 2030
			public uint allocTime;

			// Token: 0x040007EF RID: 2031
			public MeshHandle meshHandle;

			// Token: 0x040007F0 RID: 2032
			public Alloc permAllocVerts;

			// Token: 0x040007F1 RID: 2033
			public Alloc permAllocIndices;

			// Token: 0x040007F2 RID: 2034
			public Page permPage;

			// Token: 0x040007F3 RID: 2035
			public bool copyBackIndices;
		}

		// Token: 0x0200023C RID: 572
		private struct AllocToFree
		{
			// Token: 0x040007F4 RID: 2036
			public Alloc alloc;

			// Token: 0x040007F5 RID: 2037
			public Page page;

			// Token: 0x040007F6 RID: 2038
			public bool vertices;
		}

		// Token: 0x0200023D RID: 573
		private struct DeviceToFree
		{
			// Token: 0x06001136 RID: 4406 RVA: 0x00047A54 File Offset: 0x00045C54
			public void Dispose()
			{
				while (this.page != null)
				{
					Page page = this.page;
					this.page = this.page.next;
					page.Dispose();
				}
			}

			// Token: 0x040007F7 RID: 2039
			public uint handle;

			// Token: 0x040007F8 RID: 2040
			public Page page;
		}

		// Token: 0x0200023E RID: 574
		internal struct AllocationStatistics
		{
			// Token: 0x040007F9 RID: 2041
			public UIRenderDevice.AllocationStatistics.PageStatistics[] pages;

			// Token: 0x040007FA RID: 2042
			public int[] freesDeferred;

			// Token: 0x040007FB RID: 2043
			public bool completeInit;

			// Token: 0x0200023F RID: 575
			public struct PageStatistics
			{
				// Token: 0x040007FC RID: 2044
				internal HeapStatistics vertices;

				// Token: 0x040007FD RID: 2045
				internal HeapStatistics indices;
			}
		}

		// Token: 0x02000240 RID: 576
		internal struct DrawStatistics
		{
			// Token: 0x040007FE RID: 2046
			public int currentFrameIndex;

			// Token: 0x040007FF RID: 2047
			public uint totalIndices;

			// Token: 0x04000800 RID: 2048
			public uint commandCount;

			// Token: 0x04000801 RID: 2049
			public uint drawCommandCount;

			// Token: 0x04000802 RID: 2050
			public uint materialSetCount;

			// Token: 0x04000803 RID: 2051
			public uint drawRangeCount;

			// Token: 0x04000804 RID: 2052
			public uint drawRangeCallCount;

			// Token: 0x04000805 RID: 2053
			public uint immediateDraws;
		}
	}
}
