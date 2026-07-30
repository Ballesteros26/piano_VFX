using System;

namespace System.Windows.Forms
{
	/// <summary>Allows a control to act like a button on a form.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C1 RID: 449
	public interface IButtonControl
	{
		/// <summary>Gets or sets the value returned to the parent form when the button is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001DC5 RID: 7621
		// (set) Token: 0x06001DC6 RID: 7622
		DialogResult DialogResult { get; set; }

		/// <summary>Notifies a control that it is the default button so that its appearance and behavior is adjusted accordingly.</summary>
		/// <param name="value">true if the control should behave as a default button; otherwise false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DC7 RID: 7623
		void NotifyDefault(bool value);

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DC8 RID: 7624
		void PerformClick();
	}
}
