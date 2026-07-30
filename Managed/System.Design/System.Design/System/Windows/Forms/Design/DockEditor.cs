using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a user interface for specifying a <see cref="P:System.Windows.Forms.Control.Dock" /> property.</summary>
	// Token: 0x02000019 RID: 25
	public sealed class DockEditor : UITypeEditor
	{
		/// <summary>Edits the specified object value using the editor style provided by GetEditorStyle. A service provider is provided so that any required editing services can be obtained.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000104 RID: 260 RVA: 0x00003F0C File Offset: 0x0000210C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					DockEditor.DockEditorControl dockEditorControl = new DockEditor.DockEditorControl(windowsFormsEditorService);
					dockEditorControl.DockStyle = (DockStyle)value;
					windowsFormsEditorService.DropDownControl(dockEditorControl);
					return dockEditorControl.DockStyle;
				}
			}
			return base.EditValue(context, provider, value);
		}

		/// <summary>Retrieves the editing style of the EditValue method. If the method is not supported, this will return None.</summary>
		/// <returns>An enum value indicating the provided editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000105 RID: 261 RVA: 0x000020A5 File Offset: 0x000002A5
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0200001A RID: 26
		private class DockEditorControl : UserControl
		{
			// Token: 0x06000106 RID: 262 RVA: 0x00003F68 File Offset: 0x00002168
			public DockEditorControl(IWindowsFormsEditorService editorService)
			{
				this.buttonNone = new CheckBox();
				this.panel1 = new Panel();
				this.buttonBottom = new CheckBox();
				this.buttonTop = new CheckBox();
				this.panel2 = new Panel();
				this.buttonLeft = new CheckBox();
				this.buttonRight = new CheckBox();
				this.buttonFill = new CheckBox();
				this.panel1.SuspendLayout();
				this.panel2.SuspendLayout();
				base.SuspendLayout();
				this.buttonNone.Appearance = 1;
				this.buttonNone.Dock = 2;
				this.buttonNone.Location = new Point(0, 92);
				this.buttonNone.Size = new Size(150, 23);
				this.buttonNone.TabIndex = 5;
				this.buttonNone.Text = "None";
				this.buttonNone.TextAlign = ContentAlignment.MiddleLeft;
				this.buttonNone.Click += this.buttonClick;
				this.panel1.Controls.Add(this.panel2);
				this.panel1.Controls.Add(this.buttonTop);
				this.panel1.Controls.Add(this.buttonBottom);
				this.panel1.Dock = 5;
				this.panel1.Location = new Point(0, 0);
				this.panel1.Name = "panel1";
				this.panel1.Size = new Size(150, 92);
				this.panel1.TabStop = false;
				this.buttonBottom.Appearance = 1;
				this.buttonBottom.Dock = 2;
				this.buttonBottom.Location = new Point(0, 69);
				this.buttonBottom.Name = "buttonBottom";
				this.buttonBottom.Size = new Size(150, 23);
				this.buttonBottom.TabIndex = 5;
				this.buttonBottom.Click += this.buttonClick;
				this.buttonTop.Appearance = 1;
				this.buttonTop.Dock = 1;
				this.buttonTop.Location = new Point(0, 0);
				this.buttonTop.Name = "buttonTop";
				this.buttonTop.Size = new Size(150, 23);
				this.buttonTop.TabIndex = 1;
				this.buttonTop.Click += this.buttonClick;
				this.panel2.Controls.Add(this.buttonFill);
				this.panel2.Controls.Add(this.buttonRight);
				this.panel2.Controls.Add(this.buttonLeft);
				this.panel2.Dock = 5;
				this.panel2.Location = new Point(0, 23);
				this.panel2.Size = new Size(150, 46);
				this.panel2.TabIndex = 2;
				this.panel2.TabStop = false;
				this.buttonLeft.Appearance = 1;
				this.buttonLeft.Dock = 3;
				this.buttonLeft.Location = new Point(0, 0);
				this.buttonLeft.Size = new Size(24, 46);
				this.buttonLeft.TabIndex = 2;
				this.buttonLeft.Click += this.buttonClick;
				this.buttonRight.Appearance = 1;
				this.buttonRight.Dock = 4;
				this.buttonRight.Location = new Point(126, 0);
				this.buttonRight.Size = new Size(24, 46);
				this.buttonRight.TabIndex = 4;
				this.buttonRight.Click += this.buttonClick;
				this.buttonFill.Appearance = 1;
				this.buttonFill.Dock = 5;
				this.buttonFill.Location = new Point(24, 0);
				this.buttonFill.Size = new Size(102, 46);
				this.buttonFill.TabIndex = 3;
				this.buttonFill.Click += this.buttonClick;
				base.Controls.Add(this.panel1);
				base.Controls.Add(this.buttonNone);
				base.Size = new Size(150, 115);
				this.panel1.ResumeLayout(false);
				this.panel2.ResumeLayout(false);
				base.ResumeLayout(false);
				this.editorService = editorService;
				this.dockStyle = 0;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00004404 File Offset: 0x00002604
			private void buttonClick(object sender, EventArgs e)
			{
				if (sender == this.buttonNone)
				{
					this.dockStyle = 0;
				}
				else if (sender == this.buttonFill)
				{
					this.dockStyle = 5;
				}
				else if (sender == this.buttonLeft)
				{
					this.dockStyle = 3;
				}
				else if (sender == this.buttonRight)
				{
					this.dockStyle = 4;
				}
				else if (sender == this.buttonTop)
				{
					this.dockStyle = 1;
				}
				else if (sender == this.buttonBottom)
				{
					this.dockStyle = 2;
				}
				this.editorService.CloseDropDown();
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000108 RID: 264 RVA: 0x00004486 File Offset: 0x00002686
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00004490 File Offset: 0x00002690
			public DockStyle DockStyle
			{
				get
				{
					return this.dockStyle;
				}
				set
				{
					this.dockStyle = value;
					this.buttonNone.Checked = false;
					this.buttonBottom.Checked = false;
					this.buttonTop.Checked = false;
					this.buttonLeft.Checked = false;
					this.buttonRight.Checked = false;
					this.buttonFill.Checked = false;
					switch (this.DockStyle)
					{
					case 0:
						this.buttonNone.CheckState = 1;
						return;
					case 1:
						this.buttonTop.CheckState = 1;
						return;
					case 2:
						this.buttonBottom.CheckState = 1;
						return;
					case 3:
						this.buttonLeft.CheckState = 1;
						return;
					case 4:
						this.buttonRight.CheckState = 1;
						return;
					case 5:
						this.buttonFill.CheckState = 1;
						return;
					default:
						return;
					}
				}
			}

			// Token: 0x0400002D RID: 45
			private CheckBox buttonNone;

			// Token: 0x0400002E RID: 46
			private Panel panel1;

			// Token: 0x0400002F RID: 47
			private CheckBox buttonBottom;

			// Token: 0x04000030 RID: 48
			private CheckBox buttonTop;

			// Token: 0x04000031 RID: 49
			private Panel panel2;

			// Token: 0x04000032 RID: 50
			private CheckBox buttonLeft;

			// Token: 0x04000033 RID: 51
			private CheckBox buttonRight;

			// Token: 0x04000034 RID: 52
			private CheckBox buttonFill;

			// Token: 0x04000035 RID: 53
			private IWindowsFormsEditorService editorService;

			// Token: 0x04000036 RID: 54
			private DockStyle dockStyle;
		}
	}
}
