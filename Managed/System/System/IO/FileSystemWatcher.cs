using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003CF RID: 975
	[IODescription("")]
	[DefaultEvent("Changed")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x06001DCD RID: 7629 RVA: 0x00076604 File Offset: 0x00074804
		public FileSystemWatcher()
		{
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.enableRaisingEvents = false;
			this.filter = "*.*";
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.path = "";
			this.InitWatcher();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06001DCE RID: 7630 RVA: 0x00076654 File Offset: 0x00074854
		public FileSystemWatcher(string path)
			: this(path, "*.*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory and type of files to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.-or- The <paramref name="filter" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06001DCF RID: 7631 RVA: 0x00076664 File Offset: 0x00074864
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Empty path", "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException("Directory does not exist", "path");
			}
			this.enableRaisingEvents = false;
			this.filter = filter;
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.path = path;
			this.synchronizingObject = null;
			this.InitWatcher();
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00076704 File Offset: 0x00074904
		[EnvironmentPermission(SecurityAction.Assert, Read = "MONO_MANAGED_WATCHER")]
		private void InitWatcher()
		{
			object obj = FileSystemWatcher.lockobj;
			lock (obj)
			{
				if (FileSystemWatcher.watcher == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("MONO_MANAGED_WATCHER");
					int num = 0;
					if (environmentVariable == null)
					{
						num = FileSystemWatcher.InternalSupportsFSW();
					}
					bool flag2 = false;
					switch (num)
					{
					case 1:
						flag2 = DefaultWatcher.GetInstance(out FileSystemWatcher.watcher);
						break;
					case 2:
						flag2 = FAMWatcher.GetInstance(out FileSystemWatcher.watcher, false);
						break;
					case 3:
						flag2 = KeventWatcher.GetInstance(out FileSystemWatcher.watcher);
						break;
					case 4:
						flag2 = FAMWatcher.GetInstance(out FileSystemWatcher.watcher, true);
						break;
					case 5:
						flag2 = InotifyWatcher.GetInstance(out FileSystemWatcher.watcher, true);
						break;
					}
					if (num == 0 || !flag2)
					{
						if (string.Compare(environmentVariable, "disabled", true) == 0)
						{
							NullFileWatcher.GetInstance(out FileSystemWatcher.watcher);
						}
						else
						{
							DefaultWatcher.GetInstance(out FileSystemWatcher.watcher);
						}
					}
				}
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000767F4 File Offset: 0x000749F4
		[Conditional("TRACE")]
		[Conditional("DEBUG")]
		private void ShowWatcherInfo()
		{
			Console.WriteLine("Watcher implementation: {0}", (FileSystemWatcher.watcher != null) ? FileSystemWatcher.watcher.GetType().ToString() : "<none>");
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0007681D File Offset: 0x00074A1D
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x00076825 File Offset: 0x00074A25
		internal bool Waiting
		{
			get
			{
				return this.waiting;
			}
			set
			{
				this.waiting = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00076830 File Offset: 0x00074A30
		internal string MangledFilter
		{
			get
			{
				if (this.filter != "*.*")
				{
					return this.filter;
				}
				if (this.mangledFilter != null)
				{
					return this.mangledFilter;
				}
				string text = "*.*";
				if (!(FileSystemWatcher.watcher.GetType() == typeof(WindowsWatcher)))
				{
					text = "*";
				}
				return text;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x00076890 File Offset: 0x00074A90
		internal SearchPattern2 Pattern
		{
			get
			{
				if (this.pattern == null)
				{
					if (FileSystemWatcher.watcher.GetType() == typeof(KeventWatcher))
					{
						this.pattern = new SearchPattern2(this.MangledFilter, true);
					}
					else
					{
						this.pattern = new SearchPattern2(this.MangledFilter);
					}
				}
				return this.pattern;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000768EC File Offset: 0x00074AEC
		internal string FullPath
		{
			get
			{
				if (this.fullpath == null)
				{
					if (this.path == null || this.path == "")
					{
						this.fullpath = Environment.CurrentDirectory;
					}
					else
					{
						this.fullpath = global::System.IO.Path.GetFullPath(this.path);
					}
				}
				return this.fullpath;
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>true if the component is enabled; otherwise, false. The default is false. If you are using the component on a designer in Visual Studio 2005, the default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0007693F File Offset: 0x00074B3F
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x00076947 File Offset: 0x00074B47
		[IODescription("Flag to indicate if this instance is active")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				if (value == this.enableRaisingEvents)
				{
					return;
				}
				this.enableRaisingEvents = value;
				if (value)
				{
					this.Start();
					return;
				}
				this.Stop();
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.) </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0007696A File Offset: 0x00074B6A
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x00076972 File Offset: 0x00074B72
		[DefaultValue("*.*")]
		[IODescription("File name filter pattern")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null || value == "")
				{
					value = "*.*";
				}
				if (this.filter != value)
				{
					this.filter = value;
					this.pattern = null;
					this.mangledFilter = null;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>true if you want to monitor subdirectories; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000769AE File Offset: 0x00074BAE
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x000769B6 File Offset: 0x00074BB6
		[IODescription("Flag to indicate we want to watch subdirectories")]
		[DefaultValue(false)]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories == value)
				{
					return;
				}
				this.includeSubdirectories = value;
				if (value && this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the size (in bytes) of the internal buffer.</summary>
		/// <returns>The internal buffer size in bytes. The default is 8192 (8 KB).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000769E0 File Offset: 0x00074BE0
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000769E8 File Offset: 0x00074BE8
		[DefaultValue(8192)]
		[Browsable(false)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize == value)
				{
					return;
				}
				if (value < 4196)
				{
					value = 4196;
				}
				this.internalBufferSize = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of LastWrite, FileName, and DirectoryName.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00076A1E File Offset: 0x00074C1E
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x00076A26 File Offset: 0x00074C26
		[DefaultValue(NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite)]
		[IODescription("Flag to indicate which change event we want to monitor")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilter;
			}
			set
			{
				if (this.notifyFilter == value)
				{
					return;
				}
				this.notifyFilter = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.-or- The specified path contains wildcard characters.-or- The specified path contains invalid path characters.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00076A4D File Offset: 0x00074C4D
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x00076A58 File Offset: 0x00074C58
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[IODescription("The directory to monitor")]
		[SettingsBindable(true)]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (this.path == value)
				{
					return;
				}
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = Directory.Exists(value);
				}
				catch (Exception ex)
				{
				}
				if (ex != null)
				{
					throw new ArgumentException("Invalid directory name", "value", ex);
				}
				if (!flag)
				{
					throw new ArgumentException("Directory does not exist", "value");
				}
				this.path = value;
				this.fullpath = null;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0002858C File Offset: 0x0002678C
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00076AE0 File Offset: 0x00074CE0
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of a directory change.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> that represents the object used to marshal the event handler calls issued as a result of a directory change. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00076AE9 File Offset: 0x00074CE9
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x00076AF1 File Offset: 0x00074CF1
		[IODescription("The object used to marshal the event handler calls resulting from a directory change")]
		[Browsable(false)]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x000027E8 File Offset: 0x000009E8
		public void BeginInit()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x00076AFA File Offset: 0x00074CFA
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Stop();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00076B18 File Offset: 0x00074D18
		~FileSystemWatcher()
		{
			this.disposed = true;
			this.Stop();
		}

		/// <summary>Ends the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DEA RID: 7658 RVA: 0x000027E8 File Offset: 0x000009E8
		public void EndInit()
		{
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00076B4C File Offset: 0x00074D4C
		private void RaiseEvent(Delegate ev, EventArgs arg, FileSystemWatcher.EventType evtype)
		{
			if (ev == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				foreach (Delegate @delegate in ev.GetInvocationList())
				{
					switch (evtype)
					{
					case FileSystemWatcher.EventType.FileSystemEvent:
						((FileSystemEventHandler)@delegate).BeginInvoke(this, (FileSystemEventArgs)arg, null, null);
						break;
					case FileSystemWatcher.EventType.ErrorEvent:
						((ErrorEventHandler)@delegate).BeginInvoke(this, (ErrorEventArgs)arg, null, null);
						break;
					case FileSystemWatcher.EventType.RenameEvent:
						((RenamedEventHandler)@delegate).BeginInvoke(this, (RenamedEventArgs)arg, null, null);
						break;
					}
				}
				return;
			}
			this.synchronizingObject.BeginInvoke(ev, new object[] { this, arg });
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06001DEC RID: 7660 RVA: 0x00076BF1 File Offset: 0x00074DF1
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Changed, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06001DED RID: 7661 RVA: 0x00076C01 File Offset: 0x00074E01
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Created, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x06001DEE RID: 7662 RVA: 0x00076C11 File Offset: 0x00074E11
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Deleted, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x06001DEF RID: 7663 RVA: 0x00076C21 File Offset: 0x00074E21
		protected void OnError(ErrorEventArgs e)
		{
			this.RaiseEvent(this.Error, e, FileSystemWatcher.EventType.ErrorEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001DF0 RID: 7664 RVA: 0x00076C31 File Offset: 0x00074E31
		protected void OnRenamed(RenamedEventArgs e)
		{
			this.RaiseEvent(this.Renamed, e, FileSystemWatcher.EventType.RenameEvent);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF1 RID: 7665 RVA: 0x00076C41 File Offset: 0x00074E41
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <param name="timeout">The time (in milliseconds) to wait before timing out. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF2 RID: 7666 RVA: 0x00076C4C File Offset: 0x00074E4C
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			WaitForChangedResult waitForChangedResult = default(WaitForChangedResult);
			bool flag = this.EnableRaisingEvents;
			if (!flag)
			{
				this.EnableRaisingEvents = true;
			}
			bool flag3;
			lock (this)
			{
				this.waiting = true;
				flag3 = Monitor.Wait(this, timeout);
				if (flag3)
				{
					waitForChangedResult = this.lastData;
				}
			}
			this.EnableRaisingEvents = flag;
			if (!flag3)
			{
				waitForChangedResult.TimedOut = true;
			}
			return waitForChangedResult;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00076CC8 File Offset: 0x00074EC8
		internal void DispatchErrorEvents(ErrorEventArgs args)
		{
			this.OnError(args);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00076CD4 File Offset: 0x00074ED4
		internal void DispatchEvents(FileAction act, string filename, ref RenamedEventArgs renamed)
		{
			if (this.waiting)
			{
				this.lastData = default(WaitForChangedResult);
			}
			switch (act)
			{
			case FileAction.Added:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Created;
				this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, this.path, filename));
				return;
			case FileAction.Removed:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Deleted;
				this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, this.path, filename));
				return;
			case FileAction.Modified:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Changed;
				this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, this.path, filename));
				return;
			case FileAction.RenamedOldName:
				if (renamed != null)
				{
					this.OnRenamed(renamed);
				}
				this.lastData.OldName = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, filename, "");
				return;
			case FileAction.RenamedNewName:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				if (renamed == null)
				{
					renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, "", filename);
				}
				this.OnRenamed(renamed);
				renamed = null;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00076E0A File Offset: 0x0007500A
		private void Start()
		{
			FileSystemWatcher.watcher.StartDispatching(this);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00076E17 File Offset: 0x00075017
		private void Stop()
		{
			FileSystemWatcher.watcher.StopDispatching(this);
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001DF7 RID: 7671 RVA: 0x00076E24 File Offset: 0x00075024
		// (remove) Token: 0x06001DF8 RID: 7672 RVA: 0x00076E5C File Offset: 0x0007505C
		[IODescription("Occurs when a file/directory change matches the filter")]
		public event FileSystemEventHandler Changed;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06001DF9 RID: 7673 RVA: 0x00076E94 File Offset: 0x00075094
		// (remove) Token: 0x06001DFA RID: 7674 RVA: 0x00076ECC File Offset: 0x000750CC
		[IODescription("Occurs when a file/directory creation matches the filter")]
		public event FileSystemEventHandler Created;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001DFB RID: 7675 RVA: 0x00076F04 File Offset: 0x00075104
		// (remove) Token: 0x06001DFC RID: 7676 RVA: 0x00076F3C File Offset: 0x0007513C
		[IODescription("Occurs when a file/directory deletion matches the filter")]
		public event FileSystemEventHandler Deleted;

		/// <summary>Occurs when the instance of <see cref="T:System.IO.FileSystemWatcher" /> is unable to continue monitoring changes or when the internal buffer overflows.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001DFD RID: 7677 RVA: 0x00076F74 File Offset: 0x00075174
		// (remove) Token: 0x06001DFE RID: 7678 RVA: 0x00076FAC File Offset: 0x000751AC
		[Browsable(false)]
		public event ErrorEventHandler Error;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is renamed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001DFF RID: 7679 RVA: 0x00076FE4 File Offset: 0x000751E4
		// (remove) Token: 0x06001E00 RID: 7680 RVA: 0x0007701C File Offset: 0x0007521C
		[IODescription("Occurs when a file/directory rename matches the filter")]
		public event RenamedEventHandler Renamed;

		// Token: 0x06001E01 RID: 7681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalSupportsFSW();

		// Token: 0x04001A14 RID: 6676
		private bool enableRaisingEvents;

		// Token: 0x04001A15 RID: 6677
		private string filter;

		// Token: 0x04001A16 RID: 6678
		private bool includeSubdirectories;

		// Token: 0x04001A17 RID: 6679
		private int internalBufferSize;

		// Token: 0x04001A18 RID: 6680
		private NotifyFilters notifyFilter;

		// Token: 0x04001A19 RID: 6681
		private string path;

		// Token: 0x04001A1A RID: 6682
		private string fullpath;

		// Token: 0x04001A1B RID: 6683
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04001A1C RID: 6684
		private WaitForChangedResult lastData;

		// Token: 0x04001A1D RID: 6685
		private bool waiting;

		// Token: 0x04001A1E RID: 6686
		private SearchPattern2 pattern;

		// Token: 0x04001A1F RID: 6687
		private bool disposed;

		// Token: 0x04001A20 RID: 6688
		private string mangledFilter;

		// Token: 0x04001A21 RID: 6689
		private static IFileWatcher watcher;

		// Token: 0x04001A22 RID: 6690
		private static object lockobj = new object();

		// Token: 0x020003D0 RID: 976
		private enum EventType
		{
			// Token: 0x04001A29 RID: 6697
			FileSystemEvent,
			// Token: 0x04001A2A RID: 6698
			ErrorEvent,
			// Token: 0x04001A2B RID: 6699
			RenameEvent
		}
	}
}
