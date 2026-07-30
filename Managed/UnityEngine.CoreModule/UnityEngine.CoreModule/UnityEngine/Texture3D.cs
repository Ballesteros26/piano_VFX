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
	// Token: 0x0200014F RID: 335
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/Texture3D.h")]
	public sealed class Texture3D : Texture
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E77 RID: 3703
		public extern int depth
		{
			[NativeName("GetTextureLayerCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E78 RID: 3704
		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E79 RID: 3705
		public override extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00013504 File Offset: 0x00011704
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int x, int y, int z, Color color)
		{
			this.SetPixelImpl_Injected(image, x, y, z, ref color);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00013514 File Offset: 0x00011714
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int x, int y, int z)
		{
			Color color;
			this.GetPixelImpl_Injected(image, x, y, z, out color);
			return color;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00013530 File Offset: 0x00011730
		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, float u, float v, float w)
		{
			Color color;
			this.GetPixelBilinearImpl_Injected(image, u, v, w, out color);
			return color;
		}

		// Token: 0x06000E7D RID: 3709
		[FreeFunction("Texture3DScripting::Create")]
		[MethodImpl(4096)]
		private static extern bool Internal_CreateImpl([Writable] Texture3D mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags);

		// Token: 0x06000E7E RID: 3710 RVA: 0x0001354C File Offset: 0x0001174C
		private static void Internal_Create([Writable] Texture3D mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = !Texture3D.Internal_CreateImpl(mono, w, h, d, mipCount, format, flags);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x06000E7F RID: 3711
		[FreeFunction(Name = "Texture3DScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000E80 RID: 3712
		[FreeFunction(Name = "Texture3DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(int miplevel);

		// Token: 0x06000E81 RID: 3713 RVA: 0x0001357C File Offset: 0x0001177C
		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		// Token: 0x06000E82 RID: 3714
		[FreeFunction(Name = "Texture3DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32(int miplevel);

		// Token: 0x06000E83 RID: 3715 RVA: 0x00013598 File Offset: 0x00011798
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		// Token: 0x06000E84 RID: 3716
		[FreeFunction(Name = "Texture3DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels(Color[] colors, int miplevel);

		// Token: 0x06000E85 RID: 3717 RVA: 0x000135B1 File Offset: 0x000117B1
		public void SetPixels(Color[] colors)
		{
			this.SetPixels(colors, 0);
		}

		// Token: 0x06000E86 RID: 3718
		[FreeFunction(Name = "Texture3DScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels32(Color32[] colors, int miplevel);

		// Token: 0x06000E87 RID: 3719 RVA: 0x000135BD File Offset: 0x000117BD
		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		// Token: 0x06000E88 RID: 3720
		[FreeFunction(Name = "Texture3DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000E89 RID: 3721
		[FreeFunction(Name = "Texture3DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000E8A RID: 3722
		[MethodImpl(4096)]
		private extern IntPtr GetImageDataPointer();

		// Token: 0x06000E8B RID: 3723 RVA: 0x000135C9 File Offset: 0x000117C9
		public Texture3D(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000135DF File Offset: 0x000117DF
		[RequiredByNativeCode]
		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, depth, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000135F8 File Offset: 0x000117F8
		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Texture3D.Internal_Create(this, width, height, depth, mipCount, format, flags);
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0001362C File Offset: 0x0001182C
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, int mipCount)
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
				Texture3D.Internal_Create(this, width, height, depth, mipCount, graphicsFormat, textureCreationFlags);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00013683 File Offset: 0x00011883
		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
			: this(width, height, depth, textureFormat, mipChain ? (-1) : 1)
		{
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0001369C File Offset: 0x0001189C
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000136C8 File Offset: 0x000118C8
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000136D4 File Offset: 0x000118D4
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000136E0 File Offset: 0x000118E0
		public void SetPixel(int x, int y, int z, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, x, y, z, color);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00013710 File Offset: 0x00011910
		public void SetPixel(int x, int y, int z, Color color, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(mipLevel, x, y, z, color);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00013744 File Offset: 0x00011944
		public Color GetPixel(int x, int y, int z)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, x, y, z);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00013778 File Offset: 0x00011978
		public Color GetPixel(int x, int y, int z, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(mipLevel, x, y, z);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000137AC File Offset: 0x000119AC
		public Color GetPixelBilinear(float u, float v, float w)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, u, v, w);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000137E0 File Offset: 0x000119E0
		public Color GetPixelBilinear(float u, float v, float w, int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(mipLevel, u, v, w);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00013814 File Offset: 0x00011A14
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

		// Token: 0x06000E9A RID: 3738 RVA: 0x00013884 File Offset: 0x00011A84
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

		// Token: 0x06000E9B RID: 3739 RVA: 0x00013900 File Offset: 0x00011B00
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
			intPtr..ctor(this.GetImageDataPointer().ToInt64() + (long)pixelDataOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, pixelDataSize / num, Allocator.None);
		}

		// Token: 0x06000E9C RID: 3740
		[MethodImpl(4096)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, int z, ref Color color);

		// Token: 0x06000E9D RID: 3741
		[MethodImpl(4096)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, int z, out Color ret);

		// Token: 0x06000E9E RID: 3742
		[MethodImpl(4096)]
		private extern void GetPixelBilinearImpl_Injected(int image, float u, float v, float w, out Color ret);
	}
}
