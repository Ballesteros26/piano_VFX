using System;
using System.ComponentModel;

namespace System.Net.Sockets
{
	/// <summary>Specifies the method to download the policy file that an instance of the <see cref="T:System.Net.Sockets" /> class will use.</summary>
	// Token: 0x020005CC RID: 1484
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum SocketClientAccessPolicyProtocol
	{
		/// <summary>The <see cref="T:System.Net.Sockets" /> class will attempt to download the socket policy file using custom TCP protocol running on TCP port 943.</summary>
		// Token: 0x04002670 RID: 9840
		Tcp,
		/// <summary>The <see cref="T:System.Net.Sockets" /> class will attempt to download the socket policy file using HTTP protocol running on TCP port 943.</summary>
		// Token: 0x04002671 RID: 9841
		Http
	}
}
