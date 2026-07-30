using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	/// <summary>Displays a dialog for editing multi-line strings in design mode.</summary>
	// Token: 0x02000131 RID: 305
	public sealed class MultilineStringEditor : UITypeEditor
	{
		/// <summary>Edits the specified object value using the edit style provided by <see cref="M:System.Drawing.Design.ImageEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this method should return the same object passed to it.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services can be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000903 RID: 2307 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				this.editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (this.editorService != null)
				{
					if (value == null)
					{
						value = string.Empty;
					}
					else if (!(value is string))
					{
						return value;
					}
					this.control.Text = (string)value;
					this.editorService.DropDownControl(this.control);
					return this.control.Text;
				}
			}
			return base.EditValue(context, provider, value);
		}

		/// <summary>Gets the editing style of the <see cref="M:System.Drawing.Design.ImageEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the supported editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000904 RID: 2308 RVA: 0x000020A5 File Offset: 0x000002A5
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		/// <summary>Gets a value indicating whether this editor supports painting a representation of an object's value.</summary>
		/// <returns>false, indicating that this <see cref="T:System.Drawing.Design.UITypeEditor" /> does not display a visual representation in the Properties Window.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000905 RID: 2309 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x04000203 RID: 515
		private IWindowsFormsEditorService editorService;

		// Token: 0x04000204 RID: 516
		private MultilineStringEditor.EditorControl control = new MultilineStringEditor.EditorControl();

		// Token: 0x02000132 RID: 306
		private class EditorControl : TextBox
		{
			// Token: 0x06000906 RID: 2310 RVA: 0x0000F870 File Offset: 0x0000DA70
			public EditorControl()
			{
				this.Multiline = true;
				base.AcceptsReturn = true;
				base.Height = 135;
				base.Width = 280;
				base.ScrollBars = 3;
				base.WordWrap = false;
				base.BorderStyle = 1;
			}
		}
	}
}
