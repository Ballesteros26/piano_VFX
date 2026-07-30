using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides the base interface for channel sinks.</summary>
	// Token: 0x020007A6 RID: 1958
	[ComVisible(true)]
	public interface IChannelSinkBase
	{
		/// <summary>Gets a dictionary through which properties on the sink can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink can be accessed, or null if the channel sink does not support properties.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004FD5 RID: 20437
		IDictionary Properties { get; }
	}
}
