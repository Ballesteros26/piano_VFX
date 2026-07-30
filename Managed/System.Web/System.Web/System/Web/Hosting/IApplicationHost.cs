using System;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	/// <summary>Retrieves information about the application host.</summary>
	// Token: 0x02000538 RID: 1336
	public interface IApplicationHost
	{
		/// <summary>Gets the application's root virtual path.</summary>
		/// <returns>The application's root virtual path.</returns>
		// Token: 0x06003A6A RID: 14954
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		string GetVirtualPath();

		/// <summary>Gets the application's root physical path.</summary>
		/// <returns>The physical path of the application root.</returns>
		// Token: 0x06003A6B RID: 14955
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		string GetPhysicalPath();

		/// <summary>Enables creation of an <see cref="T:System.Web.Configuration.IConfigMapPath" /> interface in the target application domain.</summary>
		/// <returns>An object that is used to map virtual and physical paths of the configuration file.</returns>
		// Token: 0x06003A6C RID: 14956
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		IConfigMapPathFactory GetConfigMapPathFactory();

		/// <summary>Gets the token for the application host configuration (.config) file.</summary>
		/// <returns>A Windows handle that contains the Windows security token for the application's root. The token can be used to open and read the application configuration file.</returns>
		// Token: 0x06003A6D RID: 14957
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		IntPtr GetConfigToken();

		/// <summary>Gets the site name.</summary>
		/// <returns>The site name.</returns>
		// Token: 0x06003A6E RID: 14958
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		string GetSiteName();

		/// <summary>Gets the site ID.</summary>
		/// <returns>The site ID.</returns>
		// Token: 0x06003A6F RID: 14959
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		string GetSiteID();

		/// <summary>Indicates that a message was received.</summary>
		// Token: 0x06003A70 RID: 14960
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void MessageReceived();
	}
}
