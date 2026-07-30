using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000064 RID: 100
	public sealed class Fstab : IEquatable<Fstab>
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000AE94 File Offset: 0x00009094
		public override int GetHashCode()
		{
			return this.fs_spec.GetHashCode() ^ this.fs_file.GetHashCode() ^ this.fs_vfstype.GetHashCode() ^ this.fs_mntops.GetHashCode() ^ this.fs_type.GetHashCode() ^ this.fs_freq ^ this.fs_passno;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000AEEC File Offset: 0x000090EC
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Fstab fstab = (Fstab)obj;
			return this.Equals(fstab);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000AF20 File Offset: 0x00009120
		public bool Equals(Fstab value)
		{
			return !(value == null) && (value.fs_spec == this.fs_spec && value.fs_file == this.fs_file && value.fs_vfstype == this.fs_vfstype && value.fs_mntops == this.fs_mntops && value.fs_type == this.fs_type && value.fs_freq == this.fs_freq) && value.fs_passno == this.fs_passno;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000AFB5 File Offset: 0x000091B5
		public override string ToString()
		{
			return this.fs_spec;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AFBD File Offset: 0x000091BD
		public static bool operator ==(Fstab lhs, Fstab rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000AFC6 File Offset: 0x000091C6
		public static bool operator !=(Fstab lhs, Fstab rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000457 RID: 1111
		public string fs_spec;

		// Token: 0x04000458 RID: 1112
		public string fs_file;

		// Token: 0x04000459 RID: 1113
		public string fs_vfstype;

		// Token: 0x0400045A RID: 1114
		public string fs_mntops;

		// Token: 0x0400045B RID: 1115
		public string fs_type;

		// Token: 0x0400045C RID: 1116
		public int fs_freq;

		// Token: 0x0400045D RID: 1117
		public int fs_passno;
	}
}
