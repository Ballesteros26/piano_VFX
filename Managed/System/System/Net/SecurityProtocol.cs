using System;
using System.Security.Authentication;

namespace System.Net
{
	// Token: 0x02000416 RID: 1046
	internal static class SecurityProtocol
	{
		// Token: 0x06001FF0 RID: 8176 RVA: 0x0007CCB9 File Offset: 0x0007AEB9
		public static void ThrowOnNotAllowed(SslProtocols protocols, bool allowNone = true)
		{
			if ((!allowNone && protocols == SslProtocols.None) || (protocols & ~(SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12)) != SslProtocols.None)
			{
				throw new NotSupportedException("The requested security protocol is not supported.");
			}
		}

		// Token: 0x04001BA7 RID: 7079
		public const SslProtocols AllowedSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

		// Token: 0x04001BA8 RID: 7080
		public const SslProtocols DefaultSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

		// Token: 0x04001BA9 RID: 7081
		public const SslProtocols SystemDefaultSecurityProtocols = SslProtocols.None;
	}
}
