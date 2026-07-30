using System;

namespace System.IO
{
	/// <summary>Specifies whether the underlying handle is inheritable by child processes.</summary>
	// Token: 0x02000029 RID: 41
	[Serializable]
	public enum HandleInheritability
	{
		/// <summary>Specifies that the handle is not inheritable by child processes.</summary>
		// Token: 0x040001F4 RID: 500
		None,
		/// <summary>Specifies that the handle is inheritable by child processes.</summary>
		// Token: 0x040001F5 RID: 501
		Inheritable
	}
}
