using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Drawing.Design
{
	/// <summary>Provides a <see cref="T:System.Drawing.Design.UITypeEditor" /> for visually editing content alignment.</summary>
	// Token: 0x02000010 RID: 16
	public class ContentAlignmentEditor : UITypeEditor
	{
		/// <summary>Edits the given object value using the editor style provided by the <see cref="Overload:System.Drawing.Design.ContentAlignmentEditor.GetEditStyle" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000028 RID: 40 RVA: 0x00002EEC File Offset: 0x000010EC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider == null)
			{
				return value;
			}
			IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (windowsFormsEditorService == null)
			{
				return value;
			}
			ContentAlignmentEditor.AlignmentUI alignmentUI = new ContentAlignmentEditor.AlignmentUI(this, windowsFormsEditorService, value);
			windowsFormsEditorService.DropDownControl(alignmentUI);
			return alignmentUI.Value;
		}

		/// <summary>Gets the editing style of the <see cref="Overload:System.Drawing.Design.ContentAlignmentEditor.EditValue" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value indicating the provided editing style. If the method to retrieve the edit style is not supported, this will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000029 RID: 41 RVA: 0x00002458 File Offset: 0x00000658
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x02000011 RID: 17
		private class AlignmentUI : ListBox
		{
			// Token: 0x0600002A RID: 42 RVA: 0x00002F30 File Offset: 0x00001130
			public AlignmentUI(UITypeEditor host, IWindowsFormsEditorService service, object value)
			{
				this.service = service;
				this.value = value;
				base.Items.Add(ContentAlignment.TopLeft);
				base.Items.Add(ContentAlignment.TopCenter);
				base.Items.Add(ContentAlignment.TopRight);
				base.Items.Add(ContentAlignment.MiddleLeft);
				base.Items.Add(ContentAlignment.MiddleCenter);
				base.Items.Add(ContentAlignment.MiddleRight);
				base.Items.Add(ContentAlignment.BottomLeft);
				base.Items.Add(ContentAlignment.BottomCenter);
				base.Items.Add(ContentAlignment.BottomRight);
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00003002 File Offset: 0x00001202
			protected override void OnClick(EventArgs e)
			{
				base.OnClick(e);
				this.value = base.SelectedItem;
				this.service.CloseDropDown();
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00003022 File Offset: 0x00001222
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04000034 RID: 52
			private object value;

			// Token: 0x04000035 RID: 53
			private IWindowsFormsEditorService service;
		}
	}
}
