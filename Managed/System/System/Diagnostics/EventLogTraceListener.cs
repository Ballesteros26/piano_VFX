using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple listener that directs tracing or debugging output to an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FC RID: 508
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class EventLogTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class without a trace listener.</summary>
		// Token: 0x0600104E RID: 4174 RVA: 0x0003F83C File Offset: 0x0003DA3C
		public EventLogTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified event log.</summary>
		/// <param name="eventLog">The event log to write to. </param>
		// Token: 0x0600104F RID: 4175 RVA: 0x00049867 File Offset: 0x00047A67
		public EventLogTraceListener(EventLog eventLog)
		{
			if (eventLog == null)
			{
				throw new ArgumentNullException("eventLog");
			}
			this.event_log = eventLog;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified source.</summary>
		/// <param name="source">The name of an existing event log source. </param>
		// Token: 0x06001050 RID: 4176 RVA: 0x00049884 File Offset: 0x00047A84
		public EventLogTraceListener(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.event_log = new EventLog();
			this.event_log.Source = source;
		}

		/// <summary>Gets or sets the event log to write to.</summary>
		/// <returns>The event log to write to.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000498B1 File Offset: 0x00047AB1
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x000498B9 File Offset: 0x00047AB9
		public EventLog EventLog
		{
			get
			{
				return this.event_log;
			}
			set
			{
				this.event_log = value;
			}
		}

		/// <summary>Gets or sets the name of this <see cref="T:System.Diagnostics.EventLogTraceListener" />.</summary>
		/// <returns>The name of this trace listener.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x000498C2 File Offset: 0x00047AC2
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x000498DE File Offset: 0x00047ADE
		public override string Name
		{
			get
			{
				if (this.name == null)
				{
					return this.event_log.Source;
				}
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Closes the event log so that it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001055 RID: 4181 RVA: 0x000498E7 File Offset: 0x00047AE7
		public override void Close()
		{
			this.event_log.Close();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000498F4 File Offset: 0x00047AF4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.event_log.Dispose();
			}
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001057 RID: 4183 RVA: 0x00049904 File Offset: 0x00047B04
		public override void Write(string message)
		{
			this.TraceData(new TraceEventCache(), this.event_log.Source, TraceEventType.Information, 0, message);
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001058 RID: 4184 RVA: 0x0004991F File Offset: 0x00047B1F
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		/// <summary>Writes trace information, a data object, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06001059 RID: 4185 RVA: 0x00049928 File Offset: 0x00047B28
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, object data)
		{
			EventLogEntryType eventLogEntryType;
			if (severity - TraceEventType.Critical > 1)
			{
				if (severity != TraceEventType.Warning)
				{
					eventLogEntryType = EventLogEntryType.Information;
				}
				else
				{
					eventLogEntryType = EventLogEntryType.Warning;
				}
			}
			else
			{
				eventLogEntryType = EventLogEntryType.Error;
			}
			this.event_log.WriteEntry((data != null) ? data.ToString() : string.Empty, eventLogEntryType, id, 0);
		}

		/// <summary>Writes trace information, an array of data objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">An array of data objects.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x0600105A RID: 4186 RVA: 0x0004996C File Offset: 0x00047B6C
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, params object[] data)
		{
			string text = string.Empty;
			if (data != null)
			{
				string[] array = new string[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					array[i] = ((data[i] != null) ? data[i].ToString() : string.Empty);
				}
				text = string.Join(", ", array);
			}
			this.TraceData(eventCache, source, severity, id, text);
		}

		/// <summary>Writes trace information, a message, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="message">The trace message.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600105B RID: 4187 RVA: 0x000499CD File Offset: 0x00047BCD
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			this.TraceData(eventCache, source, severity, id, message);
		}

		/// <summary>Writes trace information, a formatted array of objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600105C RID: 4188 RVA: 0x000499DC File Offset: 0x00047BDC
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, severity, id, (format != null) ? string.Format(format, args) : null);
		}

		// Token: 0x04001156 RID: 4438
		private EventLog event_log;

		// Token: 0x04001157 RID: 4439
		private string name;
	}
}
