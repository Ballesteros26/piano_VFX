using System;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace System.Net
{
	/// <summary>Provides an Internet Protocol (IP) address.</summary>
	// Token: 0x02000431 RID: 1073
	[Serializable]
	public class IPAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as an <see cref="T:System.Int64" />.</summary>
		/// <param name="newAddress">The long value of the IP address. For example, the value 0x2414188f in big-endian format would be the IP address "143.24.20.36". </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newAddress" /> &lt; 0 or <paramref name="newAddress" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x0600204C RID: 8268 RVA: 0x0007DF1C File Offset: 0x0007C11C
		public IPAddress(long newAddress)
		{
			if (newAddress < 0L || newAddress > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("newAddress");
			}
			this.m_Address = newAddress;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array and the specified scope identifier.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <param name="scopeid">The long value of the scope identifier. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeid" /> &lt; 0 or <paramref name="scopeid" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x0600204D RID: 8269 RVA: 0x0007DF54 File Offset: 0x0007C154
		public IPAddress(byte[] address, long scopeid)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length != 16)
			{
				throw new ArgumentException(global::SR.GetString("An invalid IP address was specified."), "address");
			}
			this.m_Family = AddressFamily.InterNetworkV6;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)((int)address[i * 2] * 256 + (int)address[i * 2 + 1]);
			}
			if (scopeid < 0L || scopeid > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("scopeid");
			}
			this.m_ScopeId = scopeid;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0007DFF0 File Offset: 0x0007C1F0
		private IPAddress(ushort[] address, uint scopeid)
		{
			this.m_Family = AddressFamily.InterNetworkV6;
			this.m_Numbers = address;
			this.m_ScopeId = (long)((ulong)scopeid);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		// Token: 0x0600204F RID: 8271 RVA: 0x0007E024 File Offset: 0x0007C224
		public IPAddress(byte[] address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length != 4 && address.Length != 16)
			{
				throw new ArgumentException(global::SR.GetString("An invalid IP address was specified."), "address");
			}
			if (address.Length == 4)
			{
				this.m_Family = AddressFamily.InterNetwork;
				this.m_Address = (long)(((int)address[3] << 24) | ((int)address[2] << 16) | ((int)address[1] << 8) | (int)address[0]) & (long)((ulong)(-1));
				return;
			}
			this.m_Family = AddressFamily.InterNetworkV6;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)((int)address[i * 2] * 256 + (int)address[i * 2 + 1]);
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0007E0D9 File Offset: 0x0007C2D9
		internal IPAddress(int newAddress)
		{
			this.m_Address = (long)newAddress & (long)((ulong)(-1));
		}

		/// <summary>Determines whether a string is a valid IP address.</summary>
		/// <returns>true if <paramref name="ipString" /> is a valid IP address; otherwise, false.</returns>
		/// <param name="ipString">The string to validate.</param>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> version of the string.</param>
		// Token: 0x06002051 RID: 8273 RVA: 0x0007E0FF File Offset: 0x0007C2FF
		public static bool TryParse(string ipString, out IPAddress address)
		{
			address = IPAddress.InternalParse(ipString, true);
			return address != null;
		}

		/// <summary>Converts an IP address string to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance.</returns>
		/// <param name="ipString">A string that contains an IP address in dotted-quad notation for IPv4 and in colon-hexadecimal notation for IPv6. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="ipString" /> is not a valid IP address. </exception>
		// Token: 0x06002052 RID: 8274 RVA: 0x0007E10F File Offset: 0x0007C30F
		public static IPAddress Parse(string ipString)
		{
			return IPAddress.InternalParse(ipString, false);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0007E118 File Offset: 0x0007C318
		private unsafe static IPAddress InternalParse(string ipString, bool tryParse)
		{
			if (ipString == null)
			{
				if (tryParse)
				{
					return null;
				}
				throw new ArgumentNullException("ipString");
			}
			else if (ipString.IndexOf(':') != -1)
			{
				int num = 0;
				if (ipString[0] != '[')
				{
					ipString += "]";
				}
				else
				{
					num = 1;
				}
				int length = ipString.Length;
				fixed (string text = ipString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (IPv6AddressHelper.IsValidStrict(ptr, num, ref length) || length != ipString.Length)
					{
						ushort[] array = new ushort[8];
						string text2 = null;
						ushort[] array2;
						ushort* ptr2;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array2[0];
						}
						IPv6AddressHelper.Parse(ipString, ptr2, 0, ref text2);
						array2 = null;
						if (text2 == null || text2.Length == 0)
						{
							return new IPAddress(array, 0U);
						}
						text2 = text2.Substring(1);
						uint num2;
						if (uint.TryParse(text2, NumberStyles.None, null, out num2))
						{
							return new IPAddress(array, num2);
						}
						return new IPAddress(array, 0U);
					}
					else
					{
						text = null;
						if (tryParse)
						{
							return null;
						}
						SocketException ex = new SocketException(SocketError.InvalidArgument);
						throw new FormatException(global::SR.GetString("An invalid IP address was specified."), ex);
					}
				}
			}
			else
			{
				int length2 = ipString.Length;
				long num3;
				fixed (string text = ipString)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					num3 = IPv4AddressHelper.ParseNonCanonical(ptr3, 0, ref length2, true);
				}
				if (num3 != -1L && length2 == ipString.Length)
				{
					num3 = ((num3 & 255L) << 24) | (((num3 & 65280L) << 8) | (((num3 & 16711680L) >> 8) | ((num3 & (long)((ulong)(-16777216))) >> 24)));
					return new IPAddress(num3);
				}
				if (tryParse)
				{
					return null;
				}
				throw new FormatException(global::SR.GetString("An invalid IP address was specified."));
			}
		}

		/// <summary>An Internet Protocol (IP) address.</summary>
		/// <returns>The long value of the IP address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0007E2BC File Offset: 0x0007C4BC
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x0007E2D9 File Offset: 0x0007C4D9
		[Obsolete("This property has been deprecated. It is address family dependent. Please use IPAddress.Equals method to perform comparisons. http://go.microsoft.com/fwlink/?linkid=14202")]
		public long Address
		{
			get
			{
				if (this.m_Family == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return this.m_Address;
			}
			set
			{
				if (this.m_Family == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if (this.m_Address != value)
				{
					this.m_ToString = null;
					this.m_Address = value;
				}
			}
		}

		/// <summary>Provides a copy of the <see cref="T:System.Net.IPAddress" /> as an array of bytes.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06002056 RID: 8278 RVA: 0x0007E308 File Offset: 0x0007C508
		public byte[] GetAddressBytes()
		{
			byte[] array;
			if (this.m_Family == AddressFamily.InterNetworkV6)
			{
				array = new byte[16];
				int num = 0;
				for (int i = 0; i < 8; i++)
				{
					array[num++] = (byte)((this.m_Numbers[i] >> 8) & 255);
					array[num++] = (byte)(this.m_Numbers[i] & 255);
				}
			}
			else
			{
				array = new byte[]
				{
					(byte)this.m_Address,
					(byte)(this.m_Address >> 8),
					(byte)(this.m_Address >> 16),
					(byte)(this.m_Address >> 24)
				};
			}
			return array;
		}

		/// <summary>Gets the address family of the IP address.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> for IPv4 or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> for IPv6.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0007E39D File Offset: 0x0007C59D
		public AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Gets or sets the IPv6 address scope identifier.</summary>
		/// <returns>A long integer that specifies the scope of the address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">AddressFamily = InterNetwork. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeId" /> &lt; 0- or -<paramref name="scopeId" /> &gt; 0x00000000FFFFFFFF  </exception>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0007E3A5 File Offset: 0x0007C5A5
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x0007E3C4 File Offset: 0x0007C5C4
		public long ScopeId
		{
			get
			{
				if (this.m_Family == AddressFamily.InterNetwork)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return this.m_ScopeId;
			}
			set
			{
				if (this.m_Family == AddressFamily.InterNetwork)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.m_ScopeId != value)
				{
					this.m_Address = value;
					this.m_ScopeId = value;
				}
			}
		}

		/// <summary>Converts an Internet address to its standard notation.</summary>
		/// <returns>A string that contains the IP address in either IPv4 dotted-quad or in IPv6 colon-hexadecimal notation.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> and the address is bad. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600205A RID: 8282 RVA: 0x0007E414 File Offset: 0x0007C614
		public unsafe override string ToString()
		{
			if (this.m_ToString == null)
			{
				if (this.m_Family == AddressFamily.InterNetworkV6)
				{
					IPv6AddressFormatter pv6AddressFormatter = new IPv6AddressFormatter(this.m_Numbers, this.ScopeId);
					this.m_ToString = pv6AddressFormatter.ToString();
				}
				else
				{
					int num = 15;
					char* ptr = stackalloc char[(UIntPtr)30];
					int num2 = (int)((this.m_Address >> 24) & 255L);
					do
					{
						ptr[(IntPtr)(--num) * 2] = (char)(48 + num2 % 10);
						num2 /= 10;
					}
					while (num2 > 0);
					ptr[(IntPtr)(--num) * 2] = '.';
					num2 = (int)((this.m_Address >> 16) & 255L);
					do
					{
						ptr[(IntPtr)(--num) * 2] = (char)(48 + num2 % 10);
						num2 /= 10;
					}
					while (num2 > 0);
					ptr[(IntPtr)(--num) * 2] = '.';
					num2 = (int)((this.m_Address >> 8) & 255L);
					do
					{
						ptr[(IntPtr)(--num) * 2] = (char)(48 + num2 % 10);
						num2 /= 10;
					}
					while (num2 > 0);
					ptr[(IntPtr)(--num) * 2] = '.';
					num2 = (int)(this.m_Address & 255L);
					do
					{
						ptr[(IntPtr)(--num) * 2] = (char)(48 + num2 % 10);
						num2 /= 10;
					}
					while (num2 > 0);
					this.m_ToString = new string(ptr, num, 15 - num);
				}
			}
			return this.m_ToString;
		}

		/// <summary>Converts a long value from host byte order to network byte order.</summary>
		/// <returns>A long value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x0600205B RID: 8283 RVA: 0x0007E55C File Offset: 0x0007C75C
		public static long HostToNetworkOrder(long host)
		{
			return (((long)IPAddress.HostToNetworkOrder((int)host) & (long)((ulong)(-1))) << 32) | ((long)IPAddress.HostToNetworkOrder((int)(host >> 32)) & (long)((ulong)(-1)));
		}

		/// <summary>Converts an integer value from host byte order to network byte order.</summary>
		/// <returns>An integer value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x0600205C RID: 8284 RVA: 0x0007E57B File Offset: 0x0007C77B
		public static int HostToNetworkOrder(int host)
		{
			return (((int)IPAddress.HostToNetworkOrder((short)host) & 65535) << 16) | ((int)IPAddress.HostToNetworkOrder((short)(host >> 16)) & 65535);
		}

		/// <summary>Converts a short value from host byte order to network byte order.</summary>
		/// <returns>A short value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x0600205D RID: 8285 RVA: 0x0007E59E File Offset: 0x0007C79E
		public static short HostToNetworkOrder(short host)
		{
			return (short)(((int)(host & 255) << 8) | ((host >> 8) & 255));
		}

		/// <summary>Converts a long value from network byte order to host byte order.</summary>
		/// <returns>A long value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x0600205E RID: 8286 RVA: 0x0007E5B4 File Offset: 0x0007C7B4
		public static long NetworkToHostOrder(long network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts an integer value from network byte order to host byte order.</summary>
		/// <returns>An integer value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x0600205F RID: 8287 RVA: 0x0007E5BC File Offset: 0x0007C7BC
		public static int NetworkToHostOrder(int network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts a short value from network byte order to host byte order.</summary>
		/// <returns>A short value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06002060 RID: 8288 RVA: 0x0007E5C4 File Offset: 0x0007C7C4
		public static short NetworkToHostOrder(short network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Indicates whether the specified IP address is the loopback address.</summary>
		/// <returns>true if <paramref name="address" /> is the loopback address; otherwise, false.</returns>
		/// <param name="address">An IP address. </param>
		// Token: 0x06002061 RID: 8289 RVA: 0x0007E5CC File Offset: 0x0007C7CC
		public static bool IsLoopback(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.m_Family == AddressFamily.InterNetworkV6)
			{
				return address.Equals(IPAddress.IPv6Loopback);
			}
			return (address.m_Address & 255L) == (IPAddress.Loopback.m_Address & 255L);
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x0007E61D File Offset: 0x0007C81D
		internal bool IsBroadcast
		{
			get
			{
				return this.m_Family != AddressFamily.InterNetworkV6 && this.m_Address == IPAddress.Broadcast.m_Address;
			}
		}

		/// <summary>Gets whether the address is an IPv6 multicast global address.</summary>
		/// <returns>true if the IP address is an IPv6 multicast global address; otherwise, false.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x0007E63D File Offset: 0x0007C83D
		public bool IsIPv6Multicast
		{
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && (this.m_Numbers[0] & 65280) == 65280;
			}
		}

		/// <summary>Gets whether the address is an IPv6 link local address.</summary>
		/// <returns>true if the IP address is an IPv6 link local address; otherwise, false.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0007E660 File Offset: 0x0007C860
		public bool IsIPv6LinkLocal
		{
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && (this.m_Numbers[0] & 65472) == 65152;
			}
		}

		/// <summary>Gets whether the address is an IPv6 site local address.</summary>
		/// <returns>true if the IP address is an IPv6 site local address; otherwise, false.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0007E683 File Offset: 0x0007C883
		public bool IsIPv6SiteLocal
		{
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && (this.m_Numbers[0] & 65472) == 65216;
			}
		}

		/// <summary>Gets whether the address is an IPv6 Teredo address.</summary>
		/// <returns>true if the IP address is an IPv6 Teredo address; otherwise, false.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x0007E6A6 File Offset: 0x0007C8A6
		public bool IsIPv6Teredo
		{
			get
			{
				return this.m_Family == AddressFamily.InterNetworkV6 && this.m_Numbers[0] == 8193 && this.m_Numbers[1] == 0;
			}
		}

		/// <summary>Gets whether the IP address is an IPv4-mapped IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the IP address is an IPv4-mapped IPv6 address; otherwise, false.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0007E6D0 File Offset: 0x0007C8D0
		public bool IsIPv4MappedToIPv6
		{
			get
			{
				if (this.AddressFamily != AddressFamily.InterNetworkV6)
				{
					return false;
				}
				for (int i = 0; i < 5; i++)
				{
					if (this.m_Numbers[i] != 0)
					{
						return false;
					}
				}
				return this.m_Numbers[5] == ushort.MaxValue;
			}
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0007E710 File Offset: 0x0007C910
		internal bool Equals(object comparandObj, bool compareScopeId)
		{
			IPAddress ipaddress = comparandObj as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.m_Family != ipaddress.m_Family)
			{
				return false;
			}
			if (this.m_Family == AddressFamily.InterNetworkV6)
			{
				for (int i = 0; i < 8; i++)
				{
					if (ipaddress.m_Numbers[i] != this.m_Numbers[i])
					{
						return false;
					}
				}
				return ipaddress.m_ScopeId == this.m_ScopeId || !compareScopeId;
			}
			return ipaddress.m_Address == this.m_Address;
		}

		/// <summary>Compares two IP addresses.</summary>
		/// <returns>true if the two addresses are equal; otherwise, false.</returns>
		/// <param name="comparand">An <see cref="T:System.Net.IPAddress" /> instance to compare to the current instance. </param>
		// Token: 0x06002069 RID: 8297 RVA: 0x0007E788 File Offset: 0x0007C988
		public override bool Equals(object comparand)
		{
			return this.Equals(comparand, true);
		}

		/// <summary>Returns a hash value for an IP address.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600206A RID: 8298 RVA: 0x0007E792 File Offset: 0x0007C992
		public override int GetHashCode()
		{
			if (this.m_Family == AddressFamily.InterNetworkV6)
			{
				if (this.m_HashCode == 0)
				{
					this.m_HashCode = StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
				}
				return this.m_HashCode;
			}
			return (int)this.m_Address;
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0007E7CC File Offset: 0x0007C9CC
		internal IPAddress Snapshot()
		{
			AddressFamily family = this.m_Family;
			if (family == AddressFamily.InterNetwork)
			{
				return new IPAddress(this.m_Address);
			}
			if (family != AddressFamily.InterNetworkV6)
			{
				throw new InternalException();
			}
			return new IPAddress(this.m_Numbers, (uint)this.m_ScopeId);
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv6 address.</returns>
		// Token: 0x0600206C RID: 8300 RVA: 0x0007E810 File Offset: 0x0007CA10
		public IPAddress MapToIPv6()
		{
			if (this.AddressFamily == AddressFamily.InterNetworkV6)
			{
				return this;
			}
			return new IPAddress(new ushort[]
			{
				0,
				0,
				0,
				0,
				0,
				ushort.MaxValue,
				(ushort)(((this.m_Address & 65280L) >> 8) | ((this.m_Address & 255L) << 8)),
				(ushort)(((this.m_Address & (long)((ulong)(-16777216))) >> 24) | ((this.m_Address & 16711680L) >> 8))
			}, 0U);
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv4 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv4 address.</returns>
		// Token: 0x0600206D RID: 8301 RVA: 0x0007E884 File Offset: 0x0007CA84
		public IPAddress MapToIPv4()
		{
			if (this.AddressFamily == AddressFamily.InterNetwork)
			{
				return this;
			}
			return new IPAddress((long)((ulong)(((uint)(this.m_Numbers[6] & 65280) >> 8) | (uint)((uint)(this.m_Numbers[6] & 255) << 8) | ((((uint)(this.m_Numbers[7] & 65280) >> 8) | (uint)((uint)(this.m_Numbers[7] & 255) << 8)) << 16))));
		}

		/// <summary>Provides an IP address that indicates that the server must listen for client activity on all network interfaces. This field is read-only.</summary>
		// Token: 0x04001C95 RID: 7317
		public static readonly IPAddress Any = new IPAddress(0);

		/// <summary>Provides the IP loopback address. This field is read-only.</summary>
		// Token: 0x04001C96 RID: 7318
		public static readonly IPAddress Loopback = new IPAddress(16777343);

		/// <summary>Provides the IP broadcast address. This field is read-only.</summary>
		// Token: 0x04001C97 RID: 7319
		public static readonly IPAddress Broadcast = new IPAddress((long)((ulong)(-1)));

		/// <summary>Provides an IP address that indicates that no network interface should be used. This field is read-only.</summary>
		// Token: 0x04001C98 RID: 7320
		public static readonly IPAddress None = IPAddress.Broadcast;

		// Token: 0x04001C99 RID: 7321
		internal const long LoopbackMask = 255L;

		// Token: 0x04001C9A RID: 7322
		internal long m_Address;

		// Token: 0x04001C9B RID: 7323
		[NonSerialized]
		internal string m_ToString;

		/// <summary>The <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> method uses the <see cref="F:System.Net.IPAddress.IPv6Any" /> field to indicate that a <see cref="T:System.Net.Sockets.Socket" /> must listen for client activity on all network interfaces.</summary>
		// Token: 0x04001C9C RID: 7324
		public static readonly IPAddress IPv6Any = new IPAddress(new byte[16], 0L);

		/// <summary>Provides the IP loopback address. This property is read-only.</summary>
		// Token: 0x04001C9D RID: 7325
		public static readonly IPAddress IPv6Loopback = new IPAddress(new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 1
		}, 0L);

		/// <summary>Provides an IP address that indicates that no network interface should be used. This property is read-only.</summary>
		// Token: 0x04001C9E RID: 7326
		public static readonly IPAddress IPv6None = new IPAddress(new byte[16], 0L);

		// Token: 0x04001C9F RID: 7327
		private AddressFamily m_Family = AddressFamily.InterNetwork;

		// Token: 0x04001CA0 RID: 7328
		private ushort[] m_Numbers = new ushort[8];

		// Token: 0x04001CA1 RID: 7329
		private long m_ScopeId;

		// Token: 0x04001CA2 RID: 7330
		private int m_HashCode;

		// Token: 0x04001CA3 RID: 7331
		internal const int IPv4AddressBytes = 4;

		// Token: 0x04001CA4 RID: 7332
		internal const int IPv6AddressBytes = 16;

		// Token: 0x04001CA5 RID: 7333
		internal const int NumberOfLabels = 8;
	}
}
