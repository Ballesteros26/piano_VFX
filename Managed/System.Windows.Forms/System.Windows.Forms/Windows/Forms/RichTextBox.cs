using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows rich text box control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B7 RID: 695
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.RichTextBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[ComVisible(true)]
	public class RichTextBox : TextBoxBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RichTextBox" /> class.</summary>
		// Token: 0x06002E1F RID: 11807 RVA: 0x000B1CB8 File Offset: 0x000AFEB8
		public RichTextBox()
		{
			this.accepts_return = true;
			this.auto_size = false;
			this.auto_word_select = false;
			this.bullet_indent = 0;
			base.MaxLength = int.MaxValue;
			this.margin_right = 0;
			this.zoom = 1f;
			base.Multiline = true;
			this.document.CRLFSize = 1;
			this.shortcuts_enabled = true;
			base.EnableLinks = true;
			this.richtext = true;
			this.rtf_style = new RichTextBox.RtfSectionStyle();
			this.rtf_section_stack = null;
			this.scrollbars = RichTextBoxScrollBars.Both;
			this.alignment = HorizontalAlignment.Left;
			base.LostFocus += new EventHandler(this.RichTextBox_LostFocus);
			base.GotFocus += new EventHandler(this.RichTextBox_GotFocus);
			this.BackColor = ThemeEngine.Current.ColorWindow;
			this.backcolor_set = false;
			this.language_option = RichTextBoxLanguageOptions.AutoFontSizeAdjust;
			this.rich_text_shortcuts_enabled = true;
			this.selection_back_color = Control.DefaultBackColor;
			this.ForeColor = ThemeEngine.Current.ColorWindowText;
			base.HScrolled += new EventHandler(this.RichTextBox_HScrolled);
			base.VScrolled += new EventHandler(this.RichTextBox_VScrolled);
			base.SetStyle(ControlStyles.StandardDoubleClick, false);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000B1DE4 File Offset: 0x000AFFE4
		// Note: this type is marked as 'beforefieldinit'.
		static RichTextBox()
		{
			RichTextBox.ContentsResizedEvent = new object();
			RichTextBox.HScrollEvent = new object();
			RichTextBox.ImeChangeEvent = new object();
			RichTextBox.LinkClickedEvent = new object();
			RichTextBox.ProtectedEvent = new object();
			RichTextBox.SelectionChangedEvent = new object();
			RichTextBox.VScrollEvent = new object();
			RichTextBox.ReservedRTFChars = new char[] { '\\', '{', '}' };
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RichTextBox.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002C8 RID: 712
		// (add) Token: 0x06002E21 RID: 11809 RVA: 0x000B1E54 File Offset: 0x000B0054
		// (remove) Token: 0x06002E22 RID: 11810 RVA: 0x000B1E60 File Offset: 0x000B0060
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RichTextBox.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002C9 RID: 713
		// (add) Token: 0x06002E23 RID: 11811 RVA: 0x000B1E6C File Offset: 0x000B006C
		// (remove) Token: 0x06002E24 RID: 11812 RVA: 0x000B1E78 File Offset: 0x000B0078
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when contents within the control are resized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002CA RID: 714
		// (add) Token: 0x06002E25 RID: 11813 RVA: 0x000B1E84 File Offset: 0x000B0084
		// (remove) Token: 0x06002E26 RID: 11814 RVA: 0x000B1E98 File Offset: 0x000B0098
		public event ContentsResizedEventHandler ContentsResized
		{
			add
			{
				base.Events.AddHandler(RichTextBox.ContentsResizedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.ContentsResizedEvent, value);
			}
		}

		/// <summary>Occurs when the user completes a drag-and-drop </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002CB RID: 715
		// (add) Token: 0x06002E27 RID: 11815 RVA: 0x000B1EAC File Offset: 0x000B00AC
		// (remove) Token: 0x06002E28 RID: 11816 RVA: 0x000B1EB8 File Offset: 0x000B00B8
		[Browsable(false)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				base.DragDrop += value;
			}
			remove
			{
				base.DragDrop -= value;
			}
		}

		/// <summary>Occurs when an object is dragged into the control's bounds.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002CC RID: 716
		// (add) Token: 0x06002E29 RID: 11817 RVA: 0x000B1EC4 File Offset: 0x000B00C4
		// (remove) Token: 0x06002E2A RID: 11818 RVA: 0x000B1ED0 File Offset: 0x000B00D0
		[Browsable(false)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				base.DragEnter += value;
			}
			remove
			{
				base.DragEnter -= value;
			}
		}

		/// <summary>Occurs when an object is dragged out of the control's bounds.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002CD RID: 717
		// (add) Token: 0x06002E2B RID: 11819 RVA: 0x000B1EDC File Offset: 0x000B00DC
		// (remove) Token: 0x06002E2C RID: 11820 RVA: 0x000B1EE8 File Offset: 0x000B00E8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler DragLeave
		{
			add
			{
				base.DragLeave += value;
			}
			remove
			{
				base.DragLeave -= value;
			}
		}

		/// <summary>Occurs when an object is dragged over the control's bounds.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002CE RID: 718
		// (add) Token: 0x06002E2D RID: 11821 RVA: 0x000B1EF4 File Offset: 0x000B00F4
		// (remove) Token: 0x06002E2E RID: 11822 RVA: 0x000B1F00 File Offset: 0x000B0100
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event DragEventHandler DragOver
		{
			add
			{
				base.DragOver += value;
			}
			remove
			{
				base.DragOver -= value;
			}
		}

		/// <summary>Occurs during a drag operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002CF RID: 719
		// (add) Token: 0x06002E2F RID: 11823 RVA: 0x000B1F0C File Offset: 0x000B010C
		// (remove) Token: 0x06002E30 RID: 11824 RVA: 0x000B1F18 File Offset: 0x000B0118
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.GiveFeedback += value;
			}
			remove
			{
				base.GiveFeedback -= value;
			}
		}

		/// <summary>Occurs when the user clicks the horizontal scroll bar of the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D0 RID: 720
		// (add) Token: 0x06002E31 RID: 11825 RVA: 0x000B1F24 File Offset: 0x000B0124
		// (remove) Token: 0x06002E32 RID: 11826 RVA: 0x000B1F38 File Offset: 0x000B0138
		public event EventHandler HScroll
		{
			add
			{
				base.Events.AddHandler(RichTextBox.HScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.HScrollEvent, value);
			}
		}

		/// <summary>Occurs when the user switches input methods on an Asian version of the Windows operating system.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D1 RID: 721
		// (add) Token: 0x06002E33 RID: 11827 RVA: 0x000B1F4C File Offset: 0x000B014C
		// (remove) Token: 0x06002E34 RID: 11828 RVA: 0x000B1F60 File Offset: 0x000B0160
		public event EventHandler ImeChange
		{
			add
			{
				base.Events.AddHandler(RichTextBox.ImeChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.ImeChangeEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks on a link within the text of the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D2 RID: 722
		// (add) Token: 0x06002E35 RID: 11829 RVA: 0x000B1F74 File Offset: 0x000B0174
		// (remove) Token: 0x06002E36 RID: 11830 RVA: 0x000B1F88 File Offset: 0x000B0188
		public event LinkClickedEventHandler LinkClicked
		{
			add
			{
				base.Events.AddHandler(RichTextBox.LinkClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.LinkClickedEvent, value);
			}
		}

		/// <summary>Occurs when the user attempts to modify protected text in the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D3 RID: 723
		// (add) Token: 0x06002E37 RID: 11831 RVA: 0x000B1F9C File Offset: 0x000B019C
		// (remove) Token: 0x06002E38 RID: 11832 RVA: 0x000B1FB0 File Offset: 0x000B01B0
		public event EventHandler Protected
		{
			add
			{
				base.Events.AddHandler(RichTextBox.ProtectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.ProtectedEvent, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D4 RID: 724
		// (add) Token: 0x06002E39 RID: 11833 RVA: 0x000B1FC4 File Offset: 0x000B01C4
		// (remove) Token: 0x06002E3A RID: 11834 RVA: 0x000B1FD0 File Offset: 0x000B01D0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.QueryContinueDrag += value;
			}
			remove
			{
				base.QueryContinueDrag -= value;
			}
		}

		/// <summary>Occurs when the selection of text within the control has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D5 RID: 725
		// (add) Token: 0x06002E3B RID: 11835 RVA: 0x000B1FDC File Offset: 0x000B01DC
		// (remove) Token: 0x06002E3C RID: 11836 RVA: 0x000B1FF0 File Offset: 0x000B01F0
		[MonoTODO("Event never raised")]
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(RichTextBox.SelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.SelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks the vertical scroll bars of the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D6 RID: 726
		// (add) Token: 0x06002E3D RID: 11837 RVA: 0x000B2004 File Offset: 0x000B0204
		// (remove) Token: 0x06002E3E RID: 11838 RVA: 0x000B2018 File Offset: 0x000B0218
		public event EventHandler VScroll
		{
			add
			{
				base.Events.AddHandler(RichTextBox.VScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.VScrollEvent, value);
			}
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000B202C File Offset: 0x000B022C
		internal override void HandleLinkClicked(TextBoxBase.LinkRectangle link)
		{
			this.OnLinkClicked(new LinkClickedEventArgs(link.LinkTag.LinkText));
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000B2044 File Offset: 0x000B0244
		internal override Color ChangeBackColor(Color backColor)
		{
			if (backColor == Color.Empty)
			{
				this.backcolor_set = false;
				if (!base.ReadOnly)
				{
					backColor = SystemColors.Window;
				}
			}
			return backColor;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000B207C File Offset: 0x000B027C
		internal override void RaiseSelectionChanged()
		{
			this.OnSelectionChanged(EventArgs.Empty);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000B208C File Offset: 0x000B028C
		private void RichTextBox_LostFocus(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000B2094 File Offset: 0x000B0294
		private void RichTextBox_GotFocus(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		/// <summary>Gets or sets a value indicating whether the control will enable drag-and-drop operations.</summary>
		/// <returns>true if drag-and-drop is enabled in the control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x000B209C File Offset: 0x000B029C
		// (set) Token: 0x06002E45 RID: 11845 RVA: 0x000B20A4 File Offset: 0x000B02A4
		[Browsable(false)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000B20B0 File Offset: 0x000B02B0
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x000B20B8 File Offset: 0x000B02B8
		[EditorBrowsable(1)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(1)]
		[RefreshProperties(2)]
		[Browsable(false)]
		public override bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether automatic word selection is enabled.</summary>
		/// <returns>true if automatic word selection is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000B20C4 File Offset: 0x000B02C4
		// (set) Token: 0x06002E49 RID: 11849 RVA: 0x000B20CC File Offset: 0x000B02CC
		[MonoTODO("Value not respected, always true")]
		[DefaultValue(false)]
		public bool AutoWordSelection
		{
			get
			{
				return this.auto_word_select;
			}
			set
			{
				this.auto_word_select = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000B20D8 File Offset: 0x000B02D8
		// (set) Token: 0x06002E4B RID: 11851 RVA: 0x000B20E0 File Offset: 0x000B02E0
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000B20EC File Offset: 0x000B02EC
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x000B20F4 File Offset: 0x000B02F4
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the indentation used in the <see cref="T:System.Windows.Forms.RichTextBox" /> control when the bullet style is applied to the text.</summary>
		/// <returns>The number of pixels inserted as the indentation after a bullet. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentException">The specified indentation was less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000B2100 File Offset: 0x000B0300
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x000B2108 File Offset: 0x000B0308
		[DefaultValue(0)]
		[Localizable(true)]
		public int BulletIndent
		{
			get
			{
				return this.bullet_indent;
			}
			set
			{
				this.bullet_indent = value;
			}
		}

		/// <summary>Gets a value indicating whether there are actions that have occurred within the <see cref="T:System.Windows.Forms.RichTextBox" /> that can be reapplied.</summary>
		/// <returns>true if there are operations that have been undone that can be reapplied to the content of the control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000B2114 File Offset: 0x000B0314
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool CanRedo
		{
			get
			{
				return this.document.undo.CanRedo;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not the <see cref="T:System.Windows.Forms.RichTextBox" /> will automatically format a Uniform Resource Locator (URL) when it is typed into the control.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.RichTextBox" /> will automatically format URLs that are typed into the control as a link; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000B2128 File Offset: 0x000B0328
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x000B2130 File Offset: 0x000B0330
		[DefaultValue(true)]
		public bool DetectUrls
		{
			get
			{
				return base.EnableLinks;
			}
			set
			{
				base.EnableLinks = value;
			}
		}

		/// <summary>Gets or sets a value that enables drag-and-drop operations on text, pictures, and other data.</summary>
		/// <returns>true to enable drag-and-drop operations; otherwise, false. The default is false.</returns>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000B213C File Offset: 0x000B033C
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000B2144 File Offset: 0x000B0344
		[DefaultValue(false)]
		[MonoTODO("Stub, does nothing")]
		public bool EnableAutoDragDrop
		{
			get
			{
				return this.enable_auto_drag_drop;
			}
			set
			{
				this.enable_auto_drag_drop = value;
			}
		}

		/// <summary>Gets or sets the font used when displaying text in the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000B2150 File Offset: 0x000B0350
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x000B2158 File Offset: 0x000B0358
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (this.font != value)
				{
					if (this.auto_size && base.PreferredHeight != base.Height)
					{
						base.Height = base.PreferredHeight;
					}
					base.Font = value;
					Line line = this.document.GetLine(1);
					Line line2 = this.document.GetLine(this.document.Lines);
					this.document.FormatText(line, 1, line2, line2.text.Length + 1, base.Font, Color.Empty, Color.Empty, FormatSpecified.Font);
				}
			}
		}

		/// <summary>Gets or sets the font color used when displaying text in the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the control's foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000B21F0 File Offset: 0x000B03F0
		// (set) Token: 0x06002E58 RID: 11864 RVA: 0x000B21F8 File Offset: 0x000B03F8
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

		/// <summary>Gets or sets a value that indicates <see cref="T:System.Windows.Forms.RichTextBox" /> settings for Input Method Editor (IME) and Asian language support.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RichTextBoxLanguageOptions" /> values. The default is <see cref="F:System.Windows.Forms.RichTextBoxLanguageOptions.AutoFontSizeAdjust" />.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000B2204 File Offset: 0x000B0404
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x000B220C File Offset: 0x000B040C
		[MonoTODO("Stub, does nothing")]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public RichTextBoxLanguageOptions LanguageOption
		{
			get
			{
				return this.language_option;
			}
			set
			{
				this.language_option = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the rich text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned to the property is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000B2218 File Offset: 0x000B0418
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x000B2220 File Offset: 0x000B0420
		[DefaultValue(2147483647)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
				base.MaxLength = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>true if the control is a multiline <see cref="T:System.Windows.Forms.RichTextBox" /> control; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000B222C File Offset: 0x000B042C
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x000B2234 File Offset: 0x000B0434
		[DefaultValue(true)]
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				base.Multiline = value;
			}
		}

		/// <summary>Gets the name of the action that can be reapplied to the control when the <see cref="M:System.Windows.Forms.RichTextBox.Redo" /> method is called.</summary>
		/// <returns>A string that represents the name of the action that will be performed when a call to the <see cref="M:System.Windows.Forms.RichTextBox.Redo" /> method is made.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000B2240 File Offset: 0x000B0440
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string RedoActionName
		{
			get
			{
				return this.document.undo.RedoActionName;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true if shortcut keys are enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000B2254 File Offset: 0x000B0454
		// (set) Token: 0x06002E61 RID: 11873 RVA: 0x000B225C File Offset: 0x000B045C
		[EditorBrowsable(1)]
		[MonoTODO("Stub, does nothing")]
		[Browsable(false)]
		[DefaultValue(true)]
		public bool RichTextShortcutsEnabled
		{
			get
			{
				return this.rich_text_shortcuts_enabled;
			}
			set
			{
				this.rich_text_shortcuts_enabled = value;
			}
		}

		/// <summary>Gets or sets the size of a single line of text within the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>The size, in pixels, of a single line of text in the control. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value was less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000B2268 File Offset: 0x000B0468
		// (set) Token: 0x06002E63 RID: 11875 RVA: 0x000B2270 File Offset: 0x000B0470
		[MonoInternalNote("Teach TextControl.RecalculateLine to consider the right margin as well")]
		[MonoTODO("Stub, does nothing")]
		[Localizable(true)]
		[DefaultValue(0)]
		public int RightMargin
		{
			get
			{
				return this.margin_right;
			}
			set
			{
				this.margin_right = value;
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.RichTextBox" /> control, including all rich text format (RTF) codes.</summary>
		/// <returns>The text of the control in RTF format.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x000B227C File Offset: 0x000B047C
		// (set) Token: 0x06002E65 RID: 11877 RVA: 0x000B22C8 File Offset: 0x000B04C8
		[Browsable(false)]
		[RefreshProperties(1)]
		[DesignerSerializationVisibility(0)]
		public string Rtf
		{
			get
			{
				Line line = this.document.GetLine(1);
				Line line2 = this.document.GetLine(this.document.Lines);
				return this.GenerateRTF(line, 0, line2, line2.text.Length).ToString();
			}
			set
			{
				this.document.Empty();
				MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(value), false);
				this.InsertRTFFromStream(memoryStream, 0, 1);
				memoryStream.Close();
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the type of scroll bars to display in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RichTextBoxScrollBars" /> values. The default is RichTextBoxScrollBars.Both.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not defined in the <see cref="T:System.Windows.Forms.RichTextBoxScrollBars" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000B2308 File Offset: 0x000B0508
		// (set) Token: 0x06002E67 RID: 11879 RVA: 0x000B2310 File Offset: 0x000B0510
		[Localizable(true)]
		[DefaultValue(RichTextBoxScrollBars.Both)]
		public RichTextBoxScrollBars ScrollBars
		{
			get
			{
				return this.scrollbars;
			}
			set
			{
				if (!Enum.IsDefined(typeof(RichTextBoxScrollBars), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(RichTextBoxScrollBars));
				}
				if (value != this.scrollbars)
				{
					this.scrollbars = value;
					base.CalculateDocument();
				}
			}
		}

		/// <summary>Gets or sets the currently selected rich text format (RTF) formatted text in the control.</summary>
		/// <returns>The selected RTF text in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000B2368 File Offset: 0x000B0568
		// (set) Token: 0x06002E69 RID: 11881 RVA: 0x000B23C0 File Offset: 0x000B05C0
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(0)]
		public string SelectedRtf
		{
			get
			{
				return this.GenerateRTF(this.document.selection_start.line, this.document.selection_start.pos, this.document.selection_end.line, this.document.selection_end.pos).ToString();
			}
			set
			{
				if (this.document.selection_visible)
				{
					this.document.ReplaceSelection(string.Empty, false);
				}
				int num = this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
				MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(value), false);
				int pos = this.document.selection_start.pos;
				int line_no = this.document.selection_start.line.line_no;
				if (pos == 0)
				{
					this.reuse_line = true;
				}
				int num2;
				int num3;
				int num4;
				this.InsertRTFFromStream(memoryStream, pos, line_no, out num2, out num3, out num4);
				memoryStream.Close();
				int num5 = this.document.LineEndingLength((!XplatUI.RunningOnUnix) ? LineEnding.Hard : LineEnding.Rich);
				Line line;
				LineTag lineTag;
				this.document.CharIndexToLineTag(num + num4 + (num3 - this.document.selection_start.line.line_no) * num5, out line, out lineTag, out num);
				if (num >= line.text.Length)
				{
					num = line.text.Length - 1;
				}
				this.document.SetSelection(line, num);
				this.document.PositionCaret(line, num);
				this.document.DisplayCaret();
				base.ScrollToCaret();
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the selected text within the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
		/// <returns>A string that represents the selected text in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000B2524 File Offset: 0x000B0724
		// (set) Token: 0x06002E6B RID: 11883 RVA: 0x000B252C File Offset: 0x000B072C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[DefaultValue("")]
		public override string SelectedText
		{
			get
			{
				return base.SelectedText;
			}
			set
			{
				base.Modified = true;
				base.SelectedText = value;
			}
		}

		/// <summary>Gets or sets the alignment to apply to the current selection or insertion point.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values defined in the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> class. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000B253C File Offset: 0x000B073C
		// (set) Token: 0x06002E6D RID: 11885 RVA: 0x000B25C0 File Offset: 0x000B07C0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment SelectionAlignment
		{
			get
			{
				Line line = this.document.ParagraphStart(this.document.selection_start.line);
				HorizontalAlignment alignment = line.alignment;
				Line line2 = this.document.ParagraphEnd(this.document.selection_end.line);
				Line line3 = line;
				while (line3.alignment == alignment)
				{
					if (line3 == line2)
					{
						return alignment;
					}
					line3 = this.document.GetLine(line3.line_no + 1);
				}
				return HorizontalAlignment.Left;
			}
			set
			{
				Line line = this.document.ParagraphStart(this.document.selection_start.line);
				Line line2 = this.document.ParagraphEnd(this.document.selection_end.line);
				Line line3 = line;
				for (;;)
				{
					line3.alignment = value;
					if (line3 == line2)
					{
						break;
					}
					line3 = this.document.GetLine(line3.line_no + 1);
				}
				base.CalculateDocument();
			}
		}

		/// <summary>Gets or sets the color of text when the text is selected in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the text color when the text is selected. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000B263C File Offset: 0x000B083C
		// (set) Token: 0x06002E6F RID: 11887 RVA: 0x000B2644 File Offset: 0x000B0844
		[MonoTODO("Stub, does nothing")]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Color SelectionBackColor
		{
			get
			{
				return this.selection_back_color;
			}
			set
			{
				this.selection_back_color = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the bullet style is applied to the current selection or insertion point.</summary>
		/// <returns>true if the current selection or insertion point has the bullet style applied; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000B2650 File Offset: 0x000B0850
		// (set) Token: 0x06002E71 RID: 11889 RVA: 0x000B2654 File Offset: 0x000B0854
		[DefaultValue(false)]
		[Browsable(false)]
		[MonoTODO("Stub, does nothing")]
		[DesignerSerializationVisibility(0)]
		public bool SelectionBullet
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets whether text in the control appears on the baseline, as a superscript, or as a subscript below the baseline.</summary>
		/// <returns>A number that specifies the character offset.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value was less than -2000 or greater than 2000. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000B2658 File Offset: 0x000B0858
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x000B265C File Offset: 0x000B085C
		[DefaultValue(0)]
		[Browsable(false)]
		[MonoTODO("Stub, does nothing")]
		[DesignerSerializationVisibility(0)]
		public int SelectionCharOffset
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the text color of the current text selection or insertion point.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to apply to the current text selection or to text entered after the insertion point.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000B2660 File Offset: 0x000B0860
		// (set) Token: 0x06002E75 RID: 11893 RVA: 0x000B2740 File Offset: 0x000B0940
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Color SelectionColor
		{
			get
			{
				LineTag lineTag;
				LineTag lineTag2;
				if (this.selection_length > 0)
				{
					lineTag = this.document.selection_start.line.FindTag(this.document.selection_start.pos + 1);
					lineTag2 = this.document.selection_start.line.FindTag(this.document.selection_end.pos);
				}
				else
				{
					lineTag = this.document.selection_start.line.FindTag(this.document.selection_start.pos);
					lineTag2 = lineTag;
				}
				Color color = lineTag.Color;
				for (LineTag lineTag3 = lineTag; lineTag3 != null; lineTag3 = this.document.NextTag(lineTag3))
				{
					if (!color.Equals(lineTag3.Color))
					{
						return Color.Empty;
					}
					if (lineTag3 == lineTag2)
					{
						break;
					}
				}
				return color;
			}
			set
			{
				if (value == Color.Empty)
				{
					value = Control.DefaultForeColor;
				}
				int num = this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
				int num2 = this.document.LineTagToCharIndex(this.document.selection_end.line, this.document.selection_end.pos);
				this.document.FormatText(this.document.selection_start.line, this.document.selection_start.pos + 1, this.document.selection_end.line, this.document.selection_end.pos + 1, null, value, Color.Empty, FormatSpecified.Color);
				this.document.CharIndexToLineTag(num, out this.document.selection_start.line, out this.document.selection_start.tag, out this.document.selection_start.pos);
				this.document.CharIndexToLineTag(num2, out this.document.selection_end.line, out this.document.selection_end.tag, out this.document.selection_end.pos);
				this.document.UpdateView(this.document.selection_start.line, 0);
				this.document.AlignCaret(false);
			}
		}

		/// <summary>Gets or sets the font of the current text selection or insertion point.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font to apply to the current text selection or to text entered after the insertion point.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000B28B4 File Offset: 0x000B0AB4
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x000B2998 File Offset: 0x000B0B98
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Font SelectionFont
		{
			get
			{
				LineTag lineTag;
				LineTag lineTag2;
				if (this.selection_length > 0)
				{
					lineTag = this.document.selection_start.line.FindTag(this.document.selection_start.pos + 1);
					lineTag2 = this.document.selection_start.line.FindTag(this.document.selection_end.pos);
				}
				else
				{
					lineTag = this.document.selection_start.line.FindTag(this.document.selection_start.pos);
					lineTag2 = lineTag;
				}
				Font font = lineTag.Font;
				if (this.selection_length > 1)
				{
					for (LineTag lineTag3 = lineTag; lineTag3 != null; lineTag3 = this.document.NextTag(lineTag3))
					{
						if (!font.Equals(lineTag3.Font))
						{
							return null;
						}
						if (lineTag3 == lineTag2)
						{
							break;
						}
					}
				}
				return font;
			}
			set
			{
				int num = this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
				int num2 = this.document.LineTagToCharIndex(this.document.selection_end.line, this.document.selection_end.pos);
				this.document.FormatText(this.document.selection_start.line, this.document.selection_start.pos + 1, this.document.selection_end.line, this.document.selection_end.pos + 1, value, Color.Empty, Color.Empty, FormatSpecified.Font);
				this.document.CharIndexToLineTag(num, out this.document.selection_start.line, out this.document.selection_start.tag, out this.document.selection_start.pos);
				this.document.CharIndexToLineTag(num2, out this.document.selection_end.line, out this.document.selection_end.tag, out this.document.selection_end.pos);
				this.document.UpdateView(this.document.selection_start.line, 0);
				base.Document.AlignCaret(false);
			}
		}

		/// <summary>Gets or sets the distance between the left edge of the first line of text in the selected paragraph and the left edge of subsequent lines in the same paragraph.</summary>
		/// <returns>The distance, in pixels, for the hanging indent applied to the current text selection or the insertion point.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000B2AF8 File Offset: 0x000B0CF8
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x000B2AFC File Offset: 0x000B0CFC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[MonoTODO("Stub, does nothing")]
		[DefaultValue(0)]
		public int SelectionHangingIndent
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the length, in pixels, of the indentation of the line where the selection starts.</summary>
		/// <returns>The current distance, in pixels, of the indentation applied to the left of the current text selection or the insertion point.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000B2B00 File Offset: 0x000B0D00
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x000B2B04 File Offset: 0x000B0D04
		[MonoTODO("Stub, does nothing")]
		[DesignerSerializationVisibility(0)]
		[DefaultValue(0)]
		[Browsable(false)]
		public int SelectionIndent
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the number of characters selected in control.</summary>
		/// <returns>The number of characters selected in the text box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000B2B08 File Offset: 0x000B0D08
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x000B2B10 File Offset: 0x000B0D10
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override int SelectionLength
		{
			get
			{
				return base.SelectionLength;
			}
			set
			{
				base.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current text selection is protected.</summary>
		/// <returns>true if the current selection prevents any changes to its content; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000B2B1C File Offset: 0x000B0D1C
		// (set) Token: 0x06002E7F RID: 11903 RVA: 0x000B2B20 File Offset: 0x000B0D20
		[DefaultValue(false)]
		[MonoTODO("Stub, does nothing")]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool SelectionProtected
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>The distance (in pixels) between the right edge of the <see cref="T:System.Windows.Forms.RichTextBox" /> control and the right edge of the text that is selected or added at the current insertion point.</summary>
		/// <returns>The indentation space, in pixels, at the right of the current selection or insertion point.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000B2B24 File Offset: 0x000B0D24
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x000B2B28 File Offset: 0x000B0D28
		[MonoTODO("Stub, does nothing")]
		[Browsable(false)]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(0)]
		public int SelectionRightIndent
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the absolute tab stop positions in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>An array in which each member specifies a tab offset, in pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The array has more than the maximum 32 elements. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000B2B2C File Offset: 0x000B0D2C
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x000B2B34 File Offset: 0x000B0D34
		[Browsable(false)]
		[MonoTODO("Stub, does nothing")]
		[DesignerSerializationVisibility(0)]
		public int[] SelectionTabs
		{
			get
			{
				return new int[0];
			}
			set
			{
			}
		}

		/// <summary>Gets the selection type within the control.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxSelectionTypes" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000B2B38 File Offset: 0x000B0D38
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public RichTextBoxSelectionTypes SelectionType
		{
			get
			{
				if (this.document.selection_start == this.document.selection_end)
				{
					return RichTextBoxSelectionTypes.Empty;
				}
				if (this.SelectedText.Length > 1)
				{
					return RichTextBoxSelectionTypes.Text | RichTextBoxSelectionTypes.MultiChar;
				}
				return RichTextBoxSelectionTypes.Text;
			}
		}

		/// <summary>Gets or sets a value indicating whether a selection margin is displayed in the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
		/// <returns>true if a selection margin is enabled in the control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000B2B7C File Offset: 0x000B0D7C
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000B2B80 File Offset: 0x000B0D80
		[DefaultValue(false)]
		[MonoTODO("Stub, does nothing")]
		public bool ShowSelectionMargin
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the current text in the rich text box.</summary>
		/// <returns>The text displayed in the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000B2B84 File Offset: 0x000B0D84
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x000B2B8C File Offset: 0x000B0D8C
		[Localizable(true)]
		[RefreshProperties(1)]
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

		/// <returns>The number of characters contained in the text of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000B2B98 File Offset: 0x000B0D98
		[Browsable(false)]
		public override int TextLength
		{
			get
			{
				return base.TextLength;
			}
		}

		/// <summary>Gets the name of the action that can be undone in the control when the <see cref="M:System.Windows.Forms.TextBoxBase.Undo" /> method is called.</summary>
		/// <returns>The text name of the action that can be undone.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000B2BA0 File Offset: 0x000B0DA0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string UndoActionName
		{
			get
			{
				return this.document.undo.UndoActionName;
			}
		}

		/// <summary>Gets or sets the current zoom level of the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
		/// <returns>The factor by which the contents of the control is zoomed.</returns>
		/// <exception cref="T:System.ArgumentException">The specified zoom factor did not fall within the permissible range. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000B2BB4 File Offset: 0x000B0DB4
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000B2BBC File Offset: 0x000B0DBC
		[Localizable(true)]
		[DefaultValue(1)]
		public float ZoomFactor
		{
			get
			{
				return this.zoom;
			}
			set
			{
				this.zoom = value;
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000B2BC8 File Offset: 0x000B0DC8
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default size of the control. </summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000B2BD0 File Offset: 0x000B0DD0
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 96);
			}
		}

		/// <summary>Determines whether you can paste information from the Clipboard in the specified data format.</summary>
		/// <returns>true if you can paste data from the Clipboard in the specified data format; otherwise, false.</returns>
		/// <param name="clipFormat">One of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E8F RID: 11919 RVA: 0x000B2BDC File Offset: 0x000B0DDC
		public bool CanPaste(DataFormats.Format clipFormat)
		{
			return clipFormat.Name == DataFormats.Rtf || clipFormat.Name == DataFormats.Text || clipFormat.Name == DataFormats.UnicodeText;
		}

		/// <summary>Searches the text of a <see cref="T:System.Windows.Forms.RichTextBox" /> control for the first instance of a character from a list of characters.</summary>
		/// <returns>The location within the control where the search characters were found or -1 if the search characters are not found or an empty search character set is specified in the <paramref name="char" /> parameter.</returns>
		/// <param name="characterSet">The array of characters to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E90 RID: 11920 RVA: 0x000B2C2C File Offset: 0x000B0E2C
		public int Find(char[] characterSet)
		{
			return this.Find(characterSet, -1, -1);
		}

		/// <summary>Searches the text of a <see cref="T:System.Windows.Forms.RichTextBox" /> control, at a specific starting point, for the first instance of a character from a list of characters.</summary>
		/// <returns>The location within the control where the search characters are found.</returns>
		/// <param name="characterSet">The array of characters to search for. </param>
		/// <param name="start">The location within the control's text at which to begin searching. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E91 RID: 11921 RVA: 0x000B2C38 File Offset: 0x000B0E38
		public int Find(char[] characterSet, int start)
		{
			return this.Find(characterSet, start, -1);
		}

		/// <summary>Searches a range of text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for the first instance of a character from a list of characters.</summary>
		/// <returns>The location within the control where the search characters are found.</returns>
		/// <param name="characterSet">The array of characters to search for. </param>
		/// <param name="start">The location within the control's text at which to begin searching. </param>
		/// <param name="end">The location within the control's text at which to end searching. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="characterSet" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="start" /> is less than 0 or greater than the length of the text in the control. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E92 RID: 11922 RVA: 0x000B2C44 File Offset: 0x000B0E44
		public int Find(char[] characterSet, int start, int end)
		{
			Document.Marker marker;
			if (start == -1)
			{
				this.document.GetMarker(out marker, true);
			}
			else
			{
				marker = default(Document.Marker);
				Line line;
				LineTag lineTag;
				int num;
				this.document.CharIndexToLineTag(start, out line, out lineTag, out num);
				marker.line = line;
				marker.tag = lineTag;
				marker.pos = num;
			}
			Document.Marker marker2;
			if (end == -1)
			{
				this.document.GetMarker(out marker2, false);
			}
			else
			{
				marker2 = default(Document.Marker);
				Line line2;
				LineTag lineTag2;
				int num2;
				this.document.CharIndexToLineTag(end, out line2, out lineTag2, out num2);
				marker2.line = line2;
				marker2.tag = lineTag2;
				marker2.pos = num2;
			}
			Document.Marker marker3;
			if (this.document.FindChars(characterSet, marker, marker2, out marker3))
			{
				return this.document.LineTagToCharIndex(marker3.line, marker3.pos);
			}
			return -1;
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string.</summary>
		/// <returns>The location within the control where the search text was found or -1 if the search string is not found or an empty search string is specified in the <paramref name="str" /> parameter.</returns>
		/// <param name="str">The text to locate in the control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E93 RID: 11923 RVA: 0x000B2D20 File Offset: 0x000B0F20
		public int Find(string str)
		{
			return this.Find(str, -1, -1, RichTextBoxFinds.None);
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string within a range of text within the control and with specific options applied to the search.</summary>
		/// <returns>The location within the control where the search text was found.</returns>
		/// <param name="str">The text to locate in the control. </param>
		/// <param name="start">The location within the control's text at which to begin searching. </param>
		/// <param name="end">The location within the control's text at which to end searching. This value must be equal to negative one (-1) or greater than or equal to the <paramref name="start" /> parameter. </param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter was null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> parameter was less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="end" /> parameter was less the <paramref name="start" /> parameter. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E94 RID: 11924 RVA: 0x000B2D2C File Offset: 0x000B0F2C
		public int Find(string str, int start, int end, RichTextBoxFinds options)
		{
			Document.Marker marker;
			if (start == -1)
			{
				this.document.GetMarker(out marker, true);
			}
			else
			{
				marker = default(Document.Marker);
				Line line;
				LineTag lineTag;
				int num;
				this.document.CharIndexToLineTag(start, out line, out lineTag, out num);
				marker.line = line;
				marker.tag = lineTag;
				marker.pos = num;
			}
			Document.Marker marker2;
			if (end == -1)
			{
				this.document.GetMarker(out marker2, false);
			}
			else
			{
				marker2 = default(Document.Marker);
				Line line2;
				LineTag lineTag2;
				int num2;
				this.document.CharIndexToLineTag(end, out line2, out lineTag2, out num2);
				marker2.line = line2;
				marker2.tag = lineTag2;
				marker2.pos = num2;
			}
			Document.Marker marker3;
			if (this.document.Find(str, marker, marker2, out marker3, options))
			{
				return this.document.LineTagToCharIndex(marker3.line, marker3.pos);
			}
			return -1;
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string at a specific location within the control and with specific options applied to the search.</summary>
		/// <returns>The location within the control where the search text was found.</returns>
		/// <param name="str">The text to locate in the control. </param>
		/// <param name="start">The location within the control's text at which to begin searching. </param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E95 RID: 11925 RVA: 0x000B2E08 File Offset: 0x000B1008
		public int Find(string str, int start, RichTextBoxFinds options)
		{
			return this.Find(str, start, -1, options);
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string with specific options applied to the search.</summary>
		/// <returns>The location within the control where the search text was found.</returns>
		/// <param name="str">The text to locate in the control. </param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E96 RID: 11926 RVA: 0x000B2E14 File Offset: 0x000B1014
		public int Find(string str, RichTextBoxFinds options)
		{
			return this.Find(str, -1, -1, options);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000B2E20 File Offset: 0x000B1020
		internal override char GetCharFromPositionInternal(Point p)
		{
			LineTag lineTag;
			int num;
			this.PointToTagPos(p, out lineTag, out num);
			if (num >= lineTag.Line.text.Length)
			{
				return '\n';
			}
			return lineTag.Line.text.get_Chars(num);
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <returns>The zero-based character index at the specified location.</returns>
		/// <param name="pt">The location to search. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E98 RID: 11928 RVA: 0x000B2E64 File Offset: 0x000B1064
		public override int GetCharIndexFromPosition(Point pt)
		{
			LineTag lineTag;
			int num;
			this.PointToTagPos(pt, out lineTag, out num);
			return this.document.LineTagToCharIndex(lineTag.Line, num);
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		/// <param name="index">The character index position to search. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E99 RID: 11929 RVA: 0x000B2E90 File Offset: 0x000B1090
		public override int GetLineFromCharIndex(int index)
		{
			Line line;
			LineTag lineTag;
			int num;
			this.document.CharIndexToLineTag(index, out line, out lineTag, out num);
			return line.LineNo - 1;
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <returns>The location of the specified character.</returns>
		/// <param name="index">The index of the character for which to retrieve the location. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E9A RID: 11930 RVA: 0x000B2EB8 File Offset: 0x000B10B8
		public override Point GetPositionFromCharIndex(int index)
		{
			Line line;
			LineTag lineTag;
			int num;
			this.document.CharIndexToLineTag(index, out line, out lineTag, out num);
			return new Point(line.X + (int)line.widths[num] + this.document.OffsetX - this.document.ViewPortX, line.Y + this.document.OffsetY - this.document.ViewPortY);
		}

		/// <summary>Loads the contents of an existing data stream into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="data">A stream of data to load into the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
		/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control. </exception>
		/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E9B RID: 11931 RVA: 0x000B2F24 File Offset: 0x000B1124
		public void LoadFile(Stream data, RichTextBoxStreamType fileType)
		{
			this.document.Empty();
			if (fileType == RichTextBoxStreamType.PlainText)
			{
				StringBuilder stringBuilder;
				char[] array;
				try
				{
					stringBuilder = new StringBuilder((int)data.Length);
					array = new char[1024];
				}
				catch
				{
					throw new IOException("Not enough memory to load document");
				}
				StreamReader streamReader = new StreamReader(data, Encoding.Default, true);
				for (int i = streamReader.Read(array, 0, array.Length); i > 0; i = streamReader.Read(array, 0, array.Length))
				{
					stringBuilder.Append(array, 0, i);
				}
				if (stringBuilder.Length > 0 && stringBuilder.get_Chars(stringBuilder.Length - 1) == '\n')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				base.Text = stringBuilder.ToString();
				return;
			}
			this.InsertRTFFromStream(data, 0, 1);
			this.document.PositionCaret(this.document.GetLine(1), 0);
			this.document.SetSelectionToCaret(true);
			base.ScrollToCaret();
		}

		/// <summary>Loads a rich text format (RTF) or standard ASCII text file into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="path">The name and location of the file to load into the control. </param>
		/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control. </exception>
		/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E9C RID: 11932 RVA: 0x000B303C File Offset: 0x000B123C
		public void LoadFile(string path)
		{
			this.LoadFile(path, RichTextBoxStreamType.RichText);
		}

		/// <summary>Loads a specific type of file into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="path">The name and location of the file to load into the control. </param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
		/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control. </exception>
		/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E9D RID: 11933 RVA: 0x000B3048 File Offset: 0x000B1248
		public void LoadFile(string path, RichTextBoxStreamType fileType)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(path, 3, 1, 1, 1024);
				this.LoadFile(fileStream, fileType);
			}
			catch (Exception ex)
			{
				throw new IOException("Could not open file " + path, ex);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		/// <summary>Pastes the contents of the Clipboard in the specified Clipboard format.</summary>
		/// <param name="clipFormat">The Clipboard format in which the data should be obtained from the Clipboard. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E9E RID: 11934 RVA: 0x000B30CC File Offset: 0x000B12CC
		public void Paste(DataFormats.Format clipFormat)
		{
			base.Paste(Clipboard.GetDataObject(), clipFormat, false);
		}

		/// <summary>Reapplies the last operation that was undone in the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E9F RID: 11935 RVA: 0x000B30DC File Offset: 0x000B12DC
		public void Redo()
		{
			if (this.document.undo.Redo())
			{
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Saves the contents of a <see cref="T:System.Windows.Forms.RichTextBox" /> control to an open data stream.</summary>
		/// <param name="data">The data stream that contains the file to save to. </param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">An invalid file type is specified in the <paramref name="fileType" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EA0 RID: 11936 RVA: 0x000B310C File Offset: 0x000B130C
		public void SaveFile(Stream data, RichTextBoxStreamType fileType)
		{
			Encoding encoding;
			if (fileType == RichTextBoxStreamType.UnicodePlainText)
			{
				encoding = Encoding.Unicode;
			}
			else
			{
				encoding = Encoding.ASCII;
			}
			byte[] array;
			switch (fileType)
			{
			case RichTextBoxStreamType.PlainText:
			case RichTextBoxStreamType.TextTextOleObjs:
			case RichTextBoxStreamType.UnicodePlainText:
			{
				if (!this.Multiline)
				{
					array = encoding.GetBytes(this.document.Root.text.ToString());
					data.Write(array, 0, array.Length);
					return;
				}
				for (int i = 1; i < this.document.Lines; i++)
				{
					string text = this.document.GetLine(i).TextWithoutEnding() + Environment.NewLine;
					array = encoding.GetBytes(text);
					data.Write(array, 0, array.Length);
				}
				array = encoding.GetBytes(this.document.GetLine(this.document.Lines).text.ToString());
				data.Write(array, 0, array.Length);
				return;
			}
			}
			Line line = this.document.GetLine(1);
			Line line2 = this.document.GetLine(this.document.Lines);
			StringBuilder stringBuilder = this.GenerateRTF(line, 0, line2, line2.text.Length);
			int length = stringBuilder.Length;
			array = new byte[4096];
			for (int i = 0; i < length; i += 1024)
			{
				int num;
				if (i + 1024 < length)
				{
					num = encoding.GetBytes(stringBuilder.ToString(i, 1024), 0, 1024, array, 0);
				}
				else
				{
					num = length - i;
					num = encoding.GetBytes(stringBuilder.ToString(i, num), 0, num, array, 0);
				}
				data.Write(array, 0, num);
			}
		}

		/// <summary>Saves the contents of the <see cref="T:System.Windows.Forms.RichTextBox" /> to a rich text format (RTF) file.</summary>
		/// <param name="path">The name and location of the file to save. </param>
		/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EA1 RID: 11937 RVA: 0x000B32C4 File Offset: 0x000B14C4
		public void SaveFile(string path)
		{
			if (path.EndsWith(".rtf"))
			{
				this.SaveFile(path, RichTextBoxStreamType.RichText);
			}
			else
			{
				this.SaveFile(path, RichTextBoxStreamType.PlainText);
			}
		}

		/// <summary>Saves the contents of the <see cref="T:System.Windows.Forms.RichTextBox" /> to a specific type of file.</summary>
		/// <param name="path">The name and location of the file to save. </param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">An invalid file type is specified in the <paramref name="fileType" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EA2 RID: 11938 RVA: 0x000B32EC File Offset: 0x000B14EC
		public void SaveFile(string path, RichTextBoxStreamType fileType)
		{
			FileStream fileStream = new FileStream(path, 2, 2, 0, 1024, false);
			this.SaveFile(fileStream, fileType);
			if (fileStream != null)
			{
				fileStream.Close();
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
		// Token: 0x06002EA3 RID: 11939 RVA: 0x000B3320 File Offset: 0x000B1520
		[EditorBrowsable(1)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			Graphics graphics = Graphics.FromImage(bitmap);
			base.Draw(graphics, targetBounds);
		}

		/// <summary>Creates an IRichEditOleCallback-compatible object for handling rich-edit callback operations.</summary>
		/// <returns>An object that implements the IRichEditOleCallback interface.</returns>
		// Token: 0x06002EA4 RID: 11940 RVA: 0x000B333C File Offset: 0x000B153C
		protected virtual object CreateRichEditOleCallback()
		{
			throw new NotImplementedException();
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EA5 RID: 11941 RVA: 0x000B3344 File Offset: 0x000B1544
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.ContentsResized" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ContentsResizedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EA6 RID: 11942 RVA: 0x000B3350 File Offset: 0x000B1550
		protected virtual void OnContentsResized(ContentsResizedEventArgs e)
		{
			ContentsResizedEventHandler contentsResizedEventHandler = (ContentsResizedEventHandler)base.Events[RichTextBox.ContentsResizedEvent];
			if (contentsResizedEventHandler != null)
			{
				contentsResizedEventHandler(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EA7 RID: 11943 RVA: 0x000B3384 File Offset: 0x000B1584
		protected override void OnContextMenuChanged(EventArgs e)
		{
			base.OnContextMenuChanged(e);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000B3390 File Offset: 0x000B1590
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000B339C File Offset: 0x000B159C
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.HScroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EAA RID: 11946 RVA: 0x000B33A8 File Offset: 0x000B15A8
		protected virtual void OnHScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.HScrollEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.ImeChange" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EAB RID: 11947 RVA: 0x000B33DC File Offset: 0x000B15DC
		[MonoTODO("Stub, never called")]
		protected virtual void OnImeChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.ImeChangeEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002EAC RID: 11948 RVA: 0x000B3410 File Offset: 0x000B1610
		protected virtual void OnLinkClicked(LinkClickedEventArgs e)
		{
			LinkClickedEventHandler linkClickedEventHandler = (LinkClickedEventHandler)base.Events[RichTextBox.LinkClickedEvent];
			if (linkClickedEventHandler != null)
			{
				linkClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.Protected" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EAD RID: 11949 RVA: 0x000B3444 File Offset: 0x000B1644
		protected virtual void OnProtected(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.ProtectedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EAE RID: 11950 RVA: 0x000B3478 File Offset: 0x000B1678
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.SelectionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EAF RID: 11951 RVA: 0x000B3484 File Offset: 0x000B1684
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.SelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.VScroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B34B8 File Offset: 0x000B16B8
		protected virtual void OnVScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.VScrollEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="m">A Windows Message Object. </param>
		// Token: 0x06002EB1 RID: 11953 RVA: 0x000B34EC File Offset: 0x000B16EC
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <returns>true if the command key was processed by the control; otherwise, false.</returns>
		/// <param name="m"></param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process. </param>
		// Token: 0x06002EB2 RID: 11954 RVA: 0x000B34F8 File Offset: 0x000B16F8
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000B3504 File Offset: 0x000B1704
		internal override void SelectWord()
		{
			this.document.ExpandSelection(CaretSelection.Word, false);
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000B3514 File Offset: 0x000B1714
		private void HandleGroup(RTF rtf)
		{
			if (this.rtf_section_stack == null)
			{
				this.rtf_section_stack = new Stack();
			}
			if (rtf.Major == Major.BeginGroup)
			{
				this.rtf_section_stack.Push(this.rtf_style.Clone());
				this.rtf_skip_count = 0;
			}
			else if (rtf.Major == Major.EndGroup && this.rtf_section_stack.Count > 0)
			{
				this.FlushText(rtf, false);
				this.rtf_style = (RichTextBox.RtfSectionStyle)this.rtf_section_stack.Pop();
			}
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000B35A0 File Offset: 0x000B17A0
		[MonoInternalNote("Add QuadJust support for justified alignment")]
		private void HandleControl(RTF rtf)
		{
			Major major = rtf.Major;
			switch (major)
			{
			case Major.Destination:
				rtf.SkipGroup();
				break;
			default:
				switch (major)
				{
				case Major.ParAttr:
				{
					Minor minor = rtf.Minor;
					switch (minor)
					{
					case Minor.QuadLeft:
						this.FlushText(rtf, false);
						this.rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
						break;
					case Minor.QuadRight:
						this.FlushText(rtf, false);
						this.rtf_style.rtf_rtfalign = HorizontalAlignment.Right;
						break;
					case Minor.QuadJust:
						this.FlushText(rtf, false);
						this.rtf_style.rtf_rtfalign = HorizontalAlignment.Center;
						break;
					case Minor.QuadCenter:
						this.FlushText(rtf, false);
						this.rtf_style.rtf_rtfalign = HorizontalAlignment.Center;
						break;
					default:
						if (minor == Minor.ParDef)
						{
							this.FlushText(rtf, false);
							this.rtf_style.rtf_par_line_left_indent = 0;
							this.rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
						}
						break;
					case Minor.LeftIndent:
						this.rtf_style.rtf_par_line_left_indent = (int)((float)rtf.Param / 1440f * base.CreateGraphics().DpiX + 0.5f);
						break;
					}
					break;
				}
				case Major.CharAttr:
				{
					Minor minor = rtf.Minor;
					switch (minor)
					{
					case Minor.StrikeThru:
						this.FlushText(rtf, false);
						if (rtf.Param == -1000000)
						{
							this.rtf_style.rtf_rtfstyle |= 8;
						}
						else
						{
							this.rtf_style.rtf_rtfstyle &= -9;
						}
						break;
					case Minor.Underline:
						this.FlushText(rtf, false);
						if (rtf.Param == -1000000)
						{
							this.rtf_style.rtf_rtfstyle |= 4;
						}
						else
						{
							this.rtf_style.rtf_rtfstyle = this.rtf_style.rtf_rtfstyle & -5;
						}
						break;
					default:
						switch (minor)
						{
						case Minor.FontNum:
						{
							Font font = global::System.Windows.Forms.RTF.Font.GetFont(rtf, rtf.Param);
							if (font != null)
							{
								this.FlushText(rtf, false);
								this.rtf_style.rtf_rtffont = font;
							}
							break;
						}
						case Minor.FontSize:
							this.FlushText(rtf, false);
							this.rtf_style.rtf_rtffont_size = rtf.Param / 2;
							break;
						case Minor.Italic:
							this.FlushText(rtf, false);
							if (rtf.Param == -1000000)
							{
								this.rtf_style.rtf_rtfstyle |= 2;
							}
							else
							{
								this.rtf_style.rtf_rtfstyle &= -3;
							}
							break;
						default:
							if (minor != Minor.Plain)
							{
								if (minor == Minor.Bold)
								{
									this.FlushText(rtf, false);
									if (rtf.Param == -1000000)
									{
										this.rtf_style.rtf_rtfstyle |= 1;
									}
									else
									{
										this.rtf_style.rtf_rtfstyle &= -2;
									}
								}
							}
							else
							{
								this.FlushText(rtf, false);
								this.rtf_style.rtf_rtfstyle = 0;
							}
							break;
						}
						break;
					case Minor.NoUnderline:
						this.FlushText(rtf, false);
						this.rtf_style.rtf_rtfstyle &= -5;
						break;
					case Minor.Invisible:
						this.FlushText(rtf, false);
						this.rtf_style.rtf_visible = false;
						break;
					case Minor.ForeColor:
					{
						Color color = Color.GetColor(rtf, rtf.Param);
						if (color != null)
						{
							this.FlushText(rtf, false);
							if (color.Red == -1 && color.Green == -1 && color.Blue == -1)
							{
								this.rtf_style.rtf_color = this.ForeColor;
							}
							else
							{
								this.rtf_style.rtf_color = Color.FromArgb(color.Red, color.Green, color.Blue);
							}
							this.FlushText(rtf, false);
						}
						break;
					}
					}
					break;
				}
				case Major.PictAttr:
					if (rtf.Picture != null && rtf.Picture.IsValid())
					{
						Line line = this.document.GetLine(this.rtf_cursor_y);
						this.document.InsertPicture(line, 0, rtf.Picture);
						this.rtf_cursor_x++;
						this.FlushText(rtf, true);
						rtf.Picture = null;
					}
					break;
				default:
					if (major == Major.Unicode)
					{
						Minor minor = rtf.Minor;
						if (minor != Minor.UnicodeCharBytes)
						{
							if (minor == Minor.UnicodeChar)
							{
								this.FlushText(rtf, false);
								this.rtf_skip_count += this.rtf_style.rtf_skip_width;
								this.rtf_line.Append((char)rtf.Param);
							}
						}
						else
						{
							this.rtf_style.rtf_skip_width = rtf.Param;
						}
					}
					break;
				}
				break;
			case Major.SpecialChar:
				this.SpecialChar(rtf);
				break;
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000B3AA0 File Offset: 0x000B1CA0
		private void SpecialChar(RTF rtf)
		{
			Minor minor = rtf.Minor;
			switch (minor)
			{
			case Minor.Cell:
				Console.Write(" ");
				break;
			case Minor.Row:
			case Minor.Par:
			case Minor.Sect:
			case Minor.Page:
			case Minor.Line:
				this.FlushText(rtf, true);
				break;
			default:
				if (minor != Minor.WidowCtrl)
				{
				}
				break;
			case Minor.Tab:
				this.rtf_line.Append("\t");
				break;
			case Minor.EmDash:
				this.rtf_line.Append("—");
				break;
			case Minor.EnDash:
				this.rtf_line.Append("–");
				break;
			case Minor.Bullet:
				Console.WriteLine("*");
				break;
			case Minor.NoBrkSpace:
				Console.Write(" ");
				break;
			case Minor.NoReqHyphen:
			case Minor.NoBrkHyphen:
				this.rtf_line.Append("-");
				break;
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000B3BD0 File Offset: 0x000B1DD0
		private void HandleText(RTF rtf)
		{
			string text = rtf.EncodedText;
			if (this.rtf_skip_count > 0 && text.Length > 0)
			{
				int num = Math.Min(this.rtf_skip_count, text.Length);
				text = text.Substring(num);
				this.rtf_skip_count -= num;
			}
			if (this.rtf_style.rtf_visible)
			{
				this.rtf_line.Append(text);
			}
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B3C44 File Offset: 0x000B1E44
		private void FlushText(RTF rtf, bool newline)
		{
			int length = this.rtf_line.Length;
			if (!newline && length == 0)
			{
				return;
			}
			if (this.rtf_style.rtf_rtffont == null)
			{
				this.rtf_style.rtf_rtffont = global::System.Windows.Forms.RTF.Font.GetFont(rtf, 0);
			}
			Font font = new Font(this.rtf_style.rtf_rtffont.Name, (float)this.rtf_style.rtf_rtffont_size, this.rtf_style.rtf_rtfstyle);
			if (this.rtf_style.rtf_color == Color.Empty)
			{
				Color color = Color.GetColor(rtf, 0);
				if (color == null || (color.Red == -1 && color.Green == -1 && color.Blue == -1))
				{
					this.rtf_style.rtf_color = this.ForeColor;
				}
				else
				{
					this.rtf_style.rtf_color = Color.FromArgb(color.Red, color.Green, color.Blue);
				}
			}
			this.rtf_chars += this.rtf_line.Length;
			if (this.rtf_cursor_x == 0 && !this.reuse_line)
			{
				if (newline && !this.rtf_line.ToString().EndsWith(Environment.NewLine))
				{
					this.rtf_line.Append(Environment.NewLine);
				}
				this.document.Add(this.rtf_cursor_y, this.rtf_line.ToString(), this.rtf_style.rtf_rtfalign, font, this.rtf_style.rtf_color, (!newline) ? LineEnding.Wrap : LineEnding.Rich);
				if (this.rtf_style.rtf_par_line_left_indent != 0)
				{
					Line line = this.document.GetLine(this.rtf_cursor_y);
					line.indent = this.rtf_style.rtf_par_line_left_indent;
				}
			}
			else
			{
				Line line2 = this.document.GetLine(this.rtf_cursor_y);
				line2.indent = this.rtf_style.rtf_par_line_left_indent;
				if (this.rtf_line.Length > 0)
				{
					this.document.InsertString(line2, this.rtf_cursor_x, this.rtf_line.ToString());
					this.document.FormatText(line2, this.rtf_cursor_x + 1, line2, this.rtf_cursor_x + 1 + length, font, this.rtf_style.rtf_color, Color.Empty, FormatSpecified.Font | FormatSpecified.Color);
				}
				if (newline)
				{
					line2 = this.document.GetLine(this.rtf_cursor_y);
					line2.ending = LineEnding.Rich;
					if (!line2.Text.EndsWith(Environment.NewLine))
					{
						Line line3 = line2;
						line3.Text += Environment.NewLine;
					}
				}
				this.reuse_line = false;
			}
			if (newline)
			{
				this.rtf_cursor_x = 0;
				this.rtf_cursor_y++;
			}
			else
			{
				this.rtf_cursor_x += length;
			}
			this.rtf_line.Length = 0;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000B3F30 File Offset: 0x000B2130
		private void InsertRTFFromStream(Stream data, int cursor_x, int cursor_y)
		{
			int num;
			int num2;
			int num3;
			this.InsertRTFFromStream(data, cursor_x, cursor_y, out num, out num2, out num3);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000B3F4C File Offset: 0x000B214C
		private void InsertRTFFromStream(Stream data, int cursor_x, int cursor_y, out int to_x, out int to_y, out int chars)
		{
			RTF rtf = new RTF(data);
			rtf.ClassCallback[TokenClass.Text] = new ClassDelegate(this.HandleText);
			rtf.ClassCallback[TokenClass.Control] = new ClassDelegate(this.HandleControl);
			rtf.ClassCallback[TokenClass.Group] = new ClassDelegate(this.HandleGroup);
			this.rtf_skip_count = 0;
			this.rtf_line = new StringBuilder();
			this.rtf_style.rtf_color = Color.Empty;
			this.rtf_style.rtf_rtffont_size = (int)this.Font.Size;
			this.rtf_style.rtf_rtfalign = HorizontalAlignment.Left;
			this.rtf_style.rtf_rtfstyle = 0;
			this.rtf_style.rtf_rtffont = null;
			this.rtf_style.rtf_visible = true;
			this.rtf_style.rtf_skip_width = 1;
			this.rtf_cursor_x = cursor_x;
			this.rtf_cursor_y = cursor_y;
			this.rtf_chars = 0;
			rtf.DefaultFont(this.Font.Name);
			this.rtf_text_map = new TextMap();
			TextMap.SetupStandardTable(this.rtf_text_map.Table);
			this.document.SuspendRecalc();
			try
			{
				rtf.Read();
				this.FlushText(rtf, false);
			}
			catch (RTFException ex)
			{
				Console.WriteLine("RTF Parsing failure: {0}", ex.Message);
			}
			to_x = this.rtf_cursor_x;
			to_y = this.rtf_cursor_y;
			chars = this.rtf_chars;
			if (this.rtf_section_stack != null)
			{
				this.rtf_section_stack.Clear();
			}
			this.document.RecalculateDocument(base.CreateGraphicsInternal(), cursor_y, this.document.Lines, false);
			this.document.ResumeRecalc(true);
			this.document.Invalidate(this.document.GetLine(cursor_y), 0, this.document.GetLine(this.document.Lines), -1);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000B413C File Offset: 0x000B233C
		private void RichTextBox_HScrolled(object sender, EventArgs e)
		{
			this.OnHScroll(e);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000B4148 File Offset: 0x000B2348
		private void RichTextBox_VScrolled(object sender, EventArgs e)
		{
			this.OnVScroll(e);
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000B4154 File Offset: 0x000B2354
		private void PointToTagPos(Point pt, out LineTag tag, out int pos)
		{
			Point point = pt;
			if (point.X >= this.document.ViewPortWidth)
			{
				point.X = this.document.ViewPortWidth - 1;
			}
			else if (point.X < 0)
			{
				point.X = 0;
			}
			if (point.Y >= this.document.ViewPortHeight)
			{
				point.Y = this.document.ViewPortHeight - 1;
			}
			else if (point.Y < 0)
			{
				point.Y = 0;
			}
			tag = this.document.FindCursor(point.X + this.document.ViewPortX, point.Y + this.document.ViewPortY, out pos);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000B4224 File Offset: 0x000B2424
		private void EmitRTFFontProperties(StringBuilder rtf, int prev_index, int font_index, Font prev_font, Font font)
		{
			if (prev_index != font_index)
			{
				rtf.Append(string.Format("\\f{0}", font_index));
			}
			if (prev_font == null || prev_font.Size != font.Size)
			{
				rtf.Append(string.Format("\\fs{0}", (int)(font.Size * 2f)));
			}
			if (prev_font == null || font.Bold != prev_font.Bold)
			{
				if (font.Bold)
				{
					rtf.Append("\\b");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\b0");
				}
			}
			if (prev_font == null || font.Italic != prev_font.Italic)
			{
				if (font.Italic)
				{
					rtf.Append("\\i");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\i0");
				}
			}
			if (prev_font == null || font.Strikeout != prev_font.Strikeout)
			{
				if (font.Strikeout)
				{
					rtf.Append("\\strike");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\strike0");
				}
			}
			if (prev_font == null || font.Underline != prev_font.Underline)
			{
				if (font.Underline)
				{
					rtf.Append("\\ul");
				}
				else if (prev_font != null)
				{
					rtf.Append("\\ul0");
				}
			}
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000B43B8 File Offset: 0x000B25B8
		[MonoInternalNote("Emit unicode and other special characters properly")]
		private void EmitRTFText(StringBuilder rtf, string text)
		{
			int length = rtf.Length;
			int length2 = text.Length;
			rtf.Append(text);
			if (text.IndexOfAny(RichTextBox.ReservedRTFChars) > -1)
			{
				rtf.Replace("\\", "\\\\", length, length2);
				rtf.Replace("{", "\\{", length, length2);
				rtf.Replace("}", "\\}", length, length2);
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000B4428 File Offset: 0x000B2628
		private StringBuilder GenerateRTF(Line start_line, int start_pos, Line end_line, int end_pos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ArrayList arrayList = new ArrayList(10);
			ArrayList arrayList2 = new ArrayList(10);
			int i = start_line.line_no;
			int j = start_pos;
			LineTag lineTag = LineTag.FindTag(start_line, j);
			Font font = lineTag.Font;
			Color color = lineTag.Color;
			arrayList.Add(font.Name);
			arrayList2.Add(color);
			while (i <= end_line.line_no)
			{
				Line line = this.document.GetLine(i);
				lineTag = LineTag.FindTag(line, j);
				int num;
				if (i != end_line.line_no)
				{
					num = line.text.Length;
				}
				else
				{
					num = end_pos;
				}
				while (j < num)
				{
					if (lineTag.Font.Name != font.Name)
					{
						font = lineTag.Font;
						if (!arrayList.Contains(font.Name))
						{
							arrayList.Add(font.Name);
						}
					}
					if (lineTag.Color != color)
					{
						color = lineTag.Color;
						if (!arrayList2.Contains(color))
						{
							arrayList2.Add(color);
						}
					}
					j = lineTag.Start + lineTag.Length - 1;
					lineTag = lineTag.Next;
				}
				j = 0;
				i++;
			}
			stringBuilder.Append("{\\rtf1\\ansi");
			stringBuilder.Append("\\ansicpg1252");
			stringBuilder.Append(string.Format("\\deff{0}", arrayList.IndexOf(this.Font.Name)));
			stringBuilder.Append("\\deflang1033" + Environment.NewLine);
			stringBuilder.Append("{\\fonttbl");
			for (int k = 0; k < arrayList.Count; k++)
			{
				stringBuilder.Append(string.Format("{{\\f{0}", k));
				stringBuilder.Append("\\fnil");
				stringBuilder.Append("\\fcharset0 ");
				stringBuilder.Append((string)arrayList[k]);
				stringBuilder.Append(";}");
			}
			stringBuilder.Append("}");
			stringBuilder.Append(Environment.NewLine);
			if (arrayList2.Count > 1 || ((Color)arrayList2[0]).R != this.ForeColor.R || ((Color)arrayList2[0]).G != this.ForeColor.G || ((Color)arrayList2[0]).B != this.ForeColor.B)
			{
				stringBuilder.Append("{\\colortbl ");
				for (int k = 0; k < arrayList2.Count; k++)
				{
					stringBuilder.Append(string.Format("\\red{0}", ((Color)arrayList2[k]).R));
					stringBuilder.Append(string.Format("\\green{0}", ((Color)arrayList2[k]).G));
					stringBuilder.Append(string.Format("\\blue{0}", ((Color)arrayList2[k]).B));
					stringBuilder.Append(";");
				}
				stringBuilder.Append("}");
				stringBuilder.Append(Environment.NewLine);
			}
			stringBuilder.Append("{\\*\\generator Mono RichTextBox;}");
			lineTag = LineTag.FindTag(start_line, start_pos);
			stringBuilder.Append("\\pard");
			this.EmitRTFFontProperties(stringBuilder, -1, arrayList.IndexOf(lineTag.Font.Name), null, lineTag.Font);
			stringBuilder.Append(" ");
			font = lineTag.Font;
			color = (Color)arrayList2[0];
			i = start_line.line_no;
			j = start_pos;
			while (i <= end_line.line_no)
			{
				Line line = this.document.GetLine(i);
				lineTag = LineTag.FindTag(line, j);
				int num;
				if (i != end_line.line_no)
				{
					num = line.text.Length;
				}
				else
				{
					num = end_pos;
				}
				while (j < num)
				{
					int length = stringBuilder.Length;
					if (lineTag.Font != font)
					{
						this.EmitRTFFontProperties(stringBuilder, arrayList.IndexOf(font.Name), arrayList.IndexOf(lineTag.Font.Name), font, lineTag.Font);
						font = lineTag.Font;
					}
					if (lineTag.Color != color)
					{
						color = lineTag.Color;
						stringBuilder.Append(string.Format("\\cf{0}", arrayList2.IndexOf(color)));
					}
					if (length != stringBuilder.Length)
					{
						stringBuilder.Append(" ");
					}
					if (i != end_line.line_no)
					{
						this.EmitRTFText(stringBuilder, lineTag.Line.text.ToString(j, lineTag.Start + lineTag.Length - j - 1));
					}
					else if (end_pos < lineTag.Start + lineTag.Length - 1)
					{
						this.EmitRTFText(stringBuilder, lineTag.Line.text.ToString(j, end_pos - j));
					}
					else
					{
						this.EmitRTFText(stringBuilder, lineTag.Line.text.ToString(j, lineTag.Start + lineTag.Length - j - 1));
					}
					j = lineTag.Start + lineTag.Length - 1;
					lineTag = lineTag.Next;
				}
				if (j >= line.text.Length && line.ending != LineEnding.Wrap)
				{
					stringBuilder.Append("\\par");
					stringBuilder.Append(Environment.NewLine);
				}
				j = 0;
				i++;
			}
			stringBuilder.Append("}");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder;
		}

		// Token: 0x04001623 RID: 5667
		internal bool auto_word_select;

		// Token: 0x04001624 RID: 5668
		internal int bullet_indent;

		// Token: 0x04001625 RID: 5669
		internal bool detect_urls;

		// Token: 0x04001626 RID: 5670
		private bool reuse_line;

		// Token: 0x04001627 RID: 5671
		internal int margin_right;

		// Token: 0x04001628 RID: 5672
		internal float zoom;

		// Token: 0x04001629 RID: 5673
		private StringBuilder rtf_line;

		// Token: 0x0400162A RID: 5674
		private RichTextBox.RtfSectionStyle rtf_style;

		// Token: 0x0400162B RID: 5675
		private Stack rtf_section_stack;

		// Token: 0x0400162C RID: 5676
		private TextMap rtf_text_map;

		// Token: 0x0400162D RID: 5677
		private int rtf_skip_count;

		// Token: 0x0400162E RID: 5678
		private int rtf_cursor_x;

		// Token: 0x0400162F RID: 5679
		private int rtf_cursor_y;

		// Token: 0x04001630 RID: 5680
		private int rtf_chars;

		// Token: 0x04001631 RID: 5681
		private bool enable_auto_drag_drop;

		// Token: 0x04001632 RID: 5682
		private RichTextBoxLanguageOptions language_option;

		// Token: 0x04001633 RID: 5683
		private bool rich_text_shortcuts_enabled;

		// Token: 0x04001634 RID: 5684
		private Color selection_back_color;

		// Token: 0x0400163C RID: 5692
		private static readonly char[] ReservedRTFChars;

		// Token: 0x020002B8 RID: 696
		private class RtfSectionStyle : ICloneable
		{
			// Token: 0x06002EC2 RID: 11970 RVA: 0x000B4A68 File Offset: 0x000B2C68
			public object Clone()
			{
				return new RichTextBox.RtfSectionStyle
				{
					rtf_color = this.rtf_color,
					rtf_par_line_left_indent = this.rtf_par_line_left_indent,
					rtf_rtfalign = this.rtf_rtfalign,
					rtf_rtffont = this.rtf_rtffont,
					rtf_rtffont_size = this.rtf_rtffont_size,
					rtf_rtfstyle = this.rtf_rtfstyle,
					rtf_visible = this.rtf_visible,
					rtf_skip_width = this.rtf_skip_width
				};
			}

			// Token: 0x0400163D RID: 5693
			internal Color rtf_color;

			// Token: 0x0400163E RID: 5694
			internal Font rtf_rtffont;

			// Token: 0x0400163F RID: 5695
			internal int rtf_rtffont_size;

			// Token: 0x04001640 RID: 5696
			internal FontStyle rtf_rtfstyle;

			// Token: 0x04001641 RID: 5697
			internal HorizontalAlignment rtf_rtfalign;

			// Token: 0x04001642 RID: 5698
			internal int rtf_par_line_left_indent;

			// Token: 0x04001643 RID: 5699
			internal bool rtf_visible;

			// Token: 0x04001644 RID: 5700
			internal int rtf_skip_width;
		}
	}
}
