using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000067 RID: 103
	public sealed class Utsname : IEquatable<Utsname>
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public override int GetHashCode()
		{
			return this.sysname.GetHashCode() ^ this.nodename.GetHashCode() ^ this.release.GetHashCode() ^ this.version.GetHashCode() ^ this.machine.GetHashCode() ^ this.domainname.GetHashCode();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B3FC File Offset: 0x000095FC
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Utsname utsname = (Utsname)obj;
			return this.Equals(utsname);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B430 File Offset: 0x00009630
		public bool Equals(Utsname value)
		{
			return value.sysname == this.sysname && value.nodename == this.nodename && value.release == this.release && value.version == this.version && value.machine == this.machine && value.domainname == this.domainname;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B4AF File Offset: 0x000096AF
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4}", new object[] { this.sysname, this.nodename, this.release, this.version, this.machine });
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B4EE File Offset: 0x000096EE
		public static bool operator ==(Utsname lhs, Utsname rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000B4F7 File Offset: 0x000096F7
		public static bool operator !=(Utsname lhs, Utsname rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000469 RID: 1129
		public string sysname;

		// Token: 0x0400046A RID: 1130
		public string nodename;

		// Token: 0x0400046B RID: 1131
		public string release;

		// Token: 0x0400046C RID: 1132
		public string version;

		// Token: 0x0400046D RID: 1133
		public string machine;

		// Token: 0x0400046E RID: 1134
		public string domainname;
	}
}
