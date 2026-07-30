using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>A data-bound list control that allows custom layout by repeating a specified template for each item displayed in the list.</summary>
	// Token: 0x020003FE RID: 1022
	[Designer("System.Web.UI.Design.WebControls.RepeaterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("ItemCommand")]
	[PersistChildren(false)]
	[DefaultProperty("DataSource")]
	[ParseChildren(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Repeater : Control, INamingContainer
	{
		// Token: 0x06002D31 RID: 11569 RVA: 0x000782E3 File Offset: 0x000764E3
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ViewState["Items"] != null)
			{
				this.CreateControlHierarchy(false);
			}
		}

		/// <summary>Raises the DataBinding event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D32 RID: 11570 RVA: 0x00078309 File Offset: 0x00076509
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(EventArgs.Empty);
			this.Controls.Clear();
			base.ClearChildViewState();
			this.TrackViewState();
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0007833C File Offset: 0x0007653C
		private void DoItem(int i, ListItemType t, object d, bool databind)
		{
			RepeaterItem repeaterItem = this.CreateItem(i, t);
			if (t == ListItemType.Item || t == ListItemType.AlternatingItem)
			{
				this.items.Add(repeaterItem);
			}
			repeaterItem.DataItem = d;
			RepeaterItemEventArgs repeaterItemEventArgs = new RepeaterItemEventArgs(repeaterItem);
			this.InitializeItem(repeaterItem);
			this.Controls.Add(repeaterItem);
			this.OnItemCreated(repeaterItemEventArgs);
			if (databind)
			{
				repeaterItem.DataBind();
				this.OnItemDataBound(repeaterItemEventArgs);
			}
		}

		/// <summary>Creates a control hierarchy, with or without the specified data source.</summary>
		/// <param name="useDataSource">Indicates whether to use the specified data source. </param>
		// Token: 0x06002D34 RID: 11572 RVA: 0x000783A0 File Offset: 0x000765A0
		protected virtual void CreateControlHierarchy(bool useDataSource)
		{
			this.items = new ArrayList();
			this.itemscol = null;
			IEnumerable enumerable;
			if (useDataSource)
			{
				enumerable = this.GetData();
			}
			else
			{
				enumerable = new object[(int)this.ViewState["Items"]];
			}
			if (enumerable == null)
			{
				return;
			}
			if (this.HeaderTemplate != null)
			{
				this.DoItem(-1, ListItemType.Header, null, useDataSource);
			}
			int num = 0;
			foreach (object obj in enumerable)
			{
				if (num != 0 && this.SeparatorTemplate != null)
				{
					this.DoItem(num - 1, ListItemType.Separator, null, useDataSource);
				}
				this.DoItem(num, (num % 2 == 0) ? ListItemType.Item : ListItemType.AlternatingItem, obj, useDataSource);
				num++;
			}
			if (this.FooterTemplate != null)
			{
				this.DoItem(-1, ListItemType.Footer, null, useDataSource);
			}
			this.ViewState["Items"] = num;
		}

		/// <summary>Binds the <see cref="T:System.Web.UI.WebControls.Repeater" /> control and all its child controls to the specified data source.</summary>
		// Token: 0x06002D35 RID: 11573 RVA: 0x00078494 File Offset: 0x00076694
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.RequiresDataBinding = false;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object with the specified item type and location within the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>The new <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object.</returns>
		/// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.Repeater" /> control to place the created item. </param>
		/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" /> that represents the specified type of the <see cref="T:System.Web.UI.WebControls.Repeater" /> item to create. </param>
		// Token: 0x06002D36 RID: 11574 RVA: 0x000784A8 File Offset: 0x000766A8
		protected virtual RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new RepeaterItem(itemIndex, itemType);
		}

		/// <summary>Populates iteratively the specified <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> with a sub-hierarchy of child controls.</summary>
		/// <param name="item">The control to be initialized from an inline template. </param>
		// Token: 0x06002D37 RID: 11575 RVA: 0x000784B4 File Offset: 0x000766B4
		protected virtual void InitializeItem(RepeaterItem item)
		{
			ITemplate template = null;
			switch (item.ItemType)
			{
			case ListItemType.Header:
				template = this.HeaderTemplate;
				break;
			case ListItemType.Footer:
				template = this.FooterTemplate;
				break;
			case ListItemType.Item:
				template = this.ItemTemplate;
				break;
			case ListItemType.AlternatingItem:
				template = this.AlternatingItemTemplate;
				if (template == null)
				{
					template = this.ItemTemplate;
				}
				break;
			case ListItemType.Separator:
				template = this.SeparatorTemplate;
				break;
			}
			if (template != null)
			{
				template.InstantiateIn(item);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event if the <paramref name="EventArgs" /> parameter is an instance of <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" />.</summary>
		/// <returns>true if the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> was raised, otherwise false.</returns>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D38 RID: 11576 RVA: 0x00078530 File Offset: 0x00076730
		protected override bool OnBubbleEvent(object sender, EventArgs e)
		{
			RepeaterCommandEventArgs repeaterCommandEventArgs = e as RepeaterCommandEventArgs;
			if (repeaterCommandEventArgs != null)
			{
				this.OnItemCommand(repeaterCommandEventArgs);
				return true;
			}
			return false;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</returns>
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x00047ACE File Offset: 0x00045CCE
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects. The default is an empty <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x00078551 File Offset: 0x00076751
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public virtual RepeaterItemCollection Items
		{
			get
			{
				if (this.itemscol == null)
				{
					if (this.items == null)
					{
						this.items = new ArrayList();
					}
					this.itemscol = new RepeaterItemCollection(this.items);
				}
				return this.itemscol;
			}
		}

		/// <summary>Gets or sets the specific table in the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> to bind to the control.</summary>
		/// <returns>A string that specifies a table in the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" />.</returns>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x00078585 File Offset: 0x00076785
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x0007859C File Offset: 0x0007679C
		[WebCategory("Data")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", "");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DataMember");
				}
				else
				{
					this.ViewState["DataMember"] = value;
				}
				if (!this.Initialized)
				{
					this.OnDataPropertyChanged();
				}
			}
		}

		/// <summary>Gets or sets the data source that provides data for populating the list.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> object that contains a collection of values used to supply data to this control. The default value is null.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> object specified is not a supported source of data for the <see cref="T:System.Web.UI.WebControls.Repeater" /> control. </exception>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property. </exception>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000785D2 File Offset: 0x000767D2
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x000785DC File Offset: 0x000767DC
		[Bindable(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null && !(value is IListSource) && !(value is IEnumerable))
				{
					throw new ArgumentException(string.Format("An invalid data source is being used for {0}. A valid data source must implement either IListSource or IEnumerable", this.ID));
				}
				this.dataSource = value;
				if (!this.Initialized)
				{
					this.OnDataPropertyChanged();
					return;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control should use to retrieve its data source.</summary>
		/// <returns>The ID property of the data source control.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved for one of the following reasons:A value is specified for both the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> and <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> properties.The data source specified by the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property cannot be found on the page.The data source specified by the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property does not implement <see cref="T:System.Web.UI.IDataSource" />.</exception>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000442A9 File Offset: 0x000424A9
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x00078628 File Offset: 0x00076828
		[DefaultValue("")]
		[IDReferenceProperty(typeof(DataSourceControl))]
		public virtual string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", "");
			}
			set
			{
				if (this.dataSource != null)
				{
					throw new HttpException("Only one of DataSource and DataSourceID can be specified.");
				}
				this.ViewState["DataSourceID"] = value;
				if (!this.Initialized)
				{
					this.OnDataPropertyChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether themes are applied to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false. </returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		// (set) Token: 0x06002D42 RID: 11586 RVA: 0x00070DEC File Offset: 0x0006EFEC
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Gets or sets the object implementing <see cref="T:System.Web.UI.ITemplate" /> that defines how alternating items in the control are displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how alternating items are displayed. The default value is null.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x0007865C File Offset: 0x0007685C
		// (set) Token: 0x06002D44 RID: 11588 RVA: 0x00078664 File Offset: 0x00076864
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("")]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alt_itm_tmpl;
			}
			set
			{
				this.alt_itm_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how the footer section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how the footer section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed. The default value is null.</returns>
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x0007866D File Offset: 0x0007686D
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x00078675 File Offset: 0x00076875
		[TemplateContainer(typeof(RepeaterItem))]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footer_tmpl;
			}
			set
			{
				this.footer_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how the header section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how the header section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed. The default value is null.</returns>
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x0007867E File Offset: 0x0007687E
		// (set) Token: 0x06002D48 RID: 11592 RVA: 0x00078686 File Offset: 0x00076886
		[WebSysDescription("")]
		[TemplateContainer(typeof(RepeaterItem))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.header_tmpl;
			}
			set
			{
				this.header_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how items in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control are displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how items in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control are displayed. The default value is null.</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x0007868F File Offset: 0x0007688F
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x00078697 File Offset: 0x00076897
		[TemplateContainer(typeof(RepeaterItem))]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.item_tmpl;
			}
			set
			{
				this.item_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> interface that defines how the separator between items is displayed.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that defines how the separator between items is displayed. The default is null.</returns>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000786A0 File Offset: 0x000768A0
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x000786A8 File Offset: 0x000768A8
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[TemplateContainer(typeof(RepeaterItem))]
		public virtual ITemplate SeparatorTemplate
		{
			get
			{
				return this.separator_tmpl;
			}
			set
			{
				this.separator_tmpl = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D4D RID: 11597 RVA: 0x000786B4 File Offset: 0x000768B4
		protected virtual void OnItemCommand(RepeaterCommandEventArgs e)
		{
			RepeaterCommandEventHandler repeaterCommandEventHandler = (RepeaterCommandEventHandler)base.Events[Repeater.ItemCommandEvent];
			if (repeaterCommandEventHandler != null)
			{
				repeaterCommandEventHandler(this, e);
			}
		}

		/// <summary>Occurs when a button is clicked in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06002D4E RID: 11598 RVA: 0x000786E2 File Offset: 0x000768E2
		// (remove) Token: 0x06002D4F RID: 11599 RVA: 0x000786F5 File Offset: 0x000768F5
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event RepeaterCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(Repeater.ItemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.ItemCommandEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCreated" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D50 RID: 11600 RVA: 0x00078708 File Offset: 0x00076908
		protected virtual void OnItemCreated(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.ItemCreatedEvent];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		/// <summary>Occurs when an item is created in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06002D51 RID: 11601 RVA: 0x00078736 File Offset: 0x00076936
		// (remove) Token: 0x06002D52 RID: 11602 RVA: 0x00078749 File Offset: 0x00076949
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public event RepeaterItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(Repeater.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.ItemCreatedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemDataBound" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D53 RID: 11603 RVA: 0x0007875C File Offset: 0x0007695C
		protected virtual void OnItemDataBound(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.ItemDataBoundEvent];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		/// <summary>Occurs after an item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is data-bound but before it is rendered on the page.</summary>
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06002D54 RID: 11604 RVA: 0x0007878A File Offset: 0x0007698A
		// (remove) Token: 0x06002D55 RID: 11605 RVA: 0x0007879D File Offset: 0x0007699D
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public event RepeaterItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(Repeater.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.ItemDataBoundEvent, value);
			}
		}

		/// <summary>Returns a value indicating whether the control has been initialized.</summary>
		/// <returns>true, if the control has been initialized, otherwise, false.</returns>
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000787B0 File Offset: 0x000769B0
		protected bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property is set. </summary>
		/// <returns>true if the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property is set to a value other than an empty string (""); otherwise, false. </returns>
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000787B8 File Offset: 0x000769B8
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length != 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Repeater" /> control needs to bind to its specified data source.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Repeater" /> control needs to bind to a data source; otherwise, false.</returns>
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000787C8 File Offset: 0x000769C8
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x000787D0 File Offset: 0x000769D0
		protected bool RequiresDataBinding
		{
			get
			{
				return this.requiresDataBinding;
			}
			set
			{
				this.requiresDataBinding = value;
				if (value && this.preRendered && this.IsBoundUsingDataSourceID && this.Page != null && !this.Page.IsCallback)
				{
					this.EnsureDataBound();
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control uses when retrieving data from a data source control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used to retrieve data. The default is the <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value. </returns>
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x00078807 File Offset: 0x00076A07
		protected DataSourceSelectArguments SelectArguments
		{
			get
			{
				if (this.selectArguments == null)
				{
					this.selectArguments = this.CreateDataSourceSelectArguments();
				}
				return this.selectArguments;
			}
		}

		/// <summary>Returns the <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value. </summary>
		/// <returns>The <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value.</returns>
		// Token: 0x06002D5B RID: 11611 RVA: 0x00047DAB File Offset: 0x00045FAB
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		/// <summary>Verifies that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.Repeater.DataBind" /> method.</summary>
		// Token: 0x06002D5C RID: 11612 RVA: 0x00078823 File Offset: 0x00076A23
		protected void EnsureDataBound()
		{
			if (this.IsBoundUsingDataSourceID && this.RequiresDataBinding)
			{
				this.DataBind();
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x0007883B File Offset: 0x00076A3B
		private void SelectCallback(IEnumerable data)
		{
			this.data = data;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" /> interface from the data source.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> that represents the data from the data source.</returns>
		// Token: 0x06002D5E RID: 11614 RVA: 0x00078844 File Offset: 0x00076A44
		protected virtual IEnumerable GetData()
		{
			IEnumerable enumerable;
			if (this.IsBoundUsingDataSourceID)
			{
				if (this.DataSourceID.Length == 0)
				{
					return null;
				}
				if (this.boundDataSource == null)
				{
					return null;
				}
				this.boundDataSource.GetView(string.Empty).Select(this.SelectArguments, new DataSourceViewSelectCallback(this.SelectCallback));
				enumerable = this.data;
				this.data = null;
			}
			else
			{
				enumerable = DataSourceResolver.ResolveDataSource(this.DataSource, this.DataMember);
			}
			return enumerable;
		}

		/// <summary>Determines whether data binding is required.</summary>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="M:System.Web.UI.WebControls.Repeater.OnDataPropertyChanged" /> is called during the data-binding phase of the control.</exception>
		// Token: 0x06002D5F RID: 11615 RVA: 0x000788BC File Offset: 0x00076ABC
		protected virtual void OnDataPropertyChanged()
		{
			if (this.Initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.Repeater.RequiresDataBinding" /> property to true.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D60 RID: 11616 RVA: 0x000788CD File Offset: 0x00076ACD
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D61 RID: 11617 RVA: 0x000788D8 File Offset: 0x00076AD8
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null)
			{
				page.PreLoad += this.OnPagePreLoad;
				if (!base.IsViewStateEnabled && page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x0007891F File Offset: 0x00076B1F
		private void OnPagePreLoad(object sender, EventArgs e)
		{
			this.Initialize();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event and performs other initialization. </summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D63 RID: 11619 RVA: 0x00078927 File Offset: 0x00076B27
		protected internal override void OnLoad(EventArgs e)
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
			base.OnLoad(e);
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x00078940 File Offset: 0x00076B40
		private void Initialize()
		{
			Page page = this.Page;
			if (page != null && (!page.IsPostBack || (base.IsViewStateEnabled && this.ViewState["Items"] == null)))
			{
				this.RequiresDataBinding = true;
			}
			if (this.IsBoundUsingDataSourceID)
			{
				this.ConnectToDataSource();
			}
			this.initialized = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object contains the event data. </param>
		// Token: 0x06002D65 RID: 11621 RVA: 0x00078995 File Offset: 0x00076B95
		protected internal override void OnPreRender(EventArgs e)
		{
			this.preRendered = true;
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000789AC File Offset: 0x00076BAC
		private void ConnectToDataSource()
		{
			object obj = null;
			if (this.Parent != null)
			{
				obj = this.Parent.FindControl(this.DataSourceID);
			}
			if (obj == null || !(obj is IDataSource))
			{
				string text;
				if (obj == null)
				{
					text = "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.";
				}
				else
				{
					text = "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  '{1}' is not an IDataSource.";
				}
				throw new HttpException(string.Format(text, this.ID, this.DataSourceID));
			}
			this.boundDataSource = (IDataSource)obj;
			this.boundDataSource.GetView(string.Empty).DataSourceViewChanged += this.OnDataSourceViewChanged;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00078A37 File Offset: 0x00076C37
		// Note: this type is marked as 'beforefieldinit'.
		static Repeater()
		{
			Repeater.ItemCommandEvent = new object();
			Repeater.ItemCreatedEvent = new object();
			Repeater.ItemDataBoundEvent = new object();
		}

		/// <summary>Gets a value that indicates whether data binding is automatic.</summary>
		/// <returns>true if data binding is automatic; otherwise, false.</returns>
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x00078A58 File Offset: 0x00076C58
		protected bool IsDataBindingAutomatic
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>The name of the model type for strongly typed data binding.</summary>
		/// <returns>The name of the model type.</returns>
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ItemType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>The name of the method to call in order to read data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string SelectMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when data methods are being called.</summary>
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06002D6E RID: 11630 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06002D6F RID: 11631 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object is being created.</summary>
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06002D70 RID: 11632 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (remove) Token: 0x06002D71 RID: 11633 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public event CreatingModelDataSourceEventHandler CreatingModelDataSource
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.CreatingModelDataSource" /> event.</summary>
		/// <param name="e">An object that provides access to the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object that is being created. </param>
		// Token: 0x06002D72 RID: 11634 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OnCreatingModelDataSource(CreatingModelDataSourceEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B6A RID: 7018
		private object dataSource;

		// Token: 0x04001B6B RID: 7019
		private IDataSource boundDataSource;

		// Token: 0x04001B6C RID: 7020
		private bool initialized;

		// Token: 0x04001B6D RID: 7021
		private bool preRendered;

		// Token: 0x04001B6E RID: 7022
		private bool requiresDataBinding;

		// Token: 0x04001B6F RID: 7023
		private DataSourceSelectArguments selectArguments;

		// Token: 0x04001B70 RID: 7024
		private IEnumerable data;

		// Token: 0x04001B71 RID: 7025
		private RepeaterItemCollection itemscol;

		// Token: 0x04001B72 RID: 7026
		private ArrayList items;

		// Token: 0x04001B73 RID: 7027
		private ITemplate alt_itm_tmpl;

		// Token: 0x04001B74 RID: 7028
		private ITemplate footer_tmpl;

		// Token: 0x04001B75 RID: 7029
		private ITemplate header_tmpl;

		// Token: 0x04001B76 RID: 7030
		private ITemplate item_tmpl;

		// Token: 0x04001B77 RID: 7031
		private ITemplate separator_tmpl;
	}
}
