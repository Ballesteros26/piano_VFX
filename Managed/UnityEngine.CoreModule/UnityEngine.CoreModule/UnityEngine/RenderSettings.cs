using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000F7 RID: 247
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	[NativeHeader("Runtime/Graphics/QualitySettingsTypes.h")]
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public sealed class RenderSettings : Object
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0000D598 File Offset: 0x0000B798
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0000D5AF File Offset: 0x0000B7AF
		[Obsolete("Use RenderSettings.ambientIntensity instead (UnityUpgradable) -> ambientIntensity", false)]
		public static float ambientSkyboxAmount
		{
			get
			{
				return RenderSettings.ambientIntensity;
			}
			set
			{
				RenderSettings.ambientIntensity = value;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private RenderSettings()
		{
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000986 RID: 2438
		// (set) Token: 0x06000987 RID: 2439
		[NativeProperty("UseFog")]
		public static extern bool fog
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000988 RID: 2440
		// (set) Token: 0x06000989 RID: 2441
		[NativeProperty("LinearFogStart")]
		public static extern float fogStartDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600098A RID: 2442
		// (set) Token: 0x0600098B RID: 2443
		[NativeProperty("LinearFogEnd")]
		public static extern float fogEndDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600098C RID: 2444
		// (set) Token: 0x0600098D RID: 2445
		public static extern FogMode fogMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x0000D5D1 File Offset: 0x0000B7D1
		public static Color fogColor
		{
			get
			{
				Color color;
				RenderSettings.get_fogColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_fogColor_Injected(ref value);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000990 RID: 2448
		// (set) Token: 0x06000991 RID: 2449
		public static extern float fogDensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000992 RID: 2450
		// (set) Token: 0x06000993 RID: 2451
		public static extern AmbientMode ambientMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0000D5F1 File Offset: 0x0000B7F1
		public static Color ambientSkyColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientSkyColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientSkyColor_Injected(ref value);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0000D611 File Offset: 0x0000B811
		public static Color ambientEquatorColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientEquatorColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientEquatorColor_Injected(ref value);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0000D61C File Offset: 0x0000B81C
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0000D631 File Offset: 0x0000B831
		public static Color ambientGroundColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientGroundColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientGroundColor_Injected(ref value);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600099A RID: 2458
		// (set) Token: 0x0600099B RID: 2459
		public static extern float ambientIntensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0000D63C File Offset: 0x0000B83C
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0000D651 File Offset: 0x0000B851
		[NativeProperty("AmbientSkyColor")]
		public static Color ambientLight
		{
			get
			{
				Color color;
				RenderSettings.get_ambientLight_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientLight_Injected(ref value);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0000D65C File Offset: 0x0000B85C
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0000D671 File Offset: 0x0000B871
		public static Color subtractiveShadowColor
		{
			get
			{
				Color color;
				RenderSettings.get_subtractiveShadowColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_subtractiveShadowColor_Injected(ref value);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009A0 RID: 2464
		// (set) Token: 0x060009A1 RID: 2465
		[NativeProperty("SkyboxMaterial")]
		public static extern Material skybox
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009A2 RID: 2466
		// (set) Token: 0x060009A3 RID: 2467
		public static extern Light sun
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0000D67C File Offset: 0x0000B87C
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0000D691 File Offset: 0x0000B891
		public static SphericalHarmonicsL2 ambientProbe
		{
			get
			{
				SphericalHarmonicsL2 sphericalHarmonicsL;
				RenderSettings.get_ambientProbe_Injected(out sphericalHarmonicsL);
				return sphericalHarmonicsL;
			}
			set
			{
				RenderSettings.set_ambientProbe_Injected(ref value);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009A6 RID: 2470
		// (set) Token: 0x060009A7 RID: 2471
		public static extern Cubemap customReflection
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009A8 RID: 2472
		// (set) Token: 0x060009A9 RID: 2473
		public static extern float reflectionIntensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009AA RID: 2474
		// (set) Token: 0x060009AB RID: 2475
		public static extern int reflectionBounces
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009AC RID: 2476
		// (set) Token: 0x060009AD RID: 2477
		public static extern DefaultReflectionMode defaultReflectionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009AE RID: 2478
		// (set) Token: 0x060009AF RID: 2479
		public static extern int defaultReflectionResolution
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009B0 RID: 2480
		// (set) Token: 0x060009B1 RID: 2481
		public static extern float haloStrength
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009B2 RID: 2482
		// (set) Token: 0x060009B3 RID: 2483
		public static extern float flareStrength
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009B4 RID: 2484
		// (set) Token: 0x060009B5 RID: 2485
		public static extern float flareFadeSpeed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060009B6 RID: 2486
		[FreeFunction("GetRenderSettings")]
		[MethodImpl(4096)]
		internal static extern Object GetRenderSettings();

		// Token: 0x060009B7 RID: 2487
		[StaticAccessor("RenderSettingsScripting", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		internal static extern void Reset();

		// Token: 0x060009B8 RID: 2488
		[MethodImpl(4096)]
		private static extern void get_fogColor_Injected(out Color ret);

		// Token: 0x060009B9 RID: 2489
		[MethodImpl(4096)]
		private static extern void set_fogColor_Injected(ref Color value);

		// Token: 0x060009BA RID: 2490
		[MethodImpl(4096)]
		private static extern void get_ambientSkyColor_Injected(out Color ret);

		// Token: 0x060009BB RID: 2491
		[MethodImpl(4096)]
		private static extern void set_ambientSkyColor_Injected(ref Color value);

		// Token: 0x060009BC RID: 2492
		[MethodImpl(4096)]
		private static extern void get_ambientEquatorColor_Injected(out Color ret);

		// Token: 0x060009BD RID: 2493
		[MethodImpl(4096)]
		private static extern void set_ambientEquatorColor_Injected(ref Color value);

		// Token: 0x060009BE RID: 2494
		[MethodImpl(4096)]
		private static extern void get_ambientGroundColor_Injected(out Color ret);

		// Token: 0x060009BF RID: 2495
		[MethodImpl(4096)]
		private static extern void set_ambientGroundColor_Injected(ref Color value);

		// Token: 0x060009C0 RID: 2496
		[MethodImpl(4096)]
		private static extern void get_ambientLight_Injected(out Color ret);

		// Token: 0x060009C1 RID: 2497
		[MethodImpl(4096)]
		private static extern void set_ambientLight_Injected(ref Color value);

		// Token: 0x060009C2 RID: 2498
		[MethodImpl(4096)]
		private static extern void get_subtractiveShadowColor_Injected(out Color ret);

		// Token: 0x060009C3 RID: 2499
		[MethodImpl(4096)]
		private static extern void set_subtractiveShadowColor_Injected(ref Color value);

		// Token: 0x060009C4 RID: 2500
		[MethodImpl(4096)]
		private static extern void get_ambientProbe_Injected(out SphericalHarmonicsL2 ret);

		// Token: 0x060009C5 RID: 2501
		[MethodImpl(4096)]
		private static extern void set_ambientProbe_Injected(ref SphericalHarmonicsL2 value);
	}
}
