using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a container that holds the contents of a templated menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	// Token: 0x020003D8 RID: 984
	public sealed class MenuItemTemplateContainer : Control, IDataItemContainer, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemTemplateContainer" /> class using the specified menu item index and menu item.</summary>
		/// <param name="itemIndex">The index of the menu item.</param>
		/// <param name="dataItem">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</param>
		// Token: 0x06002A5D RID: 10845 RVA: 0x0006F0C0 File Offset: 0x0006D2C0
		public MenuItemTemplateContainer(int itemIndex, MenuItem dataItem)
		{
			this.index = itemIndex;
			this.dataItem = dataItem;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0006F0D8 File Offset: 0x0006D2D8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs == null)
			{
				return false;
			}
			MenuEventArgs menuEventArgs = new MenuEventArgs((MenuItem)this.DataItem, source, commandEventArgs);
			base.RaiseBubbleEvent(this, menuEventArgs);
			return true;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0006F10D File Offset: 0x0006D30D
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>Gets or sets the menu item associated with the container.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item associated with the container.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x0006F116 File Offset: 0x0006D316
		// (set) Token: 0x06002A61 RID: 10849 RVA: 0x0006F11E File Offset: 0x0006D31E
		public object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		/// <summary>Gets the index of the menu item associated with the container.</summary>
		/// <returns>The index of the menu item associated with the container.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x0006F127 File Offset: 0x0006D327
		public int ItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the index value of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</summary>
		/// <returns>The index value of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</returns>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x0006F127 File Offset: 0x0006D327
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the index value of the menu item for the container.</summary>
		/// <returns>The index value of the menu item for the container.</returns>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x0006F127 File Offset: 0x0006D327
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04001ADB RID: 6875
		private object dataItem;

		// Token: 0x04001ADC RID: 6876
		private int index;
	}
}
