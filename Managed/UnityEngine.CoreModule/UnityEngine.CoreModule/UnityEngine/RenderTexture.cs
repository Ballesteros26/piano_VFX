using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000153 RID: 339
	[NativeHeader("Runtime/Graphics/RenderBufferManager.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	public class RenderTexture : Texture
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000EE6 RID: 3814
		// (set) Token: 0x06000EE7 RID: 3815
		public override extern int width
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000EE8 RID: 3816
		// (set) Token: 0x06000EE9 RID: 3817
		public override extern int height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000EEA RID: 3818
		// (set) Token: 0x06000EEB RID: 3819
		public override extern TextureDimension dimension
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000EEC RID: 3820
		// (set) Token: 0x06000EED RID: 3821
		[NativeProperty("ColorFormat")]
		public new extern GraphicsFormat graphicsFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000EEE RID: 3822
		// (set) Token: 0x06000EEF RID: 3823
		[NativeProperty("MipMap")]
		public extern bool useMipMap
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000EF0 RID: 3824
		[NativeProperty("SRGBReadWrite")]
		public extern bool sRGB
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000EF1 RID: 3825
		// (set) Token: 0x06000EF2 RID: 3826
		[NativeProperty("VRUsage")]
		public extern VRTextureUsage vrUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000EF3 RID: 3827
		// (set) Token: 0x06000EF4 RID: 3828
		[NativeProperty("Memoryless")]
		public extern RenderTextureMemoryless memorylessMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x000140D0 File Offset: 0x000122D0
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x000140ED File Offset: 0x000122ED
		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000EF7 RID: 3831
		// (set) Token: 0x06000EF8 RID: 3832
		public extern GraphicsFormat stencilFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000EF9 RID: 3833
		// (set) Token: 0x06000EFA RID: 3834
		public extern bool autoGenerateMips
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000EFB RID: 3835
		// (set) Token: 0x06000EFC RID: 3836
		public extern int volumeDepth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EFD RID: 3837
		// (set) Token: 0x06000EFE RID: 3838
		public extern int antiAliasing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EFF RID: 3839
		// (set) Token: 0x06000F00 RID: 3840
		public extern bool bindTextureMS
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000F01 RID: 3841
		// (set) Token: 0x06000F02 RID: 3842
		public extern bool enableRandomWrite
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000F03 RID: 3843
		// (set) Token: 0x06000F04 RID: 3844
		public extern bool useDynamicScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000F05 RID: 3845
		[MethodImpl(4096)]
		private extern bool GetIsPowerOfTwo();

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00014104 File Offset: 0x00012304
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00002EC3 File Offset: 0x000010C3
		public bool isPowerOfTwo
		{
			get
			{
				return this.GetIsPowerOfTwo();
			}
			set
			{
			}
		}

		// Token: 0x06000F08 RID: 3848
		[FreeFunction("RenderTexture::GetActive")]
		[MethodImpl(4096)]
		private static extern RenderTexture GetActive();

		// Token: 0x06000F09 RID: 3849
		[FreeFunction("RenderTextureScripting::SetActive")]
		[MethodImpl(4096)]
		private static extern void SetActive(RenderTexture rt);

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0001411C File Offset: 0x0001231C
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00014133 File Offset: 0x00012333
		public static RenderTexture active
		{
			get
			{
				return RenderTexture.GetActive();
			}
			set
			{
				RenderTexture.SetActive(value);
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00014140 File Offset: 0x00012340
		[FreeFunction(Name = "RenderTextureScripting::GetColorBuffer", HasExplicitThis = true)]
		private RenderBuffer GetColorBuffer()
		{
			RenderBuffer renderBuffer;
			this.GetColorBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00014158 File Offset: 0x00012358
		[FreeFunction(Name = "RenderTextureScripting::GetDepthBuffer", HasExplicitThis = true)]
		private RenderBuffer GetDepthBuffer()
		{
			RenderBuffer renderBuffer;
			this.GetDepthBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00014170 File Offset: 0x00012370
		public RenderBuffer colorBuffer
		{
			get
			{
				return this.GetColorBuffer();
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00014188 File Offset: 0x00012388
		public RenderBuffer depthBuffer
		{
			get
			{
				return this.GetDepthBuffer();
			}
		}

		// Token: 0x06000F10 RID: 3856
		[MethodImpl(4096)]
		public extern IntPtr GetNativeDepthBufferPtr();

		// Token: 0x06000F11 RID: 3857
		[MethodImpl(4096)]
		public extern void DiscardContents(bool discardColor, bool discardDepth);

		// Token: 0x06000F12 RID: 3858
		[MethodImpl(4096)]
		public extern void MarkRestoreExpected();

		// Token: 0x06000F13 RID: 3859 RVA: 0x000141A0 File Offset: 0x000123A0
		public void DiscardContents()
		{
			this.DiscardContents(true, true);
		}

		// Token: 0x06000F14 RID: 3860
		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(4096)]
		private extern void ResolveAA();

		// Token: 0x06000F15 RID: 3861
		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(4096)]
		private extern void ResolveAATo(RenderTexture rt);

		// Token: 0x06000F16 RID: 3862 RVA: 0x000141AC File Offset: 0x000123AC
		public void ResolveAntiAliasedSurface()
		{
			this.ResolveAA();
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000141B6 File Offset: 0x000123B6
		public void ResolveAntiAliasedSurface(RenderTexture target)
		{
			this.ResolveAATo(target);
		}

		// Token: 0x06000F18 RID: 3864
		[FreeFunction(Name = "RenderTextureScripting::SetGlobalShaderProperty", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalShaderProperty(string propertyName);

		// Token: 0x06000F19 RID: 3865
		[MethodImpl(4096)]
		public extern bool Create();

		// Token: 0x06000F1A RID: 3866
		[MethodImpl(4096)]
		public extern void Release();

		// Token: 0x06000F1B RID: 3867
		[MethodImpl(4096)]
		public extern bool IsCreated();

		// Token: 0x06000F1C RID: 3868
		[MethodImpl(4096)]
		public extern void GenerateMips();

		// Token: 0x06000F1D RID: 3869
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono);

		// Token: 0x06000F1E RID: 3870
		[MethodImpl(4096)]
		internal extern void SetSRGBReadWrite(bool srgb);

		// Token: 0x06000F1F RID: 3871
		[FreeFunction("RenderTextureScripting::Create")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] RenderTexture rt);

		// Token: 0x06000F20 RID: 3872
		[FreeFunction("RenderTextureSupportsStencil")]
		[MethodImpl(4096)]
		public static extern bool SupportsStencil(RenderTexture rt);

		// Token: 0x06000F21 RID: 3873 RVA: 0x000141C1 File Offset: 0x000123C1
		[NativeName("SetRenderTextureDescFromScript")]
		private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
		{
			this.SetRenderTextureDescriptor_Injected(ref desc);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000141CC File Offset: 0x000123CC
		[NativeName("GetRenderTextureDesc")]
		private RenderTextureDescriptor GetDescriptor()
		{
			RenderTextureDescriptor renderTextureDescriptor;
			this.GetDescriptor_Injected(out renderTextureDescriptor);
			return renderTextureDescriptor;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000141E2 File Offset: 0x000123E2
		[FreeFunction("GetRenderBufferManager().GetTextures().GetTempBuffer")]
		private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
		{
			return RenderTexture.GetTemporary_Internal_Injected(ref desc);
		}

		// Token: 0x06000F24 RID: 3876
		[FreeFunction("GetRenderBufferManager().GetTextures().ReleaseTempBuffer")]
		[MethodImpl(4096)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000F25 RID: 3877
		// (set) Token: 0x06000F26 RID: 3878
		public extern int depth
		{
			[FreeFunction("RenderTextureScripting::GetDepth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("RenderTextureScripting::SetDepth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000141EB File Offset: 0x000123EB
		[RequiredByNativeCode]
		protected internal RenderTexture()
		{
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000141F5 File Offset: 0x000123F5
		public RenderTexture(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00014218 File Offset: 0x00012418
		public RenderTexture(RenderTexture textureToCopy)
		{
			bool flag = textureToCopy == null;
			if (flag)
			{
				throw new ArgumentNullException("textureToCopy");
			}
			RenderTexture.ValidateRenderTextureDesc(textureToCopy.descriptor);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(textureToCopy.descriptor);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00014263 File Offset: 0x00012463
		public RenderTexture(int width, int height, int depth, DefaultFormat format)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format))
		{
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00014278 File Offset: 0x00012478
		public RenderTexture(int width, int height, int depth, GraphicsFormat format)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depth = depth;
				this.graphicsFormat = format;
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x000142D8 File Offset: 0x000124D8
		public RenderTexture(int width, int height, int depth, GraphicsFormat format, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depth = depth;
				this.graphicsFormat = format;
				this.descriptor = new RenderTextureDescriptor(width, height, format, depth, mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00014348 File Offset: 0x00012548
		public RenderTexture(int width, int height, int depth, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
			: this(width, height, depth, RenderTexture.GetCompatibleFormat(format, readWrite))
		{
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0001435E File Offset: 0x0001255E
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
			: this(width, height, depth, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default))
		{
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00014373 File Offset: 0x00012573
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth)
			: this(width, height, depth, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default))
		{
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00014387 File Offset: 0x00012587
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, int mipCount)
			: this(width, height, depth, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default), mipCount)
		{
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x000143A0 File Offset: 0x000125A0
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x000143B8 File Offset: 0x000125B8
		public RenderTextureDescriptor descriptor
		{
			get
			{
				return this.GetDescriptor();
			}
			set
			{
				RenderTexture.ValidateRenderTextureDesc(value);
				this.SetRenderTextureDescriptor(value);
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000143CC File Offset: 0x000125CC
		private static void ValidateRenderTextureDesc(RenderTextureDescriptor desc)
		{
			bool flag = !SystemInfo.IsFormatSupported(desc.graphicsFormat, FormatUsage.Render);
			if (flag)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must be a supported GraphicsFormat. " + desc.graphicsFormat + " is not supported.", "desc.graphicsFormat");
			}
			bool flag2 = desc.width <= 0;
			if (flag2)
			{
				throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
			}
			bool flag3 = desc.height <= 0;
			if (flag3)
			{
				throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
			}
			bool flag4 = desc.volumeDepth <= 0;
			if (flag4)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
			}
			bool flag5 = desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8;
			if (flag5)
			{
				throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
			}
			bool flag6 = desc.depthBufferBits != 0 && desc.depthBufferBits != 16 && desc.depthBufferBits != 24;
			if (flag6)
			{
				throw new ArgumentException("RenderTextureDesc depthBufferBits must be 0, 16, or 24.", "desc.depthBufferBits");
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000144F8 File Offset: 0x000126F8
		internal static GraphicsFormat GetCompatibleFormat(RenderTextureFormat renderTextureFormat, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(renderTextureFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			bool flag = graphicsFormat == compatibleFormat;
			GraphicsFormat graphicsFormat2;
			if (flag)
			{
				graphicsFormat2 = graphicsFormat;
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' is not supported. RenderTexture::GetTemporary fallbacks to {1} format on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", graphicsFormat.ToString(), compatibleFormat.ToString()));
				graphicsFormat2 = compatibleFormat;
			}
			return graphicsFormat2;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00014554 File Offset: 0x00012754
		public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			desc.createdFromScript = true;
			return RenderTexture.GetTemporary_Internal(desc);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0001457C File Offset: 0x0001277C
		private static RenderTexture GetTemporaryImpl(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false)
		{
			return RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, format, depthBuffer)
			{
				msaaSamples = antiAliasing,
				memoryless = memorylessMode,
				vrUsage = vrUsage,
				useDynamicScale = useDynamicScale
			});
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000145C8 File Offset: 0x000127C8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000145EC File Offset: 0x000127EC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00014610 File Offset: 0x00012810
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, VRTextureUsage.None, false);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00014634 File Offset: 0x00012834
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00014654 File Offset: 0x00012854
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00014674 File Offset: 0x00012874
		public static RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, GraphicsFormatUtility.GetGraphicsFormat(format, readWrite), antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000146A0 File Offset: 0x000128A0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000146CC File Offset: 0x000128CC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, VRTextureUsage.None, false);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000146F4 File Offset: 0x000128F4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0001471C File Offset: 0x0001291C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00014744 File Offset: 0x00012944
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0001476C File Offset: 0x0001296C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00014794 File Offset: 0x00012994
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			return RenderTexture.GetTemporaryImpl(width, height, 0, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x000147BC File Offset: 0x000129BC
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x000147D7 File Offset: 0x000129D7
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isCubemap
		{
			get
			{
				return this.dimension == TextureDimension.Cube;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Cube : TextureDimension.Tex2D);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x000147E8 File Offset: 0x000129E8
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x00014803 File Offset: 0x00012A03
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isVolume
		{
			get
			{
				return this.dimension == TextureDimension.Tex3D;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Tex3D : TextureDimension.Tex2D);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00014814 File Offset: 0x00012A14
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("RenderTexture.enabled is always now, no need to use it.", false)]
		[EditorBrowsable(1)]
		public static bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00014828 File Offset: 0x00012A28
		[EditorBrowsable(1)]
		[Obsolete("GetTexelOffset always returns zero now, no point in using it.", false)]
		public Vector2 GetTexelOffset()
		{
			return Vector2.zero;
		}

		// Token: 0x06000F4B RID: 3915
		[MethodImpl(4096)]
		private extern void GetColorBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06000F4C RID: 3916
		[MethodImpl(4096)]
		private extern void GetDepthBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06000F4D RID: 3917
		[MethodImpl(4096)]
		private extern void SetRenderTextureDescriptor_Injected(ref RenderTextureDescriptor desc);

		// Token: 0x06000F4E RID: 3918
		[MethodImpl(4096)]
		private extern void GetDescriptor_Injected(out RenderTextureDescriptor ret);

		// Token: 0x06000F4F RID: 3919
		[MethodImpl(4096)]
		private static extern RenderTexture GetTemporary_Internal_Injected(ref RenderTextureDescriptor desc);
	}
}
