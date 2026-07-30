using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Used to control how <see cref="T:System.Net.NetworkInformation.Ping" /> data packets are transmitted.</summary>
	// Token: 0x02000614 RID: 1556
	public class PingOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class and sets the Time to Live and fragmentation values.</summary>
		/// <param name="ttl">An <see cref="T:System.Int32" /> value greater than zero that specifies the number of times that the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded.</param>
		/// <param name="dontFragment">true to prevent data sent to the remote host from being fragmented; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ttl " />is less than or equal to zero.</exception>
		// Token: 0x060031C2 RID: 12738 RVA: 0x000BE120 File Offset: 0x000BC320
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class.</summary>
		// Token: 0x060031C3 RID: 12739 RVA: 0x000BE150 File Offset: 0x000BC350
		public PingOptions()
		{
		}

		/// <summary>Gets or sets the number of routing nodes that can forward the <see cref="T:System.Net.NetworkInformation.Ping" /> data before it is discarded.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the number of times the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded. The default is 128.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000BE163 File Offset: 0x000BC363
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000BE16B File Offset: 0x000BC36B
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls fragmentation of the data sent to the remote host.</summary>
		/// <returns>true if the data cannot be sent in multiple packets; otherwise false. The default is false.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000BE183 File Offset: 0x000BC383
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000BE18B File Offset: 0x000BC38B
		public bool DontFragment
		{
			get
			{
				return this.dontFragment;
			}
			set
			{
				this.dontFragment = value;
			}
		}

		// Token: 0x040027FD RID: 10237
		private const int DontFragmentFlag = 2;

		// Token: 0x040027FE RID: 10238
		private int ttl = 128;

		// Token: 0x040027FF RID: 10239
		private bool dontFragment;
	}
}
