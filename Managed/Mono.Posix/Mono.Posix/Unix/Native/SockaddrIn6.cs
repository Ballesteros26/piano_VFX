using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200006E RID: 110
	[Map("struct sockaddr_in6")]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SockaddrIn6 : Sockaddr, IEquatable<SockaddrIn6>
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000BD15 File Offset: 0x00009F15
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000BD1D File Offset: 0x00009F1D
		public UnixAddressFamily sin6_family
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

		// Token: 0x0600048C RID: 1164 RVA: 0x0000BD26 File Offset: 0x00009F26
		public SockaddrIn6()
			: base(SockaddrType.SockaddrIn6, UnixAddressFamily.AF_INET6)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000BD34 File Offset: 0x00009F34
		public override string ToString()
		{
			return string.Format("{{sin6_family={0}, sin6_port=htons({1}), sin6_flowinfo={2}, sin6_addr={3}, sin6_scope_id={4}}}", new object[]
			{
				base.sa_family,
				Syscall.ntohs(this.sin6_port),
				this.sin6_flowinfo,
				this.sin6_addr,
				this.sin6_scope_id
			});
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public new static SockaddrIn6 FromSockaddrStorage(SockaddrStorage storage)
		{
			SockaddrIn6 sockaddrIn = new SockaddrIn6();
			storage.CopyTo(sockaddrIn);
			return sockaddrIn;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public override int GetHashCode()
		{
			return this.sin6_family.GetHashCode() ^ this.sin6_port.GetHashCode() ^ this.sin6_flowinfo.GetHashCode() ^ this.sin6_addr.GetHashCode() ^ this.sin6_scope_id.GetHashCode();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000BE0F File Offset: 0x0000A00F
		public override bool Equals(object obj)
		{
			return obj is SockaddrIn6 && this.Equals((SockaddrIn6)obj);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000BE28 File Offset: 0x0000A028
		public bool Equals(SockaddrIn6 value)
		{
			return value != null && (this.sin6_family == value.sin6_family && this.sin6_port == value.sin6_port && this.sin6_flowinfo == value.sin6_flowinfo && this.sin6_addr.Equals(value.sin6_addr)) && this.sin6_scope_id == value.sin6_scope_id;
		}

		// Token: 0x04000480 RID: 1152
		public ushort sin6_port;

		// Token: 0x04000481 RID: 1153
		public uint sin6_flowinfo;

		// Token: 0x04000482 RID: 1154
		public In6Addr sin6_addr;

		// Token: 0x04000483 RID: 1155
		public uint sin6_scope_id;
	}
}
