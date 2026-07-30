using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.BrowserCapabilitiesCodeGenerator" /> class is used internally by the aspnet_regbrowsers tool to parse .browser browser definition files and add browsers to the run-time collection of known browsers contained in the <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> object.</summary>
	// Token: 0x02000778 RID: 1912
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public class BrowserCapabilitiesCodeGenerator
	{
		/// <summary>Used internally to create a new instance of <see cref="T:System.Web.Configuration.BrowserCapabilitiesCodeGenerator" />.</summary>
		// Token: 0x06004D6C RID: 19820 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public BrowserCapabilitiesCodeGenerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally to coordinate the behavior of this class.</summary>
		// Token: 0x06004D6D RID: 19821 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public virtual void Create()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Loads and parses the XML contained in a collection of browser-definition files and inserts the information contained therein into an internal collection of browsers.</summary>
		/// <param name="useVirtualPath">true to use a virtual path; otherwise, false. The default is false.</param>
		/// <param name="virtualDir">The path to the virtual directory that contains the browser-definition files. The default is <see cref="F:System.String.Empty" />.</param>
		/// <exception cref="T:System.Web.HttpParseException">One of the browser-definition files does not have a root element named "browsers".- or -One of the browser-definition files fails to load.</exception>
		// Token: 0x06004D6E RID: 19822 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void ProcessBrowserFiles(bool useVirtualPath, string virtualDir)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the browser capabilities factory from the global assembly cache and deletes its strong name public key token file.</summary>
		/// <returns>true if the browser capabilities factory was uninstalled from the global assembly cache; otherwise, false.</returns>
		// Token: 0x06004D6F RID: 19823 RVA: 0x000CB340 File Offset: 0x000C9540
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public bool Uninstall()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
