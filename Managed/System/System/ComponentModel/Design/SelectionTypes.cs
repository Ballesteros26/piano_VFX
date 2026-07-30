using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate the type of a selection.</summary>
	// Token: 0x0200033E RID: 830
	[ComVisible(true)]
	[Flags]
	public enum SelectionTypes
	{
		/// <summary>Represents a regular selection. The selection service responds to the CTRL and SHIFT keys to support adding or removing components to or from the selection.</summary>
		// Token: 0x04001487 RID: 5255
		Auto = 1,
		/// <summary>Represents a regular selection. The selection service responds to the CTRL and SHIFT keys to support adding or removing components to or from the selection.</summary>
		// Token: 0x04001488 RID: 5256
		[Obsolete("This value has been deprecated. Use SelectionTypes.Auto instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Normal = 1,
		/// <summary>Represents a selection that occurs when the content of a selection is replaced. The selection service replaces the current selection with the replacement.</summary>
		// Token: 0x04001489 RID: 5257
		Replace = 2,
		/// <summary>Represents a selection that occurs when the user presses on the mouse button while the mouse pointer is over a component. If the component under the pointer is already selected, it is promoted to become the primary selected component rather than being canceled.</summary>
		// Token: 0x0400148A RID: 5258
		[Obsolete("This value has been deprecated.  It is no longer supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		MouseDown = 4,
		/// <summary>Represents a selection that occurs when the user releases the mouse button immediately after a component has been selected. If the newly selected component is already selected, it is promoted to be the primary selected component rather than being canceled.</summary>
		// Token: 0x0400148B RID: 5259
		[Obsolete("This value has been deprecated.  It is no longer supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		MouseUp = 8,
		/// <summary>Represents a selection that occurs when a user clicks a component. If the newly selected component is already selected, it is promoted to be the primary selected component rather than being canceled.</summary>
		// Token: 0x0400148C RID: 5260
		[Obsolete("This value has been deprecated. Use SelectionTypes.Primary instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Click = 16,
		/// <summary>Represents a primary selection that occurs when a user clicks on a component. If a component in the selection list is already selected, the component is promoted to be the primary selection.</summary>
		// Token: 0x0400148D RID: 5261
		Primary = 16,
		/// <summary>Represents a toggle selection that switches between the current selection and the provided selection. If a component is already selected and is passed into <see cref="Overload:System.ComponentModel.Design.ISelectionService.SetSelectedComponents" /> with a selection type of <see cref="F:System.ComponentModel.Design.SelectionTypes.Toggle" />, the component selection will be canceled.</summary>
		// Token: 0x0400148E RID: 5262
		Toggle = 32,
		/// <summary>Represents an add selection that adds the selected components to the current selection, maintaining the current set of selected components.</summary>
		// Token: 0x0400148F RID: 5263
		Add = 64,
		/// <summary>Represents a remove selection that removes the selected components from the current selection, maintaining the current set of selected components.</summary>
		// Token: 0x04001490 RID: 5264
		Remove = 128,
		/// <summary>Identifies the valid selection types as <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />, <see cref="F:System.ComponentModel.Design.SelectionTypes.Replace" />, <see cref="F:System.ComponentModel.Design.SelectionTypes.MouseDown" />, <see cref="F:System.ComponentModel.Design.SelectionTypes.MouseUp" />, or <see cref="F:System.ComponentModel.Design.SelectionTypes.Click" />.</summary>
		// Token: 0x04001491 RID: 5265
		[Obsolete("This value has been deprecated. Use Enum class methods to determine valid values, or use a type converter. http://go.microsoft.com/fwlink/?linkid=14202")]
		Valid = 31
	}
}
