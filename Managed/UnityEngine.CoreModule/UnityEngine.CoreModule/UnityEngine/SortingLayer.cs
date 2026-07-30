using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000097 RID: 151
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct SortingLayer
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000418C File Offset: 0x0000238C
		public int id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000041A4 File Offset: 0x000023A4
		public string name
		{
			get
			{
				return SortingLayer.IDToName(this.m_Id);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000041C4 File Offset: 0x000023C4
		public int value
		{
			get
			{
				return SortingLayer.GetLayerValueFromID(this.m_Id);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000041E4 File Offset: 0x000023E4
		public static SortingLayer[] layers
		{
			get
			{
				int[] sortingLayerIDsInternal = SortingLayer.GetSortingLayerIDsInternal();
				SortingLayer[] array = new SortingLayer[sortingLayerIDsInternal.Length];
				for (int i = 0; i < sortingLayerIDsInternal.Length; i++)
				{
					array[i].m_Id = sortingLayerIDsInternal[i];
				}
				return array;
			}
		}

		// Token: 0x060001D0 RID: 464
		[FreeFunction("GetTagManager().GetSortingLayerIDs")]
		[MethodImpl(4096)]
		private static extern int[] GetSortingLayerIDsInternal();

		// Token: 0x060001D1 RID: 465
		[FreeFunction("GetTagManager().GetSortingLayerValueFromUniqueID")]
		[MethodImpl(4096)]
		public static extern int GetLayerValueFromID(int id);

		// Token: 0x060001D2 RID: 466
		[FreeFunction("GetTagManager().GetSortingLayerValueFromName")]
		[MethodImpl(4096)]
		public static extern int GetLayerValueFromName(string name);

		// Token: 0x060001D3 RID: 467
		[FreeFunction("GetTagManager().GetSortingLayerUniqueIDFromName")]
		[MethodImpl(4096)]
		public static extern int NameToID(string name);

		// Token: 0x060001D4 RID: 468
		[FreeFunction("GetTagManager().GetSortingLayerNameFromUniqueID")]
		[MethodImpl(4096)]
		public static extern string IDToName(int id);

		// Token: 0x060001D5 RID: 469
		[FreeFunction("GetTagManager().IsSortingLayerUniqueIDValid")]
		[MethodImpl(4096)]
		public static extern bool IsValid(int id);

		// Token: 0x040001AD RID: 429
		private int m_Id;
	}
}
