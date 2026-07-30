using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000A4A RID: 2634
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060060C8 RID: 24776 RVA: 0x0013E898 File Offset: 0x0013CA98
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0013E898 File Offset: 0x0013CA98
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0013E8BC File Offset: 0x0013CABC
		public override int GetHashCode(T obj)
		{
			return ((short)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
