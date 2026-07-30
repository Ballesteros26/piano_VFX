using System;
using System.ComponentModel.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x0200001D RID: 29
	internal class TaskDialogDesigner : ComponentDesigner
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007B48 File Offset: 0x00005D48
		public override DesignerVerbCollection Verbs
		{
			get
			{
				DesignerVerbCollection designerVerbCollection = new DesignerVerbCollection();
				designerVerbCollection.Add(new DesignerVerb(Resources.Preview, new EventHandler(this.Preview)));
				return designerVerbCollection;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007B7E File Offset: 0x00005D7E
		private void Preview(object sender, EventArgs e)
		{
			((TaskDialog)base.Component).ShowDialog();
		}
	}
}
