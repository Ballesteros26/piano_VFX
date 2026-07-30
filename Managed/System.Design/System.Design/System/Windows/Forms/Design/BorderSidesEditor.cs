using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides an editor for setting the <see cref="P:System.Windows.Forms.ToolStripStatusLabel.BorderSides" /> property.</summary>
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class BorderSidesEditor : UITypeEditor
	{
		/// <summary>Edits the given object value using the editor style provided by <see cref="M:System.Windows.Forms.Design.BorderSidesEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
		/// <returns>The edited object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing information about the control or component.</param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> providing custom support to other objects.</param>
		/// <param name="value">The object value to edit.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the editing style of the EditValue method.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values. If the method is not supported, this method returns <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing information about the control or component.</param>
		// Token: 0x0600002C RID: 44 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
