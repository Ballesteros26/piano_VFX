using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides access to the menu editing service.</summary>
	// Token: 0x02000025 RID: 37
	public interface IMenuEditorService
	{
		/// <summary>Gets the current menu.</summary>
		/// <returns>The current <see cref="T:System.Windows.Forms.Menu" />.</returns>
		// Token: 0x0600014D RID: 333
		Menu GetMenu();

		/// <summary>Indicates whether the current menu is active.</summary>
		/// <returns>true if the current menu is currently active; otherwise, false.</returns>
		// Token: 0x0600014E RID: 334
		bool IsActive();

		/// <summary>Allows the editor service to intercept Win32 messages.</summary>
		/// <returns>true if the message is for the control; otherwise, false.</returns>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x0600014F RID: 335
		bool MessageFilter(ref Message m);

		/// <summary>Sets the specified menu visible on the form.</summary>
		/// <param name="menu">The <see cref="T:System.Windows.Forms.Menu" /> to render. </param>
		// Token: 0x06000150 RID: 336
		void SetMenu(Menu menu);

		/// <summary>Sets the selected menu item of the current menu.</summary>
		/// <param name="item">A <see cref="T:System.Windows.Forms.MenuItem" /> to set as the currently selected menu item. </param>
		// Token: 0x06000151 RID: 337
		void SetSelection(MenuItem item);
	}
}
