using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x0200001A RID: 26
	[DefaultProperty("MainInstruction")]
	[DefaultEvent("ButtonClicked")]
	[Description("Displays a task dialog.")]
	[Designer(typeof(TaskDialogDesigner))]
	public class TaskDialog : Component, IWin32Window
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000FF RID: 255 RVA: 0x00005748 File Offset: 0x00003948
		// (remove) Token: 0x06000100 RID: 256 RVA: 0x00005780 File Offset: 0x00003980
		[Category("Behavior")]
		[Description("Event raised when the task dialog has been created.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler Created;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000101 RID: 257 RVA: 0x000057B8 File Offset: 0x000039B8
		// (remove) Token: 0x06000102 RID: 258 RVA: 0x000057F0 File Offset: 0x000039F0
		[Category("Behavior")]
		[Description("Event raised when the task dialog has been destroyed.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler Destroyed;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000103 RID: 259 RVA: 0x00005828 File Offset: 0x00003A28
		// (remove) Token: 0x06000104 RID: 260 RVA: 0x00005860 File Offset: 0x00003A60
		[Category("Action")]
		[Description("Event raised when the user clicks a button.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler<TaskDialogItemClickedEventArgs> ButtonClicked;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000105 RID: 261 RVA: 0x00005898 File Offset: 0x00003A98
		// (remove) Token: 0x06000106 RID: 262 RVA: 0x000058D0 File Offset: 0x00003AD0
		[Category("Action")]
		[Description("Event raised when the user clicks a button.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler<TaskDialogItemClickedEventArgs> RadioButtonClicked;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000107 RID: 263 RVA: 0x00005908 File Offset: 0x00003B08
		// (remove) Token: 0x06000108 RID: 264 RVA: 0x00005940 File Offset: 0x00003B40
		[Category("Action")]
		[Description("Event raised when the user clicks a hyperlink.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler<HyperlinkClickedEventArgs> HyperlinkClicked;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000109 RID: 265 RVA: 0x00005978 File Offset: 0x00003B78
		// (remove) Token: 0x0600010A RID: 266 RVA: 0x000059B0 File Offset: 0x00003BB0
		[Category("Action")]
		[Description("Event raised when the user clicks the verification check box.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler VerificationClicked;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600010B RID: 267 RVA: 0x000059E8 File Offset: 0x00003BE8
		// (remove) Token: 0x0600010C RID: 268 RVA: 0x00005A20 File Offset: 0x00003C20
		[Category("Behavior")]
		[Description("Event raised periodically while the dialog is displayed.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler<TimerEventArgs> Timer;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600010D RID: 269 RVA: 0x00005A58 File Offset: 0x00003C58
		// (remove) Token: 0x0600010E RID: 270 RVA: 0x00005A90 File Offset: 0x00003C90
		[Category("Action")]
		[Description("Event raised when the user clicks the expand button on the task dialog.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler<ExpandButtonClickedEventArgs> ExpandButtonClicked;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600010F RID: 271 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (remove) Token: 0x06000110 RID: 272 RVA: 0x00005B00 File Offset: 0x00003D00
		[Category("Action")]
		[Description("Event raised when the user presses F1 while the dialog has focus.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler HelpRequested;

		// Token: 0x06000111 RID: 273 RVA: 0x00005B38 File Offset: 0x00003D38
		public TaskDialog()
		{
			this.InitializeComponent();
			this._config.cbSize = (uint)Marshal.SizeOf(this._config);
			this._config.pfCallback = new NativeMethods.TaskDialogCallback(this.TaskDialogCallback);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public TaskDialog(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
			this._config.cbSize = (uint)Marshal.SizeOf(this._config);
			this._config.pfCallback = new NativeMethods.TaskDialogCallback(this.TaskDialogCallback);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005C38 File Offset: 0x00003E38
		public static bool OSSupportsTaskDialogs
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005C50 File Offset: 0x00003E50
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		[Category("Appearance")]
		[Description("A list of the buttons on the Task Dialog.")]
		public TaskDialogItemCollection<TaskDialogButton> Buttons
		{
			get
			{
				TaskDialogItemCollection<TaskDialogButton> taskDialogItemCollection;
				if ((taskDialogItemCollection = this._buttons) == null)
				{
					taskDialogItemCollection = (this._buttons = new TaskDialogItemCollection<TaskDialogButton>(this));
				}
				return taskDialogItemCollection;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005C7C File Offset: 0x00003E7C
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		[Category("Appearance")]
		[Description("A list of the radio buttons on the Task Dialog.")]
		public TaskDialogItemCollection<TaskDialogRadioButton> RadioButtons
		{
			get
			{
				TaskDialogItemCollection<TaskDialogRadioButton> taskDialogItemCollection;
				if ((taskDialogItemCollection = this._radioButtons) == null)
				{
					taskDialogItemCollection = (this._radioButtons = new TaskDialogItemCollection<TaskDialogRadioButton>(this));
				}
				return taskDialogItemCollection;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005CA8 File Offset: 0x00003EA8
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005CCE File Offset: 0x00003ECE
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The window title of the task dialog.")]
		[DefaultValue("")]
		public string WindowTitle
		{
			get
			{
				return this._config.pszWindowTitle ?? string.Empty;
			}
			set
			{
				this._config.pszWindowTitle = (string.IsNullOrEmpty(value) ? null : value);
				this.UpdateDialog();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005CF0 File Offset: 0x00003EF0
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005D16 File Offset: 0x00003F16
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's main instruction.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string MainInstruction
		{
			get
			{
				return this._config.pszMainInstruction ?? string.Empty;
			}
			set
			{
				this._config.pszMainInstruction = (string.IsNullOrEmpty(value) ? null : value);
				this.SetElementText(NativeMethods.TaskDialogElements.MainInstruction, this.MainInstruction);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005D40 File Offset: 0x00003F40
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005D66 File Offset: 0x00003F66
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's primary content.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Content
		{
			get
			{
				return this._config.pszContent ?? string.Empty;
			}
			set
			{
				this._config.pszContent = (string.IsNullOrEmpty(value) ? null : value);
				this.SetElementText(NativeMethods.TaskDialogElements.Content, this.Content);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005D90 File Offset: 0x00003F90
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005DD5 File Offset: 0x00003FD5
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to be used in the title bar of the dialog. Used only when the dialog is shown as a modeless dialog.")]
		[DefaultValue(null)]
		public Icon WindowIcon
		{
			get
			{
				bool isDialogRunning = this.IsDialogRunning;
				Icon icon;
				if (isDialogRunning)
				{
					IntPtr intPtr = NativeMethods.SendMessage(this.Handle, 127, new IntPtr(0), IntPtr.Zero);
					icon = Icon.FromHandle(intPtr);
				}
				else
				{
					icon = this._windowIcon;
				}
				return icon;
			}
			set
			{
				this._windowIcon = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005DE0 File Offset: 0x00003FE0
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005DF8 File Offset: 0x00003FF8
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to display in the task dialog.")]
		[DefaultValue(TaskDialogIcon.Custom)]
		public TaskDialogIcon MainIcon
		{
			get
			{
				return this._mainIcon;
			}
			set
			{
				bool flag = this._mainIcon != value;
				if (flag)
				{
					this._mainIcon = value;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005E28 File Offset: 0x00004028
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005E40 File Offset: 0x00004040
		[Localizable(true)]
		[Category("Appearance")]
		[Description("A custom icon to display in the dialog.")]
		[DefaultValue(null)]
		public Icon CustomMainIcon
		{
			get
			{
				return this._customMainIcon;
			}
			set
			{
				bool flag = this._customMainIcon != value;
				if (flag)
				{
					this._customMainIcon = value;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005E70 File Offset: 0x00004070
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005E88 File Offset: 0x00004088
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to display in the footer area of the task dialog.")]
		[DefaultValue(TaskDialogIcon.Custom)]
		public TaskDialogIcon FooterIcon
		{
			get
			{
				return this._footerIcon;
			}
			set
			{
				bool flag = this._footerIcon != value;
				if (flag)
				{
					this._footerIcon = value;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005EB8 File Offset: 0x000040B8
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005ED0 File Offset: 0x000040D0
		[Localizable(true)]
		[Category("Appearance")]
		[Description("A custom icon to display in the footer area of the task dialog.")]
		[DefaultValue(null)]
		public Icon CustomFooterIcon
		{
			get
			{
				return this._customFooterIcon;
			}
			set
			{
				bool flag = this._customFooterIcon != value;
				if (flag)
				{
					this._customFooterIcon = value;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005F00 File Offset: 0x00004100
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00005F2D File Offset: 0x0000412D
		[Category("Behavior")]
		[Description("Indicates whether custom buttons should be displayed as normal buttons or command links.")]
		[DefaultValue(TaskDialogButtonStyle.Standard)]
		public TaskDialogButtonStyle ButtonStyle
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon) ? TaskDialogButtonStyle.CommandLinksNoIcon : (this.GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks) ? TaskDialogButtonStyle.CommandLinks : TaskDialogButtonStyle.Standard);
			}
			set
			{
				this.SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks, value == TaskDialogButtonStyle.CommandLinks);
				this.SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon, value == TaskDialogButtonStyle.CommandLinksNoIcon);
				this.UpdateDialog();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005F54 File Offset: 0x00004154
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005F7C File Offset: 0x0000417C
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The label for the verification checkbox.")]
		[DefaultValue("")]
		public string VerificationText
		{
			get
			{
				return this._config.pszVerificationText ?? string.Empty;
			}
			set
			{
				string text = (string.IsNullOrEmpty(value) ? null : value);
				bool flag = this._config.pszVerificationText != text;
				if (flag)
				{
					this._config.pszVerificationText = text;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005FC4 File Offset: 0x000041C4
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00005FE4 File Offset: 0x000041E4
		[Category("Behavior")]
		[Description("Indicates whether the verification checkbox is checked ot not.")]
		[DefaultValue(false)]
		public bool IsVerificationChecked
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked);
			}
			set
			{
				bool flag = value != this.IsVerificationChecked;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked, value);
					bool isDialogRunning = this.IsDialogRunning;
					if (isDialogRunning)
					{
						this.ClickVerification(value, false);
					}
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006024 File Offset: 0x00004224
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000604A File Offset: 0x0000424A
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional information to be displayed on the dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string ExpandedInformation
		{
			get
			{
				return this._config.pszExpandedInformation ?? string.Empty;
			}
			set
			{
				this._config.pszExpandedInformation = (string.IsNullOrEmpty(value) ? null : value);
				this.SetElementText(NativeMethods.TaskDialogElements.ExpandedInformation, this.ExpandedInformation);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006074 File Offset: 0x00004274
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000609C File Offset: 0x0000429C
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to use for the control for collapsing the expandable information.")]
		[DefaultValue("")]
		public string ExpandedControlText
		{
			get
			{
				return this._config.pszExpandedControlText ?? string.Empty;
			}
			set
			{
				string text = (string.IsNullOrEmpty(value) ? null : value);
				bool flag = this._config.pszExpandedControlText != text;
				if (flag)
				{
					this._config.pszExpandedControlText = text;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000060E4 File Offset: 0x000042E4
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000610C File Offset: 0x0000430C
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to use for the control for expanding the expandable information.")]
		[DefaultValue("")]
		public string CollapsedControlText
		{
			get
			{
				return this._config.pszCollapsedControlText ?? string.Empty;
			}
			set
			{
				string text = (string.IsNullOrEmpty(value) ? null : value);
				bool flag = this._config.pszCollapsedControlText != text;
				if (flag)
				{
					this._config.pszCollapsedControlText = (string.IsNullOrEmpty(value) ? null : value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000615C File Offset: 0x0000435C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006182 File Offset: 0x00004382
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to be used in the footer area of the task dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Footer
		{
			get
			{
				return this._config.pszFooterText ?? string.Empty;
			}
			set
			{
				this._config.pszFooterText = (string.IsNullOrEmpty(value) ? null : value);
				this.SetElementText(NativeMethods.TaskDialogElements.Footer, this.Footer);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000061AC File Offset: 0x000043AC
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000061CC File Offset: 0x000043CC
		[Localizable(true)]
		[Category("Appearance")]
		[Description("the width of the task dialog's client area in DLU's. If 0, task dialog will calculate the ideal width.")]
		[DefaultValue(0)]
		public int Width
		{
			get
			{
				return (int)this._config.cxWidth;
			}
			set
			{
				bool flag = this._config.cxWidth != (uint)value;
				if (flag)
				{
					this._config.cxWidth = (uint)value;
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006204 File Offset: 0x00004404
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006220 File Offset: 0x00004420
		[Category("Behavior")]
		[Description("Indicates whether hyperlinks are allowed for the Content, ExpandedInformation and Footer properties.")]
		[DefaultValue(false)]
		public bool EnableHyperlinks
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks);
			}
			set
			{
				bool flag = this.EnableHyperlinks != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006250 File Offset: 0x00004450
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000626C File Offset: 0x0000446C
		[Category("Behavior")]
		[Description("Indicates that the dialog should be able to be closed using Alt-F4, Escape and the title bar's close button even if no cancel button is specified.")]
		[DefaultValue(false)]
		public bool AllowDialogCancellation
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation);
			}
			set
			{
				bool flag = this.AllowDialogCancellation != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000629C File Offset: 0x0000449C
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000062B8 File Offset: 0x000044B8
		[Category("Behavior")]
		[Description("Indicates that the string specified by the ExpandedInformation property should be displayed at the bottom of the dialog's footer area instead of immediately after the dialog's content.")]
		[DefaultValue(false)]
		public bool ExpandFooterArea
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea);
			}
			set
			{
				bool flag = this.ExpandFooterArea != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000062EC File Offset: 0x000044EC
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000630C File Offset: 0x0000450C
		[Category("Behavior")]
		[Description("Indicates that the string specified by the ExpandedInformation property should be displayed by default.")]
		[DefaultValue(false)]
		public bool ExpandedByDefault
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault);
			}
			set
			{
				bool flag = this.ExpandedByDefault != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006340 File Offset: 0x00004540
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006360 File Offset: 0x00004560
		[Category("Behavior")]
		[Description("Indicates whether the Timer event is raised periodically while the dialog is visible.")]
		[DefaultValue(false)]
		public bool RaiseTimerEvent
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.CallbackTimer);
			}
			set
			{
				bool flag = this.RaiseTimerEvent != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.CallbackTimer, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006394 File Offset: 0x00004594
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000063B4 File Offset: 0x000045B4
		[Category("Layout")]
		[Description("Indicates whether the dialog is centered in the parent window instead of the screen.")]
		[DefaultValue(false)]
		public bool CenterParent
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow);
			}
			set
			{
				bool flag = this.CenterParent != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000063E8 File Offset: 0x000045E8
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006408 File Offset: 0x00004608
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Indicates whether text is displayed right to left.")]
		[DefaultValue(false)]
		public bool RightToLeft
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.RtlLayout);
			}
			set
			{
				bool flag = this.RightToLeft != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.RtlLayout, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000643C File Offset: 0x0000463C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000645C File Offset: 0x0000465C
		[Category("Window Style")]
		[Description("Indicates whether the dialog has a minimize box on its caption bar.")]
		[DefaultValue(false)]
		public bool MinimizeBox
		{
			get
			{
				return this.GetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized);
			}
			set
			{
				bool flag = this.MinimizeBox != value;
				if (flag)
				{
					this.SetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized, value);
					this.UpdateDialog();
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006490 File Offset: 0x00004690
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000064C9 File Offset: 0x000046C9
		[Category("Behavior")]
		[Description("The type of progress bar displayed on the dialog.")]
		[DefaultValue(ProgressBarStyle.None)]
		public ProgressBarStyle ProgressBarStyle
		{
			get
			{
				bool flag = this.GetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar);
				ProgressBarStyle progressBarStyle;
				if (flag)
				{
					progressBarStyle = ProgressBarStyle.MarqueeProgressBar;
				}
				else
				{
					bool flag2 = this.GetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar);
					if (flag2)
					{
						progressBarStyle = ProgressBarStyle.ProgressBar;
					}
					else
					{
						progressBarStyle = ProgressBarStyle.None;
					}
				}
				return progressBarStyle;
			}
			set
			{
				this.SetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar, value == ProgressBarStyle.MarqueeProgressBar);
				this.SetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar, value == ProgressBarStyle.ProgressBar);
				this.UpdateProgressBarStyle();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000064F4 File Offset: 0x000046F4
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000650C File Offset: 0x0000470C
		[Category("Behavior")]
		[Description("The marquee animation speed of the progress bar in milliseconds.")]
		[DefaultValue(100)]
		public int ProgressBarMarqueeAnimationSpeed
		{
			get
			{
				return this._progressBarMarqueeAnimationSpeed;
			}
			set
			{
				this._progressBarMarqueeAnimationSpeed = value;
				this.UpdateProgressBarMarqueeSpeed();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006520 File Offset: 0x00004720
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006538 File Offset: 0x00004738
		[Category("Behavior")]
		[Description("The lower bound of the range of the task dialog's progress bar.")]
		[DefaultValue(0)]
		public int ProgressBarMinimum
		{
			get
			{
				return this._progressBarMinimimum;
			}
			set
			{
				bool flag = this._progressBarMaximum <= value;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._progressBarMinimimum = value;
				this.UpdateProgressBarRange();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006570 File Offset: 0x00004770
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006588 File Offset: 0x00004788
		[Category("Behavior")]
		[Description("The upper bound of the range of the task dialog's progress bar.")]
		[DefaultValue(100)]
		public int ProgressBarMaximum
		{
			get
			{
				return this._progressBarMaximum;
			}
			set
			{
				bool flag = value <= this._progressBarMinimimum;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._progressBarMaximum = value;
				this.UpdateProgressBarRange();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000065C0 File Offset: 0x000047C0
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000065D8 File Offset: 0x000047D8
		[Category("Behavior")]
		[Description("The current value of the task dialog's progress bar.")]
		[DefaultValue(0)]
		public int ProgressBarValue
		{
			get
			{
				return this._progressBarValue;
			}
			set
			{
				bool flag = value < this.ProgressBarMinimum || value > this.ProgressBarMaximum;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._progressBarValue = value;
				this.UpdateProgressBarValue();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006618 File Offset: 0x00004818
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00006630 File Offset: 0x00004830
		[Category("Behavior")]
		[Description("The state of the task dialog's progress bar.")]
		[DefaultValue(ProgressBarState.Normal)]
		public ProgressBarState ProgressBarState
		{
			get
			{
				return this._progressBarState;
			}
			set
			{
				this._progressBarState = value;
				this.UpdateProgressBarState();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006644 File Offset: 0x00004844
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000665C File Offset: 0x0000485C
		[Category("Data")]
		[Description("User-defined data about the component.")]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006668 File Offset: 0x00004868
		public TaskDialogButton Show()
		{
			return this.ShowDialog(IntPtr.Zero);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006688 File Offset: 0x00004888
		public TaskDialogButton ShowDialog()
		{
			return this.ShowDialog(null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000066A4 File Offset: 0x000048A4
		public TaskDialogButton ShowDialog(IWin32Window owner)
		{
			bool flag = owner == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = NativeMethods.GetActiveWindow();
			}
			else
			{
				intPtr = owner.Handle;
			}
			return this.ShowDialog(intPtr);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000066D4 File Offset: 0x000048D4
		public void ClickVerification(bool checkState, bool setFocus)
		{
			bool flag = !this.IsDialogRunning;
			if (flag)
			{
				throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
			}
			NativeMethods.SendMessage(this.Handle, 1137, new IntPtr(checkState ? 1 : 0), new IntPtr(setFocus ? 1 : 0));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006724 File Offset: 0x00004924
		protected virtual void OnHyperlinkClicked(HyperlinkClickedEventArgs e)
		{
			bool flag = this.HyperlinkClicked != null;
			if (flag)
			{
				this.HyperlinkClicked.Invoke(this, e);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006750 File Offset: 0x00004950
		protected virtual void OnButtonClicked(TaskDialogItemClickedEventArgs e)
		{
			bool flag = this.ButtonClicked != null;
			if (flag)
			{
				this.ButtonClicked.Invoke(this, e);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000677C File Offset: 0x0000497C
		protected virtual void OnRadioButtonClicked(TaskDialogItemClickedEventArgs e)
		{
			bool flag = this.RadioButtonClicked != null;
			if (flag)
			{
				this.RadioButtonClicked.Invoke(this, e);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000067A8 File Offset: 0x000049A8
		protected virtual void OnVerificationClicked(EventArgs e)
		{
			bool flag = this.VerificationClicked != null;
			if (flag)
			{
				this.VerificationClicked.Invoke(this, e);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000067D4 File Offset: 0x000049D4
		protected virtual void OnCreated(EventArgs e)
		{
			bool flag = this.Created != null;
			if (flag)
			{
				this.Created.Invoke(this, e);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006800 File Offset: 0x00004A00
		protected virtual void OnTimer(TimerEventArgs e)
		{
			bool flag = this.Timer != null;
			if (flag)
			{
				this.Timer.Invoke(this, e);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000682C File Offset: 0x00004A2C
		protected virtual void OnDestroyed(EventArgs e)
		{
			bool flag = this.Destroyed != null;
			if (flag)
			{
				this.Destroyed.Invoke(this, e);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006858 File Offset: 0x00004A58
		protected virtual void OnExpandButtonClicked(ExpandButtonClickedEventArgs e)
		{
			bool flag = this.ExpandButtonClicked != null;
			if (flag)
			{
				this.ExpandButtonClicked.Invoke(this, e);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006884 File Offset: 0x00004A84
		protected virtual void OnHelpRequested(EventArgs e)
		{
			bool flag = this.HelpRequested != null;
			if (flag)
			{
				this.HelpRequested.Invoke(this, e);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000068B0 File Offset: 0x00004AB0
		internal void SetItemEnabled(TaskDialogItem item)
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, (item is TaskDialogButton) ? 1135 : 1136, new IntPtr(item.Id), new IntPtr(item.Enabled ? 1 : 0));
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006908 File Offset: 0x00004B08
		internal void SetButtonElevationRequired(TaskDialogButton button)
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, 1139, new IntPtr(button.Id), new IntPtr(button.ElevationRequired ? 1 : 0));
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006950 File Offset: 0x00004B50
		internal void ClickItem(TaskDialogItem item)
		{
			bool flag = !this.IsDialogRunning;
			if (flag)
			{
				throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
			}
			NativeMethods.SendMessage(this.Handle, (item is TaskDialogButton) ? 1126 : 1134, new IntPtr(item.Id), IntPtr.Zero);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000069A8 File Offset: 0x00004BA8
		private TaskDialogButton ShowDialog(IntPtr owner)
		{
			bool flag = !TaskDialog.OSSupportsTaskDialogs;
			if (flag)
			{
				throw new NotSupportedException(Resources.TaskDialogsNotSupportedError);
			}
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				throw new InvalidOperationException(Resources.TaskDialogRunningError);
			}
			bool flag2 = this._buttons.Count == 0;
			if (flag2)
			{
				throw new InvalidOperationException(Resources.TaskDialogNoButtonsError);
			}
			this._config.hwndParent = owner;
			this._config.dwCommonButtons = (NativeMethods.TaskDialogCommonButtonFlags)0;
			this._config.pButtons = IntPtr.Zero;
			this._config.cButtons = 0U;
			List<NativeMethods.TASKDIALOG_BUTTON> list = this.SetupButtons();
			List<NativeMethods.TASKDIALOG_BUTTON> list2 = this.SetupRadioButtons();
			this.SetupIcon();
			TaskDialogButton taskDialogButton2;
			try
			{
				TaskDialog.MarshalButtons(list, out this._config.pButtons, out this._config.cButtons);
				TaskDialog.MarshalButtons(list2, out this._config.pRadioButtons, out this._config.cRadioButtons);
				int num;
				int num2;
				bool flag3;
				using (new ComCtlv6ActivationContext(true))
				{
					NativeMethods.TaskDialogIndirect(ref this._config, out num, out num2, out flag3);
				}
				this.IsVerificationChecked = flag3;
				TaskDialogRadioButton taskDialogRadioButton;
				bool flag4 = this._radioButtonsById.TryGetValue(num2, ref taskDialogRadioButton);
				if (flag4)
				{
					taskDialogRadioButton.Checked = true;
				}
				TaskDialogButton taskDialogButton;
				bool flag5 = this._buttonsById.TryGetValue(num, ref taskDialogButton);
				if (flag5)
				{
					taskDialogButton2 = taskDialogButton;
				}
				else
				{
					taskDialogButton2 = null;
				}
			}
			finally
			{
				TaskDialog.CleanUpButtons(ref this._config.pButtons, ref this._config.cButtons);
				TaskDialog.CleanUpButtons(ref this._config.pRadioButtons, ref this._config.cRadioButtons);
			}
			return taskDialogButton2;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006B50 File Offset: 0x00004D50
		internal void UpdateDialog()
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				bool flag = this._inEventHandler > 0;
				if (flag)
				{
					this._updatePending = true;
				}
				else
				{
					this._updatePending = false;
					TaskDialog.CleanUpButtons(ref this._config.pButtons, ref this._config.cButtons);
					TaskDialog.CleanUpButtons(ref this._config.pRadioButtons, ref this._config.cRadioButtons);
					this._config.dwCommonButtons = (NativeMethods.TaskDialogCommonButtonFlags)0;
					List<NativeMethods.TASKDIALOG_BUTTON> list = this.SetupButtons();
					List<NativeMethods.TASKDIALOG_BUTTON> list2 = this.SetupRadioButtons();
					this.SetupIcon();
					TaskDialog.MarshalButtons(list, out this._config.pButtons, out this._config.cButtons);
					TaskDialog.MarshalButtons(list2, out this._config.pRadioButtons, out this._config.cRadioButtons);
					int num = Marshal.SizeOf(this._config);
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					try
					{
						Marshal.StructureToPtr(this._config, intPtr, false);
						NativeMethods.SendMessage(this.Handle, 1125, IntPtr.Zero, intPtr);
					}
					finally
					{
						Marshal.DestroyStructure(intPtr, typeof(NativeMethods.TASKDIALOGCONFIG));
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006C9C File Offset: 0x00004E9C
		private bool IsDialogRunning
		{
			get
			{
				return this._handle != IntPtr.Zero;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006CC0 File Offset: 0x00004EC0
		private void SetElementText(NativeMethods.TaskDialogElements element, string text)
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				IntPtr intPtr = Marshal.StringToHGlobalUni(text);
				try
				{
					IntPtr intPtr2 = NativeMethods.SendMessage(this.Handle, 1132, new IntPtr((int)element), intPtr);
				}
				finally
				{
					bool flag = intPtr != IntPtr.Zero;
					if (flag)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006D28 File Offset: 0x00004F28
		private void SetupIcon()
		{
			this.SetupIcon(this.MainIcon, this.CustomMainIcon, NativeMethods.TaskDialogFlags.UseHIconMain);
			this.SetupIcon(this.FooterIcon, this.CustomFooterIcon, NativeMethods.TaskDialogFlags.UseHIconFooter);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006D54 File Offset: 0x00004F54
		private void SetupIcon(TaskDialogIcon icon, Icon customIcon, NativeMethods.TaskDialogFlags flag)
		{
			this.SetFlag(flag, false);
			bool flag2 = icon == TaskDialogIcon.Custom;
			if (flag2)
			{
				bool flag3 = customIcon != null;
				if (flag3)
				{
					this.SetFlag(flag, true);
					bool flag4 = flag == NativeMethods.TaskDialogFlags.UseHIconMain;
					if (flag4)
					{
						this._config.hMainIcon = customIcon.Handle;
					}
					else
					{
						this._config.hFooterIcon = customIcon.Handle;
					}
				}
			}
			else
			{
				bool flag5 = flag == NativeMethods.TaskDialogFlags.UseHIconMain;
				if (flag5)
				{
					this._config.hMainIcon = new IntPtr((int)icon);
				}
				else
				{
					this._config.hFooterIcon = new IntPtr((int)icon);
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006DE4 File Offset: 0x00004FE4
		private static void CleanUpButtons(ref IntPtr buttons, ref uint count)
		{
			bool flag = buttons != IntPtr.Zero;
			if (flag)
			{
				int num = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
				int num2 = 0;
				while ((long)num2 < (long)((ulong)count))
				{
					IntPtr intPtr;
					intPtr..ctor(buttons.ToInt64() + (long)(num2 * num));
					Marshal.DestroyStructure(intPtr, typeof(NativeMethods.TASKDIALOG_BUTTON));
					num2++;
				}
				Marshal.FreeHGlobal(buttons);
				buttons = IntPtr.Zero;
				count = 0U;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006E60 File Offset: 0x00005060
		private static void MarshalButtons(List<NativeMethods.TASKDIALOG_BUTTON> buttons, out IntPtr buttonsPtr, out uint count)
		{
			buttonsPtr = IntPtr.Zero;
			count = 0U;
			bool flag = buttons.Count > 0;
			if (flag)
			{
				int num = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
				buttonsPtr = Marshal.AllocHGlobal(num * buttons.Count);
				for (int i = 0; i < buttons.Count; i++)
				{
					IntPtr intPtr;
					intPtr..ctor(buttonsPtr.ToInt64() + (long)(i * num));
					Marshal.StructureToPtr(buttons[i], intPtr, false);
				}
				count = (uint)buttons.Count;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006EF0 File Offset: 0x000050F0
		private List<NativeMethods.TASKDIALOG_BUTTON> SetupButtons()
		{
			this._buttonsById = new Dictionary<int, TaskDialogButton>();
			List<NativeMethods.TASKDIALOG_BUTTON> list = new List<NativeMethods.TASKDIALOG_BUTTON>();
			this._config.nDefaultButton = 0;
			foreach (TaskDialogButton taskDialogButton in this.Buttons)
			{
				bool flag = taskDialogButton.Id < 1;
				if (flag)
				{
					throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
				}
				this._buttonsById.Add(taskDialogButton.Id, taskDialogButton);
				bool @default = taskDialogButton.Default;
				if (@default)
				{
					this._config.nDefaultButton = taskDialogButton.Id;
				}
				bool flag2 = taskDialogButton.ButtonType == ButtonType.Custom;
				if (flag2)
				{
					bool flag3 = string.IsNullOrEmpty(taskDialogButton.Text);
					if (flag3)
					{
						throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
					}
					NativeMethods.TASKDIALOG_BUTTON taskdialog_BUTTON = default(NativeMethods.TASKDIALOG_BUTTON);
					taskdialog_BUTTON.nButtonID = taskDialogButton.Id;
					taskdialog_BUTTON.pszButtonText = taskDialogButton.Text;
					bool flag4 = this.ButtonStyle == TaskDialogButtonStyle.CommandLinks || (this.ButtonStyle == TaskDialogButtonStyle.CommandLinksNoIcon && !string.IsNullOrEmpty(taskDialogButton.CommandLinkNote));
					if (flag4)
					{
						taskdialog_BUTTON.pszButtonText = taskdialog_BUTTON.pszButtonText + "\n" + taskDialogButton.CommandLinkNote;
					}
					list.Add(taskdialog_BUTTON);
				}
				else
				{
					this._config.dwCommonButtons = this._config.dwCommonButtons | taskDialogButton.ButtonFlag;
				}
			}
			return list;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007074 File Offset: 0x00005274
		private List<NativeMethods.TASKDIALOG_BUTTON> SetupRadioButtons()
		{
			this._radioButtonsById = new Dictionary<int, TaskDialogRadioButton>();
			List<NativeMethods.TASKDIALOG_BUTTON> list = new List<NativeMethods.TASKDIALOG_BUTTON>();
			this._config.nDefaultRadioButton = 0;
			foreach (TaskDialogRadioButton taskDialogRadioButton in this.RadioButtons)
			{
				bool flag = string.IsNullOrEmpty(taskDialogRadioButton.Text);
				if (flag)
				{
					throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
				}
				bool flag2 = taskDialogRadioButton.Id < 1;
				if (flag2)
				{
					throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
				}
				this._radioButtonsById.Add(taskDialogRadioButton.Id, taskDialogRadioButton);
				bool @checked = taskDialogRadioButton.Checked;
				if (@checked)
				{
					this._config.nDefaultRadioButton = taskDialogRadioButton.Id;
				}
				list.Add(new NativeMethods.TASKDIALOG_BUTTON
				{
					nButtonID = taskDialogRadioButton.Id,
					pszButtonText = taskDialogRadioButton.Text
				});
			}
			this.SetFlag(NativeMethods.TaskDialogFlags.NoDefaultRadioButton, this._config.nDefaultRadioButton == 0);
			return list;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007194 File Offset: 0x00005394
		private void SetFlag(NativeMethods.TaskDialogFlags flag, bool value)
		{
			if (value)
			{
				this._config.dwFlags = this._config.dwFlags | flag;
			}
			else
			{
				this._config.dwFlags = this._config.dwFlags & ~flag;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000071CC File Offset: 0x000053CC
		private bool GetFlag(NativeMethods.TaskDialogFlags flag)
		{
			return (this._config.dwFlags & flag) > (NativeMethods.TaskDialogFlags)0;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000071F0 File Offset: 0x000053F0
		private uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData)
		{
			Interlocked.Increment(ref this._inEventHandler);
			uint num;
			try
			{
				switch (uNotification)
				{
				case 0U:
					this._handle = hwnd;
					this.DialogCreated();
					this.OnCreated(EventArgs.Empty);
					break;
				case 1U:
					this.DialogCreated();
					break;
				case 2U:
				{
					TaskDialogButton taskDialogButton;
					bool flag = this._buttonsById.TryGetValue((int)wParam, ref taskDialogButton);
					if (flag)
					{
						TaskDialogItemClickedEventArgs taskDialogItemClickedEventArgs = new TaskDialogItemClickedEventArgs(taskDialogButton);
						this.OnButtonClicked(taskDialogItemClickedEventArgs);
						bool cancel = taskDialogItemClickedEventArgs.Cancel;
						if (cancel)
						{
							return 1U;
						}
					}
					break;
				}
				case 3U:
				{
					string text = Marshal.PtrToStringUni(lParam);
					this.OnHyperlinkClicked(new HyperlinkClickedEventArgs(text));
					break;
				}
				case 4U:
				{
					TimerEventArgs timerEventArgs = new TimerEventArgs(wParam.ToInt32());
					this.OnTimer(timerEventArgs);
					return timerEventArgs.ResetTickCount ? 1U : 0U;
				}
				case 5U:
					this._handle = IntPtr.Zero;
					this.OnDestroyed(EventArgs.Empty);
					break;
				case 6U:
				{
					TaskDialogRadioButton taskDialogRadioButton;
					bool flag2 = this._radioButtonsById.TryGetValue((int)wParam, ref taskDialogRadioButton);
					if (flag2)
					{
						taskDialogRadioButton.Checked = true;
						TaskDialogItemClickedEventArgs taskDialogItemClickedEventArgs2 = new TaskDialogItemClickedEventArgs(taskDialogRadioButton);
						this.OnRadioButtonClicked(taskDialogItemClickedEventArgs2);
					}
					break;
				}
				case 8U:
					this.IsVerificationChecked = (int)wParam == 1;
					this.OnVerificationClicked(EventArgs.Empty);
					break;
				case 9U:
					this.OnHelpRequested(EventArgs.Empty);
					break;
				case 10U:
					this.OnExpandButtonClicked(new ExpandButtonClickedEventArgs(wParam.ToInt32() != 0));
					break;
				}
				num = 0U;
			}
			finally
			{
				Interlocked.Decrement(ref this._inEventHandler);
				bool updatePending = this._updatePending;
				if (updatePending)
				{
					this.UpdateDialog();
				}
			}
			return num;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000073D8 File Offset: 0x000055D8
		private void DialogCreated()
		{
			bool flag = this._config.hwndParent == IntPtr.Zero && this._windowIcon != null;
			if (flag)
			{
				NativeMethods.SendMessage(this.Handle, 128, new IntPtr(0), this._windowIcon.Handle);
			}
			foreach (TaskDialogButton taskDialogButton in this.Buttons)
			{
				bool flag2 = !taskDialogButton.Enabled;
				if (flag2)
				{
					this.SetItemEnabled(taskDialogButton);
				}
				bool elevationRequired = taskDialogButton.ElevationRequired;
				if (elevationRequired)
				{
					this.SetButtonElevationRequired(taskDialogButton);
				}
			}
			this.UpdateProgressBarStyle();
			this.UpdateProgressBarMarqueeSpeed();
			this.UpdateProgressBarRange();
			this.UpdateProgressBarValue();
			this.UpdateProgressBarState();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000074BC File Offset: 0x000056BC
		private void UpdateProgressBarStyle()
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, 1127, new IntPtr((this.ProgressBarStyle == ProgressBarStyle.MarqueeProgressBar) ? 1 : 0), IntPtr.Zero);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007500 File Offset: 0x00005700
		private void UpdateProgressBarMarqueeSpeed()
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, 1131, new IntPtr((this.ProgressBarMarqueeAnimationSpeed > 0) ? 1 : 0), new IntPtr(this.ProgressBarMarqueeAnimationSpeed));
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007548 File Offset: 0x00005748
		private void UpdateProgressBarRange()
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, 1129, IntPtr.Zero, new IntPtr((this.ProgressBarMaximum << 16) | this.ProgressBarMinimum));
			}
			bool flag = this.ProgressBarValue < this.ProgressBarMinimum;
			if (flag)
			{
				this.ProgressBarValue = this.ProgressBarMinimum;
			}
			bool flag2 = this.ProgressBarValue > this.ProgressBarMaximum;
			if (flag2)
			{
				this.ProgressBarValue = this.ProgressBarMaximum;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000075CC File Offset: 0x000057CC
		private void UpdateProgressBarValue()
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, 1130, new IntPtr(this.ProgressBarValue), IntPtr.Zero);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007608 File Offset: 0x00005808
		private void UpdateProgressBarState()
		{
			bool isDialogRunning = this.IsDialogRunning;
			if (isDialogRunning)
			{
				NativeMethods.SendMessage(this.Handle, 1128, new IntPtr((int)(this.ProgressBarState + 1)), IntPtr.Zero);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007648 File Offset: 0x00005848
		private void CheckCrossThreadCall()
		{
			IntPtr handle = this._handle;
			bool flag = handle != IntPtr.Zero;
			if (flag)
			{
				int num;
				int windowThreadProcessId = NativeMethods.GetWindowThreadProcessId(handle, out num);
				int currentThreadId = NativeMethods.GetCurrentThreadId();
				bool flag2 = windowThreadProcessId != currentThreadId;
				if (flag2)
				{
					throw new InvalidOperationException(Resources.TaskDialogIllegalCrossThreadCallError);
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007698 File Offset: 0x00005898
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				this.CheckCrossThreadCall();
				return this._handle;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000076B8 File Offset: 0x000058B8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					bool flag = this.components != null;
					if (flag)
					{
						this.components.Dispose();
						this.components = null;
					}
					bool flag2 = this._buttons != null;
					if (flag2)
					{
						foreach (TaskDialogButton taskDialogButton in this._buttons)
						{
							taskDialogButton.Dispose();
						}
						this._buttons.Clear();
					}
					bool flag3 = this._radioButtons != null;
					if (flag3)
					{
						foreach (TaskDialogRadioButton taskDialogRadioButton in this._radioButtons)
						{
							taskDialogRadioButton.Dispose();
						}
						this._radioButtons.Clear();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000077D4 File Offset: 0x000059D4
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x0400007A RID: 122
		private TaskDialogItemCollection<TaskDialogButton> _buttons;

		// Token: 0x0400007B RID: 123
		private TaskDialogItemCollection<TaskDialogRadioButton> _radioButtons;

		// Token: 0x0400007C RID: 124
		private NativeMethods.TASKDIALOGCONFIG _config = default(NativeMethods.TASKDIALOGCONFIG);

		// Token: 0x0400007D RID: 125
		private TaskDialogIcon _mainIcon;

		// Token: 0x0400007E RID: 126
		private Icon _customMainIcon;

		// Token: 0x0400007F RID: 127
		private Icon _customFooterIcon;

		// Token: 0x04000080 RID: 128
		private TaskDialogIcon _footerIcon;

		// Token: 0x04000081 RID: 129
		private Dictionary<int, TaskDialogButton> _buttonsById;

		// Token: 0x04000082 RID: 130
		private Dictionary<int, TaskDialogRadioButton> _radioButtonsById;

		// Token: 0x04000083 RID: 131
		private IntPtr _handle;

		// Token: 0x04000084 RID: 132
		private int _progressBarMarqueeAnimationSpeed = 100;

		// Token: 0x04000085 RID: 133
		private int _progressBarMinimimum;

		// Token: 0x04000086 RID: 134
		private int _progressBarMaximum = 100;

		// Token: 0x04000087 RID: 135
		private int _progressBarValue;

		// Token: 0x04000088 RID: 136
		private ProgressBarState _progressBarState = ProgressBarState.Normal;

		// Token: 0x04000089 RID: 137
		private int _inEventHandler;

		// Token: 0x0400008A RID: 138
		private bool _updatePending;

		// Token: 0x0400008B RID: 139
		private object _tag;

		// Token: 0x0400008C RID: 140
		private Icon _windowIcon;

		// Token: 0x0400008D RID: 141
		private IContainer components = null;
	}
}
