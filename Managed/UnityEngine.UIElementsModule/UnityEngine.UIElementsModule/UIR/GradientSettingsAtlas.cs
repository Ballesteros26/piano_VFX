using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000220 RID: 544
	internal class GradientSettingsAtlas : IDisposable
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0003D564 File Offset: 0x0003B764
		internal int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0003D57C File Offset: 0x0003B77C
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0003D584 File Offset: 0x0003B784
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001072 RID: 4210 RVA: 0x0003D58D File Offset: 0x0003B78D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_Atlas);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003D5D7 File Offset: 0x0003B7D7
		public GradientSettingsAtlas(int length = 4096)
		{
			this.m_Length = length;
			this.m_ElemWidth = 3;
			this.Reset();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003D5F8 File Offset: 0x0003B7F8
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator = new BestFitAllocator((uint)this.m_Length);
				UIRUtility.Destroy(this.m_Atlas);
				this.m_RawAtlas = default(GradientSettingsAtlas.RawTexture);
				this.MustCommit = false;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0003D64C File Offset: 0x0003B84C
		public Texture2D atlas
		{
			get
			{
				return this.m_Atlas;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003D664 File Offset: 0x0003B864
		public Alloc Add(int count)
		{
			Debug.Assert(count > 0);
			bool disposed = this.disposed;
			Alloc alloc;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Alloc);
			}
			else
			{
				Alloc alloc2 = this.m_Allocator.Allocate((uint)count);
				alloc = alloc2;
			}
			return alloc;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003D6AC File Offset: 0x0003B8AC
		public void Remove(Alloc alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		public void Write(Alloc alloc, GradientSettings[] settings, GradientRemap remap)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_RawAtlas.rgba == null;
				if (flag)
				{
					this.m_RawAtlas = new GradientSettingsAtlas.RawTexture
					{
						rgba = new Color32[this.m_ElemWidth * this.m_Length],
						width = this.m_ElemWidth,
						height = this.m_Length
					};
					int num = this.m_ElemWidth * this.m_Length;
					for (int i = 0; i < num; i++)
					{
						this.m_RawAtlas.rgba[i] = Color.black;
					}
				}
				int num2 = (int)alloc.start;
				int j = 0;
				int num3 = settings.Length;
				while (j < num3)
				{
					int num4 = 0;
					GradientSettings gradientSettings = settings[j];
					Debug.Assert(remap == null || num2 == remap.destIndex);
					bool flag2 = gradientSettings.gradientType == GradientType.Radial;
					if (flag2)
					{
						Vector2 vector = gradientSettings.radialFocus;
						vector += Vector2.one;
						vector /= 2f;
						vector.y = 1f - vector.y;
						this.m_RawAtlas.WriteRawFloat4Packed(0.003921569f, (float)gradientSettings.addressMode / 255f, vector.x, vector.y, num4++, num2);
					}
					else
					{
						bool flag3 = gradientSettings.gradientType == GradientType.Linear;
						if (flag3)
						{
							this.m_RawAtlas.WriteRawFloat4Packed(0f, (float)gradientSettings.addressMode / 255f, 0f, 0f, num4++, num2);
						}
					}
					Vector2Int vector2Int = new Vector2Int(gradientSettings.location.x, gradientSettings.location.y);
					Vector2 vector2 = new Vector2((float)(gradientSettings.location.width - 1), (float)(gradientSettings.location.height - 1));
					bool flag4 = remap != null;
					if (flag4)
					{
						vector2Int = new Vector2Int(remap.location.x, remap.location.y);
						vector2 = new Vector2((float)(remap.location.width - 1), (float)(remap.location.height - 1));
					}
					this.m_RawAtlas.WriteRawInt2Packed(vector2Int.x, vector2Int.y, num4++, num2);
					this.m_RawAtlas.WriteRawInt2Packed((int)vector2.x, (int)vector2.y, num4++, num2);
					remap = ((remap != null) ? remap.next : null);
					num2++;
					j++;
				}
				this.MustCommit = true;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0003D99D File Offset: 0x0003BB9D
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0003D9A5 File Offset: 0x0003BBA5
		public bool MustCommit { get; private set; }

		// Token: 0x0600107C RID: 4220 RVA: 0x0003D9B0 File Offset: 0x0003BBB0
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.MustCommit;
				if (!flag)
				{
					this.PrepareAtlas();
					this.m_Atlas.SetPixels32(this.m_RawAtlas.rgba);
					this.m_Atlas.Apply();
					this.MustCommit = false;
				}
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003DA14 File Offset: 0x0003BC14
		private void PrepareAtlas()
		{
			bool flag = this.m_Atlas != null;
			if (!flag)
			{
				this.m_Atlas = new Texture2D(this.m_ElemWidth, this.m_Length, TextureFormat.ARGB32, 0, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "GradientSettings " + Random.Range(int.MinValue, int.MaxValue),
					filterMode = FilterMode.Point
				};
			}
		}

		// Token: 0x0400073F RID: 1855
		private static ProfilerMarker s_MarkerWrite = new ProfilerMarker("UIR.GradientSettingsAtlas.Write");

		// Token: 0x04000740 RID: 1856
		private static ProfilerMarker s_MarkerCommit = new ProfilerMarker("UIR.GradientSettingsAtlas.Commit");

		// Token: 0x04000741 RID: 1857
		private readonly int m_Length;

		// Token: 0x04000742 RID: 1858
		private readonly int m_ElemWidth;

		// Token: 0x04000743 RID: 1859
		private BestFitAllocator m_Allocator;

		// Token: 0x04000744 RID: 1860
		private Texture2D m_Atlas;

		// Token: 0x04000745 RID: 1861
		private GradientSettingsAtlas.RawTexture m_RawAtlas;

		// Token: 0x02000221 RID: 545
		private struct RawTexture
		{
			// Token: 0x0600107F RID: 4223 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
			public void WriteRawInt2Packed(int v0, int v1, int destX, int destY)
			{
				byte b = (byte)(v0 / 255);
				byte b2 = (byte)(v0 - (int)(b * byte.MaxValue));
				byte b3 = (byte)(v1 / 255);
				byte b4 = (byte)(v1 - (int)(b3 * byte.MaxValue));
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, b2, b3, b4);
			}

			// Token: 0x06001080 RID: 4224 RVA: 0x0003DB00 File Offset: 0x0003BD00
			public void WriteRawFloat4Packed(float f0, float f1, float f2, float f3, int destX, int destY)
			{
				byte b = (byte)(f0 * 255f + 0.5f);
				byte b2 = (byte)(f1 * 255f + 0.5f);
				byte b3 = (byte)(f2 * 255f + 0.5f);
				byte b4 = (byte)(f3 * 255f + 0.5f);
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, b2, b3, b4);
			}

			// Token: 0x04000748 RID: 1864
			public Color32[] rgba;

			// Token: 0x04000749 RID: 1865
			public int width;

			// Token: 0x0400074A RID: 1866
			public int height;
		}
	}
}
