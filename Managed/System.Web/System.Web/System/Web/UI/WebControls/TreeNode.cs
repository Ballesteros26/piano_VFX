using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x0200042C RID: 1068
	[ParseChildren(true, "ChildNodes")]
	public class TreeNode : IStateManager, ICloneable
	{
		// Token: 0x06003041 RID: 12353 RVA: 0x0007F21E File Offset: 0x0007D41E
		internal TreeNode(TreeView tree)
		{
			this.Tree = tree;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class without text or a value.</summary>
		// Token: 0x06003042 RID: 12354 RVA: 0x0007F23F File Offset: 0x0007D43F
		public TreeNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		// Token: 0x06003043 RID: 12355 RVA: 0x0007F259 File Offset: 0x0007D459
		public TreeNode(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text and value.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
		// Token: 0x06003044 RID: 12356 RVA: 0x0007F27A File Offset: 0x0007D47A
		public TreeNode(string text, string value)
		{
			this.Text = text;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text, value, and image URL.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
		/// <param name="imageUrl">The URL to an image that is displayed next to the node. </param>
		// Token: 0x06003045 RID: 12357 RVA: 0x0007F2A2 File Offset: 0x0007D4A2
		public TreeNode(string text, string value, string imageUrl)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text, value, image URL, navigation URL, and target.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
		/// <param name="imageUrl">The URL to an image that is displayed next to the node. </param>
		/// <param name="navigateUrl">The URL to link to when the node is clicked. </param>
		/// <param name="target">The target window or frame in which to display the Web page content linked to when the node is clicked. </param>
		// Token: 0x06003046 RID: 12358 RVA: 0x0007F2D1 File Offset: 0x0007D4D1
		public TreeNode(string text, string value, string imageUrl, string navigateUrl, string target)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
			this.NavigateUrl = navigateUrl;
			this.Target = target;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified owner.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.TreeView" /> that will contain the new <see cref="T:System.Web.UI.WebControls.TreeNode" />.</param>
		/// <param name="isRoot">true if the <see cref="T:System.Web.UI.WebControls.TreeNode" /> is a root node; otherwise, false.</param>
		// Token: 0x06003047 RID: 12359 RVA: 0x0007F310 File Offset: 0x0007D510
		[global::System.MonoTODO("Not implemented")]
		protected TreeNode(TreeView owner, bool isRoot)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the depth of the node.</summary>
		/// <returns>The depth of the node.</returns>
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x0007F330 File Offset: 0x0007D530
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Depth
		{
			get
			{
				if (this.depth != -1)
				{
					return this.depth;
				}
				this.depth = 0;
				for (TreeNode treeNode = this.parent; treeNode != null; treeNode = treeNode.parent)
				{
					this.depth++;
				}
				return this.depth;
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0007F37C File Offset: 0x0007D57C
		private void ResetPathData()
		{
			this.path = null;
			this.depth = -1;
			this.gotBinding = false;
			if (this.nodes != null)
			{
				foreach (object obj in this.nodes)
				{
					((TreeNode)obj).ResetPathData();
				}
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x0007F3F0 File Offset: 0x0007D5F0
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x0007F3F8 File Offset: 0x0007D5F8
		internal TreeView Tree
		{
			get
			{
				return this.tree;
			}
			set
			{
				if (this.SelectedFlag && value != null)
				{
					value.SetSelectedNode(this, false);
				}
				this.tree = value;
				if (this.nodes != null)
				{
					this.nodes.SetTree(this.tree);
				}
				this.ResetPathData();
				if (this.PopulateOnDemand && !this.Populated && this.Expanded != null && this.Expanded.Value)
				{
					this.Populate();
				}
			}
		}

		/// <summary>Gets a value that indicates whether the node was created through data binding.</summary>
		/// <returns>true if the node was created through data binding; otherwise, false.</returns>
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x0007F474 File Offset: 0x0007D674
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x0007F49F File Offset: 0x0007D69F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool DataBound
		{
			get
			{
				return this.ViewState["DataBound"] != null && (bool)this.ViewState["DataBound"];
			}
			private set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Gets the data item that is bound to the control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the data item that is bound to the control. The default value is null, which indicates that the node is not bound to any data item.</returns>
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0007F4B7 File Offset: 0x0007D6B7
		[DefaultValue(null)]
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				return this.dataItem;
			}
		}

		/// <summary>Gets the path to the data bound to the node.</summary>
		/// <returns>The path to the data bound to the node. This value comes from the hierarchical data source control to which the <see cref="T:System.Web.UI.WebControls.TreeView" /> control is bound. The default value is an empty string ("").</returns>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0007F4BF File Offset: 0x0007D6BF
		// (set) Token: 0x06003050 RID: 12368 RVA: 0x0007F4EE File Offset: 0x0007D6EE
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string DataPath
		{
			get
			{
				if (this.ViewState["DataPath"] != null)
				{
					return (string)this.ViewState["DataPath"];
				}
				return string.Empty;
			}
			private set
			{
				this.ViewState["DataPath"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node's check box is selected.</summary>
		/// <returns>true if the node's check box is selected; otherwise, false. The default is false.</returns>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x0007F504 File Offset: 0x0007D704
		// (set) Token: 0x06003052 RID: 12370 RVA: 0x0007F52D File Offset: 0x0007D72D
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				object obj = this.ViewState["Checked"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Checked"] = value;
				if (this.tree != null)
				{
					this.tree.NotifyCheckChanged(this);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> collection that contains the first-level child nodes of the current node.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the first-level child nodes of the current node.</returns>
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x0007F559 File Offset: 0x0007D759
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public TreeNodeCollection ChildNodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.nodes).TrackViewState();
					}
				}
				return this.nodes;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node is expanded.</summary>
		/// <returns>true if the node is expanded, false if the node is not expanded, or null.</returns>
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0007F588 File Offset: 0x0007D788
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x0007F5A0 File Offset: 0x0007D7A0
		[DefaultValue(null)]
		public bool? Expanded
		{
			get
			{
				return (bool?)this.ViewState["Expanded"];
			}
			set
			{
				if ((bool?)this.ViewState["Expanded"] == value)
				{
					return;
				}
				this.ViewState["Expanded"] = value;
				if (this.tree != null)
				{
					this.tree.NotifyExpandedChanged(this);
				}
				if (this.PopulateOnDemand && !this.Populated && value != null && value.Value)
				{
					this.Populate();
				}
			}
		}

		/// <summary>Gets or sets the ToolTip text for the image displayed next to a node.</summary>
		/// <returns>The ToolTip text for the image displayed next to a node. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0007F640 File Offset: 0x0007D840
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x0007F66D File Offset: 0x0007D86D
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
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the node.</summary>
		/// <returns>The URL to a custom image that is displayed next to the node. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x0007F680 File Offset: 0x0007D880
		// (set) Token: 0x06003059 RID: 12377 RVA: 0x0007F6AD File Offset: 0x0007D8AD
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		public string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to navigate to when the node is clicked.</summary>
		/// <returns>The URL to navigate to when the node is clicked. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x0007F6C0 File Offset: 0x0007D8C0
		// (set) Token: 0x0600305B RID: 12379 RVA: 0x0007F6ED File Offset: 0x0007D8ED
		[DefaultValue("")]
		[UrlProperty]
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
				return string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x0007F700 File Offset: 0x0007D900
		// (set) Token: 0x0600305D RID: 12381 RVA: 0x0007F708 File Offset: 0x0007D908
		internal bool HadChildrenBeforePopulating
		{
			get
			{
				return this.hadChildrenBeforePopulating;
			}
			set
			{
				if (this.populating)
				{
					return;
				}
				this.hadChildrenBeforePopulating = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node is populated dynamically.</summary>
		/// <returns>true to populate the node dynamically; otherwise, false. The default is false.</returns>
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x0007F71C File Offset: 0x0007D91C
		// (set) Token: 0x0600305F RID: 12383 RVA: 0x0007F745 File Offset: 0x0007D945
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
				if (value && this.nodes != null && this.nodes.Count > 0)
				{
					this.HadChildrenBeforePopulating = true;
					return;
				}
				this.HadChildrenBeforePopulating = false;
			}
		}

		/// <summary>Gets or sets the event or events to raise when a node is selected.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeNodeSelectAction" /> values. The default is TreeNodeSelectAction.Select.</returns>
		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0007F788 File Offset: 0x0007D988
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x0007F7B1 File Offset: 0x0007D9B1
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

		/// <summary>Gets or sets a value that indicates whether a check box is displayed next to the node.</summary>
		/// <returns>true to display the check box; otherwise, false.</returns>
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x0007F7C9 File Offset: 0x0007D9C9
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x0007F7E0 File Offset: 0x0007D9E0
		[DefaultValue(null)]
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

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x0007F7F8 File Offset: 0x0007D9F8
		internal bool ShowCheckBoxInternal
		{
			get
			{
				if (this.ShowCheckBox != null)
				{
					return this.ShowCheckBox.Value;
				}
				return this.Tree.ShowCheckBoxes == TreeNodeTypes.All || ((this.Tree.ShowCheckBoxes & TreeNodeTypes.Leaf) > TreeNodeTypes.None && this.IsLeafNode) || ((this.Tree.ShowCheckBoxes & TreeNodeTypes.Parent) > TreeNodeTypes.None && this.IsParentNode && this.Parent != null) || ((this.Tree.ShowCheckBoxes & TreeNodeTypes.Root) > TreeNodeTypes.None && this.Parent == null && this.ChildNodes.Count > 0);
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a node.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case-insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Description _blankRenders the content in a new window without frames. _parentRenders the content in the immediate frameset parent. _searchRenders the content in the search pane._selfRenders the content in the frame with focus. _topRenders the content in the full window without frames. NoteCheck your browser documentation to determine whether the _search value is supported. For example, Microsoft Internet Explorer 5.0 and later support the _search target value.The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x0007F894 File Offset: 0x0007DA94
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x0007F8C1 File Offset: 0x0007DAC1
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
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>The text displayed for the node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is an empty string ("").</returns>
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x0007F8D4 File Offset: 0x0007DAD4
		// (set) Token: 0x06003068 RID: 12392 RVA: 0x0007F915 File Offset: 0x0007DB15
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("The display text of the tree node.")]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					obj = this.ViewState["Value"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for the node.</summary>
		/// <returns>The ToolTip text for the node. The default is an empty string ("").</returns>
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x0007F928 File Offset: 0x0007DB28
		// (set) Token: 0x0600306A RID: 12394 RVA: 0x0007F955 File Offset: 0x0007DB55
		[Localizable(true)]
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				object obj = this.ViewState["ToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets a non-displayed value used to store any additional data about the node, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about the node that is not displayed. The default value is an empty string ("").</returns>
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x0007F968 File Offset: 0x0007DB68
		// (set) Token: 0x0600306C RID: 12396 RVA: 0x0007F9A9 File Offset: 0x0007DBA9
		[Localizable(true)]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj == null)
				{
					obj = this.ViewState["Text"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node is selected.</summary>
		/// <returns>true if the node is selected; otherwise, false. The default is false.</returns>
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x0007F9BC File Offset: 0x0007DBBC
		// (set) Token: 0x0600306E RID: 12398 RVA: 0x0007F9C4 File Offset: 0x0007DBC4
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return this.SelectedFlag;
			}
			set
			{
				this.SelectedFlag = value;
				if (this.tree != null)
				{
					if (!value && this.tree.SelectedNode == this)
					{
						this.tree.SetSelectedNode(null, false);
						return;
					}
					if (value)
					{
						this.tree.SetSelectedNode(this, false);
					}
				}
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x0007FA04 File Offset: 0x0007DC04
		// (set) Token: 0x06003070 RID: 12400 RVA: 0x0007FA2D File Offset: 0x0007DC2D
		internal virtual bool SelectedFlag
		{
			get
			{
				object obj = this.ViewState["Selected"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Selected"] = value;
			}
		}

		/// <summary>Gets the parent node of the current node.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the parent node of the current node.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x0007FA45 File Offset: 0x0007DC45
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNode Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the path from the root node to the current node.</summary>
		/// <returns>A delimiter-separated list of node values that form a path from the root node to the current node.</returns>
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x0007FA50 File Offset: 0x0007DC50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValuePath
		{
			get
			{
				if (this.tree == null)
				{
					return this.Value;
				}
				StringBuilder stringBuilder = new StringBuilder(this.Value);
				for (TreeNode treeNode = this.parent; treeNode != null; treeNode = treeNode.Parent)
				{
					stringBuilder.Insert(0, this.tree.PathSeparator);
					stringBuilder.Insert(0, treeNode.Value);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0007FAB2 File Offset: 0x0007DCB2
		// (set) Token: 0x06003074 RID: 12404 RVA: 0x0007FABA File Offset: 0x0007DCBA
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
				this.ResetPathData();
			}
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x0007FAC9 File Offset: 0x0007DCC9
		internal void SetParent(TreeNode node)
		{
			this.parent = node;
			this.ResetPathData();
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0007FAD8 File Offset: 0x0007DCD8
		internal string Path
		{
			get
			{
				if (this.path != null)
				{
					return this.path;
				}
				StringBuilder stringBuilder = new StringBuilder(this.index.ToString());
				for (TreeNode treeNode = this.parent; treeNode != null; treeNode = treeNode.Parent)
				{
					stringBuilder.Insert(0, '_');
					stringBuilder.Insert(0, treeNode.Index.ToString());
				}
				this.path = stringBuilder.ToString();
				return this.path;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x0007FB4C File Offset: 0x0007DD4C
		// (set) Token: 0x06003078 RID: 12408 RVA: 0x0007FB75 File Offset: 0x0007DD75
		internal bool Populated
		{
			get
			{
				object obj = this.ViewState["Populated"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Populated"] = value;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x0007FB8D File Offset: 0x0007DD8D
		internal bool HasChildData
		{
			get
			{
				return this.nodes != null;
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0007FB98 File Offset: 0x0007DD98
		internal void Populate()
		{
			if (this.tree == null)
			{
				return;
			}
			this.populating = true;
			this.tree.NotifyPopulateRequired(this);
			this.populating = false;
			this.Populated = true;
		}

		/// <summary>Collapses the current tree node.</summary>
		// Token: 0x0600307B RID: 12411 RVA: 0x0007FBC4 File Offset: 0x0007DDC4
		public void Collapse()
		{
			this.Expanded = new bool?(false);
		}

		/// <summary>Collapses the current node and all its child nodes.</summary>
		// Token: 0x0600307C RID: 12412 RVA: 0x0007FBD2 File Offset: 0x0007DDD2
		public void CollapseAll()
		{
			this.SetExpandedRec(false, -1);
		}

		/// <summary>Expands the current tree node.</summary>
		// Token: 0x0600307D RID: 12413 RVA: 0x0007FBDC File Offset: 0x0007DDDC
		public void Expand()
		{
			this.Expanded = new bool?(true);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0007FBEA File Offset: 0x0007DDEA
		internal void Expand(int depth)
		{
			this.SetExpandedRec(true, depth);
		}

		/// <summary>Expands the current node and all its child nodes.</summary>
		// Token: 0x0600307F RID: 12415 RVA: 0x0007FBF4 File Offset: 0x0007DDF4
		public void ExpandAll()
		{
			this.SetExpandedRec(true, -1);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x0007FC00 File Offset: 0x0007DE00
		private void SetExpandedRec(bool expanded, int depth)
		{
			this.Expanded = new bool?(expanded);
			if (depth == 0)
			{
				return;
			}
			foreach (object obj in this.ChildNodes)
			{
				((TreeNode)obj).SetExpandedRec(expanded, depth - 1);
			}
		}

		/// <summary>Selects the current node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x06003081 RID: 12417 RVA: 0x0007FC6C File Offset: 0x0007DE6C
		public void Select()
		{
			this.Selected = true;
		}

		/// <summary>Alternates between the expanded and collapsed state of the node.</summary>
		// Token: 0x06003082 RID: 12418 RVA: 0x0007FC78 File Offset: 0x0007DE78
		public void ToggleExpandState()
		{
			this.Expanded = new bool?(!this.Expanded.GetValueOrDefault(false));
		}

		/// <summary>Loads the node's previously saved view state.</summary>
		/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
		// Token: 0x06003083 RID: 12419 RVA: 0x0007FCA2 File Offset: 0x0007DEA2
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		/// <summary>Loads the previously saved view state of the node. </summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the node.</param>
		// Token: 0x06003084 RID: 12420 RVA: 0x0007FCAC File Offset: 0x0007DEAC
		protected virtual void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			this.ViewState.LoadViewState(array[0]);
			if (this.tree != null && this.SelectedFlag)
			{
				this.tree.SetSelectedNode(this, true);
			}
			if (!this.PopulateOnDemand || this.Populated)
			{
				((IStateManager)this.ChildNodes).LoadViewState(array[1]);
			}
		}

		/// <summary>Saves the view state changes to a <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x06003085 RID: 12421 RVA: 0x0007FD0D File Offset: 0x0007DF0D
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Saves the current view state of the node. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the node. </returns>
		// Token: 0x06003086 RID: 12422 RVA: 0x0007FD18 File Offset: 0x0007DF18
		protected virtual object SaveViewState()
		{
			object[] array = new object[]
			{
				this.ViewState.SaveViewState(),
				(this.nodes == null) ? null : ((IStateManager)this.nodes).SaveViewState()
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to track changes to its view state.</summary>
		// Token: 0x06003087 RID: 12423 RVA: 0x0007FD69 File Offset: 0x0007DF69
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view state changes to the node. </summary>
		// Token: 0x06003088 RID: 12424 RVA: 0x0007FD71 File Offset: 0x0007DF71
		protected void TrackViewState()
		{
			if (this.marked)
			{
				return;
			}
			this.marked = true;
			this.ViewState.TrackViewState();
			if (this.nodes != null)
			{
				((IStateManager)this.nodes).TrackViewState();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>A value that indicates whether the node is saving changes to its view state. </returns>
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x0007FDA1 File Offset: 0x0007DFA1
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Gets a value that indicates whether the node is saving changes to its view state. </summary>
		/// <returns>true if the control is marked to save its state; otherwise, false. </returns>
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x0007FDA9 File Offset: 0x0007DFA9
		protected bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0007FDB1 File Offset: 0x0007DFB1
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.nodes != null)
			{
				this.nodes.SetDirty();
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class with the properties of the current <see cref="T:System.Web.UI.WebControls.TreeNode" /> instance.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.UI.WebControls.TreeNode" /> with the properties of the current <see cref="T:System.Web.UI.WebControls.TreeNode" /> instance.</returns>
		// Token: 0x0600308C RID: 12428 RVA: 0x0007FDD4 File Offset: 0x0007DFD4
		public virtual object Clone()
		{
			TreeNode treeNode = ((this.tree != null) ? this.tree.CreateNode() : new TreeNode());
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				treeNode.ViewState[(string)dictionaryEntry.Key] = ((StateItem)dictionaryEntry.Value).Value;
			}
			foreach (object obj2 in this.ChildNodes)
			{
				TreeNode treeNode2 = (TreeNode)obj2;
				treeNode.ChildNodes.Add((TreeNode)treeNode2.Clone());
			}
			return treeNode;
		}

		/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</returns>
		// Token: 0x0600308D RID: 12429 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0007FED0 File Offset: 0x0007E0D0
		internal void Bind(IHierarchyData hierarchyData)
		{
			this.hierarchyData = hierarchyData;
			this.DataBound = true;
			this.DataPath = hierarchyData.Path;
			this.dataItem = hierarchyData.Item;
			TreeNodeBinding treeNodeBinding = this.GetBinding();
			if (treeNodeBinding != null)
			{
				if (treeNodeBinding.ImageToolTipField.Length > 0)
				{
					this.ImageToolTip = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ImageToolTipField));
					if (this.ImageToolTip.Length == 0)
					{
						this.ImageToolTip = treeNodeBinding.ImageToolTip;
					}
				}
				else if (treeNodeBinding.ImageToolTip.Length > 0)
				{
					this.ImageToolTip = treeNodeBinding.ImageToolTip;
				}
				if (treeNodeBinding.ImageUrlField.Length > 0)
				{
					this.ImageUrl = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ImageUrlField));
					if (this.ImageUrl.Length == 0)
					{
						this.ImageUrl = treeNodeBinding.ImageUrl;
					}
				}
				else if (treeNodeBinding.ImageUrl.Length > 0)
				{
					this.ImageUrl = treeNodeBinding.ImageUrl;
				}
				if (treeNodeBinding.NavigateUrlField.Length > 0)
				{
					this.NavigateUrl = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.NavigateUrlField));
					if (this.NavigateUrl.Length == 0)
					{
						this.NavigateUrl = treeNodeBinding.NavigateUrl;
					}
				}
				else if (treeNodeBinding.NavigateUrl.Length > 0)
				{
					this.NavigateUrl = treeNodeBinding.NavigateUrl;
				}
				if (treeNodeBinding.HasPropertyValue("PopulateOnDemand"))
				{
					this.PopulateOnDemand = treeNodeBinding.PopulateOnDemand;
				}
				if (treeNodeBinding.HasPropertyValue("SelectAction"))
				{
					this.SelectAction = treeNodeBinding.SelectAction;
				}
				if (treeNodeBinding.HasPropertyValue("ShowCheckBox"))
				{
					this.ShowCheckBox = treeNodeBinding.ShowCheckBox;
				}
				if (treeNodeBinding.TargetField.Length > 0)
				{
					this.Target = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.TargetField));
					if (this.Target.Length == 0)
					{
						this.Target = treeNodeBinding.Target;
					}
				}
				else if (treeNodeBinding.Target.Length > 0)
				{
					this.Target = treeNodeBinding.Target;
				}
				string text = null;
				if (treeNodeBinding.TextField.Length > 0)
				{
					text = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.TextField));
					if (treeNodeBinding.FormatString.Length > 0)
					{
						text = string.Format(treeNodeBinding.FormatString, text);
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					if (treeNodeBinding.Text.Length > 0)
					{
						text = treeNodeBinding.Text;
					}
					else if (treeNodeBinding.Value.Length > 0)
					{
						text = treeNodeBinding.Value;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.Text = text;
				}
				if (treeNodeBinding.ToolTipField.Length > 0)
				{
					this.ToolTip = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ToolTipField));
					if (this.ToolTip.Length == 0)
					{
						this.ToolTip = treeNodeBinding.ToolTip;
					}
				}
				else if (treeNodeBinding.ToolTip.Length > 0)
				{
					this.ToolTip = treeNodeBinding.ToolTip;
				}
				string text2 = null;
				if (treeNodeBinding.ValueField.Length > 0)
				{
					text2 = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ValueField));
				}
				if (string.IsNullOrEmpty(text2))
				{
					if (treeNodeBinding.Value.Length > 0)
					{
						text2 = treeNodeBinding.Value;
					}
					else if (treeNodeBinding.Text.Length > 0)
					{
						text2 = treeNodeBinding.Text;
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.Value = text2;
				}
			}
			else
			{
				this.Text = (this.Value = this.GetDefaultBoundText());
			}
			INavigateUIData navigateUIData = hierarchyData as INavigateUIData;
			if (navigateUIData != null)
			{
				this.SelectAction = TreeNodeSelectAction.None;
				this.Text = navigateUIData.ToString();
				this.NavigateUrl = navigateUIData.NavigateUrl;
				this.ToolTip = navigateUIData.Description;
			}
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x00080250 File Offset: 0x0007E450
		internal void SetDataItem(object item)
		{
			this.dataItem = item;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x00080259 File Offset: 0x0007E459
		internal void SetDataPath(string path)
		{
			this.DataPath = path;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x00080262 File Offset: 0x0007E462
		internal void SetDataBound(bool bound)
		{
			this.DataBound = bound;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0008026B File Offset: 0x0007E46B
		private string GetDefaultBoundText()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.ToString();
			}
			if (this.dataItem != null)
			{
				return this.dataItem.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0008029A File Offset: 0x0007E49A
		private string GetDataItemType()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.Type;
			}
			if (this.dataItem != null)
			{
				return this.dataItem.GetType().ToString();
			}
			return string.Empty;
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000802CE File Offset: 0x0007E4CE
		internal bool IsParentNode
		{
			get
			{
				return this.ChildNodes.Count > 0 || (this.PopulateOnDemand && !this.Populated);
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000802F3 File Offset: 0x0007E4F3
		internal bool IsLeafNode
		{
			get
			{
				return !this.IsParentNode;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000802FE File Offset: 0x0007E4FE
		internal bool IsRootNode
		{
			get
			{
				return this.Depth == 0;
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0008030C File Offset: 0x0007E50C
		private TreeNodeBinding GetBinding()
		{
			if (this.tree == null)
			{
				return null;
			}
			if (this.gotBinding)
			{
				return this.binding;
			}
			this.binding = this.tree.FindBindingForNode(this.GetDataItemType(), this.Depth);
			this.gotBinding = true;
			return this.binding;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0008035C File Offset: 0x0007E55C
		private object GetBoundPropertyValue(string name)
		{
			if (this.boundProperties == null)
			{
				if (this.hierarchyData != null)
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.hierarchyData);
				}
				else
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.dataItem);
				}
			}
			PropertyDescriptor propertyDescriptor = this.boundProperties.Find(name, true);
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
			}
			if (this.hierarchyData != null)
			{
				return propertyDescriptor.GetValue(this.hierarchyData);
			}
			return propertyDescriptor.GetValue(this.dataItem);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000803E5 File Offset: 0x0007E5E5
		internal void BeginRenderText(HtmlTextWriter writer)
		{
			this.RenderPreText(writer);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000803EE File Offset: 0x0007E5EE
		internal void EndRenderText(HtmlTextWriter writer)
		{
			this.RenderPostText(writer);
		}

		/// <summary>Allows control developers to add additional rendering to the node.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
		// Token: 0x0600309B RID: 12443 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void RenderPreText(HtmlTextWriter writer)
		{
		}

		/// <summary>Allows control developers to add additional rendering to the node.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
		// Token: 0x0600309C RID: 12444 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void RenderPostText(HtmlTextWriter writer)
		{
		}

		// Token: 0x04001C0A RID: 7178
		private StateBag ViewState = new StateBag();

		// Token: 0x04001C0B RID: 7179
		private TreeNodeCollection nodes;

		// Token: 0x04001C0C RID: 7180
		private bool marked;

		// Token: 0x04001C0D RID: 7181
		private TreeView tree;

		// Token: 0x04001C0E RID: 7182
		private TreeNode parent;

		// Token: 0x04001C0F RID: 7183
		private int index;

		// Token: 0x04001C10 RID: 7184
		private string path;

		// Token: 0x04001C11 RID: 7185
		private int depth = -1;

		// Token: 0x04001C12 RID: 7186
		private object dataItem;

		// Token: 0x04001C13 RID: 7187
		private IHierarchyData hierarchyData;

		// Token: 0x04001C14 RID: 7188
		private bool gotBinding;

		// Token: 0x04001C15 RID: 7189
		private TreeNodeBinding binding;

		// Token: 0x04001C16 RID: 7190
		private PropertyDescriptorCollection boundProperties;

		// Token: 0x04001C17 RID: 7191
		private bool populating;

		// Token: 0x04001C18 RID: 7192
		private bool hadChildrenBeforePopulating;
	}
}
