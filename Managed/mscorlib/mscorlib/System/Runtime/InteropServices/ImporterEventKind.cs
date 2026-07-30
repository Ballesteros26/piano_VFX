using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the callbacks that the type library importer makes when importing a type library.</summary>
	// Token: 0x0200091C RID: 2332
	[ComVisible(true)]
	[Serializable]
	public enum ImporterEventKind
	{
		/// <summary>Specifies that the event is invoked when a type has been imported.</summary>
		// Token: 0x04002DA4 RID: 11684
		NOTIF_TYPECONVERTED,
		/// <summary>Specifies that the event is invoked when a warning occurs during conversion.</summary>
		// Token: 0x04002DA5 RID: 11685
		NOTIF_CONVERTWARNING,
		/// <summary>This property is not supported in this version of the .NET Framework.</summary>
		// Token: 0x04002DA6 RID: 11686
		ERROR_REFTOINVALIDTYPELIB
	}
}
