using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000103 RID: 259
	[NativeHeader("Runtime/Export/Graphics/Light.bindings.h")]
	[NativeHeader("Runtime/Camera/Light.h")]
	[RequireComponent(typeof(Transform))]
	[RequireComponent(typeof(Transform))]
	public sealed class Light : Behaviour
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B54 RID: 2900
		// (set) Token: 0x06000B55 RID: 2901
		[NativeProperty("LightType")]
		public extern LightType type
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B56 RID: 2902
		// (set) Token: 0x06000B57 RID: 2903
		[NativeProperty("LightShape")]
		public extern LightShape shape
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B58 RID: 2904
		// (set) Token: 0x06000B59 RID: 2905
		public extern float spotAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B5A RID: 2906
		// (set) Token: 0x06000B5B RID: 2907
		public extern float innerSpotAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0000F3BA File Offset: 0x0000D5BA
		public Color color
		{
			get
			{
				Color color;
				this.get_color_Injected(out color);
				return color;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B5E RID: 2910
		// (set) Token: 0x06000B5F RID: 2911
		public extern float colorTemperature
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B60 RID: 2912
		// (set) Token: 0x06000B61 RID: 2913
		public extern bool useColorTemperature
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B62 RID: 2914
		// (set) Token: 0x06000B63 RID: 2915
		public extern float intensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B64 RID: 2916
		// (set) Token: 0x06000B65 RID: 2917
		public extern float bounceIntensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B66 RID: 2918
		// (set) Token: 0x06000B67 RID: 2919
		public extern bool useBoundingSphereOverride
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0000F3DA File Offset: 0x0000D5DA
		public Vector4 boundingSphereOverride
		{
			get
			{
				Vector4 vector;
				this.get_boundingSphereOverride_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_boundingSphereOverride_Injected(ref value);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B6A RID: 2922
		// (set) Token: 0x06000B6B RID: 2923
		public extern int shadowCustomResolution
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B6C RID: 2924
		// (set) Token: 0x06000B6D RID: 2925
		public extern float shadowBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B6E RID: 2926
		// (set) Token: 0x06000B6F RID: 2927
		public extern float shadowNormalBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B70 RID: 2928
		// (set) Token: 0x06000B71 RID: 2929
		public extern float shadowNearPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B72 RID: 2930
		// (set) Token: 0x06000B73 RID: 2931
		public extern bool useShadowMatrixOverride
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0000F3FA File Offset: 0x0000D5FA
		public Matrix4x4 shadowMatrixOverride
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_shadowMatrixOverride_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_shadowMatrixOverride_Injected(ref value);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B76 RID: 2934
		// (set) Token: 0x06000B77 RID: 2935
		public extern float range
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000B78 RID: 2936
		// (set) Token: 0x06000B79 RID: 2937
		public extern Flare flare
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0000F404 File Offset: 0x0000D604
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0000F41A File Offset: 0x0000D61A
		public LightBakingOutput bakingOutput
		{
			get
			{
				LightBakingOutput lightBakingOutput;
				this.get_bakingOutput_Injected(out lightBakingOutput);
				return lightBakingOutput;
			}
			set
			{
				this.set_bakingOutput_Injected(ref value);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B7C RID: 2940
		// (set) Token: 0x06000B7D RID: 2941
		public extern int cullingMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B7E RID: 2942
		// (set) Token: 0x06000B7F RID: 2943
		public extern int renderingLayerMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000B80 RID: 2944
		// (set) Token: 0x06000B81 RID: 2945
		public extern LightShadowCasterMode lightShadowCasterMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000B82 RID: 2946
		[MethodImpl(4096)]
		public extern void Reset();

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000B83 RID: 2947
		// (set) Token: 0x06000B84 RID: 2948
		public extern LightShadows shadows
		{
			[NativeMethod("GetShadowType")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Light_Bindings::SetShadowType", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B85 RID: 2949
		// (set) Token: 0x06000B86 RID: 2950
		public extern float shadowStrength
		{
			[MethodImpl(4096)]
			get;
			[FreeFunction("Light_Bindings::SetShadowStrength", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B87 RID: 2951
		// (set) Token: 0x06000B88 RID: 2952
		public extern LightShadowResolution shadowResolution
		{
			[MethodImpl(4096)]
			get;
			[FreeFunction("Light_Bindings::SetShadowResolution", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0000F424 File Offset: 0x0000D624
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		[EditorBrowsable(1)]
		public float shadowSoftness
		{
			get
			{
				return 4f;
			}
			set
			{
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0000F43C File Offset: 0x0000D63C
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		[EditorBrowsable(1)]
		public float shadowSoftnessFade
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B8D RID: 2957
		// (set) Token: 0x06000B8E RID: 2958
		public extern float[] layerShadowCullDistances
		{
			[FreeFunction("Light_Bindings::GetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = false)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Light_Bindings::SetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B8F RID: 2959
		// (set) Token: 0x06000B90 RID: 2960
		public extern float cookieSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B91 RID: 2961
		// (set) Token: 0x06000B92 RID: 2962
		public extern Texture cookie
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B93 RID: 2963
		// (set) Token: 0x06000B94 RID: 2964
		public extern LightRenderMode renderMode
		{
			[MethodImpl(4096)]
			get;
			[FreeFunction("Light_Bindings::SetRenderMode", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0000F454 File Offset: 0x0000D654
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0000F46C File Offset: 0x0000D66C
		[Obsolete("warning bakedIndex has been removed please use bakingOutput.isBaked instead.", true)]
		[EditorBrowsable(1)]
		public int bakedIndex
		{
			get
			{
				return this.m_BakedIndex;
			}
			set
			{
				this.m_BakedIndex = value;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0000F476 File Offset: 0x0000D676
		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			this.AddCommandBuffer(evt, buffer, ShadowMapPass.All);
		}

		// Token: 0x06000B98 RID: 2968
		[FreeFunction("Light_Bindings::AddCommandBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask);

		// Token: 0x06000B99 RID: 2969 RVA: 0x0000F487 File Offset: 0x0000D687
		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			this.AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
		}

		// Token: 0x06000B9A RID: 2970
		[FreeFunction("Light_Bindings::AddCommandBufferAsync", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

		// Token: 0x06000B9B RID: 2971
		[MethodImpl(4096)]
		public extern void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer);

		// Token: 0x06000B9C RID: 2972
		[MethodImpl(4096)]
		public extern void RemoveCommandBuffers(LightEvent evt);

		// Token: 0x06000B9D RID: 2973
		[MethodImpl(4096)]
		public extern void RemoveAllCommandBuffers();

		// Token: 0x06000B9E RID: 2974
		[FreeFunction("Light_Bindings::GetCommandBuffers", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern CommandBuffer[] GetCommandBuffers(LightEvent evt);

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B9F RID: 2975
		public extern int commandBufferCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0000F49C File Offset: 0x0000D69C
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0000F4B3 File Offset: 0x0000D6B3
		[Obsolete("Use QualitySettings.pixelLightCount instead.")]
		public static int pixelLightCount
		{
			get
			{
				return QualitySettings.pixelLightCount;
			}
			set
			{
				QualitySettings.pixelLightCount = value;
			}
		}

		// Token: 0x06000BA2 RID: 2978
		[FreeFunction("Light_Bindings::GetLights")]
		[MethodImpl(4096)]
		public static extern Light[] GetLights(LightType type, int layer);

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06000BAA RID: 2986
		[MethodImpl(4096)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06000BAB RID: 2987
		[MethodImpl(4096)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x06000BAC RID: 2988
		[MethodImpl(4096)]
		private extern void get_boundingSphereOverride_Injected(out Vector4 ret);

		// Token: 0x06000BAD RID: 2989
		[MethodImpl(4096)]
		private extern void set_boundingSphereOverride_Injected(ref Vector4 value);

		// Token: 0x06000BAE RID: 2990
		[MethodImpl(4096)]
		private extern void get_shadowMatrixOverride_Injected(out Matrix4x4 ret);

		// Token: 0x06000BAF RID: 2991
		[MethodImpl(4096)]
		private extern void set_shadowMatrixOverride_Injected(ref Matrix4x4 value);

		// Token: 0x06000BB0 RID: 2992
		[MethodImpl(4096)]
		private extern void get_bakingOutput_Injected(out LightBakingOutput ret);

		// Token: 0x06000BB1 RID: 2993
		[MethodImpl(4096)]
		private extern void set_bakingOutput_Injected(ref LightBakingOutput value);

		// Token: 0x040002B9 RID: 697
		private int m_BakedIndex;
	}
}
