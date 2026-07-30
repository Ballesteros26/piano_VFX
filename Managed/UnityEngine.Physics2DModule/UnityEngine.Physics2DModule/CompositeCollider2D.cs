using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[RequireComponent(typeof(Rigidbody2D))]
	[NativeHeader("Modules/Physics2D/Public/CompositeCollider2D.h")]
	public sealed class CompositeCollider2D : Collider2D
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000368 RID: 872
		// (set) Token: 0x06000369 RID: 873
		public extern CompositeCollider2D.GeometryType geometryType
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600036A RID: 874
		// (set) Token: 0x0600036B RID: 875
		public extern CompositeCollider2D.GenerationType generationType
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600036C RID: 876
		// (set) Token: 0x0600036D RID: 877
		public extern float vertexDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600036E RID: 878
		// (set) Token: 0x0600036F RID: 879
		public extern float edgeRadius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000370 RID: 880
		// (set) Token: 0x06000371 RID: 881
		public extern float offsetDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000372 RID: 882
		[MethodImpl(4096)]
		public extern void GenerateGeometry();

		// Token: 0x06000373 RID: 883 RVA: 0x00006F58 File Offset: 0x00005158
		public int GetPathPointCount(int index)
		{
			int num = this.pathCount - 1;
			bool flag = index < 0 || index > num;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, num));
			}
			return this.GetPathPointCount_Internal(index);
		}

		// Token: 0x06000374 RID: 884
		[NativeMethod("GetPathPointCount_Binding")]
		[MethodImpl(4096)]
		private extern int GetPathPointCount_Internal(int index);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000375 RID: 885
		public extern int pathCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000376 RID: 886
		public extern int pointCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00006FAC File Offset: 0x000051AC
		public int GetPath(int index, Vector2[] points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathArray_Internal(index, points);
		}

		// Token: 0x06000378 RID: 888
		[NativeMethod("GetPathArray_Binding")]
		[MethodImpl(4096)]
		private extern int GetPathArray_Internal(int index, [NotNull] Vector2[] points);

		// Token: 0x06000379 RID: 889 RVA: 0x00007018 File Offset: 0x00005218
		public int GetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathList_Internal(index, points);
		}

		// Token: 0x0600037A RID: 890
		[NativeMethod("GetPathList_Binding")]
		[MethodImpl(4096)]
		private extern int GetPathList_Internal(int index, [NotNull] List<Vector2> points);

		// Token: 0x02000023 RID: 35
		public enum GeometryType
		{
			// Token: 0x0400007D RID: 125
			Outlines,
			// Token: 0x0400007E RID: 126
			Polygons
		}

		// Token: 0x02000024 RID: 36
		public enum GenerationType
		{
			// Token: 0x04000080 RID: 128
			Synchronous,
			// Token: 0x04000081 RID: 129
			Manual
		}
	}
}
