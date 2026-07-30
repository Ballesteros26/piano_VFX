using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x02000603 RID: 1539
	public abstract class IPv6InterfaceProperties
	{
		/// <summary>Gets the index of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the index of the network interface for IPv6 address.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003136 RID: 12598
		public abstract int Index { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003137 RID: 12599
		public abstract int Mtu { get; }

		/// <summary>Gets the scope ID of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The scope ID of the network interface associated with an IPv6 address.</returns>
		/// <param name="scopeLevel">The scope level.</param>
		// Token: 0x06003138 RID: 12600 RVA: 0x00004239 File Offset: 0x00002439
		public virtual long GetScopeId(ScopeLevel scopeLevel)
		{
			throw new NotImplementedException();
		}
	}
}
