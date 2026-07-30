using System;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x020001F5 RID: 501
	internal abstract class EventLogImpl
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x0004916C File Offset: 0x0004736C
		protected EventLogImpl(EventLog coreEventLog)
		{
			this._coreEventLog = coreEventLog;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0004917B File Offset: 0x0004737B
		protected EventLog CoreEventLog
		{
			get
			{
				return this._coreEventLog;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00049184 File Offset: 0x00047384
		public int EntryCount
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", this._coreEventLog.Log, this._coreEventLog.MachineName));
				}
				return this.GetEntryCount();
			}
		}

		// Token: 0x17000324 RID: 804
		public EventLogEntry this[int index]
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", this._coreEventLog.Log, this._coreEventLog.MachineName));
				}
				if (index < 0 || index >= this.EntryCount)
				{
					throw new ArgumentException("Index out of range");
				}
				return this.GetEntry(index);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000492AC File Offset: 0x000474AC
		public string LogDisplayName
		{
			get
			{
				if (this._coreEventLog.Log != null && this._coreEventLog.Log.Length == 0)
				{
					throw new InvalidOperationException("Event log names must consist of printable characters and cannot contain \\, *, ?, or spaces.");
				}
				if (this._coreEventLog.Log != null)
				{
					if (this._coreEventLog.Log.Length == 0)
					{
						return string.Empty;
					}
					if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot find Log {0} on computer {1}.", this._coreEventLog.Log, this._coreEventLog.MachineName));
					}
				}
				return this.GetLogDisplayName();
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00049358 File Offset: 0x00047558
		public EventLogEntry[] GetEntries()
		{
			string log = this.CoreEventLog.Log;
			if (log == null || log.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (!EventLog.Exists(log))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", log, this._coreEventLog.MachineName));
			}
			int entryCount = this.GetEntryCount();
			EventLogEntry[] array = new EventLogEntry[entryCount];
			for (int i = 0; i < entryCount; i++)
			{
				array[i] = this.GetEntry(i);
			}
			return array;
		}

		// Token: 0x0600100E RID: 4110
		public abstract void DisableNotification();

		// Token: 0x0600100F RID: 4111
		public abstract void EnableNotification();

		// Token: 0x06001010 RID: 4112
		public abstract void BeginInit();

		// Token: 0x06001011 RID: 4113
		public abstract void Clear();

		// Token: 0x06001012 RID: 4114
		public abstract void Close();

		// Token: 0x06001013 RID: 4115
		public abstract void CreateEventSource(EventSourceCreationData sourceData);

		// Token: 0x06001014 RID: 4116
		public abstract void Delete(string logName, string machineName);

		// Token: 0x06001015 RID: 4117
		public abstract void DeleteEventSource(string source, string machineName);

		// Token: 0x06001016 RID: 4118
		public abstract void Dispose(bool disposing);

		// Token: 0x06001017 RID: 4119
		public abstract void EndInit();

		// Token: 0x06001018 RID: 4120
		public abstract bool Exists(string logName, string machineName);

		// Token: 0x06001019 RID: 4121
		protected abstract int GetEntryCount();

		// Token: 0x0600101A RID: 4122
		protected abstract EventLogEntry GetEntry(int index);

		// Token: 0x0600101B RID: 4123 RVA: 0x000493D8 File Offset: 0x000475D8
		public EventLog[] GetEventLogs(string machineName)
		{
			string[] logNames = this.GetLogNames(machineName);
			EventLog[] array = new EventLog[logNames.Length];
			for (int i = 0; i < logNames.Length; i++)
			{
				EventLog eventLog = new EventLog(logNames[i], machineName);
				array[i] = eventLog;
			}
			return array;
		}

		// Token: 0x0600101C RID: 4124
		protected abstract string GetLogDisplayName();

		// Token: 0x0600101D RID: 4125
		public abstract string LogNameFromSourceName(string source, string machineName);

		// Token: 0x0600101E RID: 4126
		public abstract bool SourceExists(string source, string machineName);

		// Token: 0x0600101F RID: 4127
		public abstract void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData);

		// Token: 0x06001020 RID: 4128
		protected abstract string FormatMessage(string source, uint messageID, string[] replacementStrings);

		// Token: 0x06001021 RID: 4129
		protected abstract string[] GetLogNames(string machineName);

		// Token: 0x06001022 RID: 4130 RVA: 0x00049414 File Offset: 0x00047614
		protected void ValidateCustomerLogName(string logName, string machineName)
		{
			if (logName.Length >= 8)
			{
				string text = logName.Substring(0, 8);
				if (string.Compare(text, "AppEvent", true) == 0 || string.Compare(text, "SysEvent", true) == 0 || string.Compare(text, "SecEvent", true) == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The log name: '{0}' is invalid for customer log creation.", logName));
				}
				foreach (string text2 in this.GetLogNames(machineName))
				{
					if (text2.Length >= 8 && string.Compare(text2, 0, text, 0, 8, true) == 0)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'.", logName, text2));
					}
				}
			}
			if (!this.SourceExists(logName, machineName))
			{
				return;
			}
			if (machineName == ".")
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the local computer.", logName));
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the computer {1}.", logName, machineName));
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001023 RID: 4131
		public abstract OverflowAction OverflowAction { get; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001024 RID: 4132
		public abstract int MinimumRetentionDays { get; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001025 RID: 4133
		// (set) Token: 0x06001026 RID: 4134
		public abstract long MaximumKilobytes { get; set; }

		// Token: 0x06001027 RID: 4135
		public abstract void ModifyOverflowPolicy(OverflowAction action, int retentionDays);

		// Token: 0x06001028 RID: 4136
		public abstract void RegisterDisplayName(string resourceFile, long resourceId);

		// Token: 0x04001148 RID: 4424
		private readonly EventLog _coreEventLog;
	}
}
