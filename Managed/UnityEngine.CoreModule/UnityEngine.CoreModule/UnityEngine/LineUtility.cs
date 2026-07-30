using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000140 RID: 320
	[NativeHeader("Runtime/Export/Graphics/LineUtility.bindings.h")]
	public sealed class LineUtility
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public static void Simplify(List<Vector3> points, float tolerance, List<int> pointsToKeep)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = pointsToKeep == null;
			if (flag2)
			{
				throw new ArgumentNullException("pointsToKeep");
			}
			LineUtility.GeneratePointsToKeep3D(points, tolerance, pointsToKeep);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0000F600 File Offset: 0x0000D800
		public static void Simplify(List<Vector3> points, float tolerance, List<Vector3> simplifiedPoints)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = simplifiedPoints == null;
			if (flag2)
			{
				throw new ArgumentNullException("simplifiedPoints");
			}
			LineUtility.GenerateSimplifiedPoints3D(points, tolerance, simplifiedPoints);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0000F640 File Offset: 0x0000D840
		public static void Simplify(List<Vector2> points, float tolerance, List<int> pointsToKeep)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = pointsToKeep == null;
			if (flag2)
			{
				throw new ArgumentNullException("pointsToKeep");
			}
			LineUtility.GeneratePointsToKeep2D(points, tolerance, pointsToKeep);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0000F680 File Offset: 0x0000D880
		public static void Simplify(List<Vector2> points, float tolerance, List<Vector2> simplifiedPoints)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = simplifiedPoints == null;
			if (flag2)
			{
				throw new ArgumentNullException("simplifiedPoints");
			}
			LineUtility.GenerateSimplifiedPoints2D(points, tolerance, simplifiedPoints);
		}

		// Token: 0x06000C01 RID: 3073
		[FreeFunction("LineUtility_Bindings::GeneratePointsToKeep3D", IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern void GeneratePointsToKeep3D(object pointsList, float tolerance, object pointsToKeepList);

		// Token: 0x06000C02 RID: 3074
		[FreeFunction("LineUtility_Bindings::GeneratePointsToKeep2D", IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern void GeneratePointsToKeep2D(object pointsList, float tolerance, object pointsToKeepList);

		// Token: 0x06000C03 RID: 3075
		[FreeFunction("LineUtility_Bindings::GenerateSimplifiedPoints3D", IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern void GenerateSimplifiedPoints3D(object pointsList, float tolerance, object simplifiedPoints);

		// Token: 0x06000C04 RID: 3076
		[FreeFunction("LineUtility_Bindings::GenerateSimplifiedPoints2D", IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern void GenerateSimplifiedPoints2D(object pointsList, float tolerance, object simplifiedPoints);
	}
}
