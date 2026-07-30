using System;
using Unity.Profiling;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000195 RID: 405
	internal class UIRAtlasAllocator : IDisposable
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00029DC8 File Offset: 0x00027FC8
		public int maxAtlasSize { get; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00029DD0 File Offset: 0x00027FD0
		public int maxImageWidth { get; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00029DD8 File Offset: 0x00027FD8
		public int maxImageHeight { get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00029DE0 File Offset: 0x00027FE0
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00029DE8 File Offset: 0x00027FE8
		public int virtualWidth { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00029DF1 File Offset: 0x00027FF1
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00029DF9 File Offset: 0x00027FF9
		public int virtualHeight { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00029E02 File Offset: 0x00028002
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00029E0A File Offset: 0x0002800A
		public int physicalWidth { get; private set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00029E13 File Offset: 0x00028013
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00029E1B File Offset: 0x0002801B
		public int physicalHeight { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00029E24 File Offset: 0x00028024
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00029E2C File Offset: 0x0002802C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06000B46 RID: 2886 RVA: 0x00029E35 File Offset: 0x00028035
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00029E48 File Offset: 0x00028048
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_OpenRows.Length; i++)
					{
						UIRAtlasAllocator.Row row = this.m_OpenRows[i];
						bool flag = row != null;
						if (flag)
						{
							row.Release();
						}
					}
					this.m_OpenRows = null;
					UIRAtlasAllocator.AreaNode next;
					for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = next)
					{
						next = areaNode.next;
						areaNode.Release();
					}
					this.m_FirstUnpartitionedArea = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00029EE0 File Offset: 0x000280E0
		private static int GetLog2OfNextPower(int n)
		{
			float num = (float)Mathf.NextPowerOfTwo(n);
			float num2 = Mathf.Log(num, 2f);
			return Mathf.RoundToInt(num2);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00029F0C File Offset: 0x0002810C
		public UIRAtlasAllocator(int initialAtlasSize, int maxAtlasSize, int sidePadding = 1)
		{
			Assert.IsTrue(initialAtlasSize > 0 && initialAtlasSize <= maxAtlasSize);
			Assert.IsTrue(initialAtlasSize == Mathf.NextPowerOfTwo(initialAtlasSize));
			Assert.IsTrue(maxAtlasSize == Mathf.NextPowerOfTwo(maxAtlasSize));
			this.m_1SidePadding = sidePadding;
			this.m_2SidePadding = sidePadding << 1;
			this.maxAtlasSize = maxAtlasSize;
			this.maxImageWidth = maxAtlasSize;
			this.maxImageHeight = ((initialAtlasSize == maxAtlasSize) ? (maxAtlasSize / 2 + this.m_2SidePadding) : (maxAtlasSize / 4 + this.m_2SidePadding));
			this.virtualWidth = initialAtlasSize;
			this.virtualHeight = initialAtlasSize;
			int num = UIRAtlasAllocator.GetLog2OfNextPower(maxAtlasSize) + 1;
			this.m_OpenRows = new UIRAtlasAllocator.Row[num];
			RectInt rectInt = new RectInt(0, 0, initialAtlasSize, initialAtlasSize);
			this.m_FirstUnpartitionedArea = UIRAtlasAllocator.AreaNode.Acquire(rectInt);
			this.BuildAreas();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00029FD4 File Offset: 0x000281D4
		public bool TryAllocate(int width, int height, out RectInt location)
		{
			bool flag;
			using (UIRAtlasAllocator.s_MarkerTryAllocate.Auto())
			{
				location = default(RectInt);
				bool disposed = this.disposed;
				if (disposed)
				{
					flag = false;
				}
				else
				{
					bool flag2 = width < 1 || height < 1;
					if (flag2)
					{
						flag = false;
					}
					else
					{
						bool flag3 = width > this.maxImageWidth || height > this.maxImageHeight;
						if (flag3)
						{
							flag = false;
						}
						else
						{
							int log2OfNextPower = UIRAtlasAllocator.GetLog2OfNextPower(Mathf.Max(height - this.m_2SidePadding, 1));
							int num = (1 << log2OfNextPower) + this.m_2SidePadding;
							UIRAtlasAllocator.Row row = this.m_OpenRows[log2OfNextPower];
							bool flag4 = row != null && row.width - row.Cursor < width;
							if (flag4)
							{
								row = null;
							}
							bool flag5 = row == null;
							if (flag5)
							{
								for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = areaNode.next)
								{
									bool flag6 = this.TryPartitionArea(areaNode, log2OfNextPower, num, width);
									if (flag6)
									{
										row = this.m_OpenRows[log2OfNextPower];
										break;
									}
								}
								bool flag7 = row == null;
								if (flag7)
								{
									return false;
								}
							}
							location = new RectInt(row.offsetX + row.Cursor, row.offsetY, width, height);
							row.Cursor += width;
							Assert.IsTrue(row.Cursor <= row.width);
							this.physicalWidth = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalWidth, location.xMax));
							this.physicalHeight = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalHeight, location.yMax));
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002A19C File Offset: 0x0002839C
		private bool TryPartitionArea(UIRAtlasAllocator.AreaNode areaNode, int rowIndex, int rowHeight, int minWidth)
		{
			RectInt rect = areaNode.rect;
			bool flag = rect.height < rowHeight || rect.width < minWidth;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
				bool flag3 = row != null;
				if (flag3)
				{
					row.Release();
				}
				row = UIRAtlasAllocator.Row.Acquire(rect.x, rect.y, rect.width, rowHeight);
				this.m_OpenRows[rowIndex] = row;
				rect.y += rowHeight;
				rect.height -= rowHeight;
				bool flag4 = rect.height == 0;
				if (flag4)
				{
					bool flag5 = areaNode == this.m_FirstUnpartitionedArea;
					if (flag5)
					{
						this.m_FirstUnpartitionedArea = areaNode.next;
					}
					areaNode.RemoveFromChain();
					areaNode.Release();
				}
				else
				{
					areaNode.rect = rect;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002A27C File Offset: 0x0002847C
		private void BuildAreas()
		{
			UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea;
			while (this.virtualWidth < this.maxAtlasSize || this.virtualHeight < this.maxAtlasSize)
			{
				bool flag = this.virtualWidth > this.virtualHeight;
				RectInt rectInt;
				if (flag)
				{
					rectInt = new RectInt(0, this.virtualHeight, this.virtualWidth, this.virtualHeight);
					this.virtualHeight *= 2;
				}
				else
				{
					rectInt = new RectInt(this.virtualWidth, 0, this.virtualWidth, this.virtualHeight);
					this.virtualWidth *= 2;
				}
				UIRAtlasAllocator.AreaNode areaNode2 = UIRAtlasAllocator.AreaNode.Acquire(rectInt);
				areaNode2.AddAfter(areaNode);
				areaNode = areaNode2;
			}
		}

		// Token: 0x040004A8 RID: 1192
		private UIRAtlasAllocator.AreaNode m_FirstUnpartitionedArea;

		// Token: 0x040004A9 RID: 1193
		private UIRAtlasAllocator.Row[] m_OpenRows;

		// Token: 0x040004AA RID: 1194
		private int m_1SidePadding;

		// Token: 0x040004AB RID: 1195
		private int m_2SidePadding;

		// Token: 0x040004AC RID: 1196
		private static ProfilerMarker s_MarkerTryAllocate = new ProfilerMarker("UIRAtlasAllocator.TryAllocate");

		// Token: 0x02000196 RID: 406
		private class Row
		{
			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002A349 File Offset: 0x00028549
			// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002A351 File Offset: 0x00028551
			public int offsetX { get; private set; }

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002A35A File Offset: 0x0002855A
			// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0002A362 File Offset: 0x00028562
			public int offsetY { get; private set; }

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002A36B File Offset: 0x0002856B
			// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002A373 File Offset: 0x00028573
			public int width { get; private set; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002A37C File Offset: 0x0002857C
			// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002A384 File Offset: 0x00028584
			public int height { get; private set; }

			// Token: 0x06000B56 RID: 2902 RVA: 0x0002A390 File Offset: 0x00028590
			public static UIRAtlasAllocator.Row Acquire(int offsetX, int offsetY, int width, int height)
			{
				UIRAtlasAllocator.Row row = UIRAtlasAllocator.Row.s_Pool.Get();
				row.offsetX = offsetX;
				row.offsetY = offsetY;
				row.width = width;
				row.height = height;
				row.Cursor = 0;
				return row;
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x0002A3D5 File Offset: 0x000285D5
			public void Release()
			{
				UIRAtlasAllocator.Row.s_Pool.Release(this);
				this.offsetX = -1;
				this.offsetY = -1;
				this.width = -1;
				this.height = -1;
				this.Cursor = -1;
			}

			// Token: 0x040004AE RID: 1198
			private static ObjectPool<UIRAtlasAllocator.Row> s_Pool = new ObjectPool<UIRAtlasAllocator.Row>(100);

			// Token: 0x040004B3 RID: 1203
			public int Cursor;
		}

		// Token: 0x02000197 RID: 407
		private class AreaNode
		{
			// Token: 0x06000B5A RID: 2906 RVA: 0x0002A41C File Offset: 0x0002861C
			public static UIRAtlasAllocator.AreaNode Acquire(RectInt rect)
			{
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.s_Pool.Get();
				areaNode.rect = rect;
				areaNode.previous = null;
				areaNode.next = null;
				return areaNode;
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x0002A44F File Offset: 0x0002864F
			public void Release()
			{
				UIRAtlasAllocator.AreaNode.s_Pool.Release(this);
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x0002A460 File Offset: 0x00028660
			public void RemoveFromChain()
			{
				bool flag = this.previous != null;
				if (flag)
				{
					this.previous.next = this.next;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this.previous;
				}
				this.previous = null;
				this.next = null;
			}

			// Token: 0x06000B5D RID: 2909 RVA: 0x0002A4B8 File Offset: 0x000286B8
			public void AddAfter(UIRAtlasAllocator.AreaNode previous)
			{
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.previous);
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.next);
				this.previous = previous;
				bool flag = previous != null;
				if (flag)
				{
					this.next = previous.next;
					previous.next = this;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this;
				}
			}

			// Token: 0x040004B4 RID: 1204
			private static ObjectPool<UIRAtlasAllocator.AreaNode> s_Pool = new ObjectPool<UIRAtlasAllocator.AreaNode>(100);

			// Token: 0x040004B5 RID: 1205
			public RectInt rect;

			// Token: 0x040004B6 RID: 1206
			public UIRAtlasAllocator.AreaNode previous;

			// Token: 0x040004B7 RID: 1207
			public UIRAtlasAllocator.AreaNode next;
		}
	}
}
