using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000F6 RID: 246
	[NativeHeader("Runtime/Graphics/Renderer.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[UsedByNativeCode]
	public class Renderer : Component
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
		[Obsolete("Use shadowCastingMode instead.", false)]
		[EditorBrowsable(1)]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.On : ShadowCastingMode.Off);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000D30F File Offset: 0x0000B50F
		[Obsolete("Use motionVectorGenerationMode instead.", false)]
		public bool motionVectors
		{
			get
			{
				return this.motionVectorGenerationMode == MotionVectorGenerationMode.Object;
			}
			set
			{
				this.motionVectorGenerationMode = (value ? MotionVectorGenerationMode.Object : MotionVectorGenerationMode.Camera);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0000D320 File Offset: 0x0000B520
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0000D33B File Offset: 0x0000B53B
		[Obsolete("Use lightProbeUsage instead.", false)]
		public bool useLightProbes
		{
			get
			{
				return this.lightProbeUsage > LightProbeUsage.Off;
			}
			set
			{
				this.lightProbeUsage = (value ? LightProbeUsage.BlendProbes : LightProbeUsage.Off);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0000D34C File Offset: 0x0000B54C
		public Bounds bounds
		{
			[FreeFunction(Name = "RendererScripting::GetBounds", HasExplicitThis = true)]
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000D362 File Offset: 0x0000B562
		[FreeFunction(Name = "RendererScripting::SetStaticLightmapST", HasExplicitThis = true)]
		private void SetStaticLightmapST(Vector4 st)
		{
			this.SetStaticLightmapST_Injected(ref st);
		}

		// Token: 0x06000926 RID: 2342
		[FreeFunction(Name = "RendererScripting::GetMaterial", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Material GetMaterial();

		// Token: 0x06000927 RID: 2343
		[FreeFunction(Name = "RendererScripting::GetSharedMaterial", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Material GetSharedMaterial();

		// Token: 0x06000928 RID: 2344
		[FreeFunction(Name = "RendererScripting::SetMaterial", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetMaterial(Material m);

		// Token: 0x06000929 RID: 2345
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Material[] GetMaterialArray();

		// Token: 0x0600092A RID: 2346
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void CopyMaterialArray([Out] Material[] m);

		// Token: 0x0600092B RID: 2347
		[FreeFunction(Name = "RendererScripting::GetSharedMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void CopySharedMaterialArray([Out] Material[] m);

		// Token: 0x0600092C RID: 2348
		[FreeFunction(Name = "RendererScripting::SetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetMaterialArray([NotNull] Material[] m);

		// Token: 0x0600092D RID: 2349
		[FreeFunction(Name = "RendererScripting::SetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_SetPropertyBlock(MaterialPropertyBlock properties);

		// Token: 0x0600092E RID: 2350
		[FreeFunction(Name = "RendererScripting::GetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_GetPropertyBlock([NotNull] MaterialPropertyBlock dest);

		// Token: 0x0600092F RID: 2351
		[FreeFunction(Name = "RendererScripting::SetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_SetPropertyBlockMaterialIndex(MaterialPropertyBlock properties, int materialIndex);

		// Token: 0x06000930 RID: 2352
		[FreeFunction(Name = "RendererScripting::GetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_GetPropertyBlockMaterialIndex([NotNull] MaterialPropertyBlock dest, int materialIndex);

		// Token: 0x06000931 RID: 2353
		[FreeFunction(Name = "RendererScripting::HasPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasPropertyBlock();

		// Token: 0x06000932 RID: 2354 RVA: 0x0000D36C File Offset: 0x0000B56C
		public void SetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_SetPropertyBlock(properties);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000D377 File Offset: 0x0000B577
		public void SetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_SetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000D383 File Offset: 0x0000B583
		public void GetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_GetPropertyBlock(properties);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000D38E File Offset: 0x0000B58E
		public void GetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_GetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x06000936 RID: 2358
		[FreeFunction(Name = "RendererScripting::GetClosestReflectionProbes", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetClosestReflectionProbesInternal(object result);

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000937 RID: 2359
		// (set) Token: 0x06000938 RID: 2360
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000939 RID: 2361
		public extern bool isVisible
		{
			[NativeName("IsVisibleInScene")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600093A RID: 2362
		// (set) Token: 0x0600093B RID: 2363
		public extern ShadowCastingMode shadowCastingMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600093C RID: 2364
		// (set) Token: 0x0600093D RID: 2365
		public extern bool receiveShadows
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600093E RID: 2366
		// (set) Token: 0x0600093F RID: 2367
		public extern bool forceRenderingOff
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000940 RID: 2368
		// (set) Token: 0x06000941 RID: 2369
		public extern MotionVectorGenerationMode motionVectorGenerationMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000942 RID: 2370
		// (set) Token: 0x06000943 RID: 2371
		public extern LightProbeUsage lightProbeUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000944 RID: 2372
		// (set) Token: 0x06000945 RID: 2373
		public extern ReflectionProbeUsage reflectionProbeUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000946 RID: 2374
		// (set) Token: 0x06000947 RID: 2375
		public extern uint renderingLayerMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000948 RID: 2376
		// (set) Token: 0x06000949 RID: 2377
		public extern int rendererPriority
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600094A RID: 2378
		// (set) Token: 0x0600094B RID: 2379
		public extern RayTracingMode rayTracingMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600094C RID: 2380
		// (set) Token: 0x0600094D RID: 2381
		public extern string sortingLayerName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600094E RID: 2382
		// (set) Token: 0x0600094F RID: 2383
		public extern int sortingLayerID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000950 RID: 2384
		// (set) Token: 0x06000951 RID: 2385
		public extern int sortingOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000952 RID: 2386
		// (set) Token: 0x06000953 RID: 2387
		internal extern int sortingGroupID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000954 RID: 2388
		// (set) Token: 0x06000955 RID: 2389
		internal extern int sortingGroupOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000956 RID: 2390
		// (set) Token: 0x06000957 RID: 2391
		[NativeProperty("IsDynamicOccludee")]
		public extern bool allowOcclusionWhenDynamic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000958 RID: 2392
		// (set) Token: 0x06000959 RID: 2393
		[NativeProperty("StaticBatchRoot")]
		internal extern Transform staticBatchRootTransform
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600095A RID: 2394
		internal extern int staticBatchIndex
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600095B RID: 2395
		[MethodImpl(4096)]
		internal extern void SetStaticBatchInfo(int firstSubMesh, int subMeshCount);

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600095C RID: 2396
		public extern bool isPartOfStaticBatch
		{
			[NativeName("IsPartOfStaticBatch")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0000D39C File Offset: 0x0000B59C
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_worldToLocalMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_localToWorldMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600095F RID: 2399
		// (set) Token: 0x06000960 RID: 2400
		public extern GameObject lightProbeProxyVolumeOverride
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000961 RID: 2401
		// (set) Token: 0x06000962 RID: 2402
		public extern Transform probeAnchor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000963 RID: 2403
		[NativeName("GetLightmapIndexInt")]
		[MethodImpl(4096)]
		private extern int GetLightmapIndex(LightmapType lt);

		// Token: 0x06000964 RID: 2404
		[NativeName("SetLightmapIndexInt")]
		[MethodImpl(4096)]
		private extern void SetLightmapIndex(int index, LightmapType lt);

		// Token: 0x06000965 RID: 2405 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		[NativeName("GetLightmapST")]
		private Vector4 GetLightmapST(LightmapType lt)
		{
			Vector4 vector;
			this.GetLightmapST_Injected(lt, out vector);
			return vector;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
		[NativeName("SetLightmapST")]
		private void SetLightmapST(Vector4 st, LightmapType lt)
		{
			this.SetLightmapST_Injected(ref st, lt);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0000D409 File Offset: 0x0000B609
		public int lightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.StaticLightmap);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0000D418 File Offset: 0x0000B618
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0000D431 File Offset: 0x0000B631
		public int realtimeLightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.DynamicLightmap);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0000D440 File Offset: 0x0000B640
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0000D459 File Offset: 0x0000B659
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetStaticLightmapST(value);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0000D464 File Offset: 0x0000B664
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0000D47D File Offset: 0x0000B67D
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapST(value, LightmapType.DynamicLightmap);
			}
		}

		// Token: 0x0600096F RID: 2415
		[MethodImpl(4096)]
		private extern int GetMaterialCount();

		// Token: 0x06000970 RID: 2416
		[NativeName("GetMaterialArray")]
		[MethodImpl(4096)]
		private extern Material[] GetSharedMaterialArray();

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0000D48C File Offset: 0x0000B68C
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public Material[] materials
		{
			get
			{
				return this.GetMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public Material material
		{
			get
			{
				return this.GetMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public Material sharedMaterial
		{
			get
			{
				return this.GetSharedMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public Material[] sharedMaterials
		{
			get
			{
				return this.GetSharedMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000D504 File Offset: 0x0000B704
		public void GetMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopyMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000D544 File Offset: 0x0000B744
		public void GetSharedMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopySharedMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000D584 File Offset: 0x0000B784
		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			this.GetClosestReflectionProbesInternal(result);
		}

		// Token: 0x0600097D RID: 2429
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x0600097E RID: 2430
		[MethodImpl(4096)]
		private extern void SetStaticLightmapST_Injected(ref Vector4 st);

		// Token: 0x0600097F RID: 2431
		[MethodImpl(4096)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000980 RID: 2432
		[MethodImpl(4096)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000981 RID: 2433
		[MethodImpl(4096)]
		private extern void GetLightmapST_Injected(LightmapType lt, out Vector4 ret);

		// Token: 0x06000982 RID: 2434
		[MethodImpl(4096)]
		private extern void SetLightmapST_Injected(ref Vector4 st, LightmapType lt);
	}
}
