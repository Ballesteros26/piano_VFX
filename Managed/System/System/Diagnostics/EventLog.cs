using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides interaction with Windows event logs. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F0 RID: 496
	[MonitoringDescription("Represents an event log")]
	[InstallerType(typeof(EventLogInstaller))]
	[DefaultEvent("EntryWritten")]
	public class EventLog : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Does not associate the instance with any log.</summary>
		// Token: 0x06000FA4 RID: 4004 RVA: 0x000483B8 File Offset: 0x000465B8
		public EventLog()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the local computer.</summary>
		/// <param name="logName">The name of the log on the local computer. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid. </exception>
		// Token: 0x06000FA5 RID: 4005 RVA: 0x000483C5 File Offset: 0x000465C5
		public EventLog(string logName)
			: this(logName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer.</summary>
		/// <param name="logName">The name of the log on the specified computer. </param>
		/// <param name="machineName">The computer on which the log exists. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.-or- The computer name is invalid. </exception>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x000483D3 File Offset: 0x000465D3
		public EventLog(string logName, string machineName)
			: this(logName, machineName, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer and creates or assigns the specified source to the <see cref="T:System.Diagnostics.EventLog" />.</summary>
		/// <param name="logName">The name of the log on the specified computer </param>
		/// <param name="machineName">The computer on which the log exists. </param>
		/// <param name="source">The source of event log entries. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.-or- The computer name is invalid. </exception>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x000483E4 File Offset: 0x000465E4
		public EventLog(string logName, string machineName, string source)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			this.source = source;
			this.machineName = machineName;
			this.logName = logName;
			this.Impl = EventLog.CreateEventLogImpl(this);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Diagnostics.EventLog" /> receives <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event notifications.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.EventLog" /> receives notification when an entry is written to the log; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The event log is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0004844C File Offset: 0x0004664C
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00048454 File Offset: 0x00046654
		[DefaultValue(false)]
		[Browsable(false)]
		[MonitoringDescription("If enabled raises event when a log is written.")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.doRaiseEvents;
			}
			set
			{
				if (value == this.doRaiseEvents)
				{
					return;
				}
				if (value)
				{
					this.Impl.EnableNotification();
				}
				else
				{
					this.Impl.DisableNotification();
				}
				this.doRaiseEvents = value;
			}
		}

		/// <summary>Gets the contents of the event log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryCollection" /> holding the entries in the event log. Each entry is associated with an instance of the <see cref="T:System.Diagnostics.EventLogEntry" /> class.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00048482 File Offset: 0x00046682
		[Browsable(false)]
		[MonitoringDescription("The entries in the log.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public EventLogEntryCollection Entries
		{
			get
			{
				return new EventLogEntryCollection(this.Impl);
			}
		}

		/// <summary>Gets or sets the name of the log to read from or write to.</summary>
		/// <returns>The name of the log. This can be Application, System, Security, or a custom log name. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0004848F File Offset: 0x0004668F
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x000484B4 File Offset: 0x000466B4
		[ReadOnly(true)]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		[TypeConverter("System.Diagnostics.Design.LogConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("Name of the log that is read and written.")]
		public string Log
		{
			get
			{
				if (this.source != null && this.source.Length > 0)
				{
					return this.GetLogName();
				}
				return this.logName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (string.Compare(this.logName, value, true) != 0)
				{
					this.logName = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets the event log's friendly name.</summary>
		/// <returns>A name that represents the event log in the system's event viewer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="P:System.Diagnostics.EventLog.Log" /> does not exist in the registry for this computer.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000484E0 File Offset: 0x000466E0
		[Browsable(false)]
		public string LogDisplayName
		{
			get
			{
				return this.Impl.LogDisplayName;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the server on which the event log resides. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x000484ED File Offset: 0x000466ED
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x000484F8 File Offset: 0x000466F8
		[ReadOnly(true)]
		[DefaultValue(".")]
		[RecommendedAsConfigurable(true)]
		[MonitoringDescription("Name of the machine that this log get written to.")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null || value.Trim().Length == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value {0} for property MachineName.", value));
				}
				if (string.Compare(this.machineName, value, true) != 0)
				{
					this.Close();
					this.machineName = value;
				}
			}
		}

		/// <summary>Gets or sets the source name to register and use when writing to the event log.</summary>
		/// <returns>The name registered with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The source name results in a registry key path longer than 254 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00048547 File Offset: 0x00046747
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x00048550 File Offset: 0x00046750
		[ReadOnly(true)]
		[MonitoringDescription("The application name that writes the log.")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.source == null || (this.source.Length == 0 && (this.logName == null || this.logName.Length == 0)))
				{
					this.source = value;
					return;
				}
				if (string.Compare(this.source, value, true) != 0)
				{
					this.source = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of an <see cref="T:System.Diagnostics.EventLog" /> entry written event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event-handler calls issued as a result of an <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event on the event log.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000485B5 File Offset: 0x000467B5
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x000485BD File Offset: 0x000467BD
		[Browsable(false)]
		[DefaultValue(null)]
		[MonitoringDescription("An object that synchronizes event handler calls.")]
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

		/// <summary>Gets the configured behavior for storing new entries when the event log reaches its maximum log file size.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.OverflowAction" /> value that specifies the configured behavior for storing new entries when the event log reaches its maximum log size. The default is <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x000485C6 File Offset: 0x000467C6
		[MonoTODO]
		[ComVisible(false)]
		[Browsable(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				return this.Impl.OverflowAction;
			}
		}

		/// <summary>Gets the number of days to retain entries in the event log.</summary>
		/// <returns>The number of days that entries in the event log are retained. The default value is 7.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000485D3 File Offset: 0x000467D3
		[Browsable(false)]
		[ComVisible(false)]
		[MonoTODO]
		public int MinimumRetentionDays
		{
			get
			{
				return this.Impl.MinimumRetentionDays;
			}
		}

		/// <summary>Gets or sets the maximum event log size in kilobytes.</summary>
		/// <returns>The maximum event log size in kilobytes. The default is 512, indicating a maximum file size of 512 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 64, or greater than 4194240, or not an even multiple of 64. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.- or -The registry key for the event log could not be opened on the target computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000485E0 File Offset: 0x000467E0
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x000485ED File Offset: 0x000467ED
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ComVisible(false)]
		[MonoTODO]
		public long MaximumKilobytes
		{
			get
			{
				return this.Impl.MaximumKilobytes;
			}
			set
			{
				this.Impl.MaximumKilobytes = value;
			}
		}

		/// <summary>Changes the configured behavior for writing new entries when the event log reaches its maximum file size.</summary>
		/// <param name="action">The overflow behavior for writing new entries to the event log. </param>
		/// <param name="retentionDays">The minimum number of days each event log entry is retained. This parameter is used only if <paramref name="action" /> is set to <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="P:System.Diagnostics.EventLog.OverflowAction" /> value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="retentionDays" /> is less than one, or larger than 365. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.- or -The registry key for the event log could not be opened on the target computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FB8 RID: 4024 RVA: 0x000485FB File Offset: 0x000467FB
		[ComVisible(false)]
		[MonoTODO]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			this.Impl.ModifyOverflowPolicy(action, retentionDays);
		}

		/// <summary>Specifies the localized name of the event log, which is displayed in the server Event Viewer.</summary>
		/// <param name="resourceFile">The fully specified path to a localized resource file. </param>
		/// <param name="resourceId">The resource identifier that indexes a localized string within the resource file. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.- or -The registry key for the event log could not be opened on the target computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFile " />is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0004860A File Offset: 0x0004680A
		[MonoTODO]
		[ComVisible(false)]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			this.Impl.RegisterDisplayName(resourceFile, resourceId);
		}

		/// <summary>Begins the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Diagnostics.EventLog" /> is already initialized.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FBA RID: 4026 RVA: 0x00048619 File Offset: 0x00046819
		public void BeginInit()
		{
			this.Impl.BeginInit();
		}

		/// <summary>Removes all entries from the event log.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.-or- The log cannot be opened. A Windows error code is not available. </exception>
		/// <exception cref="T:System.ArgumentException">A value is not specified for the <see cref="P:System.Diagnostics.EventLog.Log" /> property. Make sure the log name is not an empty string. </exception>
		/// <exception cref="T:System.InvalidOperationException">The log does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FBB RID: 4027 RVA: 0x00048628 File Offset: 0x00046828
		public void Clear()
		{
			string log = this.Log;
			if (log == null || log.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (!EventLog.Exists(log, this.MachineName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", log, this.machineName));
			}
			this.Impl.Clear();
			this.Reset();
		}

		/// <summary>Closes the event log and releases read and write handles.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log's read handle or write handle was not released successfully. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FBC RID: 4028 RVA: 0x0004868D File Offset: 0x0004688D
		public void Close()
		{
			this.Impl.Close();
			this.EnableRaisingEvents = false;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000486A4 File Offset: 0x000468A4
		internal void Reset()
		{
			bool enableRaisingEvents = this.EnableRaisingEvents;
			this.Close();
			this.EnableRaisingEvents = enableRaisingEvents;
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the local computer. This method can also create a new custom log on the local computer.</summary>
		/// <param name="source">The source name by which the application is registered on the local computer. </param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is an empty string ("") or null.- or - <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.- or - <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name results in a registry key path longer than 254 characters.- or - The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name.- or - The source cannot be registered because it already exists on the local computer.- or - The source name matches an existing event log name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FBE RID: 4030 RVA: 0x000486C5 File Offset: 0x000468C5
		public static void CreateEventSource(string source, string logName)
		{
			EventLog.CreateEventSource(source, logName, ".");
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the specified computer. This method can also be used to create a new custom log on the specified computer.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, <paramref name="logName" /> defaults to Application. </param>
		/// <param name="machineName">The name of the computer to register this event source with, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> is not a valid computer name.- or - <paramref name="source" /> is an empty string ("") or null.- or - <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.- or - <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name results in a registry key path longer than 254 characters.- or - The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name on the specified computer.- or - The source cannot be registered because it already exists on the specified computer.- or - The source name matches an existing event source name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FBF RID: 4031 RVA: 0x000486D3 File Offset: 0x000468D3
		[Obsolete("use CreateEventSource(EventSourceCreationData) instead")]
		public static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, machineName));
		}

		/// <summary>Establishes a valid event source for writing localized event messages, using the specified configuration properties for the event source and the corresponding event log.</summary>
		/// <param name="sourceData">The configuration properties for the event source and its target event log. </param>
		/// <exception cref="T:System.ArgumentException">The computer name specified in <paramref name="sourceData" /> is not valid.- or - The source name specified in <paramref name="sourceData" /> is null.- or - The log name specified in <paramref name="sourceData" /> is not valid. Event log names must consist of printable characters and cannot include the characters '*', '?', or '\'.- or - The log name specified in <paramref name="sourceData" /> is not valid for user log creation. The Event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name specified in <paramref name="sourceData" /> results in a registry key path longer than 254 characters.- or - The first 8 characters of the log name specified in <paramref name="sourceData" /> are not unique.- or - The source name specified in <paramref name="sourceData" /> is already registered.- or - The source name specified in <paramref name="sourceData" /> matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceData" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC0 RID: 4032 RVA: 0x000486E4 File Offset: 0x000468E4
		[MonoNotSupported("remote machine is not supported")]
		public static void CreateEventSource(EventSourceCreationData sourceData)
		{
			if (sourceData.Source == null || sourceData.Source.Length == 0)
			{
				throw new ArgumentException("Source property value has not been specified.");
			}
			if (sourceData.LogName == null || sourceData.LogName.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (EventLog.SourceExists(sourceData.Source, sourceData.MachineName))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Source '{0}' already exists on '{1}'.", sourceData.Source, sourceData.MachineName));
			}
			EventLog.CreateEventLogImpl(sourceData.LogName, sourceData.MachineName, sourceData.Source).CreateEventSource(sourceData);
		}

		/// <summary>Removes an event log from the local computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer.- or - The log does not exist on the local computer. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.-or- The log cannot be opened. A Windows error code is not available. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00048782 File Offset: 0x00046982
		public static void Delete(string logName)
		{
			EventLog.Delete(logName, ".");
		}

		/// <summary>Removes an event log from the specified computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the specified computer. </param>
		/// <param name="machineName">The name of the computer to delete the log from, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or null. - or - <paramref name="machineName" /> is not a valid computer name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer.- or - The log does not exist on the specified computer. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.-or- The log cannot be opened. A Windows error code is not available. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC2 RID: 4034 RVA: 0x00048790 File Offset: 0x00046990
		[MonoNotSupported("remote machine is not supported")]
		public static void Delete(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			if (logName == null || logName.Length == 0)
			{
				throw new ArgumentException("Log to delete was not specified.");
			}
			EventLog.CreateEventLogImpl(logName, machineName, string.Empty).Delete(logName, machineName);
		}

		/// <summary>Removes the event source registration from the event log of the local computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter does not exist in the registry of the local computer.- or - You do not have write access on the registry key for the event log.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC3 RID: 4035 RVA: 0x000487E1 File Offset: 0x000469E1
		public static void DeleteEventSource(string source)
		{
			EventLog.DeleteEventSource(source, ".");
		}

		/// <summary>Removes the application's event source registration from the specified computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system. </param>
		/// <param name="machineName">The name of the computer to remove the registration from, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid. - or - The <paramref name="source" /> parameter does not exist in the registry of the specified computer.- or - You do not have write access on the registry key for the event log.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> cannot be deleted because in the registry, the parent registry key for <paramref name="source" /> does not contain a subkey with the same name.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC4 RID: 4036 RVA: 0x000487EE File Offset: 0x000469EE
		[MonoNotSupported("remote machine is not supported")]
		public static void DeleteEventSource(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			EventLog.CreateEventLogImpl(string.Empty, machineName, source).DeleteEventSource(source, machineName);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.EventLog" />, and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000FC5 RID: 4037 RVA: 0x00048829 File Offset: 0x00046A29
		protected override void Dispose(bool disposing)
		{
			if (this.Impl != null)
			{
				this.Impl.Dispose(disposing);
			}
		}

		/// <summary>Ends the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0004883F File Offset: 0x00046A3F
		public void EndInit()
		{
			this.Impl.EndInit();
		}

		/// <summary>Determines whether the log exists on the local computer.</summary>
		/// <returns>true if the log exists on the local computer; otherwise, false.</returns>
		/// <param name="logName">The name of the log to search for. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer. </param>
		/// <exception cref="T:System.ArgumentException">The logName is null or the value is empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC7 RID: 4039 RVA: 0x0004884C File Offset: 0x00046A4C
		public static bool Exists(string logName)
		{
			return EventLog.Exists(logName, ".");
		}

		/// <summary>Determines whether the log exists on the specified computer.</summary>
		/// <returns>true if the log exists on the specified computer; otherwise, false.</returns>
		/// <param name="logName">The log for which to search. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer. </param>
		/// <param name="machineName">The name of the computer on which to search for the log, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid format. Make sure you have used proper syntax for the computer on which you are searching.-or- The <paramref name="logName" /> is null or the value is empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00048859 File Offset: 0x00046A59
		[MonoNotSupported("remote machine is not supported")]
		public static bool Exists(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			return logName != null && logName.Length != 0 && EventLog.CreateEventLogImpl(logName, machineName, string.Empty).Exists(logName, machineName);
		}

		/// <summary>Searches for all event logs on the local computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the local computer.</returns>
		/// <exception cref="T:System.SystemException">You do not have read access to the registry.-or- There is no event log service on the computer. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00048896 File Offset: 0x00046A96
		public static EventLog[] GetEventLogs()
		{
			return EventLog.GetEventLogs(".");
		}

		/// <summary>Searches for all event logs on the given computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the given computer.</returns>
		/// <param name="machineName">The computer on which to search for event logs. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid computer name. </exception>
		/// <exception cref="T:System.InvalidOperationException">You do not have read access to the registry.-or- There is no event log service on the computer. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FCA RID: 4042 RVA: 0x000488A2 File Offset: 0x00046AA2
		[MonoNotSupported("remote machine is not supported")]
		public static EventLog[] GetEventLogs(string machineName)
		{
			return EventLog.CreateEventLogImpl(new EventLog()).GetEventLogs(machineName);
		}

		/// <summary>Gets the name of the log to which the specified source is registered.</summary>
		/// <returns>The name of the log associated with the specified source in the registry.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <param name="machineName">The name of the computer on which to look, or "." for the local computer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FCB RID: 4043 RVA: 0x000488B4 File Offset: 0x00046AB4
		[MonoNotSupported("remote machine is not supported")]
		public static string LogNameFromSourceName(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'MachineName'.", machineName));
			}
			return EventLog.CreateEventLogImpl(string.Empty, machineName, source).LogNameFromSourceName(source, machineName);
		}

		/// <summary>Determines whether an event source is registered on the local computer.</summary>
		/// <returns>true if the event source is registered on the local computer; otherwise, false.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FCC RID: 4044 RVA: 0x000488EF File Offset: 0x00046AEF
		public static bool SourceExists(string source)
		{
			return EventLog.SourceExists(source, ".");
		}

		/// <summary>Determines whether an event source is registered on a specified computer.</summary>
		/// <returns>true if the event source is registered on the given computer; otherwise, false.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <param name="machineName">The name the computer on which to look, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="machineName" /> is an invalid computer name. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FCD RID: 4045 RVA: 0x000488FC File Offset: 0x00046AFC
		[MonoNotSupported("remote machine is not supported")]
		public static bool SourceExists(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			return EventLog.CreateEventLogImpl(string.Empty, machineName, source).SourceExists(source, machineName);
		}

		/// <summary>Writes an information type entry, with the given message text, to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" />  is not valid.- or -The source is already registered for a different event log.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FCE RID: 4046 RVA: 0x00048937 File Offset: 0x00046B37
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" />  is not valid.- or -The source is already registered for a different event log.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FCF RID: 4047 RVA: 0x00048941 File Offset: 0x00046B41
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0004894C File Offset: 0x00046B4C
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00048958 File Offset: 0x00046B58
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, and appends binary data to the message.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00048966 File Offset: 0x00046B66
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			this.WriteEntry(new string[] { message }, type, (long)eventID, category, rawData);
		}

		/// <summary>Writes an information type entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004897F File Offset: 0x00046B7F
		public static void WriteEntry(string source, string message)
		{
			EventLog.WriteEntry(source, message, EventLogEntryType.Information);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD4 RID: 4052 RVA: 0x00048989 File Offset: 0x00046B89
		public static void WriteEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD5 RID: 4053 RVA: 0x00048994 File Offset: 0x00046B94
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, using the specified registered event source. The <paramref name="category" /> can be used by the Event Viewer to filter events in the log.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD6 RID: 4054 RVA: 0x000489A0 File Offset: 0x00046BA0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log (using the specified registered event source) and appends binary data to the message.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD7 RID: 4055 RVA: 0x000489B0 File Offset: 0x00046BB0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEntry(message, type, eventID, category, rawData);
			}
		}

		/// <summary>Writes a localized entry to the event log.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD8 RID: 4056 RVA: 0x000489F4 File Offset: 0x00046BF4
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD9 RID: 4057 RVA: 0x00048A00 File Offset: 0x00046C00
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			string[] array;
			if (values != null)
			{
				array = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
					{
						array[i] = string.Empty;
					}
					else
					{
						array[i] = values[i].ToString();
					}
				}
			}
			else
			{
				array = new string[0];
			}
			this.WriteEntry(array, instance.EntryType, instance.InstanceId, (short)instance.CategoryId, data);
		}

		/// <summary>Writes an event log entry with the given event data and message replacement strings, using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer. </param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FDA RID: 4058 RVA: 0x00048A75 File Offset: 0x00046C75
		public static void WriteEvent(string source, EventInstance instance, params object[] values)
		{
			EventLog.WriteEvent(source, instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data, and using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer. </param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FDB RID: 4059 RVA: 0x00048A80 File Offset: 0x00046C80
		public static void WriteEvent(string source, EventInstance instance, byte[] data, params object[] values)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEvent(instance, data, values);
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal void OnEntryWritten(EventLogEntry newEntry)
		{
			if (this.doRaiseEvents && this.EntryWritten != null)
			{
				this.EntryWritten(this, new EntryWrittenEventArgs(newEntry));
			}
		}

		/// <summary>Occurs when an entry is written to an event log on the local computer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000FDD RID: 4061 RVA: 0x00048AE4 File Offset: 0x00046CE4
		// (remove) Token: 0x06000FDE RID: 4062 RVA: 0x00048B1C File Offset: 0x00046D1C
		[MonitoringDescription("Raised for each EventLog entry written.")]
		public event EntryWrittenEventHandler EntryWritten;

		// Token: 0x06000FDF RID: 4063 RVA: 0x00048B51 File Offset: 0x00046D51
		internal string GetLogName()
		{
			if (this.logName != null && this.logName.Length > 0)
			{
				return this.logName;
			}
			this.logName = EventLog.LogNameFromSourceName(this.source, this.machineName);
			return this.logName;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00048B8D File Offset: 0x00046D8D
		private static EventLogImpl CreateEventLogImpl(string logName, string machineName, string source)
		{
			return EventLog.CreateEventLogImpl(new EventLog(logName, machineName, source));
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00048B9C File Offset: 0x00046D9C
		private static EventLogImpl CreateEventLogImpl(EventLog eventLog)
		{
			string eventLogImplType = EventLog.EventLogImplType;
			if (eventLogImplType == "local")
			{
				return new LocalFileEventLog(eventLog);
			}
			if (eventLogImplType == "win32")
			{
				return new Win32EventLog(eventLog);
			}
			if (!(eventLogImplType == "null"))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", EventLog.EventLogImplType));
			}
			return new NullEventLog(eventLog);
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00048C06 File Offset: 0x00046E06
		private static bool Win32EventLogEnabled
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00048C18 File Offset: 0x00046E18
		private static string EventLogImplType
		{
			get
			{
				string text = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (text == null)
				{
					if (EventLog.Win32EventLogEnabled)
					{
						return "win32";
					}
					text = "null";
				}
				else if (EventLog.Win32EventLogEnabled && string.Compare(text, "win32", true) == 0)
				{
					text = "win32";
				}
				else if (string.Compare(text, "null", true) == 0)
				{
					text = "null";
				}
				else
				{
					if (string.Compare(text, 0, "local", 0, "local".Length, true) != 0)
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", text));
					}
					text = "local";
				}
				return text;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00048CB4 File Offset: 0x00046EB4
		private void WriteEntry(string[] replacementStrings, EventLogEntryType type, long instanceID, short category, byte[] rawData)
		{
			if (this.Source.Length == 0)
			{
				throw new ArgumentException("Source property was not setbefore writing to the event log.");
			}
			if (!Enum.IsDefined(typeof(EventLogEntryType), type))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(EventLogEntryType));
			}
			this.ValidateEventID(instanceID);
			if (!EventLog.SourceExists(this.Source, this.MachineName))
			{
				if (this.Log == null || this.Log.Length == 0)
				{
					this.Log = "Application";
				}
				EventLog.CreateEventSource(this.Source, this.Log, this.MachineName);
			}
			else if (this.logName != null && this.logName.Length != 0)
			{
				string text = EventLog.LogNameFromSourceName(this.Source, this.MachineName);
				if (string.Compare(this.logName, text, true, CultureInfo.InvariantCulture) != 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered in log '{1}' (it is registered in log '{2}'). The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property.", this.Source, this.logName, text));
				}
			}
			if (rawData == null)
			{
				rawData = new byte[0];
			}
			this.Impl.WriteEntry(replacementStrings, type, (uint)instanceID, category, rawData);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00048DD8 File Offset: 0x00046FD8
		private void ValidateEventID(long instanceID)
		{
			int eventID = EventLog.GetEventID(instanceID);
			if (eventID < 0 || eventID > 65535)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'.", instanceID, 0, ushort.MaxValue));
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00048E24 File Offset: 0x00047024
		internal static int GetEventID(long instanceID)
		{
			int num = (int)(((instanceID < 0L) ? (-instanceID) : instanceID) & 1073741823L);
			if (instanceID >= 0L)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x04001125 RID: 4389
		private string source;

		// Token: 0x04001126 RID: 4390
		private string logName;

		// Token: 0x04001127 RID: 4391
		private string machineName;

		// Token: 0x04001128 RID: 4392
		private bool doRaiseEvents;

		// Token: 0x04001129 RID: 4393
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400112A RID: 4394
		internal const string LOCAL_FILE_IMPL = "local";

		// Token: 0x0400112B RID: 4395
		private const string WIN32_IMPL = "win32";

		// Token: 0x0400112C RID: 4396
		private const string NULL_IMPL = "null";

		// Token: 0x0400112D RID: 4397
		internal const string EVENTLOG_TYPE_VAR = "MONO_EVENTLOG_TYPE";

		// Token: 0x0400112E RID: 4398
		private EventLogImpl Impl;
	}
}
