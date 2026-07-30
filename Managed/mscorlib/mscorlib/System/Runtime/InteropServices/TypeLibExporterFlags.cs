using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how a type library should be produced.</summary>
	// Token: 0x02000929 RID: 2345
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibExporterFlags
	{
		/// <summary>Exports references to types that were imported from COM as IUnknown if the type does not have a registered type library. Set this flag when you want the type library exporter to look for dependent types in the registry rather than in the same directory as the input assembly.</summary>
		// Token: 0x04002DEE RID: 11758
		OnlyReferenceRegistered = 1,
		/// <summary>Specifies no flags. This is the default.</summary>
		// Token: 0x04002DEF RID: 11759
		None = 0,
		/// <summary>Allows the caller to explicitly resolve type library references without consulting the registry.</summary>
		// Token: 0x04002DF0 RID: 11760
		CallerResolvedReferences = 2,
		/// <summary>When exporting type libraries, the .NET Framework resolves type name conflicts by decorating the type with the name of the namespace; for example, System.Windows.Forms.HorizontalAlignment is exported as System_Windows_Forms_HorizontalAlignment. When there is a conflict with the name of a type that is not visible from COM, the .NET Framework exports the undecorated name. Set the <see cref="F:System.Runtime.InteropServices.TypeLibExporterFlags.OldNames" /> flag or use the /oldnames option in the Type Library Exporter (Tlbexp.exe) to force the .NET Framework to export the decorated name. Note that exporting the decorated name was the default behavior in versions prior to the .NET Framework version 2.0.</summary>
		// Token: 0x04002DF1 RID: 11761
		OldNames = 4,
		/// <summary>When compiling on a 64-bit computer, specifies that the Type Library Exporter (Tlbexp.exe) generates a 32-bit type library. All data types are transformed appropriately.</summary>
		// Token: 0x04002DF2 RID: 11762
		ExportAs32Bit = 16,
		/// <summary>When compiling on a 32-bit computer, specifies that the Type Library Exporter (Tlbexp.exe) generates a 64-bit type library. All data types are transformed appropriately.</summary>
		// Token: 0x04002DF3 RID: 11763
		ExportAs64Bit = 32
	}
}
