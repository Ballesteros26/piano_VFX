using System;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	/// <summary>Defines the method a class implements to process an assembly after the assembly has been built.</summary>
	// Token: 0x02000608 RID: 1544
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.High)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
	public interface IAssemblyPostProcessor : IDisposable
	{
		/// <summary>Called before the assembly is loaded to allow the implementing class to modify the assembly.</summary>
		/// <param name="path">The path to the assembly.</param>
		// Token: 0x060042A9 RID: 17065
		void PostProcessAssembly(string path);
	}
}
