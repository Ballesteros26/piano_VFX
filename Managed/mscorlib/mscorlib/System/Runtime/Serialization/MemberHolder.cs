using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020006D7 RID: 1751
	[Serializable]
	internal class MemberHolder
	{
		// Token: 0x06004A0B RID: 18955 RVA: 0x00108E68 File Offset: 0x00107068
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this.memberType = type;
			this.context = ctx;
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x00108E7E File Offset: 0x0010707E
		public override int GetHashCode()
		{
			return this.memberType.GetHashCode();
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x00108E8C File Offset: 0x0010708C
		public override bool Equals(object obj)
		{
			if (!(obj is MemberHolder))
			{
				return false;
			}
			MemberHolder memberHolder = (MemberHolder)obj;
			return memberHolder.memberType == this.memberType && memberHolder.context.State == this.context.State;
		}

		// Token: 0x040026B1 RID: 9905
		internal MemberInfo[] members;

		// Token: 0x040026B2 RID: 9906
		internal Type memberType;

		// Token: 0x040026B3 RID: 9907
		internal StreamingContext context;
	}
}
