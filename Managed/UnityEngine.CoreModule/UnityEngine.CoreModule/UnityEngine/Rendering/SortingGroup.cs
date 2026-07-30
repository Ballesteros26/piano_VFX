using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200038F RID: 911
	[NativeType(Header = "Runtime/2D/Sorting/SortingGroup.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class SortingGroup : Behaviour
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001FBC RID: 8124
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static extern int invalidSortingGroupID
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001FBD RID: 8125
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern void UpdateAllSortingGroups();

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001FBE RID: 8126
		// (set) Token: 0x06001FBF RID: 8127
		public extern string sortingLayerName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001FC0 RID: 8128
		// (set) Token: 0x06001FC1 RID: 8129
		public extern int sortingLayerID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001FC2 RID: 8130
		// (set) Token: 0x06001FC3 RID: 8131
		public extern int sortingOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001FC4 RID: 8132
		internal extern int sortingGroupID
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001FC5 RID: 8133
		internal extern int sortingGroupOrder
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001FC6 RID: 8134
		internal extern int index
		{
			[MethodImpl(4096)]
			get;
		}
	}
}
