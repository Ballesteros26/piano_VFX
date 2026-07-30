using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	/// <summary>Provides envoy information.</summary>
	// Token: 0x0200074C RID: 1868
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		/// <summary>Gets or sets the list of envoys that were contributed by the server context and object chains when the object was marshaled.</summary>
		/// <returns>A chain of envoy sinks.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004D39 RID: 19769
		// (set) Token: 0x06004D3A RID: 19770
		IMessageSink EnvoySinks { get; set; }
	}
}
