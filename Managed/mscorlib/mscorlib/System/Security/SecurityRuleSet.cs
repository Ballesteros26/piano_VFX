using System;

namespace System.Security
{
	/// <summary>Identifies the set of security rules the common language runtime should enforce for an assembly.  </summary>
	// Token: 0x02000535 RID: 1333
	public enum SecurityRuleSet : byte
	{
		/// <summary>Unsupported. Using this value results in a <see cref="T:System.IO.FileLoadException" /> being thrown.</summary>
		// Token: 0x04001F2E RID: 7982
		None,
		/// <summary>Indicates that the runtime will enforce level 1 (.NET Framework version 2.0) transparency rules.</summary>
		// Token: 0x04001F2F RID: 7983
		Level1,
		/// <summary>Indicates that the runtime will enforce level 2 transparency rules.</summary>
		// Token: 0x04001F30 RID: 7984
		Level2
	}
}
