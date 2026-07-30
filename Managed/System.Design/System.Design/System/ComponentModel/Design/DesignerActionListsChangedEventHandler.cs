using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.DesignerActionService.DesignerActionListsChanged" /> event of a <see cref="T:System.ComponentModel.Design.DesignerActionService" />. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.DesignerActionListsChangedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000113 RID: 275
	// (Invoke) Token: 0x0600080A RID: 2058
	[ComVisible(true)]
	public delegate void DesignerActionListsChangedEventHandler(object sender, DesignerActionListsChangedEventArgs e);
}
