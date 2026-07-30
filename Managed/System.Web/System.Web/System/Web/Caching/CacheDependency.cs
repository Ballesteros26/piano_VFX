using System;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Unity;

namespace System.Web.Caching
{
	/// <summary>Establishes a dependency relationship between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and a file, cache key, an array of either, or another <see cref="T:System.Web.Caching.CacheDependency" /> object. The <see cref="T:System.Web.Caching.CacheDependency" /> class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.</summary>
	// Token: 0x0200067E RID: 1662
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CacheDependency : IDisposable
	{
		// Token: 0x14000115 RID: 277
		// (add) Token: 0x0600470C RID: 18188 RVA: 0x000C7CE7 File Offset: 0x000C5EE7
		// (remove) Token: 0x0600470D RID: 18189 RVA: 0x000C7CFA File Offset: 0x000C5EFA
		internal event EventHandler DependencyChanged
		{
			add
			{
				this.events.AddHandler(CacheDependency.dependencyChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(CacheDependency.dependencyChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class.</summary>
		// Token: 0x0600470E RID: 18190 RVA: 0x000C7D0D File Offset: 0x000C5F0D
		protected CacheDependency()
			: this(null, null, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors a file or directory for changes.</summary>
		/// <param name="filename">The path to a file or directory that the cached object is dependent upon. When this resource changes, the cached object becomes obsolete and is removed from the cache. </param>
		// Token: 0x0600470F RID: 18191 RVA: 0x000C7D1D File Offset: 0x000C5F1D
		public CacheDependency(string filename)
			: this(new string[] { filename }, null, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories) for changes.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		// Token: 0x06004710 RID: 18192 RVA: 0x000C7D36 File Offset: 0x000C5F36
		public CacheDependency(string[] filenames)
			: this(filenames, null, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors a file or directory for changes.</summary>
		/// <param name="filename">The path to a file or directory that the cached object is dependent upon. When this resource changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="start">The time against which to check the last modified date of the directory or file. </param>
		// Token: 0x06004711 RID: 18193 RVA: 0x000C7D46 File Offset: 0x000C5F46
		public CacheDependency(string filename, DateTime start)
			: this(new string[] { filename }, null, null, start)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories) for changes and specifies a time when change monitoring begins.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="start">The time against which to check the last modified date of the objects in the array. </param>
		// Token: 0x06004712 RID: 18194 RVA: 0x000C7D5B File Offset: 0x000C5F5B
		public CacheDependency(string[] filenames, DateTime start)
			: this(filenames, null, null, start)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		// Token: 0x06004713 RID: 18195 RVA: 0x000C7D67 File Offset: 0x000C5F67
		public CacheDependency(string[] filenames, string[] cachekeys)
			: this(filenames, cachekeys, null, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes. It also makes itself dependent upon a separate instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		/// <param name="dependency">Another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that this instance is dependent upon. </param>
		// Token: 0x06004714 RID: 18196 RVA: 0x000C7D77 File Offset: 0x000C5F77
		public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency)
			: this(filenames, cachekeys, dependency, DateTime.Now)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		/// <param name="start">The date and time against which to check the last modified date of the objects passed in the <paramref name="filenames" /> and <paramref name="cachekeys" /> arrays. </param>
		// Token: 0x06004715 RID: 18197 RVA: 0x000C7D87 File Offset: 0x000C5F87
		public CacheDependency(string[] filenames, string[] cachekeys, DateTime start)
			: this(filenames, cachekeys, null, start)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes. It also makes itself dependent upon another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class and a time when the change monitoring begins.</summary>
		/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
		/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
		/// <param name="dependency">Another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that this instance is dependent upon. </param>
		/// <param name="start">The time against which to check the last modified date of the objects in the arrays and the <see cref="T:System.Web.Caching.CacheDependency" /> object. </param>
		// Token: 0x06004716 RID: 18198 RVA: 0x000C7D94 File Offset: 0x000C5F94
		public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency, DateTime start)
		{
			int num = ((filenames != null) ? filenames.Length : 0);
			if (num > 0)
			{
				this.watchers = new FileSystemWatcher[num];
				for (int i = 0; i < num; i++)
				{
					string text = filenames[i];
					if (!string.IsNullOrEmpty(text))
					{
						FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
						if (Directory.Exists(text))
						{
							fileSystemWatcher.Path = text;
						}
						else
						{
							string directoryName = Path.GetDirectoryName(text);
							if (directoryName == null || !Directory.Exists(directoryName))
							{
								goto IL_00F8;
							}
							fileSystemWatcher.Path = directoryName;
							fileSystemWatcher.Filter = Path.GetFileName(text);
						}
						fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
						fileSystemWatcher.Created += this.OnChanged;
						fileSystemWatcher.Changed += this.OnChanged;
						fileSystemWatcher.Deleted += this.OnChanged;
						fileSystemWatcher.Renamed += new RenamedEventHandler(this.OnChanged);
						fileSystemWatcher.EnableRaisingEvents = true;
						this.watchers[i] = fileSystemWatcher;
					}
					IL_00F8:;
				}
			}
			this.cachekeys = cachekeys;
			this.dependency = dependency;
			if (dependency != null)
			{
				dependency.DependencyChanged += this.OnChildDependencyChanged;
			}
			this.start = start;
			this.FinishInit();
		}

		/// <summary>Retrieves a unique identifier for a <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
		/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.CacheDependency" /> object.</returns>
		// Token: 0x06004717 RID: 18199 RVA: 0x000C7ED8 File Offset: 0x000C60D8
		public virtual string GetUniqueID()
		{
			StringBuilder stringBuilder = new StringBuilder();
			object obj = this.locker;
			lock (obj)
			{
				if (this.watchers != null)
				{
					foreach (FileSystemWatcher fileSystemWatcher in this.watchers)
					{
						if (fileSystemWatcher != null && fileSystemWatcher.Path != null && fileSystemWatcher.Path.Length != 0)
						{
							stringBuilder.Append("_" + fileSystemWatcher.Path);
						}
					}
				}
			}
			if (this.cachekeys != null)
			{
				foreach (string text in this.cachekeys)
				{
					stringBuilder.AppendFormat("_" + text, Array.Empty<object>());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000C7FBC File Offset: 0x000C61BC
		private void OnChanged(object sender, FileSystemEventArgs args)
		{
			this.OnDependencyChanged(sender, args);
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x000C7FC8 File Offset: 0x000C61C8
		private bool DoOnChanged()
		{
			DateTime now = DateTime.Now;
			if (now < this.start)
			{
				return false;
			}
			this.hasChanged = true;
			this.utcLastModified = now.ToUniversalTime();
			this.DisposeWatchers();
			if (this.cache != null)
			{
				this.cache.CheckDependencies();
			}
			return true;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x000C801C File Offset: 0x000C621C
		private void DisposeWatchers()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.watchers != null)
				{
					foreach (FileSystemWatcher fileSystemWatcher in this.watchers)
					{
						if (fileSystemWatcher != null)
						{
							fileSystemWatcher.Dispose();
						}
					}
				}
				this.watchers = null;
			}
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
		// Token: 0x0600471B RID: 18203 RVA: 0x000C808C File Offset: 0x000C628C
		public void Dispose()
		{
			this.DependencyDispose();
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x0000393A File Offset: 0x00001B3A
		internal virtual void DependencyDisposeInternal()
		{
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Web.Caching.CacheDependency" /> class and any classes that derive from <see cref="T:System.Web.Caching.CacheDependency" />.</summary>
		// Token: 0x0600471D RID: 18205 RVA: 0x000C8094 File Offset: 0x000C6294
		protected virtual void DependencyDispose()
		{
			this.DependencyDisposeInternal();
			this.DisposeWatchers();
			if (this.dependency != null)
			{
				this.dependency.DependencyChanged -= this.OnChildDependencyChanged;
				this.dependency.Dispose();
			}
			this.cache = null;
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x000C80D3 File Offset: 0x000C62D3
		internal void SetCache(Cache c)
		{
			this.cache = c;
			this.used = c != null;
		}

		/// <summary>Completes initialization of the <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
		// Token: 0x0600471F RID: 18207 RVA: 0x000C80E6 File Offset: 0x000C62E6
		protected internal void FinishInit()
		{
			this.utcLastModified = DateTime.UtcNow;
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x000C80F3 File Offset: 0x000C62F3
		internal bool IsUsed
		{
			get
			{
				return this.used;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x000C80FB File Offset: 0x000C62FB
		// (set) Token: 0x06004722 RID: 18210 RVA: 0x000C8103 File Offset: 0x000C6303
		internal DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		/// <summary>Gets the time when the dependency was last changed.</summary>
		/// <returns>The time when the dependency was last changed.</returns>
		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x000C810C File Offset: 0x000C630C
		public DateTime UtcLastModified
		{
			get
			{
				return this.utcLastModified;
			}
		}

		/// <summary>Marks the time when a dependency last changed.</summary>
		/// <param name="utcLastModified">The time when the dependency last changed. </param>
		// Token: 0x06004724 RID: 18212 RVA: 0x000C8114 File Offset: 0x000C6314
		protected void SetUtcLastModified(DateTime utcLastModified)
		{
			this.utcLastModified = utcLastModified;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.Caching.CacheDependency" /> object has changed.</summary>
		/// <returns>true if the <see cref="T:System.Web.Caching.CacheDependency" /> object has changed; otherwise, false. The default is false.</returns>
		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x000C8120 File Offset: 0x000C6320
		public bool HasChanged
		{
			get
			{
				if (this.hasChanged)
				{
					return true;
				}
				if (DateTime.Now < this.start)
				{
					return false;
				}
				if (this.cache != null && this.cachekeys != null)
				{
					foreach (string text in this.cachekeys)
					{
						if (this.cache.GetKeyLastChange(text) > this.start)
						{
							this.hasChanged = true;
							break;
						}
					}
				}
				if (this.hasChanged)
				{
					this.DisposeWatchers();
				}
				return this.hasChanged;
			}
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000C81AA File Offset: 0x000C63AA
		private void OnChildDependencyChanged(object o, EventArgs e)
		{
			this.hasChanged = true;
			this.OnDependencyChanged(o, e);
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000C81BC File Offset: 0x000C63BC
		private void OnDependencyChanged(object sender, EventArgs e)
		{
			if (!this.DoOnChanged())
			{
				return;
			}
			EventHandler eventHandler = this.events[CacheDependency.dependencyChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		/// <summary>Notifies the base <see cref="T:System.Web.Caching.CacheDependency" /> object that the dependency represented by a derived <see cref="T:System.Web.Caching.CacheDependency" /> class has changed.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06004728 RID: 18216 RVA: 0x000C7FBC File Offset: 0x000C61BC
		protected void NotifyDependencyChanged(object sender, EventArgs e)
		{
			this.OnDependencyChanged(sender, e);
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string[] GetFileDependencies()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void ItemRemoved()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void KeepDependenciesAlive()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetCacheDependencyChanged(Action<object, EventArgs> dependencyChangedAction)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000C8200 File Offset: 0x000C6400
		public bool TakeOwnership()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04002565 RID: 9573
		private static readonly object dependencyChangedEvent = new object();

		// Token: 0x04002566 RID: 9574
		private string[] cachekeys;

		// Token: 0x04002567 RID: 9575
		private CacheDependency dependency;

		// Token: 0x04002568 RID: 9576
		private DateTime start;

		// Token: 0x04002569 RID: 9577
		private Cache cache;

		// Token: 0x0400256A RID: 9578
		private FileSystemWatcher[] watchers;

		// Token: 0x0400256B RID: 9579
		private bool hasChanged;

		// Token: 0x0400256C RID: 9580
		private bool used;

		// Token: 0x0400256D RID: 9581
		private DateTime utcLastModified;

		// Token: 0x0400256E RID: 9582
		private object locker = new object();

		// Token: 0x0400256F RID: 9583
		private EventHandlerList events = new EventHandlerList();
	}
}
