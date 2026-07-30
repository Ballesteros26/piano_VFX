using System;
using System.Collections;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000023 RID: 35
	public sealed class UnixUserInfo
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000073CC File Offset: 0x000055CC
		public UnixUserInfo(string user)
		{
			this.passwd = new Passwd();
			Passwd passwd;
			if (Syscall.getpwnam_r(user, this.passwd, out passwd) != 0 || passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "user");
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007418 File Offset: 0x00005618
		[CLSCompliant(false)]
		public UnixUserInfo(uint user)
		{
			this.passwd = new Passwd();
			Passwd passwd;
			if (Syscall.getpwuid_r(user, this.passwd, out passwd) != 0 || passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid user id"), "user");
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007464 File Offset: 0x00005664
		public UnixUserInfo(long user)
		{
			this.passwd = new Passwd();
			Passwd passwd;
			if (Syscall.getpwuid_r(Convert.ToUInt32(user), this.passwd, out passwd) != 0 || passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid user id"), "user");
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000074B5 File Offset: 0x000056B5
		public UnixUserInfo(Passwd passwd)
		{
			this.passwd = UnixUserInfo.CopyPasswd(passwd);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000074CC File Offset: 0x000056CC
		private static Passwd CopyPasswd(Passwd pw)
		{
			return new Passwd
			{
				pw_name = pw.pw_name,
				pw_passwd = pw.pw_passwd,
				pw_uid = pw.pw_uid,
				pw_gid = pw.pw_gid,
				pw_gecos = pw.pw_gecos,
				pw_dir = pw.pw_dir,
				pw_shell = pw.pw_shell
			};
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007532 File Offset: 0x00005732
		public string UserName
		{
			get
			{
				return this.passwd.pw_name;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000753F File Offset: 0x0000573F
		public string Password
		{
			get
			{
				return this.passwd.pw_passwd;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000754C File Offset: 0x0000574C
		public long UserId
		{
			get
			{
				return (long)((ulong)this.passwd.pw_uid);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000755A File Offset: 0x0000575A
		public UnixGroupInfo Group
		{
			get
			{
				return new UnixGroupInfo((long)((ulong)this.passwd.pw_gid));
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000756D File Offset: 0x0000576D
		public long GroupId
		{
			get
			{
				return (long)((ulong)this.passwd.pw_gid);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000757B File Offset: 0x0000577B
		public string GroupName
		{
			get
			{
				return this.Group.GroupName;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007588 File Offset: 0x00005788
		public string RealName
		{
			get
			{
				return this.passwd.pw_gecos;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007595 File Offset: 0x00005795
		public string HomeDirectory
		{
			get
			{
				return this.passwd.pw_dir;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000075A2 File Offset: 0x000057A2
		public string ShellProgram
		{
			get
			{
				return this.passwd.pw_shell;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000075AF File Offset: 0x000057AF
		public override int GetHashCode()
		{
			return this.passwd.GetHashCode();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000075BC File Offset: 0x000057BC
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.passwd.Equals(((UnixUserInfo)obj).passwd);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000075EC File Offset: 0x000057EC
		public override string ToString()
		{
			return this.passwd.ToString();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000075F9 File Offset: 0x000057F9
		public static UnixUserInfo GetRealUser()
		{
			return new UnixUserInfo(UnixUserInfo.GetRealUserId());
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007605 File Offset: 0x00005805
		public static long GetRealUserId()
		{
			return (long)((ulong)Syscall.getuid());
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007610 File Offset: 0x00005810
		public static string GetLoginName()
		{
			StringBuilder stringBuilder = new StringBuilder(4);
			int num;
			do
			{
				stringBuilder.Capacity *= 2;
				num = Syscall.getlogin_r(stringBuilder, (ulong)((long)stringBuilder.Capacity));
			}
			while (num == -1 && Stdlib.GetLastError() == Errno.ERANGE);
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			return stringBuilder.ToString();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007659 File Offset: 0x00005859
		public Passwd ToPasswd()
		{
			return UnixUserInfo.CopyPasswd(this.passwd);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007668 File Offset: 0x00005868
		public static UnixUserInfo[] GetLocalUsers()
		{
			ArrayList arrayList = new ArrayList();
			object pwd_lock = Syscall.pwd_lock;
			lock (pwd_lock)
			{
				if (Syscall.setpwent() != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				try
				{
					Passwd passwd;
					while ((passwd = Syscall.getpwent()) != null)
					{
						arrayList.Add(new UnixUserInfo(passwd));
					}
					if (Stdlib.GetLastError() != (Errno)0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
				}
				finally
				{
					Syscall.endpwent();
				}
			}
			return (UnixUserInfo[])arrayList.ToArray(typeof(UnixUserInfo));
		}

		// Token: 0x04000091 RID: 145
		private Passwd passwd;
	}
}
