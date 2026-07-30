using System;

namespace System.Web.UI.Design
{
	/// <summary>A delegate that refers to a method in a custom <see cref="T:System.ComponentModel.Design.DesignerActionList" /> object that is to be called by the <see cref="Overload:System.Web.UI.Design.ControlDesigner.InvokeTransactedChange" /> method for implementing property changes in the designer's associated control.</summary>
	/// <returns>true if the transaction completed successfully; false if the transaction should be rolled back.</returns>
	/// <param name="context">The method to call when the transaction is invoked.</param>
	// Token: 0x020000AC RID: 172
	// (Invoke) Token: 0x0600052C RID: 1324
	public delegate bool TransactedChangeCallback(object context);
}
