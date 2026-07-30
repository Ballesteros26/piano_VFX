using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020001EB RID: 491
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[NativeHeader("Runtime/Camera/RenderLoops/MotionVectorRenderLoop.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	[NativeHeader("Runtime/Shaders/GraphicsCapsScriptBindings.h")]
	public sealed class SystemInfo
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x000233CC File Offset: 0x000215CC
		[NativeProperty]
		public static float batteryLevel
		{
			get
			{
				return SystemInfo.GetBatteryLevel();
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x000233E4 File Offset: 0x000215E4
		public static BatteryStatus batteryStatus
		{
			get
			{
				return SystemInfo.GetBatteryStatus();
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x000233FC File Offset: 0x000215FC
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.GetOperatingSystem();
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00023414 File Offset: 0x00021614
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.GetOperatingSystemFamily();
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0002342C File Offset: 0x0002162C
		public static string processorType
		{
			get
			{
				return SystemInfo.GetProcessorType();
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00023444 File Offset: 0x00021644
		public static int processorFrequency
		{
			get
			{
				return SystemInfo.GetProcessorFrequencyMHz();
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0002345C File Offset: 0x0002165C
		public static int processorCount
		{
			get
			{
				return SystemInfo.GetProcessorCount();
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00023474 File Offset: 0x00021674
		public static int systemMemorySize
		{
			get
			{
				return SystemInfo.GetPhysicalMemoryMB();
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0002348C File Offset: 0x0002168C
		public static string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.GetDeviceUniqueIdentifier();
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x000234A4 File Offset: 0x000216A4
		public static string deviceName
		{
			get
			{
				return SystemInfo.GetDeviceName();
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x000234BC File Offset: 0x000216BC
		public static string deviceModel
		{
			get
			{
				return SystemInfo.GetDeviceModel();
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x000234D4 File Offset: 0x000216D4
		public static bool supportsAccelerometer
		{
			get
			{
				return SystemInfo.SupportsAccelerometer();
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x000234EC File Offset: 0x000216EC
		public static bool supportsGyroscope
		{
			get
			{
				return SystemInfo.IsGyroAvailable();
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00023504 File Offset: 0x00021704
		public static bool supportsLocationService
		{
			get
			{
				return SystemInfo.SupportsLocationService();
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0002351C File Offset: 0x0002171C
		public static bool supportsVibration
		{
			get
			{
				return SystemInfo.SupportsVibration();
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00023534 File Offset: 0x00021734
		public static bool supportsAudio
		{
			get
			{
				return SystemInfo.SupportsAudio();
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0002354C File Offset: 0x0002174C
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.GetDeviceType();
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00023564 File Offset: 0x00021764
		public static int graphicsMemorySize
		{
			get
			{
				return SystemInfo.GetGraphicsMemorySize();
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0002357C File Offset: 0x0002177C
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceName();
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00023594 File Offset: 0x00021794
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendor();
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000235AC File Offset: 0x000217AC
		public static int graphicsDeviceID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceID();
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000235C4 File Offset: 0x000217C4
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendorID();
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000235DC File Offset: 0x000217DC
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceType();
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000235F4 File Offset: 0x000217F4
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.GetGraphicsUVStartsAtTop();
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0002360C File Offset: 0x0002180C
		public static string graphicsDeviceVersion
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVersion();
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00023624 File Offset: 0x00021824
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.GetGraphicsShaderLevel();
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0002363C File Offset: 0x0002183C
		public static bool graphicsMultiThreaded
		{
			get
			{
				return SystemInfo.GetGraphicsMultiThreaded();
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00023654 File Offset: 0x00021854
		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return SystemInfo.GetRenderingThreadingMode();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0002366C File Offset: 0x0002186C
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.HasHiddenSurfaceRemovalOnGPU();
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00023684 File Offset: 0x00021884
		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return SystemInfo.HasDynamicUniformArrayIndexingInFragmentShaders();
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0002369C File Offset: 0x0002189C
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.SupportsShadows();
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x000236B4 File Offset: 0x000218B4
		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return SystemInfo.SupportsRawShadowDepthSampling();
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000236CC File Offset: 0x000218CC
		[Obsolete("supportsRenderTextures always returns true, no need to call it")]
		public static bool supportsRenderTextures
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000236E0 File Offset: 0x000218E0
		public static bool supportsMotionVectors
		{
			get
			{
				return SystemInfo.SupportsMotionVectors();
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000236F8 File Offset: 0x000218F8
		[Obsolete("supportsRenderToCubemap always returns true, no need to call it")]
		public static bool supportsRenderToCubemap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0002370C File Offset: 0x0002190C
		[Obsolete("supportsImageEffects always returns true, no need to call it")]
		public static bool supportsImageEffects
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00023720 File Offset: 0x00021920
		public static bool supports3DTextures
		{
			get
			{
				return SystemInfo.Supports3DTextures();
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00023738 File Offset: 0x00021938
		public static bool supportsCompressed3DTextures
		{
			get
			{
				return SystemInfo.SupportsCompressed3DTextures();
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00023750 File Offset: 0x00021950
		public static bool supports2DArrayTextures
		{
			get
			{
				return SystemInfo.Supports2DArrayTextures();
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00023768 File Offset: 0x00021968
		public static bool supports3DRenderTextures
		{
			get
			{
				return SystemInfo.Supports3DRenderTextures();
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00023780 File Offset: 0x00021980
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return SystemInfo.SupportsCubemapArrayTextures();
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00023798 File Offset: 0x00021998
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.GetCopyTextureSupport();
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x000237B0 File Offset: 0x000219B0
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.SupportsComputeShaders();
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x000237C8 File Offset: 0x000219C8
		public static bool supportsGeometryShaders
		{
			get
			{
				return SystemInfo.SupportsGeometryShaders();
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000237E0 File Offset: 0x000219E0
		public static bool supportsTessellationShaders
		{
			get
			{
				return SystemInfo.SupportsTessellationShaders();
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x000237F8 File Offset: 0x000219F8
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.SupportsInstancing();
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00023810 File Offset: 0x00021A10
		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return SystemInfo.SupportsHardwareQuadTopology();
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00023828 File Offset: 0x00021A28
		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return SystemInfo.Supports32bitsIndexBuffer();
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00023840 File Offset: 0x00021A40
		public static bool supportsSparseTextures
		{
			get
			{
				return SystemInfo.SupportsSparseTextures();
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x00023858 File Offset: 0x00021A58
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.SupportedRenderTargetCount();
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00023870 File Offset: 0x00021A70
		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return SystemInfo.SupportsSeparatedRenderTargetsBlend();
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00023888 File Offset: 0x00021A88
		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return SystemInfo.SupportedRandomWriteTargetCount();
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000238A0 File Offset: 0x00021AA0
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.SupportsMultisampledTextures();
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000238B8 File Offset: 0x00021AB8
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.SupportsMultisampleAutoResolve();
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000238D0 File Offset: 0x00021AD0
		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return SystemInfo.SupportsTextureWrapMirrorOnce();
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000238E8 File Offset: 0x00021AE8
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.UsesReversedZBuffer();
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00023900 File Offset: 0x00021B00
		[Obsolete("supportsStencil always returns true, no need to call it")]
		public static int supportsStencil
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00023914 File Offset: 0x00021B14
		private static bool IsValidEnumValue(Enum value)
		{
			bool flag = !Enum.IsDefined(value.GetType(), value);
			return !flag;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00023940 File Offset: 0x00021B40
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.HasRenderTextureNative(format);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00023978 File Offset: 0x00021B78
		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsBlendingOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.SupportsBlendingOnRenderTextureFormatNative(format);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000239B0 File Offset: 0x00021BB0
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsTextureFormat; format is not a valid TextureFormat");
			}
			return SystemInfo.SupportsTextureFormatNative(format);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000239E8 File Offset: 0x00021BE8
		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; format is not a valid VertexAttributeFormat");
			}
			bool flag2 = dimension < 1 || dimension > 4;
			if (flag2)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; dimension must be in 1..4 range");
			}
			return SystemInfo.SupportsVertexAttributeFormatNative(format, dimension);
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00023A38 File Offset: 0x00021C38
		public static NPOTSupport npotSupport
		{
			get
			{
				return SystemInfo.GetNPOTSupport();
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00023A50 File Offset: 0x00021C50
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.GetMaxTextureSize();
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00023A68 File Offset: 0x00021C68
		public static int maxCubemapSize
		{
			get
			{
				return SystemInfo.GetMaxCubemapSize();
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00023A80 File Offset: 0x00021C80
		internal static int maxRenderTextureSize
		{
			get
			{
				return SystemInfo.GetMaxRenderTextureSize();
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00023A98 File Offset: 0x00021C98
		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsVertex();
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00023AB0 File Offset: 0x00021CB0
		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsFragment();
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00023AC8 File Offset: 0x00021CC8
		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsGeometry();
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x00023AE0 File Offset: 0x00021CE0
		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsDomain();
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00023AF8 File Offset: 0x00021CF8
		public static int maxComputeBufferInputsHull
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsHull();
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00023B10 File Offset: 0x00021D10
		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsCompute();
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00023B28 File Offset: 0x00021D28
		public static int maxComputeWorkGroupSize
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSize();
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00023B40 File Offset: 0x00021D40
		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeX();
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00023B58 File Offset: 0x00021D58
		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeY();
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00023B70 File Offset: 0x00021D70
		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeZ();
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00023B88 File Offset: 0x00021D88
		public static bool supportsAsyncCompute
		{
			get
			{
				return SystemInfo.SupportsAsyncCompute();
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00023BA0 File Offset: 0x00021DA0
		public static bool supportsGpuRecorder
		{
			get
			{
				return SystemInfo.SupportsGpuRecorder();
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00023BB8 File Offset: 0x00021DB8
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.SupportsGPUFence();
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00023BD0 File Offset: 0x00021DD0
		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return SystemInfo.SupportsAsyncGPUReadback();
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00023BE8 File Offset: 0x00021DE8
		public static bool supportsRayTracing
		{
			get
			{
				return SystemInfo.SupportsRayTracing();
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00023C00 File Offset: 0x00021E00
		public static bool supportsSetConstantBuffer
		{
			get
			{
				return SystemInfo.SupportsSetConstantBuffer();
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00023C18 File Offset: 0x00021E18
		public static bool minConstantBufferOffsetAlignment
		{
			get
			{
				return SystemInfo.MinConstantBufferOffsetAlignment();
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00023C30 File Offset: 0x00021E30
		public static bool hasMipMaxLevel
		{
			get
			{
				return SystemInfo.HasMipMaxLevel();
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00023C48 File Offset: 0x00021E48
		public static bool supportsMipStreaming
		{
			get
			{
				return SystemInfo.SupportsMipStreaming();
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00023C60 File Offset: 0x00021E60
		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00023C74 File Offset: 0x00021E74
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.UsesLoadStoreActions();
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00023C8C File Offset: 0x00021E8C
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.GetHDRDisplaySupportFlags();
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00023CA4 File Offset: 0x00021EA4
		public static bool supportsConservativeRaster
		{
			get
			{
				return SystemInfo.SupportsConservativeRaster();
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00023CBC File Offset: 0x00021EBC
		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001575 RID: 5493
		[FreeFunction("systeminfo::GetBatteryLevel")]
		[MethodImpl(4096)]
		private static extern float GetBatteryLevel();

		// Token: 0x06001576 RID: 5494
		[FreeFunction("systeminfo::GetBatteryStatus")]
		[MethodImpl(4096)]
		private static extern BatteryStatus GetBatteryStatus();

		// Token: 0x06001577 RID: 5495
		[FreeFunction("systeminfo::GetOperatingSystem")]
		[MethodImpl(4096)]
		private static extern string GetOperatingSystem();

		// Token: 0x06001578 RID: 5496
		[FreeFunction("systeminfo::GetOperatingSystemFamily")]
		[MethodImpl(4096)]
		private static extern OperatingSystemFamily GetOperatingSystemFamily();

		// Token: 0x06001579 RID: 5497
		[FreeFunction("systeminfo::GetProcessorType")]
		[MethodImpl(4096)]
		private static extern string GetProcessorType();

		// Token: 0x0600157A RID: 5498
		[FreeFunction("systeminfo::GetProcessorFrequencyMHz")]
		[MethodImpl(4096)]
		private static extern int GetProcessorFrequencyMHz();

		// Token: 0x0600157B RID: 5499
		[FreeFunction("systeminfo::GetProcessorCount")]
		[MethodImpl(4096)]
		private static extern int GetProcessorCount();

		// Token: 0x0600157C RID: 5500
		[FreeFunction("systeminfo::GetPhysicalMemoryMB")]
		[MethodImpl(4096)]
		private static extern int GetPhysicalMemoryMB();

		// Token: 0x0600157D RID: 5501
		[FreeFunction("systeminfo::GetDeviceUniqueIdentifier")]
		[MethodImpl(4096)]
		private static extern string GetDeviceUniqueIdentifier();

		// Token: 0x0600157E RID: 5502
		[FreeFunction("systeminfo::GetDeviceName")]
		[MethodImpl(4096)]
		private static extern string GetDeviceName();

		// Token: 0x0600157F RID: 5503
		[FreeFunction("systeminfo::GetDeviceModel")]
		[MethodImpl(4096)]
		private static extern string GetDeviceModel();

		// Token: 0x06001580 RID: 5504
		[FreeFunction("systeminfo::SupportsAccelerometer")]
		[MethodImpl(4096)]
		private static extern bool SupportsAccelerometer();

		// Token: 0x06001581 RID: 5505
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern bool IsGyroAvailable();

		// Token: 0x06001582 RID: 5506
		[FreeFunction("systeminfo::SupportsLocationService")]
		[MethodImpl(4096)]
		private static extern bool SupportsLocationService();

		// Token: 0x06001583 RID: 5507
		[FreeFunction("systeminfo::SupportsVibration")]
		[MethodImpl(4096)]
		private static extern bool SupportsVibration();

		// Token: 0x06001584 RID: 5508
		[FreeFunction("systeminfo::SupportsAudio")]
		[MethodImpl(4096)]
		private static extern bool SupportsAudio();

		// Token: 0x06001585 RID: 5509
		[FreeFunction("systeminfo::GetDeviceType")]
		[MethodImpl(4096)]
		private static extern DeviceType GetDeviceType();

		// Token: 0x06001586 RID: 5510
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMemorySize")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsMemorySize();

		// Token: 0x06001587 RID: 5511
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceName")]
		[MethodImpl(4096)]
		private static extern string GetGraphicsDeviceName();

		// Token: 0x06001588 RID: 5512
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendor")]
		[MethodImpl(4096)]
		private static extern string GetGraphicsDeviceVendor();

		// Token: 0x06001589 RID: 5513
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceID")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsDeviceID();

		// Token: 0x0600158A RID: 5514
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendorID")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsDeviceVendorID();

		// Token: 0x0600158B RID: 5515
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceType")]
		[MethodImpl(4096)]
		private static extern GraphicsDeviceType GetGraphicsDeviceType();

		// Token: 0x0600158C RID: 5516
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsUVStartsAtTop")]
		[MethodImpl(4096)]
		private static extern bool GetGraphicsUVStartsAtTop();

		// Token: 0x0600158D RID: 5517
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVersion")]
		[MethodImpl(4096)]
		private static extern string GetGraphicsDeviceVersion();

		// Token: 0x0600158E RID: 5518
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsShaderLevel")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsShaderLevel();

		// Token: 0x0600158F RID: 5519
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMultiThreaded")]
		[MethodImpl(4096)]
		private static extern bool GetGraphicsMultiThreaded();

		// Token: 0x06001590 RID: 5520
		[FreeFunction("ScriptingGraphicsCaps::GetRenderingThreadingMode")]
		[MethodImpl(4096)]
		private static extern RenderingThreadingMode GetRenderingThreadingMode();

		// Token: 0x06001591 RID: 5521
		[FreeFunction("ScriptingGraphicsCaps::HasHiddenSurfaceRemovalOnGPU")]
		[MethodImpl(4096)]
		private static extern bool HasHiddenSurfaceRemovalOnGPU();

		// Token: 0x06001592 RID: 5522
		[FreeFunction("ScriptingGraphicsCaps::HasDynamicUniformArrayIndexingInFragmentShaders")]
		[MethodImpl(4096)]
		private static extern bool HasDynamicUniformArrayIndexingInFragmentShaders();

		// Token: 0x06001593 RID: 5523
		[FreeFunction("ScriptingGraphicsCaps::SupportsShadows")]
		[MethodImpl(4096)]
		private static extern bool SupportsShadows();

		// Token: 0x06001594 RID: 5524
		[FreeFunction("ScriptingGraphicsCaps::SupportsRawShadowDepthSampling")]
		[MethodImpl(4096)]
		private static extern bool SupportsRawShadowDepthSampling();

		// Token: 0x06001595 RID: 5525
		[FreeFunction("SupportsMotionVectors")]
		[MethodImpl(4096)]
		private static extern bool SupportsMotionVectors();

		// Token: 0x06001596 RID: 5526
		[FreeFunction("ScriptingGraphicsCaps::Supports3DTextures")]
		[MethodImpl(4096)]
		private static extern bool Supports3DTextures();

		// Token: 0x06001597 RID: 5527
		[FreeFunction("ScriptingGraphicsCaps::SupportsCompressed3DTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsCompressed3DTextures();

		// Token: 0x06001598 RID: 5528
		[FreeFunction("ScriptingGraphicsCaps::Supports2DArrayTextures")]
		[MethodImpl(4096)]
		private static extern bool Supports2DArrayTextures();

		// Token: 0x06001599 RID: 5529
		[FreeFunction("ScriptingGraphicsCaps::Supports3DRenderTextures")]
		[MethodImpl(4096)]
		private static extern bool Supports3DRenderTextures();

		// Token: 0x0600159A RID: 5530
		[FreeFunction("ScriptingGraphicsCaps::SupportsCubemapArrayTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsCubemapArrayTextures();

		// Token: 0x0600159B RID: 5531
		[FreeFunction("ScriptingGraphicsCaps::GetCopyTextureSupport")]
		[MethodImpl(4096)]
		private static extern CopyTextureSupport GetCopyTextureSupport();

		// Token: 0x0600159C RID: 5532
		[FreeFunction("ScriptingGraphicsCaps::SupportsComputeShaders")]
		[MethodImpl(4096)]
		private static extern bool SupportsComputeShaders();

		// Token: 0x0600159D RID: 5533
		[FreeFunction("ScriptingGraphicsCaps::SupportsGeometryShaders")]
		[MethodImpl(4096)]
		private static extern bool SupportsGeometryShaders();

		// Token: 0x0600159E RID: 5534
		[FreeFunction("ScriptingGraphicsCaps::SupportsTessellationShaders")]
		[MethodImpl(4096)]
		private static extern bool SupportsTessellationShaders();

		// Token: 0x0600159F RID: 5535
		[FreeFunction("ScriptingGraphicsCaps::SupportsInstancing")]
		[MethodImpl(4096)]
		private static extern bool SupportsInstancing();

		// Token: 0x060015A0 RID: 5536
		[FreeFunction("ScriptingGraphicsCaps::SupportsHardwareQuadTopology")]
		[MethodImpl(4096)]
		private static extern bool SupportsHardwareQuadTopology();

		// Token: 0x060015A1 RID: 5537
		[FreeFunction("ScriptingGraphicsCaps::Supports32bitsIndexBuffer")]
		[MethodImpl(4096)]
		private static extern bool Supports32bitsIndexBuffer();

		// Token: 0x060015A2 RID: 5538
		[FreeFunction("ScriptingGraphicsCaps::SupportsSparseTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsSparseTextures();

		// Token: 0x060015A3 RID: 5539
		[FreeFunction("ScriptingGraphicsCaps::SupportedRenderTargetCount")]
		[MethodImpl(4096)]
		private static extern int SupportedRenderTargetCount();

		// Token: 0x060015A4 RID: 5540
		[FreeFunction("ScriptingGraphicsCaps::SupportsSeparatedRenderTargetsBlend")]
		[MethodImpl(4096)]
		private static extern bool SupportsSeparatedRenderTargetsBlend();

		// Token: 0x060015A5 RID: 5541
		[FreeFunction("ScriptingGraphicsCaps::SupportedRandomWriteTargetCount")]
		[MethodImpl(4096)]
		private static extern int SupportedRandomWriteTargetCount();

		// Token: 0x060015A6 RID: 5542
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsVertex")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsVertex();

		// Token: 0x060015A7 RID: 5543
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsFragment")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsFragment();

		// Token: 0x060015A8 RID: 5544
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsGeometry")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsGeometry();

		// Token: 0x060015A9 RID: 5545
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsDomain")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsDomain();

		// Token: 0x060015AA RID: 5546
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsHull")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsHull();

		// Token: 0x060015AB RID: 5547
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsCompute")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsCompute();

		// Token: 0x060015AC RID: 5548
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledTextures")]
		[MethodImpl(4096)]
		private static extern int SupportsMultisampledTextures();

		// Token: 0x060015AD RID: 5549
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleAutoResolve")]
		[MethodImpl(4096)]
		private static extern bool SupportsMultisampleAutoResolve();

		// Token: 0x060015AE RID: 5550
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureWrapMirrorOnce")]
		[MethodImpl(4096)]
		private static extern int SupportsTextureWrapMirrorOnce();

		// Token: 0x060015AF RID: 5551
		[FreeFunction("ScriptingGraphicsCaps::UsesReversedZBuffer")]
		[MethodImpl(4096)]
		private static extern bool UsesReversedZBuffer();

		// Token: 0x060015B0 RID: 5552
		[FreeFunction("ScriptingGraphicsCaps::HasRenderTexture")]
		[MethodImpl(4096)]
		private static extern bool HasRenderTextureNative(RenderTextureFormat format);

		// Token: 0x060015B1 RID: 5553
		[FreeFunction("ScriptingGraphicsCaps::SupportsBlendingOnRenderTextureFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsBlendingOnRenderTextureFormatNative(RenderTextureFormat format);

		// Token: 0x060015B2 RID: 5554
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsTextureFormatNative(TextureFormat format);

		// Token: 0x060015B3 RID: 5555
		[FreeFunction("ScriptingGraphicsCaps::SupportsVertexAttributeFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsVertexAttributeFormatNative(VertexAttributeFormat format, int dimension);

		// Token: 0x060015B4 RID: 5556
		[FreeFunction("ScriptingGraphicsCaps::GetNPOTSupport")]
		[MethodImpl(4096)]
		private static extern NPOTSupport GetNPOTSupport();

		// Token: 0x060015B5 RID: 5557
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxTextureSize();

		// Token: 0x060015B6 RID: 5558
		[FreeFunction("ScriptingGraphicsCaps::GetMaxCubemapSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxCubemapSize();

		// Token: 0x060015B7 RID: 5559
		[FreeFunction("ScriptingGraphicsCaps::GetMaxRenderTextureSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxRenderTextureSize();

		// Token: 0x060015B8 RID: 5560
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSize();

		// Token: 0x060015B9 RID: 5561
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeX")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSizeX();

		// Token: 0x060015BA RID: 5562
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeY")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSizeY();

		// Token: 0x060015BB RID: 5563
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeZ")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSizeZ();

		// Token: 0x060015BC RID: 5564
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncCompute")]
		[MethodImpl(4096)]
		private static extern bool SupportsAsyncCompute();

		// Token: 0x060015BD RID: 5565
		[FreeFunction("ScriptingGraphicsCaps::SupportsGpuRecorder")]
		[MethodImpl(4096)]
		private static extern bool SupportsGpuRecorder();

		// Token: 0x060015BE RID: 5566
		[FreeFunction("ScriptingGraphicsCaps::SupportsGPUFence")]
		[MethodImpl(4096)]
		private static extern bool SupportsGPUFence();

		// Token: 0x060015BF RID: 5567
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncGPUReadback")]
		[MethodImpl(4096)]
		private static extern bool SupportsAsyncGPUReadback();

		// Token: 0x060015C0 RID: 5568
		[FreeFunction("ScriptingGraphicsCaps::SupportsRayTracing")]
		[MethodImpl(4096)]
		private static extern bool SupportsRayTracing();

		// Token: 0x060015C1 RID: 5569
		[FreeFunction("ScriptingGraphicsCaps::SupportsSetConstantBuffer")]
		[MethodImpl(4096)]
		private static extern bool SupportsSetConstantBuffer();

		// Token: 0x060015C2 RID: 5570
		[FreeFunction("ScriptingGraphicsCaps::MinConstantBufferOffsetAlignment")]
		[MethodImpl(4096)]
		private static extern bool MinConstantBufferOffsetAlignment();

		// Token: 0x060015C3 RID: 5571
		[FreeFunction("ScriptingGraphicsCaps::HasMipMaxLevel")]
		[MethodImpl(4096)]
		private static extern bool HasMipMaxLevel();

		// Token: 0x060015C4 RID: 5572
		[FreeFunction("ScriptingGraphicsCaps::SupportsMipStreaming")]
		[MethodImpl(4096)]
		private static extern bool SupportsMipStreaming();

		// Token: 0x060015C5 RID: 5573
		[FreeFunction("ScriptingGraphicsCaps::IsFormatSupported")]
		[MethodImpl(4096)]
		public static extern bool IsFormatSupported(GraphicsFormat format, FormatUsage usage);

		// Token: 0x060015C6 RID: 5574
		[FreeFunction("ScriptingGraphicsCaps::GetCompatibleFormat")]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage);

		// Token: 0x060015C7 RID: 5575
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsFormat")]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetGraphicsFormat(DefaultFormat format);

		// Token: 0x060015C8 RID: 5576
		[FreeFunction("ScriptingGraphicsCaps::UsesLoadStoreActions")]
		[MethodImpl(4096)]
		private static extern bool UsesLoadStoreActions();

		// Token: 0x060015C9 RID: 5577
		[FreeFunction("ScriptingGraphicsCaps::GetHDRDisplaySupportFlags")]
		[MethodImpl(4096)]
		private static extern HDRDisplaySupportFlags GetHDRDisplaySupportFlags();

		// Token: 0x060015CA RID: 5578
		[FreeFunction("ScriptingGraphicsCaps::SupportsConservativeRaster")]
		[MethodImpl(4096)]
		private static extern bool SupportsConservativeRaster();

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00023CD0 File Offset: 0x00021ED0
		[Obsolete("SystemInfo.supportsGPUFence has been deprecated, use SystemInfo.supportsGraphicsFence instead (UnityUpgradable) ->  supportsGraphicsFence", true)]
		public static bool supportsGPUFence
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006BE RID: 1726
		public const string unsupportedIdentifier = "n/a";
	}
}
