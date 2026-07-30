using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides methods to manage the global designer verbs and menu commands available in design mode, and to show some types of shortcut menus.</summary>
	// Token: 0x02000333 RID: 819
	[ComVisible(true)]
	public interface IMenuCommandService
	{
		/// <summary>Gets a collection of the designer verbs that are currently available.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the designer verbs that are currently available.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060019DE RID: 6622
		DesignerVerbCollection Verbs { get; }

		/// <summary>Adds the specified standard menu command to the menu.</summary>
		/// <param name="command">The <see cref="T:System.ComponentModel.Design.MenuCommand" /> to add. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.CommandID" /> of the specified <see cref="T:System.ComponentModel.Design.MenuCommand" /> is already present on a menu. </exception>
		// Token: 0x060019DF RID: 6623
		void AddCommand(MenuCommand command);

		/// <summary>Adds the specified designer verb to the set of global designer verbs.</summary>
		/// <param name="verb">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add. </param>
		// Token: 0x060019E0 RID: 6624
		void AddVerb(DesignerVerb verb);

		/// <summary>Searches for the specified command ID and returns the menu command associated with it.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the command ID, or null if no command is found.</returns>
		/// <param name="commandID">The <see cref="T:System.ComponentModel.Design.CommandID" /> to search for. </param>
		// Token: 0x060019E1 RID: 6625
		MenuCommand FindCommand(CommandID commandID);

		/// <summary>Invokes a menu or designer verb command matching the specified command ID.</summary>
		/// <returns>true if the command was found and invoked successfully; otherwise, false.</returns>
		/// <param name="commandID">The <see cref="T:System.ComponentModel.Design.CommandID" /> of the command to search for and execute. </param>
		// Token: 0x060019E2 RID: 6626
		bool GlobalInvoke(CommandID commandID);

		/// <summary>Removes the specified standard menu command from the menu.</summary>
		/// <param name="command">The <see cref="T:System.ComponentModel.Design.MenuCommand" /> to remove. </param>
		// Token: 0x060019E3 RID: 6627
		void RemoveCommand(MenuCommand command);

		/// <summary>Removes the specified designer verb from the collection of global designer verbs.</summary>
		/// <param name="verb">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove. </param>
		// Token: 0x060019E4 RID: 6628
		void RemoveVerb(DesignerVerb verb);

		/// <summary>Shows the specified shortcut menu at the specified location.</summary>
		/// <param name="menuID">The <see cref="T:System.ComponentModel.Design.CommandID" /> for the shortcut menu to show. </param>
		/// <param name="x">The x-coordinate at which to display the menu, in screen coordinates. </param>
		/// <param name="y">The y-coordinate at which to display the menu, in screen coordinates. </param>
		// Token: 0x060019E5 RID: 6629
		void ShowContextMenu(CommandID menuID, int x, int y);
	}
}
