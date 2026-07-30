using System;
using System.Drawing.Design;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a user interface for editing binary data.</summary>
	// Token: 0x020000F3 RID: 243
	public sealed class BinaryEditor : UITypeEditor
	{
		/// <summary>Edits the value of the specified object using the specified service provider and context.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">The object to edit the value of. </param>
		// Token: 0x060006E2 RID: 1762 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editor style used by the <see cref="M:System.ComponentModel.Design.BinaryEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>An enum value indicating the provided editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x060006E3 RID: 1763 RVA: 0x00004FAC File Offset: 0x000031AC
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
