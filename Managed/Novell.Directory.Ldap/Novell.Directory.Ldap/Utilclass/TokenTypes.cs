using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public enum TokenTypes
	{
		// Token: 0x04000224 RID: 548
		EOL = 10,
		// Token: 0x04000225 RID: 549
		EOF = -1,
		// Token: 0x04000226 RID: 550
		NUMBER = -2,
		// Token: 0x04000227 RID: 551
		WORD = -3,
		// Token: 0x04000228 RID: 552
		REAL = -4,
		// Token: 0x04000229 RID: 553
		STRING = -5
	}
}
