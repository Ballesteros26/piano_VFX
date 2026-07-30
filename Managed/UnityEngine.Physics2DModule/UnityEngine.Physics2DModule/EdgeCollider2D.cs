using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[NativeHeader("Modules/Physics2D/Public/EdgeCollider2D.h")]
	public sealed class EdgeCollider2D : Collider2D
	{
		// Token: 0x06000334 RID: 820
		[MethodImpl(4096)]
		public extern void Reset();

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000335 RID: 821
		// (set) Token: 0x06000336 RID: 822
		public extern float edgeRadius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000337 RID: 823
		public extern int edgeCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000338 RID: 824
		public extern int pointCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000339 RID: 825
		// (set) Token: 0x0600033A RID: 826
		public extern Vector2[] points
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600033B RID: 827
		[NativeMethod("GetPoints_Binding")]
		[MethodImpl(4096)]
		public extern int GetPoints([NotNull] List<Vector2> points);

		// Token: 0x0600033C RID: 828
		[NativeMethod("SetPoints_Binding")]
		[MethodImpl(4096)]
		public extern bool SetPoints([NotNull] List<Vector2> points);

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600033D RID: 829
		// (set) Token: 0x0600033E RID: 830
		public extern bool useAdjacentStartPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600033F RID: 831
		// (set) Token: 0x06000340 RID: 832
		public extern bool useAdjacentEndPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00006D24 File Offset: 0x00004F24
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00006D3A File Offset: 0x00004F3A
		public Vector2 adjacentStartPoint
		{
			get
			{
				Vector2 vector;
				this.get_adjacentStartPoint_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_adjacentStartPoint_Injected(ref value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00006D44 File Offset: 0x00004F44
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00006D5A File Offset: 0x00004F5A
		public Vector2 adjacentEndPoint
		{
			get
			{
				Vector2 vector;
				this.get_adjacentEndPoint_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_adjacentEndPoint_Injected(ref value);
			}
		}

		// Token: 0x06000346 RID: 838
		[MethodImpl(4096)]
		private extern void get_adjacentStartPoint_Injected(out Vector2 ret);

		// Token: 0x06000347 RID: 839
		[MethodImpl(4096)]
		private extern void set_adjacentStartPoint_Injected(ref Vector2 value);

		// Token: 0x06000348 RID: 840
		[MethodImpl(4096)]
		private extern void get_adjacentEndPoint_Injected(out Vector2 ret);

		// Token: 0x06000349 RID: 841
		[MethodImpl(4096)]
		private extern void set_adjacentEndPoint_Injected(ref Vector2 value);
	}
}
