using System;
using System.Collections;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000015 RID: 21
	public sealed class UnixEnvironment
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000047A6 File Offset: 0x000029A6
		private UnixEnvironment()
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000047AE File Offset: 0x000029AE
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000047B5 File Offset: 0x000029B5
		public static string CurrentDirectory
		{
			get
			{
				return UnixDirectoryInfo.GetCurrentDirectory();
			}
			set
			{
				UnixDirectoryInfo.SetCurrentDirectory(value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000047C0 File Offset: 0x000029C0
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000047E2 File Offset: 0x000029E2
		public static string MachineName
		{
			get
			{
				Utsname utsname;
				if (Syscall.uname(out utsname) != 0)
				{
					throw UnixMarshal.CreateExceptionForLastError();
				}
				return utsname.nodename;
			}
			set
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.sethostname(value));
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000047EF File Offset: 0x000029EF
		public static string UserName
		{
			get
			{
				return UnixUserInfo.GetRealUser().UserName;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000047FB File Offset: 0x000029FB
		public static UnixGroupInfo RealGroup
		{
			get
			{
				return new UnixGroupInfo(UnixEnvironment.RealGroupId);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004807 File Offset: 0x00002A07
		public static long RealGroupId
		{
			get
			{
				return (long)((ulong)Syscall.getgid());
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000480F File Offset: 0x00002A0F
		public static UnixUserInfo RealUser
		{
			get
			{
				return new UnixUserInfo(UnixEnvironment.RealUserId);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000481B File Offset: 0x00002A1B
		public static long RealUserId
		{
			get
			{
				return (long)((ulong)Syscall.getuid());
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004823 File Offset: 0x00002A23
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000482F File Offset: 0x00002A2F
		public static UnixGroupInfo EffectiveGroup
		{
			get
			{
				return new UnixGroupInfo(UnixEnvironment.EffectiveGroupId);
			}
			set
			{
				UnixEnvironment.EffectiveGroupId = value.GroupId;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000483C File Offset: 0x00002A3C
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004844 File Offset: 0x00002A44
		public static long EffectiveGroupId
		{
			get
			{
				return (long)((ulong)Syscall.getegid());
			}
			set
			{
				Syscall.setegid(Convert.ToUInt32(value));
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004852 File Offset: 0x00002A52
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000485E File Offset: 0x00002A5E
		public static UnixUserInfo EffectiveUser
		{
			get
			{
				return new UnixUserInfo(UnixEnvironment.EffectiveUserId);
			}
			set
			{
				UnixEnvironment.EffectiveUserId = value.UserId;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000486B File Offset: 0x00002A6B
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004873 File Offset: 0x00002A73
		public static long EffectiveUserId
		{
			get
			{
				return (long)((ulong)Syscall.geteuid());
			}
			set
			{
				Syscall.seteuid(Convert.ToUInt32(value));
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004881 File Offset: 0x00002A81
		public static string Login
		{
			get
			{
				return UnixUserInfo.GetRealUser().UserName;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000488D File Offset: 0x00002A8D
		[CLSCompliant(false)]
		public static long GetConfigurationValue(SysconfName name)
		{
			long num = Syscall.sysconf(name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000048A8 File Offset: 0x00002AA8
		[CLSCompliant(false)]
		public static string GetConfigurationString(ConfstrName name)
		{
			ulong num = Syscall.confstr(name, null, 0UL);
			if (num == 18446744073709551615UL)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			if (num == 0UL)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder((int)num + 1);
			num = Syscall.confstr(name, stringBuilder, num);
			if (num == 18446744073709551615UL)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000048F5 File Offset: 0x00002AF5
		public static void SetNiceValue(int inc)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.nice(inc));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004902 File Offset: 0x00002B02
		public static int CreateSession()
		{
			int num = Syscall.setsid();
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			return num;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000490F File Offset: 0x00002B0F
		public static void SetProcessGroup()
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.setpgrp());
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000491B File Offset: 0x00002B1B
		public static int GetProcessGroup()
		{
			return Syscall.getpgrp();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004924 File Offset: 0x00002B24
		public static UnixGroupInfo[] GetSupplementaryGroups()
		{
			uint[] array = UnixEnvironment._GetSupplementaryGroupIds();
			UnixGroupInfo[] array2 = new UnixGroupInfo[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new UnixGroupInfo((long)((ulong)array[i]));
			}
			return array2;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000495B File Offset: 0x00002B5B
		private static uint[] _GetSupplementaryGroupIds()
		{
			int num = Syscall.getgroups(0, new uint[0]);
			if (num == -1)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			uint[] array = new uint[num];
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.getgroups(array));
			return array;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004984 File Offset: 0x00002B84
		public static void SetSupplementaryGroups(UnixGroupInfo[] groups)
		{
			uint[] array = new uint[groups.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToUInt32(groups[i].GroupId);
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.setgroups(array));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000049C4 File Offset: 0x00002BC4
		public static long[] GetSupplementaryGroupIds()
		{
			uint[] array = UnixEnvironment._GetSupplementaryGroupIds();
			long[] array2 = new long[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (long)((ulong)array[i]);
			}
			return array2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000049F8 File Offset: 0x00002BF8
		public static void SetSupplementaryGroupIds(long[] list)
		{
			uint[] array = new uint[list.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToUInt32(list[i]);
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.setgroups(array));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004A32 File Offset: 0x00002C32
		public static int GetParentProcessId()
		{
			return Syscall.getppid();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004A39 File Offset: 0x00002C39
		public static UnixProcess GetParentProcess()
		{
			return new UnixProcess(UnixEnvironment.GetParentProcessId());
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004A48 File Offset: 0x00002C48
		public static string[] GetUserShells()
		{
			ArrayList arrayList = new ArrayList();
			object usershell_lock = Syscall.usershell_lock;
			lock (usershell_lock)
			{
				try
				{
					if (Syscall.setusershell() != 0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
					string text;
					while ((text = Syscall.getusershell()) != null)
					{
						arrayList.Add(text);
					}
				}
				finally
				{
					Syscall.endusershell();
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
	}
}
