using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Displays a hierarchical collection of labeled items, each represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000392 RID: 914
	[DefaultProperty("Nodes")]
	[DefaultEvent("AfterSelect")]
	[ClassInterface(1)]
	[Docking(DockingBehavior.Ask)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.TreeViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class TreeView : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeView" /> class.</summary>
		// Token: 0x06004287 RID: 17031 RVA: 0x001069DC File Offset: 0x00104BDC
		public TreeView()
		{
			this.vbar = new ImplicitVScrollBar();
			this.hbar = new ImplicitHScrollBar();
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.background_color = ThemeEngine.Current.ColorWindow;
			this.foreground_color = ThemeEngine.Current.ColorWindowText;
			this.draw_mode = TreeViewDrawMode.Normal;
			this.root_node = new TreeNode(this);
			this.root_node.Text = "ROOT NODE";
			this.nodes = new TreeNodeCollection(this.root_node);
			this.root_node.SetNodes(this.nodes);
			base.MouseDown += this.MouseDownHandler;
			base.MouseUp += this.MouseUpHandler;
			base.MouseMove += this.MouseMoveHandler;
			base.SizeChanged += new EventHandler(this.SizeChangedHandler);
			base.FontChanged += new EventHandler(this.FontChangedHandler);
			base.LostFocus += new EventHandler(this.LostFocusHandler);
			base.GotFocus += new EventHandler(this.GotFocusHandler);
			base.MouseWheel += this.MouseWheelHandler;
			base.VisibleChanged += new EventHandler(this.VisibleChangedHandler);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
			this.string_format = new StringFormat();
			this.string_format.LineAlignment = 1;
			this.string_format.Alignment = 1;
			this.vbar.Visible = false;
			this.hbar.Visible = false;
			this.vbar.ValueChanged += new EventHandler(this.VScrollBarValueChanged);
			this.hbar.ValueChanged += new EventHandler(this.HScrollBarValueChanged);
			base.SuspendLayout();
			base.Controls.AddImplicit(this.vbar);
			base.Controls.AddImplicit(this.hbar);
			base.ResumeLayout();
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x00106C20 File Offset: 0x00104E20
		// Note: this type is marked as 'beforefieldinit'.
		static TreeView()
		{
			TreeView.ItemDragEvent = new object();
			TreeView.AfterCheckEvent = new object();
			TreeView.AfterCollapseEvent = new object();
			TreeView.AfterExpandEvent = new object();
			TreeView.AfterLabelEditEvent = new object();
			TreeView.AfterSelectEvent = new object();
			TreeView.BeforeCheckEvent = new object();
			TreeView.BeforeCollapseEvent = new object();
			TreeView.BeforeExpandEvent = new object();
			TreeView.BeforeLabelEditEvent = new object();
			TreeView.BeforeSelectEvent = new object();
			TreeView.DrawNodeEvent = new object();
			TreeView.NodeMouseClickEvent = new object();
			TreeView.NodeMouseDoubleClickEvent = new object();
			TreeView.NodeMouseHoverEvent = new object();
			TreeView.RightToLeftLayoutChangedEvent = new object();
			TreeView.UIACheckBoxesChangedEvent = new object();
			TreeView.UIALabelEditChangedEvent = new object();
			TreeView.UIANodeTextChangedEvent = new object();
			TreeView.UIACollectionChangedEvent = new object();
		}

		/// <summary>Occurs when the user begins dragging a node.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000413 RID: 1043
		// (add) Token: 0x06004289 RID: 17033 RVA: 0x00106CF8 File Offset: 0x00104EF8
		// (remove) Token: 0x0600428A RID: 17034 RVA: 0x00106D0C File Offset: 0x00104F0C
		public event ItemDragEventHandler ItemDrag
		{
			add
			{
				base.Events.AddHandler(TreeView.ItemDragEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.ItemDragEvent, value);
			}
		}

		/// <summary>Occurs after the tree node check box is checked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000414 RID: 1044
		// (add) Token: 0x0600428B RID: 17035 RVA: 0x00106D20 File Offset: 0x00104F20
		// (remove) Token: 0x0600428C RID: 17036 RVA: 0x00106D34 File Offset: 0x00104F34
		public event TreeViewEventHandler AfterCheck
		{
			add
			{
				base.Events.AddHandler(TreeView.AfterCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.AfterCheckEvent, value);
			}
		}

		/// <summary>Occurs after the tree node is collapsed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000415 RID: 1045
		// (add) Token: 0x0600428D RID: 17037 RVA: 0x00106D48 File Offset: 0x00104F48
		// (remove) Token: 0x0600428E RID: 17038 RVA: 0x00106D5C File Offset: 0x00104F5C
		public event TreeViewEventHandler AfterCollapse
		{
			add
			{
				base.Events.AddHandler(TreeView.AfterCollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.AfterCollapseEvent, value);
			}
		}

		/// <summary>Occurs after the tree node is expanded.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000416 RID: 1046
		// (add) Token: 0x0600428F RID: 17039 RVA: 0x00106D70 File Offset: 0x00104F70
		// (remove) Token: 0x06004290 RID: 17040 RVA: 0x00106D84 File Offset: 0x00104F84
		public event TreeViewEventHandler AfterExpand
		{
			add
			{
				base.Events.AddHandler(TreeView.AfterExpandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.AfterExpandEvent, value);
			}
		}

		/// <summary>Occurs after the tree node label text is edited.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000417 RID: 1047
		// (add) Token: 0x06004291 RID: 17041 RVA: 0x00106D98 File Offset: 0x00104F98
		// (remove) Token: 0x06004292 RID: 17042 RVA: 0x00106DAC File Offset: 0x00104FAC
		public event NodeLabelEditEventHandler AfterLabelEdit
		{
			add
			{
				base.Events.AddHandler(TreeView.AfterLabelEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.AfterLabelEditEvent, value);
			}
		}

		/// <summary>Occurs after the tree node is selected.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000418 RID: 1048
		// (add) Token: 0x06004293 RID: 17043 RVA: 0x00106DC0 File Offset: 0x00104FC0
		// (remove) Token: 0x06004294 RID: 17044 RVA: 0x00106DD4 File Offset: 0x00104FD4
		public event TreeViewEventHandler AfterSelect
		{
			add
			{
				base.Events.AddHandler(TreeView.AfterSelectEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.AfterSelectEvent, value);
			}
		}

		/// <summary>Occurs before the tree node check box is checked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000419 RID: 1049
		// (add) Token: 0x06004295 RID: 17045 RVA: 0x00106DE8 File Offset: 0x00104FE8
		// (remove) Token: 0x06004296 RID: 17046 RVA: 0x00106DFC File Offset: 0x00104FFC
		public event TreeViewCancelEventHandler BeforeCheck
		{
			add
			{
				base.Events.AddHandler(TreeView.BeforeCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.BeforeCheckEvent, value);
			}
		}

		/// <summary>Occurs before the tree node is collapsed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400041A RID: 1050
		// (add) Token: 0x06004297 RID: 17047 RVA: 0x00106E10 File Offset: 0x00105010
		// (remove) Token: 0x06004298 RID: 17048 RVA: 0x00106E24 File Offset: 0x00105024
		public event TreeViewCancelEventHandler BeforeCollapse
		{
			add
			{
				base.Events.AddHandler(TreeView.BeforeCollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.BeforeCollapseEvent, value);
			}
		}

		/// <summary>Occurs before the tree node is expanded.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400041B RID: 1051
		// (add) Token: 0x06004299 RID: 17049 RVA: 0x00106E38 File Offset: 0x00105038
		// (remove) Token: 0x0600429A RID: 17050 RVA: 0x00106E4C File Offset: 0x0010504C
		public event TreeViewCancelEventHandler BeforeExpand
		{
			add
			{
				base.Events.AddHandler(TreeView.BeforeExpandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.BeforeExpandEvent, value);
			}
		}

		/// <summary>Occurs before the tree node label text is edited.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400041C RID: 1052
		// (add) Token: 0x0600429B RID: 17051 RVA: 0x00106E60 File Offset: 0x00105060
		// (remove) Token: 0x0600429C RID: 17052 RVA: 0x00106E74 File Offset: 0x00105074
		public event NodeLabelEditEventHandler BeforeLabelEdit
		{
			add
			{
				base.Events.AddHandler(TreeView.BeforeLabelEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.BeforeLabelEditEvent, value);
			}
		}

		/// <summary>Occurs before the tree node is selected.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400041D RID: 1053
		// (add) Token: 0x0600429D RID: 17053 RVA: 0x00106E88 File Offset: 0x00105088
		// (remove) Token: 0x0600429E RID: 17054 RVA: 0x00106E9C File Offset: 0x0010509C
		public event TreeViewCancelEventHandler BeforeSelect
		{
			add
			{
				base.Events.AddHandler(TreeView.BeforeSelectEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.BeforeSelectEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.TreeView" /> is drawn and the <see cref="P:System.Windows.Forms.TreeView.DrawMode" /> property is set to a <see cref="T:System.Windows.Forms.TreeViewDrawMode" /> value other than <see cref="F:System.Windows.Forms.TreeViewDrawMode.Normal" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400041E RID: 1054
		// (add) Token: 0x0600429F RID: 17055 RVA: 0x00106EB0 File Offset: 0x001050B0
		// (remove) Token: 0x060042A0 RID: 17056 RVA: 0x00106EC4 File Offset: 0x001050C4
		public event DrawTreeNodeEventHandler DrawNode
		{
			add
			{
				base.Events.AddHandler(TreeView.DrawNodeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.DrawNodeEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks a <see cref="T:System.Windows.Forms.TreeNode" /> with the mouse. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400041F RID: 1055
		// (add) Token: 0x060042A1 RID: 17057 RVA: 0x00106ED8 File Offset: 0x001050D8
		// (remove) Token: 0x060042A2 RID: 17058 RVA: 0x00106EEC File Offset: 0x001050EC
		public event TreeNodeMouseClickEventHandler NodeMouseClick
		{
			add
			{
				base.Events.AddHandler(TreeView.NodeMouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.NodeMouseClickEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks a <see cref="T:System.Windows.Forms.TreeNode" /> with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000420 RID: 1056
		// (add) Token: 0x060042A3 RID: 17059 RVA: 0x00106F00 File Offset: 0x00105100
		// (remove) Token: 0x060042A4 RID: 17060 RVA: 0x00106F14 File Offset: 0x00105114
		public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(TreeView.NodeMouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.NodeMouseDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the mouse hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000421 RID: 1057
		// (add) Token: 0x060042A5 RID: 17061 RVA: 0x00106F28 File Offset: 0x00105128
		// (remove) Token: 0x060042A6 RID: 17062 RVA: 0x00106F3C File Offset: 0x0010513C
		public event TreeNodeMouseHoverEventHandler NodeMouseHover
		{
			add
			{
				base.Events.AddHandler(TreeView.NodeMouseHoverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.NodeMouseHoverEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TreeView.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000422 RID: 1058
		// (add) Token: 0x060042A7 RID: 17063 RVA: 0x00106F50 File Offset: 0x00105150
		// (remove) Token: 0x060042A8 RID: 17064 RVA: 0x00106F64 File Offset: 0x00105164
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TreeView.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000423 RID: 1059
		// (add) Token: 0x060042A9 RID: 17065 RVA: 0x00106F78 File Offset: 0x00105178
		// (remove) Token: 0x060042AA RID: 17066 RVA: 0x00106F84 File Offset: 0x00105184
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TreeView.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000424 RID: 1060
		// (add) Token: 0x060042AB RID: 17067 RVA: 0x00106F90 File Offset: 0x00105190
		// (remove) Token: 0x060042AC RID: 17068 RVA: 0x00106F9C File Offset: 0x0010519C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TreeView.Padding" /> property changes.</summary>
		// Token: 0x14000425 RID: 1061
		// (add) Token: 0x060042AD RID: 17069 RVA: 0x00106FA8 File Offset: 0x001051A8
		// (remove) Token: 0x060042AE RID: 17070 RVA: 0x00106FB4 File Offset: 0x001051B4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TreeView" /> is drawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000426 RID: 1062
		// (add) Token: 0x060042AF RID: 17071 RVA: 0x00106FC0 File Offset: 0x001051C0
		// (remove) Token: 0x060042B0 RID: 17072 RVA: 0x00106FCC File Offset: 0x001051CC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TreeView.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000427 RID: 1063
		// (add) Token: 0x060042B1 RID: 17073 RVA: 0x00106FD8 File Offset: 0x001051D8
		// (remove) Token: 0x060042B2 RID: 17074 RVA: 0x00106FE4 File Offset: 0x001051E4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x14000428 RID: 1064
		// (add) Token: 0x060042B3 RID: 17075 RVA: 0x00106FF0 File Offset: 0x001051F0
		// (remove) Token: 0x060042B4 RID: 17076 RVA: 0x00107004 File Offset: 0x00105204
		internal event EventHandler UIACheckBoxesChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.UIACheckBoxesChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.UIACheckBoxesChangedEvent, value);
			}
		}

		// Token: 0x14000429 RID: 1065
		// (add) Token: 0x060042B5 RID: 17077 RVA: 0x00107018 File Offset: 0x00105218
		// (remove) Token: 0x060042B6 RID: 17078 RVA: 0x0010702C File Offset: 0x0010522C
		internal event EventHandler UIALabelEditChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.UIALabelEditChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.UIALabelEditChangedEvent, value);
			}
		}

		// Token: 0x1400042A RID: 1066
		// (add) Token: 0x060042B7 RID: 17079 RVA: 0x00107040 File Offset: 0x00105240
		// (remove) Token: 0x060042B8 RID: 17080 RVA: 0x00107054 File Offset: 0x00105254
		internal event TreeViewEventHandler UIANodeTextChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.UIANodeTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.UIANodeTextChangedEvent, value);
			}
		}

		// Token: 0x1400042B RID: 1067
		// (add) Token: 0x060042B9 RID: 17081 RVA: 0x00107068 File Offset: 0x00105268
		// (remove) Token: 0x060042BA RID: 17082 RVA: 0x0010707C File Offset: 0x0010527C
		internal event CollectionChangeEventHandler UIACollectionChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.UIACollectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.UIACollectionChangedEvent, value);
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x00107090 File Offset: 0x00105290
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x00107098 File Offset: 0x00105298
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.CreateDashPen();
				base.Invalidate();
			}
		}

		/// <summary>Gets or set the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x001070B0 File Offset: 0x001052B0
		// (set) Token: 0x060042BE RID: 17086 RVA: 0x001070B8 File Offset: 0x001052B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>Gets or sets the border style of the tree view control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x001070C4 File Offset: 0x001052C4
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x001070CC File Offset: 0x001052CC
		[DispId(-504)]
		[DefaultValue(BorderStyle.Fixed3D)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the tree view control.</summary>
		/// <returns>true if a check box is displayed next to each tree node in the tree view control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x001070D8 File Offset: 0x001052D8
		// (set) Token: 0x060042C2 RID: 17090 RVA: 0x001070E0 File Offset: 0x001052E0
		[DefaultValue(false)]
		public bool CheckBoxes
		{
			get
			{
				return this.checkboxes;
			}
			set
			{
				if (value == this.checkboxes)
				{
					return;
				}
				this.checkboxes = value;
				if (!this.checkboxes)
				{
					this.root_node.CollapseAllUncheck();
				}
				base.Invalidate();
				this.OnUIACheckBoxesChanged(EventArgs.Empty);
			}
		}

		/// <summary>The current foreground color for this control, which is the color the control uses to draw its text.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x00107120 File Offset: 0x00105320
		// (set) Token: 0x060042C4 RID: 17092 RVA: 0x00107128 File Offset: 0x00105328
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the selection highlight spans the width of the tree view control.</summary>
		/// <returns>true if the selection highlight spans the width of the tree view control; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x00107134 File Offset: 0x00105334
		// (set) Token: 0x060042C6 RID: 17094 RVA: 0x0010713C File Offset: 0x0010533C
		[DefaultValue(false)]
		public bool FullRowSelect
		{
			get
			{
				return this.full_row_select;
			}
			set
			{
				if (value == this.full_row_select)
				{
					return;
				}
				this.full_row_select = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected tree node remains highlighted even when the tree view has lost the focus.</summary>
		/// <returns>true if the selected tree node is not highlighted when the tree view has lost the focus; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x00107158 File Offset: 0x00105358
		// (set) Token: 0x060042C8 RID: 17096 RVA: 0x00107160 File Offset: 0x00105360
		[DefaultValue(true)]
		public bool HideSelection
		{
			get
			{
				return this.hide_selection;
			}
			set
			{
				if (this.hide_selection == value)
				{
					return;
				}
				this.hide_selection = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it.</summary>
		/// <returns>true if a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x0010717C File Offset: 0x0010537C
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x00107184 File Offset: 0x00105384
		[DefaultValue(false)]
		public bool HotTracking
		{
			get
			{
				return this.hot_tracking;
			}
			set
			{
				this.hot_tracking = value;
			}
		}

		/// <summary>Gets or sets the image-list index value of the default image that is displayed by the tree nodes.</summary>
		/// <returns>A zero-based index that represents the position of an <see cref="T:System.Drawing.Image" /> in an <see cref="T:System.Windows.Forms.ImageList" />. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x00107190 File Offset: 0x00105390
		// (set) Token: 0x060042CC RID: 17100 RVA: 0x00107198 File Offset: 0x00105398
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RelatedImageList("ImageList")]
		[RefreshProperties(2)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'value'. 'value' must be greater than or equal to 0.");
				}
				if (this.image_index == value)
				{
					return;
				}
				this.image_index = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> objects used by the tree nodes.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> objects used by the tree nodes. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x001071E4 File Offset: 0x001053E4
		// (set) Token: 0x060042CE RID: 17102 RVA: 0x001071EC File Offset: 0x001053EC
		[RefreshProperties(2)]
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				this.image_list = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the distance to indent each of the child tree node levels.</summary>
		/// <returns>The distance, in pixels, to indent each of the child tree node levels. The default value is 19.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0 (see Remarks).-or- The assigned value is greater than 32,000. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x001071FC File Offset: 0x001053FC
		// (set) Token: 0x060042D0 RID: 17104 RVA: 0x00107204 File Offset: 0x00105404
		[Localizable(true)]
		public int Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				if (this.indent == value)
				{
					return;
				}
				if (value > 32000)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'Indent'. 'Indent' must be less than or equal to 32000");
				}
				if (value < 0)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'Indent'. 'Indent' must be greater than or equal to 0.");
				}
				this.indent = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the height of each tree node in the tree view control.</summary>
		/// <returns>The height, in pixels, of each tree node in the tree view.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than one.-or- The assigned value is greater than the <see cref="F:System.Int16.MaxValue" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x00107274 File Offset: 0x00105474
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x00107294 File Offset: 0x00105494
		public int ItemHeight
		{
			get
			{
				if (this.item_height == -1)
				{
					return base.FontHeight + 3;
				}
				return this.item_height;
			}
			set
			{
				if (value == this.item_height)
				{
					return;
				}
				this.item_height = value;
				base.Invalidate();
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x001072B0 File Offset: 0x001054B0
		internal int ActualItemHeight
		{
			get
			{
				int num = this.ItemHeight;
				if (this.ImageList != null && this.ImageList.ImageSize.Height > num)
				{
					num = this.ImageList.ImageSize.Height;
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the label text of the tree nodes can be edited.</summary>
		/// <returns>true if the label text of the tree nodes can be edited; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x00107300 File Offset: 0x00105500
		// (set) Token: 0x060042D5 RID: 17109 RVA: 0x00107308 File Offset: 0x00105508
		[DefaultValue(false)]
		public bool LabelEdit
		{
			get
			{
				return this.label_edit;
			}
			set
			{
				this.label_edit = value;
				this.OnUIALabelEditChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the collection of tree nodes that are assigned to the tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the tree view control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060042D6 RID: 17110 RVA: 0x0010731C File Offset: 0x0010551C
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		[MergableProperty(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				return this.nodes;
			}
		}

		/// <summary>Gets or sets the spacing between the <see cref="T:System.Windows.Forms.TreeView" /> control's contents and its edges.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> indicating the space between the control edges and its contents.</returns>
		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x00107324 File Offset: 0x00105524
		// (set) Token: 0x060042D8 RID: 17112 RVA: 0x0010732C File Offset: 0x0010552C
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Gets or sets the delimiter string that the tree node path uses.</summary>
		/// <returns>The delimiter string that the tree node <see cref="P:System.Windows.Forms.TreeNode.FullPath" /> property uses. The default is the backslash character (\).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x00107338 File Offset: 0x00105538
		// (set) Token: 0x060042DA RID: 17114 RVA: 0x00107340 File Offset: 0x00105540
		[DefaultValue("\\")]
		public string PathSeparator
		{
			get
			{
				return this.path_separator;
			}
			set
			{
				this.path_separator = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TreeView" /> should be laid out from right-to-left.</summary>
		/// <returns>true to indicate the control should be laid out from right-to-left; otherwise, false. The default is false.</returns>
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x0010734C File Offset: 0x0010554C
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x00107354 File Offset: 0x00105554
		[DefaultValue(false)]
		[Localizable(true)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
			set
			{
				if (this.right_to_left_layout != value)
				{
					this.right_to_left_layout = value;
					this.OnRightToLeftLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree view control displays scroll bars when they are needed.</summary>
		/// <returns>true if the tree view control displays scroll bars when they are needed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x00107374 File Offset: 0x00105574
		// (set) Token: 0x060042DE RID: 17118 RVA: 0x0010737C File Offset: 0x0010557C
		[DefaultValue(true)]
		public bool Scrollable
		{
			get
			{
				return this.scrollable;
			}
			set
			{
				if (this.scrollable == value)
				{
					return;
				}
				this.scrollable = value;
				this.UpdateScrollBars(false);
			}
		}

		/// <summary>Gets or sets the image list index value of the image that is displayed when a tree node is selected.</summary>
		/// <returns>A zero-based index value that represents the position of an <see cref="T:System.Drawing.Image" /> in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <exception cref="T:System.ArgumentException">The index assigned value is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0010739C File Offset: 0x0010559C
		// (set) Token: 0x060042E0 RID: 17120 RVA: 0x001073A4 File Offset: 0x001055A4
		[Localizable(true)]
		[DefaultValue(-1)]
		[RelatedImageList("ImageList")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public int SelectedImageIndex
		{
			get
			{
				return this.selected_image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'value'. 'value' must be greater than or equal to 0.");
				}
				this.UpdateNode(this.SelectedNode);
			}
		}

		/// <summary>Gets or sets the tree node that is currently selected in the tree view control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that is currently selected in the tree view control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x001073E0 File Offset: 0x001055E0
		// (set) Token: 0x060042E2 RID: 17122 RVA: 0x001073FC File Offset: 0x001055FC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public TreeNode SelectedNode
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return this.pre_selected_node;
				}
				return this.selected_node;
			}
			set
			{
				if (!base.IsHandleCreated)
				{
					this.pre_selected_node = value;
					return;
				}
				if (this.selected_node == value)
				{
					this.selection_action = TreeViewAction.Unknown;
					return;
				}
				if (value != null)
				{
					TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(value, false, this.selection_action);
					this.OnBeforeSelect(treeViewCancelEventArgs);
					if (treeViewCancelEventArgs.Cancel)
					{
						return;
					}
				}
				Rectangle rectangle = Rectangle.Empty;
				if (this.selected_node != null)
				{
					rectangle = this.Bloat(this.selected_node.Bounds);
				}
				if (this.focused_node != null)
				{
					rectangle = Rectangle.Union(rectangle, this.Bloat(this.focused_node.Bounds));
				}
				if (value != null)
				{
					rectangle = Rectangle.Union(rectangle, this.Bloat(value.Bounds));
				}
				this.highlighted_node = value;
				this.selected_node = value;
				this.focused_node = value;
				if (this.full_row_select || this.draw_mode != TreeViewDrawMode.Normal)
				{
					rectangle.X = 0;
					rectangle.Width = this.ViewportRectangle.Width;
				}
				if (rectangle != Rectangle.Empty)
				{
					base.Invalidate(rectangle);
				}
				if (this.selected_node != null)
				{
					this.selected_node.EnsureVisible();
				}
				if (value != null)
				{
					this.OnAfterSelect(new TreeViewEventArgs(value, TreeViewAction.Unknown));
				}
				this.selection_action = TreeViewAction.Unknown;
			}
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x00107548 File Offset: 0x00105748
		private Rectangle Bloat(Rectangle rect)
		{
			rect.Y--;
			rect.X--;
			rect.Height += 2;
			rect.Width += 2;
			return rect;
		}

		/// <summary>Gets or sets a value indicating whether lines are drawn between tree nodes in the tree view control.</summary>
		/// <returns>true if lines are drawn between tree nodes in the tree view control; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x00107594 File Offset: 0x00105794
		// (set) Token: 0x060042E5 RID: 17125 RVA: 0x0010759C File Offset: 0x0010579C
		[DefaultValue(true)]
		public bool ShowLines
		{
			get
			{
				return this.show_lines;
			}
			set
			{
				if (this.show_lines == value)
				{
					return;
				}
				this.show_lines = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating ToolTips are shown when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>true if ToolTips are shown when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x001075B8 File Offset: 0x001057B8
		// (set) Token: 0x060042E7 RID: 17127 RVA: 0x001075C0 File Offset: 0x001057C0
		[DefaultValue(false)]
		public bool ShowNodeToolTips
		{
			get
			{
				return this.show_node_tool_tips;
			}
			set
			{
				this.show_node_tool_tips = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to tree nodes that contain child tree nodes.</summary>
		/// <returns>true if plus sign and minus sign buttons are displayed next to tree nodes that contain child tree nodes; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x001075CC File Offset: 0x001057CC
		// (set) Token: 0x060042E9 RID: 17129 RVA: 0x001075D4 File Offset: 0x001057D4
		[DefaultValue(true)]
		public bool ShowPlusMinus
		{
			get
			{
				return this.show_plus_minus;
			}
			set
			{
				if (this.show_plus_minus == value)
				{
					return;
				}
				this.show_plus_minus = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether lines are drawn between the tree nodes that are at the root of the tree view.</summary>
		/// <returns>true if lines are drawn between the tree nodes that are at the root of the tree view; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x001075F0 File Offset: 0x001057F0
		// (set) Token: 0x060042EB RID: 17131 RVA: 0x001075F8 File Offset: 0x001057F8
		[DefaultValue(true)]
		public bool ShowRootLines
		{
			get
			{
				return this.show_root_lines;
			}
			set
			{
				if (this.show_root_lines == value)
				{
					return;
				}
				this.show_root_lines = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree nodes in the tree view are sorted.</summary>
		/// <returns>true if the tree nodes in the tree view are sorted; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x00107614 File Offset: 0x00105814
		// (set) Token: 0x060042ED RID: 17133 RVA: 0x0010761C File Offset: 0x0010581C
		[DefaultValue(false)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted == value)
				{
					return;
				}
				this.sorted = value;
				if (this.sorted && this.tree_view_node_sorter == null)
				{
					this.Sort(null);
				}
			}
		}

		/// <summary>Gets or sets the image list used for indicating the state of the <see cref="T:System.Windows.Forms.TreeView" /> and its nodes.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> used for indicating the state of the <see cref="T:System.Windows.Forms.TreeView" /> and its nodes.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x00107650 File Offset: 0x00105850
		// (set) Token: 0x060042EF RID: 17135 RVA: 0x00107658 File Offset: 0x00105858
		[DefaultValue(null)]
		public ImageList StateImageList
		{
			get
			{
				return this.state_image_list;
			}
			set
			{
				this.state_image_list = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.TreeView" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x00107668 File Offset: 0x00105868
		// (set) Token: 0x060042F1 RID: 17137 RVA: 0x00107670 File Offset: 0x00105870
		[Browsable(false)]
		[EditorBrowsable(1)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the first fully-visible tree node in the tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the first fully-visible tree node in the tree view control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x0010767C File Offset: 0x0010587C
		// (set) Token: 0x060042F3 RID: 17139 RVA: 0x001076D8 File Offset: 0x001058D8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public TreeNode TopNode
		{
			get
			{
				if (this.root_node.FirstNode == null)
				{
					return null;
				}
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node.FirstNode);
				openTreeNodeEnumerator.MoveNext();
				for (int i = 0; i < this.skipped_nodes; i++)
				{
					openTreeNodeEnumerator.MoveNext();
				}
				return openTreeNodeEnumerator.CurrentNode;
			}
			set
			{
				this.SetTop(value);
			}
		}

		/// <summary>Gets or sets the implementation of <see cref="T:System.Collections.IComparer" /> to perform a custom sort of the <see cref="T:System.Windows.Forms.TreeView" /> nodes.</summary>
		/// <returns>The <see cref="T:System.Collections.IComparer" /> to perform the custom sort.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x001076E4 File Offset: 0x001058E4
		// (set) Token: 0x060042F5 RID: 17141 RVA: 0x001076EC File Offset: 0x001058EC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public IComparer TreeViewNodeSorter
		{
			get
			{
				return this.tree_view_node_sorter;
			}
			set
			{
				this.tree_view_node_sorter = value;
				if (this.tree_view_node_sorter != null)
				{
					this.Sort();
					this.sorted = true;
				}
			}
		}

		/// <summary>Gets the number of tree nodes that can be fully visible in the tree view control.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.TreeNode" /> items that can be fully visible in the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x00107710 File Offset: 0x00105910
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int VisibleCount
		{
			get
			{
				return this.ViewportRectangle.Height / this.ActualItemHeight;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer. The <see cref="P:System.Windows.Forms.TreeView.DoubleBuffered" /> property has no effect on the <see cref="T:System.Windows.Forms.TreeView" /> control. </summary>
		/// <returns>true if the control uses a secondary buffer; otherwise, false.</returns>
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x00107734 File Offset: 0x00105934
		// (set) Token: 0x060042F8 RID: 17144 RVA: 0x0010773C File Offset: 0x0010593C
		[EditorBrowsable(1)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Gets or sets the color of the lines connecting the nodes of the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the lines connecting the tree nodes.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x00107748 File Offset: 0x00105948
		// (set) Token: 0x060042FA RID: 17146 RVA: 0x0010779C File Offset: 0x0010599C
		[DefaultValue("Color [Black]")]
		public Color LineColor
		{
			get
			{
				if (this.line_color == Color.Empty)
				{
					Color color = ControlPaint.Dark(this.BackColor);
					if (color == this.BackColor)
					{
						color = ControlPaint.Light(this.BackColor);
					}
					return color;
				}
				return this.line_color;
			}
			set
			{
				this.line_color = value;
				if (this.show_lines)
				{
					this.CreateDashPen();
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key of the default image for each node in the <see cref="T:System.Windows.Forms.TreeView" /> control when it is in an unselected state.</summary>
		/// <returns>The key of the default image shown for each node <see cref="T:System.Windows.Forms.TreeView" /> control when the node is in an unselected state.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x001077BC File Offset: 0x001059BC
		// (set) Token: 0x060042FC RID: 17148 RVA: 0x001077C4 File Offset: 0x001059C4
		[RefreshProperties(2)]
		[Localizable(true)]
		[DefaultValue("")]
		[RelatedImageList("ImageList")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(ImageKeyConverter))]
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
				this.image_index = -1;
				this.image_key = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the key of the default image shown when a <see cref="T:System.Windows.Forms.TreeNode" /> is in a selected state.</summary>
		/// <returns>The key of the default image shown when a <see cref="T:System.Windows.Forms.TreeNode" /> is in a selected state.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x001077F8 File Offset: 0x001059F8
		// (set) Token: 0x060042FE RID: 17150 RVA: 0x00107800 File Offset: 0x00105A00
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(ImageKeyConverter))]
		[RelatedImageList("ImageList")]
		[Localizable(true)]
		[RefreshProperties(2)]
		public string SelectedImageKey
		{
			get
			{
				return this.selected_image_key;
			}
			set
			{
				if (this.selected_image_key == value)
				{
					return;
				}
				this.selected_image_index = -1;
				this.selected_image_key = value;
				this.UpdateNode(this.SelectedNode);
			}
		}

		/// <summary>Gets or sets the layout of the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x0010783C File Offset: 0x00105A3C
		// (set) Token: 0x06004300 RID: 17152 RVA: 0x00107844 File Offset: 0x00105A44
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets or sets the mode in which the control is drawn.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewDrawMode" /> values. The default is <see cref="F:System.Windows.Forms.TreeViewDrawMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TreeViewDrawMode" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x00107850 File Offset: 0x00105A50
		// (set) Token: 0x06004302 RID: 17154 RVA: 0x00107858 File Offset: 0x00105A58
		[DefaultValue(TreeViewDrawMode.Normal)]
		public TreeViewDrawMode DrawMode
		{
			get
			{
				return this.draw_mode;
			}
			set
			{
				this.draw_mode = value;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x00107864 File Offset: 0x00105A64
		internal ScrollBar UIAHScrollBar
		{
			get
			{
				return this.hbar;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x0010786C File Offset: 0x00105A6C
		internal ScrollBar UIAVScrollBar
		{
			get
			{
				return this.vbar;
			}
		}

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x00107874 File Offset: 0x00105A74
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x0010788C File Offset: 0x00105A8C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		/// <summary>Disables any redrawing of the tree view.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004307 RID: 17159 RVA: 0x00107898 File Offset: 0x00105A98
		public void BeginUpdate()
		{
			this.update_stack++;
		}

		/// <summary>Enables the redrawing of the tree view.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004308 RID: 17160 RVA: 0x001078A8 File Offset: 0x00105AA8
		public void EndUpdate()
		{
			if (this.update_stack > 1)
			{
				this.update_stack--;
			}
			else
			{
				this.update_stack = 0;
				if (this.update_needed)
				{
					this.RecalculateVisibleOrder(this.root_node);
					this.UpdateScrollBars(false);
					base.Invalidate(this.ViewportRectangle);
					this.update_needed = false;
				}
			}
		}

		/// <summary>Sorts the items in <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004309 RID: 17161 RVA: 0x0010790C File Offset: 0x00105B0C
		public void Sort()
		{
			IComparer comparer2;
			if (this.Nodes.Count >= 2)
			{
				IComparer comparer = this.tree_view_node_sorter;
				comparer2 = comparer;
			}
			else
			{
				comparer2 = null;
			}
			this.Sort(comparer2);
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x00107940 File Offset: 0x00105B40
		private void Sort(IComparer sorter)
		{
			this.sorted = true;
			this.Nodes.Sort(sorter);
			this.RecalculateVisibleOrder(this.root_node);
			this.UpdateScrollBars(false);
			base.Invalidate();
		}

		/// <summary>Expands all the tree nodes.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600430B RID: 17163 RVA: 0x0010797C File Offset: 0x00105B7C
		public void ExpandAll()
		{
			this.BeginUpdate();
			this.root_node.ExpandAll();
			this.EndUpdate();
			if (!base.IsHandleCreated)
			{
				return;
			}
			bool flag = false;
			foreach (object obj in this.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.Nodes.Count > 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			if (base.IsHandleCreated && this.vbar.VisibleInternal)
			{
				this.vbar.Value = this.vbar.Maximum - this.VisibleCount + 1;
			}
			else
			{
				this.RecalculateVisibleOrder(this.root_node);
				this.UpdateScrollBars(true);
				if (this.vbar.VisibleInternal)
				{
					this.SetTop(this.Nodes[this.Nodes.Count - 1]);
					this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
				}
			}
		}

		/// <summary>Collapses all the tree nodes.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600430C RID: 17164 RVA: 0x00107AC4 File Offset: 0x00105CC4
		public void CollapseAll()
		{
			this.BeginUpdate();
			this.root_node.CollapseAll();
			this.EndUpdate();
			if (this.vbar.VisibleInternal)
			{
				this.vbar.Value = this.vbar.Maximum - this.VisibleCount + 1;
			}
		}

		/// <summary>Retrieves the tree node that is at the specified point.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified point, in tree view (client) coordinates, or null if there is no node at that location.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to evaluate and retrieve the node from. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600430D RID: 17165 RVA: 0x00107B18 File Offset: 0x00105D18
		public TreeNode GetNodeAt(Point pt)
		{
			return this.GetNodeAt(pt.Y);
		}

		/// <summary>Retrieves the tree node at the point with the specified coordinates.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified location, in tree view (client) coordinates, or null if there is no node at that location.</returns>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> position to evaluate and retrieve the node from. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> position to evaluate and retrieve the node from. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600430E RID: 17166 RVA: 0x00107B28 File Offset: 0x00105D28
		public TreeNode GetNodeAt(int x, int y)
		{
			return this.GetNodeAt(y);
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x00107B34 File Offset: 0x00105D34
		private TreeNode GetNodeAtUseX(int x, int y)
		{
			TreeNode nodeAt = this.GetNodeAt(y);
			if (nodeAt == null || (!this.IsTextArea(nodeAt, x) && !this.full_row_select))
			{
				return null;
			}
			return nodeAt;
		}

		/// <summary>Retrieves the number of tree nodes, optionally including those in all subtrees, assigned to the tree view control.</summary>
		/// <returns>The number of tree nodes, optionally including those in all subtrees, assigned to the tree view control.</returns>
		/// <param name="includeSubTrees">true to count the <see cref="T:System.Windows.Forms.TreeNode" /> items that the subtrees contain; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004310 RID: 17168 RVA: 0x00107B6C File Offset: 0x00105D6C
		public int GetNodeCount(bool includeSubTrees)
		{
			return this.root_node.GetNodeCount(includeSubTrees);
		}

		/// <summary>Provides node information, given a point.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeViewHitTestInfo" />.</returns>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> at which to retrieve node information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004311 RID: 17169 RVA: 0x00107B7C File Offset: 0x00105D7C
		public TreeViewHitTestInfo HitTest(Point pt)
		{
			return this.HitTest(pt.X, pt.Y);
		}

		/// <summary>Provides node information, given x- and y-coordinates.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeViewHitTestInfo" />.</returns>
		/// <param name="x">The x-coordinate at which to retrieve node information </param>
		/// <param name="y">The y-coordinate at which to retrieve node information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004312 RID: 17170 RVA: 0x00107B94 File Offset: 0x00105D94
		public TreeViewHitTestInfo HitTest(int x, int y)
		{
			TreeNode nodeAt = this.GetNodeAt(y);
			if (nodeAt == null)
			{
				return new TreeViewHitTestInfo(null, TreeViewHitTestLocations.None);
			}
			if (this.IsTextArea(nodeAt, x))
			{
				return new TreeViewHitTestInfo(nodeAt, TreeViewHitTestLocations.Label);
			}
			if (this.IsPlusMinusArea(nodeAt, x))
			{
				return new TreeViewHitTestInfo(nodeAt, TreeViewHitTestLocations.PlusMinus);
			}
			if ((this.checkboxes || nodeAt.StateImage != null) && this.IsCheckboxArea(nodeAt, x))
			{
				return new TreeViewHitTestInfo(nodeAt, TreeViewHitTestLocations.StateImage);
			}
			if (x > nodeAt.Bounds.Right)
			{
				return new TreeViewHitTestInfo(nodeAt, TreeViewHitTestLocations.RightOfLabel);
			}
			if (this.IsImage(nodeAt, x))
			{
				return new TreeViewHitTestInfo(nodeAt, TreeViewHitTestLocations.Image);
			}
			return new TreeViewHitTestInfo(null, TreeViewHitTestLocations.Indent);
		}

		/// <summary>Overrides <see cref="M:System.ComponentModel.Component.ToString" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004313 RID: 17171 RVA: 0x00107C48 File Offset: 0x00105E48
		public override string ToString()
		{
			int count = this.Nodes.Count;
			if (count <= 0)
			{
				return base.ToString() + ", Nodes.Count: 0";
			}
			return string.Concat(new object[]
			{
				base.ToString(),
				", Nodes.Count: ",
				count,
				", Nodes[0]: ",
				this.Nodes[0]
			});
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x00107CB8 File Offset: 0x00105EB8
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.RecalculateVisibleOrder(this.root_node);
			this.UpdateScrollBars(false);
			if (this.pre_selected_node != null)
			{
				this.SelectedNode = this.pre_selected_node;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TreeView" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004315 RID: 17173 RVA: 0x00107CF8 File Offset: 0x00105EF8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.image_list = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Returns an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> for which to return an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</param>
		/// <param name="state">The visible state of the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		// Token: 0x06004316 RID: 17174 RVA: 0x00107D10 File Offset: 0x00105F10
		protected OwnerDrawPropertyBag GetItemRenderStyles(TreeNode node, int state)
		{
			return node.prop_bag;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the Keys values.</param>
		// Token: 0x06004317 RID: 17175 RVA: 0x00107D18 File Offset: 0x00105F18
		protected override bool IsInputKey(Keys keyData)
		{
			if (base.IsHandleCreated && (keyData & Keys.Alt) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				switch (keys)
				{
				case Keys.Escape:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.End:
				case Keys.Home:
					break;
				default:
					if (keys != Keys.Return)
					{
						goto IL_0081;
					}
					break;
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
					return true;
				}
				if (this.edit_node != null)
				{
					return true;
				}
			}
			IL_0081:
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06004318 RID: 17176 RVA: 0x00107DB0 File Offset: 0x00105FB0
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Keys keys = e.KeyData & Keys.KeyCode;
			switch (keys)
			{
			case Keys.PageUp:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					int visibleCount = this.VisibleCount;
					int num = 0;
					while (num < visibleCount && openTreeNodeEnumerator.MovePrevious())
					{
						num++;
					}
					this.selection_action = TreeViewAction.ByKeyboard;
					this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				break;
			case Keys.PageDown:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					int visibleCount2 = this.VisibleCount;
					int num2 = 0;
					while (num2 < visibleCount2 && openTreeNodeEnumerator.MoveNext())
					{
						num2++;
					}
					this.selection_action = TreeViewAction.ByKeyboard;
					this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				break;
			case Keys.End:
				if (this.root_node.Nodes.Count > 0)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node.Nodes[0]);
					while (openTreeNodeEnumerator.MoveNext())
					{
					}
					this.selection_action = TreeViewAction.ByKeyboard;
					this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
				}
				break;
			case Keys.Home:
				if (this.root_node.Nodes.Count > 0)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node.Nodes[0]);
					if (openTreeNodeEnumerator.MoveNext())
					{
						this.selection_action = TreeViewAction.ByKeyboard;
						this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
					}
				}
				break;
			case Keys.Left:
				if (this.selected_node != null)
				{
					if (this.selected_node.IsExpanded && this.selected_node.Nodes.Count > 0)
					{
						this.selected_node.Collapse();
					}
					else
					{
						TreeNode parent = this.selected_node.Parent;
						if (parent != null)
						{
							this.selection_action = TreeViewAction.ByKeyboard;
							this.SelectedNode = parent;
						}
					}
				}
				break;
			case Keys.Up:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					if (openTreeNodeEnumerator.MovePrevious() && openTreeNodeEnumerator.MovePrevious())
					{
						this.selection_action = TreeViewAction.ByKeyboard;
						this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
					}
				}
				break;
			case Keys.Right:
				if (this.selected_node != null)
				{
					if (!this.selected_node.IsExpanded)
					{
						this.selected_node.Expand();
					}
					else
					{
						TreeNode firstNode = this.selected_node.FirstNode;
						if (firstNode != null)
						{
							this.SelectedNode = firstNode;
						}
					}
				}
				break;
			case Keys.Down:
				if (this.selected_node != null)
				{
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.selected_node);
					if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
					{
						this.selection_action = TreeViewAction.ByKeyboard;
						this.SelectedNode = openTreeNodeEnumerator.CurrentNode;
					}
				}
				break;
			default:
				switch (keys)
				{
				case Keys.Multiply:
					if (this.selected_node != null)
					{
						this.selected_node.ExpandAll();
					}
					break;
				case Keys.Add:
					if (this.selected_node != null && this.selected_node.IsExpanded)
					{
						this.selected_node.Expand();
					}
					break;
				case Keys.Subtract:
					if (this.selected_node != null && this.selected_node.IsExpanded)
					{
						this.selected_node.Collapse();
					}
					break;
				}
				break;
			}
			base.OnKeyDown(e);
			if (!e.Handled && this.checkboxes && this.selected_node != null && (e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				this.selected_node.check_reason = TreeViewAction.ByKeyboard;
				this.selected_node.Checked = !this.selected_node.Checked;
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06004319 RID: 17177 RVA: 0x0010817C File Offset: 0x0010637C
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.KeyChar == ' ')
			{
				e.Handled = true;
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnKeyUp(System.Windows.Forms.KeyEventArgs)" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x0600431A RID: 17178 RVA: 0x0010819C File Offset: 0x0010639C
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if ((e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600431B RID: 17179 RVA: 0x001081C0 File Offset: 0x001063C0
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			this.is_hovering = true;
			TreeNode nodeAt = this.GetNodeAt(base.PointToClient(Control.MousePosition));
			if (nodeAt != null)
			{
				this.MouseEnteredItem(nodeAt);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600431C RID: 17180 RVA: 0x001081FC File Offset: 0x001063FC
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.is_hovering = false;
			if (this.tooltip_currently_showing != null)
			{
				this.MouseLeftItem(this.tooltip_currently_showing);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data. </param>
		// Token: 0x0600431D RID: 17181 RVA: 0x00108224 File Offset: 0x00106424
		protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler = (TreeNodeMouseClickEventHandler)base.Events[TreeView.NodeMouseClickEvent];
			if (treeNodeMouseClickEventHandler != null)
			{
				treeNodeMouseClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data. </param>
		// Token: 0x0600431E RID: 17182 RVA: 0x00108258 File Offset: 0x00106458
		protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
		{
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler = (TreeNodeMouseClickEventHandler)base.Events[TreeView.NodeMouseDoubleClickEvent];
			if (treeNodeMouseClickEventHandler != null)
			{
				treeNodeMouseClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event. </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> that contains the event data.</param>
		// Token: 0x0600431F RID: 17183 RVA: 0x0010828C File Offset: 0x0010648C
		protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e)
		{
			TreeNodeMouseHoverEventHandler treeNodeMouseHoverEventHandler = (TreeNodeMouseHoverEventHandler)base.Events[TreeView.NodeMouseHoverEvent];
			if (treeNodeMouseHoverEventHandler != null)
			{
				treeNodeMouseHoverEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.ItemDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> that contains the event data. </param>
		// Token: 0x06004320 RID: 17184 RVA: 0x001082C0 File Offset: 0x001064C0
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			ItemDragEventHandler itemDragEventHandler = (ItemDragEventHandler)base.Events[TreeView.ItemDragEvent];
			if (itemDragEventHandler != null)
			{
				itemDragEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> that contains the event data. </param>
		// Token: 0x06004321 RID: 17185 RVA: 0x001082F4 File Offset: 0x001064F4
		protected virtual void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			DrawTreeNodeEventHandler drawTreeNodeEventHandler = (DrawTreeNodeEventHandler)base.Events[TreeView.DrawNodeEvent];
			if (drawTreeNodeEventHandler != null)
			{
				drawTreeNodeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004322 RID: 17186 RVA: 0x00108328 File Offset: 0x00106528
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TreeView.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x06004323 RID: 17187 RVA: 0x0010835C File Offset: 0x0010655C
		protected internal virtual void OnAfterCheck(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterCheckEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x06004324 RID: 17188 RVA: 0x00108390 File Offset: 0x00106590
		protected internal virtual void OnAfterCollapse(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterCollapseEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterExpand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x06004325 RID: 17189 RVA: 0x001083C4 File Offset: 0x001065C4
		protected internal virtual void OnAfterExpand(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterExpandEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x06004326 RID: 17190 RVA: 0x001083F8 File Offset: 0x001065F8
		protected virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e)
		{
			NodeLabelEditEventHandler nodeLabelEditEventHandler = (NodeLabelEditEventHandler)base.Events[TreeView.AfterLabelEditEvent];
			if (nodeLabelEditEventHandler != null)
			{
				nodeLabelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
		// Token: 0x06004327 RID: 17191 RVA: 0x0010842C File Offset: 0x0010662C
		protected virtual void OnAfterSelect(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.AfterSelectEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06004328 RID: 17192 RVA: 0x00108460 File Offset: 0x00106660
		protected internal virtual void OnBeforeCheck(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeCheckEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06004329 RID: 17193 RVA: 0x00108494 File Offset: 0x00106694
		protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeCollapseEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x0600432A RID: 17194 RVA: 0x001084C8 File Offset: 0x001066C8
		protected internal virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeExpandEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x0600432B RID: 17195 RVA: 0x001084FC File Offset: 0x001066FC
		protected virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
		{
			NodeLabelEditEventHandler nodeLabelEditEventHandler = (NodeLabelEditEventHandler)base.Events[TreeView.BeforeLabelEditEvent];
			if (nodeLabelEditEventHandler != null)
			{
				nodeLabelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x0600432C RID: 17196 RVA: 0x00108530 File Offset: 0x00106730
		protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			TreeViewCancelEventHandler treeViewCancelEventHandler = (TreeViewCancelEventHandler)base.Events[TreeView.BeforeSelectEvent];
			if (treeViewCancelEventHandler != null)
			{
				treeViewCancelEventHandler(this, e);
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600432D RID: 17197 RVA: 0x00108564 File Offset: 0x00106764
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600432E RID: 17198 RVA: 0x00108570 File Offset: 0x00106770
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x0600432F RID: 17199 RVA: 0x0010857C File Offset: 0x0010677C
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_CONTEXTMENU)
			{
				if (msg == Msg.WM_LBUTTONDBLCLK)
				{
					int num = m.LParam.ToInt32();
					this.DoubleClickHandler(null, new MouseEventArgs(MouseButtons.Left, 2, num & 65535, (num >> 16) & 65535, 0));
				}
			}
			else if (this.WmContextMenu(ref m))
			{
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x001085FC File Offset: 0x001067FC
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x00108600 File Offset: 0x00106800
		internal IntPtr CreateNodeHandle()
		{
			long num;
			this.handle_count = (num = this.handle_count) + 1L;
			return (IntPtr)num;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x00108624 File Offset: 0x00106824
		internal override void HandleClick(int clicks, MouseEventArgs me)
		{
			if (this.GetNodeAt(me.Location) != null)
			{
				if (clicks > 1 && base.GetStyle(ControlStyles.StandardDoubleClick))
				{
					this.OnDoubleClick(me);
					this.OnMouseDoubleClick(me);
				}
				else
				{
					this.OnClick(me);
					this.OnMouseClick(me);
				}
			}
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x0010867C File Offset: 0x0010687C
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x00108680 File Offset: 0x00106880
		internal TreeNode NodeFromHandle(IntPtr handle)
		{
			return this.NodeFromHandleRecursive(this.root_node, handle);
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x00108690 File Offset: 0x00106890
		private TreeNode NodeFromHandleRecursive(TreeNode node, IntPtr handle)
		{
			if (node.handle == handle)
			{
				return node;
			}
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				TreeNode treeNode2 = this.NodeFromHandleRecursive(treeNode, handle);
				if (treeNode2 != null)
				{
					return treeNode2;
				}
			}
			return null;
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x00108728 File Offset: 0x00106928
		internal Rectangle ViewportRectangle
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				if (this.vbar != null && this.vbar.Visible)
				{
					clientRectangle.Width -= this.vbar.Width;
				}
				if (this.hbar != null && this.hbar.Visible)
				{
					clientRectangle.Height -= this.hbar.Height;
				}
				return clientRectangle;
			}
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x001087A8 File Offset: 0x001069A8
		private TreeNode GetNodeAt(int y)
		{
			if (this.nodes.Count <= 0)
			{
				return null;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.TopNode);
			int num = y / this.ActualItemHeight;
			for (int i = -1; i < num; i++)
			{
				if (!openTreeNodeEnumerator.MoveNext())
				{
					return null;
				}
			}
			return openTreeNodeEnumerator.CurrentNode;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x00108804 File Offset: 0x00106A04
		private bool IsTextArea(TreeNode node, int x)
		{
			return node != null && node.Bounds.Left <= x && node.Bounds.Right >= x;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x00108844 File Offset: 0x00106A44
		private bool IsSelectableArea(TreeNode node, int x)
		{
			if (node == null)
			{
				return false;
			}
			int num = node.Bounds.Left;
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width;
			}
			return num <= x && node.Bounds.Right >= x;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x001088A8 File Offset: 0x00106AA8
		private bool IsPlusMinusArea(TreeNode node, int x)
		{
			if (node.Nodes.Count == 0 || (node.parent == this.root_node && !this.show_root_lines))
			{
				return false;
			}
			int num = node.Bounds.Left + 5;
			if (this.show_root_lines || node.Parent != null)
			{
				num -= this.indent;
			}
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width + 3;
			}
			if (this.checkboxes)
			{
				num -= 19;
			}
			else if (node.StateImage != null)
			{
				num -= 19;
			}
			return x > num && x < num + 8;
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0010896C File Offset: 0x00106B6C
		private bool IsCheckboxArea(TreeNode node, int x)
		{
			int num = this.CheckBoxLeft(node);
			return x > num && x < num + 10;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x00108994 File Offset: 0x00106B94
		private bool IsImage(TreeNode node, int x)
		{
			if (this.ImageList == null)
			{
				return false;
			}
			int num = node.Bounds.Left;
			num -= this.ImageList.ImageSize.Width + 5;
			return x >= num && x <= num + this.ImageList.ImageSize.Width + 5;
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x001089FC File Offset: 0x00106BFC
		private int CheckBoxLeft(TreeNode node)
		{
			int num = node.Bounds.Left + 5;
			if (this.show_root_lines || node.Parent != null)
			{
				num -= this.indent;
			}
			if (!this.show_root_lines && node.Parent == null)
			{
				num -= this.indent;
			}
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width + 3;
			}
			return num;
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x00108A7C File Offset: 0x00106C7C
		internal void RecalculateVisibleOrder(TreeNode start)
		{
			if (this.update_stack > 0)
			{
				return;
			}
			int num;
			if (start == null)
			{
				start = this.root_node;
				num = 0;
			}
			else
			{
				num = start.visible_order;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(start);
			while (openTreeNodeEnumerator.MoveNext())
			{
				openTreeNodeEnumerator.CurrentNode.visible_order = num;
				num++;
			}
			this.max_visible_order = num;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x00108AE0 File Offset: 0x00106CE0
		internal void SetTop(TreeNode node)
		{
			int num = 0;
			if (node != null)
			{
				num = Math.Max(0, node.visible_order - 1);
			}
			if (!this.vbar.is_visible)
			{
				this.skipped_nodes = num;
				return;
			}
			this.vbar.Value = Math.Min(num, this.vbar.Maximum - this.VisibleCount + 1);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x00108B44 File Offset: 0x00106D44
		internal void SetBottom(TreeNode node)
		{
			if (!this.vbar.is_visible)
			{
				return;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(node);
			int bottom = this.ViewportRectangle.Bottom;
			int num = 0;
			while (openTreeNodeEnumerator.MovePrevious())
			{
				if (openTreeNodeEnumerator.CurrentNode.Bounds.Bottom <= bottom)
				{
					break;
				}
				num++;
			}
			int num2 = this.vbar.Value + num;
			if (this.vbar.Value + num < this.vbar.Maximum)
			{
				this.vbar.Value = num2;
			}
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x00108BEC File Offset: 0x00106DEC
		internal void UpdateBelow(TreeNode node)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			if (node == this.root_node)
			{
				base.Invalidate(this.ViewportRectangle);
				return;
			}
			int num = Math.Max(node.Bounds.Top - 1, 0);
			Rectangle rectangle;
			rectangle..ctor(0, num, base.Width, base.Height - num);
			base.Invalidate(rectangle);
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x00108C5C File Offset: 0x00106E5C
		internal void UpdateNode(TreeNode node)
		{
			if (node == null)
			{
				return;
			}
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			if (node == this.root_node)
			{
				base.Invalidate();
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(0, node.Bounds.Top - 1, base.Width, node.Bounds.Height + 1);
			base.Invalidate(rectangle);
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x00108CCC File Offset: 0x00106ECC
		internal void UpdateNodePlusMinus(TreeNode node)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			int num = node.Bounds.Left + 5;
			if (this.show_root_lines || node.Parent != null)
			{
				num -= this.indent;
			}
			if (this.ImageList != null)
			{
				num -= this.ImageList.ImageSize.Width + 3;
			}
			if (this.checkboxes)
			{
				num -= 19;
			}
			base.Invalidate(new Rectangle(num, node.Bounds.Top, 8, node.Bounds.Height));
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x00108D7C File Offset: 0x00106F7C
		internal override void OnPaintInternal(PaintEventArgs pe)
		{
			this.Draw(pe.ClipRectangle, pe.Graphics);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x00108D90 File Offset: 0x00106F90
		internal void CreateDashPen()
		{
			this.dash = new Pen(this.LineColor, 1f);
			this.dash.DashStyle = 2;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x00108DC0 File Offset: 0x00106FC0
		private void Draw(Rectangle clip, Graphics dc)
		{
			dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), clip);
			if (this.dash == null)
			{
				this.CreateDashPen();
			}
			Rectangle viewportRectangle = this.ViewportRectangle;
			Rectangle rectangle = clip;
			if (clip.Bottom > viewportRectangle.Bottom)
			{
				clip.Height = viewportRectangle.Bottom - clip.Top;
			}
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.TopNode);
			while (openTreeNodeEnumerator.MoveNext())
			{
				TreeNode currentNode = openTreeNodeEnumerator.CurrentNode;
				if (currentNode.GetY() + this.ActualItemHeight >= clip.Top)
				{
					if (currentNode.GetY() > clip.Bottom)
					{
						break;
					}
					this.DrawTreeNode(currentNode, dc, clip);
				}
			}
			if (this.hbar.Visible && this.vbar.Visible)
			{
				Rectangle rectangle2;
				rectangle2..ctor(this.hbar.Right, this.vbar.Bottom, this.vbar.Width, this.hbar.Height);
				if (rectangle.IntersectsWith(rectangle2))
				{
					dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorControl), rectangle2);
				}
			}
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x00108F10 File Offset: 0x00107110
		private void DrawNodeState(TreeNode node, Graphics dc, int x, int y)
		{
			if (node.Checked)
			{
				if (this.StateImageList.Images[1] != null)
				{
					dc.DrawImage(this.StateImageList.Images[1], new Rectangle(x, y, 16, 16));
				}
			}
			else if (this.StateImageList.Images[0] != null)
			{
				dc.DrawImage(this.StateImageList.Images[0], new Rectangle(x, y, 16, 16));
			}
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x00108FA0 File Offset: 0x001071A0
		private void DrawNodeCheckBox(TreeNode node, Graphics dc, int x, int middle)
		{
			Pen sizedPen = ThemeEngine.Current.ResPool.GetSizedPen(Color.Black, 2);
			dc.DrawRectangle(sizedPen, x + 3, middle - 4, 11, 11);
			if (node.Checked)
			{
				Pen pen = ThemeEngine.Current.ResPool.GetPen(Color.Black);
				int num = 5;
				int num2 = 3;
				Rectangle rectangle;
				rectangle..ctor(x + 4, middle - 3, num, num);
				for (int i = 0; i < num2; i++)
				{
					dc.DrawLine(pen, rectangle.Left + 1, rectangle.Top + num2 + i, rectangle.Left + 3, rectangle.Top + 5 + i);
					dc.DrawLine(pen, rectangle.Left + 3, rectangle.Top + 5 + i, rectangle.Left + 7, rectangle.Top + 1 + i);
				}
			}
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x00109084 File Offset: 0x00107284
		private void DrawNodeLines(TreeNode node, Graphics dc, Rectangle clip, Pen dash, int x, int y, int middle)
		{
			int num = 9;
			int num2 = 0;
			if (node.nodes.Count > 0 && this.show_plus_minus)
			{
				num = 13;
			}
			if (this.checkboxes)
			{
				num2 = 3;
			}
			if (this.show_root_lines || node.Parent != null)
			{
				dc.DrawLine(dash, x - this.indent + num, middle, x + num2, middle);
			}
			if (node.PrevNode != null || node.Parent != null)
			{
				num = 9;
				dc.DrawLine(dash, x - this.indent + num, node.Bounds.Top, x - this.indent + num, middle - ((!this.show_plus_minus || node.Nodes.Count <= 0) ? 0 : 4));
			}
			if (node.NextNode != null)
			{
				num = 9;
				dc.DrawLine(dash, x - this.indent + num, middle + ((!this.show_plus_minus || node.Nodes.Count <= 0) ? 0 : 4), x - this.indent + num, node.Bounds.Bottom);
			}
			num = 0;
			if (this.show_plus_minus)
			{
				num = 9;
			}
			for (TreeNode treeNode = node.Parent; treeNode != null; treeNode = treeNode.Parent)
			{
				if (treeNode.NextNode != null)
				{
					int num3 = treeNode.GetLinesX() - this.indent + num;
					dc.DrawLine(dash, num3, node.Bounds.Top, num3, node.Bounds.Bottom);
				}
			}
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x0010922C File Offset: 0x0010742C
		private void DrawNodeImage(TreeNode node, Graphics dc, Rectangle clip, int x, int y)
		{
			if (!this.RectsIntersect(clip, x, y, this.ImageList.ImageSize.Width, this.ImageList.ImageSize.Height))
			{
				return;
			}
			int image = node.Image;
			if (image > -1 && image < this.ImageList.Images.Count)
			{
				this.ImageList.Draw(dc, x, y, this.ImageList.ImageSize.Width, this.ImageList.ImageSize.Height, image);
			}
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x001092CC File Offset: 0x001074CC
		private void LabelEditFinished(object sender, EventArgs e)
		{
			this.EndEdit(this.edit_node);
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x001092DC File Offset: 0x001074DC
		internal void BeginEdit(TreeNode node)
		{
			if (this.edit_node != null)
			{
				this.EndEdit(this.edit_node);
			}
			if (this.edit_text_box == null)
			{
				this.edit_text_box = new LabelEditTextBox();
				this.edit_text_box.BorderStyle = BorderStyle.FixedSingle;
				this.edit_text_box.Visible = false;
				this.edit_text_box.EditingCancelled += new EventHandler(this.LabelEditCancelled);
				this.edit_text_box.EditingFinished += new EventHandler(this.LabelEditFinished);
				this.edit_text_box.TextChanged += new EventHandler(this.LabelTextChanged);
				base.Controls.Add(this.edit_text_box);
			}
			node.EnsureVisible();
			this.edit_text_box.Bounds = node.Bounds;
			this.edit_text_box.Text = node.Text;
			this.edit_text_box.Visible = true;
			this.edit_text_box.Focus();
			this.edit_text_box.SelectAll();
			this.edit_args = new NodeLabelEditEventArgs(node);
			this.OnBeforeLabelEdit(this.edit_args);
			this.edit_node = node;
			if (this.edit_args.CancelEdit)
			{
				this.edit_node = null;
				this.EndEdit(node);
			}
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x0010940C File Offset: 0x0010760C
		private void LabelEditCancelled(object sender, EventArgs e)
		{
			this.edit_args.SetLabel(null);
			this.EndEdit(this.edit_node);
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x00109428 File Offset: 0x00107628
		private void LabelTextChanged(object sender, EventArgs e)
		{
			int num = TextRenderer.MeasureTextInternal(this.edit_text_box.Text, this.edit_text_box.Font, false).Width + 4;
			this.edit_text_box.Width = num;
			if (this.edit_args != null)
			{
				this.edit_args.SetLabel(this.edit_text_box.Text);
			}
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x0010948C File Offset: 0x0010768C
		internal void EndEdit(TreeNode node)
		{
			if (this.edit_text_box != null && this.edit_text_box.Visible)
			{
				this.edit_text_box.Visible = false;
				base.Focus();
			}
			Application.DoEvents();
			if (this.edit_node != null && this.edit_node == node)
			{
				this.edit_node = null;
				NodeLabelEditEventArgs nodeLabelEditEventArgs = new NodeLabelEditEventArgs(this.edit_args.Node, this.edit_args.Label);
				this.OnAfterLabelEdit(nodeLabelEditEventArgs);
				if (nodeLabelEditEventArgs.CancelEdit)
				{
					return;
				}
				if (nodeLabelEditEventArgs.Label != null)
				{
					nodeLabelEditEventArgs.Node.Text = nodeLabelEditEventArgs.Label;
				}
			}
			this.edit_node = null;
			this.UpdateNode(node);
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x00109544 File Offset: 0x00107744
		internal void CancelEdit(TreeNode node)
		{
			this.edit_args.SetLabel(null);
			if (this.edit_text_box != null && this.edit_text_box.Visible)
			{
				this.edit_text_box.Visible = false;
				base.Focus();
			}
			this.edit_node = null;
			this.UpdateNode(node);
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x0010959C File Offset: 0x0010779C
		internal int GetNodeWidth(TreeNode node)
		{
			Font font = node.NodeFont;
			if (node.NodeFont == null)
			{
				font = this.Font;
			}
			return (int)TextRenderer.MeasureString(node.Text, font, 0, this.string_format).Width + 3;
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x001095E0 File Offset: 0x001077E0
		private void DrawSelectionAndFocus(TreeNode node, Graphics dc, Rectangle r)
		{
			if (this.Focused && this.focused_node == node && !this.full_row_select)
			{
				ControlPaint.DrawFocusRectangle(dc, r, this.ForeColor, this.BackColor);
			}
			if (this.draw_mode != TreeViewDrawMode.Normal)
			{
				return;
			}
			r.Inflate(-1, -1);
			if (this.Focused && node == this.highlighted_node)
			{
				Color color = ((node == this.selected_node || !(node.BackColor != Color.Empty)) ? ThemeEngine.Current.ColorHighlight : node.BackColor);
				dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(color), r);
			}
			else if (!this.hide_selection && node == this.highlighted_node)
			{
				dc.FillRectangle(SystemBrushes.Control, r);
			}
			else
			{
				Color color2 = ((node != this.selected_node) ? node.BackColor : this.BackColor);
				dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(color2), r);
			}
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x00109700 File Offset: 0x00107900
		private void DrawStaticNode(TreeNode node, Graphics dc)
		{
			if (!this.full_row_select || this.show_lines)
			{
				this.DrawSelectionAndFocus(node, dc, node.Bounds);
			}
			Font font = node.NodeFont;
			if (node.NodeFont == null)
			{
				font = this.Font;
			}
			Color color = ((!this.Focused || node != this.highlighted_node) ? node.ForeColor : ThemeEngine.Current.ColorHighlightText);
			if (color.IsEmpty)
			{
				color = this.ForeColor;
			}
			dc.DrawString(node.Text, font, ThemeEngine.Current.ResPool.GetSolidBrush(color), node.Bounds, this.string_format);
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x001097B8 File Offset: 0x001079B8
		private void DrawTreeNode(TreeNode node, Graphics dc, Rectangle clip)
		{
			int count = node.nodes.Count;
			int y = node.GetY();
			int num = y + this.ActualItemHeight / 2;
			if (this.full_row_select && !this.show_lines)
			{
				Rectangle rectangle;
				rectangle..ctor(1, y, this.ViewportRectangle.Width - 2, this.ActualItemHeight);
				this.DrawSelectionAndFocus(node, dc, rectangle);
			}
			if (this.draw_mode == TreeViewDrawMode.Normal || this.draw_mode == TreeViewDrawMode.OwnerDrawText)
			{
				if ((this.show_root_lines || node.Parent != null) && this.show_plus_minus && count > 0)
				{
					ThemeEngine.Current.TreeViewDrawNodePlusMinus(this, node, dc, node.GetLinesX() - this.Indent + 5, num);
				}
				if (this.checkboxes && this.state_image_list == null)
				{
					this.DrawNodeCheckBox(node, dc, this.CheckBoxLeft(node) - 3, num);
				}
				if (this.checkboxes && this.state_image_list != null)
				{
					this.DrawNodeState(node, dc, this.CheckBoxLeft(node) - 3, y);
				}
				if (!this.checkboxes && node.StateImage != null)
				{
					dc.DrawImage(node.StateImage, new Rectangle(this.CheckBoxLeft(node) - 3, y, 16, 16));
				}
				if (this.show_lines)
				{
					this.DrawNodeLines(node, dc, clip, this.dash, node.GetLinesX(), y, num);
				}
				if (this.ImageList != null)
				{
					this.DrawNodeImage(node, dc, clip, node.GetImageX(), y);
				}
			}
			if (this.draw_mode != TreeViewDrawMode.Normal)
			{
				dc.FillRectangle(Brushes.White, node.Bounds);
				TreeNodeStates treeNodeStates = TreeNodeStates.Default;
				if (node.IsSelected)
				{
					treeNodeStates = TreeNodeStates.Selected;
				}
				if (node.Checked)
				{
					treeNodeStates |= TreeNodeStates.Checked;
				}
				if (node == this.focused_node)
				{
					treeNodeStates |= TreeNodeStates.Focused;
				}
				Rectangle bounds = node.Bounds;
				if (this.draw_mode == TreeViewDrawMode.OwnerDrawText)
				{
					bounds.X += 3;
					bounds.Y++;
				}
				else
				{
					bounds.X = 0;
					bounds.Width = base.Width;
				}
				DrawTreeNodeEventArgs drawTreeNodeEventArgs = new DrawTreeNodeEventArgs(dc, node, bounds, treeNodeStates);
				this.OnDrawNode(drawTreeNodeEventArgs);
				if (!drawTreeNodeEventArgs.DrawDefault)
				{
					return;
				}
			}
			if (!node.IsEditing)
			{
				this.DrawStaticNode(node, dc);
			}
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x00109A14 File Offset: 0x00107C14
		internal void UpdateScrollBars(bool force)
		{
			if (!force && (base.IsDisposed || this.update_stack > 0 || !base.IsHandleCreated || !base.Visible))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = -1;
			int actualItemHeight = this.ActualItemHeight;
			if (this.scrollable)
			{
				OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this.root_node);
				while (openTreeNodeEnumerator.MoveNext())
				{
					int right = openTreeNodeEnumerator.CurrentNode.Bounds.Right;
					if (right > num2)
					{
						num2 = right;
					}
					num += actualItemHeight;
				}
				num -= actualItemHeight;
				num2 += this.hbar_offset;
				if (num > base.ClientRectangle.Height)
				{
					flag = true;
					if (num2 > base.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth)
					{
						flag2 = true;
					}
				}
				else if (num2 > base.ClientRectangle.Width)
				{
					flag2 = true;
				}
				if (!flag && flag2 && num > base.ClientRectangle.Height - SystemInformation.HorizontalScrollBarHeight)
				{
					flag = true;
				}
			}
			if (flag)
			{
				int num3 = ((!flag2) ? base.ClientRectangle.Height : (base.ClientRectangle.Height - this.hbar.Height));
				this.vbar.SetValues(Math.Max(0, this.max_visible_order - 2), num3 / this.ActualItemHeight);
				if (!this.vbar_bounds_set)
				{
					this.vbar.Bounds = new Rectangle(base.ClientRectangle.Width - this.vbar.Width, 0, this.vbar.Width, base.ClientRectangle.Height - ((!flag2) ? 0 : SystemInformation.VerticalScrollBarWidth));
					this.vbar_bounds_set = true;
					this.hbar_bounds_set = false;
				}
				this.vbar.Visible = true;
				if (this.skipped_nodes > 0)
				{
					int num4 = Math.Min(this.skipped_nodes, this.vbar.Maximum - this.VisibleCount + 1);
					this.skipped_nodes = 0;
					this.vbar.SafeValueSet(num4);
					this.skipped_nodes = num4;
				}
			}
			else
			{
				this.skipped_nodes = 0;
				this.RecalculateVisibleOrder(this.root_node);
				this.vbar.Visible = false;
				this.vbar.Value = 0;
				this.vbar_bounds_set = false;
			}
			if (flag2)
			{
				this.hbar.SetValues(num2 + 1, base.ClientRectangle.Width - ((!flag) ? 0 : SystemInformation.VerticalScrollBarWidth));
				if (!this.hbar_bounds_set)
				{
					this.hbar.Bounds = new Rectangle(0, base.ClientRectangle.Height - this.hbar.Height, base.ClientRectangle.Width - ((!flag) ? 0 : SystemInformation.VerticalScrollBarWidth), this.hbar.Height);
					this.hbar_bounds_set = true;
				}
				this.hbar.Visible = true;
			}
			else
			{
				this.hbar_offset = 0;
				this.hbar.Visible = false;
				this.hbar_bounds_set = false;
			}
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x00109D64 File Offset: 0x00107F64
		private void SizeChangedHandler(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				if (this.max_visible_order == -1)
				{
					this.RecalculateVisibleOrder(this.root_node);
				}
				this.UpdateScrollBars(false);
			}
			if (this.vbar.Visible)
			{
				this.vbar.Bounds = new Rectangle(base.ClientRectangle.Width - this.vbar.Width, 0, this.vbar.Width, base.ClientRectangle.Height - ((!this.hbar.Visible) ? 0 : SystemInformation.HorizontalScrollBarHeight));
			}
			if (this.hbar.Visible)
			{
				this.hbar.Bounds = new Rectangle(0, base.ClientRectangle.Height - this.hbar.Height, base.ClientRectangle.Width - ((!this.vbar.Visible) ? 0 : SystemInformation.VerticalScrollBarWidth), this.hbar.Height);
			}
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x00109E7C File Offset: 0x0010807C
		private void VScrollBarValueChanged(object sender, EventArgs e)
		{
			this.EndEdit(this.edit_node);
			this.SetVScrollPos(this.vbar.Value, null);
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x00109E9C File Offset: 0x0010809C
		private void SetVScrollPos(int pos, TreeNode new_top)
		{
			if (!this.vbar.VisibleInternal)
			{
				return;
			}
			if (pos < 0)
			{
				pos = 0;
			}
			if (this.skipped_nodes == pos)
			{
				return;
			}
			int num = this.skipped_nodes - pos;
			this.skipped_nodes = pos;
			if (!base.IsHandleCreated)
			{
				return;
			}
			int num2 = num * this.ActualItemHeight;
			XplatUI.ScrollWindow(this.Handle, this.ViewportRectangle, 0, num2, false);
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x00109F0C File Offset: 0x0010810C
		private void HScrollBarValueChanged(object sender, EventArgs e)
		{
			this.EndEdit(this.edit_node);
			int num = this.hbar_offset;
			this.hbar_offset = this.hbar.Value;
			if (this.hbar_offset < 0)
			{
				this.hbar_offset = 0;
			}
			XplatUI.ScrollWindow(this.Handle, this.ViewportRectangle, num - this.hbar_offset, 0, false);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x00109F6C File Offset: 0x0010816C
		internal void ExpandBelow(TreeNode node, int count_to_next)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			int num = ((node.Bounds.Bottom < 0) ? 0 : node.Bounds.Bottom);
			Rectangle rectangle;
			rectangle..ctor(0, num, this.ViewportRectangle.Width, this.ViewportRectangle.Height - num);
			int num2 = count_to_next * this.ActualItemHeight;
			if (num2 > 0)
			{
				XplatUI.ScrollWindow(this.Handle, rectangle, 0, num2, false);
			}
			if (this.show_plus_minus)
			{
				base.Invalidate(new Rectangle(0, node.GetY(), base.Width, this.ActualItemHeight));
			}
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x0010A028 File Offset: 0x00108228
		internal void CollapseBelow(TreeNode node, int count_to_next)
		{
			if (this.update_stack > 0)
			{
				this.update_needed = true;
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(0, node.Bounds.Bottom, this.ViewportRectangle.Width, this.ViewportRectangle.Height - node.Bounds.Bottom);
			int num = count_to_next * this.ActualItemHeight;
			if (num > 0)
			{
				XplatUI.ScrollWindow(this.Handle, rectangle, 0, -num, false);
			}
			if (this.show_plus_minus)
			{
				base.Invalidate(new Rectangle(0, node.GetY(), base.Width, this.ActualItemHeight));
			}
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x0010A0D8 File Offset: 0x001082D8
		private void MouseWheelHandler(object sender, MouseEventArgs e)
		{
			if (this.vbar == null || !this.vbar.is_visible)
			{
				return;
			}
			if (e.Delta < 0)
			{
				this.vbar.Value = Math.Min(this.vbar.Value + SystemInformation.MouseWheelScrollLines, this.vbar.Maximum - this.VisibleCount + 1);
			}
			else
			{
				this.vbar.Value = Math.Max(0, this.vbar.Value - SystemInformation.MouseWheelScrollLines);
			}
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x0010A16C File Offset: 0x0010836C
		private void VisibleChangedHandler(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				this.UpdateScrollBars(false);
			}
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x0010A180 File Offset: 0x00108380
		private void FontChangedHandler(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				TreeNode topNode = this.TopNode;
				this.InvalidateNodeWidthRecursive(this.root_node);
				this.SetTop(topNode);
			}
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x0010A1B4 File Offset: 0x001083B4
		private void InvalidateNodeWidthRecursive(TreeNode node)
		{
			node.InvalidateWidth();
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.InvalidateNodeWidthRecursive(treeNode);
			}
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x0010A22C File Offset: 0x0010842C
		private void GotFocusHandler(object sender, EventArgs e)
		{
			if (this.selected_node == null)
			{
				if (this.pre_selected_node != null)
				{
					this.SelectedNode = this.pre_selected_node;
					return;
				}
				this.SelectedNode = this.TopNode;
			}
			else if (this.selected_node != null)
			{
				this.UpdateNode(this.selected_node);
			}
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x0010A284 File Offset: 0x00108484
		private void LostFocusHandler(object sender, EventArgs e)
		{
			this.UpdateNode(this.SelectedNode);
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x0010A294 File Offset: 0x00108494
		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				base.Focus();
			}
			TreeNode nodeAt = this.GetNodeAt(e.Y);
			if (nodeAt == null)
			{
				return;
			}
			this.mouse_click_node = nodeAt;
			if (this.show_plus_minus && this.IsPlusMinusArea(nodeAt, e.X) && e.Button == MouseButtons.Left)
			{
				nodeAt.Toggle();
				return;
			}
			if (this.checkboxes && this.IsCheckboxArea(nodeAt, e.X) && e.Button == MouseButtons.Left)
			{
				nodeAt.check_reason = TreeViewAction.ByMouse;
				nodeAt.Checked = !nodeAt.Checked;
				this.UpdateNode(nodeAt);
				return;
			}
			if (this.IsSelectableArea(nodeAt, e.X) || this.full_row_select)
			{
				TreeNode treeNode = this.highlighted_node;
				this.highlighted_node = nodeAt;
				if (this.label_edit && e.Clicks == 1 && this.highlighted_node == treeNode && e.Button == MouseButtons.Left)
				{
					this.BeginEdit(nodeAt);
				}
				else if (this.highlighted_node != this.focused_node)
				{
					Size dragSize = SystemInformation.DragSize;
					this.mouse_rect.X = e.X - dragSize.Width;
					this.mouse_rect.Y = e.Y - dragSize.Height;
					this.mouse_rect.Width = dragSize.Width * 2;
					this.mouse_rect.Height = dragSize.Height * 2;
					this.select_mmove = true;
				}
				base.Invalidate(this.highlighted_node.Bounds);
				if (treeNode != null)
				{
					base.Invalidate(this.Bloat(treeNode.Bounds));
				}
			}
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x0010A460 File Offset: 0x00108660
		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.GetNodeAt(e.Y);
			if (nodeAt != null && nodeAt == this.mouse_click_node)
			{
				if (e.Clicks == 2)
				{
					this.OnNodeMouseDoubleClick(new TreeNodeMouseClickEventArgs(nodeAt, e.Button, e.Clicks, e.X, e.Y));
				}
				else
				{
					this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(nodeAt, e.Button, e.Clicks, e.X, e.Y));
				}
			}
			this.mouse_click_node = null;
			this.drag_begin_x = -1;
			this.drag_begin_y = -1;
			if (!this.select_mmove)
			{
				return;
			}
			this.select_mmove = false;
			if (e.Button == MouseButtons.Right && this.selected_node != null)
			{
				base.Invalidate(this.highlighted_node.Bounds);
				this.highlighted_node = this.selected_node;
				base.Invalidate(this.selected_node.Bounds);
				return;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this.highlighted_node, false, TreeViewAction.ByMouse);
			this.OnBeforeSelect(treeViewCancelEventArgs);
			if (!treeViewCancelEventArgs.Cancel)
			{
				TreeNode treeNode = this.focused_node;
				TreeNode treeNode2 = this.highlighted_node;
				this.selected_node = this.highlighted_node;
				this.focused_node = this.highlighted_node;
				this.OnAfterSelect(new TreeViewEventArgs(this.selected_node, TreeViewAction.ByMouse));
				if (treeNode2 != null)
				{
					Rectangle rectangle;
					if (treeNode != null)
					{
						rectangle = Rectangle.Union(this.Bloat(treeNode.Bounds), this.Bloat(treeNode2.Bounds));
					}
					else
					{
						rectangle = this.Bloat(treeNode2.Bounds);
					}
					rectangle.X = 0;
					rectangle.Width = this.ViewportRectangle.Width;
					base.Invalidate(rectangle);
				}
			}
			else
			{
				if (this.highlighted_node != null)
				{
					base.Invalidate(this.highlighted_node.Bounds);
				}
				this.highlighted_node = this.focused_node;
				this.selected_node = this.focused_node;
				if (this.selected_node != null)
				{
					base.Invalidate(this.selected_node.Bounds);
				}
			}
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x0010A66C File Offset: 0x0010886C
		private void MouseMoveHandler(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.GetNodeAt(e.Location);
			if (nodeAt != this.tooltip_currently_showing)
			{
				this.MouseLeftItem(this.tooltip_currently_showing);
			}
			if (nodeAt != null && nodeAt != this.tooltip_currently_showing)
			{
				this.MouseEnteredItem(nodeAt);
			}
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				if (this.drag_begin_x == -1 && this.drag_begin_y == -1)
				{
					this.drag_begin_x = e.X;
					this.drag_begin_y = e.Y;
				}
				else
				{
					double num = Math.Pow((double)(this.drag_begin_x - e.X), 2.0);
					double num2 = Math.Pow((double)(this.drag_begin_y - e.Y), 2.0);
					double num3 = Math.Sqrt(num + num2);
					if (num3 > 3.0)
					{
						TreeNode nodeAtUseX = this.GetNodeAtUseX(e.X, e.Y);
						if (nodeAtUseX != null)
						{
							this.OnItemDrag(new ItemDragEventArgs(e.Button, nodeAtUseX));
						}
						this.drag_begin_x = -1;
						this.drag_begin_y = -1;
					}
				}
			}
			if (!this.select_mmove || this.mouse_rect.Contains(e.X, e.Y))
			{
				return;
			}
			base.Invalidate(this.highlighted_node.Bounds);
			if (this.selected_node != null)
			{
				base.Invalidate(this.selected_node.Bounds);
			}
			if (this.focused_node != null)
			{
				base.Invalidate(this.focused_node.Bounds);
			}
			this.highlighted_node = this.selected_node;
			this.focused_node = this.selected_node;
			this.select_mmove = false;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x0010A82C File Offset: 0x00108A2C
		private void DoubleClickHandler(object sender, MouseEventArgs e)
		{
			TreeNode nodeAtUseX = this.GetNodeAtUseX(e.X, e.Y);
			if (nodeAtUseX != null)
			{
				nodeAtUseX.Toggle();
			}
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x0010A858 File Offset: 0x00108A58
		private bool RectsIntersect(Rectangle r, int left, int top, int width, int height)
		{
			return r.Left <= left + width && r.Right >= left && r.Top <= top + height && r.Bottom >= top;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x0010A8A4 File Offset: 0x00108AA4
		private bool WmContextMenu(ref Message m)
		{
			Point point;
			point..ctor(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			TreeNode treeNode;
			if (point.X == -1 || point.Y == -1)
			{
				treeNode = this.SelectedNode;
				if (treeNode == null)
				{
					return false;
				}
				point..ctor(treeNode.Bounds.Left, treeNode.Bounds.Top + treeNode.Bounds.Height / 2);
			}
			else
			{
				point = base.PointToClient(point);
				treeNode = this.GetNodeAt(point);
				if (treeNode == null)
				{
					return false;
				}
			}
			if (treeNode.ContextMenu != null)
			{
				treeNode.ContextMenu.Show(this, point);
				return true;
			}
			if (treeNode.ContextMenuStrip != null)
			{
				treeNode.ContextMenuStrip.Show(this, point);
				return true;
			}
			return false;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x0010A994 File Offset: 0x00108B94
		private void MouseEnteredItem(TreeNode item)
		{
			this.tooltip_currently_showing = item;
			if (!this.is_hovering)
			{
				return;
			}
			if (this.ShowNodeToolTips && !string.IsNullOrEmpty(this.tooltip_currently_showing.ToolTipText))
			{
				this.ToolTipWindow.Present(this, this.tooltip_currently_showing.ToolTipText);
			}
			this.OnNodeMouseHover(new TreeNodeMouseHoverEventArgs(this.tooltip_currently_showing));
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x0010A9FC File Offset: 0x00108BFC
		private void MouseLeftItem(TreeNode item)
		{
			this.ToolTipWindow.Hide(this);
			this.tooltip_currently_showing = null;
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x0010AA14 File Offset: 0x00108C14
		private ToolTip ToolTipWindow
		{
			get
			{
				if (this.tooltip_window == null)
				{
					this.tooltip_window = new ToolTip();
				}
				return this.tooltip_window;
			}
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x0010AA34 File Offset: 0x00108C34
		internal void OnUIACheckBoxesChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TreeView.UIACheckBoxesChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x0010AA68 File Offset: 0x00108C68
		internal void OnUIALabelEditChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TreeView.UIALabelEditChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x0010AA9C File Offset: 0x00108C9C
		internal void OnUIANodeTextChanged(TreeViewEventArgs e)
		{
			TreeViewEventHandler treeViewEventHandler = (TreeViewEventHandler)base.Events[TreeView.UIANodeTextChangedEvent];
			if (treeViewEventHandler != null)
			{
				treeViewEventHandler(this, e);
			}
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x0010AAD0 File Offset: 0x00108CD0
		internal void OnUIACollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)base.Events[TreeView.UIACollectionChangedEvent];
			if (collectionChangeEventHandler != null)
			{
				if (sender == this.root_node)
				{
					sender = this;
				}
				collectionChangeEventHandler.Invoke(sender, e);
			}
		}

		// Token: 0x04001BF8 RID: 7160
		private string path_separator = "\\";

		// Token: 0x04001BF9 RID: 7161
		private int item_height = -1;

		// Token: 0x04001BFA RID: 7162
		private bool sorted;

		// Token: 0x04001BFB RID: 7163
		internal TreeNode root_node;

		// Token: 0x04001BFC RID: 7164
		internal bool nodes_added;

		// Token: 0x04001BFD RID: 7165
		private TreeNodeCollection nodes;

		// Token: 0x04001BFE RID: 7166
		private TreeViewAction selection_action;

		// Token: 0x04001BFF RID: 7167
		internal TreeNode selected_node;

		// Token: 0x04001C00 RID: 7168
		private TreeNode pre_selected_node;

		// Token: 0x04001C01 RID: 7169
		private TreeNode focused_node;

		// Token: 0x04001C02 RID: 7170
		internal TreeNode highlighted_node;

		// Token: 0x04001C03 RID: 7171
		private Rectangle mouse_rect;

		// Token: 0x04001C04 RID: 7172
		private bool select_mmove;

		// Token: 0x04001C05 RID: 7173
		private ImageList image_list;

		// Token: 0x04001C06 RID: 7174
		private int image_index = -1;

		// Token: 0x04001C07 RID: 7175
		private int selected_image_index = -1;

		// Token: 0x04001C08 RID: 7176
		private string image_key;

		// Token: 0x04001C09 RID: 7177
		private bool is_hovering;

		// Token: 0x04001C0A RID: 7178
		private TreeNode mouse_click_node;

		// Token: 0x04001C0B RID: 7179
		private bool right_to_left_layout;

		// Token: 0x04001C0C RID: 7180
		private string selected_image_key;

		// Token: 0x04001C0D RID: 7181
		private bool show_node_tool_tips;

		// Token: 0x04001C0E RID: 7182
		private ImageList state_image_list;

		// Token: 0x04001C0F RID: 7183
		private TreeNode tooltip_currently_showing;

		// Token: 0x04001C10 RID: 7184
		private ToolTip tooltip_window;

		// Token: 0x04001C11 RID: 7185
		private bool full_row_select;

		// Token: 0x04001C12 RID: 7186
		private bool hot_tracking;

		// Token: 0x04001C13 RID: 7187
		private int indent = 19;

		// Token: 0x04001C14 RID: 7188
		private NodeLabelEditEventArgs edit_args;

		// Token: 0x04001C15 RID: 7189
		private LabelEditTextBox edit_text_box;

		// Token: 0x04001C16 RID: 7190
		internal TreeNode edit_node;

		// Token: 0x04001C17 RID: 7191
		private bool checkboxes;

		// Token: 0x04001C18 RID: 7192
		private bool label_edit;

		// Token: 0x04001C19 RID: 7193
		private bool scrollable = true;

		// Token: 0x04001C1A RID: 7194
		private bool show_lines = true;

		// Token: 0x04001C1B RID: 7195
		private bool show_root_lines = true;

		// Token: 0x04001C1C RID: 7196
		private bool show_plus_minus = true;

		// Token: 0x04001C1D RID: 7197
		private bool hide_selection = true;

		// Token: 0x04001C1E RID: 7198
		private int max_visible_order = -1;

		// Token: 0x04001C1F RID: 7199
		internal VScrollBar vbar;

		// Token: 0x04001C20 RID: 7200
		internal HScrollBar hbar;

		// Token: 0x04001C21 RID: 7201
		private bool vbar_bounds_set;

		// Token: 0x04001C22 RID: 7202
		private bool hbar_bounds_set;

		// Token: 0x04001C23 RID: 7203
		internal int skipped_nodes;

		// Token: 0x04001C24 RID: 7204
		internal int hbar_offset;

		// Token: 0x04001C25 RID: 7205
		private int update_stack;

		// Token: 0x04001C26 RID: 7206
		private bool update_needed;

		// Token: 0x04001C27 RID: 7207
		private Pen dash;

		// Token: 0x04001C28 RID: 7208
		private Color line_color;

		// Token: 0x04001C29 RID: 7209
		private StringFormat string_format;

		// Token: 0x04001C2A RID: 7210
		private int drag_begin_x = -1;

		// Token: 0x04001C2B RID: 7211
		private int drag_begin_y = -1;

		// Token: 0x04001C2C RID: 7212
		private long handle_count = 1L;

		// Token: 0x04001C2D RID: 7213
		private TreeViewDrawMode draw_mode;

		// Token: 0x04001C2E RID: 7214
		private IComparer tree_view_node_sorter;
	}
}
