using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the locations where the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> attribute is respected during code generation for a resource by a <see cref="T:System.Web.Compilation.BuildProvider" /> object.</summary>
	// Token: 0x02000601 RID: 1537
	[Flags]
	public enum BuildProviderAppliesTo
	{
		/// <summary>Specifies that the build provider generates code for only those resources in Web content directories, which are directories other than the reserved ASP.NET directories \App_Code, \App_GlobalResources, and \App_LocalResources.</summary>
		// Token: 0x040023A9 RID: 9129
		Web = 1,
		/// <summary>Specifies that the build provider generates code for only those resources in the \App_Code directory.</summary>
		// Token: 0x040023AA RID: 9130
		Code = 2,
		/// <summary>Specifies that the build provider generates code for resources in the \App_GlobalResources and \App_LocalResources directories.</summary>
		// Token: 0x040023AB RID: 9131
		Resources = 4,
		/// <summary>Specifies that the build provider generates code for resources wherever the resources are found. This is the default value for the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> attribute.</summary>
		// Token: 0x040023AC RID: 9132
		All = 7
	}
}
