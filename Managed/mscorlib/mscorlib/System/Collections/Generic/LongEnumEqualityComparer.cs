using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000A4B RID: 2635
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060060CB RID: 24779 RVA: 0x0013E8D8 File Offset: 0x0013CAD8
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x0013E8F8 File Offset: 0x0013CAF8
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x0013E913 File Offset: 0x0013CB13
		public override bool Equals(object obj)
		{
			return obj is LongEnumEqualityComparer<T>;
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0013E4CD File Offset: 0x0013C6CD
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0013E4CD File Offset: 0x0013C6CD
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x0013E91E File Offset: 0x0013CB1E
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
