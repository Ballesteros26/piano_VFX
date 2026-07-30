using System;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Provides information on processes currently executing.</summary>
	// Token: 0x020000CC RID: 204
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ProcessInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessInfo" /> class.</summary>
		// Token: 0x06000B0C RID: 2828 RVA: 0x00002050 File Offset: 0x00000250
		public ProcessInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessInfo" /> class and sets internal information indicating the status of the process.</summary>
		/// <param name="startTime">A <see cref="T:System.DateTime" /> that indicates the time at which the process started. </param>
		/// <param name="age">The <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started. </param>
		/// <param name="processID">The ID number assigned to the process. </param>
		/// <param name="requestCount">The number of start requests for the process. </param>
		/// <param name="status">One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the current status of the process. </param>
		/// <param name="shutdownReason">One of the <see cref="T:System.Web.ProcessShutdownReason" /> values. </param>
		/// <param name="peakMemoryUsed">The maximum memory used, in kilobytes (KB). </param>
		// Token: 0x06000B0D RID: 2829 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		public ProcessInfo(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
		{
			this.age = age;
			this.peakMemoryUsed = peakMemoryUsed;
			this.processID = processID;
			this.requestCount = requestCount;
			this.shutdownReason = shutdownReason;
			this.startTime = startTime;
			this.status = status;
		}

		/// <summary>Gets the length of time the process has been running.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0001D031 File Offset: 0x0001B231
		public TimeSpan Age
		{
			get
			{
				return this.age;
			}
		}

		/// <summary>Gets the maximum amount of memory the process has used.</summary>
		/// <returns>The maximum memory used, in kilobytes (KB).</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001D039 File Offset: 0x0001B239
		public int PeakMemoryUsed
		{
			get
			{
				return this.peakMemoryUsed;
			}
		}

		/// <summary>Gets the ID number assigned to the process.</summary>
		/// <returns>The process ID number assigned by Windows.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001D041 File Offset: 0x0001B241
		public int ProcessID
		{
			get
			{
				return this.processID;
			}
		}

		/// <summary>Gets the number of start requests for the process.</summary>
		/// <returns>The number of requests executed by the process.</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001D049 File Offset: 0x0001B249
		public int RequestCount
		{
			get
			{
				return this.requestCount;
			}
		}

		/// <summary>Gets a value that indicates why the process shut down.</summary>
		/// <returns>On of the <see cref="T:System.Web.ProcessShutdownReason" /> values.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001D051 File Offset: 0x0001B251
		public ProcessShutdownReason ShutdownReason
		{
			get
			{
				return this.shutdownReason;
			}
		}

		/// <summary>Gets the time at which the process started.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates the time at which the process started.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0001D059 File Offset: 0x0001B259
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		/// <summary>Gets the current status of the process.</summary>
		/// <returns>One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the current status of the process.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001D061 File Offset: 0x0001B261
		public ProcessStatus Status
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Sets internal information indicating the status of the process.</summary>
		/// <param name="startTime">A <see cref="T:System.DateTime" /> that indicates the time at which the process started. </param>
		/// <param name="age">A <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started. </param>
		/// <param name="processID">The ID number assigned to the process. </param>
		/// <param name="requestCount">The number of start requests for the process. </param>
		/// <param name="status">One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the time elapsed since the process started. </param>
		/// <param name="shutdownReason">One of the <see cref="T:System.Web.ProcessShutdownReason" /> values. </param>
		/// <param name="peakMemoryUsed">The maximum memory used, in kilobytes (KB). </param>
		// Token: 0x06000B15 RID: 2837 RVA: 0x0001D069 File Offset: 0x0001B269
		public void SetAll(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
		{
			this.age = age;
			this.peakMemoryUsed = peakMemoryUsed;
			this.processID = processID;
			this.requestCount = requestCount;
			this.shutdownReason = shutdownReason;
			this.startTime = startTime;
			this.status = status;
		}

		// Token: 0x04001075 RID: 4213
		private TimeSpan age;

		// Token: 0x04001076 RID: 4214
		private int peakMemoryUsed;

		// Token: 0x04001077 RID: 4215
		private int processID;

		// Token: 0x04001078 RID: 4216
		private int requestCount;

		// Token: 0x04001079 RID: 4217
		private ProcessShutdownReason shutdownReason;

		// Token: 0x0400107A RID: 4218
		private DateTime startTime;

		// Token: 0x0400107B RID: 4219
		private ProcessStatus status;
	}
}
