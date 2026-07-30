using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Indicates that the implementing channel wants to hook into the outside listener service.</summary>
	// Token: 0x020007A4 RID: 1956
	[ComVisible(true)]
	public interface IChannelReceiverHook
	{
		/// <summary>Gets the type of listener to hook into.</summary>
		/// <returns>The type of listener to hook into (for example, "http").</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004FD0 RID: 20432
		string ChannelScheme { get; }

		/// <summary>Gets the channel sink chain that the current channel is using.</summary>
		/// <returns>The channel sink chain that the current channel is using.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06004FD1 RID: 20433
		IServerChannelSink ChannelSinkChain { get; }

		/// <summary>Gets a Boolean value that indicates whether <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> needs to be hooked into the outside listener service.</summary>
		/// <returns>A Boolean value that indicates whether <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> needs to be hooked into the outside listener service.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06004FD2 RID: 20434
		bool WantsToListen { get; }

		/// <summary>Adds a URI on which the channel hook will listen.</summary>
		/// <param name="channelUri">A URI on which the channel hook will listen. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06004FD3 RID: 20435
		void AddHookChannelUri(string channelUri);
	}
}
