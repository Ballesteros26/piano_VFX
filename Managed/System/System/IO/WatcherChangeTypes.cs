using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003EC RID: 1004
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x04001AE7 RID: 6887
		All = 15,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x04001AE8 RID: 6888
		Changed = 4,
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x04001AE9 RID: 6889
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x04001AEA RID: 6890
		Deleted = 2,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x04001AEB RID: 6891
		Renamed = 8
	}
}
