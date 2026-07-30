using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a node of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200038A RID: 906
	[TypeConverter(typeof(TreeNodeConverter))]
	[DefaultProperty("Text")]
	[Serializable]
	public class TreeNode : MarshalByRefObject, ISerializable, ICloneable
	{
		// Token: 0x060041E1 RID: 16865 RVA: 0x00104190 File Offset: 0x00102390
		internal TreeNode(TreeView tree_view)
			: this()
		{
			this.tree_view = tree_view;
			this.is_expanded = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class using the specified serialization information and context.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the data to deserialize the class.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source and destination of the serialized stream.</param>
		// Token: 0x060041E2 RID: 16866 RVA: 0x001041A8 File Offset: 0x001023A8
		protected TreeNode(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				SerializationEntry serializationEntry = enumerator.Current;
				string text = serializationEntry.Name;
				if (text != null)
				{
					if (TreeNode.<>f__switch$map5 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
						dictionary.Add("Text", 0);
						dictionary.Add("PropBag", 1);
						dictionary.Add("ImageIndex", 2);
						dictionary.Add("SelectedImageIndex", 3);
						dictionary.Add("Tag", 4);
						dictionary.Add("IsChecked", 5);
						dictionary.Add("ChildCount", 6);
						TreeNode.<>f__switch$map5 = dictionary;
					}
					int num2;
					if (TreeNode.<>f__switch$map5.TryGetValue(text, ref num2))
					{
						switch (num2)
						{
						case 0:
							this.Text = (string)serializationEntry.Value;
							break;
						case 1:
							this.prop_bag = (OwnerDrawPropertyBag)serializationEntry.Value;
							break;
						case 2:
							this.image_index = (int)serializationEntry.Value;
							break;
						case 3:
							this.selected_image_index = (int)serializationEntry.Value;
							break;
						case 4:
							this.tag = serializationEntry.Value;
							break;
						case 5:
							this.check = (bool)serializationEntry.Value;
							break;
						case 6:
							num = (int)serializationEntry.Value;
							break;
						}
					}
				}
			}
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					TreeNode treeNode = (TreeNode)serializationInfo.GetValue("children" + i, typeof(TreeNode));
					this.Nodes.Add(treeNode);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class.</summary>
		// Token: 0x060041E3 RID: 16867 RVA: 0x00104380 File Offset: 0x00102580
		public TreeNode()
		{
			this.nodes = new TreeNodeCollection(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
		// Token: 0x060041E4 RID: 16868 RVA: 0x001043F4 File Offset: 0x001025F4
		public TreeNode(string text)
			: this()
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text and child tree nodes.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
		/// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects. </param>
		// Token: 0x060041E5 RID: 16869 RVA: 0x00104404 File Offset: 0x00102604
		public TreeNode(string text, TreeNode[] children)
			: this(text)
		{
			this.Nodes.AddRange(children);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text and images to display when the tree node is in a selected and unselected state.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
		/// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected. </param>
		/// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected. </param>
		// Token: 0x060041E6 RID: 16870 RVA: 0x0010441C File Offset: 0x0010261C
		public TreeNode(string text, int imageIndex, int selectedImageIndex)
			: this(text)
		{
			this.image_index = imageIndex;
			this.selected_image_index = selectedImageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text, child tree nodes, and images to display when the tree node is in a selected and unselected state.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
		/// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected. </param>
		/// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected. </param>
		/// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects. </param>
		// Token: 0x060041E7 RID: 16871 RVA: 0x00104434 File Offset: 0x00102634
		public TreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
			: this(text, imageIndex, selectedImageIndex)
		{
			this.Nodes.AddRange(children);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination information for this serialization.</param>
		// Token: 0x060041E8 RID: 16872 RVA: 0x0010444C File Offset: 0x0010264C
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Text", this.Text);
			si.AddValue("prop_bag", this.prop_bag, typeof(OwnerDrawPropertyBag));
			si.AddValue("ImageIndex", this.ImageIndex);
			si.AddValue("SelectedImageIndex", this.SelectedImageIndex);
			si.AddValue("Tag", this.Tag);
			si.AddValue("Checked", this.Checked);
			si.AddValue("NumberOfChildren", this.Nodes.Count);
			for (int i = 0; i < this.Nodes.Count; i++)
			{
				si.AddValue("Child-" + i, this.Nodes[i], typeof(TreeNode));
			}
		}

		/// <summary>Copies the tree node and the entire subtree rooted at this tree node.</summary>
		/// <returns>The <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041E9 RID: 16873 RVA: 0x00104528 File Offset: 0x00102728
		public virtual object Clone()
		{
			TreeNode treeNode = new TreeNode(this.text, this.image_index, this.selected_image_index);
			if (this.nodes != null)
			{
				foreach (object obj in this.nodes)
				{
					TreeNode treeNode2 = (TreeNode)obj;
					treeNode.Nodes.Add((TreeNode)treeNode2.Clone());
				}
			}
			treeNode.Tag = this.tag;
			treeNode.Checked = this.Checked;
			if (this.prop_bag != null)
			{
				treeNode.prop_bag = OwnerDrawPropertyBag.Copy(this.prop_bag);
			}
			return treeNode;
		}

		/// <summary>Loads the state of the <see cref="T:System.Windows.Forms.TreeNode" /> from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the state of the stream during deserialization.</param>
		// Token: 0x060041EA RID: 16874 RVA: 0x00104600 File Offset: 0x00102800
		protected virtual void Deserialize(SerializationInfo serializationInfo, StreamingContext context)
		{
			this.Text = serializationInfo.GetString("Text");
			this.prop_bag = (OwnerDrawPropertyBag)serializationInfo.GetValue("prop_bag", typeof(OwnerDrawPropertyBag));
			this.ImageIndex = serializationInfo.GetInt32("ImageIndex");
			this.SelectedImageIndex = serializationInfo.GetInt32("SelectedImageIndex");
			this.Tag = serializationInfo.GetValue("Tag", typeof(object));
			this.Checked = serializationInfo.GetBoolean("Checked");
			int @int = serializationInfo.GetInt32("NumberOfChildren");
			for (int i = 0; i < @int; i++)
			{
				this.Nodes.Add((TreeNode)serializationInfo.GetValue("Child-" + i, typeof(TreeNode)));
			}
		}

		/// <summary>Saves the state of the <see cref="T:System.Windows.Forms.TreeNode" /> to the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />. </summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the state of the stream during serialization</param>
		// Token: 0x060041EB RID: 16875 RVA: 0x001046DC File Offset: 0x001028DC
		protected virtual void Serialize(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Text", this.Text);
			si.AddValue("prop_bag", this.prop_bag, typeof(OwnerDrawPropertyBag));
			si.AddValue("ImageIndex", this.ImageIndex);
			si.AddValue("SelectedImageIndex", this.SelectedImageIndex);
			si.AddValue("Tag", this.Tag);
			si.AddValue("Checked", this.Checked);
			si.AddValue("NumberOfChildren", this.Nodes.Count);
			for (int i = 0; i < this.Nodes.Count; i++)
			{
				si.AddValue("Child-" + i, this.Nodes[i], typeof(TreeNode));
			}
		}

		/// <summary>Gets or sets the background color of the tree node.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the tree node. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x001047B8 File Offset: 0x001029B8
		// (set) Token: 0x060041ED RID: 16877 RVA: 0x001047D8 File Offset: 0x001029D8
		public Color BackColor
		{
			get
			{
				if (this.prop_bag != null)
				{
					return this.prop_bag.BackColor;
				}
				return Color.Empty;
			}
			set
			{
				if (this.prop_bag == null)
				{
					this.prop_bag = new OwnerDrawPropertyBag();
				}
				this.prop_bag.BackColor = value;
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.UpdateNode(this);
				}
			}
		}

		/// <summary>Gets the bounds of the tree node.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x060041EE RID: 16878 RVA: 0x0010481C File Offset: 0x00102A1C
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				if (this.TreeView == null)
				{
					return Rectangle.Empty;
				}
				int x = this.GetX();
				int y = this.GetY();
				if (this.width == -1)
				{
					this.width = this.TreeView.GetNodeWidth(this);
				}
				Rectangle rectangle;
				rectangle..ctor(x, y, this.width, this.TreeView.ActualItemHeight);
				return rectangle;
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00104884 File Offset: 0x00102A84
		internal int GetY()
		{
			if (this.TreeView == null)
			{
				return 0;
			}
			return (this.visible_order - 1) * this.TreeView.ActualItemHeight - this.TreeView.skipped_nodes * this.TreeView.ActualItemHeight;
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x001048CC File Offset: 0x00102ACC
		internal int GetX()
		{
			if (this.TreeView == null)
			{
				return 0;
			}
			int indentLevel = this.IndentLevel;
			int num = ((!this.TreeView.ShowRootLines) ? 0 : 1);
			int num2 = ((!this.TreeView.CheckBoxes) ? 0 : 19);
			if (!this.TreeView.CheckBoxes && this.StateImage != null)
			{
				num2 = 19;
			}
			int num3 = ((this.TreeView.ImageList == null) ? 0 : (this.TreeView.ImageList.ImageSize.Width + 3));
			return (indentLevel + num) * this.TreeView.Indent + num2 + num3 - this.TreeView.hbar_offset;
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00104990 File Offset: 0x00102B90
		internal int GetLinesX()
		{
			int num = ((!this.TreeView.ShowRootLines) ? 0 : 1);
			return (this.IndentLevel + num) * this.TreeView.Indent - this.TreeView.hbar_offset;
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x001049D8 File Offset: 0x00102BD8
		internal int GetImageX()
		{
			return this.GetLinesX() + ((!this.TreeView.CheckBoxes && this.StateImage == null) ? 0 : 19);
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x00104A10 File Offset: 0x00102C10
		internal int IndentLevel
		{
			get
			{
				TreeNode treeNode = this;
				int num = 0;
				while (treeNode.Parent != null)
				{
					treeNode = treeNode.Parent;
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree node is in a checked state.</summary>
		/// <returns>true if the tree node is in a checked state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x00104A40 File Offset: 0x00102C40
		// (set) Token: 0x060041F5 RID: 16885 RVA: 0x00104A48 File Offset: 0x00102C48
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.check;
			}
			set
			{
				if (this.check == value)
				{
					return;
				}
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, this.check_reason);
				if (this.TreeView != null)
				{
					this.TreeView.OnBeforeCheck(treeViewCancelEventArgs);
				}
				if (!treeViewCancelEventArgs.Cancel)
				{
					this.check = value;
					if (this.TreeView != null)
					{
						this.TreeView.OnAfterCheck(new TreeViewEventArgs(this, this.check_reason));
					}
					if (this.TreeView != null)
					{
						this.TreeView.UpdateNode(this);
					}
				}
				this.check_reason = TreeViewAction.Unknown;
			}
		}

		/// <summary>Gets the shortcut menu associated with this tree node.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> associated with the tree node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x00104ADC File Offset: 0x00102CDC
		// (set) Token: 0x060041F7 RID: 16887 RVA: 0x00104AE4 File Offset: 0x00102CE4
		[DefaultValue(null)]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return this.context_menu;
			}
			set
			{
				this.context_menu = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with this tree node.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the tree node.</returns>
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x00104AF0 File Offset: 0x00102CF0
		// (set) Token: 0x060041F9 RID: 16889 RVA: 0x00104AF8 File Offset: 0x00102CF8
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.context_menu_strip;
			}
			set
			{
				this.context_menu_strip = value;
			}
		}

		/// <summary>Gets the first child tree node in the tree node collection.</summary>
		/// <returns>The first child <see cref="T:System.Windows.Forms.TreeNode" /> in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x00104B04 File Offset: 0x00102D04
		[Browsable(false)]
		public TreeNode FirstNode
		{
			get
			{
				if (this.nodes.Count > 0)
				{
					return this.nodes[0];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the foreground color of the tree node.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x00104B28 File Offset: 0x00102D28
		// (set) Token: 0x060041FC RID: 16892 RVA: 0x00104B68 File Offset: 0x00102D68
		public Color ForeColor
		{
			get
			{
				if (this.prop_bag != null)
				{
					return this.prop_bag.ForeColor;
				}
				if (this.TreeView != null)
				{
					return this.TreeView.ForeColor;
				}
				return Color.Empty;
			}
			set
			{
				if (this.prop_bag == null)
				{
					this.prop_bag = new OwnerDrawPropertyBag();
				}
				this.prop_bag.ForeColor = value;
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.UpdateNode(this);
				}
			}
		}

		/// <summary>Gets the path from the root tree node to the current tree node.</summary>
		/// <returns>The path from the root tree node to the current tree node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is not contained in a <see cref="T:System.Windows.Forms.TreeView" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x00104BAC File Offset: 0x00102DAC
		[Browsable(false)]
		public string FullPath
		{
			get
			{
				if (this.TreeView == null)
				{
					throw new InvalidOperationException("No TreeView associated");
				}
				StringBuilder stringBuilder = new StringBuilder();
				this.BuildFullPath(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets the image list index value of the image displayed when the tree node is in the unselected state.</summary>
		/// <returns>A zero-based index value that represents the image position in the assigned <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x00104BE4 File Offset: 0x00102DE4
		// (set) Token: 0x060041FF RID: 16895 RVA: 0x00104BEC File Offset: 0x00102DEC
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[RelatedImageList("TreeView.ImageList")]
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (this.image_index == value)
				{
					return;
				}
				this.image_index = value;
				this.image_key = string.Empty;
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.UpdateNode(this);
				}
			}
		}

		/// <summary>Gets or sets the key for the image associated with this tree node when the node is in an unselected state.</summary>
		/// <returns>The key for the image associated with this tree node when the node is in an unselected state.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x00104C2C File Offset: 0x00102E2C
		// (set) Token: 0x06004201 RID: 16897 RVA: 0x00104C34 File Offset: 0x00102E34
		[RelatedImageList("TreeView.ImageList")]
		[TypeConverter(typeof(TreeViewImageKeyConverter))]
		[RefreshProperties(2)]
		[DefaultValue("")]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				if (this.image_key == value)
				{
					return;
				}
				this.image_key = value;
				this.image_index = -1;
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.UpdateNode(this);
				}
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in an editable state.</summary>
		/// <returns>true if the tree node is in editable state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x00104C78 File Offset: 0x00102E78
		[Browsable(false)]
		public bool IsEditing
		{
			get
			{
				TreeView treeView = this.TreeView;
				return treeView != null && treeView.edit_node == this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in the expanded state.</summary>
		/// <returns>true if the tree node is in the expanded state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x00104CA0 File Offset: 0x00102EA0
		[Browsable(false)]
		public bool IsExpanded
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null && treeView.IsHandleCreated)
				{
					bool flag = false;
					foreach (object obj in this.TreeView.Nodes)
					{
						TreeNode treeNode = (TreeNode)obj;
						if (treeNode.Nodes.Count > 0)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return this.is_expanded;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in the selected state.</summary>
		/// <returns>true if the tree node is in the selected state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x00104D4C File Offset: 0x00102F4C
		[Browsable(false)]
		public bool IsSelected
		{
			get
			{
				return this.TreeView != null && this.TreeView.IsHandleCreated && this.TreeView.SelectedNode == this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is visible or partially visible.</summary>
		/// <returns>true if the tree node is visible or partially visible; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x00104D84 File Offset: 0x00102F84
		[Browsable(false)]
		public bool IsVisible
		{
			get
			{
				return this.TreeView != null && this.TreeView.IsHandleCreated && this.TreeView.Visible && this.visible_order > this.TreeView.skipped_nodes && this.visible_order - this.TreeView.skipped_nodes <= this.TreeView.VisibleCount && this.ArePreviousNodesExpanded;
			}
		}

		/// <summary>Gets the last child tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the last child tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x00104E00 File Offset: 0x00103000
		[Browsable(false)]
		public TreeNode LastNode
		{
			get
			{
				return (this.nodes != null && this.nodes.Count != 0) ? this.nodes[this.nodes.Count - 1] : null;
			}
		}

		/// <summary>Gets the zero-based depth of the tree node in the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The zero-based depth of the tree node in the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x00104E48 File Offset: 0x00103048
		[Browsable(false)]
		public int Level
		{
			get
			{
				return this.IndentLevel;
			}
		}

		/// <summary>Gets or sets the name of the tree node.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x00104E50 File Offset: 0x00103050
		// (set) Token: 0x06004209 RID: 16905 RVA: 0x00104E58 File Offset: 0x00103058
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets the next sibling tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next sibling tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x00104E74 File Offset: 0x00103074
		[Browsable(false)]
		public TreeNode NextNode
		{
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				int index = this.Index;
				if (this.parent.Nodes.Count > index + 1)
				{
					return this.parent.Nodes[index + 1];
				}
				return null;
			}
		}

		/// <summary>Gets the next visible tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next visible tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x00104EC4 File Offset: 0x001030C4
		[Browsable(false)]
		public TreeNode NextVisibleNode
		{
			get
			{
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
				openTreeNodeEnumerator.MoveNext();
				if (!openTreeNodeEnumerator.MoveNext())
				{
					return null;
				}
				TreeNode currentNode = openTreeNodeEnumerator.CurrentNode;
				if (!currentNode.IsInClippingRect)
				{
					return null;
				}
				return currentNode;
			}
		}

		/// <summary>Gets or sets the font used to display the text on the tree node's label.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> used to display the text on the tree node's label.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x00104F04 File Offset: 0x00103104
		// (set) Token: 0x0600420D RID: 16909 RVA: 0x00104F40 File Offset: 0x00103140
		[DefaultValue(null)]
		[Localizable(true)]
		public Font NodeFont
		{
			get
			{
				if (this.prop_bag != null)
				{
					return this.prop_bag.Font;
				}
				if (this.TreeView != null)
				{
					return this.TreeView.Font;
				}
				return null;
			}
			set
			{
				if (this.prop_bag == null)
				{
					this.prop_bag = new OwnerDrawPropertyBag();
				}
				this.prop_bag.Font = value;
				this.Invalidate();
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects assigned to the current tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the current tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x00104F78 File Offset: 0x00103178
		[ListBindable(false)]
		[Browsable(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
				}
				return this.nodes;
			}
		}

		/// <summary>Gets the parent tree node of the current tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the parent of the current tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x00104F98 File Offset: 0x00103198
		[Browsable(false)]
		public TreeNode Parent
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null && treeView.root_node == this.parent)
				{
					return null;
				}
				return this.parent;
			}
		}

		/// <summary>Gets the previous sibling tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous sibling tree node.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x00104FCC File Offset: 0x001031CC
		[Browsable(false)]
		public TreeNode PrevNode
		{
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				int index = this.Index;
				if (index <= 0 || index > this.parent.Nodes.Count)
				{
					return null;
				}
				return this.parent.Nodes[index - 1];
			}
		}

		/// <summary>Gets the previous visible tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous visible tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x00105020 File Offset: 0x00103220
		[Browsable(false)]
		public TreeNode PrevVisibleNode
		{
			get
			{
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
				openTreeNodeEnumerator.MovePrevious();
				if (!openTreeNodeEnumerator.MovePrevious())
				{
					return null;
				}
				TreeNode currentNode = openTreeNodeEnumerator.CurrentNode;
				if (!currentNode.IsInClippingRect)
				{
					return null;
				}
				return currentNode;
			}
		}

		/// <summary>Gets or sets the image list index value of the image that is displayed when the tree node is in the selected state.</summary>
		/// <returns>A zero-based index value that represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x00105060 File Offset: 0x00103260
		// (set) Token: 0x06004213 RID: 16915 RVA: 0x00105068 File Offset: 0x00103268
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		[RelatedImageList("TreeView.ImageList")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[RefreshProperties(2)]
		[Localizable(true)]
		public int SelectedImageIndex
		{
			get
			{
				return this.selected_image_index;
			}
			set
			{
				this.selected_image_index = value;
			}
		}

		/// <summary>Gets or sets the key of the image displayed in the tree node when it is in a selected state.</summary>
		/// <returns>The key of the image displayed when the tree node is in a selected state.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x00105074 File Offset: 0x00103274
		// (set) Token: 0x06004215 RID: 16917 RVA: 0x0010507C File Offset: 0x0010327C
		[Localizable(true)]
		[TypeConverter(typeof(TreeViewImageKeyConverter))]
		[RelatedImageList("TreeView.ImageList")]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		public string SelectedImageKey
		{
			get
			{
				return this.selected_image_key;
			}
			set
			{
				this.selected_image_key = value;
			}
		}

		/// <summary>Gets or sets the index of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" /> when the parent <see cref="T:System.Windows.Forms.TreeView" /> has its <see cref="P:System.Windows.Forms.TreeView.CheckBoxes" /> property set to false.</summary>
		/// <returns>The index of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1 or greater than 14.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x00105088 File Offset: 0x00103288
		// (set) Token: 0x06004217 RID: 16919 RVA: 0x00105090 File Offset: 0x00103290
		[RelatedImageList("TreeView.StateImageList")]
		[DefaultValue(-1)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		public int StateImageIndex
		{
			get
			{
				return this.state_image_index;
			}
			set
			{
				if (this.state_image_index != value)
				{
					this.state_image_index = value;
					this.state_image_key = string.Empty;
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" /> when the parent <see cref="T:System.Windows.Forms.TreeView" /> has its <see cref="P:System.Windows.Forms.TreeView.CheckBoxes" /> property set to false.</summary>
		/// <returns>The key of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x001050C4 File Offset: 0x001032C4
		// (set) Token: 0x06004219 RID: 16921 RVA: 0x001050CC File Offset: 0x001032CC
		[RelatedImageList("TreeView.StateImageList")]
		[DefaultValue("")]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string StateImageKey
		{
			get
			{
				return this.state_image_key;
			}
			set
			{
				if (this.state_image_key != value)
				{
					this.state_image_key = value;
					this.state_image_index = -1;
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the object that contains data about the tree node.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the tree node. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x001050F4 File Offset: 0x001032F4
		// (set) Token: 0x0600421B RID: 16923 RVA: 0x001050FC File Offset: 0x001032FC
		[DefaultValue(null)]
		[Localizable(false)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the label of the tree node.</summary>
		/// <returns>The text displayed in the label of the tree node.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x00105108 File Offset: 0x00103308
		// (set) Token: 0x0600421D RID: 16925 RVA: 0x00105124 File Offset: 0x00103324
		[Localizable(true)]
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return string.Empty;
				}
				return this.text;
			}
			set
			{
				if (this.text == value)
				{
					return;
				}
				this.text = value;
				this.Invalidate();
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					treeView.OnUIANodeTextChanged(new TreeViewEventArgs(this));
				}
			}
		}

		/// <summary>Gets or sets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>Gets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x0010516C File Offset: 0x0010336C
		// (set) Token: 0x0600421F RID: 16927 RVA: 0x00105174 File Offset: 0x00103374
		[Localizable(false)]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.tool_tip_text;
			}
			set
			{
				this.tool_tip_text = value;
			}
		}

		/// <summary>Gets the parent tree view that the tree node is assigned to.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeView" /> that represents the parent tree view that the tree node is assigned to, or null if the node has not been assigned to a tree view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x00105180 File Offset: 0x00103380
		[Browsable(false)]
		public TreeView TreeView
		{
			get
			{
				if (this.tree_view != null)
				{
					return this.tree_view;
				}
				TreeNode treeNode;
				for (treeNode = this.parent; treeNode != null; treeNode = treeNode.parent)
				{
					if (treeNode.TreeView != null)
					{
						break;
					}
				}
				if (treeNode == null)
				{
					return null;
				}
				return treeNode.TreeView;
			}
		}

		/// <summary>Gets the handle of the tree node.</summary>
		/// <returns>The tree node handle.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x001051D8 File Offset: 0x001033D8
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero && this.TreeView != null)
				{
					this.handle = this.TreeView.CreateNodeHandle();
				}
				return this.handle;
			}
		}

		/// <summary>Returns the tree node with the specified handle and assigned to the specified tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node assigned to the specified <see cref="T:System.Windows.Forms.TreeView" /> control with the specified handle.</returns>
		/// <param name="tree">The <see cref="T:System.Windows.Forms.TreeView" /> that contains the tree node. </param>
		/// <param name="handle">The handle of the tree node. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004222 RID: 16930 RVA: 0x0010521C File Offset: 0x0010341C
		public static TreeNode FromHandle(TreeView tree, IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			return tree.NodeFromHandle(handle);
		}

		/// <summary>Initiates the editing of the tree node label.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.TreeView.LabelEdit" /> is set to false. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004223 RID: 16931 RVA: 0x00105238 File Offset: 0x00103438
		public void BeginEdit()
		{
			TreeView treeView = this.TreeView;
			if (treeView != null)
			{
				treeView.BeginEdit(this);
			}
		}

		/// <summary>Collapses the tree node.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004224 RID: 16932 RVA: 0x0010525C File Offset: 0x0010345C
		public void Collapse()
		{
			this.CollapseInternal(false);
		}

		/// <summary>Collapses the <see cref="T:System.Windows.Forms.TreeNode" /> and optionally collapses its children.</summary>
		/// <param name="ignoreChildren">true to leave the child nodes in their current state; false to collapse the child nodes.</param>
		// Token: 0x06004225 RID: 16933 RVA: 0x00105268 File Offset: 0x00103468
		public void Collapse(bool ignoreChildren)
		{
			if (ignoreChildren)
			{
				this.Collapse();
			}
			else
			{
				this.CollapseRecursive(this);
			}
		}

		/// <summary>Ends the editing of the tree node label.</summary>
		/// <param name="cancel">true if the editing of the tree node label text was canceled without being saved; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004226 RID: 16934 RVA: 0x00105284 File Offset: 0x00103484
		public void EndEdit(bool cancel)
		{
			TreeView treeView = this.TreeView;
			if (!cancel && treeView != null)
			{
				treeView.EndEdit(this);
			}
			else if (cancel && treeView != null)
			{
				treeView.CancelEdit(this);
			}
		}

		/// <summary>Expands the tree node.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004227 RID: 16935 RVA: 0x001052C4 File Offset: 0x001034C4
		public void Expand()
		{
			this.Expand(false);
		}

		/// <summary>Expands all the child tree nodes.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004228 RID: 16936 RVA: 0x001052D0 File Offset: 0x001034D0
		public void ExpandAll()
		{
			this.ExpandRecursive(this);
			if (this.TreeView != null)
			{
				this.TreeView.UpdateNode(this.TreeView.root_node);
			}
		}

		/// <summary>Ensures that the tree node is visible, expanding tree nodes and scrolling the tree view control as necessary.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004229 RID: 16937 RVA: 0x00105308 File Offset: 0x00103508
		public void EnsureVisible()
		{
			if (this.TreeView == null)
			{
				return;
			}
			if (this.Parent != null)
			{
				this.ExpandParentRecursive(this.Parent);
			}
			Rectangle bounds = this.Bounds;
			if (bounds.Y < 0)
			{
				this.TreeView.SetTop(this);
			}
			else if (bounds.Bottom > this.TreeView.ViewportRectangle.Bottom)
			{
				this.TreeView.SetBottom(this);
			}
		}

		/// <summary>Returns the number of child tree nodes.</summary>
		/// <returns>The number of child tree nodes assigned to the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		/// <param name="includeSubTrees">true if the resulting count includes all tree nodes indirectly rooted at this tree node; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600422A RID: 16938 RVA: 0x00105388 File Offset: 0x00103588
		public int GetNodeCount(bool includeSubTrees)
		{
			if (!includeSubTrees)
			{
				return this.Nodes.Count;
			}
			int num = 0;
			this.GetNodeCountRecursive(this, ref num);
			return num;
		}

		/// <summary>Removes the current tree node from the tree view control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600422B RID: 16939 RVA: 0x001053B4 File Offset: 0x001035B4
		public void Remove()
		{
			if (this.parent == null)
			{
				return;
			}
			int index = this.Index;
			this.parent.Nodes.RemoveAt(index);
		}

		/// <summary>Toggles the tree node to either the expanded or collapsed state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600422C RID: 16940 RVA: 0x001053E8 File Offset: 0x001035E8
		public void Toggle()
		{
			if (this.is_expanded)
			{
				this.Collapse();
			}
			else
			{
				this.Expand();
			}
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600422D RID: 16941 RVA: 0x00105408 File Offset: 0x00103608
		public override string ToString()
		{
			return "TreeNode: " + this.Text;
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x0010541C File Offset: 0x0010361C
		internal bool ArePreviousNodesExpanded
		{
			get
			{
				for (TreeNode treeNode = this.Parent; treeNode != null; treeNode = treeNode.Parent)
				{
					if (!treeNode.is_expanded)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x00105450 File Offset: 0x00103650
		internal bool IsRoot
		{
			get
			{
				TreeView treeView = this.TreeView;
				return treeView != null && treeView.root_node == this;
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x0010547C File Offset: 0x0010367C
		private bool BuildFullPath(StringBuilder path)
		{
			if (this.parent == null)
			{
				return false;
			}
			if (this.parent.BuildFullPath(path))
			{
				path.Append(this.TreeView.PathSeparator);
			}
			path.Append(this.text);
			return true;
		}

		/// <summary>Gets the position of the tree node in the tree node collection.</summary>
		/// <returns>A zero-based index value that represents the position of the tree node in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x001054C8 File Offset: 0x001036C8
		public int Index
		{
			get
			{
				if (this.parent == null)
				{
					return 0;
				}
				return this.parent.Nodes.IndexOf(this);
			}
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x001054E8 File Offset: 0x001036E8
		private void Expand(bool byInternal)
		{
			if (this.is_expanded || this.nodes.Count < 1)
			{
				this.is_expanded = true;
				return;
			}
			bool flag = false;
			TreeView treeView = this.TreeView;
			if (treeView != null)
			{
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, TreeViewAction.Expand);
				treeView.OnBeforeExpand(treeViewCancelEventArgs);
				flag = treeViewCancelEventArgs.Cancel;
			}
			if (!flag)
			{
				this.is_expanded = true;
				int num = this.CountToNext();
				if (treeView != null)
				{
					treeView.OnAfterExpand(new TreeViewEventArgs(this));
					treeView.RecalculateVisibleOrder(this);
					treeView.UpdateScrollBars(false);
					if (this.visible_order < treeView.skipped_nodes + treeView.VisibleCount + 1 && this.ArePreviousNodesExpanded)
					{
						treeView.ExpandBelow(this, num);
					}
				}
			}
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x001055A0 File Offset: 0x001037A0
		private void CollapseInternal(bool byInternal)
		{
			if (!this.is_expanded || this.nodes.Count < 1)
			{
				return;
			}
			if (this.IsRoot)
			{
				return;
			}
			bool flag = false;
			TreeView treeView = this.TreeView;
			if (treeView != null)
			{
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, TreeViewAction.Collapse);
				treeView.OnBeforeCollapse(treeViewCancelEventArgs);
				flag = treeViewCancelEventArgs.Cancel;
			}
			if (!flag)
			{
				int num = this.CountToNext();
				this.is_expanded = false;
				if (treeView != null)
				{
					treeView.OnAfterCollapse(new TreeViewEventArgs(this));
					bool visible = treeView.hbar.Visible;
					bool visible2 = treeView.vbar.Visible;
					treeView.RecalculateVisibleOrder(this);
					treeView.UpdateScrollBars(false);
					if (this.visible_order < treeView.skipped_nodes + treeView.VisibleCount + 1 && this.ArePreviousNodesExpanded)
					{
						treeView.CollapseBelow(this, num);
					}
					if (!byInternal && this.HasFocusInChildren())
					{
						treeView.SelectedNode = this;
					}
					if ((visible & !treeView.hbar.Visible) || (visible2 & !treeView.vbar.Visible))
					{
						treeView.Invalidate();
					}
				}
			}
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x001056C4 File Offset: 0x001038C4
		private int CountToNext()
		{
			bool flag = this.is_expanded;
			this.is_expanded = false;
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
			TreeNode treeNode = null;
			if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
			{
				treeNode = openTreeNodeEnumerator.CurrentNode;
			}
			this.is_expanded = flag;
			openTreeNodeEnumerator.Reset();
			openTreeNodeEnumerator.MoveNext();
			int num = 0;
			while (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.CurrentNode != treeNode)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x0010573C File Offset: 0x0010393C
		private bool HasFocusInChildren()
		{
			if (this.TreeView == null)
			{
				return false;
			}
			foreach (object obj in this.nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode == this.TreeView.SelectedNode)
				{
					return true;
				}
				if (treeNode.HasFocusInChildren())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x001057E0 File Offset: 0x001039E0
		private void ExpandRecursive(TreeNode node)
		{
			node.Expand(true);
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.ExpandRecursive(treeNode);
			}
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x00105858 File Offset: 0x00103A58
		private void ExpandParentRecursive(TreeNode node)
		{
			node.Expand(true);
			if (node.Parent != null)
			{
				this.ExpandParentRecursive(node.Parent);
			}
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00105884 File Offset: 0x00103A84
		internal void CollapseAll()
		{
			this.CollapseRecursive(this);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x00105890 File Offset: 0x00103A90
		internal void CollapseAllUncheck()
		{
			this.CollapseUncheckRecursive(this);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x0010589C File Offset: 0x00103A9C
		private void CollapseRecursive(TreeNode node)
		{
			node.Collapse();
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.CollapseRecursive(treeNode);
			}
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x00105914 File Offset: 0x00103B14
		private void CollapseUncheckRecursive(TreeNode node)
		{
			node.Collapse();
			node.Checked = false;
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.CollapseUncheckRecursive(treeNode);
			}
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00105990 File Offset: 0x00103B90
		internal void SetNodes(TreeNodeCollection nodes)
		{
			this.nodes = nodes;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x0010599C File Offset: 0x00103B9C
		private void GetNodeCountRecursive(TreeNode node, ref int count)
		{
			count += node.Nodes.Count;
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.GetNodeCountRecursive(treeNode, ref count);
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x00105A1C File Offset: 0x00103C1C
		internal bool NeedsWidth
		{
			get
			{
				return this.width == -1;
			}
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00105A28 File Offset: 0x00103C28
		internal void Invalidate()
		{
			this.width = -1;
			TreeView treeView = this.TreeView;
			if (treeView == null)
			{
				return;
			}
			treeView.UpdateNode(this);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x00105A54 File Offset: 0x00103C54
		internal void InvalidateWidth()
		{
			this.width = -1;
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00105A60 File Offset: 0x00103C60
		internal void SetWidth(int width)
		{
			this.width = width;
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00105A6C File Offset: 0x00103C6C
		internal void SetParent(TreeNode parent)
		{
			this.parent = parent;
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x00105A78 File Offset: 0x00103C78
		private bool IsInClippingRect
		{
			get
			{
				if (this.TreeView == null)
				{
					return false;
				}
				Rectangle bounds = this.Bounds;
				return bounds.Y >= 0 || bounds.Y <= this.TreeView.ClientRectangle.Height;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x00105AC8 File Offset: 0x00103CC8
		internal Image StateImage
		{
			get
			{
				if (this.TreeView != null)
				{
					if (this.TreeView.StateImageList == null)
					{
						return null;
					}
					if (this.state_image_index >= 0)
					{
						return this.TreeView.StateImageList.Images[this.state_image_index];
					}
					if (this.state_image_key != string.Empty)
					{
						return this.TreeView.StateImageList.Images[this.state_image_key];
					}
				}
				return null;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x00105B4C File Offset: 0x00103D4C
		internal int Image
		{
			get
			{
				if (this.TreeView == null || this.TreeView.ImageList == null)
				{
					return -1;
				}
				if (this.IsSelected)
				{
					if (this.selected_image_index >= 0)
					{
						return this.selected_image_index;
					}
					if (!string.IsNullOrEmpty(this.selected_image_key))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.selected_image_key);
					}
					if (!string.IsNullOrEmpty(this.TreeView.SelectedImageKey))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.TreeView.SelectedImageKey);
					}
					if (this.TreeView.SelectedImageIndex >= 0)
					{
						return this.TreeView.SelectedImageIndex;
					}
				}
				else
				{
					if (this.image_index >= 0)
					{
						return this.image_index;
					}
					if (!string.IsNullOrEmpty(this.image_key))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.image_key);
					}
					if (!string.IsNullOrEmpty(this.TreeView.ImageKey))
					{
						return this.TreeView.ImageList.Images.IndexOfKey(this.TreeView.ImageKey);
					}
					if (this.TreeView.ImageIndex >= 0)
					{
						return this.TreeView.ImageIndex;
					}
				}
				if (this.TreeView.ImageList.Images.Count > 0)
				{
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x04001BCE RID: 7118
		private TreeView tree_view;

		// Token: 0x04001BCF RID: 7119
		internal TreeNode parent;

		// Token: 0x04001BD0 RID: 7120
		private string text;

		// Token: 0x04001BD1 RID: 7121
		private int image_index = -1;

		// Token: 0x04001BD2 RID: 7122
		private int selected_image_index = -1;

		// Token: 0x04001BD3 RID: 7123
		private ContextMenu context_menu;

		// Token: 0x04001BD4 RID: 7124
		private ContextMenuStrip context_menu_strip;

		// Token: 0x04001BD5 RID: 7125
		private string image_key = string.Empty;

		// Token: 0x04001BD6 RID: 7126
		private string selected_image_key = string.Empty;

		// Token: 0x04001BD7 RID: 7127
		private int state_image_index = -1;

		// Token: 0x04001BD8 RID: 7128
		private string state_image_key = string.Empty;

		// Token: 0x04001BD9 RID: 7129
		private string tool_tip_text = string.Empty;

		// Token: 0x04001BDA RID: 7130
		internal TreeNodeCollection nodes;

		// Token: 0x04001BDB RID: 7131
		internal TreeViewAction check_reason;

		// Token: 0x04001BDC RID: 7132
		internal int visible_order;

		// Token: 0x04001BDD RID: 7133
		internal int width = -1;

		// Token: 0x04001BDE RID: 7134
		internal bool is_expanded;

		// Token: 0x04001BDF RID: 7135
		private bool check;

		// Token: 0x04001BE0 RID: 7136
		internal OwnerDrawPropertyBag prop_bag;

		// Token: 0x04001BE1 RID: 7137
		private object tag;

		// Token: 0x04001BE2 RID: 7138
		internal IntPtr handle;

		// Token: 0x04001BE3 RID: 7139
		private string name = string.Empty;
	}
}
