using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000039 RID: 57
	internal class SplitContainerDesigner : ParentControlDesigner
	{
		// Token: 0x060001EB RID: 491 RVA: 0x000074AC File Offset: 0x000056AC
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			SplitContainer splitContainer = (SplitContainer)component;
			base.EnableDesignMode(splitContainer.Panel1, "Panel1");
			base.EnableDesignMode(splitContainer.Panel2, "Panel2");
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000074EB File Offset: 0x000056EB
		public override ControlDesigner InternalControlDesigner(int internalControlIndex)
		{
			if (internalControlIndex == 0)
			{
				return this.GetDesigner(((SplitContainer)this.Control).Panel1);
			}
			if (internalControlIndex != 1)
			{
				return null;
			}
			return this.GetDesigner(((SplitContainer)this.Control).Panel2);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007528 File Offset: 0x00005728
		private ControlDesigner GetDesigner(IComponent component)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				return designerHost.GetDesigner(component) as ControlDesigner;
			}
			return null;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00004FAC File Offset: 0x000031AC
		public override int NumberOfInternalControlDesigners()
		{
			return 2;
		}
	}
}
