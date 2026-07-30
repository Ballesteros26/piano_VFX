using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a user interface for selecting a file name.</summary>
	// Token: 0x0200001E RID: 30
	public class FileNameEditor : UITypeEditor
	{
		/// <summary>Edits the specified object using the editor style provided by the <see cref="M:System.Windows.Forms.Design.FileNameEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000137 RID: 311 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editing style used by the <see cref="M:System.Windows.Forms.Design.FileNameEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values indicating the provided editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000138 RID: 312 RVA: 0x00004FAC File Offset: 0x000031AC
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>Initializes the open file dialog when it is created.</summary>
		/// <param name="openFileDialog">The <see cref="T:System.Windows.Forms.OpenFileDialog" /> to use to select a file name. </param>
		// Token: 0x06000139 RID: 313 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void InitializeDialog(OpenFileDialog openFileDialog)
		{
			throw new NotImplementedException();
		}
	}
}
