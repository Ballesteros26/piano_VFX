using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting an XML file using a standard <see cref="T:System.Windows.Forms.OpenFileDialog" /> box.</summary>
	// Token: 0x020000BE RID: 190
	public class XmlFileEditor : UITypeEditor
	{
		/// <summary>Edits the value of the specified object using the specified service provider and context.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this method should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000598 RID: 1432 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editing style associated with this editor, using the specified <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the provided editing style. If the method is not supported in the specified context, this will return the <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" /> identifier.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000599 RID: 1433 RVA: 0x00004FAC File Offset: 0x000031AC
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
