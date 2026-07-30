using System;

namespace System.Diagnostics
{
	// Token: 0x02000205 RID: 517
	internal class NullEventLog : EventLogImpl
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x00049F9C File Offset: 0x0004819C
		public NullEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void BeginInit()
		{
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Clear()
		{
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Close()
		{
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Delete(string logName, string machineName)
		{
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void DeleteEventSource(string source, string machineName)
		{
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Dispose(bool disposing)
		{
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void DisableNotification()
		{
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void EnableNotification()
		{
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void EndInit()
		{
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool Exists(string logName, string machineName)
		{
			return true;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0004A1BE File Offset: 0x000483BE
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00004240 File Offset: 0x00002440
		protected override int GetEntryCount()
		{
			return 0;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00009E57 File Offset: 0x00008057
		protected override EventLogEntry GetEntry(int index)
		{
			return null;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0004A3D8 File Offset: 0x000485D8
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0004A9D8 File Offset: 0x00048BD8
		protected override string[] GetLogNames(string machineName)
		{
			return new string[0];
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00009E57 File Offset: 0x00008057
		public override string LogNameFromSourceName(string source, string machineName)
		{
			return null;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00004240 File Offset: 0x00002440
		public override bool SourceExists(string source, string machineName)
		{
			return false;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0004A8BF File Offset: 0x00048ABF
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0004A8C6 File Offset: 0x00048AC6
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x0004A8D1 File Offset: 0x00048AD1
		public override long MaximumKilobytes
		{
			get
			{
				return long.MaxValue;
			}
			set
			{
				throw new NotSupportedException("This EventLog implementation does not support setting max kilobytes policy");
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004A8DD File Offset: 0x00048ADD
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0004A8E9 File Offset: 0x00048AE9
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}
	}
}
