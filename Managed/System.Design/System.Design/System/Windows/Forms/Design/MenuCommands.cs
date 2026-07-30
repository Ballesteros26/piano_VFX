using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Defines a set of <see cref="T:System.ComponentModel.Design.CommandID" /> fields that each correspond to a command function provided by the host environment.</summary>
	// Token: 0x0200002F RID: 47
	public sealed class MenuCommands : StandardCommands
	{
		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the component tray menu.</summary>
		// Token: 0x0400005C RID: 92
		public static readonly CommandID ComponentTrayMenu = new CommandID(MenuCommands.wfMenuGroup, 1286);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the container menu.</summary>
		// Token: 0x0400005D RID: 93
		public static readonly CommandID ContainerMenu = new CommandID(MenuCommands.wfMenuGroup, 1281);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the properties page for the designer.</summary>
		// Token: 0x0400005E RID: 94
		public static readonly CommandID DesignerProperties = new CommandID(MenuCommands.wfCommandSet, 4097);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the cancel key handler.</summary>
		// Token: 0x0400005F RID: 95
		public static readonly CommandID KeyCancel = new CommandID(MenuCommands.guidVSStd2K, 103);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the default key handler.</summary>
		// Token: 0x04000060 RID: 96
		public static readonly CommandID KeyDefaultAction = new CommandID(MenuCommands.guidVSStd2K, 3);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move down key handler.</summary>
		// Token: 0x04000061 RID: 97
		public static readonly CommandID KeyMoveDown = new CommandID(MenuCommands.guidVSStd2K, 13);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move left key handler.</summary>
		// Token: 0x04000062 RID: 98
		public static readonly CommandID KeyMoveLeft = new CommandID(MenuCommands.guidVSStd2K, 7);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move right key handler.</summary>
		// Token: 0x04000063 RID: 99
		public static readonly CommandID KeyMoveRight = new CommandID(MenuCommands.guidVSStd2K, 9);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move up key handler.</summary>
		// Token: 0x04000064 RID: 100
		public static readonly CommandID KeyMoveUp = new CommandID(MenuCommands.guidVSStd2K, 11);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge down key handler.</summary>
		// Token: 0x04000065 RID: 101
		public static readonly CommandID KeyNudgeDown = new CommandID(MenuCommands.guidVSStd2K, 1225);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge height decrease key handler.</summary>
		// Token: 0x04000066 RID: 102
		public static readonly CommandID KeyNudgeHeightDecrease = new CommandID(MenuCommands.guidVSStd2K, 1229);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge height increase key handler.</summary>
		// Token: 0x04000067 RID: 103
		public static readonly CommandID KeyNudgeHeightIncrease = new CommandID(MenuCommands.guidVSStd2K, 1228);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge left key handler.</summary>
		// Token: 0x04000068 RID: 104
		public static readonly CommandID KeyNudgeLeft = new CommandID(MenuCommands.guidVSStd2K, 1224);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge right key handler.</summary>
		// Token: 0x04000069 RID: 105
		public static readonly CommandID KeyNudgeRight = new CommandID(MenuCommands.guidVSStd2K, 1226);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge up key handler.</summary>
		// Token: 0x0400006A RID: 106
		public static readonly CommandID KeyNudgeUp = new CommandID(MenuCommands.guidVSStd2K, 1227);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge width decrease key handler.</summary>
		// Token: 0x0400006B RID: 107
		public static readonly CommandID KeyNudgeWidthDecrease = new CommandID(MenuCommands.guidVSStd2K, 1230);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge width increase key handler.</summary>
		// Token: 0x0400006C RID: 108
		public static readonly CommandID KeyNudgeWidthIncrease = new CommandID(MenuCommands.guidVSStd2K, 1231);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the reverse cancel key handler.</summary>
		// Token: 0x0400006D RID: 109
		public static readonly CommandID KeyReverseCancel = new CommandID(MenuCommands.wfCommandSet, 16385);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the select next key handler.</summary>
		// Token: 0x0400006E RID: 110
		public static readonly CommandID KeySelectNext = new CommandID(MenuCommands.guidVSStd2K, 4);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the select previous key handler.</summary>
		// Token: 0x0400006F RID: 111
		public static readonly CommandID KeySelectPrevious = new CommandID(MenuCommands.guidVSStd2K, 5);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size height decrease key handler.</summary>
		// Token: 0x04000070 RID: 112
		public static readonly CommandID KeySizeHeightDecrease = new CommandID(MenuCommands.guidVSStd2K, 14);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size height increase key handler.</summary>
		// Token: 0x04000071 RID: 113
		public static readonly CommandID KeySizeHeightIncrease = new CommandID(MenuCommands.guidVSStd2K, 12);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size width decrease key handler.</summary>
		// Token: 0x04000072 RID: 114
		public static readonly CommandID KeySizeWidthDecrease = new CommandID(MenuCommands.guidVSStd2K, 8);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size width increase key handler.</summary>
		// Token: 0x04000073 RID: 115
		public static readonly CommandID KeySizeWidthIncrease = new CommandID(MenuCommands.guidVSStd2K, 10);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the tab order select key handler.</summary>
		// Token: 0x04000074 RID: 116
		public static readonly CommandID KeyTabOrderSelect = new CommandID(MenuCommands.wfCommandSet, 16405);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the selection menu.</summary>
		// Token: 0x04000075 RID: 117
		public static readonly CommandID SelectionMenu = new CommandID(MenuCommands.wfMenuGroup, 1280);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the tray selection menu.</summary>
		// Token: 0x04000076 RID: 118
		public static readonly CommandID TraySelectionMenu = new CommandID(MenuCommands.wfMenuGroup, 1283);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the edit label handler.</summary>
		// Token: 0x04000077 RID: 119
		public static readonly CommandID EditLabel = new CommandID(MenuCommands.guidVSStd97, 338);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the end key handler.</summary>
		// Token: 0x04000078 RID: 120
		public static readonly CommandID KeyEnd = new CommandID(MenuCommands.guidVSStd2K, 17);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the home key handler.</summary>
		// Token: 0x04000079 RID: 121
		public static readonly CommandID KeyHome = new CommandID(MenuCommands.guidVSStd2K, 15);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the smart tag invocation handler.</summary>
		// Token: 0x0400007A RID: 122
		public static readonly CommandID KeyInvokeSmartTag = new CommandID(MenuCommands.guidVSStd2K, 147);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the SHIFT-END key handler.</summary>
		// Token: 0x0400007B RID: 123
		public static readonly CommandID KeyShiftEnd = new CommandID(MenuCommands.guidVSStd2K, 18);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the SHIFT-HOME key handler.</summary>
		// Token: 0x0400007C RID: 124
		public static readonly CommandID KeyShiftHome = new CommandID(MenuCommands.guidVSStd2K, 16);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to set the status rectangle.</summary>
		// Token: 0x0400007D RID: 125
		public static readonly CommandID SetStatusRectangle = new CommandID(MenuCommands.wfCommandSet, 16388);

		/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to set the status rectangle text.</summary>
		// Token: 0x0400007E RID: 126
		public static readonly CommandID SetStatusText = new CommandID(MenuCommands.wfCommandSet, 16387);

		// Token: 0x0400007F RID: 127
		private static readonly Guid guidVSStd97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

		// Token: 0x04000080 RID: 128
		private static readonly Guid guidVSStd2K = new Guid("{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}");

		// Token: 0x04000081 RID: 129
		private static readonly Guid wfCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x04000082 RID: 130
		private static readonly Guid wfMenuGroup = new Guid("{74D21312-2AEE-11d1-8BFB-00A0C90F26F7}");
	}
}
