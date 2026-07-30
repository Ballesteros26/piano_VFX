using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the base class for types that define a list of items used to create a smart tag panel.</summary>
	// Token: 0x02000110 RID: 272
	public class DesignerActionList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionList" /> class.</summary>
		/// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
		// Token: 0x060007EE RID: 2030 RVA: 0x0000D70B File Offset: 0x0000B90B
		public DesignerActionList(IComponent component)
		{
			this.component = component;
			this.action_items = new DesignerActionItemCollection();
		}

		/// <summary>Gets or sets a value indicating whether the smart tag panel should automatically be displayed when it is created.</summary>
		/// <returns>true if the panel should be shown when the owning component is created; otherwise, false. The default is false.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0000D725 File Offset: 0x0000B925
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0000D72D File Offset: 0x0000B92D
		public virtual bool AutoShow
		{
			get
			{
				return this.auto_show;
			}
			set
			{
				this.auto_show = value;
			}
		}

		/// <summary>Gets the component related to <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</summary>
		/// <returns>A component related to <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0000D736 File Offset: 0x0000B936
		public IComponent Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Returns an object that represents a service provided by the component associated with the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />. This value is null if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
		/// <param name="serviceType">A service provided by the <see cref="T:System.ComponentModel.Component" />.</param>
		// Token: 0x060007F2 RID: 2034 RVA: 0x0000256A File Offset: 0x0000076A
		public object GetService(Type serviceType)
		{
			return null;
		}

		/// <summary>Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
		// Token: 0x060007F3 RID: 2035 RVA: 0x0000D73E File Offset: 0x0000B93E
		public virtual DesignerActionItemCollection GetSortedActionItems()
		{
			return this.action_items;
		}

		// Token: 0x040001B1 RID: 433
		private IComponent component;

		// Token: 0x040001B2 RID: 434
		private bool auto_show;

		// Token: 0x040001B3 RID: 435
		private DesignerActionItemCollection action_items;
	}
}
