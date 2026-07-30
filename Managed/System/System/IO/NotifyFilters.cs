using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003E4 RID: 996
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x04001AC8 RID: 6856
		Attributes = 4,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x04001AC9 RID: 6857
		CreationTime = 64,
		/// <summary>The name of the directory.</summary>
		// Token: 0x04001ACA RID: 6858
		DirectoryName = 2,
		/// <summary>The name of the file.</summary>
		// Token: 0x04001ACB RID: 6859
		FileName = 1,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x04001ACC RID: 6860
		LastAccess = 32,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x04001ACD RID: 6861
		LastWrite = 16,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x04001ACE RID: 6862
		Security = 256,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x04001ACF RID: 6863
		Size = 8
	}
}
