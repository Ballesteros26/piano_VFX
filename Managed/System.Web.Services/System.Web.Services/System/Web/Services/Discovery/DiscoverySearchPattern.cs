using System;
using System.Security.Permissions;

namespace System.Web.Services.Discovery
{
	/// <summary>Establishes an interface for file extension search patterns for discoverable file types.</summary>
	// Token: 0x020000AE RID: 174
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class DiscoverySearchPattern
	{
		/// <summary>Gets the file name pattern to use as a search target.</summary>
		/// <returns>A file name pattern.</returns>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600048E RID: 1166
		public abstract string Pattern { get; }

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> object for a given file name.</summary>
		/// <returns>A file name.</returns>
		/// <param name="filename">The name of a discovery file or a file that appears in a dynamically generated discovery document. For example, an .asmx or .xsd file.</param>
		// Token: 0x0600048F RID: 1167
		public abstract DiscoveryReference GetDiscoveryReference(string filename);
	}
}
