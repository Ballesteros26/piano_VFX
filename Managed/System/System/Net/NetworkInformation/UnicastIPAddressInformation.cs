using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's unicast address.</summary>
	// Token: 0x02000621 RID: 1569
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003202 RID: 12802
		public abstract long AddressPreferredLifetime { get; }

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06003203 RID: 12803
		public abstract long AddressValidLifetime { get; }

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003204 RID: 12804
		public abstract long DhcpLeaseLifetime { get; }

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06003205 RID: 12805
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06003206 RID: 12806
		public abstract PrefixOrigin PrefixOrigin { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003207 RID: 12807
		public abstract SuffixOrigin SuffixOrigin { get; }

		/// <summary>Gets the IPv4 mask.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IPv4 mask.</returns>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003208 RID: 12808
		public abstract IPAddress IPv4Mask { get; }

		/// <summary>Gets the length, in bits, of the prefix or network part of the IP address.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.the length, in bits, of the prefix or network part of the IP address.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x00004239 File Offset: 0x00002439
		public virtual int PrefixLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
