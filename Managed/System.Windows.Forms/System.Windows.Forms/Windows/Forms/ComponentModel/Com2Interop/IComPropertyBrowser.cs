using System;
using System.ComponentModel.Design;
using Microsoft.Win32;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	/// <summary>Allows Visual Studio to communicate internally with the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	// Token: 0x0200009F RID: 159
	public interface IComPropertyBrowser
	{
		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is browsing a COM object and the user renames the object.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000790 RID: 1936
		// (remove) Token: 0x06000791 RID: 1937
		event ComponentRenameEventHandler ComComponentNameChanged;

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object; otherwise, false.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000792 RID: 1938
		bool InPropertySet { get; }

		/// <summary>Closes any open drop-down controls on the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
		// Token: 0x06000793 RID: 1939
		void DropDownDone();

		/// <summary>Commits all pending changes to the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.PropertyGrid" /> successfully commits changes; otherwise, false.</returns>
		// Token: 0x06000794 RID: 1940
		bool EnsurePendingChangesCommitted();

		/// <summary>Activates the <see cref="T:System.Windows.Forms.PropertyGrid" /> control when the user chooses Properties for a control in Design view.</summary>
		// Token: 0x06000795 RID: 1941
		void HandleF4();

		/// <summary>Loads user states from the registry into the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
		/// <param name="key">The registry key that contains the user states.</param>
		// Token: 0x06000796 RID: 1942
		void LoadState(RegistryKey key);

		/// <summary>Saves user states from the <see cref="T:System.Windows.Forms.PropertyGrid" /> control to the registry.</summary>
		/// <param name="key">The registry key that contains the user states.</param>
		// Token: 0x06000797 RID: 1943
		void SaveState(RegistryKey key);
	}
}
