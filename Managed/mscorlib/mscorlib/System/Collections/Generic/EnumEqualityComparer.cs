using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000A48 RID: 2632
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060060BE RID: 24766 RVA: 0x0013E828 File Offset: 0x0013CA28
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x0013E848 File Offset: 0x0013CA48
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x0013E4CD File Offset: 0x0013C6CD
		public EnumEqualityComparer()
		{
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x0013E4CD File Offset: 0x0013C6CD
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x0013E863 File Offset: 0x0013CA63
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x0013E88D File Offset: 0x0013CA8D
		public override bool Equals(object obj)
		{
			return obj is EnumEqualityComparer<T>;
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
