using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the callbacks that the type library exporter makes when exporting a type library.</summary>
	// Token: 0x02000912 RID: 2322
	[ComVisible(true)]
	[Serializable]
	public enum ExporterEventKind
	{
		/// <summary>Specifies that the event is invoked when a type has been exported.</summary>
		// Token: 0x04002D97 RID: 11671
		NOTIF_TYPECONVERTED,
		/// <summary>Specifies that the event is invoked when a warning occurs during conversion.</summary>
		// Token: 0x04002D98 RID: 11672
		NOTIF_CONVERTWARNING,
		/// <summary>This value is not supported in this version of the .NET Framework.</summary>
		// Token: 0x04002D99 RID: 11673
		ERROR_REFTOINVALIDASSEMBLY
	}
}
