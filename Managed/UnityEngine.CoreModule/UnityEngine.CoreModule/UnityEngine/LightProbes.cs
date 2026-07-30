using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000E5 RID: 229
	[NativeHeader("Runtime/Export/Graphics/Graphics.bindings.h")]
	[NativeAsStruct]
	[StructLayout(0)]
	public sealed class LightProbes : Object
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private LightProbes()
		{
		}

		// Token: 0x06000790 RID: 1936
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void Tetrahedralize();

		// Token: 0x06000791 RID: 1937
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void TetrahedralizeAsync();

		// Token: 0x06000792 RID: 1938 RVA: 0x0000BF33 File Offset: 0x0000A133
		[FreeFunction]
		public static void GetInterpolatedProbe(Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe)
		{
			LightProbes.GetInterpolatedProbe_Injected(ref position, renderer, out probe);
		}

		// Token: 0x06000793 RID: 1939
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern bool AreLightProbesAllowed(Renderer renderer);

		// Token: 0x06000794 RID: 1940 RVA: 0x0000BF40 File Offset: 0x0000A140
		public static void CalculateInterpolatedLightAndOcclusionProbes(Vector3[] positions, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null && lightProbes.Length < positions.Length;
			if (flag3)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes has less elements than positions");
			}
			bool flag4 = occlusionProbes != null && occlusionProbes.Length < positions.Length;
			if (flag4)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes has less elements than positions");
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(positions, positions.Length, lightProbes, occlusionProbes);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public static void CalculateInterpolatedLightAndOcclusionProbes(List<Vector3> positions, List<SphericalHarmonicsL2> lightProbes, List<Vector4> occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null;
			if (flag3)
			{
				bool flag4 = lightProbes.Capacity < positions.Count;
				if (flag4)
				{
					lightProbes.Capacity = positions.Count;
				}
				bool flag5 = lightProbes.Count < positions.Count;
				if (flag5)
				{
					NoAllocHelpers.ResizeList<SphericalHarmonicsL2>(lightProbes, positions.Count);
				}
			}
			bool flag6 = occlusionProbes != null;
			if (flag6)
			{
				bool flag7 = occlusionProbes.Capacity < positions.Count;
				if (flag7)
				{
					occlusionProbes.Capacity = positions.Count;
				}
				bool flag8 = occlusionProbes.Count < positions.Count;
				if (flag8)
				{
					NoAllocHelpers.ResizeList<Vector4>(occlusionProbes, positions.Count);
				}
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(NoAllocHelpers.ExtractArrayFromListT<Vector3>(positions), positions.Count, NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes));
		}

		// Token: 0x06000796 RID: 1942
		[FreeFunction]
		[NativeName("CalculateInterpolatedLightAndOcclusionProbes")]
		[MethodImpl(4096)]
		internal static extern void CalculateInterpolatedLightAndOcclusionProbes_Internal(Vector3[] positions, int positionsCount, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes);

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000797 RID: 1943
		public extern Vector3[] positions
		{
			[NativeName("GetLightProbePositions")]
			[FreeFunction(HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000798 RID: 1944
		// (set) Token: 0x06000799 RID: 1945
		public extern SphericalHarmonicsL2[] bakedProbes
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetBakedCoefficients")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetBakedCoefficients")]
			[FreeFunction(HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600079A RID: 1946
		public extern int count
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetLightProbeCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600079B RID: 1947
		public extern int cellCount
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetTetrahedraSize")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600079C RID: 1948
		[FreeFunction]
		[NativeName("GetLightProbeCount")]
		[MethodImpl(4096)]
		internal static extern int GetCount();

		// Token: 0x0600079D RID: 1949 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("Use GetInterpolatedProbe instead.", true)]
		[EditorBrowsable(1)]
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000C0BC File Offset: 0x0000A2BC
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00002EC3 File Offset: 0x000010C3
		[EditorBrowsable(1)]
		[Obsolete("Use bakedProbes instead.", true)]
		public float[] coefficients
		{
			get
			{
				return new float[0];
			}
			set
			{
			}
		}

		// Token: 0x060007A0 RID: 1952
		[MethodImpl(4096)]
		private static extern void GetInterpolatedProbe_Injected(ref Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe);
	}
}
