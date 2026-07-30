using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how an assembly should be produced.</summary>
	// Token: 0x0200092A RID: 2346
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		/// <summary>Generates a primary interop assembly. For more information, see the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> attribute. A keyfile must be specified.</summary>
		// Token: 0x04002DF5 RID: 11765
		PrimaryInteropAssembly = 1,
		/// <summary>Imports all interfaces as interfaces that suppress the common language runtime's stack crawl for <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> permission. Be sure you understand the responsibilities associated with suppressing this security check. </summary>
		// Token: 0x04002DF6 RID: 11766
		UnsafeInterfaces = 2,
		/// <summary>Imports all SAFEARRAY instances as <see cref="T:System.Array" /> instead of typed, single-dimensional, zero-based managed arrays. This option is useful when dealing with multi-dimensional, non-zero-based SAFEARRAY instances, which otherwise cannot be accessed unless you edit the resulting assembly by using the MSIL Disassembler (Ildasm.exe) and MSIL Assembler (Ilasm.exe) tools.</summary>
		// Token: 0x04002DF7 RID: 11767
		SafeArrayAsSystemArray = 4,
		/// <summary>Transforms [out, retval] parameters of methods on dispatch-only interfaces (dispinterface) into return values.</summary>
		// Token: 0x04002DF8 RID: 11768
		TransformDispRetVals = 8,
		/// <summary>No special settings. This is the default.</summary>
		// Token: 0x04002DF9 RID: 11769
		None = 0,
		/// <summary>Not used.</summary>
		// Token: 0x04002DFA RID: 11770
		PreventClassMembers = 16,
		/// <summary>Imports a type library for any platform.</summary>
		// Token: 0x04002DFB RID: 11771
		ImportAsAgnostic = 2048,
		/// <summary>Imports a type library for the Itanium platform.</summary>
		// Token: 0x04002DFC RID: 11772
		ImportAsItanium = 1024,
		/// <summary>Imports a type library for the x86 64-bit platform.</summary>
		// Token: 0x04002DFD RID: 11773
		ImportAsX64 = 512,
		/// <summary>Imports a type library for the x86 platform.</summary>
		// Token: 0x04002DFE RID: 11774
		ImportAsX86 = 256,
		/// <summary>Uses reflection-only loading.</summary>
		// Token: 0x04002DFF RID: 11775
		ReflectionOnlyLoading = 4096,
		/// <summary>Uses serializable classes.</summary>
		// Token: 0x04002E00 RID: 11776
		SerializableValueClasses = 32,
		/// <summary>Prevents inclusion of a version resource in the interop assembly. For more information, see the <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource" /> method.</summary>
		// Token: 0x04002E01 RID: 11777
		NoDefineVersionResource = 8192,
		/// <summary>Imports a library for the ARM platform.</summary>
		// Token: 0x04002E02 RID: 11778
		ImportAsArm = 16384
	}
}
