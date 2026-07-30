using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the status and data resulting from a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> operation.</summary>
	// Token: 0x02000615 RID: 1557
	public class PingReply
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x000020EB File Offset: 0x000002EB
		internal PingReply()
		{
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000BE194 File Offset: 0x000BC394
		internal PingReply(IPStatus ipStatus)
		{
			this.ipStatus = ipStatus;
			this.buffer = new byte[0];
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000BE1B0 File Offset: 0x000BC3B0
		internal PingReply(byte[] data, int dataLength, IPAddress address, int time)
		{
			this.address = address;
			this.rtt = (long)time;
			this.ipStatus = this.GetIPStatus((IcmpV4Type)data[20], (IcmpV4Code)data[21]);
			if (this.ipStatus == IPStatus.Success)
			{
				this.buffer = new byte[dataLength - 28];
				Array.Copy(data, 28, this.buffer, 0, dataLength - 28);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000BE21E File Offset: 0x000BC41E
		internal PingReply(IPAddress address, byte[] buffer, PingOptions options, long roundtripTime, IPStatus status)
		{
			this.address = address;
			this.buffer = buffer;
			this.options = options;
			this.rtt = roundtripTime;
			this.ipStatus = status;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000BE24C File Offset: 0x000BC44C
		private IPStatus GetIPStatus(IcmpV4Type type, IcmpV4Code code)
		{
			switch (type)
			{
			case IcmpV4Type.ICMP4_ECHO_REPLY:
				return IPStatus.Success;
			case (IcmpV4Type)1:
			case (IcmpV4Type)2:
				break;
			case IcmpV4Type.ICMP4_DST_UNREACH:
				switch (code)
				{
				case IcmpV4Code.ICMP4_UNREACH_NET:
					return IPStatus.DestinationNetworkUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_HOST:
					return IPStatus.DestinationHostUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PROTOCOL:
					return IPStatus.DestinationProtocolUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PORT:
					return IPStatus.DestinationPortUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_FRAG_NEEDED:
					return IPStatus.PacketTooBig;
				default:
					return IPStatus.DestinationUnreachable;
				}
				break;
			case IcmpV4Type.ICMP4_SOURCE_QUENCH:
				return IPStatus.SourceQuench;
			default:
				if (type == IcmpV4Type.ICMP4_TIME_EXCEEDED)
				{
					return IPStatus.TtlExpired;
				}
				if (type == IcmpV4Type.ICMP4_PARAM_PROB)
				{
					return IPStatus.ParameterProblem;
				}
				break;
			}
			return IPStatus.Unknown;
		}

		/// <summary>Gets the status of an attempt to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPStatus" /> value indicating the result of the request.</returns>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000BE2D4 File Offset: 0x000BC4D4
		public IPStatus Status
		{
			get
			{
				return this.ipStatus;
			}
		}

		/// <summary>Gets the address of the host that sends the Internet Control Message Protocol (ICMP) echo reply.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> containing the destination for the ICMP echo message.</returns>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000BE2DC File Offset: 0x000BC4DC
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the number of milliseconds taken to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Int64" /> that specifies the round trip time, in milliseconds. </returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000BE2E4 File Offset: 0x000BC4E4
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		/// <summary>Gets the options used to transmit the reply to an Internet Control Message Protocol (ICMP) echo request.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object that contains the Time to Live (TTL) and the fragmentation directive used for transmitting the reply if <see cref="P:System.Net.NetworkInformation.PingReply.Status" /> is <see cref="F:System.Net.NetworkInformation.IPStatus.Success" />; otherwise, null.</returns>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000BE2EC File Offset: 0x000BC4EC
		public PingOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets the buffer of data received in an Internet Control Message Protocol (ICMP) echo reply message.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the data received in an ICMP echo reply message, or an empty array, if no reply was received.</returns>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000BE2F4 File Offset: 0x000BC4F4
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x04002800 RID: 10240
		private IPAddress address;

		// Token: 0x04002801 RID: 10241
		private PingOptions options;

		// Token: 0x04002802 RID: 10242
		private IPStatus ipStatus;

		// Token: 0x04002803 RID: 10243
		private long rtt;

		// Token: 0x04002804 RID: 10244
		private byte[] buffer;
	}
}
