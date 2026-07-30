using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/Physics2D/Public/PolygonCollider2D.h")]
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000354 RID: 852
		public extern bool autoTiling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000355 RID: 853
		[NativeMethod("GetPointCount")]
		[MethodImpl(4096)]
		public extern int GetTotalPointCount();

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000356 RID: 854
		// (set) Token: 0x06000357 RID: 855
		public extern Vector2[] points
		{
			[NativeMethod("GetPoints_Binding")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPoints_Binding")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000358 RID: 856
		// (set) Token: 0x06000359 RID: 857
		public extern int pathCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00006D84 File Offset: 0x00004F84
		public Vector2[] GetPath(int index)
		{
			bool flag = index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist.", index));
			}
			bool flag2 = index < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist; negative path index is invalid.", index));
			}
			return this.GetPath_Internal(index);
		}

		// Token: 0x0600035B RID: 859
		[NativeMethod("GetPath_Binding")]
		[MethodImpl(4096)]
		private extern Vector2[] GetPath_Internal(int index);

		// Token: 0x0600035C RID: 860 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public void SetPath(int index, Vector2[] points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPath_Internal(index, points);
		}

		// Token: 0x0600035D RID: 861
		[NativeMethod("SetPath_Binding")]
		[MethodImpl(4096)]
		private extern void SetPath_Internal(int index, [NotNull] Vector2[] points);

		// Token: 0x0600035E RID: 862 RVA: 0x00006E1C File Offset: 0x0000501C
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

		// Token: 0x0600035F RID: 863
		[NativeMethod("GetPathList_Binding")]
		[MethodImpl(4096)]
		private extern int GetPathList_Internal(int index, [NotNull] List<Vector2> points);

		// Token: 0x06000360 RID: 864 RVA: 0x00006E88 File Offset: 0x00005088
		public void SetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPathList_Internal(index, points);
		}

		// Token: 0x06000361 RID: 865
		[NativeMethod("SetPathList_Binding")]
		[MethodImpl(4096)]
		private extern void SetPathList_Internal(int index, [NotNull] List<Vector2> points);

		// Token: 0x06000362 RID: 866 RVA: 0x00006EBD File Offset: 0x000050BD
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides)
		{
			this.CreatePrimitive(sides, Vector2.one, Vector2.zero);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00006ED2 File Offset: 0x000050D2
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides, Vector2 scale)
		{
			this.CreatePrimitive(sides, scale, Vector2.zero);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00006EE4 File Offset: 0x000050E4
		public void CreatePrimitive(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset)
		{
			bool flag = sides < 3;
			if (flag)
			{
				Debug.LogWarning("Cannot create a 2D polygon primitive collider with less than two sides.", this);
			}
			else
			{
				bool flag2 = scale.x <= 0f || scale.y <= 0f;
				if (flag2)
				{
					Debug.LogWarning("Cannot create a 2D polygon primitive collider with an axis scale less than or equal to zero.", this);
				}
				else
				{
					this.CreatePrimitive_Internal(sides, scale, offset, true);
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00006F47 File Offset: 0x00005147
		[NativeMethod("CreatePrimitive")]
		private void CreatePrimitive_Internal(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset, bool autoRefresh)
		{
			this.CreatePrimitive_Internal_Injected(sides, ref scale, ref offset, autoRefresh);
		}

		// Token: 0x06000367 RID: 871
		[MethodImpl(4096)]
		private extern void CreatePrimitive_Internal_Injected(int sides, [DefaultValue("Vector2.one")] ref Vector2 scale, [DefaultValue("Vector2.zero")] ref Vector2 offset, bool autoRefresh);
	}
}
