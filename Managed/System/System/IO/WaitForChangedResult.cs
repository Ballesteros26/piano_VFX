using System;

namespace System.IO
{
	/// <summary>Contains information on the change that occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003EB RID: 1003
	public struct WaitForChangedResult
	{
		/// <summary>Gets or sets the type of change that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0007937B File Offset: 0x0007757B
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x00079383 File Offset: 0x00077583
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		/// <summary>Gets or sets the name of the file or directory that changed.</summary>
		/// <returns>The name of the file or directory that changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0007938C File Offset: 0x0007758C
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x00079394 File Offset: 0x00077594
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the original name of the file or directory that was renamed.</summary>
		/// <returns>The original name of the file or directory that was renamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0007939D File Offset: 0x0007759D
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x000793A5 File Offset: 0x000775A5
		public string OldName
		{
			get
			{
				return this.oldName;
			}
			set
			{
				this.oldName = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the wait operation timed out.</summary>
		/// <returns>true if the <see cref="M:System.IO.FileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes)" /> method timed out; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000793AE File Offset: 0x000775AE
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x000793B6 File Offset: 0x000775B6
		public bool TimedOut
		{
			get
			{
				return this.timedOut;
			}
			set
			{
				this.timedOut = value;
			}
		}

		// Token: 0x04001AE2 RID: 6882
		private WatcherChangeTypes changeType;

		// Token: 0x04001AE3 RID: 6883
		private string name;

		// Token: 0x04001AE4 RID: 6884
		private string oldName;

		// Token: 0x04001AE5 RID: 6885
		private bool timedOut;
	}
}
