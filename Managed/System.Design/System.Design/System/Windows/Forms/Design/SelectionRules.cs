using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Defines identifiers that are used to indicate selection rules for a component.</summary>
	// Token: 0x02000037 RID: 55
	[Flags]
	public enum SelectionRules
	{
		/// <summary>Indicates the component supports sizing in all directions.</summary>
		// Token: 0x040000D9 RID: 217
		AllSizeable = 15,
		/// <summary>Indicates the component supports resize from the bottom.</summary>
		// Token: 0x040000DA RID: 218
		BottomSizeable = 2,
		/// <summary>Indicates the component supports resize from the left.</summary>
		// Token: 0x040000DB RID: 219
		LeftSizeable = 4,
		/// <summary>Indicates the component is locked to its container. Overrides the <see cref="F:System.Windows.Forms.Design.SelectionRules.Moveable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.AllSizeable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.BottomSizeable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.LeftSizeable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.RightSizeable" />, and <see cref="F:System.Windows.Forms.Design.SelectionRules.TopSizeable" /> bit flags of this enumeration.</summary>
		// Token: 0x040000DC RID: 220
		Locked = -2147483648,
		/// <summary>Indicates the component supports a location property that allows it to be moved on the screen.</summary>
		// Token: 0x040000DD RID: 221
		Moveable = 268435456,
		/// <summary>Indicates no special selection attributes.</summary>
		// Token: 0x040000DE RID: 222
		None = 0,
		/// <summary>Indicates the component supports resize from the right.</summary>
		// Token: 0x040000DF RID: 223
		RightSizeable = 8,
		/// <summary>Indicates the component supports resize from the top.</summary>
		// Token: 0x040000E0 RID: 224
		TopSizeable = 1,
		/// <summary>Indicates the component has some form of visible user interface and the selection service is drawing a selection border around this user interface. If a selected component has this rule set, you can assume that the component implements <see cref="T:System.ComponentModel.IComponent" /> and that it is associated with a corresponding designer instance.</summary>
		// Token: 0x040000E1 RID: 225
		Visible = 1073741824
	}
}
