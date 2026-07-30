using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a user interface for configuring an <see cref="P:System.Windows.Forms.Control.Anchor" /> property.</summary>
	// Token: 0x02000002 RID: 2
	public sealed class AnchorEditor : UITypeEditor
	{
		/// <summary>Edits the value of the specified object using the specified service provider and context.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService windowsFormsEditorService = null;
			if (provider != null)
			{
				windowsFormsEditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
			}
			if (windowsFormsEditorService != null)
			{
				AnchorSelector anchorSelector = new AnchorSelector(windowsFormsEditorService, (AnchorStyles)value);
				windowsFormsEditorService.DropDownControl(anchorSelector);
				value = anchorSelector.AnchorStyles;
			}
			return value;
		}

		/// <summary>Gets the editor style used by the <see cref="M:System.Windows.Forms.Design.AnchorEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values indicating the provided editing style. If the method is not supported, this method will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000003 RID: 3 RVA: 0x000020A5 File Offset: 0x000002A5
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
	}
}
