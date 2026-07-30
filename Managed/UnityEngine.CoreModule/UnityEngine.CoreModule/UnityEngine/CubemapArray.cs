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
	// Token: 0x02000151 RID: 337
	[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h")]
	[ExcludeFromPreset]
	public sealed class CubemapArray : Texture
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000EBD RID: 3773
		public extern int cubemapCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000EBE RID: 3774
		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000EBF RID: 3775
		public override extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000EC0 RID: 3776
		[FreeFunction("CubemapArrayScripting::Create")]
		[MethodImpl(4096)]
		private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureCreationFlags flags);

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00013C98 File Offset: 0x00011E98
		private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = !CubemapArray.Internal_CreateImpl(mono, ext, count, mipCount, format, flags);
			if (flag)
			{
				throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
			}
		}

		// Token: 0x06000EC2 RID: 3778
		[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000EC3 RID: 3779
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00013CC8 File Offset: 0x00011EC8
		public Color[] GetPixels(CubemapFace face, int arrayElement)
		{
			return this.GetPixels(face, arrayElement, 0);
		}

		// Token: 0x06000EC5 RID: 3781
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32(CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00013CE4 File Offset: 0x00011EE4
		public Color32[] GetPixels32(CubemapFace face, int arrayElement)
		{
			return this.GetPixels32(face, arrayElement, 0);
		}

		// Token: 0x06000EC7 RID: 3783
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00013CFF File Offset: 0x00011EFF
		public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels(colors, face, arrayElement, 0);
		}

		// Token: 0x06000EC9 RID: 3785
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x06000ECA RID: 3786 RVA: 0x00013D0D File Offset: 0x00011F0D
		public void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels32(colors, face, arrayElement, 0);
		}

		// Token: 0x06000ECB RID: 3787
		[FreeFunction(Name = "CubemapArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000ECC RID: 3788
		[FreeFunction(Name = "CubemapArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000ECD RID: 3789
		[MethodImpl(4096)]
		private extern IntPtr GetImageDataPointer();

		// Token: 0x06000ECE RID: 3790 RVA: 0x00013D1B File Offset: 0x00011F1B
		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags)
			: this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00013D2F File Offset: 0x00011F2F
		[RequiredByNativeCode]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, cubemapCount, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00013D44 File Offset: 0x00011F44
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, format, flags);
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00013D74 File Offset: 0x00011F74
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, [DefaultValue("true")] bool linear)
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
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, graphicsFormat, textureCreationFlags);
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00013DCB File Offset: 0x00011FCB
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("true")] bool linear)
			: this(width, cubemapCount, textureFormat, mipChain ? (-1) : 1, linear)
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00013DE2 File Offset: 0x00011FE2
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain)
			: this(width, cubemapCount, textureFormat, mipChain ? (-1) : 1, false)
		{
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00013DF8 File Offset: 0x00011FF8
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00013E24 File Offset: 0x00012024
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00013E30 File Offset: 0x00012030
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00013E3C File Offset: 0x0001203C
		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, int element, int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, element, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00013EB0 File Offset: 0x000120B0
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, int element, int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00013F30 File Offset: 0x00012130
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face, int element) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = (int)(element * 6 + face);
			int pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, num);
			int pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, num);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, num);
			int num2 = UnsafeUtility.SizeOf<T>();
			IntPtr intPtr;
			intPtr..ctor(this.GetImageDataPointer().ToInt64() + (long)(pixelDataOffset * num + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, pixelDataSize / num2, Allocator.None);
		}
	}
}
