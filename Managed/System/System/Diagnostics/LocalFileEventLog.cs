using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000203 RID: 515
	internal class LocalFileEventLog : EventLogImpl
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x00049F9C File Offset: 0x0004819C
		public LocalFileEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void BeginInit()
		{
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00049FA8 File Offset: 0x000481A8
		public override void Clear()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return;
			}
			string[] files = Directory.GetFiles(text, "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00049FF2 File Offset: 0x000481F2
		public override void Close()
		{
			if (this.file_watcher != null)
			{
				this.file_watcher.EnableRaisingEvents = false;
				this.file_watcher = null;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0004A010 File Offset: 0x00048210
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			string text = this.FindLogStore(sourceData.LogName);
			if (!Directory.Exists(text))
			{
				base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
				Directory.CreateDirectory(text);
				Directory.CreateDirectory(Path.Combine(text, sourceData.LogName));
				if (this.RunningOnUnix)
				{
					LocalFileEventLog.ModifyAccessPermissions(text, "777");
					LocalFileEventLog.ModifyAccessPermissions(text, "+t");
				}
			}
			Directory.CreateDirectory(Path.Combine(text, sourceData.Source));
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0004A08D File Offset: 0x0004828D
		public override void Delete(string logName, string machineName)
		{
			string text = this.FindLogStore(logName);
			if (!Directory.Exists(text))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", logName, machineName));
			}
			Directory.Delete(text, true);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004A0BC File Offset: 0x000482BC
		public override void DeleteEventSource(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
			}
			Directory.Delete(text);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004A113 File Offset: 0x00048313
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004A11B File Offset: 0x0004831B
		public override void DisableNotification()
		{
			if (this.file_watcher == null)
			{
				return;
			}
			this.file_watcher.EnableRaisingEvents = false;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004A134 File Offset: 0x00048334
		public override void EnableNotification()
		{
			if (this.file_watcher == null)
			{
				string text = this.FindLogStore(base.CoreEventLog.Log);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				this.file_watcher = new FileSystemWatcher();
				this.file_watcher.Path = text;
				this.file_watcher.Created += delegate(object o, FileSystemEventArgs e)
				{
					LocalFileEventLog localFileEventLog = this;
					lock (localFileEventLog)
					{
						if (this._notifying)
						{
							return;
						}
						this._notifying = true;
					}
					Thread.Sleep(100);
					try
					{
						while (this.GetLatestIndex() > this.last_notification_index)
						{
							try
							{
								EventLog coreEventLog = base.CoreEventLog;
								int num = this.last_notification_index;
								this.last_notification_index = num + 1;
								coreEventLog.OnEntryWritten(this.GetEntry(num));
							}
							catch (Exception)
							{
							}
						}
					}
					finally
					{
						localFileEventLog = this;
						lock (localFileEventLog)
						{
							this._notifying = false;
						}
					}
				};
			}
			this.last_notification_index = this.GetLatestIndex();
			this.file_watcher.EnableRaisingEvents = true;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void EndInit()
		{
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0004A1B0 File Offset: 0x000483B0
		public override bool Exists(string logName, string machineName)
		{
			return Directory.Exists(this.FindLogStore(logName));
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0004A1BE File Offset: 0x000483BE
		[MonoTODO("Use MessageTable from PE for lookup")]
		protected override string FormatMessage(string source, uint eventID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004A1CC File Offset: 0x000483CC
		protected override int GetEntryCount()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return 0;
			}
			return Directory.GetFiles(text, "*.log").Length;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0004A204 File Offset: 0x00048404
		protected override EventLogEntry GetEntry(int index)
		{
			string text = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (index + 1).ToString(CultureInfo.InvariantCulture) + ".log");
			EventLogEntry eventLogEntry;
			using (TextReader textReader = File.OpenText(text))
			{
				int num = int.Parse(Path.GetFileNameWithoutExtension(text), CultureInfo.InvariantCulture);
				uint num2 = uint.Parse(textReader.ReadLine().Substring(12), CultureInfo.InvariantCulture);
				EventLogEntryType eventLogEntryType = (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType), textReader.ReadLine().Substring(11));
				string text2 = textReader.ReadLine().Substring(8);
				string text3 = textReader.ReadLine().Substring(10);
				short num3 = short.Parse(text3, CultureInfo.InvariantCulture);
				string text4 = "(" + text3 + ")";
				DateTime dateTime = DateTime.ParseExact(textReader.ReadLine().Substring(15), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
				DateTime lastWriteTime = File.GetLastWriteTime(text);
				int num4 = int.Parse(textReader.ReadLine().Substring(20));
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				while (list.Count < num4)
				{
					char c = (char)textReader.Read();
					if (c == '\0')
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string[] array = list.ToArray();
				string text5 = this.FormatMessage(text2, num2, array);
				int eventID = EventLog.GetEventID((long)((ulong)num2));
				byte[] array2 = Convert.FromBase64String(textReader.ReadToEnd());
				eventLogEntry = new EventLogEntry(text4, num3, num, eventID, text2, text5, null, Environment.MachineName, eventLogEntryType, dateTime, lastWriteTime, array2, array, (long)((ulong)num2));
			}
			return eventLogEntry;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0004A3D8 File Offset: 0x000485D8
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004A3E8 File Offset: 0x000485E8
		protected override string[] GetLogNames(string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return new string[0];
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				array[i] = Path.GetFileName(directories[i]);
			}
			return array;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004A440 File Offset: 0x00048640
		public override string LogNameFromSourceName(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return string.Empty;
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				return string.Empty;
			}
			return new DirectoryInfo(text).Parent.Name;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004A481 File Offset: 0x00048681
		public override bool SourceExists(string source, string machineName)
		{
			return Directory.Exists(this.EventLogStore) && this.FindSourceDirectory(source) != null;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004A49C File Offset: 0x0004869C
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			object obj = LocalFileEventLog.lockObject;
			lock (obj)
			{
				string text = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (this.GetLatestIndex() + 1).ToString(CultureInfo.InvariantCulture) + ".log");
				try
				{
					using (TextWriter textWriter = File.CreateText(text))
					{
						textWriter.WriteLine("InstanceID: {0}", instanceID.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("EntryType: {0}", (int)type);
						textWriter.WriteLine("Source: {0}", base.CoreEventLog.Source);
						textWriter.WriteLine("Category: {0}", category.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("TimeGenerated: {0}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
						textWriter.WriteLine("ReplacementStrings: {0}", replacementStrings.Length.ToString(CultureInfo.InvariantCulture));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string text2 in replacementStrings)
						{
							stringBuilder.Append(text2);
							stringBuilder.Append('\0');
						}
						textWriter.Write(stringBuilder.ToString());
						textWriter.Write(Convert.ToBase64String(rawData));
					}
				}
				catch (IOException)
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004A650 File Offset: 0x00048850
		private string FindSourceDirectory(string source)
		{
			string text = null;
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string[] directories2 = Directory.GetDirectories(directories[i], "*");
				for (int j = 0; j < directories2.Length; j++)
				{
					if (string.Compare(Path.GetFileName(directories2[j]), source, true, CultureInfo.InvariantCulture) == 0)
					{
						text = directories2[j];
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0004A6C0 File Offset: 0x000488C0
		private bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004A6EC File Offset: 0x000488EC
		private string FindLogStore(string logName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return Path.Combine(this.EventLogStore, logName);
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				if (string.Compare(Path.GetFileName(directories[i]), logName, true, CultureInfo.InvariantCulture) == 0)
				{
					return directories[i];
				}
			}
			return Path.Combine(this.EventLogStore, logName);
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0004A758 File Offset: 0x00048958
		private string EventLogStore
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (environmentVariable != null && environmentVariable.Length > "local".Length + 1)
				{
					return environmentVariable.Substring("local".Length + 1);
				}
				if (this.RunningOnUnix)
				{
					return "/var/lib/mono/eventlog";
				}
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mono\\eventlog");
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0004A7BC File Offset: 0x000489BC
		private int GetLatestIndex()
		{
			int num = 0;
			string[] files = Directory.GetFiles(this.FindLogStore(base.CoreEventLog.Log), "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					int num2 = int.Parse(Path.GetFileNameWithoutExtension(files[i]), CultureInfo.InvariantCulture);
					if (num2 > num)
					{
						num = num2;
					}
				}
				catch
				{
				}
			}
			return num;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004A828 File Offset: 0x00048A28
		private static void ModifyAccessPermissions(string path, string permissions)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "chmod";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = string.Format("{0} \"{1}\"", permissions, path);
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				throw new SecurityException("Access permissions could not be modified.", ex);
			}
			process.WaitForExit();
			if (process.ExitCode != 0)
			{
				process.Close();
				throw new SecurityException("Access permissions could not be modified.");
			}
			process.Close();
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0004A8BF File Offset: 0x00048ABF
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0004A8C6 File Offset: 0x00048AC6
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x0004A8D1 File Offset: 0x00048AD1
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

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004A8DD File Offset: 0x00048ADD
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0004A8E9 File Offset: 0x00048AE9
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}

		// Token: 0x0400117D RID: 4477
		private const string DateFormat = "yyyyMMddHHmmssfff";

		// Token: 0x0400117E RID: 4478
		private static readonly object lockObject = new object();

		// Token: 0x0400117F RID: 4479
		private FileSystemWatcher file_watcher;

		// Token: 0x04001180 RID: 4480
		private int last_notification_index;

		// Token: 0x04001181 RID: 4481
		private bool _notifying;
	}
}
