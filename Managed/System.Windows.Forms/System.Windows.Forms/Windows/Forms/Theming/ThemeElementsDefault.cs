using System;
using System.Windows.Forms.Theming.Default;

namespace System.Windows.Forms.Theming
{
	// Token: 0x020004C6 RID: 1222
	internal class ThemeElementsDefault
	{
		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x0012F638 File Offset: 0x0012D838
		public virtual TabControlPainter TabControlPainter
		{
			get
			{
				if (this.tabControlPainter == null)
				{
					this.tabControlPainter = new TabControlPainter();
				}
				return this.tabControlPainter;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x0012F658 File Offset: 0x0012D858
		public virtual ButtonPainter ButtonPainter
		{
			get
			{
				if (this.buttonPainter == null)
				{
					this.buttonPainter = new ButtonPainter();
				}
				return this.buttonPainter;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0012F678 File Offset: 0x0012D878
		public virtual LabelPainter LabelPainter
		{
			get
			{
				if (this.labelPainter == null)
				{
					this.labelPainter = new LabelPainter();
				}
				return this.labelPainter;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x0012F698 File Offset: 0x0012D898
		public virtual LinkLabelPainter LinkLabelPainter
		{
			get
			{
				if (this.linklabelPainter == null)
				{
					this.linklabelPainter = new LinkLabelPainter();
				}
				return this.linklabelPainter;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x0012F6B8 File Offset: 0x0012D8B8
		public virtual ToolStripPainter ToolStripPainter
		{
			get
			{
				if (this.toolStripPainter == null)
				{
					this.toolStripPainter = new ToolStripPainter();
				}
				return this.toolStripPainter;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x0012F6D8 File Offset: 0x0012D8D8
		public virtual CheckBoxPainter CheckBoxPainter
		{
			get
			{
				if (this.checkBoxPainter == null)
				{
					this.checkBoxPainter = new CheckBoxPainter();
				}
				return this.checkBoxPainter;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0012F6F8 File Offset: 0x0012D8F8
		public virtual RadioButtonPainter RadioButtonPainter
		{
			get
			{
				if (this.radioButtonPainter == null)
				{
					this.radioButtonPainter = new RadioButtonPainter();
				}
				return this.radioButtonPainter;
			}
		}

		// Token: 0x040029EB RID: 10731
		protected TabControlPainter tabControlPainter;

		// Token: 0x040029EC RID: 10732
		protected ButtonPainter buttonPainter;

		// Token: 0x040029ED RID: 10733
		protected LabelPainter labelPainter;

		// Token: 0x040029EE RID: 10734
		protected LinkLabelPainter linklabelPainter;

		// Token: 0x040029EF RID: 10735
		protected ToolStripPainter toolStripPainter;

		// Token: 0x040029F0 RID: 10736
		protected CheckBoxPainter checkBoxPainter;

		// Token: 0x040029F1 RID: 10737
		protected RadioButtonPainter radioButtonPainter;
	}
}
