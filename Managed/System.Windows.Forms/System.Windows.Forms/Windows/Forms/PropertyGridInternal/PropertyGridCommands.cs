using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	/// <summary>Contains a set of menu commands used by the designer in Visual Studio.</summary>
	// Token: 0x020002A4 RID: 676
	public class PropertyGridCommands
	{
		/// <summary>Represents the command identifier for the Commands menu item. </summary>
		// Token: 0x040015D8 RID: 5592
		public static readonly CommandID Commands;

		/// <summary>Represents the command identifier for the Description menu item.</summary>
		// Token: 0x040015D9 RID: 5593
		public static readonly CommandID Description;

		/// <summary>Represents the command identifier for the Hide menu item.</summary>
		// Token: 0x040015DA RID: 5594
		public static readonly CommandID Hide;

		/// <summary>Represents the command identifier for the Reset menu item.</summary>
		// Token: 0x040015DB RID: 5595
		public static readonly CommandID Reset;

		/// <summary>Represents the GUID for the internal property browser’s command set.</summary>
		// Token: 0x040015DC RID: 5596
		protected static readonly Guid wfcMenuCommand;

		/// <summary>Represents the GUID the internal property browser uses to create a shortcut menu.</summary>
		// Token: 0x040015DD RID: 5597
		protected static readonly Guid wfcMenuGroup;
	}
}
