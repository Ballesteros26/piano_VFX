using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies the type of Windows account used.</summary>
	// Token: 0x0200062C RID: 1580
	[ComVisible(true)]
	[Serializable]
	public enum WindowsAccountType
	{
		/// <summary>A standard user account.</summary>
		// Token: 0x040022F0 RID: 8944
		Normal,
		/// <summary>A Windows guest account.</summary>
		// Token: 0x040022F1 RID: 8945
		Guest,
		/// <summary>A Windows system account.</summary>
		// Token: 0x040022F2 RID: 8946
		System,
		/// <summary>An anonymous account.</summary>
		// Token: 0x040022F3 RID: 8947
		Anonymous
	}
}
