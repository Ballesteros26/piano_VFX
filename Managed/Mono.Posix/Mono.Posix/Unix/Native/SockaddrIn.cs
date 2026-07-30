using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200006D RID: 109
	[Map("struct sockaddr_in")]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SockaddrIn : Sockaddr, IEquatable<SockaddrIn>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000BC1E File Offset: 0x00009E1E
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000BC26 File Offset: 0x00009E26
		public UnixAddressFamily sin_family
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

		// Token: 0x06000484 RID: 1156 RVA: 0x0000BC2F File Offset: 0x00009E2F
		public SockaddrIn()
			: base(SockaddrType.SockaddrIn, UnixAddressFamily.AF_INET)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000BC39 File Offset: 0x00009E39
		public override string ToString()
		{
			return string.Format("{{sin_family={0}, sin_port=htons({1}), sin_addr={2}}}", base.sa_family, Syscall.ntohs(this.sin_port), this.sin_addr);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public new static SockaddrIn FromSockaddrStorage(SockaddrStorage storage)
		{
			SockaddrIn sockaddrIn = new SockaddrIn();
			storage.CopyTo(sockaddrIn);
			return sockaddrIn;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000BC88 File Offset: 0x00009E88
		public override int GetHashCode()
		{
			return this.sin_family.GetHashCode() ^ this.sin_port.GetHashCode() ^ this.sin_addr.GetHashCode();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000BCC7 File Offset: 0x00009EC7
		public override bool Equals(object obj)
		{
			return obj is SockaddrIn && this.Equals((SockaddrIn)obj);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000BCDF File Offset: 0x00009EDF
		public bool Equals(SockaddrIn value)
		{
			return value != null && (this.sin_family == value.sin_family && this.sin_port == value.sin_port) && this.sin_addr.Equals(value.sin_addr);
		}

		// Token: 0x0400047E RID: 1150
		public ushort sin_port;

		// Token: 0x0400047F RID: 1151
		public InAddr sin_addr;
	}
}
