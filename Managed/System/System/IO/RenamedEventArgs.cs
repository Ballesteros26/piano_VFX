using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003E6 RID: 998
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values. </param>
		/// <param name="directory">The name of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		/// <param name="oldName">The old name of the affected file or directory. </param>
		// Token: 0x06001E57 RID: 7767 RVA: 0x00079065 File Offset: 0x00077265
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName)
			: base(changeType, directory, name)
		{
			this.oldName = oldName;
			this.oldFullPath = Path.Combine(directory, oldName);
		}

		/// <summary>Gets the previous fully qualified path of the affected file or directory.</summary>
		/// <returns>The previous fully qualified path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x00079086 File Offset: 0x00077286
		public string OldFullPath
		{
			get
			{
				return this.oldFullPath;
			}
		}

		/// <summary>Gets the old name of the affected file or directory.</summary>
		/// <returns>The previous name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0007908E File Offset: 0x0007728E
		public string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x04001AD1 RID: 6865
		private string oldName;

		// Token: 0x04001AD2 RID: 6866
		private string oldFullPath;
	}
}
