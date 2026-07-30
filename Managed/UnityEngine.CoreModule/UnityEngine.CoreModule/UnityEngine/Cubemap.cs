using System;
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
	// Token: 0x0200014E RID: 334
	[NativeHeader("Runtime/Graphics/CubemapTexture.h")]
	[ExcludeFromPreset]
	public sealed class Cubemap : Texture
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E49 RID: 3657
		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000E4A RID: 3658
		[FreeFunction("CubemapScripting::Create")]
		[MethodImpl(4096)]
		private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x06000E4B RID: 3659 RVA: 0x00013118 File Offset: 0x00011318
		private static void Internal_Create([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Cubemap.Internal_CreateImpl(mono, ext, mipCount, format, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x06000E4C RID: 3660
		[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000E4D RID: 3661
		[FreeFunction("CubemapScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateExternalTexture(IntPtr nativeTexture);

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E4E RID: 3662
		public override extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00013145 File Offset: 0x00011345
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int x, int y, Color color)
		{
			this.SetPixelImpl_Injected(image, x, y, ref color);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00013154 File Offset: 0x00011354
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int x, int y)
		{
			Color color;
			this.GetPixelImpl_Injected(image, x, y, out color);
			return color;
		}

		// Token: 0x06000E51 RID: 3665
		[NativeName("FixupEdges")]
		[MethodImpl(4096)]
		public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

		// Token: 0x06000E52 RID: 3666 RVA: 0x0001316D File Offset: 0x0001136D
		public void SmoothEdges()
		{
			this.SmoothEdges(1);
		}

		// Token: 0x06000E53 RID: 3667
		[FreeFunction(Name = "CubemapScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(CubemapFace face, int miplevel);

		// Token: 0x06000E54 RID: 3668 RVA: 0x00013178 File Offset: 0x00011378
		public Color[] GetPixels(CubemapFace face)
		{
			return this.GetPixels(face, 0);
		}

		// Token: 0x06000E55 RID: 3669
		[FreeFunction(Name = "CubemapScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int miplevel);

		// Token: 0x06000E56 RID: 3670
		[FreeFunction(Name = "CubemapScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000E57 RID: 3671
		[FreeFunction(Name = "CubemapScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000E58 RID: 3672 RVA: 0x00013192 File Offset: 0x00011392
		public void SetPixels(Color[] colors, CubemapFace face)
		{
			this.SetPixels(colors, face, 0);
		}

		// Token: 0x06000E59 RID: 3673
		[MethodImpl(4096)]
		private extern IntPtr GetWritableImageData(int frame);

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E5A RID: 3674
		public extern bool streamingMipmaps
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E5B RID: 3675
		public extern int streamingMipmapsPriority
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E5C RID: 3676
		// (set) Token: 0x06000E5D RID: 3677
		public extern int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E5E RID: 3678
		// (set) Token: 0x06000E5F RID: 3679
		internal extern bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E60 RID: 3680
		public extern int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E61 RID: 3681
		public extern int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E62 RID: 3682
		public extern int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000E63 RID: 3683
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ClearRequestedMipmapLevel();

		// Token: 0x06000E64 RID: 3684
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsRequestedMipmapLevelLoaded();

		// Token: 0x06000E65 RID: 3685 RVA: 0x0001319F File Offset: 0x0001139F
		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags)
			: this(width, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000131B4 File Offset: 0x000113B4
		[RequiredByNativeCode]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Cubemap.Internal_Create(this, width, Texture.GenerateAllMips, format, flags, IntPtr.Zero);
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000131E9 File Offset: 0x000113E9
		public Cubemap(int width, TextureFormat format, int mipCount)
			: this(width, format, mipCount, IntPtr.Zero)
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000131FC File Offset: 0x000113FC
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Cubemap.Internal_Create(this, width, mipCount, format, flags, IntPtr.Zero);
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00013230 File Offset: 0x00011430
		internal Cubemap(int width, TextureFormat textureFormat, int mipCount, IntPtr nativeTex)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, false);
				TextureCreationFlags textureCreationFlags = ((mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None);
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Cubemap.Internal_Create(this, width, mipCount, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00013282 File Offset: 0x00011482
		internal Cubemap(int width, TextureFormat textureFormat, bool mipChain, IntPtr nativeTex)
			: this(width, textureFormat, mipChain ? (-1) : 1, nativeTex)
		{
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00013297 File Offset: 0x00011497
		public Cubemap(int width, TextureFormat textureFormat, bool mipChain)
			: this(width, textureFormat, mipChain ? (-1) : 1, IntPtr.Zero)
		{
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000132B0 File Offset: 0x000114B0
		public static Cubemap CreateExternalTexture(int width, TextureFormat format, bool mipmap, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Cubemap(width, format, mipmap, nativeTex);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000132E8 File Offset: 0x000114E8
		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0001335C File Offset: 0x0001155C
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000133DC File Offset: 0x000115DC
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, (int)face);
			int pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, (int)face);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, (int)face);
			int num = UnsafeUtility.SizeOf<T>();
			IntPtr intPtr;
			intPtr..ctor(this.GetWritableImageData(0).ToInt64() + (long)(pixelDataOffset * (int)face + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, pixelDataSize / num, Allocator.None);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00013460 File Offset: 0x00011660
		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl((int)face, x, y, color);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00013490 File Offset: 0x00011690
		public Color GetPixel(CubemapFace face, int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl((int)face, x, y);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000134C0 File Offset: 0x000116C0
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000134EC File Offset: 0x000116EC
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000134F8 File Offset: 0x000116F8
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000E75 RID: 3701
		[MethodImpl(4096)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

		// Token: 0x06000E76 RID: 3702
		[MethodImpl(4096)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);
	}
}
