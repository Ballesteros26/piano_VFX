using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Provides a user interface to select and configure a <see cref="T:System.Drawing.Font" /> object.</summary>
	// Token: 0x02000014 RID: 20
	public class FontEditor : UITypeEditor
	{
		/// <summary>Edits the value of the specified object using the editor style indicated by <see cref="M:System.Drawing.Design.FontEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object that was passed to it.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services. </param>
		/// <param name="value">The object to edit. </param>
		// Token: 0x06000035 RID: 53 RVA: 0x00003114 File Offset: 0x00001314
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.fontEdit = new FontDialog();
			if (value is Font)
			{
				this.fontEdit.Font = (Font)value;
			}
			else
			{
				this.fontEdit.Font = new Font(FontFamily.GenericSansSerif, 12f);
			}
			this.fontEdit.FontMustExist = true;
			if (this.fontEdit.ShowDialog() == 1)
			{
				return this.fontEdit.Font;
			}
			return value;
		}

		/// <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.FontEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by <see cref="M:System.Drawing.Design.FontEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000036 RID: 54 RVA: 0x00003188 File Offset: 0x00001388
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x04000038 RID: 56
		private FontDialog fontEdit;
	}
}
