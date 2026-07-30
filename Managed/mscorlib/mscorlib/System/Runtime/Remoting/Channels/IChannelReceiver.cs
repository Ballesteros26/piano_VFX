using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides required functions and properties for the receiver channels.</summary>
	// Token: 0x020007A3 RID: 1955
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		/// <summary>Gets the channel-specific data.</summary>
		/// <returns>The channel data.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004FCC RID: 20428
		object ChannelData { get; }

		/// <summary>Returns an array of all the URLs for a URI.</summary>
		/// <returns>An array of the URLs.</returns>
		/// <param name="objectURI">The URI for which URLs are required. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06004FCD RID: 20429
		string[] GetUrlsForUri(string objectURI);

		/// <summary>Instructs the current channel to start listening for requests.</summary>
		/// <param name="data">Optional initialization information. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06004FCE RID: 20430
		void StartListening(object data);

		/// <summary>Instructs the current channel to stop listening for requests.</summary>
		/// <param name="data">Optional state information for the channel. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06004FCF RID: 20431
		void StopListening(object data);
	}
}
