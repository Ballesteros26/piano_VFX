using System;

namespace System.Windows.Forms
{
	/// <summary>Provides the functionality for a control to act as a parent for other controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C6 RID: 454
	public interface IContainerControl
	{
		/// <summary>Gets or sets the control that is active on the container control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the container control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001DD0 RID: 7632
		// (set) Token: 0x06001DD1 RID: 7633
		Control ActiveControl { get; set; }

		/// <summary>Activates a specified control.</summary>
		/// <returns>true if the control is successfully activated; otherwise, false.</returns>
		/// <param name="active">The <see cref="T:System.Windows.Forms.Control" /> being activated. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD2 RID: 7634
		bool ActivateControl(Control active);
	}
}
