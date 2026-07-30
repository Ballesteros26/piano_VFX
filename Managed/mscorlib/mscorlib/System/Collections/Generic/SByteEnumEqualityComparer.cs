using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000A49 RID: 2633
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060060C5 RID: 24773 RVA: 0x0013E898 File Offset: 0x0013CA98
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x0013E898 File Offset: 0x0013CA98
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x0013E8A0 File Offset: 0x0013CAA0
		public override int GetHashCode(T obj)
		{
			return ((sbyte)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}
