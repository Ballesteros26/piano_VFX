using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a user interface for editing regular expressions.</summary>
	// Token: 0x020000D9 RID: 217
	public class RegexTypeEditor : UITypeEditor
	{
		/// <summary>Edits the value of the given regular expression object using the given service provider and context.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this method returns the same object it received.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can provide additional context information.</param>
		/// <param name="provider">A service provider.</param>
		/// <param name="value">The regular expression object whose value is to be edited.</param>
		// Token: 0x06000650 RID: 1616 RVA: 0x0000234B File Offset: 0x0000054B
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editor style used by the <see cref="M:System.Web.UI.Design.WebControls.RegexTypeEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the editor style used by the method.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000651 RID: 1617 RVA: 0x0000234B File Offset: 0x0000054B
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
