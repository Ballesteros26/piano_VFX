using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting and editing an expressions binding collection at design time.</summary>
	// Token: 0x0200007C RID: 124
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ExpressionsCollectionEditor : UITypeEditor
	{
		/// <summary>Edits the value of the specified object with the specified service provider and context.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> object containing the selected expressions; otherwise, if no expressions are selected, the <paramref name="value" /> object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information such as the associated control.</param>
		/// <param name="provider">A service provider object through which editing services can be obtained.</param>
		/// <param name="value">An instance of the object being edited.</param>
		// Token: 0x060003FC RID: 1020 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the editing style that is associated with this editor for the specified context.</summary>
		/// <returns>An <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the editing style for the provided user interface.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
		// Token: 0x060003FD RID: 1021 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
