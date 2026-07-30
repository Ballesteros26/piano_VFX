using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003CD RID: 973
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system. </param>
		/// <param name="directory">The root directory of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		// Token: 0x06001DC4 RID: 7620 RVA: 0x000765BA File Offset: 0x000747BA
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this.changeType = changeType;
			this.directory = directory;
			this.name = name;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000765D7 File Offset: 0x000747D7
		internal void SetName(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the type of directory event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values that represents the kind of change detected in the file system.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x000765E0 File Offset: 0x000747E0
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		/// <summary>Gets the fully qualifed path of the affected file or directory.</summary>
		/// <returns>The path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x000765E8 File Offset: 0x000747E8
		public string FullPath
		{
			get
			{
				return Path.Combine(this.directory, this.name);
			}
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x000765FB File Offset: 0x000747FB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04001A11 RID: 6673
		private WatcherChangeTypes changeType;

		// Token: 0x04001A12 RID: 6674
		private string directory;

		// Token: 0x04001A13 RID: 6675
		private string name;
	}
}
