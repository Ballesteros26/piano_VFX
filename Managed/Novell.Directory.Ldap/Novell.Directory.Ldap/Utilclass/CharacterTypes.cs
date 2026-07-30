using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000044 RID: 68
	[CLSCompliant(false)]
	[Serializable]
	public enum CharacterTypes : sbyte
	{
		// Token: 0x0400019E RID: 414
		WHITESPACE = 1,
		// Token: 0x0400019F RID: 415
		NUMERIC,
		// Token: 0x040001A0 RID: 416
		ALPHABETIC = 4,
		// Token: 0x040001A1 RID: 417
		STRINGQUOTE = 8,
		// Token: 0x040001A2 RID: 418
		COMMENTCHAR = 16
	}
}
