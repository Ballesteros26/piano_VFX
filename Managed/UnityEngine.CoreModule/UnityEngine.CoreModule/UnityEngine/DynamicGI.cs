using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000CE RID: 206
	[NativeHeader("Runtime/GI/DynamicGI.h")]
	public sealed class DynamicGI
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005A8 RID: 1448
		// (set) Token: 0x060005A9 RID: 1449
		public static extern float indirectScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005AA RID: 1450
		// (set) Token: 0x060005AB RID: 1451
		public static extern float updateThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005AC RID: 1452
		// (set) Token: 0x060005AD RID: 1453
		public static extern int materialUpdateTimeSlice
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00009808 File Offset: 0x00007A08
		public static void SetEmissive(Renderer renderer, Color color)
		{
			DynamicGI.SetEmissive_Injected(renderer, ref color);
		}

		// Token: 0x060005AF RID: 1455
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern void SetEnvironmentData([NotNull] float[] input);

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005B0 RID: 1456
		// (set) Token: 0x060005B1 RID: 1457
		public static extern bool synchronousMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005B2 RID: 1458
		public static extern bool isConverged
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005B3 RID: 1459
		internal static extern int scheduledMaterialUpdatesCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005B4 RID: 1460
		// (set) Token: 0x060005B5 RID: 1461
		internal static extern bool asyncMaterialUpdates
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005B6 RID: 1462
		[MethodImpl(4096)]
		public static extern void UpdateEnvironment();

		// Token: 0x060005B7 RID: 1463 RVA: 0x00002EC3 File Offset: 0x000010C3
		[EditorBrowsable(1)]
		[Obsolete("DynamicGI.UpdateMaterials(Renderer) is deprecated; instead, use extension method from RendererExtensions: 'renderer.UpdateGIMaterials()' (UnityUpgradable).", true)]
		public static void UpdateMaterials(Renderer renderer)
		{
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00002EC3 File Offset: 0x000010C3
		[EditorBrowsable(1)]
		[Obsolete("DynamicGI.UpdateMaterials(Terrain) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials()' (UnityUpgradable).", true)]
		public static void UpdateMaterials(Object renderer)
		{
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("DynamicGI.UpdateMaterials(Terrain, int, int, int, int) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials(x, y, width, height)' (UnityUpgradable).", true)]
		[EditorBrowsable(1)]
		public static void UpdateMaterials(Object renderer, int x, int y, int width, int height)
		{
		}

		// Token: 0x060005BB RID: 1467
		[MethodImpl(4096)]
		private static extern void SetEmissive_Injected(Renderer renderer, ref Color color);
	}
}
