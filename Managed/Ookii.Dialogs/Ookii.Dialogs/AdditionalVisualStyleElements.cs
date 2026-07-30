using System;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	// Token: 0x02000002 RID: 2
	public static class AdditionalVisualStyleElements
	{
		// Token: 0x02000048 RID: 72
		public static class TextStyle
		{
			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060002EE RID: 750 RVA: 0x0000A174 File Offset: 0x00008374
			public static VisualStyleElement MainInstruction
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TextStyle._mainInstruction) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TextStyle._mainInstruction = VisualStyleElement.CreateElement("TEXTSTYLE", 1, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000A1A4 File Offset: 0x000083A4
			public static VisualStyleElement BodyText
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TextStyle._bodyText) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TextStyle._bodyText = VisualStyleElement.CreateElement("TEXTSTYLE", 4, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x040000ED RID: 237
			private const string _className = "TEXTSTYLE";

			// Token: 0x040000EE RID: 238
			private static VisualStyleElement _mainInstruction;

			// Token: 0x040000EF RID: 239
			private static VisualStyleElement _bodyText;
		}

		// Token: 0x02000049 RID: 73
		public static class TaskDialog
		{
			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000A1D4 File Offset: 0x000083D4
			public static VisualStyleElement PrimaryPanel
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TaskDialog._primaryPanel) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TaskDialog._primaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 1, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000A204 File Offset: 0x00008404
			public static VisualStyleElement SecondaryPanel
			{
				get
				{
					VisualStyleElement visualStyleElement;
					if ((visualStyleElement = AdditionalVisualStyleElements.TaskDialog._secondaryPanel) == null)
					{
						visualStyleElement = (AdditionalVisualStyleElements.TaskDialog._secondaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 8, 0));
					}
					return visualStyleElement;
				}
			}

			// Token: 0x040000F0 RID: 240
			private const string _className = "TASKDIALOG";

			// Token: 0x040000F1 RID: 241
			private static VisualStyleElement _primaryPanel;

			// Token: 0x040000F2 RID: 242
			private static VisualStyleElement _secondaryPanel;
		}
	}
}
