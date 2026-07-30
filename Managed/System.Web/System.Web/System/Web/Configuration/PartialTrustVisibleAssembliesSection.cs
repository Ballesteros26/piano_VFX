using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures a set of partial-trust assemblies. This class cannot be inherited.</summary>
	// Token: 0x020006AC RID: 1708
	public sealed class PartialTrustVisibleAssembliesSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembliesSection" /> class.</summary>
		// Token: 0x06004825 RID: 18469 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PartialTrustVisibleAssembliesSection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembliesSection" /> collection of APTCA-marked assemblies to use in partial-trust ASP.NET applications.</summary>
		/// <returns>A collection of the APTCA-marked assemblies to use in partial-trust ASP.NET applications.</returns>
		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public PartialTrustVisibleAssemblyCollection PartialTrustVisibleAssemblies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
