using System;

namespace System.Web.UI.Design
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event that is raised by visual design tools, such as Visual Studio 2005, implementing the <see cref="T:System.Web.UI.Design.IControlDesignerView" /> interface. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Web.UI.Design.ViewEventArgs" /> object that contains the event data.</param>
	// Token: 0x020000B5 RID: 181
	// (Invoke) Token: 0x06000549 RID: 1353
	public delegate void ViewEventHandler(object sender, ViewEventArgs e);
}
