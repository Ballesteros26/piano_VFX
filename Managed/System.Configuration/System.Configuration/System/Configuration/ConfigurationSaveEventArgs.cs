using System;

namespace System.Configuration
{
	// Token: 0x0200002D RID: 45
	internal class ConfigurationSaveEventArgs : EventArgs
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006753 File Offset: 0x00004953
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000675B File Offset: 0x0000495B
		public string StreamPath { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006764 File Offset: 0x00004964
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000676C File Offset: 0x0000496C
		public bool Start { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006775 File Offset: 0x00004975
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000677D File Offset: 0x0000497D
		public object Context { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006786 File Offset: 0x00004986
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000678E File Offset: 0x0000498E
		public bool Failed { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006797 File Offset: 0x00004997
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000679F File Offset: 0x0000499F
		public Exception Exception { get; private set; }

		// Token: 0x0600019E RID: 414 RVA: 0x000067A8 File Offset: 0x000049A8
		public ConfigurationSaveEventArgs(string streamPath, bool start, Exception ex, object context)
		{
			this.StreamPath = streamPath;
			this.Start = start;
			this.Failed = ex != null;
			this.Exception = ex;
			this.Context = context;
		}
	}
}
