using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the defined native object types.</summary>
	// Token: 0x0200060F RID: 1551
	public enum ResourceType
	{
		/// <summary>An unknown object type.</summary>
		// Token: 0x04002201 RID: 8705
		Unknown,
		/// <summary>A file or directory.</summary>
		// Token: 0x04002202 RID: 8706
		FileObject,
		/// <summary>A Windows service.</summary>
		// Token: 0x04002203 RID: 8707
		Service,
		/// <summary>A printer.</summary>
		// Token: 0x04002204 RID: 8708
		Printer,
		/// <summary>A registry key.</summary>
		// Token: 0x04002205 RID: 8709
		RegistryKey,
		/// <summary>A network share.</summary>
		// Token: 0x04002206 RID: 8710
		LMShare,
		/// <summary>A local kernel object.</summary>
		// Token: 0x04002207 RID: 8711
		KernelObject,
		/// <summary>A window station or desktop object on the local computer.</summary>
		// Token: 0x04002208 RID: 8712
		WindowObject,
		/// <summary>A directory service (DS) object or a property set or property of a directory service object.</summary>
		// Token: 0x04002209 RID: 8713
		DSObject,
		/// <summary>A directory service object and all of its property sets and properties.</summary>
		// Token: 0x0400220A RID: 8714
		DSObjectAll,
		/// <summary>An object defined by a provider.</summary>
		// Token: 0x0400220B RID: 8715
		ProviderDefined,
		/// <summary>A Windows Management Instrumentation (WMI) object.</summary>
		// Token: 0x0400220C RID: 8716
		WmiGuidObject,
		/// <summary>An object for a registry entry under WOW64.</summary>
		// Token: 0x0400220D RID: 8717
		RegistryWow6432Key
	}
}
