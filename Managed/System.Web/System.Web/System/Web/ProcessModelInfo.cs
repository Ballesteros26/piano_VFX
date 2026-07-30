using System;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Contains methods that return information about worker processes. </summary>
	// Token: 0x020000CD RID: 205
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ProcessModelInfo
	{
		/// <summary>Returns information about the worker process that is executing the current request.</summary>
		/// <returns>A <see cref="T:System.Web.ProcessInfo" /> that contains information about the current process.</returns>
		/// <exception cref="T:System.Web.HttpException">Process information is not available for the current request. </exception>
		// Token: 0x06000B17 RID: 2839 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		[global::System.MonoTODO("Retrieve appropriate variables from worker")]
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public static ProcessInfo GetCurrentProcessInfo()
		{
			DateTime now = DateTime.Now;
			TimeSpan zero = TimeSpan.Zero;
			int num = 0;
			int num2 = 0;
			ProcessStatus processStatus = ProcessStatus.Terminated;
			ProcessShutdownReason processShutdownReason = ProcessShutdownReason.None;
			int num3 = 0;
			return new ProcessInfo(now, zero, num, num2, processStatus, processShutdownReason, num3);
		}

		/// <summary>Returns information about recent worker processes.</summary>
		/// <returns>An array of the most recent <see cref="T:System.Web.ProcessInfo" /> objects (up to 100); otherwise, if the number of available objects is less than <paramref name="numRecords" />, all available objects.</returns>
		/// <param name="numRecords">The number of processes for which information is requested. </param>
		/// <exception cref="T:System.Web.HttpException">Process information is not available. </exception>
		// Token: 0x06000B18 RID: 2840 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Retrieve process information.")]
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public static ProcessInfo[] GetHistory(int numRecords)
		{
			throw new NotImplementedException();
		}
	}
}
