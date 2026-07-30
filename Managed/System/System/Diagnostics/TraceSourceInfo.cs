using System;

namespace System.Diagnostics
{
	// Token: 0x0200021C RID: 540
	internal class TraceSourceInfo
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x0004B724 File Offset: 0x00049924
		public TraceSourceInfo(string name, SourceLevels levels)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0004B745 File Offset: 0x00049945
		internal TraceSourceInfo(string name, SourceLevels levels, TraceImplSettings settings)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener
			{
				IndentSize = settings.IndentSize
			});
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0004B783 File Offset: 0x00049983
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0004B78B File Offset: 0x0004998B
		public SourceLevels Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0004B793 File Offset: 0x00049993
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		// Token: 0x04001205 RID: 4613
		private string name;

		// Token: 0x04001206 RID: 4614
		private SourceLevels levels;

		// Token: 0x04001207 RID: 4615
		private TraceListenerCollection listeners;
	}
}
