using System;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides trace event data specific to a thread and a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C3 RID: 451
	public class TraceEventCache
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003FBC5 File Offset: 0x0003DDC5
		internal Guid ActivityId
		{
			get
			{
				return Trace.CorrelationManager.ActivityId;
			}
		}

		/// <summary>Gets the call stack for the current thread.</summary>
		/// <returns>A string containing stack trace information. This value can be an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" />
		/// </PermissionSet>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0003FBD1 File Offset: 0x0003DDD1
		public string Callstack
		{
			get
			{
				if (this.stackTrace == null)
				{
					this.stackTrace = Environment.StackTrace;
				}
				return this.stackTrace;
			}
		}

		/// <summary>Gets the correlation data, contained in a stack. </summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> containing correlation data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003FBEC File Offset: 0x0003DDEC
		public Stack LogicalOperationStack
		{
			get
			{
				return Trace.CorrelationManager.LogicalOperationStack;
			}
		}

		/// <summary>Gets the date and time at which the event trace occurred.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure whose value is a date and time expressed in Coordinated Universal Time (UTC).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
		public DateTime DateTime
		{
			get
			{
				if (this.dateTime == DateTime.MinValue)
				{
					this.dateTime = DateTime.UtcNow;
				}
				return this.dateTime;
			}
		}

		/// <summary>Gets the unique identifier of the current process.</summary>
		/// <returns>The system-generated unique identifier of the current process.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003FC1D File Offset: 0x0003DE1D
		public int ProcessId
		{
			get
			{
				return TraceEventCache.GetProcessId();
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.  </summary>
		/// <returns>A string that represents a unique integer identifier for this managed thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0003FC24 File Offset: 0x0003DE24
		public string ThreadId
		{
			get
			{
				return TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>The tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003FC43 File Offset: 0x0003DE43
		public long Timestamp
		{
			get
			{
				if (this.timeStamp == -1L)
				{
					this.timeStamp = Stopwatch.GetTimestamp();
				}
				return this.timeStamp;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003FC60 File Offset: 0x0003DE60
		private static void InitProcessInfo()
		{
			if (TraceEventCache.processName == null)
			{
				Process currentProcess = Process.GetCurrentProcess();
				try
				{
					TraceEventCache.processId = currentProcess.Id;
					TraceEventCache.processName = currentProcess.ProcessName;
				}
				finally
				{
					currentProcess.Dispose();
				}
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
		internal static int GetProcessId()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processId;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003FCBE File Offset: 0x0003DEBE
		internal static string GetProcessName()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processName;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003FCCC File Offset: 0x0003DECC
		internal static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x04001050 RID: 4176
		private static volatile int processId;

		// Token: 0x04001051 RID: 4177
		private static volatile string processName;

		// Token: 0x04001052 RID: 4178
		private long timeStamp = -1L;

		// Token: 0x04001053 RID: 4179
		private DateTime dateTime = DateTime.MinValue;

		// Token: 0x04001054 RID: 4180
		private string stackTrace;
	}
}
