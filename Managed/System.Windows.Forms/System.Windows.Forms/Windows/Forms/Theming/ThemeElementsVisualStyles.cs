using System;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.Theming.VisualStyles;

namespace System.Windows.Forms.Theming
{
	// Token: 0x020004C7 RID: 1223
	internal class ThemeElementsVisualStyles : ThemeElementsDefault
	{
		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x0012F720 File Offset: 0x0012D920
		public override global::System.Windows.Forms.Theming.Default.CheckBoxPainter CheckBoxPainter
		{
			get
			{
				if (this.checkBoxPainter == null)
				{
					this.checkBoxPainter = new global::System.Windows.Forms.Theming.VisualStyles.CheckBoxPainter();
				}
				return this.checkBoxPainter;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x0012F740 File Offset: 0x0012D940
		public override global::System.Windows.Forms.Theming.Default.RadioButtonPainter RadioButtonPainter
		{
			get
			{
				if (this.radioButtonPainter == null)
				{
					this.radioButtonPainter = new global::System.Windows.Forms.Theming.VisualStyles.RadioButtonPainter();
				}
				return this.radioButtonPainter;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x0012F760 File Offset: 0x0012D960
		public override global::System.Windows.Forms.Theming.Default.ToolStripPainter ToolStripPainter
		{
			get
			{
				if (this.toolStripPainter == null)
				{
					this.toolStripPainter = new global::System.Windows.Forms.Theming.VisualStyles.ToolStripPainter();
				}
				return this.toolStripPainter;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x0012F780 File Offset: 0x0012D980
		public override global::System.Windows.Forms.Theming.Default.TabControlPainter TabControlPainter
		{
			get
			{
				if (this.tabControlPainter == null)
				{
					this.tabControlPainter = new global::System.Windows.Forms.Theming.VisualStyles.TabControlPainter();
				}
				return this.tabControlPainter;
			}
		}
	}
}
