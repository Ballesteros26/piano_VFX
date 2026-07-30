using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200006C RID: 108
	[CLSCompliant(false)]
	public sealed class SockaddrUn : Sockaddr, IEquatable<SockaddrUn>
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B9C5 File Offset: 0x00009BC5
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000B9CD File Offset: 0x00009BCD
		public UnixAddressFamily sun_family
		{
			get
			{
				return base.sa_family;
			}
			set
			{
				base.sa_family = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000B9D6 File Offset: 0x00009BD6
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000B9DE File Offset: 0x00009BDE
		public byte[] sun_path { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000B9E7 File Offset: 0x00009BE7
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000B9EF File Offset: 0x00009BEF
		public long sun_path_len { get; set; }

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		internal override byte[] DynamicData()
		{
			return this.sun_path;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000BA00 File Offset: 0x00009C00
		internal override long GetDynamicLength()
		{
			return this.sun_path_len;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000BA08 File Offset: 0x00009C08
		internal override void SetDynamicLength(long value)
		{
			this.sun_path_len = value;
		}

		// Token: 0x06000476 RID: 1142
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_SockaddrUn_get_sizeof_sun_path", SetLastError = true)]
		private static extern int get_sizeof_sun_path();

		// Token: 0x06000477 RID: 1143 RVA: 0x0000BA11 File Offset: 0x00009C11
		public SockaddrUn()
			: base((SockaddrType)32770, UnixAddressFamily.AF_UNIX)
		{
			this.sun_path = new byte[SockaddrUn.sizeof_sun_path];
			this.sun_path_len = 0L;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000BA37 File Offset: 0x00009C37
		public SockaddrUn(int size)
			: base((SockaddrType)32770, UnixAddressFamily.AF_UNIX)
		{
			this.sun_path = new byte[size];
			this.sun_path_len = 0L;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000BA5C File Offset: 0x00009C5C
		public SockaddrUn(string path, bool linuxAbstractNamespace = false)
			: base((SockaddrType)32770, UnixAddressFamily.AF_UNIX)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			byte[] bytes = UnixEncoding.Instance.GetBytes(path);
			if (linuxAbstractNamespace)
			{
				this.sun_path = new byte[1 + bytes.Length];
				Array.Copy(bytes, 0, this.sun_path, 1, bytes.Length);
			}
			else
			{
				this.sun_path = bytes;
			}
			this.sun_path_len = (long)this.sun_path.Length;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000BACB File Offset: 0x00009CCB
		public bool IsLinuxAbstractNamespace
		{
			get
			{
				return this.sun_path_len > 0L && this.sun_path[0] == 0;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public string Path
		{
			get
			{
				int num = (this.IsLinuxAbstractNamespace ? 1 : 0);
				int num2 = 0;
				while ((long)(num + num2) < this.sun_path_len && this.sun_path[num + num2] != 0)
				{
					num2++;
				}
				return UnixEncoding.Instance.GetString(this.sun_path, num, num2);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000BB30 File Offset: 0x00009D30
		public override string ToString()
		{
			return string.Format("{{sa_family={0}, sun_path=\"{1}{2}\"}}", base.sa_family, this.IsLinuxAbstractNamespace ? "\\0" : "", this.Path);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000BB64 File Offset: 0x00009D64
		public new static SockaddrUn FromSockaddrStorage(SockaddrStorage storage)
		{
			SockaddrUn sockaddrUn = new SockaddrUn((int)storage.data_len);
			storage.CopyTo(sockaddrUn);
			return sockaddrUn;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000BB88 File Offset: 0x00009D88
		public override int GetHashCode()
		{
			return this.sun_family.GetHashCode() ^ this.IsLinuxAbstractNamespace.GetHashCode() ^ this.Path.GetHashCode();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public override bool Equals(object obj)
		{
			return obj is SockaddrUn && this.Equals((SockaddrUn)obj);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public bool Equals(SockaddrUn value)
		{
			return value != null && (this.sun_family == value.sun_family && this.IsLinuxAbstractNamespace == value.IsLinuxAbstractNamespace) && this.Path == value.Path;
		}

		// Token: 0x0400047D RID: 1149
		private static readonly int sizeof_sun_path = SockaddrUn.get_sizeof_sun_path();
	}
}
