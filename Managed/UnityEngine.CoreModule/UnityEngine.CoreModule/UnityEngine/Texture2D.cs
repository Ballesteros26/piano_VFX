using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014C RID: 332
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h")]
	[NativeHeader("Runtime/Graphics/Texture2D.h")]
	[UsedByNativeCode]
	public sealed class Texture2D : Texture
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000DE5 RID: 3557
		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000DE6 RID: 3558
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D whiteTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000DE7 RID: 3559
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D blackTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000DE8 RID: 3560
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D redTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000DE9 RID: 3561
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D grayTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000DEA RID: 3562
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D linearGrayTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000DEB RID: 3563
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D normalTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000DEC RID: 3564
		[MethodImpl(4096)]
		public extern void Compress(bool highQuality);

		// Token: 0x06000DED RID: 3565
		[FreeFunction("Texture2DScripting::Create")]
		[MethodImpl(4096)]
		private static extern bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x06000DEE RID: 3566 RVA: 0x000127A8 File Offset: 0x000109A8
		private static void Internal_Create([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Texture2D.Internal_CreateImpl(mono, w, h, mipCount, format, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000DEF RID: 3567
		public override extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000DF0 RID: 3568
		[NativeConditional("ENABLE_VIRTUALTEXTURING && UNITY_EDITOR")]
		[NativeName("VTOnly")]
		public extern bool vtOnly
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000DF1 RID: 3569
		[NativeName("Apply")]
		[MethodImpl(4096)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000DF2 RID: 3570
		[NativeName("Resize")]
		[MethodImpl(4096)]
		private extern bool ResizeImpl(int width, int height);

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000127D7 File Offset: 0x000109D7
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int x, int y, Color color)
		{
			this.SetPixelImpl_Injected(image, x, y, ref color);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000127E4 File Offset: 0x000109E4
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int x, int y)
		{
			Color color;
			this.GetPixelImpl_Injected(image, x, y, out color);
			return color;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00012800 File Offset: 0x00010A00
		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, float u, float v)
		{
			Color color;
			this.GetPixelBilinearImpl_Injected(image, u, v, out color);
			return color;
		}

		// Token: 0x06000DF6 RID: 3574
		[FreeFunction(Name = "Texture2DScripting::ResizeWithFormat", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool ResizeWithFormatImpl(int width, int height, GraphicsFormat format, bool hasMipMap);

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00012819 File Offset: 0x00010A19
		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			this.ReadPixelsImpl_Injected(ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06000DF8 RID: 3576
		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame);

		// Token: 0x06000DF9 RID: 3577
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool LoadRawTextureDataImpl(IntPtr data, int size);

		// Token: 0x06000DFA RID: 3578
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool LoadRawTextureDataImplArray(byte[] data);

		// Token: 0x06000DFB RID: 3579
		[FreeFunction(Name = "Texture2DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000DFC RID: 3580
		[FreeFunction(Name = "Texture2DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000DFD RID: 3581
		[MethodImpl(4096)]
		private extern IntPtr GetWritableImageData(int frame);

		// Token: 0x06000DFE RID: 3582
		[MethodImpl(4096)]
		private extern long GetRawImageDataSize();

		// Token: 0x06000DFF RID: 3583
		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		[MethodImpl(4096)]
		private static extern void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect);

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E00 RID: 3584
		internal extern bool isPreProcessed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E01 RID: 3585
		public extern bool streamingMipmaps
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000E02 RID: 3586
		public extern int streamingMipmapsPriority
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000E03 RID: 3587
		// (set) Token: 0x06000E04 RID: 3588
		public extern int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000E05 RID: 3589
		// (set) Token: 0x06000E06 RID: 3590
		public extern int minimumMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetMinimumMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetMinimumMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E07 RID: 3591
		// (set) Token: 0x06000E08 RID: 3592
		internal extern bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E09 RID: 3593
		public extern int calculatedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetCalculatedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E0A RID: 3594
		public extern int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E0B RID: 3595
		public extern int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E0C RID: 3596
		public extern int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000E0D RID: 3597
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ClearRequestedMipmapLevel();

		// Token: 0x06000E0E RID: 3598
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsRequestedMipmapLevelLoaded();

		// Token: 0x06000E0F RID: 3599
		[FreeFunction(Name = "GetTextureStreamingManager().ClearMinimumMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ClearMinimumMipmapLevel();

		// Token: 0x06000E10 RID: 3600
		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		// Token: 0x06000E11 RID: 3601
		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetAllPixels32(Color32[] colors, int miplevel);

		// Token: 0x06000E12 RID: 3602
		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel);

		// Token: 0x06000E13 RID: 3603
		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern byte[] GetRawTextureData();

		// Token: 0x06000E14 RID: 3604
		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, int miplevel);

		// Token: 0x06000E15 RID: 3605 RVA: 0x00012828 File Offset: 0x00010A28
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return this.GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		// Token: 0x06000E16 RID: 3606
		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32(int miplevel);

		// Token: 0x06000E17 RID: 3607 RVA: 0x00012848 File Offset: 0x00010A48
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		// Token: 0x06000E18 RID: 3608
		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		// Token: 0x06000E19 RID: 3609 RVA: 0x00012864 File Offset: 0x00010A64
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return this.PackTextures(textures, padding, maximumAtlasSize, false);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00012880 File Offset: 0x00010A80
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return this.PackTextures(textures, padding, 2048);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000128A0 File Offset: 0x00010AA0
		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, int mipCount, IntPtr nativeTex)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Texture2D.Internal_Create(this, width, height, mipCount, format, flags, nativeTex);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000128D1 File Offset: 0x00010AD1
		public Texture2D(int width, int height, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000128E5 File Offset: 0x00010AE5
		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, format, flags, Texture.GenerateAllMips, IntPtr.Zero)
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000128FE File Offset: 0x00010AFE
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags)
			: this(width, height, format, flags, mipCount, IntPtr.Zero)
		{
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00012914 File Offset: 0x00010B14
		internal Texture2D(int width, int height, TextureFormat textureFormat, int mipCount, bool linear, IntPtr nativeTex)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = ((mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None);
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Texture2D.Internal_Create(this, width, height, mipCount, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0001296D File Offset: 0x00010B6D
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero)
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00012983 File Offset: 0x00010B83
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipChain ? (-1) : 1, linear, IntPtr.Zero)
		{
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0001299F File Offset: 0x00010B9F
		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain)
			: this(width, height, textureFormat, mipChain ? (-1) : 1, false, IntPtr.Zero)
		{
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000129BA File Offset: 0x00010BBA
		public Texture2D(int width, int height)
			: this(width, height, TextureFormat.RGBA32, Texture.GenerateAllMips, false, IntPtr.Zero)
		{
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000129D4 File Offset: 0x00010BD4
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain ? (-1) : 1, linear, nativeTex);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00012A14 File Offset: 0x00010C14
		public void SetPixel(int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, x, y, color);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00012A44 File Offset: 0x00010C44
		public void SetPixel(int x, int y, Color color, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(mipLevel, x, y, color);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00012A74 File Offset: 0x00010C74
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			this.SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00012ABC File Offset: 0x00010CBC
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00012B03 File Offset: 0x00010D03
		public void SetPixels(Color[] colors)
		{
			this.SetPixels(0, 0, this.width, this.height, colors, 0);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00012B20 File Offset: 0x00010D20
		public Color GetPixel(int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, x, y);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00012B50 File Offset: 0x00010D50
		public Color GetPixel(int x, int y, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(mipLevel, x, y);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00012B80 File Offset: 0x00010D80
		public Color GetPixelBilinear(float u, float v)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, u, v);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00012BB0 File Offset: 0x00010DB0
		public Color GetPixelBilinear(float u, float v, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(mipLevel, u, v);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00012BE0 File Offset: 0x00010DE0
		public void LoadRawTextureData(IntPtr data, int size)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImpl(data, size);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00012C44 File Offset: 0x00010E44
		public void LoadRawTextureData(byte[] data)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImplArray(data);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00012CA0 File Offset: 0x00010EA0
		public void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !data.IsCreated || data.Length == 0;
			if (flag2)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			bool flag3 = !this.LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length * UnsafeUtility.SizeOf<T>());
			if (flag3)
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00012D18 File Offset: 0x00010F18
		public void SetPixelData<T>(T[] data, int mipLevel, int sourceDataStartIndex = 0)
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = data == null || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImplArray(data, mipLevel, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00012D88 File Offset: 0x00010F88
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, int sourceDataStartIndex = 0) where T : struct
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = !data.IsCreated || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00012E04 File Offset: 0x00011004
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int pixelDataOffset = base.GetPixelDataOffset(mipLevel, 0);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, 0);
			int num = UnsafeUtility.SizeOf<T>();
			IntPtr intPtr;
			intPtr..ctor(this.GetWritableImageData(0).ToInt64() + (long)pixelDataOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, pixelDataSize / num, Allocator.None);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00012E74 File Offset: 0x00011074
		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetWritableImageData(0), (int)(this.GetRawImageDataSize() / (long)num), Allocator.None);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00012EC0 File Offset: 0x000110C0
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00012EEC File Offset: 0x000110EC
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00012EF8 File Offset: 0x000110F8
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00012F04 File Offset: 0x00011104
		public bool Resize(int width, int height)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ResizeImpl(width, height);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00012F34 File Offset: 0x00011134
		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.ResizeWithFormatImpl(width, height, GraphicsFormatUtility.GetGraphicsFormat(format, base.activeTextureColorSpace == ColorSpace.Linear), hasMipMap);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00012F60 File Offset: 0x00011160
		public bool Resize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ResizeWithFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00012F94 File Offset: 0x00011194
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00012FC3 File Offset: 0x000111C3
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			this.ReadPixels(source, destX, destY, true);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00012FD4 File Offset: 0x000111D4
		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			bool flag = sizes == null;
			if (flag)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentException("results list cannot be null");
			}
			bool flag3 = padding < 0;
			if (flag3)
			{
				throw new ArgumentException("padding can not be negative");
			}
			bool flag4 = atlasSize <= 0;
			if (flag4)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			bool flag5 = sizes.Length == 0;
			bool flag6;
			if (flag5)
			{
				flag6 = true;
			}
			else
			{
				NoAllocHelpers.EnsureListElemCount<Rect>(results, sizes.Length);
				Texture2D.GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromListT<Rect>(results));
				flag6 = results.Count != 0;
			}
			return flag6;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00013070 File Offset: 0x00011270
		public void SetPixels32(Color32[] colors, int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001307C File Offset: 0x0001127C
		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00013088 File Offset: 0x00011288
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001309B File Offset: 0x0001129B
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000130B0 File Offset: 0x000112B0
		public Color[] GetPixels(int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000130FC File Offset: 0x000112FC
		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		// Token: 0x06000E45 RID: 3653
		[MethodImpl(4096)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

		// Token: 0x06000E46 RID: 3654
		[MethodImpl(4096)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);

		// Token: 0x06000E47 RID: 3655
		[MethodImpl(4096)]
		private extern void GetPixelBilinearImpl_Injected(int image, float u, float v, out Color ret);

		// Token: 0x06000E48 RID: 3656
		[MethodImpl(4096)]
		private extern void ReadPixelsImpl_Injected(ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x0200014D RID: 333
		[Flags]
		public enum EXRFlags
		{
			// Token: 0x0400042F RID: 1071
			None = 0,
			// Token: 0x04000430 RID: 1072
			OutputAsFloat = 1,
			// Token: 0x04000431 RID: 1073
			CompressZIP = 2,
			// Token: 0x04000432 RID: 1074
			CompressRLE = 4,
			// Token: 0x04000433 RID: 1075
			CompressPIZ = 8
		}
	}
}
