using System;
using System.Security.Permissions;

namespace System.Web.Util
{
	/// <summary>Provides the ability to move work items to another thread for execution.</summary>
	// Token: 0x02000152 RID: 338
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WorkItem
	{
		/// <summary>Moves a work item to a separate thread for execution.</summary>
		/// <param name="callback">A <see cref="T:System.Web.Util.WorkItemCallback" /> that represents the method that is to be called on a separate thread.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
		// Token: 0x06000F1A RID: 3866 RVA: 0x0002A8DC File Offset: 0x00028ADC
		[global::System.MonoTODO("Not implemented, not currently supported by Mono")]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void Post(WorkItemCallback callback)
		{
			throw new PlatformNotSupportedException("Not supported on mono");
		}
	}
}
