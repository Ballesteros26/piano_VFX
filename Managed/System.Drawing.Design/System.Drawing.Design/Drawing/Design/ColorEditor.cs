using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Drawing.Design
{
	/// <summary>Provides a <see cref="T:System.Drawing.Design.UITypeEditor" /> for visually picking a color.</summary>
	// Token: 0x0200000B RID: 11
	public class ColorEditor : UITypeEditor
	{
		/// <summary>Edits the given object value using the editor style provided by the <see cref="M:System.Drawing.Design.ColorEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000011 RID: 17 RVA: 0x000020E8 File Offset: 0x000002E8
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				this.editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (this.editorService != null)
				{
					if (this.editor_control == null)
					{
						this.editor_control = this.GetEditorControl(value);
					}
					this.editorService.DropDownControl(this.editor_control);
					if (this.color_chosen)
					{
						return this.selected_color;
					}
					return null;
				}
			}
			return base.EditValue(context, provider, value);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002164 File Offset: 0x00000364
		private Control GetEditorControl(object value)
		{
			TabControl tabControl = new TabControl();
			tabControl.Dock = 5;
			TabPage tabPage = new TabPage("Custom");
			TabPage tabPage2 = new TabPage("Web");
			TabPage tabPage3 = new TabPage("System");
			ColorEditor.ColorListBox colorListBox = new ColorEditor.ColorListBox();
			ColorEditor.ColorListBox colorListBox2 = new ColorEditor.ColorListBox();
			colorListBox.Dock = 5;
			colorListBox2.Dock = 5;
			tabPage2.Controls.Add(colorListBox);
			tabPage3.Controls.Add(colorListBox2);
			ColorEditor.SystemColorCompare systemColorCompare = new ColorEditor.SystemColorCompare();
			ArrayList arrayList = new ArrayList();
			PropertyInfo[] properties = typeof(SystemColors).GetProperties(BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < properties.Length; i++)
			{
				Color color = (Color)properties[i].GetValue(null, null);
				arrayList.Add(color);
			}
			arrayList.Sort(systemColorCompare);
			colorListBox2.Items.AddRange(arrayList.ToArray());
			colorListBox2.MouseUp += new MouseEventHandler(this.HandleMouseUp);
			colorListBox2.SelectedValueChanged += this.HandleChange;
			ColorEditor.WebColorCompare webColorCompare = new ColorEditor.WebColorCompare();
			arrayList = new ArrayList();
			foreach (object obj in Enum.GetValues(typeof(KnownColor)))
			{
				Color color2 = Color.FromKnownColor((KnownColor)obj);
				if (!color2.IsSystemColor)
				{
					arrayList.Add(color2);
				}
			}
			arrayList.Sort(webColorCompare);
			colorListBox.Items.AddRange(arrayList.ToArray());
			colorListBox.MouseUp += new MouseEventHandler(this.HandleMouseUp);
			colorListBox.SelectedValueChanged += this.HandleChange;
			ColorEditor.CustomColorPicker customColorPicker = new ColorEditor.CustomColorPicker();
			customColorPicker.Dock = 5;
			customColorPicker.ColorChanged += this.CustomColorPicked;
			tabPage.Controls.Add(customColorPicker);
			tabControl.TabPages.Add(tabPage);
			tabControl.TabPages.Add(tabPage2);
			tabControl.TabPages.Add(tabPage3);
			if (value != null)
			{
				Color color3 = (Color)value;
				if (color3.IsSystemColor)
				{
					colorListBox2.SelectedValue = color3;
					tabControl.SelectedTab = tabPage3;
				}
				else if (color3.IsKnownColor)
				{
					colorListBox.SelectedValue = color3;
					tabControl.SelectedTab = tabPage2;
				}
				this.selected_color = color3;
				this.color_chosen = true;
			}
			tabControl.Height = 216;
			return tabControl;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023EC File Offset: 0x000005EC
		private void HandleChange(object sender, EventArgs e)
		{
			this.selected_color = (Color)((ColorEditor.ColorListBox)sender).Items[((ColorEditor.ColorListBox)sender).SelectedIndex];
			this.color_chosen = true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000241B File Offset: 0x0000061B
		private void CustomColorPicked(object sender, EventArgs e)
		{
			this.selected_color = (Color)sender;
			this.color_chosen = true;
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002443 File Offset: 0x00000643
		private void HandleMouseUp(object sender, MouseEventArgs e)
		{
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		/// <summary>Gets the editing style of the Edit method. If the method is not supported, this will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</summary>
		/// <returns>An enum value indicating the provided editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000016 RID: 22 RVA: 0x00002458 File Offset: 0x00000658
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		/// <summary>Gets a value indicating if this editor supports the painting of a representation of an object's value.</summary>
		/// <returns>true if <see cref="Overload:System.Drawing.Design.ColorEditor.PaintValue" /> is implemented; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000017 RID: 23 RVA: 0x0000245B File Offset: 0x0000065B
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Paints a representative value of the given object to the provided canvas.</summary>
		/// <param name="e">What to paint and where to paint it. </param>
		// Token: 0x06000018 RID: 24 RVA: 0x00002460 File Offset: 0x00000660
		public override void PaintValue(PaintValueEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (e.Value != null)
			{
				using (SolidBrush solidBrush = new SolidBrush((Color)e.Value))
				{
					graphics.FillRectangle(solidBrush, e.Bounds);
				}
			}
		}

		// Token: 0x0400002B RID: 43
		private IWindowsFormsEditorService editorService;

		// Token: 0x0400002C RID: 44
		private Color selected_color;

		// Token: 0x0400002D RID: 45
		private bool color_chosen;

		// Token: 0x0400002E RID: 46
		private Control editor_control;

		// Token: 0x0200000C RID: 12
		private class ColorListBox : ListBox
		{
			// Token: 0x06000019 RID: 25 RVA: 0x000024B8 File Offset: 0x000006B8
			public ColorListBox()
			{
				this.DrawMode = 1;
				base.Sorted = true;
				this.ItemHeight = 14;
				base.BorderStyle = 1;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x000024E0 File Offset: 0x000006E0
			protected override void OnDrawItem(DrawItemEventArgs e)
			{
				e.DrawBackground();
				Color color = (Color)base.Items[e.Index];
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(solidBrush, 2, e.Bounds.Top + 2, 21, 9);
				}
				e.Graphics.DrawRectangle(SystemPens.WindowText, 2, e.Bounds.Top + 2, 21, 9);
				e.Graphics.DrawString(color.Name, this.Font, SystemBrushes.WindowText, 26f, (float)e.Bounds.Top);
				if ((e.State & 1) != null)
				{
					e.DrawFocusRectangle();
				}
				base.OnDrawItem(e);
			}
		}

		// Token: 0x0200000D RID: 13
		private class SystemColorCompare : IComparer
		{
			// Token: 0x0600001B RID: 27 RVA: 0x000025BC File Offset: 0x000007BC
			public int Compare(object x, object y)
			{
				Color color = (Color)x;
				Color color2 = (Color)y;
				return string.Compare(color.Name, color2.Name);
			}
		}

		// Token: 0x0200000E RID: 14
		private class WebColorCompare : IComparer
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000025EC File Offset: 0x000007EC
			public int Compare(object x, object y)
			{
				Color color = (Color)x;
				Color color2 = (Color)y;
				return string.Compare(color.Name, color2.Name);
			}
		}

		// Token: 0x0200000F RID: 15
		private class CustomColorPicker : UserControl
		{
			// Token: 0x0600001F RID: 31 RVA: 0x0000261C File Offset: 0x0000081C
			public CustomColorPicker()
			{
				this.colors = new Color[8, 8];
				this.colors[0, 0] = Color.White;
				this.colors[1, 0] = Color.FromArgb(224, 224, 224);
				this.colors[2, 0] = Color.Silver;
				this.colors[3, 0] = Color.Gray;
				this.colors[4, 0] = Color.FromArgb(64, 64, 64);
				this.colors[5, 0] = Color.Black;
				this.colors[6, 0] = Color.White;
				this.colors[7, 0] = Color.White;
				this.colors[0, 1] = Color.FromArgb(255, 192, 192);
				this.colors[1, 1] = Color.FromArgb(255, 128, 128);
				this.colors[2, 1] = Color.Red;
				this.colors[3, 1] = Color.FromArgb(192, 0, 0);
				this.colors[4, 1] = Color.Maroon;
				this.colors[5, 1] = Color.FromArgb(64, 0, 0);
				this.colors[6, 1] = Color.White;
				this.colors[7, 1] = Color.White;
				this.colors[0, 2] = Color.FromArgb(255, 224, 192);
				this.colors[1, 2] = Color.FromArgb(255, 192, 128);
				this.colors[2, 2] = Color.FromArgb(255, 128, 0);
				this.colors[3, 2] = Color.FromArgb(192, 64, 0);
				this.colors[4, 2] = Color.FromArgb(128, 64, 0);
				this.colors[5, 2] = Color.FromArgb(128, 64, 64);
				this.colors[6, 2] = Color.White;
				this.colors[7, 2] = Color.White;
				this.colors[0, 3] = Color.FromArgb(255, 255, 192);
				this.colors[1, 3] = Color.FromArgb(255, 255, 128);
				this.colors[2, 3] = Color.Yellow;
				this.colors[3, 3] = Color.FromArgb(192, 192, 0);
				this.colors[4, 3] = Color.Olive;
				this.colors[5, 3] = Color.FromArgb(64, 64, 0);
				this.colors[6, 3] = Color.White;
				this.colors[7, 3] = Color.White;
				this.colors[0, 4] = Color.FromArgb(192, 255, 192);
				this.colors[1, 4] = Color.FromArgb(128, 255, 128);
				this.colors[2, 4] = Color.Lime;
				this.colors[3, 4] = Color.FromArgb(0, 192, 0);
				this.colors[4, 4] = Color.Green;
				this.colors[5, 4] = Color.FromArgb(0, 64, 0);
				this.colors[6, 4] = Color.White;
				this.colors[7, 4] = Color.White;
				this.colors[0, 5] = Color.FromArgb(192, 255, 255);
				this.colors[1, 5] = Color.FromArgb(128, 255, 255);
				this.colors[2, 5] = Color.Cyan;
				this.colors[3, 5] = Color.FromArgb(0, 192, 192);
				this.colors[4, 5] = Color.Teal;
				this.colors[5, 5] = Color.FromArgb(0, 64, 64);
				this.colors[6, 5] = Color.White;
				this.colors[7, 5] = Color.White;
				this.colors[0, 6] = Color.FromArgb(192, 192, 255);
				this.colors[1, 6] = Color.FromArgb(128, 128, 255);
				this.colors[2, 6] = Color.Blue;
				this.colors[3, 6] = Color.FromArgb(0, 0, 192);
				this.colors[4, 6] = Color.Navy;
				this.colors[5, 6] = Color.FromArgb(0, 0, 64);
				this.colors[6, 6] = Color.White;
				this.colors[7, 6] = Color.White;
				this.colors[0, 7] = Color.FromArgb(255, 192, 255);
				this.colors[1, 7] = Color.FromArgb(255, 128, 255);
				this.colors[2, 7] = Color.Fuchsia;
				this.colors[3, 7] = Color.FromArgb(192, 0, 192);
				this.colors[4, 7] = Color.Purple;
				this.colors[5, 7] = Color.FromArgb(64, 0, 64);
				this.colors[6, 7] = Color.White;
				this.colors[7, 7] = Color.White;
			}

			// Token: 0x14000001 RID: 1
			// (add) Token: 0x06000020 RID: 32 RVA: 0x00002C18 File Offset: 0x00000E18
			// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002C50 File Offset: 0x00000E50
			public event EventHandler ColorChanged;

			// Token: 0x06000022 RID: 34 RVA: 0x00002C88 File Offset: 0x00000E88
			protected override void OnPaint(PaintEventArgs e)
			{
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						this.DrawRect(e.Graphics, this.colors[i, j], j * 24, i * 24);
					}
				}
				if (this.highlighting)
				{
					int num = this.x / 24;
					int num2 = this.y / 24;
					ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(num * 24 - 2, num2 * 24 - 2, 24, 24));
				}
				base.OnPaint(e);
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00002D14 File Offset: 0x00000F14
			private void DrawRect(Graphics g, Color color, int x, int y)
			{
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					g.FillRectangle(solidBrush, x, y, 20, 20);
				}
				ControlPaint.DrawBorder3D(g, x, y, 20, 20);
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002D60 File Offset: 0x00000F60
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.X % 24 < 20 && e.Y % 24 < 20)
				{
					this.x = e.X;
					this.y = e.Y;
					this.highlighting = true;
					base.Invalidate();
				}
				base.OnMouseDown(e);
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00002DB4 File Offset: 0x00000FB4
			protected override void OnMouseUp(MouseEventArgs e)
			{
				if (this.highlighting && base.ClientRectangle.Contains(e.X, e.Y))
				{
					if (this.ColorChanged != null)
					{
						this.ColorChanged(this.colors[this.y / 24, this.x / 24], EventArgs.Empty);
					}
					this.highlighting = false;
				}
				base.OnMouseUp(e);
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002E30 File Offset: 0x00001030
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.highlighting)
				{
					int num = this.x;
					int num2 = this.y;
					this.x = e.X;
					this.y = e.Y;
					if ((num / 24 != this.x / 24 || num2 / 24 != this.y / 24) && this.x / 24 < 8 && this.y / 24 < 8)
					{
						Region region = new Region();
						region.Union(new Rectangle(num - 2, num2 - 2, 24, 24));
						region.Union(new Rectangle(this.x - 2, this.y - 2, 24, 24));
						base.Invalidate(region);
					}
				}
				base.OnMouseMove(e);
			}

			// Token: 0x0400002F RID: 47
			private Color[,] colors;

			// Token: 0x04000030 RID: 48
			private bool highlighting;

			// Token: 0x04000031 RID: 49
			private int x;

			// Token: 0x04000032 RID: 50
			private int y;
		}
	}
}
