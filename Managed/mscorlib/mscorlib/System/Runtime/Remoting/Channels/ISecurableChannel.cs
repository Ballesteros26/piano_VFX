using System;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>The <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> contains one property, <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />, which gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
	// Token: 0x020007AD RID: 1965
	public interface ISecurableChannel
	{
		/// <summary>Gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
		/// <returns>A Boolean value that indicates whether the current channel is secure.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004FE3 RID: 20451
		// (set) Token: 0x06004FE4 RID: 20452
		bool IsSecured { get; set; }
	}
}
