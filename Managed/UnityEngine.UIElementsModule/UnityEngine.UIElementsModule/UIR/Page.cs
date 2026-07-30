using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000248 RID: 584
	internal class Page : IDisposable
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x0004844E File Offset: 0x0004664E
		public Page(uint vertexMaxCount, uint indexMaxCount, uint maxQueuedFrameCount, bool mockPage)
		{
			vertexMaxCount = Math.Min(vertexMaxCount, 65535U);
			this.vertices = new Page.DataSet<Vertex>(Utility.GPUBufferType.Vertex, vertexMaxCount, maxQueuedFrameCount, 32U, mockPage);
			this.indices = new Page.DataSet<ushort>(Utility.GPUBufferType.Index, indexMaxCount, maxQueuedFrameCount, 32U, mockPage);
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00048489 File Offset: 0x00046689
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x00048491 File Offset: 0x00046691
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600114F RID: 4431 RVA: 0x0004849A File Offset: 0x0004669A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000484AC File Offset: 0x000466AC
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.indices.Dispose();
					this.vertices.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000484F0 File Offset: 0x000466F0
		public bool isEmpty
		{
			get
			{
				return this.vertices.allocator.isEmpty && this.indices.allocator.isEmpty;
			}
		}

		// Token: 0x04000825 RID: 2085
		public Page.DataSet<Vertex> vertices;

		// Token: 0x04000826 RID: 2086
		public Page.DataSet<ushort> indices;

		// Token: 0x04000827 RID: 2087
		public Page next;

		// Token: 0x02000249 RID: 585
		public class DataSet<T> : IDisposable where T : struct
		{
			// Token: 0x06001152 RID: 4434 RVA: 0x00048528 File Offset: 0x00046728
			public DataSet(Utility.GPUBufferType bufferType, uint totalCount, uint maxQueuedFrameCount, uint updateRangePoolSize, bool mockBuffer)
			{
				bool flag = !mockBuffer;
				if (flag)
				{
					this.gpuData = new Utility.GPUBuffer<T>((int)totalCount, bufferType);
				}
				this.cpuData = new NativeArray<T>((int)totalCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.allocator = new GPUBufferAllocator(totalCount);
				bool flag2 = !mockBuffer;
				if (flag2)
				{
					this.m_ElemStride = (uint)this.gpuData.ElementStride;
				}
				this.m_UpdateRangePoolSize = updateRangePoolSize;
				uint num = this.m_UpdateRangePoolSize * maxQueuedFrameCount;
				this.updateRanges = new NativeArray<GfxUpdateBufferRange>((int)num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart = 0U;
			}

			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06001153 RID: 4435 RVA: 0x000485C2 File Offset: 0x000467C2
			// (set) Token: 0x06001154 RID: 4436 RVA: 0x000485CA File Offset: 0x000467CA
			private protected bool disposed { protected get; private set; }

			// Token: 0x06001155 RID: 4437 RVA: 0x000485D3 File Offset: 0x000467D3
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x000485E8 File Offset: 0x000467E8
			public void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						Utility.GPUBuffer<T> gpubuffer = this.gpuData;
						if (gpubuffer != null)
						{
							gpubuffer.Dispose();
						}
						this.cpuData.Dispose();
						this.updateRanges.Dispose();
					}
					this.disposed = true;
				}
			}

			// Token: 0x06001157 RID: 4439 RVA: 0x00048640 File Offset: 0x00046840
			public void RegisterUpdate(uint start, uint size)
			{
				Debug.Assert((ulong)(start + size) <= (ulong)((long)this.cpuData.Length));
				int num = (int)(this.m_UpdateRangesBatchStart + this.m_UpdateRangesEnqueued);
				bool flag = this.m_UpdateRangesEnqueued > 0U;
				if (flag)
				{
					int num2 = num - 1;
					GfxUpdateBufferRange gfxUpdateBufferRange = this.updateRanges[num2];
					uint num3 = start * this.m_ElemStride;
					bool flag2 = gfxUpdateBufferRange.offsetFromWriteStart + gfxUpdateBufferRange.size == num3;
					if (flag2)
					{
						this.updateRanges[num2] = new GfxUpdateBufferRange
						{
							source = gfxUpdateBufferRange.source,
							offsetFromWriteStart = gfxUpdateBufferRange.offsetFromWriteStart,
							size = gfxUpdateBufferRange.size + size * this.m_ElemStride
						};
						this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
						return;
					}
				}
				this.m_UpdateRangeMin = Math.Min(this.m_UpdateRangeMin, start);
				this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
				bool flag3 = this.m_UpdateRangesEnqueued == this.m_UpdateRangePoolSize;
				if (flag3)
				{
					this.m_UpdateRangesSaturated = true;
				}
				else
				{
					UIntPtr uintPtr;
					uintPtr..ctor(this.cpuData.Slice((int)start, (int)size).GetUnsafeReadOnlyPtr<T>());
					this.updateRanges[num] = new GfxUpdateBufferRange
					{
						source = uintPtr,
						offsetFromWriteStart = start * this.m_ElemStride,
						size = size * this.m_ElemStride
					};
					this.m_UpdateRangesEnqueued += 1U;
				}
			}

			// Token: 0x06001158 RID: 4440 RVA: 0x000487CC File Offset: 0x000469CC
			public void SendUpdates()
			{
				bool flag = this.m_UpdateRangesEnqueued == 0U;
				if (!flag)
				{
					bool updateRangesSaturated = this.m_UpdateRangesSaturated;
					if (updateRangesSaturated)
					{
						uint num = this.m_UpdateRangeMax - this.m_UpdateRangeMin;
						this.m_UpdateRangesEnqueued = 1U;
						this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
						{
							source = new UIntPtr(this.cpuData.Slice((int)this.m_UpdateRangeMin, (int)num).GetUnsafeReadOnlyPtr<T>()),
							offsetFromWriteStart = this.m_UpdateRangeMin * this.m_ElemStride,
							size = num * this.m_ElemStride
						};
					}
					uint num2 = this.m_UpdateRangeMin * this.m_ElemStride;
					uint num3 = this.m_UpdateRangeMax * this.m_ElemStride;
					bool flag2 = num2 > 0U;
					if (flag2)
					{
						for (uint num4 = 0U; num4 < this.m_UpdateRangesEnqueued; num4 += 1U)
						{
							int num5 = (int)(num4 + this.m_UpdateRangesBatchStart);
							this.updateRanges[num5] = new GfxUpdateBufferRange
							{
								source = this.updateRanges[num5].source,
								offsetFromWriteStart = this.updateRanges[num5].offsetFromWriteStart - num2,
								size = this.updateRanges[num5].size
							};
						}
					}
					Utility.GPUBuffer<T> gpubuffer = this.gpuData;
					if (gpubuffer != null)
					{
						gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, (int)this.m_UpdateRangesEnqueued), (int)num2, (int)num3);
					}
					this.m_UpdateRangeMin = uint.MaxValue;
					this.m_UpdateRangeMax = 0U;
					this.m_UpdateRangesEnqueued = 0U;
					this.m_UpdateRangesBatchStart += this.m_UpdateRangePoolSize;
					bool flag3 = (ulong)this.m_UpdateRangesBatchStart >= (ulong)((long)this.updateRanges.Length);
					if (flag3)
					{
						this.m_UpdateRangesBatchStart = 0U;
					}
					this.m_UpdateRangesSaturated = false;
				}
			}

			// Token: 0x04000829 RID: 2089
			public Utility.GPUBuffer<T> gpuData;

			// Token: 0x0400082A RID: 2090
			public NativeArray<T> cpuData;

			// Token: 0x0400082B RID: 2091
			public NativeArray<GfxUpdateBufferRange> updateRanges;

			// Token: 0x0400082C RID: 2092
			public GPUBufferAllocator allocator;

			// Token: 0x0400082D RID: 2093
			private readonly uint m_UpdateRangePoolSize;

			// Token: 0x0400082E RID: 2094
			private uint m_ElemStride;

			// Token: 0x0400082F RID: 2095
			private uint m_UpdateRangeMin;

			// Token: 0x04000830 RID: 2096
			private uint m_UpdateRangeMax;

			// Token: 0x04000831 RID: 2097
			private uint m_UpdateRangesEnqueued;

			// Token: 0x04000832 RID: 2098
			private uint m_UpdateRangesBatchStart;

			// Token: 0x04000833 RID: 2099
			private bool m_UpdateRangesSaturated;
		}
	}
}
