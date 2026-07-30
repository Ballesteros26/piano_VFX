using System;
using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
	/// <summary>Enumerates the levels of isolated storage scope that are supported by <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" />.</summary>
	// Token: 0x020003EF RID: 1007
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum IsolatedStorageScope
	{
		/// <summary>No isolated storage usage.</summary>
		// Token: 0x04001866 RID: 6246
		None = 0,
		/// <summary>Isolated storage scoped by user identity.</summary>
		// Token: 0x04001867 RID: 6247
		User = 1,
		/// <summary>Isolated storage scoped to the application domain identity.</summary>
		// Token: 0x04001868 RID: 6248
		Domain = 2,
		/// <summary>Isolated storage scoped to the identity of the assembly.</summary>
		// Token: 0x04001869 RID: 6249
		Assembly = 4,
		/// <summary>The isolated store can be placed in a location on the file system that might roam (if roaming user data is enabled on the underlying operating system).</summary>
		// Token: 0x0400186A RID: 6250
		Roaming = 8,
		/// <summary>Isolated storage scoped to the machine.</summary>
		// Token: 0x0400186B RID: 6251
		Machine = 16,
		/// <summary>Isolated storage scoped to the application.</summary>
		// Token: 0x0400186C RID: 6252
		Application = 32
	}
}
