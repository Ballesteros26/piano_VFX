using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the protection level for the key in user interface (UI) prompting scenarios.</summary>
	// Token: 0x02000075 RID: 117
	[Flags]
	public enum CngUIProtectionLevels
	{
		/// <summary>No UI prompt is displayed when the key is accessed.</summary>
		// Token: 0x040002DC RID: 732
		None = 0,
		/// <summary>A UI prompt is displayed the first time the key is accessed in a process.</summary>
		// Token: 0x040002DD RID: 733
		ProtectKey = 1,
		/// <summary>A UI prompt is displayed every time the key is accessed.</summary>
		// Token: 0x040002DE RID: 734
		ForceHighProtection = 2
	}
}
