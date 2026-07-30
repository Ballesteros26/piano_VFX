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
	// Token: 0x02000150 RID: 336
	[NativeHeader("Runtime/Graphics/Texture2DArray.h")]
	public sealed class Texture2DArray : Texture
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E9F RID: 3743
		public static extern int allSlices
		{
			[NativeName("GetAllTextureLayersIdentifier")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000EA0 RID: 3744
		public extern int depth
		{
			[NativeName("GetTextureLayerCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000EA1 RID: 3745
		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000EA2 RID: 3746
		public override extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000EA3 RID: 3747
		[FreeFunction("Texture2DArrayScripting::Create")]
		[MethodImpl(4096)]
		private static extern bool Internal_CreateImpl([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags);

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00013970 File Offset: 0x00011B70
		private static void Internal_Create([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = !Texture2DArray.Internal_CreateImpl(mono, w, h, d, mipCount, format, flags);
			if (flag)
			{
				throw new UnityException("Failed to create 2D array texture because of invalid parameters.");
			}
		}

		// Token: 0x06000EA5 RID: 3749
		[FreeFunction(Name = "Texture2DArrayScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06000EA6 RID: 3750
		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(int arrayElement, int miplevel);

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000139A0 File Offset: 0x00011BA0
		public Color[] GetPixels(int arrayElement)
		{
			return this.GetPixels(arrayElement, 0);
		}

		// Token: 0x06000EA8 RID: 3752
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000EA9 RID: 3753
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06000EAA RID: 3754
		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32(int arrayElement, int miplevel);

		// Token: 0x06000EAB RID: 3755 RVA: 0x000139BC File Offset: 0x00011BBC
		public Color32[] GetPixels32(int arrayElement)
		{
			return this.GetPixels32(arrayElement, 0);
		}

		// Token: 0x06000EAC RID: 3756
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels(Color[] colors, int arrayElement, int miplevel);

		// Token: 0x06000EAD RID: 3757 RVA: 0x000139D6 File Offset: 0x00011BD6
		public void SetPixels(Color[] colors, int arrayElement)
		{
			this.SetPixels(colors, arrayElement, 0);
		}

		// Token: 0x06000EAE RID: 3758
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetPixels32(Color32[] colors, int arrayElement, int miplevel);

		// Token: 0x06000EAF RID: 3759 RVA: 0x000139E3 File Offset: 0x00011BE3
		public void SetPixels32(Color32[] colors, int arrayElement)
		{
			this.SetPixels32(colors, arrayElement, 0);
		}

		// Token: 0x06000EB0 RID: 3760
		[MethodImpl(4096)]
		private extern IntPtr GetImageDataPointer();

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000139F0 File Offset: 0x00011BF0
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00013A06 File Offset: 0x00011C06
		[RequiredByNativeCode]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, depth, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00013A1C File Offset: 0x00011C1C
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			if (flag)
			{
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, format, flags);
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00013A50 File Offset: 0x00011C50
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, [DefaultValue("true")] bool linear)
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
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, graphicsFormat, textureCreationFlags);
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00013AAB File Offset: 0x00011CAB
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("true")] bool linear)
			: this(width, height, depth, textureFormat, mipChain ? (-1) : 1, linear)
		{
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00013AC4 File Offset: 0x00011CC4
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
			: this(width, height, depth, textureFormat, mipChain ? (-1) : 1, false)
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00013ADC File Offset: 0x00011CDC
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00013B08 File Offset: 0x00011D08
		public void SetPixelData<T>(T[] data, int mipLevel, int element, int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, element, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00013B7C File Offset: 0x00011D7C
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, int element, int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00013BFC File Offset: 0x00011DFC
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, int element) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, element);
			int pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, element);
			int pixelDataSize = base.GetPixelDataSize(mipLevel, element);
			int num = UnsafeUtility.SizeOf<T>();
			IntPtr intPtr;
			intPtr..ctor(this.GetImageDataPointer().ToInt64() + (long)(pixelDataOffset * element + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, pixelDataSize / num, Allocator.None);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00013C7E File Offset: 0x00011E7E
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00013C8A File Offset: 0x00011E8A
		public void Apply()
		{
			this.Apply(true, false);
		}
	}
}
