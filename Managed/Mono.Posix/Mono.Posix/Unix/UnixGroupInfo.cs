using System;
using System.Collections;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000018 RID: 24
	public sealed class UnixGroupInfo
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00005338 File Offset: 0x00003538
		public UnixGroupInfo(string group)
		{
			this.group = new Group();
			Group group2;
			if (Syscall.getgrnam_r(group, this.group, out group2) != 0 || group2 == null)
			{
				throw new ArgumentException(Locale.GetText("invalid group name"), "group");
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005384 File Offset: 0x00003584
		public UnixGroupInfo(long group)
		{
			this.group = new Group();
			Group group2;
			if (Syscall.getgrgid_r(Convert.ToUInt32(group), this.group, out group2) != 0 || group2 == null)
			{
				throw new ArgumentException(Locale.GetText("invalid group id"), "group");
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000053D5 File Offset: 0x000035D5
		public UnixGroupInfo(Group group)
		{
			this.group = UnixGroupInfo.CopyGroup(group);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000053E9 File Offset: 0x000035E9
		private static Group CopyGroup(Group group)
		{
			return new Group
			{
				gr_gid = group.gr_gid,
				gr_mem = group.gr_mem,
				gr_name = group.gr_name,
				gr_passwd = group.gr_passwd
			};
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005420 File Offset: 0x00003620
		public string GroupName
		{
			get
			{
				return this.group.gr_name;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000542D File Offset: 0x0000362D
		public string Password
		{
			get
			{
				return this.group.gr_passwd;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000543A File Offset: 0x0000363A
		public long GroupId
		{
			get
			{
				return (long)((ulong)this.group.gr_gid);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005448 File Offset: 0x00003648
		public UnixUserInfo[] GetMembers()
		{
			ArrayList arrayList = new ArrayList(this.group.gr_mem.Length);
			for (int i = 0; i < this.group.gr_mem.Length; i++)
			{
				try
				{
					arrayList.Add(new UnixUserInfo(this.group.gr_mem[i]));
				}
				catch (ArgumentException)
				{
				}
			}
			return (UnixUserInfo[])arrayList.ToArray(typeof(UnixUserInfo));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000054C4 File Offset: 0x000036C4
		public string[] GetMemberNames()
		{
			return (string[])this.group.gr_mem.Clone();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000054DB File Offset: 0x000036DB
		public override int GetHashCode()
		{
			return this.group.GetHashCode();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000054E8 File Offset: 0x000036E8
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.group.Equals(((UnixGroupInfo)obj).group);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005518 File Offset: 0x00003718
		public override string ToString()
		{
			return this.group.ToString();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005525 File Offset: 0x00003725
		public Group ToGroup()
		{
			return UnixGroupInfo.CopyGroup(this.group);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005534 File Offset: 0x00003734
		public static UnixGroupInfo[] GetLocalGroups()
		{
			ArrayList arrayList = new ArrayList();
			object grp_lock = Syscall.grp_lock;
			lock (grp_lock)
			{
				if (Syscall.setgrent() != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				try
				{
					Group group;
					while ((group = Syscall.getgrent()) != null)
					{
						arrayList.Add(new UnixGroupInfo(group));
					}
					if (Stdlib.GetLastError() != (Errno)0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
				}
				finally
				{
					Syscall.endgrent();
				}
			}
			return (UnixGroupInfo[])arrayList.ToArray(typeof(UnixGroupInfo));
		}

		// Token: 0x04000075 RID: 117
		private Group group;
	}
}
