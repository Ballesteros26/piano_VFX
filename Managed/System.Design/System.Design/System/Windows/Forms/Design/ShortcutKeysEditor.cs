using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides an editor for picking shortcut keys.</summary>
	// Token: 0x02000038 RID: 56
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ShortcutKeysEditor : UITypeEditor
	{
		/// <summary>Edits the given object value using the editor style provided by the <see cref="M:System.Windows.Forms.Design.ShortcutKeysEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>The new value of the <see cref="T:System.Object" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to edit.</param>
		// Token: 0x060001E8 RID: 488 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editor style used by the <see cref="Overload:System.Windows.Forms.Design.ShortcutKeysEditor.EditValue" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="Overload:System.Windows.Forms.Design.ShortcutKeysEditor.EditValue" /> method.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
		// Token: 0x060001E9 RID: 489 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
