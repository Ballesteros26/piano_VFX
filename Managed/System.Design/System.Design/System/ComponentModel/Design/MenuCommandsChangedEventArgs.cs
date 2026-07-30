using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> event.</summary>
	// Token: 0x0200012E RID: 302
	[ComVisible(true)]
	public class MenuCommandsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommandsChangedEventArgs" /> class.</summary>
		/// <param name="changeType">The type of change.</param>
		/// <param name="command">The menu command.</param>
		// Token: 0x060008FB RID: 2299 RVA: 0x0000F7B1 File Offset: 0x0000D9B1
		public MenuCommandsChangedEventArgs(MenuCommandsChangedType changeType, MenuCommand command)
		{
			this.change_type = changeType;
			this.command = command;
		}

		/// <summary>Gets the type of change that caused <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> to be raised.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.MenuCommandsChangedType" /> that caused <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> to be raised.</returns>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0000F7C7 File Offset: 0x0000D9C7
		public MenuCommandsChangedType ChangeType
		{
			get
			{
				return this.change_type;
			}
		}

		/// <summary>Gets the command that was added, removed, or changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.MenuCommand" /> that was added, removed, or changed.</returns>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0000F7CF File Offset: 0x0000D9CF
		public MenuCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x040001FD RID: 509
		private MenuCommandsChangedType change_type;

		// Token: 0x040001FE RID: 510
		private MenuCommand command;
	}
}
