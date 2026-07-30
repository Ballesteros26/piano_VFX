using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000058 RID: 88
	[Map("struct utimbuf")]
	public struct Utimbuf : IEquatable<Utimbuf>
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000A7AE File Offset: 0x000089AE
		public override int GetHashCode()
		{
			return this.actime.GetHashCode() ^ this.modtime.GetHashCode();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Utimbuf utimbuf = (Utimbuf)obj;
			return utimbuf.actime == this.actime && utimbuf.modtime == this.modtime;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A81C File Offset: 0x00008A1C
		public bool Equals(Utimbuf value)
		{
			return value.actime == this.actime && value.modtime == this.modtime;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A83C File Offset: 0x00008A3C
		public static bool operator ==(Utimbuf lhs, Utimbuf rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A846 File Offset: 0x00008A46
		public static bool operator !=(Utimbuf lhs, Utimbuf rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000429 RID: 1065
		[time_t]
		public long actime;

		// Token: 0x0400042A RID: 1066
		[time_t]
		public long modtime;
	}
}
