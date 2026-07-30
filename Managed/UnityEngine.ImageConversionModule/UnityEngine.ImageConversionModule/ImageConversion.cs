using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/ImageConversion/ScriptBindings/ImageConversion.bindings.h")]
	public static class ImageConversion
	{
		// Token: 0x06000001 RID: 1
		[NativeMethod(Name = "ImageConversionBindings::EncodeToTGA", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeToTGA(this Texture2D tex);

		// Token: 0x06000002 RID: 2
		[NativeMethod(Name = "ImageConversionBindings::EncodeToPNG", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeToPNG(this Texture2D tex);

		// Token: 0x06000003 RID: 3
		[NativeMethod(Name = "ImageConversionBindings::EncodeToJPG", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeToJPG(this Texture2D tex, int quality);

		// Token: 0x06000004 RID: 4 RVA: 0x00002050 File Offset: 0x00000250
		public static byte[] EncodeToJPG(this Texture2D tex)
		{
			return tex.EncodeToJPG(75);
		}

		// Token: 0x06000005 RID: 5
		[NativeMethod(Name = "ImageConversionBindings::EncodeToEXR", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeToEXR(this Texture2D tex, Texture2D.EXRFlags flags);

		// Token: 0x06000006 RID: 6 RVA: 0x0000206C File Offset: 0x0000026C
		public static byte[] EncodeToEXR(this Texture2D tex)
		{
			return tex.EncodeToEXR(Texture2D.EXRFlags.None);
		}

		// Token: 0x06000007 RID: 7
		[NativeMethod(Name = "ImageConversionBindings::LoadImage", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern bool LoadImage([NotNull] this Texture2D tex, byte[] data, bool markNonReadable);

		// Token: 0x06000008 RID: 8 RVA: 0x00002088 File Offset: 0x00000288
		public static bool LoadImage(this Texture2D tex, byte[] data)
		{
			return tex.LoadImage(data, false);
		}

		// Token: 0x06000009 RID: 9
		[FreeFunction("ImageConversionBindings::EncodeArrayToTGA", true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeArrayToTGA(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U);

		// Token: 0x0600000A RID: 10
		[FreeFunction("ImageConversionBindings::EncodeArrayToPNG", true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeArrayToPNG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U);

		// Token: 0x0600000B RID: 11
		[FreeFunction("ImageConversionBindings::EncodeArrayToJPG", true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeArrayToJPG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, int quality = 75);

		// Token: 0x0600000C RID: 12
		[FreeFunction("ImageConversionBindings::EncodeArrayToEXR", true)]
		[MethodImpl(4096)]
		public static extern byte[] EncodeArrayToEXR(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None);
	}
}
