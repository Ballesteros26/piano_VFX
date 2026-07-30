using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002B5 RID: 693
	internal class RelatedPropertyManager : PropertyManager
	{
		// Token: 0x06002E18 RID: 11800 RVA: 0x000B1BBC File Offset: 0x000AFDBC
		public RelatedPropertyManager(BindingManagerBase parent, string property_name)
		{
			this.parent = parent;
			this.property_name = property_name;
			if (parent.Position != -1)
			{
				base.SetDataSource(parent.Current);
			}
			parent.PositionChanged += new EventHandler(this.parent_PositionChanged);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000B1C08 File Offset: 0x000AFE08
		private void parent_PositionChanged(object sender, EventArgs args)
		{
			if (this.parent.Position == -1)
			{
				base.SetDataSource(null);
			}
			else
			{
				base.SetDataSource(this.parent.Current);
			}
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000B1C50 File Offset: 0x000AFE50
		public override PropertyDescriptorCollection GetItemProperties()
		{
			PropertyDescriptor propertyDescriptor = this.parent.GetItemProperties().Find(this.property_name, true);
			return TypeDescriptor.GetProperties(propertyDescriptor.GetValue(this.parent.Current));
		}

		// Token: 0x04001620 RID: 5664
		private BindingManagerBase parent;
	}
}
