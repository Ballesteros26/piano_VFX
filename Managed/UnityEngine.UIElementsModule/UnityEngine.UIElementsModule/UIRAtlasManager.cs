using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000198 RID: 408
	internal class UIRAtlasManager : IDisposable
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000B60 RID: 2912 RVA: 0x0002A52C File Offset: 0x0002872C
		// (remove) Token: 0x06000B61 RID: 2913 RVA: 0x0002A560 File Offset: 0x00028760
		[field: DebuggerBrowsable(0)]
		public static event Action<UIRAtlasManager> atlasManagerCreated;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000B62 RID: 2914 RVA: 0x0002A594 File Offset: 0x00028794
		// (remove) Token: 0x06000B63 RID: 2915 RVA: 0x0002A5C8 File Offset: 0x000287C8
		[field: DebuggerBrowsable(0)]
		public static event Action<UIRAtlasManager> atlasManagerDisposed;

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002A5FC File Offset: 0x000287FC
		public static UIRAtlasManager.ReadOnlyList<UIRAtlasManager> Instances()
		{
			return UIRAtlasManager.s_InstancesreadOnly;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002A613 File Offset: 0x00028813
		public int maxImageSize { get; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002A61B File Offset: 0x0002881B
		public RenderTextureFormat format { get; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002A623 File Offset: 0x00028823
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x0002A62B File Offset: 0x0002882B
		public RenderTexture atlas { get; private set; }

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002A634 File Offset: 0x00028834
		public UIRAtlasManager(RenderTextureFormat format = RenderTextureFormat.ARGB32, FilterMode filterMode = FilterMode.Bilinear, int maxImageSize = 64, int initialSize = 64)
		{
			bool flag = filterMode != FilterMode.Bilinear && filterMode > FilterMode.Point;
			if (flag)
			{
				throw new NotSupportedException("The only supported atlas filter modes are point or bilinear");
			}
			this.format = format;
			this.maxImageSize = maxImageSize;
			this.m_FloatFormat = format == RenderTextureFormat.ARGBFloat;
			this.m_FilterMode = filterMode;
			this.m_UVs = new Dictionary<Texture2D, RectInt>(64);
			this.m_Blitter = new TextureBlitter(64);
			this.m_InitialSize = initialSize;
			this.m_2SidePadding = ((filterMode == FilterMode.Point) ? 0 : 2);
			this.m_1SidePadding = ((filterMode == FilterMode.Point) ? 0 : 1);
			this.Reset();
			UIRAtlasManager.s_Instances.Add(this);
			bool flag2 = UIRAtlasManager.atlasManagerCreated != null;
			if (flag2)
			{
				UIRAtlasManager.atlasManagerCreated.Invoke(this);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002A6F5 File Offset: 0x000288F5
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0002A6FD File Offset: 0x000288FD
		private protected bool disposed { protected get; private set; }

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002A706 File Offset: 0x00028906
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002A718 File Offset: 0x00028918
		protected virtual void Dispose(bool disposing)
		{
			UIRAtlasManager.s_Instances.Remove(this);
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.atlas);
					this.atlas = null;
					bool flag = this.m_Allocator != null;
					if (flag)
					{
						this.m_Allocator.Dispose();
						this.m_Allocator = null;
					}
					bool flag2 = this.m_Blitter != null;
					if (flag2)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
					bool flag3 = UIRAtlasManager.atlasManagerDisposed != null;
					if (flag3)
					{
						UIRAtlasManager.atlasManagerDisposed.Invoke(this);
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002A7C4 File Offset: 0x000289C4
		private static void LogDisposeError()
		{
			Debug.LogError("An attempt to use a disposed atlas manager has been detected.");
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002A7D2 File Offset: 0x000289D2
		public static void MarkAllForReset()
		{
			UIRAtlasManager.s_GlobalResetVersion++;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002A7E1 File Offset: 0x000289E1
		public void MarkForReset()
		{
			this.m_ResetVersion = UIRAtlasManager.s_GlobalResetVersion - 1;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002A7F4 File Offset: 0x000289F4
		public bool RequiresReset()
		{
			return this.m_ResetVersion != UIRAtlasManager.s_GlobalResetVersion;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002A818 File Offset: 0x00028A18
		public bool IsReleased()
		{
			return this.atlas != null && !this.atlas.IsCreated();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002A84C File Offset: 0x00028A4C
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				UIRAtlasManager.LogDisposeError();
			}
			else
			{
				this.m_Blitter.Reset();
				this.m_UVs.Clear();
				this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, 4096, this.m_1SidePadding);
				this.m_ForceReblitAll = false;
				this.m_ColorSpace = QualitySettings.activeColorSpace;
				UIRUtility.Destroy(this.atlas);
				this.m_ResetVersion = UIRAtlasManager.s_GlobalResetVersion;
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002A8CC File Offset: 0x00028ACC
		public bool TryGetLocation(Texture2D image, out RectInt uvs)
		{
			uvs = default(RectInt);
			bool disposed = this.disposed;
			bool flag;
			if (disposed)
			{
				UIRAtlasManager.LogDisposeError();
				flag = false;
			}
			else
			{
				bool flag2 = image == null;
				if (flag2)
				{
					flag = false;
				}
				else
				{
					bool flag3 = this.m_UVs.TryGetValue(image, ref uvs);
					if (flag3)
					{
						flag = true;
					}
					else
					{
						bool flag4 = !this.IsTextureValid(image);
						if (flag4)
						{
							flag = false;
						}
						else
						{
							bool flag5 = !this.AllocateRect(image.width, image.height, out uvs);
							if (flag5)
							{
								flag = false;
							}
							else
							{
								this.m_UVs[image] = uvs;
								this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(uvs.x, uvs.y), true, Color.white);
								flag = true;
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		public bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool flag = !this.m_Allocator.TryAllocate(width + this.m_2SidePadding, height + this.m_2SidePadding, out uvs);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				uvs = new RectInt(uvs.x + this.m_1SidePadding, uvs.y + this.m_1SidePadding, width, height);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002AA09 File Offset: 0x00028C09
		public void EnqueueBlit(Texture image, int x, int y, bool addBorder, Color tint)
		{
			this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(x, y), addBorder, tint);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002AA38 File Offset: 0x00028C38
		public static bool IsTextureFormatSupported(TextureFormat format)
		{
			switch (format)
			{
			case TextureFormat.Alpha8:
			case TextureFormat.ARGB4444:
			case TextureFormat.RGB24:
			case TextureFormat.RGBA32:
			case TextureFormat.ARGB32:
			case TextureFormat.RGB565:
			case TextureFormat.R16:
			case TextureFormat.DXT1:
			case TextureFormat.DXT5:
			case TextureFormat.RGBA4444:
			case TextureFormat.BGRA32:
			case TextureFormat.BC7:
			case TextureFormat.BC4:
			case TextureFormat.BC5:
			case TextureFormat.DXT1Crunched:
			case TextureFormat.DXT5Crunched:
			case TextureFormat.PVRTC_RGB2:
			case TextureFormat.PVRTC_RGBA2:
			case TextureFormat.PVRTC_RGB4:
			case TextureFormat.PVRTC_RGBA4:
			case TextureFormat.ETC_RGB4:
			case TextureFormat.EAC_R:
			case TextureFormat.EAC_R_SIGNED:
			case TextureFormat.EAC_RG:
			case TextureFormat.EAC_RG_SIGNED:
			case TextureFormat.ETC2_RGB:
			case TextureFormat.ETC2_RGBA1:
			case TextureFormat.ETC2_RGBA8:
			case TextureFormat.ASTC_4x4:
			case TextureFormat.ASTC_5x5:
			case TextureFormat.ASTC_6x6:
			case TextureFormat.ASTC_8x8:
			case TextureFormat.ASTC_10x10:
			case TextureFormat.ASTC_12x12:
			case TextureFormat.ASTC_RGBA_4x4:
			case TextureFormat.ASTC_RGBA_5x5:
			case TextureFormat.ASTC_RGBA_6x6:
			case TextureFormat.ASTC_RGBA_8x8:
			case TextureFormat.ASTC_RGBA_10x10:
			case TextureFormat.ASTC_RGBA_12x12:
			case TextureFormat.ETC_RGB4_3DS:
			case TextureFormat.ETC_RGBA8_3DS:
			case TextureFormat.RG16:
			case TextureFormat.R8:
			case TextureFormat.ETC_RGB4Crunched:
			case TextureFormat.ETC2_RGBA8Crunched:
				return true;
			case TextureFormat.RHalf:
			case TextureFormat.RGHalf:
			case TextureFormat.RGBAHalf:
			case TextureFormat.RFloat:
			case TextureFormat.RGFloat:
			case TextureFormat.RGBAFloat:
			case TextureFormat.YUY2:
			case TextureFormat.RGB9e5Float:
			case TextureFormat.BC6H:
			case TextureFormat.ASTC_HDR_4x4:
			case TextureFormat.ASTC_HDR_5x5:
			case TextureFormat.ASTC_HDR_6x6:
			case TextureFormat.ASTC_HDR_8x8:
			case TextureFormat.ASTC_HDR_10x10:
			case TextureFormat.ASTC_HDR_12x12:
				return false;
			}
			return false;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002AB7C File Offset: 0x00028D7C
		private bool IsTextureValid(Texture2D image)
		{
			bool isReadable = image.isReadable;
			bool flag;
			if (isReadable)
			{
				flag = false;
			}
			else
			{
				bool flag2 = image.width > this.maxImageSize || image.height > this.maxImageSize;
				if (flag2)
				{
					flag = false;
				}
				else
				{
					bool flag3 = !UIRAtlasManager.IsTextureFormatSupported(image.format);
					if (flag3)
					{
						flag = false;
					}
					else
					{
						bool flag4 = !this.m_FloatFormat && this.m_ColorSpace == ColorSpace.Linear && image.activeTextureColorSpace > ColorSpace.Gamma;
						if (flag4)
						{
							flag = false;
						}
						else
						{
							bool flag5 = SystemInfo.graphicsShaderLevel >= 35;
							if (flag5)
							{
								bool flag6 = image.filterMode != FilterMode.Bilinear && image.filterMode > FilterMode.Point;
								if (flag6)
								{
									return false;
								}
							}
							else
							{
								bool flag7 = this.m_FilterMode != image.filterMode;
								if (flag7)
								{
									return false;
								}
							}
							bool flag8 = image.wrapMode != TextureWrapMode.Clamp;
							flag = !flag8;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002AC74 File Offset: 0x00028E74
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				UIRAtlasManager.LogDisposeError();
			}
			else
			{
				this.UpdateAtlasTexture();
				bool forceReblitAll = this.m_ForceReblitAll;
				if (forceReblitAll)
				{
					this.m_ForceReblitAll = false;
					this.m_Blitter.Reset();
					foreach (KeyValuePair<Texture2D, RectInt> keyValuePair in this.m_UVs)
					{
						this.m_Blitter.QueueBlit(keyValuePair.Key, new RectInt(0, 0, keyValuePair.Key.width, keyValuePair.Key.height), new Vector2Int(keyValuePair.Value.x, keyValuePair.Value.y), true, Color.white);
					}
				}
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002AD74 File Offset: 0x00028F74
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				bool flag2 = this.m_UVs.Count > this.m_Blitter.queueLength;
				if (flag2)
				{
					this.m_ForceReblitAll = true;
				}
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag3 = this.atlas.width != this.m_Allocator.physicalWidth || this.atlas.height != this.m_Allocator.physicalHeight;
				if (flag3)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					this.m_Blitter.BlitOneNow(renderTexture, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					UIRUtility.Destroy(this.atlas);
					this.atlas = renderTexture;
				}
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002AE60 File Offset: 0x00029060
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_Allocator.physicalWidth == 0 || this.m_Allocator.physicalHeight == 0;
			RenderTexture renderTexture;
			if (flag)
			{
				renderTexture = null;
			}
			else
			{
				renderTexture = new RenderTexture(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Atlas " + Random.Range(int.MinValue, int.MaxValue),
					filterMode = this.m_FilterMode
				};
			}
			return renderTexture;
		}

		// Token: 0x040004BA RID: 1210
		private static List<UIRAtlasManager> s_Instances = new List<UIRAtlasManager>();

		// Token: 0x040004BB RID: 1211
		private static UIRAtlasManager.ReadOnlyList<UIRAtlasManager> s_InstancesreadOnly = new UIRAtlasManager.ReadOnlyList<UIRAtlasManager>(UIRAtlasManager.s_Instances);

		// Token: 0x040004BC RID: 1212
		private int m_InitialSize;

		// Token: 0x040004BD RID: 1213
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x040004BE RID: 1214
		private Dictionary<Texture2D, RectInt> m_UVs;

		// Token: 0x040004BF RID: 1215
		private bool m_ForceReblitAll;

		// Token: 0x040004C0 RID: 1216
		private bool m_FloatFormat;

		// Token: 0x040004C1 RID: 1217
		private FilterMode m_FilterMode;

		// Token: 0x040004C2 RID: 1218
		private ColorSpace m_ColorSpace;

		// Token: 0x040004C3 RID: 1219
		private TextureBlitter m_Blitter;

		// Token: 0x040004C4 RID: 1220
		private int m_2SidePadding;

		// Token: 0x040004C5 RID: 1221
		private int m_1SidePadding;

		// Token: 0x040004C6 RID: 1222
		private static ProfilerMarker s_MarkerReset = new ProfilerMarker("UIR.AtlasManager.Reset");

		// Token: 0x040004CB RID: 1227
		private static int s_GlobalResetVersion;

		// Token: 0x040004CC RID: 1228
		private int m_ResetVersion = UIRAtlasManager.s_GlobalResetVersion;

		// Token: 0x02000199 RID: 409
		public struct ReadOnlyList<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000B7D RID: 2941 RVA: 0x0002AF1F File Offset: 0x0002911F
			public ReadOnlyList(List<T> list)
			{
				this.m_List = list;
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x0002AF2C File Offset: 0x0002912C
			public IEnumerator<T> GetEnumerator()
			{
				return this.m_List.GetEnumerator();
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0002AF50 File Offset: 0x00029150
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_List.GetEnumerator();
			}

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0002AF72 File Offset: 0x00029172
			public int Count
			{
				get
				{
					return this.m_List.Count;
				}
			}

			// Token: 0x170002D9 RID: 729
			public T this[int i]
			{
				get
				{
					return this.m_List[i];
				}
			}

			// Token: 0x040004CD RID: 1229
			private List<T> m_List;
		}
	}
}
