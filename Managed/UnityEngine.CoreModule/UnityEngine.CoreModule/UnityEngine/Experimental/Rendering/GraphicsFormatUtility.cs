using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003DA RID: 986
	[NativeHeader("Runtime/Graphics/TextureFormat.h")]
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/Format.h")]
	public class GraphicsFormatUtility
	{
		// Token: 0x060021E4 RID: 8676
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern GraphicsFormat GetFormat(Texture texture);

		// Token: 0x060021E5 RID: 8677 RVA: 0x00039794 File Offset: 0x00037994
		public static GraphicsFormat GetGraphicsFormat(TextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_TextureFormat(format, isSRGB);
		}

		// Token: 0x060021E6 RID: 8678
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_TextureFormat(TextureFormat format, bool isSRGB);

		// Token: 0x060021E7 RID: 8679 RVA: 0x000397B0 File Offset: 0x000379B0
		public static TextureFormat GetTextureFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.GetTextureFormat_Native_GraphicsFormat(format);
		}

		// Token: 0x060021E8 RID: 8680
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern TextureFormat GetTextureFormat_Native_GraphicsFormat(GraphicsFormat format);

		// Token: 0x060021E9 RID: 8681 RVA: 0x000397C8 File Offset: 0x000379C8
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_RenderTextureFormat(format, isSRGB);
		}

		// Token: 0x060021EA RID: 8682
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_RenderTextureFormat(RenderTextureFormat format, bool isSRGB);

		// Token: 0x060021EB RID: 8683 RVA: 0x000397E4 File Offset: 0x000379E4
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear;
			bool flag2 = ((readWrite == RenderTextureReadWrite.Default) ? flag : (readWrite == RenderTextureReadWrite.sRGB));
			return GraphicsFormatUtility.GetGraphicsFormat(format, flag2);
		}

		// Token: 0x060021EC RID: 8684
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSRGBFormat(GraphicsFormat format);

		// Token: 0x060021ED RID: 8685
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSwizzleFormat(GraphicsFormat format);

		// Token: 0x060021EE RID: 8686
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetSRGBFormat(GraphicsFormat format);

		// Token: 0x060021EF RID: 8687
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetLinearFormat(GraphicsFormat format);

		// Token: 0x060021F0 RID: 8688
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern RenderTextureFormat GetRenderTextureFormat(GraphicsFormat format);

		// Token: 0x060021F1 RID: 8689
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetColorComponentCount(GraphicsFormat format);

		// Token: 0x060021F2 RID: 8690
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetAlphaComponentCount(GraphicsFormat format);

		// Token: 0x060021F3 RID: 8691
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetComponentCount(GraphicsFormat format);

		// Token: 0x060021F4 RID: 8692
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern string GetFormatString(GraphicsFormat format);

		// Token: 0x060021F5 RID: 8693
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsCompressedFormat(GraphicsFormat format);

		// Token: 0x060021F6 RID: 8694
		[FreeFunction("IsAnyCompressedTextureFormat", true)]
		[MethodImpl(4096)]
		internal static extern bool IsCompressedTextureFormat(TextureFormat format);

		// Token: 0x060021F7 RID: 8695
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsPackedFormat(GraphicsFormat format);

		// Token: 0x060021F8 RID: 8696
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool Is16BitPackedFormat(GraphicsFormat format);

		// Token: 0x060021F9 RID: 8697
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern GraphicsFormat ConvertToAlphaFormat(GraphicsFormat format);

		// Token: 0x060021FA RID: 8698
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsAlphaOnlyFormat(GraphicsFormat format);

		// Token: 0x060021FB RID: 8699
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsAlphaTestFormat(GraphicsFormat format);

		// Token: 0x060021FC RID: 8700
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool HasAlphaChannel(GraphicsFormat format);

		// Token: 0x060021FD RID: 8701
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsDepthFormat(GraphicsFormat format);

		// Token: 0x060021FE RID: 8702
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsStencilFormat(GraphicsFormat format);

		// Token: 0x060021FF RID: 8703
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsIEEE754Format(GraphicsFormat format);

		// Token: 0x06002200 RID: 8704
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsFloatFormat(GraphicsFormat format);

		// Token: 0x06002201 RID: 8705
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsHalfFormat(GraphicsFormat format);

		// Token: 0x06002202 RID: 8706
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsUnsignedFormat(GraphicsFormat format);

		// Token: 0x06002203 RID: 8707
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSignedFormat(GraphicsFormat format);

		// Token: 0x06002204 RID: 8708
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsNormFormat(GraphicsFormat format);

		// Token: 0x06002205 RID: 8709
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsUNormFormat(GraphicsFormat format);

		// Token: 0x06002206 RID: 8710
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSNormFormat(GraphicsFormat format);

		// Token: 0x06002207 RID: 8711
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsIntegerFormat(GraphicsFormat format);

		// Token: 0x06002208 RID: 8712
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsUIntFormat(GraphicsFormat format);

		// Token: 0x06002209 RID: 8713
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSIntFormat(GraphicsFormat format);

		// Token: 0x0600220A RID: 8714
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsXRFormat(GraphicsFormat format);

		// Token: 0x0600220B RID: 8715
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsDXTCFormat(GraphicsFormat format);

		// Token: 0x0600220C RID: 8716
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsRGTCFormat(GraphicsFormat format);

		// Token: 0x0600220D RID: 8717
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsBPTCFormat(GraphicsFormat format);

		// Token: 0x0600220E RID: 8718
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsBCFormat(GraphicsFormat format);

		// Token: 0x0600220F RID: 8719
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsPVRTCFormat(GraphicsFormat format);

		// Token: 0x06002210 RID: 8720
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsETCFormat(GraphicsFormat format);

		// Token: 0x06002211 RID: 8721
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsEACFormat(GraphicsFormat format);

		// Token: 0x06002212 RID: 8722
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsASTCFormat(GraphicsFormat format);

		// Token: 0x06002213 RID: 8723 RVA: 0x00039814 File Offset: 0x00037A14
		public static bool IsCrunchFormat(TextureFormat format)
		{
			return format == TextureFormat.DXT1Crunched || format == TextureFormat.DXT5Crunched || format == TextureFormat.ETC_RGB4Crunched || format == TextureFormat.ETC2_RGBA8Crunched;
		}

		// Token: 0x06002214 RID: 8724
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleR(GraphicsFormat format);

		// Token: 0x06002215 RID: 8725
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleG(GraphicsFormat format);

		// Token: 0x06002216 RID: 8726
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleB(GraphicsFormat format);

		// Token: 0x06002217 RID: 8727
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleA(GraphicsFormat format);

		// Token: 0x06002218 RID: 8728
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetBlockSize(GraphicsFormat format);

		// Token: 0x06002219 RID: 8729
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetBlockWidth(GraphicsFormat format);

		// Token: 0x0600221A RID: 8730
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetBlockHeight(GraphicsFormat format);

		// Token: 0x0600221B RID: 8731 RVA: 0x00039840 File Offset: 0x00037A40
		public static uint ComputeMipmapSize(int width, int height, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize_Native_2D(width, height, format);
		}

		// Token: 0x0600221C RID: 8732
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern uint ComputeMipmapSize_Native_2D(int width, int height, GraphicsFormat format);

		// Token: 0x0600221D RID: 8733 RVA: 0x0003985C File Offset: 0x00037A5C
		public static uint ComputeMipmapSize(int width, int height, int depth, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize_Native_3D(width, height, depth, format);
		}

		// Token: 0x0600221E RID: 8734
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern uint ComputeMipmapSize_Native_3D(int width, int height, int depth, GraphicsFormat format);
	}
}
