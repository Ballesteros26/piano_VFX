using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001AF RID: 431
	[NativeHeader("Runtime/Export/Scripting/NoAllocHelpers.bindings.h")]
	internal sealed class NoAllocHelpers
	{
		// Token: 0x060013CD RID: 5069 RVA: 0x000203C4 File Offset: 0x0001E5C4
		public static void ResizeList<T>(List<T> list, int size)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list");
			}
			bool flag2 = size < 0 || size > list.Capacity;
			if (flag2)
			{
				throw new ArgumentException("invalid size to resize.", "list");
			}
			bool flag3 = size != list.Count;
			if (flag3)
			{
				NoAllocHelpers.Internal_ResizeList(list, size);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00020424 File Offset: 0x0001E624
		public static void EnsureListElemCount<T>(List<T> list, int count)
		{
			list.Clear();
			bool flag = list.Capacity < count;
			if (flag)
			{
				list.Capacity = count;
			}
			NoAllocHelpers.ResizeList<T>(list, count);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00020458 File Offset: 0x0001E658
		public static int SafeLength(Array values)
		{
			return (values != null) ? values.Length : 0;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00020478 File Offset: 0x0001E678
		public static int SafeLength<T>(List<T> values)
		{
			return (values != null) ? values.Count : 0;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00020498 File Offset: 0x0001E698
		public static T[] ExtractArrayFromListT<T>(List<T> list)
		{
			return (T[])NoAllocHelpers.ExtractArrayFromList(list);
		}

		// Token: 0x060013D2 RID: 5074
		[FreeFunction("NoAllocHelpers_Bindings::Internal_ResizeList")]
		[MethodImpl(4096)]
		internal static extern void Internal_ResizeList(object list, int size);

		// Token: 0x060013D3 RID: 5075
		[FreeFunction("NoAllocHelpers_Bindings::ExtractArrayFromList")]
		[MethodImpl(4096)]
		public static extern Array ExtractArrayFromList(object list);
	}
}
