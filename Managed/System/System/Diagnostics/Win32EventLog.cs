using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200021D RID: 541
	internal class Win32EventLog : EventLogImpl
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x0004B79B File Offset: 0x0004999B
		public Win32EventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void BeginInit()
		{
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0004B7AF File Offset: 0x000499AF
		public override void Clear()
		{
			if (Win32EventLog.PInvoke.ClearEventLog(this.ReadHandle, null) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0004B7CC File Offset: 0x000499CC
		public override void Close()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._readHandle != IntPtr.Zero)
				{
					this.CloseEventLog(this._readHandle);
					this._readHandle = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0004B830 File Offset: 0x00049A30
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(sourceData.MachineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("EventLog registry key is missing.");
				}
				bool flag = false;
				RegistryKey registryKey = null;
				try
				{
					registryKey = eventLogKey.OpenSubKey(sourceData.LogName, true);
					if (registryKey == null)
					{
						base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
						registryKey = eventLogKey.CreateSubKey(sourceData.LogName);
						registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
						Win32EventLog.UpdateLogRegistry(registryKey);
						using (RegistryKey registryKey2 = registryKey.CreateSubKey(sourceData.LogName))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey2, sourceData);
						}
						flag = true;
					}
					if (sourceData.LogName != sourceData.Source)
					{
						if (!flag)
						{
							string[] array = (string[])registryKey.GetValue("Sources");
							if (array == null)
							{
								registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
							}
							else
							{
								bool flag2 = false;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == sourceData.Source)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									string[] array2 = new string[array.Length + 1];
									Array.Copy(array, 0, array2, 0, array.Length);
									array2[array.Length] = sourceData.Source;
									registryKey.SetValue("Sources", array2);
								}
							}
						}
						using (RegistryKey registryKey3 = registryKey.CreateSubKey(sourceData.Source))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey3, sourceData);
						}
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004BA34 File Offset: 0x00049C34
		public override void Delete(string logName, string machineName)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("The event log key does not exist.");
				}
				using (RegistryKey registryKey = eventLogKey.OpenSubKey(logName, false))
				{
					if (registryKey == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", logName, machineName));
					}
					base.CoreEventLog.Clear();
					string text = (string)registryKey.GetValue("File");
					if (text != null)
					{
						try
						{
							File.Delete(text);
						}
						catch (Exception)
						{
						}
					}
				}
				eventLogKey.DeleteSubKeyTree(logName);
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004BAEC File Offset: 0x00049CEC
		public override void DeleteEventSource(string source, string machineName)
		{
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, true))
			{
				if (registryKey == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
				}
				registryKey.DeleteSubKeyTree(source);
				string[] array = (string[])registryKey.GetValue("Sources");
				if (array != null)
				{
					List<string> list = new List<string>();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != source)
						{
							list.Add(array[i]);
						}
					}
					string[] array2 = list.ToArray();
					registryKey.SetValue("Sources", array2);
				}
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004A113 File Offset: 0x00048313
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void EndInit()
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004BB94 File Offset: 0x00049D94
		public override bool Exists(string logName, string machineName)
		{
			bool flag;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyByName(logName, machineName, false))
			{
				flag = registryKey != null;
			}
			return flag;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004BBCC File Offset: 0x00049DCC
		[MonoTODO]
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "EventMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], messageID, replacementStrings);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return string.Join(", ", replacementStrings);
			}
			return text;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004BC14 File Offset: 0x00049E14
		private string FormatCategory(string source, int category)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "CategoryMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], (uint)category, new string[0]);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return "(" + category.ToString(CultureInfo.InvariantCulture) + ")";
			}
			return text;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0004BC74 File Offset: 0x00049E74
		protected override int GetEntryCount()
		{
			int num = 0;
			if (Win32EventLog.PInvoke.GetNumberOfEventLogRecords(this.ReadHandle, ref num) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return num;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0004BCA0 File Offset: 0x00049EA0
		protected override EventLogEntry GetEntry(int index)
		{
			index += this.OldestEventLogEntry;
			int num = 0;
			int num2 = 0;
			byte[] array = new byte[524287];
			this.ReadEventLog(index, array, ref num, ref num2);
			MemoryStream memoryStream = new MemoryStream(array);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryReader.ReadBytes(8);
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			int num5 = binaryReader.ReadInt32();
			uint num6 = binaryReader.ReadUInt32();
			int eventID = EventLog.GetEventID((long)((ulong)num6));
			short num7 = binaryReader.ReadInt16();
			short num8 = binaryReader.ReadInt16();
			short num9 = binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt32();
			int num10 = binaryReader.ReadInt32();
			int num11 = binaryReader.ReadInt32();
			int num12 = binaryReader.ReadInt32();
			int num13 = binaryReader.ReadInt32();
			int num14 = binaryReader.ReadInt32();
			DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds((double)num4);
			DateTime dateTime2 = new DateTime(1970, 1, 1).AddSeconds((double)num5);
			StringBuilder stringBuilder = new StringBuilder();
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text2 = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text3 = null;
			if (num11 != 0)
			{
				memoryStream.Position = (long)num12;
				byte[] array2 = binaryReader.ReadBytes(num11);
				text3 = Win32EventLog.LookupAccountSid(text2, array2);
			}
			memoryStream.Position = (long)num10;
			string[] array3 = new string[(int)num8];
			for (int i = 0; i < (int)num8; i++)
			{
				stringBuilder.Length = 0;
				while (binaryReader.PeekChar() != 0)
				{
					stringBuilder.Append(binaryReader.ReadChar());
				}
				binaryReader.ReadChar();
				array3[i] = stringBuilder.ToString();
			}
			byte[] array4 = new byte[num13];
			memoryStream.Position = (long)num14;
			binaryReader.Read(array4, 0, num13);
			string text4 = this.FormatMessage(text, num6, array3);
			return new EventLogEntry(this.FormatCategory(text, (int)num9), num9, num3, eventID, text, text4, text3, text2, (EventLogEntryType)num7, dateTime, dateTime2, array4, array3, (long)((ulong)num6));
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0004A3D8 File Offset: 0x000485D8
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004BF0C File Offset: 0x0004A10C
		protected override string[] GetLogNames(string machineName)
		{
			string[] array;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					array = new string[0];
				}
				else
				{
					array = eventLogKey.GetSubKeyNames();
				}
			}
			return array;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004BF54 File Offset: 0x0004A154
		public override string LogNameFromSourceName(string source, string machineName)
		{
			string text;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false))
			{
				if (registryKey == null)
				{
					text = string.Empty;
				}
				else
				{
					text = Win32EventLog.GetLogName(registryKey);
				}
			}
			return text;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0004BF9C File Offset: 0x0004A19C
		public override bool SourceExists(string source, string machineName)
		{
			RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false);
			if (registryKey != null)
			{
				registryKey.Close();
				return true;
			}
			return false;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0004BFC0 File Offset: 0x0004A1C0
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			IntPtr intPtr = this.RegisterEventSource();
			try
			{
				if (Win32EventLog.PInvoke.ReportEvent(intPtr, (ushort)type, (ushort)category, instanceID, IntPtr.Zero, (ushort)replacementStrings.Length, (uint)rawData.Length, replacementStrings, rawData) != 1)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				this.DeregisterEventSource(intPtr);
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004C018 File Offset: 0x0004A218
		private static void UpdateLogRegistry(RegistryKey logKey)
		{
			if (logKey.GetValue("File") == null)
			{
				string logName = Win32EventLog.GetLogName(logKey);
				string text;
				if (logName.Length > 8)
				{
					text = logName.Substring(0, 8) + ".evt";
				}
				else
				{
					text = logName + ".evt";
				}
				string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "config");
				logKey.SetValue("File", Path.Combine(text2, text));
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004C088 File Offset: 0x0004A288
		private static void UpdateSourceRegistry(RegistryKey sourceKey, EventSourceCreationData data)
		{
			if (data.CategoryCount > 0)
			{
				sourceKey.SetValue("CategoryCount", data.CategoryCount);
			}
			if (data.CategoryResourceFile != null && data.CategoryResourceFile.Length > 0)
			{
				sourceKey.SetValue("CategoryMessageFile", data.CategoryResourceFile);
			}
			if (data.MessageResourceFile != null && data.MessageResourceFile.Length > 0)
			{
				sourceKey.SetValue("EventMessageFile", data.MessageResourceFile);
			}
			if (data.ParameterResourceFile != null && data.ParameterResourceFile.Length > 0)
			{
				sourceKey.SetValue("ParameterMessageFile", data.ParameterResourceFile);
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004C129 File Offset: 0x0004A329
		private static string GetLogName(RegistryKey logKey)
		{
			string name = logKey.Name;
			return name.Substring(name.LastIndexOf("\\") + 1);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004C144 File Offset: 0x0004A344
		private void ReadEventLog(int index, byte[] buffer, ref int bytesRead, ref int minBufferNeeded)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Win32EventLog.PInvoke.ReadEventLog(this.ReadHandle, (Win32EventLog.ReadFlags)6, index, buffer, buffer.Length, ref bytesRead, ref minBufferNeeded) != 1)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (i >= 2)
					{
						throw new Win32Exception(lastWin32Error);
					}
					base.CoreEventLog.Reset();
				}
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0004C192 File Offset: 0x0004A392
		[MonoTODO("Support remote machines")]
		private static RegistryKey GetEventLogKey(string machineName, bool writable)
		{
			return Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004C1A4 File Offset: 0x0004A3A4
		private static RegistryKey FindSourceKeyByName(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						using (RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], writable))
						{
							if (registryKey3 == null)
							{
								break;
							}
							RegistryKey registryKey4 = registryKey3.OpenSubKey(source, writable);
							if (registryKey4 != null)
							{
								return registryKey4;
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004C244 File Offset: 0x0004A444
		private static RegistryKey FindLogKeyByName(string logName, string machineName, bool writable)
		{
			RegistryKey registryKey;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, writable))
			{
				if (eventLogKey == null)
				{
					registryKey = null;
				}
				else
				{
					registryKey = eventLogKey.OpenSubKey(logName, writable);
				}
			}
			return registryKey;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004C288 File Offset: 0x0004A488
		private static RegistryKey FindLogKeyBySource(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey3 = null;
						try
						{
							RegistryKey registryKey4 = registryKey.OpenSubKey(subKeyNames[i], writable);
							if (registryKey4 != null)
							{
								registryKey3 = registryKey4.OpenSubKey(source, writable);
								if (registryKey3 != null)
								{
									return registryKey4;
								}
							}
						}
						finally
						{
							if (registryKey3 != null)
							{
								registryKey3.Close();
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004C328 File Offset: 0x0004A528
		private int OldestEventLogEntry
		{
			get
			{
				int num = 0;
				if (Win32EventLog.PInvoke.GetOldestEventLogRecord(this.ReadHandle, ref num) != 1)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				return num;
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004C353 File Offset: 0x0004A553
		private void CloseEventLog(IntPtr hEventLog)
		{
			if (Win32EventLog.PInvoke.CloseEventLog(hEventLog) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004C369 File Offset: 0x0004A569
		private void DeregisterEventSource(IntPtr hEventLog)
		{
			if (Win32EventLog.PInvoke.DeregisterEventSource(hEventLog) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004C380 File Offset: 0x0004A580
		private static string LookupAccountSid(string machineName, byte[] sid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder();
			uint capacity2 = (uint)stringBuilder2.Capacity;
			string text = null;
			while (text == null)
			{
				Win32EventLog.SidNameUse sidNameUse;
				if (!Win32EventLog.PInvoke.LookupAccountSid(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sidNameUse))
				{
					if (Marshal.GetLastWin32Error() == 122)
					{
						stringBuilder.EnsureCapacity((int)capacity);
						stringBuilder2.EnsureCapacity((int)capacity2);
					}
					else
					{
						text = string.Empty;
					}
				}
				else
				{
					text = string.Format("{0}\\{1}", stringBuilder2.ToString(), stringBuilder.ToString());
				}
			}
			return text;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0004C400 File Offset: 0x0004A600
		private static string FetchMessage(string msgDll, uint messageID, string[] replacementStrings)
		{
			IntPtr intPtr = Win32EventLog.PInvoke.LoadLibraryEx(msgDll, IntPtr.Zero, Win32EventLog.LoadFlags.LibraryAsDataFile);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr[] array = new IntPtr[replacementStrings.Length];
			try
			{
				for (int i = 0; i < replacementStrings.Length; i++)
				{
					array[i] = Marshal.StringToHGlobalAuto(replacementStrings[i]);
				}
				if (Win32EventLog.PInvoke.FormatMessage(Win32EventLog.FormatMessageFlags.AllocateBuffer | Win32EventLog.FormatMessageFlags.FromHModule | Win32EventLog.FormatMessageFlags.ArgumentArray, intPtr, messageID, 0, ref intPtr2, 0, array) != 0)
				{
					string text = Marshal.PtrToStringAuto(intPtr2);
					intPtr2 = Win32EventLog.PInvoke.LocalFree(intPtr2);
					return text.TrimEnd(null);
				}
				Marshal.GetLastWin32Error();
			}
			finally
			{
				foreach (IntPtr intPtr3 in array)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
				}
				Win32EventLog.PInvoke.FreeLibrary(intPtr);
			}
			return null;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004C4D4 File Offset: 0x0004A6D4
		private string[] GetMessageResourceDlls(string source, string valueName)
		{
			RegistryKey registryKey = Win32EventLog.FindSourceKeyByName(source, base.CoreEventLog.MachineName, false);
			if (registryKey != null)
			{
				string text = registryKey.GetValue(valueName) as string;
				if (text != null)
				{
					return text.Split(new char[] { ';' });
				}
			}
			return new string[0];
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0004C520 File Offset: 0x0004A720
		private IntPtr ReadHandle
		{
			get
			{
				if (this._readHandle != IntPtr.Zero)
				{
					return this._readHandle;
				}
				string logName = base.CoreEventLog.GetLogName();
				this._readHandle = Win32EventLog.PInvoke.OpenEventLog(base.CoreEventLog.MachineName, logName);
				if (this._readHandle == IntPtr.Zero)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' on computer '{1}' cannot be opened.", logName, base.CoreEventLog.MachineName), new Win32Exception());
				}
				return this._readHandle;
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
		private IntPtr RegisterEventSource()
		{
			IntPtr intPtr = Win32EventLog.PInvoke.RegisterEventSource(base.CoreEventLog.MachineName, base.CoreEventLog.Source);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event source '{0}' on computer '{1}' cannot be opened.", base.CoreEventLog.Source, base.CoreEventLog.MachineName), new Win32Exception());
			}
			return intPtr;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004C610 File Offset: 0x0004A810
		public override void DisableNotification()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._notifyResetEvent != null)
				{
					this._notifyResetEvent.Close();
					this._notifyResetEvent = null;
				}
				this._notifyThread = null;
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004C66C File Offset: 0x0004A86C
		public override void EnableNotification()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._notifyResetEvent == null)
				{
					this._notifyResetEvent = new ManualResetEvent(false);
					this._lastEntryWritten = this.OldestEventLogEntry + base.EntryCount;
					if (Win32EventLog.PInvoke.NotifyChangeEventLog(this.ReadHandle, this._notifyResetEvent.SafeWaitHandle.DangerousGetHandle()) == 0)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to receive notifications for log '{0}' on computer '{1}'.", base.CoreEventLog.GetLogName(), base.CoreEventLog.MachineName), new Win32Exception());
					}
					this._notifyThread = new Thread(delegate
					{
						this.NotifyEventThread(this._notifyResetEvent);
					});
					this._notifyThread.IsBackground = true;
					this._notifyThread.Start();
				}
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0004C750 File Offset: 0x0004A950
		private void NotifyEventThread(ManualResetEvent resetEvent)
		{
			if (resetEvent == null)
			{
				return;
			}
			for (;;)
			{
				try
				{
					resetEvent.WaitOne();
				}
				catch (ObjectDisposedException)
				{
					break;
				}
				object eventLock = this._eventLock;
				lock (eventLock)
				{
					if (resetEvent == this._notifyResetEvent)
					{
						if (!(this._readHandle == IntPtr.Zero))
						{
							int oldestEventLogEntry = this.OldestEventLogEntry;
							if (this._lastEntryWritten < oldestEventLogEntry)
							{
								this._lastEntryWritten = oldestEventLogEntry;
							}
							int num = this._lastEntryWritten - oldestEventLogEntry;
							int num2 = base.EntryCount + oldestEventLogEntry;
							for (int i = num; i < num2 - 1; i++)
							{
								EventLogEntry entry = this.GetEntry(i);
								base.CoreEventLog.OnEntryWritten(entry);
							}
							this._lastEntryWritten = num2;
							continue;
						}
					}
				}
				break;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00004239 File Offset: 0x00002439
		public override OverflowAction OverflowAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00004239 File Offset: 0x00002439
		public override int MinimumRetentionDays
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00004239 File Offset: 0x00002439
		public override long MaximumKilobytes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00004239 File Offset: 0x00002439
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00004239 File Offset: 0x00002439
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001208 RID: 4616
		private const int MESSAGE_NOT_FOUND = 317;

		// Token: 0x04001209 RID: 4617
		private ManualResetEvent _notifyResetEvent;

		// Token: 0x0400120A RID: 4618
		private IntPtr _readHandle;

		// Token: 0x0400120B RID: 4619
		private Thread _notifyThread;

		// Token: 0x0400120C RID: 4620
		private int _lastEntryWritten;

		// Token: 0x0400120D RID: 4621
		private object _eventLock = new object();

		// Token: 0x0200021E RID: 542
		private class PInvoke
		{
			// Token: 0x060011B2 RID: 4530
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ClearEventLog(IntPtr hEventLog, string lpBackupFileName);

			// Token: 0x060011B3 RID: 4531
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int CloseEventLog(IntPtr hEventLog);

			// Token: 0x060011B4 RID: 4532
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int DeregisterEventSource(IntPtr hEventLog);

			// Token: 0x060011B5 RID: 4533
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int FormatMessage(Win32EventLog.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, ref IntPtr lpBuffer, int nSize, IntPtr[] arguments);

			// Token: 0x060011B6 RID: 4534
			[DllImport("kernel32", SetLastError = true)]
			public static extern bool FreeLibrary(IntPtr hModule);

			// Token: 0x060011B7 RID: 4535
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetNumberOfEventLogRecords(IntPtr hEventLog, ref int NumberOfRecords);

			// Token: 0x060011B8 RID: 4536
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetOldestEventLogRecord(IntPtr hEventLog, ref int OldestRecord);

			// Token: 0x060011B9 RID: 4537
			[DllImport("kernel32", SetLastError = true)]
			public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, Win32EventLog.LoadFlags dwFlags);

			// Token: 0x060011BA RID: 4538
			[DllImport("kernel32", SetLastError = true)]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x060011BB RID: 4539
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out Win32EventLog.SidNameUse peUse);

			// Token: 0x060011BC RID: 4540
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int NotifyChangeEventLog(IntPtr hEventLog, IntPtr hEvent);

			// Token: 0x060011BD RID: 4541
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr OpenEventLog(string machineName, string logName);

			// Token: 0x060011BE RID: 4542
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr RegisterEventSource(string machineName, string sourceName);

			// Token: 0x060011BF RID: 4543
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReportEvent(IntPtr hHandle, ushort wType, ushort wCategory, uint dwEventID, IntPtr sid, ushort wNumStrings, uint dwDataSize, string[] lpStrings, byte[] lpRawData);

			// Token: 0x060011C0 RID: 4544
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReadEventLog(IntPtr hEventLog, Win32EventLog.ReadFlags dwReadFlags, int dwRecordOffset, byte[] buffer, int nNumberOfBytesToRead, ref int pnBytesRead, ref int pnMinNumberOfBytesNeeded);

			// Token: 0x0400120E RID: 4622
			public const int ERROR_INSUFFICIENT_BUFFER = 122;

			// Token: 0x0400120F RID: 4623
			public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;
		}

		// Token: 0x0200021F RID: 543
		private enum ReadFlags
		{
			// Token: 0x04001211 RID: 4625
			Sequential = 1,
			// Token: 0x04001212 RID: 4626
			Seek,
			// Token: 0x04001213 RID: 4627
			ForwardsRead = 4,
			// Token: 0x04001214 RID: 4628
			BackwardsRead = 8
		}

		// Token: 0x02000220 RID: 544
		private enum LoadFlags : uint
		{
			// Token: 0x04001216 RID: 4630
			LibraryAsDataFile = 2U
		}

		// Token: 0x02000221 RID: 545
		[Flags]
		private enum FormatMessageFlags
		{
			// Token: 0x04001218 RID: 4632
			AllocateBuffer = 256,
			// Token: 0x04001219 RID: 4633
			IgnoreInserts = 512,
			// Token: 0x0400121A RID: 4634
			FromHModule = 2048,
			// Token: 0x0400121B RID: 4635
			FromSystem = 4096,
			// Token: 0x0400121C RID: 4636
			ArgumentArray = 8192
		}

		// Token: 0x02000222 RID: 546
		private enum SidNameUse
		{
			// Token: 0x0400121E RID: 4638
			User = 1,
			// Token: 0x0400121F RID: 4639
			Group,
			// Token: 0x04001220 RID: 4640
			Domain,
			// Token: 0x04001221 RID: 4641
			lias,
			// Token: 0x04001222 RID: 4642
			WellKnownGroup,
			// Token: 0x04001223 RID: 4643
			DeletedAccount,
			// Token: 0x04001224 RID: 4644
			Invalid,
			// Token: 0x04001225 RID: 4645
			Unknown,
			// Token: 0x04001226 RID: 4646
			Computer
		}
	}
}
