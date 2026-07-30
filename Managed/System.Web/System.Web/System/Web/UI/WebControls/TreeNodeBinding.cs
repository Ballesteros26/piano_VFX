using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the relationship between a data item and the node it is binding to in a <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x0200042D RID: 1069
	[DefaultProperty("TextField")]
	public sealed class TreeNodeBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		/// <summary>Gets or sets the value to match against a <see cref="P:System.Web.UI.IHierarchyData.Type" /> property for a data item to determine whether to apply the tree node binding.</summary>
		/// <returns>The value to match against a data item's <see cref="P:System.Web.UI.IHierarchyData.Type" /> property to determine whether to apply the tree node binding. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property is not set.</returns>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000803F8 File Offset: 0x0007E5F8
		// (set) Token: 0x0600309E RID: 12446 RVA: 0x00080425 File Offset: 0x0007E625
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				object obj = this.ViewState["DataMember"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		/// <summary>Gets or sets the node depth at which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The node depth at which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is -1, indicating that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.Depth" /> property is not set.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x00080438 File Offset: 0x0007E638
		// (set) Token: 0x060030A0 RID: 12448 RVA: 0x00080461 File Offset: 0x0007E661
		[DefaultValue(-1)]
		public int Depth
		{
			get
			{
				object obj = this.ViewState["Depth"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				this.ViewState["Depth"] = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the text of a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>A formatting string that specifies the display format for the text of a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.FormatString" /> property is not set.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x0008047C File Offset: 0x0007E67C
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x000804A9 File Offset: 0x0007E6A9
		[Localizable(true)]
		[DefaultValue("")]
		public string FormatString
		{
			get
			{
				object obj = this.ViewState["FormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for the image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The ToolTip text for the image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the P:System.Web.UI.WebControls.TreeNodeBinding.ImageToolTip property is not set.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000804BC File Offset: 0x0007E6BC
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x000804E9 File Offset: 0x0007E6E9
		[Localizable(true)]
		[DefaultValue("")]
		public string ImageToolTip
		{
			get
			{
				object obj = this.ViewState["ImageToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageToolTipField" /> property is not set.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000804FC File Offset: 0x0007E6FC
		// (set) Token: 0x060030A6 RID: 12454 RVA: 0x00080529 File Offset: 0x0007E729
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageToolTipField
		{
			get
			{
				object obj = this.ViewState["ImageToolTipField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageToolTipField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The URL to an image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageUrl" /> property is not set.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x0008053C File Offset: 0x0007E73C
		// (set) Token: 0x060030A8 RID: 12456 RVA: 0x00080569 File Offset: 0x0007E769
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageUrlField" /> property is not set.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x0008057C File Offset: 0x0007E77C
		// (set) Token: 0x060030AA RID: 12458 RVA: 0x000805A9 File Offset: 0x0007E7A9
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ImageUrlField
		{
			get
			{
				object obj = this.ViewState["ImageUrlField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is clicked.</summary>
		/// <returns>The URL to link to when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is clicked. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.NavigateUrl" /> property is not set.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000805BC File Offset: 0x0007E7BC
		// (set) Token: 0x060030AC RID: 12460 RVA: 0x000805E9 File Offset: 0x0007E7E9
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				object obj = this.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.NavigateUrlField" /> property is not set.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000805FC File Offset: 0x0007E7FC
		// (set) Token: 0x060030AE RID: 12462 RVA: 0x00080629 File Offset: 0x0007E829
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string NavigateUrlField
		{
			get
			{
				object obj = this.ViewState["NavigateUrlField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is populated dynamically.</summary>
		/// <returns>true to populate the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied dynamically; otherwise, false. The default is false.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x0008063C File Offset: 0x0007E83C
		// (set) Token: 0x060030B0 RID: 12464 RVA: 0x00080665 File Offset: 0x0007E865
		[DefaultValue(false)]
		public bool PopulateOnDemand
		{
			get
			{
				object obj = this.ViewState["PopulateOnDemand"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PopulateOnDemand"] = value;
			}
		}

		/// <summary>Gets or sets the event or events to raise when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is selected.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeNodeSelectAction" /> values. The default is TreeNodeSelectAction.Select.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x00080680 File Offset: 0x0007E880
		// (set) Token: 0x060030B2 RID: 12466 RVA: 0x000806A9 File Offset: 0x0007E8A9
		[DefaultValue(TreeNodeSelectAction.Select)]
		public TreeNodeSelectAction SelectAction
		{
			get
			{
				object obj = this.ViewState["SelectAction"];
				if (obj != null)
				{
					return (TreeNodeSelectAction)obj;
				}
				return TreeNodeSelectAction.Select;
			}
			set
			{
				this.ViewState["SelectAction"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a check box is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>true to display a check box next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied; otherwise, false. The default is false.</returns>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000806C1 File Offset: 0x0007E8C1
		// (set) Token: 0x060030B4 RID: 12468 RVA: 0x000806D8 File Offset: 0x0007E8D8
		[DefaultValue(false)]
		public bool? ShowCheckBox
		{
			get
			{
				return (bool?)this.ViewState["ShowCheckBox"];
			}
			set
			{
				this.ViewState["ShowCheckBox"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content that is associated with a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Description _blankRenders the content in a new window without frames. _parentRenders the content in the immediate frameset parent. _searchRenders the content in the search pane._selfRenders the content in the frame with focus. _topRenders the content in the full window without frames. NoteCheck your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer version 5.0 and later supports the _search target value.The default is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000806F0 File Offset: 0x0007E8F0
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x0008071D File Offset: 0x0007E91D
		[DefaultValue("")]
		public string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Target" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Target" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.TargetField" /> property is not set.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x00080730 File Offset: 0x0007E930
		// (set) Token: 0x060030B8 RID: 12472 RVA: 0x0008075D File Offset: 0x0007E95D
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string TargetField
		{
			get
			{
				object obj = this.ViewState["TargetField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["TargetField"] = value;
			}
		}

		/// <summary>Gets or sets the text that is displayed for the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The text displayed for the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string ("").</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x00080770 File Offset: 0x0007E970
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x0008079D File Offset: 0x0007E99D
		[DefaultValue("")]
		[WebSysDescription("The display text of the tree node.")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Text" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Text" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.TextField" /> property is not set.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000807B0 File Offset: 0x0007E9B0
		// (set) Token: 0x060030BC RID: 12476 RVA: 0x000807DD File Offset: 0x0007E9DD
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TextField
		{
			get
			{
				object obj = this.ViewState["TextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["TextField"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The ToolTip text for a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ToolTip" /> property is not set.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000807F0 File Offset: 0x0007E9F0
		// (set) Token: 0x060030BE RID: 12478 RVA: 0x0008081D File Offset: 0x0007EA1D
		[DefaultValue("")]
		[Localizable(true)]
		public string ToolTip
		{
			get
			{
				object obj = this.ViewState["ToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ToolTipField" /> property is not set.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x00080830 File Offset: 0x0007EA30
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x0008085D File Offset: 0x0007EA5D
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipField
		{
			get
			{
				object obj = this.ViewState["ToolTipField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ToolTipField"] = value;
			}
		}

		/// <summary>Gets or sets a displayed value that is not displayed but is used to store any additional data about a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied; this data is not displayed. The default is an empty string ("").</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x00080870 File Offset: 0x0007EA70
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x0008089D File Offset: 0x0007EA9D
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000808B0 File Offset: 0x0007EAB0
		internal bool HasPropertyValue(string propName)
		{
			return this.ViewState[propName] != null;
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Value" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Value" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ValueField" /> property is not set.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000808C4 File Offset: 0x0007EAC4
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x000808F1 File Offset: 0x0007EAF1
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ValueField
		{
			get
			{
				object obj = this.ViewState["ValueField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ValueField"] = value;
			}
		}

		/// <summary>Loads the previously saved view state for the node.</summary>
		/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
		// Token: 0x060030C6 RID: 12486 RVA: 0x00080904 File Offset: 0x0007EB04
		void IStateManager.LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the view state changes to an object.</summary>
		/// <returns>The object that contains the view state changes.</returns>
		// Token: 0x060030C7 RID: 12487 RVA: 0x00080912 File Offset: 0x0007EB12
		object IStateManager.SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to track changes to its view state.</summary>
		// Token: 0x060030C8 RID: 12488 RVA: 0x0008091F File Offset: 0x0007EB1F
		void IStateManager.TrackViewState()
		{
			this.ViewState.TrackViewState();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true, if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x0008092C File Offset: 0x0007EB2C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.ViewState.IsTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema" />.</summary>
		/// <returns>An object that represents the schema that is associated with the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060030CB RID: 12491 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</summary>
		/// <returns>An object that represents a copy of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</returns>
		// Token: 0x060030CC RID: 12492 RVA: 0x0008093C File Offset: 0x0007EB3C
		object ICloneable.Clone()
		{
			TreeNodeBinding treeNodeBinding = new TreeNodeBinding();
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				treeNodeBinding.ViewState[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return treeNodeBinding;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000809B4 File Offset: 0x0007EBB4
		internal void SetDirty()
		{
			foreach (object obj in this.ViewState.Keys)
			{
				string text = (string)obj;
				this.ViewState.SetItemDirty(text, true);
			}
		}

		/// <summary>Returns the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property.</summary>
		/// <returns>Returns the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property. If the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property is null or an empty string (""), (Empty) is returned.</returns>
		// Token: 0x060030CE RID: 12494 RVA: 0x00080A18 File Offset: 0x0007EC18
		public override string ToString()
		{
			if (this.DataMember.Length <= 0)
			{
				return "(Empty)";
			}
			return this.DataMember;
		}

		// Token: 0x04001C19 RID: 7193
		private StateBag ViewState = new StateBag();
	}
}
