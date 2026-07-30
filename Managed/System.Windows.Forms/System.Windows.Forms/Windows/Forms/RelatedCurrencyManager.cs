using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002B3 RID: 691
	[DefaultMember("Item")]
	internal class RelatedCurrencyManager : CurrencyManager
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x000B1B44 File Offset: 0x000AFD44
		public RelatedCurrencyManager(BindingManagerBase parent, PropertyDescriptor prop_desc)
			: base(prop_desc.GetValue(parent.Current))
		{
			this.parent = parent;
			this.prop_desc = prop_desc;
			parent.PositionChanged += new EventHandler(this.parent_PositionChanged);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000B1B84 File Offset: 0x000AFD84
		private void parent_PositionChanged(object sender, EventArgs args)
		{
			base.SetDataSource(this.prop_desc.GetValue(this.parent.Current));
		}

		// Token: 0x0400161D RID: 5661
		private BindingManagerBase parent;

		// Token: 0x0400161E RID: 5662
		private PropertyDescriptor prop_desc;
	}
}
