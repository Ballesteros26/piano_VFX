using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for a designer to select components.</summary>
	// Token: 0x02000337 RID: 823
	[ComVisible(true)]
	public interface ISelectionService
	{
		/// <summary>Gets the object that is currently the primary selected object.</summary>
		/// <returns>The object that is currently the primary selected object.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060019EF RID: 6639
		object PrimarySelection { get; }

		/// <summary>Gets the count of selected objects.</summary>
		/// <returns>The number of selected objects.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060019F0 RID: 6640
		int SelectionCount { get; }

		/// <summary>Occurs when the current selection changes.</summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060019F1 RID: 6641
		// (remove) Token: 0x060019F2 RID: 6642
		event EventHandler SelectionChanged;

		/// <summary>Occurs when the current selection is about to change.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060019F3 RID: 6643
		// (remove) Token: 0x060019F4 RID: 6644
		event EventHandler SelectionChanging;

		/// <summary>Gets a value indicating whether the specified component is currently selected.</summary>
		/// <returns>true if the component is part of the user's current selection; otherwise, false.</returns>
		/// <param name="component">The component to test. </param>
		// Token: 0x060019F5 RID: 6645
		bool GetComponentSelected(object component);

		/// <summary>Gets a collection of components that are currently selected.</summary>
		/// <returns>A collection that represents the current set of components that are selected.</returns>
		// Token: 0x060019F6 RID: 6646
		ICollection GetSelectedComponents();

		/// <summary>Selects the specified collection of components.</summary>
		/// <param name="components">The collection of components to select. </param>
		// Token: 0x060019F7 RID: 6647
		void SetSelectedComponents(ICollection components);

		/// <summary>Selects the components from within the specified collection of components that match the specified selection type.</summary>
		/// <param name="components">The collection of components to select. </param>
		/// <param name="selectionType">A value from the <see cref="T:System.ComponentModel.Design.SelectionTypes" /> enumeration. The default is <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />. </param>
		// Token: 0x060019F8 RID: 6648
		void SetSelectedComponents(ICollection components, SelectionTypes selectionType);
	}
}
