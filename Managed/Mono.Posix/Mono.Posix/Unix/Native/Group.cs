using System;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000065 RID: 101
	public sealed class Group : IEquatable<Group>
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000AFDC File Offset: 0x000091DC
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.gr_mem.Length; i++)
			{
				num ^= this.gr_mem[i].GetHashCode();
			}
			return this.gr_name.GetHashCode() ^ this.gr_passwd.GetHashCode() ^ this.gr_gid.GetHashCode() ^ num;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000B034 File Offset: 0x00009234
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Group group = (Group)obj;
			return this.Equals(group);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000B068 File Offset: 0x00009268
		public bool Equals(Group value)
		{
			if (value == null)
			{
				return false;
			}
			if (value.gr_gid != this.gr_gid)
			{
				return false;
			}
			if (value.gr_gid != this.gr_gid || !(value.gr_name == this.gr_name) || !(value.gr_passwd == this.gr_passwd))
			{
				return false;
			}
			if (value.gr_mem == this.gr_mem)
			{
				return true;
			}
			if (value.gr_mem == null || this.gr_mem == null)
			{
				return false;
			}
			if (value.gr_mem.Length != this.gr_mem.Length)
			{
				return false;
			}
			for (int i = 0; i < this.gr_mem.Length; i++)
			{
				if (this.gr_mem[i] != value.gr_mem[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000B12C File Offset: 0x0000932C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.gr_name).Append(":").Append(this.gr_passwd)
				.Append(":");
			stringBuilder.Append(this.gr_gid).Append(":");
			Group.GetMembers(stringBuilder, this.gr_mem);
			return stringBuilder.ToString();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000B194 File Offset: 0x00009394
		private static void GetMembers(StringBuilder sb, string[] members)
		{
			if (members.Length != 0)
			{
				sb.Append(members[0]);
			}
			for (int i = 1; i < members.Length; i++)
			{
				sb.Append(",");
				sb.Append(members[i]);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000B1D3 File Offset: 0x000093D3
		public static bool operator ==(Group lhs, Group rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B1DC File Offset: 0x000093DC
		public static bool operator !=(Group lhs, Group rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x0400045E RID: 1118
		public string gr_name;

		// Token: 0x0400045F RID: 1119
		public string gr_passwd;

		// Token: 0x04000460 RID: 1120
		[CLSCompliant(false)]
		public uint gr_gid;

		// Token: 0x04000461 RID: 1121
		public string[] gr_mem;
	}
}
