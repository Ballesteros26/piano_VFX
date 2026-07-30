using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that inform <see cref="M:System.Windows.Forms.ContainerControl.ValidateChildren(System.Windows.Forms.ValidationConstraints)" /> about how it should validate a container's child controls. </summary>
	// Token: 0x020003A5 RID: 933
	[Flags]
	public enum ValidationConstraints
	{
		/// <summary>Validates all child controls, and all children of these child controls, regardless of their property settings. </summary>
		// Token: 0x04001C89 RID: 7305
		None = 0,
		/// <summary>Validates child controls that can be selected.</summary>
		// Token: 0x04001C8A RID: 7306
		Selectable = 1,
		/// <summary>Validates child controls whose <see cref="P:System.Windows.Forms.Control.Enabled" /> property is set to true.</summary>
		// Token: 0x04001C8B RID: 7307
		Enabled = 2,
		/// <summary>Validates child controls whose <see cref="P:System.Windows.Forms.Control.Visible" /> property is set to true.</summary>
		// Token: 0x04001C8C RID: 7308
		Visible = 4,
		/// <summary>Validates child controls that have a <see cref="P:System.Windows.Forms.Control.TabStop" /> value set, which means that the user can navigate to the control using the TAB key. </summary>
		// Token: 0x04001C8D RID: 7309
		TabStop = 8,
		/// <summary>Validates child controls that are directly hosted within the container. Does not validate any of the children of these children. For example, if you have a <see cref="T:System.Windows.Forms.Form" /> that contains a custom <see cref="T:System.Windows.Forms.UserControl" />, and the <see cref="T:System.Windows.Forms.UserControl" /> contains a <see cref="T:System.Windows.Forms.Button" />, using <see cref="F:System.Windows.Forms.ValidationConstraints.ImmediateChildren" /> will cause the <see cref="E:System.Windows.Forms.Control.Validating" /> event of the <see cref="T:System.Windows.Forms.UserControl" /> to occur, but not the <see cref="E:System.Windows.Forms.Control.Validating" /> event of the <see cref="T:System.Windows.Forms.Button" />. </summary>
		// Token: 0x04001C8E RID: 7310
		ImmediateChildren = 16
	}
}
