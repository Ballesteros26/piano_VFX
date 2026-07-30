using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Creates server channel sinks for the server channel through which remoting messages flow.</summary>
	// Token: 0x020007AF RID: 1967
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		/// <returns>The next sink provider in the channel sink provider chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004FE9 RID: 20457
		// (set) Token: 0x06004FEA RID: 20458
		IServerChannelSinkProvider Next { get; set; }

		/// <summary>Creates a sink chain.</summary>
		/// <returns>The first sink of the newly formed channel sink chain, or null, which indicates that this provider will not or cannot provide a connection for this endpoint.</returns>
		/// <param name="channel">The channel for which to create the channel sink chain. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06004FEB RID: 20459
		IServerChannelSink CreateSink(IChannelReceiver channel);

		/// <summary>Returns the channel data for the channel that the current sink is associated with.</summary>
		/// <param name="channelData">A <see cref="T:System.Runtime.Remoting.Channels.IChannelDataStore" /> object in which the channel data is to be returned. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06004FEC RID: 20460
		void GetChannelData(IChannelDataStore channelData);
	}
}
