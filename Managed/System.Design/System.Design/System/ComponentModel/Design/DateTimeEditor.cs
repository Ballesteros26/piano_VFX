using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	/// <summary>This date time editor is a <see cref="T:System.Drawing.Design.UITypeEditor" /> suitable for visually editing <see cref="T:System.DateTime" /> objects.</summary>
	// Token: 0x020000FF RID: 255
	public class DateTimeEditor : UITypeEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DateTimeEditor" /> class. </summary>
		// Token: 0x06000757 RID: 1879 RVA: 0x0000BF6D File Offset: 0x0000A16D
		public DateTimeEditor()
		{
			this.control.DateSelected += new DateRangeEventHandler(this.control_DateSelected);
		}

		/// <summary>Edits the specified object value using the editor style provided by GetEditorStyle. A service provider is provided so that any required editing services can be obtained.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x06000758 RID: 1880 RVA: 0x0000BF98 File Offset: 0x0000A198
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				this.editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (this.editorService != null)
				{
					if (!(value is DateTime))
					{
						return value;
					}
					this.editContent = (DateTime)value;
					if (this.editContent > this.control.MaxDate || this.editContent < this.control.MinDate)
					{
						this.control.SelectionStart = DateTime.Today;
					}
					else
					{
						this.control.SelectionStart = this.editContent;
					}
					this.editorService.DropDownControl(this.control);
					return this.editContent;
				}
			}
			return base.EditValue(context, provider, value);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000C066 File Offset: 0x0000A266
		private void control_DateSelected(object sender, DateRangeEventArgs e)
		{
			this.editContent = e.Start;
			this.editorService.CloseDropDown();
		}

		/// <summary>Retrieves the editing style of the <see cref="Overload:System.ComponentModel.Design.DateTimeEditor.EditValue" /> method. If the method is not supported, this will return None.</summary>
		/// <returns>An enum value indicating the provided editing style.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		// Token: 0x0600075A RID: 1882 RVA: 0x000020A5 File Offset: 0x000002A5
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x04000187 RID: 391
		private IWindowsFormsEditorService editorService;

		// Token: 0x04000188 RID: 392
		private DateTimeEditor.EditorControl control = new DateTimeEditor.EditorControl();

		// Token: 0x04000189 RID: 393
		private DateTime editContent;

		// Token: 0x02000100 RID: 256
		private class EditorControl : MonthCalendar
		{
			// Token: 0x0600075B RID: 1883 RVA: 0x0000C07F File Offset: 0x0000A27F
			public EditorControl()
			{
				base.MaxSelectionCount = 1;
			}
		}
	}
}
