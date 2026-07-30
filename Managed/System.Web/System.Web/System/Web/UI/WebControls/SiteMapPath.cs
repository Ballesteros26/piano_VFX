using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a set of text or image hyperlinks that enable users to more easily navigate a Web site, while taking a minimal amount of page space.</summary>
	// Token: 0x0200040C RID: 1036
	[Designer("System.Web.UI.Design.WebControls.SiteMapPathDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class SiteMapPath : CompositeControl
	{
		/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> is created by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> and is associated with its corresponding <see cref="T:System.Web.SiteMapNode" />. This event is raised by the <see cref="M:System.Web.UI.WebControls.SiteMapPath.OnItemCreated(System.Web.UI.WebControls.SiteMapNodeItemEventArgs)" /> method.</summary>
		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06002DE9 RID: 11753 RVA: 0x0007954D File Offset: 0x0007774D
		// (remove) Token: 0x06002DEA RID: 11754 RVA: 0x00079560 File Offset: 0x00077760
		public event SiteMapNodeItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(SiteMapPath.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(SiteMapPath.ItemCreatedEvent, value);
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> has been bound to its underlying <see cref="T:System.Web.SiteMapNode" /> data by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" />. This event is raised by the <see cref="M:System.Web.UI.WebControls.SiteMapPath.OnItemDataBound(System.Web.UI.WebControls.SiteMapNodeItemEventArgs)" /> method.</summary>
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06002DEB RID: 11755 RVA: 0x00079573 File Offset: 0x00077773
		// (remove) Token: 0x06002DEC RID: 11756 RVA: 0x00079586 File Offset: 0x00077786
		public event SiteMapNodeItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(SiteMapPath.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(SiteMapPath.ItemDataBoundEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemCreated" /> event of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002DED RID: 11757 RVA: 0x0007959C File Offset: 0x0007779C
		protected virtual void OnItemCreated(SiteMapNodeItemEventArgs e)
		{
			if (base.Events != null)
			{
				SiteMapNodeItemEventHandler siteMapNodeItemEventHandler = (SiteMapNodeItemEventHandler)base.Events[SiteMapPath.ItemCreatedEvent];
				if (siteMapNodeItemEventHandler != null)
				{
					siteMapNodeItemEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemDataBound" /> event of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002DEE RID: 11758 RVA: 0x000795D4 File Offset: 0x000777D4
		protected virtual void OnItemDataBound(SiteMapNodeItemEventArgs e)
		{
			if (base.Events != null)
			{
				SiteMapNodeItemEventHandler siteMapNodeItemEventHandler = (SiteMapNodeItemEventHandler)base.Events[SiteMapPath.ItemDataBoundEvent];
				if (siteMapNodeItemEventHandler != null)
				{
					siteMapNodeItemEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets the style used for the display text for the current node.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings for the display text for the current node of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</returns>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x0007960A File Offset: 0x0007780A
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public Style CurrentNodeStyle
		{
			get
			{
				if (this.currentNodeStyle == null)
				{
					this.currentNodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.currentNodeStyle).TrackViewState();
					}
				}
				return this.currentNodeStyle;
			}
		}

		/// <summary>Gets or sets a control template to use for the node of a site navigation path that represents the currently displayed page.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for the navigation path node that represents the currently displayed page.</returns>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x00079638 File Offset: 0x00077838
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x00079640 File Offset: 0x00077840
		[DefaultValue(null)]
		[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate CurrentNodeTemplate
		{
			get
			{
				return this.currentNodeTemplate;
			}
			set
			{
				this.currentNodeTemplate = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets the style used for the display text for all nodes in the site navigation path.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings for the display text in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x0007964F File Offset: 0x0007784F
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style NodeStyle
		{
			get
			{
				if (this.nodeStyle == null)
				{
					this.nodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.nodeStyle).TrackViewState();
					}
				}
				return this.nodeStyle;
			}
		}

		/// <summary>Gets or sets a control template to use for all functional nodes of a site navigation path.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for each node of a navigation path.</returns>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x0007967D File Offset: 0x0007787D
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x00079685 File Offset: 0x00077885
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		public virtual ITemplate NodeTemplate
		{
			get
			{
				return this.nodeTemplate;
			}
			set
			{
				this.nodeTemplate = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets the number of levels of parent nodes the control displays, relative to the currently displayed node.</summary>
		/// <returns>An integer that specifies the number of levels of parent nodes displayed, relative to the current context node. The default value is -1, which indicates no restriction on the number of parent levels that the control displays.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value for <see cref="P:System.Web.UI.WebControls.SiteMapPath.ParentLevelsDisplayed" /> is less than -1.</exception>
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x00079694 File Offset: 0x00077894
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000796A7 File Offset: 0x000778A7
		[Themeable(false)]
		[DefaultValue(-1)]
		public virtual int ParentLevelsDisplayed
		{
			get
			{
				return this.ViewState.GetInt("ParentLevelsDisplayed", -1);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ParentLevelsDisplayed"] = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets the order that the navigation path nodes are rendered in.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.PathDirection" /> that indicates the hierarchical order that navigation nodes are rendered in. The default is <see cref="F:System.Web.UI.WebControls.PathDirection.RootToCurrent" />, which indicates that the nodes are rendered in hierarchical order from the top-most node to the current node, from left to right.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value for <see cref="P:System.Web.UI.WebControls.SiteMapPath.PathDirection" /> is not one of the base <see cref="T:System.Web.UI.WebControls.PathDirection" /> enumerations. </exception>
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000796D4 File Offset: 0x000778D4
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x000796E7 File Offset: 0x000778E7
		[DefaultValue(PathDirection.RootToCurrent)]
		public virtual PathDirection PathDirection
		{
			get
			{
				return (PathDirection)this.ViewState.GetInt("PathDirection", 0);
			}
			set
			{
				if (value != PathDirection.RootToCurrent && value != PathDirection.CurrentToRoot)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PathDirection"] = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets the string that delimits <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> nodes in the rendered navigation path.</summary>
		/// <returns>A string that represents the delimiter for the nodes in a navigation path. The default is " &gt; ", which is a character pointing from left to right, and corresponds to the default <see cref="T:System.Web.UI.WebControls.PathDirection" />, which is set to <see cref="F:System.Web.UI.WebControls.PathDirection.RootToCurrent" />.</returns>
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x00079717 File Offset: 0x00077917
		// (set) Token: 0x06002DFA RID: 11770 RVA: 0x0007972E File Offset: 0x0007792E
		[DefaultValue(" > ")]
		[Localizable(true)]
		public virtual string PathSeparator
		{
			get
			{
				return this.ViewState.GetString("PathSeparator", " > ");
			}
			set
			{
				this.ViewState["PathSeparator"] = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets the style used for the <see cref="P:System.Web.UI.WebControls.SiteMapPath.PathSeparator" /> string.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings of the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's <see cref="P:System.Web.UI.WebControls.SiteMapPath.PathSeparator" /> text.</returns>
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x00079747 File Offset: 0x00077947
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style PathSeparatorStyle
		{
			get
			{
				if (this.pathSeparatorStyle == null)
				{
					this.pathSeparatorStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.pathSeparatorStyle).TrackViewState();
					}
				}
				return this.pathSeparatorStyle;
			}
		}

		/// <summary>Gets or sets a control template to use for the path delimiter of a site navigation path.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for the path delimiter of a navigation path.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x00079775 File Offset: 0x00077975
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x0007977D File Offset: 0x0007797D
		[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate PathSeparatorTemplate
		{
			get
			{
				return this.pathSeparatorTemplate;
			}
			set
			{
				this.pathSeparatorTemplate = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Web.SiteMapProvider" /> that is associated with the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> instance that is associated with the control. If no provider is explicitly set, the default site map provider is used.</returns>
		/// <exception cref="T:System.Web.HttpException">The provider named by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.SiteMapProvider" /> property is not available.- or -There is no default provider configured for the site.</exception>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x0007978C File Offset: 0x0007798C
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x0007980B File Offset: 0x00077A0B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public SiteMapProvider Provider
		{
			get
			{
				if (this.provider == null)
				{
					if (this.SiteMapProvider.Length == 0)
					{
						this.provider = SiteMap.Provider;
						if (this.provider == null)
						{
							throw new HttpException("There is no default provider configured for the site.");
						}
					}
					else
					{
						this.provider = SiteMap.Providers[this.SiteMapProvider];
						if (this.provider == null)
						{
							throw new HttpException("SiteMap provider '" + this.SiteMapProvider + "' not found.");
						}
					}
				}
				return this.provider;
			}
			set
			{
				this.provider = value;
				this.UpdateControls();
			}
		}

		/// <summary>Indicates whether the site navigation node that represents the currently displayed page is rendered as a hyperlink.</summary>
		/// <returns>true if the node that represents the current page is rendered as a hyperlink; otherwise, false. The default value is false.</returns>
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x0007981A File Offset: 0x00077A1A
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x0007982D File Offset: 0x00077A2D
		[DefaultValue(false)]
		public virtual bool RenderCurrentNodeAsLink
		{
			get
			{
				return this.ViewState.GetBool("RenderCurrentNodeAsLink", false);
			}
			set
			{
				this.ViewState["RenderCurrentNodeAsLink"] = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets the style for the root node display text.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style settings for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control root node display text.</returns>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x0007984B File Offset: 0x00077A4B
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style RootNodeStyle
		{
			get
			{
				if (this.rootNodeStyle == null)
				{
					this.rootNodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.rootNodeStyle).TrackViewState();
					}
				}
				return this.rootNodeStyle;
			}
		}

		/// <summary>Gets or sets a control template to use for the root node of a site navigation path.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that implements the <see cref="M:System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control)" /> method, to render custom content for the root node of a navigation path.</returns>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x00079879 File Offset: 0x00077A79
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x00079881 File Offset: 0x00077A81
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		public virtual ITemplate RootNodeTemplate
		{
			get
			{
				return this.rootNodeTemplate;
			}
			set
			{
				this.rootNodeTemplate = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control writes an additional hyperlink attribute for hyperlinked navigation nodes. Depending on client support, when a mouse hovers over a hyperlink that has the additional attribute set, a ToolTip is displayed.</summary>
		/// <returns>true if alternate text should be written for hyperlinked navigation nodes; otherwise, false. The default value is true.</returns>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00079890 File Offset: 0x00077A90
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000798A3 File Offset: 0x00077AA3
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool ShowToolTips
		{
			get
			{
				return this.ViewState.GetBool("ShowToolTips", true);
			}
			set
			{
				this.ViewState["ShowToolTips"] = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.SiteMapProvider" /> used to render the site navigation control.</summary>
		/// <returns>The name of a <see cref="T:System.Web.SiteMapProvider" /> that defines the navigation structure for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> to display. All available providers are contained in the <see cref="P:System.Web.SiteMap.Providers" /> collection, and can be enumerated and retrieved by name using the <see cref="P:System.Web.SiteMapProviderCollection.Item(System.String)" /> property.</returns>
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0007919F File Offset: 0x0007739F
		// (set) Token: 0x06002E08 RID: 11784 RVA: 0x000798C1 File Offset: 0x00077AC1
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string SiteMapProvider
		{
			get
			{
				return this.ViewState.GetString("SiteMapProvider", "");
			}
			set
			{
				this.ViewState["SiteMapProvider"] = value;
				this.UpdateControls();
			}
		}

		/// <summary>Gets or sets a value that is used to render alternate text for screen readers to skip the control's content.</summary>
		/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control renders as alternate text with an invisible image, as a hint to screen readers. The default value is "Skip Navigation Links". </returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x000798DA File Offset: 0x00077ADA
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x0006BD85 File Offset: 0x00069F85
		[Localizable(true)]
		public virtual string SkipLinkText
		{
			get
			{
				return this.ViewState.GetString("SkipLinkText", "Skip Navigation Links");
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000798F1 File Offset: 0x00077AF1
		private void UpdateControls()
		{
			base.ChildControlsCreated = false;
		}

		/// <summary>Binds a data source to the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control and its child controls.</summary>
		// Token: 0x06002E0C RID: 11788 RVA: 0x000798FC File Offset: 0x00077AFC
		public override void DataBind()
		{
			base.DataBind();
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (control is SiteMapNodeItem)
				{
					SiteMapNodeItem siteMapNodeItem = (SiteMapNodeItem)control;
					this.OnItemDataBound(new SiteMapNodeItemEventArgs(siteMapNodeItem));
				}
			}
		}

		/// <summary>Clears the current child controls collection, and rebuilds it by calling the <see cref="M:System.Web.UI.WebControls.SiteMapPath.CreateControlHierarchy" /> method.</summary>
		// Token: 0x06002E0D RID: 11789 RVA: 0x00079970 File Offset: 0x00077B70
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateControlHierarchy();
			this.DataBind();
		}

		/// <summary>Examines the site map structure provided by the <see cref="P:System.Web.UI.WebControls.SiteMapPath.SiteMapProvider" /> and builds a child controls collection based on the styles and templates defined for the functional nodes.</summary>
		/// <exception cref="T:System.Web.HttpException">No <see cref="P:System.Web.UI.WebControls.SiteMapPath.SiteMapProvider" /> is available to the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control. </exception>
		// Token: 0x06002E0E RID: 11790 RVA: 0x0007998C File Offset: 0x00077B8C
		protected virtual void CreateControlHierarchy()
		{
			ArrayList arrayList = new ArrayList();
			SiteMapNode siteMapNode = this.Provider.CurrentNode;
			if (siteMapNode == null)
			{
				return;
			}
			int num = ((this.ParentLevelsDisplayed != -1) ? (this.ParentLevelsDisplayed + 1) : int.MaxValue);
			while (siteMapNode != null && num > 0)
			{
				if (arrayList.Count > 0)
				{
					SiteMapNodeItem siteMapNodeItem = new SiteMapNodeItem(arrayList.Count, SiteMapNodeItemType.PathSeparator);
					this.InitializeItem(siteMapNodeItem);
					SiteMapNodeItemEventArgs siteMapNodeItemEventArgs = new SiteMapNodeItemEventArgs(siteMapNodeItem);
					this.OnItemCreated(siteMapNodeItemEventArgs);
					arrayList.Add(siteMapNodeItem);
				}
				SiteMapNodeItemType siteMapNodeItemType;
				if (arrayList.Count == 0)
				{
					siteMapNodeItemType = SiteMapNodeItemType.Current;
				}
				else if (siteMapNode.ParentNode == null)
				{
					siteMapNodeItemType = SiteMapNodeItemType.Root;
				}
				else
				{
					siteMapNodeItemType = SiteMapNodeItemType.Parent;
				}
				SiteMapNodeItem siteMapNodeItem2 = new SiteMapNodeItem(arrayList.Count, siteMapNodeItemType);
				siteMapNodeItem2.SiteMapNode = siteMapNode;
				this.InitializeItem(siteMapNodeItem2);
				SiteMapNodeItemEventArgs siteMapNodeItemEventArgs2 = new SiteMapNodeItemEventArgs(siteMapNodeItem2);
				this.OnItemCreated(siteMapNodeItemEventArgs2);
				arrayList.Add(siteMapNodeItem2);
				siteMapNode = siteMapNode.ParentNode;
				num--;
			}
			if (this.PathDirection == PathDirection.RootToCurrent)
			{
				for (int i = arrayList.Count - 1; i >= 0; i--)
				{
					this.Controls.Add((Control)arrayList[i]);
				}
				return;
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				this.Controls.Add((Control)arrayList[j]);
			}
		}

		/// <summary>Populates a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" />, which is a Web server control that represents a <see cref="T:System.Web.SiteMapNode" />, with a set of child controls based on the node's function and the specified templates and styles for the node.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> to initialize. </param>
		// Token: 0x06002E0F RID: 11791 RVA: 0x00079AD4 File Offset: 0x00077CD4
		protected virtual void InitializeItem(SiteMapNodeItem item)
		{
			switch (item.ItemType)
			{
			case SiteMapNodeItemType.Root:
			{
				if (this.RootNodeTemplate != null)
				{
					item.ApplyStyle(this.NodeStyle);
					item.ApplyStyle(this.RootNodeStyle);
					this.RootNodeTemplate.InstantiateIn(item);
					return;
				}
				if (this.NodeTemplate != null)
				{
					item.ApplyStyle(this.NodeStyle);
					item.ApplyStyle(this.RootNodeStyle);
					this.NodeTemplate.InstantiateIn(item);
					return;
				}
				WebControl webControl = this.CreateHyperLink(item);
				webControl.ApplyStyle(this.NodeStyle);
				webControl.ApplyStyle(this.RootNodeStyle);
				item.Controls.Add(webControl);
				return;
			}
			case SiteMapNodeItemType.Parent:
			{
				if (this.NodeTemplate != null)
				{
					item.ApplyStyle(this.NodeStyle);
					this.NodeTemplate.InstantiateIn(item);
					return;
				}
				WebControl webControl2 = this.CreateHyperLink(item);
				webControl2.ApplyStyle(this.NodeStyle);
				item.Controls.Add(webControl2);
				return;
			}
			case SiteMapNodeItemType.Current:
			{
				if (this.CurrentNodeTemplate != null)
				{
					item.ApplyStyle(this.NodeStyle);
					item.ApplyStyle(this.CurrentNodeStyle);
					this.CurrentNodeTemplate.InstantiateIn(item);
					return;
				}
				if (this.NodeTemplate != null)
				{
					item.ApplyStyle(this.NodeStyle);
					item.ApplyStyle(this.CurrentNodeStyle);
					this.NodeTemplate.InstantiateIn(item);
					return;
				}
				if (this.RenderCurrentNodeAsLink)
				{
					HyperLink hyperLink = this.CreateHyperLink(item);
					hyperLink.ApplyStyle(this.NodeStyle);
					hyperLink.ApplyStyle(this.CurrentNodeStyle);
					item.Controls.Add(hyperLink);
					return;
				}
				Literal literal = this.CreateLiteral(item);
				item.ApplyStyle(this.NodeStyle);
				item.ApplyStyle(this.CurrentNodeStyle);
				item.Controls.Add(literal);
				return;
			}
			case SiteMapNodeItemType.PathSeparator:
			{
				if (this.PathSeparatorTemplate != null)
				{
					item.ApplyStyle(this.PathSeparatorStyle);
					this.PathSeparatorTemplate.InstantiateIn(item);
					return;
				}
				Literal literal2 = new Literal();
				literal2.Text = HttpUtility.HtmlEncode(this.PathSeparator);
				item.ApplyStyle(this.PathSeparatorStyle);
				item.Controls.Add(literal2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00079CDC File Offset: 0x00077EDC
		private HyperLink CreateHyperLink(SiteMapNodeItem item)
		{
			HyperLink hyperLink = new HyperLink();
			hyperLink.Text = item.SiteMapNode.Title;
			hyperLink.NavigateUrl = item.SiteMapNode.Url;
			if (this.ShowToolTips)
			{
				hyperLink.ToolTip = item.SiteMapNode.Description;
			}
			return hyperLink;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00079D2B File Offset: 0x00077F2B
		private Literal CreateLiteral(SiteMapNodeItem item)
		{
			return new Literal
			{
				Text = item.SiteMapNode.Title
			};
		}

		/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.SiteMapPath.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
		// Token: 0x06002E12 RID: 11794 RVA: 0x00079D44 File Offset: 0x00077F44
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.CurrentNodeStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.NodeStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.PathSeparatorStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.RootNodeStyle).LoadViewState(array[4]);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnDataBinding(System.EventArgs)" /> method of the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> class and raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06002E13 RID: 11795 RVA: 0x00047E2B File Offset: 0x0004602B
		[global::System.MonoTODO("why override?")]
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
		}

		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002E14 RID: 11796 RVA: 0x00079DB8 File Offset: 0x00077FB8
		[global::System.MonoTODO("why override?")]
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>Renders the nodes in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
		// Token: 0x06002E15 RID: 11797 RVA: 0x00079DC4 File Offset: 0x00077FC4
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			string text = this.ClientID + "_SkipLink";
			string skipLinkText = this.SkipLinkText;
			bool flag = !string.IsNullOrEmpty(skipLinkText);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + text);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Page.ClientScript.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			base.RenderContents(writer);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, text);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
		}

		/// <summary>Saves changes to view state for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control.</summary>
		/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x06002E16 RID: 11798 RVA: 0x00079E9C File Offset: 0x0007809C
		protected override object SaveViewState()
		{
			object[] array = new object[5];
			array[0] = base.SaveViewState();
			if (this.currentNodeStyle != null)
			{
				array[1] = ((IStateManager)this.currentNodeStyle).SaveViewState();
			}
			if (this.nodeStyle != null)
			{
				array[2] = ((IStateManager)this.nodeStyle).SaveViewState();
			}
			if (this.pathSeparatorStyle != null)
			{
				array[3] = ((IStateManager)this.pathSeparatorStyle).SaveViewState();
			}
			if (this.rootNodeStyle != null)
			{
				array[4] = ((IStateManager)this.rootNodeStyle).SaveViewState();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Tracks changes to the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's view state.</summary>
		// Token: 0x06002E17 RID: 11799 RVA: 0x00079F28 File Offset: 0x00078128
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.currentNodeStyle != null)
			{
				((IStateManager)this.currentNodeStyle).TrackViewState();
			}
			if (this.nodeStyle != null)
			{
				((IStateManager)this.nodeStyle).TrackViewState();
			}
			if (this.pathSeparatorStyle != null)
			{
				((IStateManager)this.pathSeparatorStyle).TrackViewState();
			}
			if (this.rootNodeStyle != null)
			{
				((IStateManager)this.rootNodeStyle).TrackViewState();
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00079F87 File Offset: 0x00078187
		// Note: this type is marked as 'beforefieldinit'.
		static SiteMapPath()
		{
			SiteMapPath.ItemCreatedEvent = new object();
			SiteMapPath.ItemDataBoundEvent = new object();
		}

		// Token: 0x04001B8C RID: 7052
		private SiteMapProvider provider;

		// Token: 0x04001B8D RID: 7053
		private Style currentNodeStyle;

		// Token: 0x04001B8E RID: 7054
		private Style nodeStyle;

		// Token: 0x04001B8F RID: 7055
		private Style pathSeparatorStyle;

		// Token: 0x04001B90 RID: 7056
		private Style rootNodeStyle;

		// Token: 0x04001B91 RID: 7057
		private ITemplate currentNodeTemplate;

		// Token: 0x04001B92 RID: 7058
		private ITemplate nodeTemplate;

		// Token: 0x04001B93 RID: 7059
		private ITemplate pathSeparatorTemplate;

		// Token: 0x04001B94 RID: 7060
		private ITemplate rootNodeTemplate;
	}
}
