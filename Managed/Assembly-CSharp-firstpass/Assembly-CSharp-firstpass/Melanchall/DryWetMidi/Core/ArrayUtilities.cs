using System;
using System.Collections.Generic;
using System.Linq;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000199 RID: 409
	internal static class ArrayUtilities
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x00021D7E File Offset: 0x0001FF7E
		internal static bool Equals<T>(T[] array1, T[] array2)
		{
			return array1 == array2 || (array1 != null && array2 != null && array1.Length == array2.Length && array1.SequenceEqual(array2));
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00021D9F File Offset: 0x0001FF9F
		internal static int GetHashCode<T>(T[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.GetHashCode(EqualityComparer<T>.Default);
		}
	}
}
