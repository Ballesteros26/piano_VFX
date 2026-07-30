using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>A data bound list control that displays items using templates.</summary>
	// Token: 0x02000382 RID: 898
	[Designer("System.Web.UI.Design.WebControls.DataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlValueProperty("SelectedValue")]
	[Editor("System.Web.UI.Design.WebControls.DataListComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.ComponentEditor, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataList : BaseDataList, INamingContainer, IRepeatInfoUser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataList" /> class.</summary>
		// Token: 0x06002259 RID: 8793 RVA: 0x000586AE File Offset: 0x000568AE
		public DataList()
		{
			this.idx = -1;
		}

		/// <summary>Gets the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that represents the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000586BD File Offset: 0x000568BD
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual TableItemStyle AlternatingItemStyle
		{
			get
			{
				if (this.alternatingItemStyle == null)
				{
					this.alternatingItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.alternatingItemStyle.TrackViewState();
					}
				}
				return this.alternatingItemStyle;
			}
		}

		/// <summary>Gets or sets the template for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000586EB File Offset: 0x000568EB
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x000586F3 File Offset: 0x000568F3
		[TemplateContainer(typeof(DataListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
			}
		}

		/// <summary>Gets or sets the index number of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control to edit.</summary>
		/// <returns>The index number of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control to edit.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000586FC File Offset: 0x000568FC
		// (set) Token: 0x0600225E RID: 8798 RVA: 0x00058725 File Offset: 0x00056925
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual int EditItemIndex
		{
			get
			{
				object obj = this.ViewState["EditItemIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("EditItemIndex", "< -1");
				}
				this.ViewState["EditItemIndex"] = value;
			}
		}

		/// <summary>Gets the style properties for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x00058751 File Offset: 0x00056951
		[WebCategory("Style")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		public virtual TableItemStyle EditItemStyle
		{
			get
			{
				if (this.editItemStyle == null)
				{
					this.editItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.editItemStyle.TrackViewState();
					}
				}
				return this.editItemStyle;
			}
		}

		/// <summary>Gets or sets the template for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x0005877F File Offset: 0x0005697F
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x00058787 File Offset: 0x00056987
		[WebCategory("Style")]
		[WebSysDescription("")]
		[TemplateContainer(typeof(DataListItem))]
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the rows of a <see cref="T:System.Web.UI.WebControls.Table" /> control, defined in each template of a <see cref="T:System.Web.UI.WebControls.DataList" /> control, are extracted and displayed.</summary>
		/// <returns>true if the rows of a <see cref="T:System.Web.UI.WebControls.Table" /> control, defined in each template of a <see cref="T:System.Web.UI.WebControls.DataList" /> control, are extracted and displayed; otherwise, false. The default value is false.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00058790 File Offset: 0x00056990
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x000587B9 File Offset: 0x000569B9
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual bool ExtractTemplateRows
		{
			get
			{
				object obj = this.ViewState["ExtractTemplateRows"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ExtractTemplateRows"] = value;
			}
		}

		/// <summary>Gets the style properties for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000587D1 File Offset: 0x000569D1
		[WebCategory("Style")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.footerStyle.TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		/// <summary>Gets or sets the template for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000587FF File Offset: 0x000569FF
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x00058807 File Offset: 0x00056A07
		[WebCategory("Style")]
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[DefaultValue(null)]
		[Browsable(false)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		/// <summary>Gets or sets the grid line style for the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the <see cref="P:System.Web.UI.WebControls.DataList.RepeatLayout" /> property is set to RepeatLayout.Table.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default value is None.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00058810 File Offset: 0x00056A10
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x00058827 File Offset: 0x00056A27
		[DefaultValue(GridLines.None)]
		public override GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return this.TableStyle.GridLines;
			}
			set
			{
				this.TableStyle.GridLines = value;
			}
		}

		/// <summary>Gets the style properties for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x00058835 File Offset: 0x00056A35
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.headerStyle.TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the template for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00058863 File Offset: 0x00056A63
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x0005886B File Offset: 0x00056A6B
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects representing the individual items within the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> that contains a collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects representing the individual items within the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x00058874 File Offset: 0x00056A74
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual DataListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new DataListItemCollection(this.ItemList);
				}
				return this.items;
			}
		}

		/// <summary>Gets the style properties for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x00058895 File Offset: 0x00056A95
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Style")]
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.itemStyle.TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		/// <summary>Gets or sets the template for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000588C3 File Offset: 0x00056AC3
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x000588CB File Offset: 0x00056ACB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Style")]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("")]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
			}
		}

		/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is 0, which indicates that the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control are displayed in a single row or column, based on the value of the <see cref="P:System.Web.UI.WebControls.DataList.RepeatDirection" /> property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified number of columns is a negative value. </exception>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000588D4 File Offset: 0x00056AD4
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x000588FD File Offset: 0x00056AFD
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("")]
		public virtual int RepeatColumns
		{
			get
			{
				object obj = this.ViewState["RepeatColumns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "RepeatColumns value has to be 0 for 'not set' or > 0.");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Web.UI.WebControls.DataList" /> control displays vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is Vertical.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0005892C File Offset: 0x00056B2C
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x00058955 File Offset: 0x00056B55
		[DefaultValue(RepeatDirection.Vertical)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				object obj = this.ViewState["RepeatDirection"];
				if (obj != null)
				{
					return (RepeatDirection)obj;
				}
				return RepeatDirection.Vertical;
			}
			set
			{
				this.ViewState["RepeatDirection"] = value;
			}
		}

		/// <summary>Gets or sets whether the control is displayed in a table or flow layout.</summary>
		/// <returns>A value that specifies whether the control is displayed in a table or in flow layout.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the supported <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values.</exception>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x00058970 File Offset: 0x00056B70
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x00058999 File Offset: 0x00056B99
		[DefaultValue(RepeatLayout.Table)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				object obj = this.ViewState["RepeatLayout"];
				if (obj != null)
				{
					return (RepeatLayout)obj;
				}
				return RepeatLayout.Table;
			}
			set
			{
				if (value == RepeatLayout.OrderedList || value == RepeatLayout.UnorderedList)
				{
					throw new ArgumentOutOfRangeException(string.Format("DataList does not support the '{0}' layout.", value));
				}
				this.ViewState["RepeatLayout"] = value;
			}
		}

		/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than -1.</exception>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000589D0 File Offset: 0x00056BD0
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x000589F9 File Offset: 0x00056BF9
		[WebCategory("Layout")]
		[Bindable(true)]
		[WebSysDescription("")]
		[DefaultValue(-1)]
		public virtual int SelectedIndex
		{
			get
			{
				object obj = this.ViewState["SelectedIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", "< -1");
				}
				this.ViewState["SelectedIndex"] = value;
			}
		}

		/// <summary>Gets the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> that represents the item selected in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x00058A28 File Offset: 0x00056C28
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataListItem SelectedItem
		{
			get
			{
				if (this.SelectedIndex < 0)
				{
					return null;
				}
				if (this.SelectedIndex >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("SelectedItem", ">= Items.Count");
				}
				return this.items[this.SelectedIndex];
			}
		}

		/// <summary>Gets the style properties for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00058A74 File Offset: 0x00056C74
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DefaultValue(null)]
		public virtual TableItemStyle SelectedItemStyle
		{
			get
			{
				if (this.selectedItemStyle == null)
				{
					this.selectedItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectedItemStyle.TrackViewState();
					}
				}
				return this.selectedItemStyle;
			}
		}

		/// <summary>Gets or sets the template for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x00058AA2 File Offset: 0x00056CA2
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x00058AAA File Offset: 0x00056CAA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[DefaultValue(null)]
		[Browsable(false)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual ITemplate SelectedItemTemplate
		{
			get
			{
				return this.selectedItemTemplate;
			}
			set
			{
				this.selectedItemTemplate = value;
			}
		}

		/// <summary>Gets the style properties of the separator between each item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the separator between each item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00058AB3 File Offset: 0x00056CB3
		[WebCategory("Style")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		public virtual TableItemStyle SeparatorStyle
		{
			get
			{
				if (this.separatorStyle == null)
				{
					this.separatorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.separatorStyle.TrackViewState();
					}
				}
				return this.separatorStyle;
			}
		}

		/// <summary>Gets or sets the template for the separator between the items of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the separator between items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00058AE1 File Offset: 0x00056CE1
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x00058AE9 File Offset: 0x00056CE9
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual ITemplate SeparatorTemplate
		{
			get
			{
				return this.separatorTemplate;
			}
			set
			{
				this.separatorTemplate = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the footer section is displayed in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>true if the footer section is displayed; otherwise, false. The default value is true, however this property is only examined when the <see cref="P:System.Web.UI.WebControls.DataList.FooterTemplate" /> property is not null.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00058AF4 File Offset: 0x00056CF4
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x00055D09 File Offset: 0x00053F09
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual bool ShowFooter
		{
			get
			{
				object obj = this.ViewState["ShowFooter"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the header section is displayed in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>true if the header is displayed; otherwise, false. The default value is true, however this property is only examined when the <see cref="P:System.Web.UI.WebControls.DataList.HeaderTemplate" /> property is not null.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x00058B20 File Offset: 0x00056D20
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x00055D34 File Offset: 0x00053F34
		[WebSysDescription("")]
		[DefaultValue(true)]
		[WebCategory("Appearance")]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.ViewState["ShowHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowHeader"] = value;
			}
		}

		/// <summary>Gets the value of the key field for the selected data list item.</summary>
		/// <returns>The key field value for the selected data list item. The default is null, which indicates that no data list item is currently selected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataKeyField" /> property has not been set.</exception>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x00058B4C File Offset: 0x00056D4C
		[global::System.MonoTODO("incomplete")]
		[Browsable(false)]
		public object SelectedValue
		{
			get
			{
				if (this.DataKeyField.Length == 0)
				{
					throw new InvalidOperationException(global::Locale.GetText("No DataKeyField present."));
				}
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0 && selectedIndex < base.DataKeys.Count)
				{
					return base.DataKeys[selectedIndex];
				}
				return null;
			}
		}

		/// <summary>Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>Returns the <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" /> tag if the <see cref="P:System.Web.UI.WebControls.DataList.RepeatLayout" /> is set to <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />; otherwise, returns the <see cref="F:System.Web.UI.HtmlTextWriterTag.Span" /> tag. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0004D090 File Offset: 0x0004B290
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x00047D06 File Offset: 0x00045F06
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00058B9D File Offset: 0x00056D9D
		private ArrayList ItemList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00058BB8 File Offset: 0x00056DB8
		private void DoItem(int i, ListItemType t, object d, bool databind)
		{
			DataListItem dataListItem = this.CreateItem(i, t);
			if (databind)
			{
				dataListItem.DataItem = d;
			}
			DataListItemEventArgs dataListItemEventArgs = new DataListItemEventArgs(dataListItem);
			this.InitializeItem(dataListItem);
			this.Controls.Add(dataListItem);
			if (i != -1)
			{
				this.ItemList.Add(dataListItem);
			}
			this.OnItemCreated(dataListItemEventArgs);
			if (databind)
			{
				dataListItem.DataBind();
				this.OnItemDataBound(dataListItemEventArgs);
				dataListItem.DataItem = null;
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x00058C23 File Offset: 0x00056E23
		private void DoItemInLoop(int i, object d, bool databind, ListItemType type)
		{
			this.DoItem(i, type, d, databind);
			if (this.SeparatorTemplate != null)
			{
				this.DoItem(i, ListItemType.Separator, null, databind);
			}
		}

		/// <summary>Creates the control hierarchy that is used to render the data list control, with or without the specified data source.</summary>
		/// <param name="useDataSource">true to use the control's data source; false to indicate that the control is being recreated from view state and should not be data-bound.</param>
		// Token: 0x06002289 RID: 8841 RVA: 0x00058C44 File Offset: 0x00056E44
		protected override void CreateControlHierarchy(bool useDataSource)
		{
			this.Controls.Clear();
			this.ItemList.Clear();
			IEnumerable enumerable = null;
			ArrayList arrayList = null;
			if (useDataSource)
			{
				this.idx = 0;
				if (base.IsBoundUsingDataSourceID)
				{
					enumerable = this.GetData();
				}
				else
				{
					enumerable = DataSourceResolver.ResolveDataSource(this.DataSource, base.DataMember);
				}
				arrayList = base.DataKeysArray;
				arrayList.Clear();
			}
			else
			{
				this.idx = (int)this.ViewState["Items"];
			}
			if (enumerable == null && this.idx == 0)
			{
				return;
			}
			if (this.headerTemplate != null)
			{
				this.DoItem(-1, ListItemType.Header, null, useDataSource);
			}
			int selectedIndex = this.SelectedIndex;
			int editItemIndex = this.EditItemIndex;
			if (enumerable != null)
			{
				string dataKeyField = this.DataKeyField;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						if (useDataSource && !string.IsNullOrEmpty(dataKeyField))
						{
							arrayList.Add(DataBinder.GetPropertyValue(obj, dataKeyField));
						}
						ListItemType listItemType = ListItemType.Item;
						if (this.idx == editItemIndex)
						{
							listItemType = ListItemType.EditItem;
						}
						else if (this.idx == selectedIndex)
						{
							listItemType = ListItemType.SelectedItem;
						}
						else if ((this.idx & 1) != 0)
						{
							listItemType = ListItemType.AlternatingItem;
						}
						this.DoItemInLoop(this.idx, obj, useDataSource, listItemType);
						this.idx++;
					}
					goto IL_0186;
				}
			}
			for (int i = 0; i < this.idx; i++)
			{
				ListItemType listItemType = ListItemType.Item;
				if (i == editItemIndex)
				{
					listItemType = ListItemType.EditItem;
				}
				else if (i == selectedIndex)
				{
					listItemType = ListItemType.SelectedItem;
				}
				else if ((i & 1) != 0)
				{
					listItemType = ListItemType.AlternatingItem;
				}
				this.DoItemInLoop(i, null, useDataSource, listItemType);
			}
			IL_0186:
			if (this.footerTemplate != null)
			{
				this.DoItem(-1, ListItemType.Footer, null, useDataSource);
			}
			this.ViewState["Items"] = this.idx;
		}

		/// <summary>Creates the default style object that is used internally by the <see cref="T:System.Web.UI.WebControls.DataList" /> control to implement all style related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableStyle" /> that contains the default style properties for the control.</returns>
		// Token: 0x0600228A RID: 8842 RVA: 0x00058E14 File Offset: 0x00057014
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellSpacing = 0
			};
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object.</summary>
		/// <returns>A new <see cref="T:System.Web.UI.WebControls.DataListItem" /> created with the specified list-item type.</returns>
		/// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.DataList" /> to place the created item.</param>
		/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" /> that represents the specified type of the item to create.</param>
		// Token: 0x0600228B RID: 8843 RVA: 0x00058E22 File Offset: 0x00057022
		protected virtual DataListItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new DataListItem(itemIndex, itemType);
		}

		/// <summary>Initializes a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object based on the specified templates and styles for the list-item type.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataListItem" /> to initialize.</param>
		// Token: 0x0600228C RID: 8844 RVA: 0x00058E2C File Offset: 0x0005702C
		protected virtual void InitializeItem(DataListItem item)
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
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				if (item.ItemType == ListItemType.EditItem && this.EditItemTemplate != null)
				{
					template = this.EditItemTemplate;
				}
				else if (item.ItemType == ListItemType.SelectedItem && this.SelectedItemTemplate != null)
				{
					template = this.SelectedItemTemplate;
				}
				else if (item.ItemType == ListItemType.AlternatingItem && this.AlternatingItemTemplate != null)
				{
					template = this.AlternatingItemTemplate;
				}
				else
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

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <param name="savedState">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.DataList" />.</param>
		// Token: 0x0600228D RID: 8845 RVA: 0x00058EE0 File Offset: 0x000570E0
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				this.ItemStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.SelectedItemStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.AlternatingItemStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.EditItemStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.SeparatorStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.HeaderStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.FooterStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				base.ControlStyle.LoadViewState(array[8]);
			}
		}

		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">The event data.</param>
		// Token: 0x0600228E RID: 8846 RVA: 0x00058F98 File Offset: 0x00057198
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			DataListCommandEventArgs dataListCommandEventArgs = e as DataListCommandEventArgs;
			if (dataListCommandEventArgs == null)
			{
				return false;
			}
			string commandName = dataListCommandEventArgs.CommandName;
			CultureInfo invariantCulture = Helpers.InvariantCulture;
			this.OnItemCommand(dataListCommandEventArgs);
			if (string.Compare(commandName, "Cancel", true, invariantCulture) == 0)
			{
				this.OnCancelCommand(dataListCommandEventArgs);
			}
			else if (string.Compare(commandName, "Delete", true, invariantCulture) == 0)
			{
				this.OnDeleteCommand(dataListCommandEventArgs);
			}
			else if (string.Compare(commandName, "Edit", true, invariantCulture) == 0)
			{
				this.OnEditCommand(dataListCommandEventArgs);
			}
			else if (string.Compare(commandName, "Select", true, invariantCulture) == 0)
			{
				this.SelectedIndex = dataListCommandEventArgs.Item.ItemIndex;
				this.OnSelectedIndexChanged(dataListCommandEventArgs);
			}
			else if (string.Compare(commandName, "Update", true, invariantCulture) == 0)
			{
				this.OnUpdateCommand(dataListCommandEventArgs);
			}
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.CancelCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x0600228F RID: 8847 RVA: 0x00059050 File Offset: 0x00057250
		protected virtual void OnCancelCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.cancelCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.DeleteCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002290 RID: 8848 RVA: 0x00059080 File Offset: 0x00057280
		protected virtual void OnDeleteCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.deleteCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.EditCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002291 RID: 8849 RVA: 0x000590B0 File Offset: 0x000572B0
		protected virtual void OnEditCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.editCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002292 RID: 8850 RVA: 0x000590E0 File Offset: 0x000572E0
		protected internal override void OnInit(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterRequiresControlState(this);
			}
			base.OnInit(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002293 RID: 8851 RVA: 0x00059108 File Offset: 0x00057308
		protected virtual void OnItemCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.itemCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemCreated" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002294 RID: 8852 RVA: 0x00059138 File Offset: 0x00057338
		protected virtual void OnItemCreated(DataListItemEventArgs e)
		{
			DataListItemEventHandler dataListItemEventHandler = (DataListItemEventHandler)base.Events[DataList.itemCreatedEvent];
			if (dataListItemEventHandler != null)
			{
				dataListItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemDataBound" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002295 RID: 8853 RVA: 0x00059168 File Offset: 0x00057368
		protected virtual void OnItemDataBound(DataListItemEventArgs e)
		{
			DataListItemEventHandler dataListItemEventHandler = (DataListItemEventHandler)base.Events[DataList.itemDataBoundEvent];
			if (dataListItemEventHandler != null)
			{
				dataListItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.UpdateCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002296 RID: 8854 RVA: 0x00059198 File Offset: 0x00057398
		protected virtual void OnUpdateCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.updateCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Prepares the control hierarchy for rendering in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x06002297 RID: 8855 RVA: 0x000591C8 File Offset: 0x000573C8
		protected override void PrepareControlHierarchy()
		{
			if (!this.HasControls() || this.Controls.Count == 0)
			{
				return;
			}
			Style style = null;
			foreach (object obj in this.Controls)
			{
				DataListItem dataListItem = (DataListItem)obj;
				switch (dataListItem.ItemType)
				{
				case ListItemType.Header:
					if (!this.ShowHeader)
					{
						dataListItem.Visible = false;
					}
					else if (this.headerStyle != null)
					{
						dataListItem.MergeStyle(this.headerStyle);
					}
					break;
				case ListItemType.Footer:
					if (!this.ShowFooter)
					{
						dataListItem.Visible = false;
					}
					else if (this.footerStyle != null)
					{
						dataListItem.MergeStyle(this.footerStyle);
					}
					break;
				case ListItemType.Item:
					dataListItem.MergeStyle(this.itemStyle);
					break;
				case ListItemType.AlternatingItem:
					if (style == null)
					{
						if (this.alternatingItemStyle != null)
						{
							style = new TableItemStyle();
							style.CopyFrom(this.itemStyle);
							style.CopyFrom(this.alternatingItemStyle);
						}
						else
						{
							style = this.itemStyle;
						}
					}
					dataListItem.MergeStyle(style);
					break;
				case ListItemType.SelectedItem:
					if (this.selectedItemStyle != null)
					{
						dataListItem.MergeStyle(this.selectedItemStyle);
					}
					else
					{
						dataListItem.MergeStyle(this.itemStyle);
					}
					break;
				case ListItemType.EditItem:
					if (this.editItemStyle != null)
					{
						dataListItem.MergeStyle(this.editItemStyle);
					}
					else
					{
						dataListItem.MergeStyle(this.itemStyle);
					}
					break;
				case ListItemType.Separator:
					if (this.separatorStyle != null)
					{
						dataListItem.MergeStyle(this.separatorStyle);
					}
					else
					{
						dataListItem.MergeStyle(this.itemStyle);
					}
					break;
				}
			}
		}

		/// <summary>Renders the list items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002298 RID: 8856 RVA: 0x00059388 File Offset: 0x00057588
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			repeatInfo.RepeatColumns = this.RepeatColumns;
			repeatInfo.RepeatDirection = this.RepeatDirection;
			repeatInfo.RepeatLayout = this.RepeatLayout;
			repeatInfo.CaptionAlign = this.CaptionAlign;
			repeatInfo.Caption = this.Caption;
			repeatInfo.UseAccessibleHeader = this.UseAccessibleHeader;
			if (this.ExtractTemplateRows)
			{
				repeatInfo.OuterTableImplied = true;
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				if (base.ControlStyleCreated)
				{
					base.ControlStyle.AddAttributesToRender(writer);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
				writer.RenderEndTag();
				return;
			}
			repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
		}

		/// <summary>Saves the changes to the control view state since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.DataList" /> view state. </returns>
		// Token: 0x06002299 RID: 8857 RVA: 0x00059450 File Offset: 0x00057650
		protected override object SaveViewState()
		{
			object[] array = new object[9];
			array[0] = base.SaveViewState();
			if (this.itemStyle != null)
			{
				array[1] = this.itemStyle.SaveViewState();
			}
			if (this.selectedItemStyle != null)
			{
				array[2] = this.selectedItemStyle.SaveViewState();
			}
			if (this.alternatingItemStyle != null)
			{
				array[3] = this.alternatingItemStyle.SaveViewState();
			}
			if (this.editItemStyle != null)
			{
				array[4] = this.editItemStyle.SaveViewState();
			}
			if (this.separatorStyle != null)
			{
				array[5] = this.separatorStyle.SaveViewState();
			}
			if (this.headerStyle != null)
			{
				array[6] = this.headerStyle.SaveViewState();
			}
			if (this.footerStyle != null)
			{
				array[7] = this.footerStyle.SaveViewState();
			}
			if (base.ControlStyleCreated)
			{
				array[8] = base.ControlStyle.SaveViewState();
			}
			return array;
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.DataList" /> control so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x0600229A RID: 8858 RVA: 0x00059520 File Offset: 0x00057720
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.alternatingItemStyle != null)
			{
				this.alternatingItemStyle.TrackViewState();
			}
			if (this.editItemStyle != null)
			{
				this.editItemStyle.TrackViewState();
			}
			if (this.footerStyle != null)
			{
				this.footerStyle.TrackViewState();
			}
			if (this.headerStyle != null)
			{
				this.headerStyle.TrackViewState();
			}
			if (this.itemStyle != null)
			{
				this.itemStyle.TrackViewState();
			}
			if (this.selectedItemStyle != null)
			{
				this.selectedItemStyle.TrackViewState();
			}
			if (this.separatorStyle != null)
			{
				this.separatorStyle.TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.TrackViewState();
			}
		}

		/// <summary>Occurs when the Cancel button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x0600229B RID: 8859 RVA: 0x000595CB File Offset: 0x000577CB
		// (remove) Token: 0x0600229C RID: 8860 RVA: 0x000595DE File Offset: 0x000577DE
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataListCommandEventHandler CancelCommand
		{
			add
			{
				base.Events.AddHandler(DataList.cancelCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.cancelCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Delete button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x0600229D RID: 8861 RVA: 0x000595F1 File Offset: 0x000577F1
		// (remove) Token: 0x0600229E RID: 8862 RVA: 0x00059604 File Offset: 0x00057804
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataListCommandEventHandler DeleteCommand
		{
			add
			{
				base.Events.AddHandler(DataList.deleteCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.deleteCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Edit button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x0600229F RID: 8863 RVA: 0x00059617 File Offset: 0x00057817
		// (remove) Token: 0x060022A0 RID: 8864 RVA: 0x0005962A File Offset: 0x0005782A
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataListCommandEventHandler EditCommand
		{
			add
			{
				base.Events.AddHandler(DataList.editCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.editCommandEvent, value);
			}
		}

		/// <summary>Occurs when any button is clicked in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060022A1 RID: 8865 RVA: 0x0005963D File Offset: 0x0005783D
		// (remove) Token: 0x060022A2 RID: 8866 RVA: 0x00059650 File Offset: 0x00057850
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(DataList.itemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.itemCommandEvent, value);
			}
		}

		/// <summary>Occurs on the server when an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control is created.</summary>
		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060022A3 RID: 8867 RVA: 0x00059663 File Offset: 0x00057863
		// (remove) Token: 0x060022A4 RID: 8868 RVA: 0x00059676 File Offset: 0x00057876
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(DataList.itemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.itemCreatedEvent, value);
			}
		}

		/// <summary>Occurs when an item is data bound to the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060022A5 RID: 8869 RVA: 0x00059689 File Offset: 0x00057889
		// (remove) Token: 0x060022A6 RID: 8870 RVA: 0x0005969C File Offset: 0x0005789C
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(DataList.itemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.itemDataBoundEvent, value);
			}
		}

		/// <summary>Occurs when the Update button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060022A7 RID: 8871 RVA: 0x000596AF File Offset: 0x000578AF
		// (remove) Token: 0x060022A8 RID: 8872 RVA: 0x000596C2 File Offset: 0x000578C2
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListCommandEventHandler UpdateCommand
		{
			add
			{
				base.Events.AddHandler(DataList.updateCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.updateCommandEvent, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasFooter" />.</summary>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000596D5 File Offset: 0x000578D5
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.ShowFooter && this.footerTemplate != null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasHeader" />.</summary>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x000596EA File Offset: 0x000578EA
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.ShowHeader && this.headerTemplate != null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasSeparators" />.</summary>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000596FF File Offset: 0x000578FF
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.separatorTemplate != null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.RepeatedItemCount" />.</summary>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x0005970C File Offset: 0x0005790C
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				if (this.idx == -1)
				{
					object obj = this.ViewState["Items"];
					this.idx = ((obj == null) ? 0 : ((int)obj));
				}
				return this.idx;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(System.Web.UI.WebControls.ListItemType,System.Int32)" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values.</param>
		/// <param name="repeatIndex">The index of the item in the list control.</param>
		// Token: 0x060022AD RID: 8877 RVA: 0x0005974C File Offset: 0x0005794C
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			DataListItem dataListItem;
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
				if (repeatIndex >= 0 && (!this.HasControls() || repeatIndex >= this.Controls.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				dataListItem = this.FindFirstItem(itemType);
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				if (repeatIndex >= 0 && (!this.HasControls() || repeatIndex >= this.Controls.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				dataListItem = this.FindBestItem(repeatIndex);
				break;
			case ListItemType.Separator:
				if (repeatIndex >= 0 && (!this.HasControls() || repeatIndex >= this.Controls.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				dataListItem = this.FindSpecificItem(itemType, repeatIndex);
				break;
			default:
				dataListItem = null;
				break;
			}
			if (dataListItem == null || !dataListItem.ControlStyleCreated)
			{
				return null;
			}
			return dataListItem.ControlStyle;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x00059814 File Offset: 0x00057A14
		private DataListItem FindFirstItem(ListItemType itemType)
		{
			for (int i = 0; i < this.Controls.Count; i++)
			{
				DataListItem dataListItem = this.Controls[i] as DataListItem;
				if (dataListItem != null && dataListItem.ItemType == itemType)
				{
					return dataListItem;
				}
			}
			return null;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00059858 File Offset: 0x00057A58
		private DataListItem FindSpecificItem(ListItemType itemType, int repeatIndex)
		{
			for (int i = 0; i < this.Controls.Count; i++)
			{
				DataListItem dataListItem = this.Controls[i] as DataListItem;
				if (dataListItem != null && dataListItem.ItemType == itemType && dataListItem.ItemIndex == repeatIndex)
				{
					return dataListItem;
				}
			}
			return null;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000598A8 File Offset: 0x00057AA8
		private DataListItem FindBestItem(int repeatIndex)
		{
			int i = 0;
			while (i < this.Controls.Count)
			{
				DataListItem dataListItem = this.Controls[i] as DataListItem;
				if (dataListItem != null && dataListItem.ItemIndex == repeatIndex)
				{
					ListItemType itemType = dataListItem.ItemType;
					if (itemType - ListItemType.Item <= 3)
					{
						return dataListItem;
					}
					return null;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(System.Web.UI.WebControls.ListItemType,System.Int32,System.Web.UI.WebControls.RepeatInfo,System.Web.UI.HtmlTextWriter)" />.</summary>
		/// <param name="itemType">The type of the item.</param>
		/// <param name="repeatIndex">The index of the item.</param>
		/// <param name="repeatInfo">Information that is used to render the item.</param>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render the item.</param>
		// Token: 0x060022B1 RID: 8881 RVA: 0x000598FC File Offset: 0x00057AFC
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			if (!this.HasControls())
			{
				return;
			}
			DataListItem dataListItem = null;
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
				dataListItem = this.FindFirstItem(itemType);
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				dataListItem = this.FindBestItem(repeatIndex);
				break;
			case ListItemType.Separator:
				dataListItem = this.FindSpecificItem(itemType, repeatIndex);
				break;
			}
			if (dataListItem != null)
			{
				bool extractTemplateRows = this.ExtractTemplateRows;
				bool flag = this.RepeatLayout == RepeatLayout.Table;
				if (!flag || extractTemplateRows)
				{
					Style style = ((IRepeatInfoUser)this).GetItemStyle(itemType, repeatIndex);
					if (style != null)
					{
						dataListItem.ControlStyle.CopyFrom(style);
					}
				}
				dataListItem.RenderItem(writer, extractTemplateRows, flag);
			}
		}

		/// <summary>Represents the Cancel command name. This field is read-only.</summary>
		// Token: 0x04001916 RID: 6422
		public const string CancelCommandName = "Cancel";

		/// <summary>Represents the Delete command name. This field is read-only.</summary>
		// Token: 0x04001917 RID: 6423
		public const string DeleteCommandName = "Delete";

		/// <summary>Represents the Edit command name. This field is read-only.</summary>
		// Token: 0x04001918 RID: 6424
		public const string EditCommandName = "Edit";

		/// <summary>Represents the Select command name. This field is read-only.</summary>
		// Token: 0x04001919 RID: 6425
		public const string SelectCommandName = "Select";

		/// <summary>Represents the Update command name. This field is read-only.</summary>
		// Token: 0x0400191A RID: 6426
		public const string UpdateCommandName = "Update";

		// Token: 0x0400191B RID: 6427
		private static readonly object cancelCommandEvent = new object();

		// Token: 0x0400191C RID: 6428
		private static readonly object deleteCommandEvent = new object();

		// Token: 0x0400191D RID: 6429
		private static readonly object editCommandEvent = new object();

		// Token: 0x0400191E RID: 6430
		private static readonly object itemCommandEvent = new object();

		// Token: 0x0400191F RID: 6431
		private static readonly object itemCreatedEvent = new object();

		// Token: 0x04001920 RID: 6432
		private static readonly object itemDataBoundEvent = new object();

		// Token: 0x04001921 RID: 6433
		private static readonly object updateCommandEvent = new object();

		// Token: 0x04001922 RID: 6434
		private TableItemStyle alternatingItemStyle;

		// Token: 0x04001923 RID: 6435
		private TableItemStyle editItemStyle;

		// Token: 0x04001924 RID: 6436
		private TableItemStyle footerStyle;

		// Token: 0x04001925 RID: 6437
		private TableItemStyle headerStyle;

		// Token: 0x04001926 RID: 6438
		private TableItemStyle itemStyle;

		// Token: 0x04001927 RID: 6439
		private TableItemStyle selectedItemStyle;

		// Token: 0x04001928 RID: 6440
		private TableItemStyle separatorStyle;

		// Token: 0x04001929 RID: 6441
		private ITemplate alternatingItemTemplate;

		// Token: 0x0400192A RID: 6442
		private ITemplate editItemTemplate;

		// Token: 0x0400192B RID: 6443
		private ITemplate footerTemplate;

		// Token: 0x0400192C RID: 6444
		private ITemplate headerTemplate;

		// Token: 0x0400192D RID: 6445
		private ITemplate itemTemplate;

		// Token: 0x0400192E RID: 6446
		private ITemplate selectedItemTemplate;

		// Token: 0x0400192F RID: 6447
		private ITemplate separatorTemplate;

		// Token: 0x04001930 RID: 6448
		private DataListItemCollection items;

		// Token: 0x04001931 RID: 6449
		private ArrayList list;

		// Token: 0x04001932 RID: 6450
		private int idx;
	}
}
