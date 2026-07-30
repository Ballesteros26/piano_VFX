using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	/// <summary>Defines a method for getting information about how the scrollbars of windows need to be themed when displayed in the Visual Studio designer.</summary>
	// Token: 0x02000176 RID: 374
	public interface IContainsThemedScrollbarWindows
	{
		/// <summary>Gets an enumeration of objects that represent windows and how their scrollbars need to be themed when displayed in the Visual Studio designer.</summary>
		/// <returns>An enumeration of objects that represent windows and how their scrollbars need to be themed when displayed in the Visual Studio designer.</returns>
		// Token: 0x06000B00 RID: 2816
		IEnumerable ThemedScrollbarWindows();
	}
}
