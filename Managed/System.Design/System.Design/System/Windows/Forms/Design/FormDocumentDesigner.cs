using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000023 RID: 35
	internal class FormDocumentDesigner : DocumentDesigner
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000506D File Offset: 0x0000326D
		public override void Initialize(IComponent component)
		{
			Form form = component as Form;
			if (form == null)
			{
				throw new NotSupportedException("FormDocumentDesigner can be initialized only with Forms");
			}
			form.TopLevel = false;
			form.Visible = true;
			base.Initialize(component);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005097 File Offset: 0x00003297
		public override bool CanParent(Control control)
		{
			return !(control is Form) && base.CanParent(control);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000050AC File Offset: 0x000032AC
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
			case 161:
			case 163:
			case 164:
			case 166:
			case 167:
			case 169:
			{
				ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[] { base.Component });
					return;
				}
				return;
			}
			}
			base.WndProc(ref m);
		}
	}
}
