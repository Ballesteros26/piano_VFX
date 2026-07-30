using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034D RID: 845
	[NativeHeader("Runtime/Camera/GraphicsSettings.h")]
	[StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	public sealed class GraphicsSettings : Object
	{
		// Token: 0x06001B3A RID: 6970 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private GraphicsSettings()
		{
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001B3B RID: 6971
		// (set) Token: 0x06001B3C RID: 6972
		public static extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0002CCD1 File Offset: 0x0002AED1
		public static Vector3 transparencySortAxis
		{
			get
			{
				Vector3 vector;
				GraphicsSettings.get_transparencySortAxis_Injected(out vector);
				return vector;
			}
			set
			{
				GraphicsSettings.set_transparencySortAxis_Injected(ref value);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001B3F RID: 6975
		// (set) Token: 0x06001B40 RID: 6976
		public static extern bool realtimeDirectRectangularAreaLights
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001B41 RID: 6977
		// (set) Token: 0x06001B42 RID: 6978
		public static extern bool lightsUseLinearIntensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001B43 RID: 6979
		// (set) Token: 0x06001B44 RID: 6980
		public static extern bool lightsUseColorTemperature
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001B45 RID: 6981
		// (set) Token: 0x06001B46 RID: 6982
		public static extern bool useScriptableRenderPipelineBatching
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001B47 RID: 6983
		// (set) Token: 0x06001B48 RID: 6984
		public static extern bool logWhenShaderIsCompiled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001B49 RID: 6985
		public static extern VideoShadersIncludeMode videoShadersIncludeMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001B4A RID: 6986
		[MethodImpl(4096)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		// Token: 0x06001B4B RID: 6987 RVA: 0x0002CCDC File Offset: 0x0002AEDC
		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return GraphicsSettings.HasShaderDefine(Graphics.activeTier, defineHash);
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001B4C RID: 6988
		[NativeName("CurrentRenderPipeline")]
		private static extern ScriptableObject INTERNAL_currentRenderPipeline
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		public static RenderPipelineAsset currentRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline as RenderPipelineAsset;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0002CD18 File Offset: 0x0002AF18
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x0002CD2F File Offset: 0x0002AF2F
		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return GraphicsSettings.defaultRenderPipeline;
			}
			set
			{
				GraphicsSettings.defaultRenderPipeline = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001B50 RID: 6992
		// (set) Token: 0x06001B51 RID: 6993
		[NativeName("DefaultRenderPipeline")]
		private static extern ScriptableObject INTERNAL_defaultRenderPipeline
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0002CD3C File Offset: 0x0002AF3C
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0002CD58 File Offset: 0x0002AF58
		public static RenderPipelineAsset defaultRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_defaultRenderPipeline as RenderPipelineAsset;
			}
			set
			{
				GraphicsSettings.INTERNAL_defaultRenderPipeline = value;
			}
		}

		// Token: 0x06001B54 RID: 6996
		[NativeName("GetAllConfiguredRenderPipelinesForScript")]
		[MethodImpl(4096)]
		private static extern ScriptableObject[] GetAllConfiguredRenderPipelines();

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0002CD64 File Offset: 0x0002AF64
		public static RenderPipelineAsset[] allConfiguredRenderPipelines
		{
			get
			{
				return Enumerable.ToArray<RenderPipelineAsset>(Enumerable.Cast<RenderPipelineAsset>(GraphicsSettings.GetAllConfiguredRenderPipelines()));
			}
		}

		// Token: 0x06001B56 RID: 6998
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern Object GetGraphicsSettings();

		// Token: 0x06001B57 RID: 6999
		[NativeName("SetShaderModeScript")]
		[MethodImpl(4096)]
		public static extern void SetShaderMode(BuiltinShaderType type, BuiltinShaderMode mode);

		// Token: 0x06001B58 RID: 7000
		[NativeName("GetShaderModeScript")]
		[MethodImpl(4096)]
		public static extern BuiltinShaderMode GetShaderMode(BuiltinShaderType type);

		// Token: 0x06001B59 RID: 7001
		[NativeName("SetCustomShaderScript")]
		[MethodImpl(4096)]
		public static extern void SetCustomShader(BuiltinShaderType type, Shader shader);

		// Token: 0x06001B5A RID: 7002
		[NativeName("GetCustomShaderScript")]
		[MethodImpl(4096)]
		public static extern Shader GetCustomShader(BuiltinShaderType type);

		// Token: 0x06001B5B RID: 7003
		[MethodImpl(4096)]
		private static extern void get_transparencySortAxis_Injected(out Vector3 ret);

		// Token: 0x06001B5C RID: 7004
		[MethodImpl(4096)]
		private static extern void set_transparencySortAxis_Injected(ref Vector3 value);
	}
}
