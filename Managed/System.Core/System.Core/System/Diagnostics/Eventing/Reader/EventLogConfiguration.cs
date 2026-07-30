using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains static information and configuration settings for an event log. Many of the configurations settings were defined by the event provider that created the log.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038A RID: 906
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogConfiguration : IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogConfiguration" /> object by specifying the local event log for which to get information and configuration settings. </summary>
		/// <param name="logName">The name of the local event log for which to get information and configuration settings.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AD8 RID: 6872 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogConfiguration(string logName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogConfiguration" /> object by specifying the name of the log for which to get information and configuration settings. The log can be on the local computer or a remote computer, based on the event log session specified.</summary>
		/// <param name="logName">The name of the event log for which to get information and configuration settings.</param>
		/// <param name="session">The event log session used to determine the event log service that the specified log belongs to. The session is either connected to the event log service on the local computer or a remote computer.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AD9 RID: 6873 RVA: 0x0000220F File Offset: 0x0000040F
		[SecurityCritical]
		public EventLogConfiguration(string logName, EventLogSession session)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the flag that indicates if the event log is a classic event log. A classic event log is one that has its events defined in a .mc file instead of a manifest (.xml file) used by the event provider.</summary>
		/// <returns>Returns true if the event log is a classic log, and returns false if the event log is not a classic log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x000565C4 File Offset: 0x000547C4
		public bool IsClassicLog
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the event log is enabled or disabled. An enabled log is one in which events can be logged, and a disabled log is one in which events cannot be logged.</summary>
		/// <returns>Returns true if the log is enabled, and returns false if the log is disabled.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000565E0 File Offset: 0x000547E0
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x0000220F File Offset: 0x0000040F
		public bool IsEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the file directory path to the location of the file where the events are stored for the log.</summary>
		/// <returns>Returns a string that contains the path to the event log file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x0000220F File Offset: 0x0000040F
		public string LogFilePath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogIsolation" /> value that specifies whether the event log is an application, system, or custom event log. </summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogIsolation" /> value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000565FC File Offset: 0x000547FC
		public EventLogIsolation LogIsolation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EventLogIsolation.Application;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogMode" /> value that determines how events are handled when the event log becomes full.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogMode" /> value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00056618 File Offset: 0x00054818
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogMode LogMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EventLogMode.Circular;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the name of the event log.</summary>
		/// <returns>Returns a string that contains the name of the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x000560B4 File Offset: 0x000542B4
		public string LogName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogType" /> value that determines the type of the event log.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogType" /> value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00056634 File Offset: 0x00054834
		public EventLogType LogType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EventLogType.Administrative;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, that the event log file is allowed to be. When the file reaches this maximum size, it is considered full.</summary>
		/// <returns>Returns a long value that represents the maximum size, in bytes, that the event log file is allowed to be.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00056650 File Offset: 0x00054850
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0000220F File Offset: 0x0000040F
		public long MaximumSizeInBytes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the name of the event provider that created this event log.</summary>
		/// <returns>Returns a string that contains the name of the event provider that created this event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000560B4 File Offset: 0x000542B4
		public string OwningProviderName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the size of the buffer that the event provider uses for publishing events to the log.</summary>
		/// <returns>Returns an integer value that can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0005666C File Offset: 0x0005486C
		public int? ProviderBufferSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the control globally unique identifier (GUID) for the event log if the log is a debug log. If this log is not a debug log, this value will be null. </summary>
		/// <returns>Returns a GUID value or null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00056688 File Offset: 0x00054888
		public Guid? ProviderControlGuid
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets keyword mask used by the event provider.</summary>
		/// <returns>Returns a long value that can be null if the event provider did not define any keywords.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x000566A4 File Offset: 0x000548A4
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0000220F File Offset: 0x0000040F
		public long? ProviderKeywords
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the maximum latency time used by the event provider when publishing events to the log.</summary>
		/// <returns>Returns an integer value that can be null if no latency time was specified by the event provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000566C0 File Offset: 0x000548C0
		public int? ProviderLatency
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the maximum event level (which defines the severity of the event) that is allowed to be logged in the event log. This value is defined by the event provider.</summary>
		/// <returns>Returns an integer value that can be null if the maximum event level was not defined in the event provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000566DC File Offset: 0x000548DC
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x0000220F File Offset: 0x0000040F
		public int? ProviderLevel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the maximum number of buffers used by the event provider to publish events to the event log.</summary>
		/// <returns>Returns an integer value that is the maximum number of buffers used by the event provider to publish events to the event log. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000566F8 File Offset: 0x000548F8
		public int? ProviderMaximumNumberOfBuffers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the minimum number of buffers used by the event provider to publish events to the event log.</summary>
		/// <returns>Returns an integer value that is the minimum number of buffers used by the event provider to publish events to the event log. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00056714 File Offset: 0x00054914
		public int? ProviderMinimumNumberOfBuffers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of the names of all the event providers that can publish events to this event log.</summary>
		/// <returns>Returns an enumerable collection of strings that contain the event provider names.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x0005672F File Offset: 0x0005492F
		public IEnumerable<string> ProviderNames
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets the security descriptor of the event log. The security descriptor defines the users and groups of users that can read and write to the event log.</summary>
		/// <returns>Returns a string that contains the security descriptor for the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0000220F File Offset: 0x0000040F
		public string SecurityDescriptor
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Releases all the resources used by this object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0000220F File Offset: 0x0000040F
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0000220F File Offset: 0x0000040F
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the configuration settings that </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x0000220F File Offset: 0x0000040F
		public void SaveChanges()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
