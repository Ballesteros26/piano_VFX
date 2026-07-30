using System;
using System.Security.Cryptography;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x0200049C RID: 1180
	internal class SessionId
	{
		// Token: 0x06003592 RID: 13714 RVA: 0x0008BE5C File Offset: 0x0008A05C
		internal static string Create()
		{
			byte[] array = new byte[12];
			RandomNumberGenerator randomNumberGenerator = SessionId.rng;
			lock (randomNumberGenerator)
			{
				SessionId.rng.GetBytes(array);
			}
			return MachineKeySectionUtils.GetHexString(array);
		}

		// Token: 0x04001D5B RID: 7515
		internal const int IdLength = 24;

		// Token: 0x04001D5C RID: 7516
		private const int half_len = 12;

		// Token: 0x04001D5D RID: 7517
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();
	}
}
