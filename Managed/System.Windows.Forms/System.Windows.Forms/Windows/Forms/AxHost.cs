using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Wraps ActiveX controls and exposes them as fully featured Windows Forms controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004D RID: 77
	[Designer("System.Windows.Forms.Design.AxHostDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DesignTimeVisible(false)]
	[DefaultEvent("Enter")]
	[ClassInterface(1)]
	[MonoTODO("Possibly implement this for Win32; find a way for Linux and Mac")]
	[ToolboxItem(false)]
	[ComVisible(true)]
	public abstract class AxHost : Control, ISupportInitialize, ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost" /> class, wrapping the ActiveX control indicated by the specified CLSID. </summary>
		/// <param name="clsid">The CLSID of the ActiveX control to wrap.</param>
		// Token: 0x06000271 RID: 625 RVA: 0x00011448 File Offset: 0x0000F648
		protected AxHost(string clsid)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost" /> class, wrapping the ActiveX control indicated by the specified CLSID, and using the shortcut-menu behavior indicated by the specified <paramref name="flags" /> value.</summary>
		/// <param name="clsid">The CLSID of the ActiveX control to wrap.</param>
		/// <param name="flags">An <see cref="T:System.Int32" /> that modifies the shortcut-menu behavior for the control.</param>
		// Token: 0x06000272 RID: 626 RVA: 0x0001145C File Offset: 0x0000F65C
		protected AxHost(string clsid, int flags)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00011470 File Offset: 0x0000F670
		// Note: this type is marked as 'beforefieldinit'.
		static AxHost()
		{
			AxHost.MouseClickEvent = new object();
			AxHost.MouseDoubleClickEvent = new object();
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BackColorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000274 RID: 628 RVA: 0x00011488 File Offset: 0x0000F688
		// (remove) Token: 0x06000275 RID: 629 RVA: 0x00011494 File Offset: 0x0000F694
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BackgroundImageChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000276 RID: 630 RVA: 0x000114A0 File Offset: 0x0000F6A0
		// (remove) Token: 0x06000277 RID: 631 RVA: 0x000114AC File Offset: 0x0000F6AC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BindingContextChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000278 RID: 632 RVA: 0x000114B8 File Offset: 0x0000F6B8
		// (remove) Token: 0x06000279 RID: 633 RVA: 0x000114C4 File Offset: 0x0000F6C4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BindingContextChanged
		{
			add
			{
				base.BindingContextChanged += value;
			}
			remove
			{
				base.BindingContextChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ChangeUICues" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600027A RID: 634 RVA: 0x000114D0 File Offset: 0x0000F6D0
		// (remove) Token: 0x0600027B RID: 635 RVA: 0x000114DC File Offset: 0x0000F6DC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event UICuesEventHandler ChangeUICues
		{
			add
			{
				base.ChangeUICues += value;
			}
			remove
			{
				base.ChangeUICues -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Click" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600027C RID: 636 RVA: 0x000114E8 File Offset: 0x0000F6E8
		// (remove) Token: 0x0600027D RID: 637 RVA: 0x000114F4 File Offset: 0x0000F6F4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ContextMenuChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600027E RID: 638 RVA: 0x00011500 File Offset: 0x0000F700
		// (remove) Token: 0x0600027F RID: 639 RVA: 0x0001150C File Offset: 0x0000F70C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				base.ContextMenuChanged += value;
			}
			remove
			{
				base.ContextMenuChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.CursorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000280 RID: 640 RVA: 0x00011518 File Offset: 0x0000F718
		// (remove) Token: 0x06000281 RID: 641 RVA: 0x00011524 File Offset: 0x0000F724
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DoubleClick" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000282 RID: 642 RVA: 0x00011530 File Offset: 0x0000F730
		// (remove) Token: 0x06000283 RID: 643 RVA: 0x0001153C File Offset: 0x0000F73C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragDrop" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000284 RID: 644 RVA: 0x00011548 File Offset: 0x0000F748
		// (remove) Token: 0x06000285 RID: 645 RVA: 0x00011554 File Offset: 0x0000F754
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragEnter" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000286 RID: 646 RVA: 0x00011560 File Offset: 0x0000F760
		// (remove) Token: 0x06000287 RID: 647 RVA: 0x0001156C File Offset: 0x0000F76C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragLeave" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000288 RID: 648 RVA: 0x00011578 File Offset: 0x0000F778
		// (remove) Token: 0x06000289 RID: 649 RVA: 0x00011584 File Offset: 0x0000F784
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragOver" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600028A RID: 650 RVA: 0x00011590 File Offset: 0x0000F790
		// (remove) Token: 0x0600028B RID: 651 RVA: 0x0001159C File Offset: 0x0000F79C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.EnabledChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600028C RID: 652 RVA: 0x000115A8 File Offset: 0x0000F7A8
		// (remove) Token: 0x0600028D RID: 653 RVA: 0x000115B4 File Offset: 0x0000F7B4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.FontChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600028E RID: 654 RVA: 0x000115C0 File Offset: 0x0000F7C0
		// (remove) Token: 0x0600028F RID: 655 RVA: 0x000115CC File Offset: 0x0000F7CC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ForeColorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000290 RID: 656 RVA: 0x000115D8 File Offset: 0x0000F7D8
		// (remove) Token: 0x06000291 RID: 657 RVA: 0x000115E4 File Offset: 0x0000F7E4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.GiveFeedback" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000292 RID: 658 RVA: 0x000115F0 File Offset: 0x0000F7F0
		// (remove) Token: 0x06000293 RID: 659 RVA: 0x000115FC File Offset: 0x0000F7FC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.HelpRequested" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000294 RID: 660 RVA: 0x00011608 File Offset: 0x0000F808
		// (remove) Token: 0x06000295 RID: 661 RVA: 0x00011614 File Offset: 0x0000F814
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event HelpEventHandler HelpRequested
		{
			add
			{
				base.HelpRequested += value;
			}
			remove
			{
				base.HelpRequested -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ImeModeChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000296 RID: 662 RVA: 0x00011620 File Offset: 0x0000F820
		// (remove) Token: 0x06000297 RID: 663 RVA: 0x0001162C File Offset: 0x0000F82C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyDown" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000298 RID: 664 RVA: 0x00011638 File Offset: 0x0000F838
		// (remove) Token: 0x06000299 RID: 665 RVA: 0x00011644 File Offset: 0x0000F844
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyPress" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600029A RID: 666 RVA: 0x00011650 File Offset: 0x0000F850
		// (remove) Token: 0x0600029B RID: 667 RVA: 0x0001165C File Offset: 0x0000F85C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyUp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600029C RID: 668 RVA: 0x00011668 File Offset: 0x0000F868
		// (remove) Token: 0x0600029D RID: 669 RVA: 0x00011674 File Offset: 0x0000F874
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Layout" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600029E RID: 670 RVA: 0x00011680 File Offset: 0x0000F880
		// (remove) Token: 0x0600029F RID: 671 RVA: 0x0001168C File Offset: 0x0000F88C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event LayoutEventHandler Layout
		{
			add
			{
				base.Layout += value;
			}
			remove
			{
				base.Layout -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060002A0 RID: 672 RVA: 0x00011698 File Offset: 0x0000F898
		// (remove) Token: 0x060002A1 RID: 673 RVA: 0x000116A4 File Offset: 0x0000F8A4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseEnter" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060002A2 RID: 674 RVA: 0x000116B0 File Offset: 0x0000F8B0
		// (remove) Token: 0x060002A3 RID: 675 RVA: 0x000116BC File Offset: 0x0000F8BC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseHover" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060002A4 RID: 676 RVA: 0x000116C8 File Offset: 0x0000F8C8
		// (remove) Token: 0x060002A5 RID: 677 RVA: 0x000116D4 File Offset: 0x0000F8D4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseHover
		{
			add
			{
				base.MouseHover += value;
			}
			remove
			{
				base.MouseHover -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseLeave" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x000116E0 File Offset: 0x0000F8E0
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x000116EC File Offset: 0x0000F8EC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060002A8 RID: 680 RVA: 0x000116F8 File Offset: 0x0000F8F8
		// (remove) Token: 0x060002A9 RID: 681 RVA: 0x00011704 File Offset: 0x0000F904
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060002AA RID: 682 RVA: 0x00011710 File Offset: 0x0000F910
		// (remove) Token: 0x060002AB RID: 683 RVA: 0x0001171C File Offset: 0x0000F91C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseWheel" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060002AC RID: 684 RVA: 0x00011728 File Offset: 0x0000F928
		// (remove) Token: 0x060002AD RID: 685 RVA: 0x00011734 File Offset: 0x0000F934
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseWheel
		{
			add
			{
				base.MouseWheel += value;
			}
			remove
			{
				base.MouseWheel -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Paint" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060002AE RID: 686 RVA: 0x00011740 File Offset: 0x0000F940
		// (remove) Token: 0x060002AF RID: 687 RVA: 0x0001174C File Offset: 0x0000F94C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.QueryAccessibilityHelp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060002B0 RID: 688 RVA: 0x00011758 File Offset: 0x0000F958
		// (remove) Token: 0x060002B1 RID: 689 RVA: 0x00011764 File Offset: 0x0000F964
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.QueryAccessibilityHelp += value;
			}
			remove
			{
				base.QueryAccessibilityHelp -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.QueryContinueDrag" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060002B2 RID: 690 RVA: 0x00011770 File Offset: 0x0000F970
		// (remove) Token: 0x060002B3 RID: 691 RVA: 0x0001177C File Offset: 0x0000F97C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.RightToLeftChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060002B4 RID: 692 RVA: 0x00011788 File Offset: 0x0000F988
		// (remove) Token: 0x060002B5 RID: 693 RVA: 0x00011794 File Offset: 0x0000F994
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.StyleChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060002B6 RID: 694 RVA: 0x000117A0 File Offset: 0x0000F9A0
		// (remove) Token: 0x060002B7 RID: 695 RVA: 0x000117AC File Offset: 0x0000F9AC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler StyleChanged
		{
			add
			{
				base.StyleChanged += value;
			}
			remove
			{
				base.StyleChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060002B8 RID: 696 RVA: 0x000117B8 File Offset: 0x0000F9B8
		// (remove) Token: 0x060002B9 RID: 697 RVA: 0x000117C4 File Offset: 0x0000F9C4
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

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060002BA RID: 698 RVA: 0x000117D0 File Offset: 0x0000F9D0
		// (remove) Token: 0x060002BB RID: 699 RVA: 0x000117E4 File Offset: 0x0000F9E4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(AxHost.MouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(AxHost.MouseClickEvent, value);
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060002BC RID: 700 RVA: 0x000117F8 File Offset: 0x0000F9F8
		// (remove) Token: 0x060002BD RID: 701 RVA: 0x0001180C File Offset: 0x0000FA0C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(AxHost.MouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(AxHost.MouseDoubleClickEvent, value);
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.TextChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060002BE RID: 702 RVA: 0x00011820 File Offset: 0x0000FA20
		// (remove) Token: 0x060002BF RID: 703 RVA: 0x0001182C File Offset: 0x0000FA2C
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

		/// <summary>Returns a collection of type <see cref="T:System.Attribute" /> for the current object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the current object.</returns>
		// Token: 0x060002C0 RID: 704 RVA: 0x00011838 File Offset: 0x0000FA38
		[EditorBrowsable(2)]
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the class name of the current object.</summary>
		/// <returns>Returns null in all cases.</returns>
		// Token: 0x060002C1 RID: 705 RVA: 0x00011844 File Offset: 0x0000FA44
		[EditorBrowsable(2)]
		string ICustomTypeDescriptor.GetClassName()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the name of the current object.</summary>
		/// <returns>Returns null in all cases.</returns>
		// Token: 0x060002C2 RID: 706 RVA: 0x00011850 File Offset: 0x0000FA50
		[EditorBrowsable(2)]
		string ICustomTypeDescriptor.GetComponentName()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns a type converter for the current object.</summary>
		/// <returns>Returns null in all cases.</returns>
		// Token: 0x060002C3 RID: 707 RVA: 0x0001185C File Offset: 0x0000FA5C
		[EditorBrowsable(2)]
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the default event for the current object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for the current object, or null if the object does not have events.</returns>
		// Token: 0x060002C4 RID: 708 RVA: 0x00011868 File Offset: 0x0000FA68
		[EditorBrowsable(2)]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the default property for the current object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for the current object, or null if the object does not have properties.</returns>
		// Token: 0x060002C5 RID: 709 RVA: 0x00011874 File Offset: 0x0000FA74
		[EditorBrowsable(2)]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns an editor of the specified type for the current object.</summary>
		/// <returns>An object of the specified type that is the editor for the current object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for the current object.</param>
		// Token: 0x060002C6 RID: 710 RVA: 0x00011880 File Offset: 0x0000FA80
		[EditorBrowsable(2)]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the events for the current object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for the current object.</returns>
		// Token: 0x060002C7 RID: 711 RVA: 0x0001188C File Offset: 0x0000FA8C
		[EditorBrowsable(2)]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the events for the current object using the specified attribute array as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for the <see cref="T:System.Windows.Forms.AxHost" /> that match the given set of attributes.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x060002C8 RID: 712 RVA: 0x00011898 File Offset: 0x0000FA98
		[EditorBrowsable(2)]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the properties for the current object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the events for the current object.</returns>
		// Token: 0x060002C9 RID: 713 RVA: 0x000118A4 File Offset: 0x0000FAA4
		[EditorBrowsable(2)]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the properties for the current object using the specified attribute array as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the events for the current <see cref="T:System.Windows.Forms.AxHost" /> that match the given set of attributes.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x060002CA RID: 714 RVA: 0x000118B0 File Offset: 0x0000FAB0
		[EditorBrowsable(2)]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns the object that owns the specified value.</summary>
		/// <returns>The current object.</returns>
		/// <param name="pd">Not used.</param>
		// Token: 0x060002CB RID: 715 RVA: 0x000118BC File Offset: 0x0000FABC
		[EditorBrowsable(2)]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000118C8 File Offset: 0x0000FAC8
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000118D4 File Offset: 0x0000FAD4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Color BackColor
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000118E0 File Offset: 0x0000FAE0
		// (set) Token: 0x060002CF RID: 719 RVA: 0x000118EC File Offset: 0x0000FAEC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image BackgroundImage
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000118F8 File Offset: 0x0000FAF8
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00011904 File Offset: 0x0000FB04
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Gets or sets the control containing the ActiveX control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContainerControl" /> that represents the control containing the ActiveX control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00011910 File Offset: 0x0000FB10
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0001191C File Offset: 0x0000FB1C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public ContainerControl ContainingControl
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00011928 File Offset: 0x0000FB28
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00011934 File Offset: 0x0000FB34
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override ContextMenu ContextMenu
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00011940 File Offset: 0x0000FB40
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0001194C File Offset: 0x0000FB4C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Cursor Cursor
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00011958 File Offset: 0x0000FB58
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public bool EditMode
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00011964 File Offset: 0x0000FB64
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00011970 File Offset: 0x0000FB70
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new virtual bool Enabled
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0001197C File Offset: 0x0000FB7C
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00011988 File Offset: 0x0000FB88
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Font Font
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00011994 File Offset: 0x0000FB94
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000119A0 File Offset: 0x0000FBA0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Gets a value indicating whether the ActiveX control has an About dialog box.</summary>
		/// <returns>true if the ActiveX control has an About dialog box; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000119AC File Offset: 0x0000FBAC
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool HasAboutBox
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImeMode" /> value.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000119B8 File Offset: 0x0000FBB8
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x000119C4 File Offset: 0x0000FBC4
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new ImeMode ImeMode
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Gets or sets the persisted state of the ActiveX control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.AxHost.State" /> that represents the persisted state of the ActiveX control.</returns>
		/// <exception cref="T:System.Exception">The ActiveX control is already loaded. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000119D0 File Offset: 0x0000FBD0
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000119DC File Offset: 0x0000FBDC
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DefaultValue(null)]
		[RefreshProperties(1)]
		public AxHost.State OcxState
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000119E8 File Offset: 0x0000FBE8
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000119F4 File Offset: 0x0000FBF4
		[Localizable(true)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new virtual bool RightToLeft
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AA RID: 170
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00011A00 File Offset: 0x0000FC00
		public override ISite Site
		{
			set
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00011A0C File Offset: 0x0000FC0C
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00011A14 File Offset: 0x0000FC14
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override CreateParams CreateParams
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00011A2C File Offset: 0x0000FC2C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011A38 File Offset: 0x0000FC38
		[EditorBrowsable(2)]
		[CLSCompliant(false)]
		protected static Color GetColorFromOleColor(uint color)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00011A44 File Offset: 0x0000FC44
		[EditorBrowsable(2)]
		protected static Font GetFontFromIFont(object font)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011A50 File Offset: 0x0000FC50
		[EditorBrowsable(2)]
		protected static Font GetFontFromIFontDisp(object font)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00011A5C File Offset: 0x0000FC5C
		[EditorBrowsable(2)]
		protected static object GetIFontDispFromFont(Font font)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011A68 File Offset: 0x0000FC68
		[EditorBrowsable(2)]
		protected static object GetIFontFromFont(Font font)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns an OLE IPictureDisp object corresponding to the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the OLE IPictureDisp object.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to convert.</param>
		// Token: 0x060002F0 RID: 752 RVA: 0x00011A74 File Offset: 0x0000FC74
		[EditorBrowsable(2)]
		protected static object GetIPictureDispFromPicture(Image image)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns an OLE IPicture object corresponding to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the OLE IPicture object.</returns>
		/// <param name="cursor">
		///   <see cref="T:System.Windows.Forms.Cursor" />
		/// </param>
		// Token: 0x060002F1 RID: 753 RVA: 0x00011A80 File Offset: 0x0000FC80
		[EditorBrowsable(2)]
		protected static object GetIPictureFromCursor(Cursor cursor)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns an OLE IPicture object corresponding to the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the OLE IPicture object.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to convert.</param>
		// Token: 0x060002F2 RID: 754 RVA: 0x00011A8C File Offset: 0x0000FC8C
		[EditorBrowsable(2)]
		protected static object GetIPictureFromPicture(Image image)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011A98 File Offset: 0x0000FC98
		[EditorBrowsable(2)]
		protected static double GetOADateFromTime(DateTime time)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		[EditorBrowsable(2)]
		[CLSCompliant(false)]
		protected static uint GetOleColorFromColor(Color color)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> corresponding to the specified OLE IPicture object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the IPicture. </returns>
		/// <param name="picture">The IPicture to convert.</param>
		// Token: 0x060002F5 RID: 757 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		[EditorBrowsable(2)]
		protected static Image GetPictureFromIPicture(object picture)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> corresponding to the specified OLE IPictureDisp object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the IPictureDisp. </returns>
		/// <param name="picture">The IPictureDisp to convert.</param>
		// Token: 0x060002F6 RID: 758 RVA: 0x00011ABC File Offset: 0x0000FCBC
		[EditorBrowsable(2)]
		protected static Image GetPictureFromIPictureDisp(object picture)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		[EditorBrowsable(2)]
		protected static DateTime GetTimeFromOADate(double date)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Begins the initialization of the ActiveX control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060002F8 RID: 760 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		[EditorBrowsable(2)]
		public void BeginInit()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002F9 RID: 761 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public void DoVerb(int verb)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Ends the initialization of an ActiveX control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002FA RID: 762 RVA: 0x00011AEC File Offset: 0x0000FCEC
		[EditorBrowsable(2)]
		public void EndInit()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Retrieves a reference to the underlying ActiveX control.</summary>
		/// <returns>An object that represents the ActiveX control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060002FB RID: 763 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		[EditorBrowsable(2)]
		public object GetOcx()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Determines if the ActiveX control has a property page.</summary>
		/// <returns>true if the ActiveX control has a property page; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060002FC RID: 764 RVA: 0x00011B04 File Offset: 0x0000FD04
		public bool HasPropertyPages()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002FD RID: 765 RVA: 0x00011B10 File Offset: 0x0000FD10
		[EditorBrowsable(2)]
		public void InvokeEditMode()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060002FE RID: 766 RVA: 0x00011B1C File Offset: 0x0000FD1C
		[EditorBrowsable(2)]
		public void MakeDirty()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002FF RID: 767 RVA: 0x00011B28 File Offset: 0x0000FD28
		public override bool PreProcessMessage(ref Message msg)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Displays the ActiveX control's About dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000300 RID: 768 RVA: 0x00011B34 File Offset: 0x0000FD34
		public void ShowAboutBox()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Displays the property pages associated with the ActiveX control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000301 RID: 769 RVA: 0x00011B40 File Offset: 0x0000FD40
		public void ShowPropertyPages()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Displays the property pages associated with the ActiveX control assigned to the specified parent control.</summary>
		/// <param name="control">The parent <see cref="T:System.Windows.Forms.Control" /> of the ActiveX control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000302 RID: 770 RVA: 0x00011B4C File Offset: 0x0000FD4C
		public void ShowPropertyPages(Control control)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>When overridden in a derived class, attaches interfaces to the underlying ActiveX control.</summary>
		// Token: 0x06000303 RID: 771 RVA: 0x00011B58 File Offset: 0x0000FD58
		protected virtual void AttachInterfaces()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011B64 File Offset: 0x0000FD64
		protected override void CreateHandle()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Called by the system to create the ActiveX control.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the ActiveX control. </returns>
		/// <param name="clsid">The CLSID of the ActiveX control.</param>
		// Token: 0x06000305 RID: 773 RVA: 0x00011B70 File Offset: 0x0000FD70
		protected virtual object CreateInstanceCore(Guid clsid)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011B7C File Offset: 0x0000FD7C
		[EditorBrowsable(2)]
		protected virtual void CreateSink()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00011B88 File Offset: 0x0000FD88
		protected override void DestroyHandle()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00011B94 File Offset: 0x0000FD94
		[EditorBrowsable(2)]
		protected virtual void DetachSink()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000309 RID: 777 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>This method is not supported by this control.</summary>
		/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
		// Token: 0x0600030A RID: 778 RVA: 0x00011BAC File Offset: 0x0000FDAC
		[EditorBrowsable(1)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Called by the system to retrieve the current bounds of the ActiveX control.</summary>
		/// <returns>The unmodified <paramref name="bounds" /> value.</returns>
		/// <param name="bounds">The original bounds of the ActiveX control.</param>
		/// <param name="factor">A scaling factor. </param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value.</param>
		// Token: 0x0600030B RID: 779 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		[EditorBrowsable(2)]
		protected new virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Determines if a character is an input character that the ActiveX control recognizes.</summary>
		/// <returns>true if the character should be sent directly to the ActiveX control and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		// Token: 0x0600030C RID: 780 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		protected override bool IsInputChar(char charCode)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600030D RID: 781 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		protected override void OnBackColorChanged(EventArgs e)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600030E RID: 782 RVA: 0x00011BDC File Offset: 0x0000FDDC
		protected override void OnFontChanged(EventArgs e)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600030F RID: 783 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		protected override void OnForeColorChanged(EventArgs e)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000310 RID: 784 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		protected override void OnHandleCreated(EventArgs e)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Not used.</summary>
		// Token: 0x06000311 RID: 785 RVA: 0x00011C00 File Offset: 0x0000FE00
		protected virtual void OnInPlaceActive()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000312 RID: 786 RVA: 0x00011C0C File Offset: 0x0000FE0C
		[EditorBrowsable(2)]
		protected override void OnLostFocus(EventArgs e)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06000313 RID: 787 RVA: 0x00011C18 File Offset: 0x0000FE18
		protected override bool ProcessDialogKey(Keys keyData)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06000314 RID: 788 RVA: 0x00011C24 File Offset: 0x0000FE24
		protected override bool ProcessMnemonic(char charCode)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00011C30 File Offset: 0x0000FE30
		[EditorBrowsable(2)]
		protected bool PropsValid()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified 32-bit signed integers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x06000316 RID: 790 RVA: 0x00011C3C File Offset: 0x0000FE3C
		[EditorBrowsable(2)]
		protected void RaiseOnMouseDown(short button, short shift, int x, int y)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified single-precision floating-point numbers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x06000317 RID: 791 RVA: 0x00011C48 File Offset: 0x0000FE48
		[EditorBrowsable(2)]
		protected void RaiseOnMouseDown(short button, short shift, float x, float y)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified objects.</summary>
		/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="o2">Not used.</param>
		/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="o4">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x06000318 RID: 792 RVA: 0x00011C54 File Offset: 0x0000FE54
		[EditorBrowsable(2)]
		protected void RaiseOnMouseDown(object o1, object o2, object o3, object o4)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified 32-bit signed integers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x06000319 RID: 793 RVA: 0x00011C60 File Offset: 0x0000FE60
		[EditorBrowsable(2)]
		protected void RaiseOnMouseMove(short button, short shift, int x, int y)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified single-precision floating-point numbers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x0600031A RID: 794 RVA: 0x00011C6C File Offset: 0x0000FE6C
		[EditorBrowsable(2)]
		protected void RaiseOnMouseMove(short button, short shift, float x, float y)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified objects.</summary>
		/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="o2">Not used.</param>
		/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="o4">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x0600031B RID: 795 RVA: 0x00011C78 File Offset: 0x0000FE78
		[EditorBrowsable(2)]
		protected void RaiseOnMouseMove(object o1, object o2, object o3, object o4)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified 32-bit signed integers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x0600031C RID: 796 RVA: 0x00011C84 File Offset: 0x0000FE84
		[EditorBrowsable(2)]
		protected void RaiseOnMouseUp(short button, short shift, int x, int y)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified single-precision floating-point numbers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x0600031D RID: 797 RVA: 0x00011C90 File Offset: 0x0000FE90
		[EditorBrowsable(2)]
		protected void RaiseOnMouseUp(short button, short shift, float x, float y)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified objects.</summary>
		/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
		/// <param name="o2">Not used.</param>
		/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="o4">The y-coordinate of a mouse click, in pixels. </param>
		// Token: 0x0600031E RID: 798 RVA: 0x00011C9C File Offset: 0x0000FE9C
		[EditorBrowsable(2)]
		protected void RaiseOnMouseUp(object o1, object o2, object o3, object o4)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Calls the <see cref="M:System.Windows.Forms.AxHost.ShowAboutBox" /> method to display the ActiveX control's About dialog box.</summary>
		/// <param name="d">The <see cref="T:System.Windows.Forms.AxHost.AboutBoxDelegate" /> to call. </param>
		// Token: 0x0600031F RID: 799 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		protected void SetAboutBoxDelegate(AxHost.AboutBoxDelegate d)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x06000320 RID: 800 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="value">true to make the control visible; otherwise, false. </param>
		// Token: 0x06000321 RID: 801 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		protected override void SetVisibleCore(bool value)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000322 RID: 802 RVA: 0x00011CCC File Offset: 0x0000FECC
		protected override void WndProc(ref Message m)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Specifies the type of member that referenced the ActiveX control while it was in an invalid state.</summary>
		// Token: 0x0200004E RID: 78
		public enum ActiveXInvokeKind
		{
			/// <summary>A method referenced the ActiveX control.</summary>
			// Token: 0x04000601 RID: 1537
			MethodInvoke,
			/// <summary>The get accessor of a property referenced the ActiveX control.</summary>
			// Token: 0x04000602 RID: 1538
			PropertyGet,
			/// <summary>The set accessor of a property referenced the ActiveX control.</summary>
			// Token: 0x04000603 RID: 1539
			PropertySet
		}

		/// <summary>Provides an editor that uses a modal dialog box to display a property page for an ActiveX control.</summary>
		// Token: 0x0200004F RID: 79
		[ComVisible(false)]
		public class AxComponentEditor : WindowsFormsComponentEditor
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.AxComponentEditor" /> class. </summary>
			// Token: 0x06000323 RID: 803 RVA: 0x00011CD8 File Offset: 0x0000FED8
			public AxComponentEditor()
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <returns>true if the component was changed during editing; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
			/// <param name="obj"></param>
			/// <param name="parent"></param>
			// Token: 0x06000324 RID: 804 RVA: 0x00011CEC File Offset: 0x0000FEEC
			public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Specifies the CLSID of an ActiveX control hosted by an <see cref="T:System.Windows.Forms.AxHost" /> control.</summary>
		// Token: 0x02000050 RID: 80
		[AttributeUsage(4, Inherited = false)]
		public sealed class ClsidAttribute : Attribute
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.ClsidAttribute" /> class. </summary>
			/// <param name="clsid">The CLSID of the ActiveX control.</param>
			// Token: 0x06000325 RID: 805 RVA: 0x00011CF8 File Offset: 0x0000FEF8
			public ClsidAttribute(string clsid)
			{
				this.clsid = clsid;
			}

			/// <summary>The CLSID of the ActiveX control.</summary>
			/// <returns>The CLSID of the ActiveX control.</returns>
			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000326 RID: 806 RVA: 0x00011D08 File Offset: 0x0000FF08
			public string Value
			{
				get
				{
					return this.clsid;
				}
			}

			// Token: 0x04000604 RID: 1540
			private string clsid;
		}

		/// <summary>Connects an ActiveX control to a client that handles the control’s events.</summary>
		// Token: 0x02000051 RID: 81
		public class ConnectionPointCookie
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.ConnectionPointCookie" /> class.</summary>
			/// <param name="source">A connectable object that contains connection points.</param>
			/// <param name="sink">The client's sink which receives outgoing calls from the connection point.</param>
			/// <param name="eventInterface">The outgoing interface whose connection point object is being requested.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="source" /> does not implement <paramref name="eventInterface" />.</exception>
			/// <exception cref="T:System.InvalidCastException">
			///   <paramref name="sink" /> does not implement <paramref name="eventInterface" />.-or-<paramref name="source" /> does not implement <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPointContainer" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The connection point has already reached its limit of connections and cannot accept any more.</exception>
			// Token: 0x06000327 RID: 807 RVA: 0x00011D10 File Offset: 0x0000FF10
			public ConnectionPointCookie(object source, object sink, Type eventInterface)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>Disconnects the ActiveX control from the client.</summary>
			// Token: 0x06000328 RID: 808 RVA: 0x00011D24 File Offset: 0x0000FF24
			public void Disconnect()
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.AxHost.ConnectionPointCookie" /> class.</summary>
			// Token: 0x06000329 RID: 809 RVA: 0x00011D30 File Offset: 0x0000FF30
			~ConnectionPointCookie()
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>The exception that is thrown when the ActiveX control is referenced while in an invalid state.</summary>
		// Token: 0x02000052 RID: 82
		public class InvalidActiveXStateException : Exception
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.InvalidActiveXStateException" /> class without specifying information about the member that referenced the ActiveX control.</summary>
			// Token: 0x0600032A RID: 810 RVA: 0x00011D70 File Offset: 0x0000FF70
			public InvalidActiveXStateException()
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.InvalidActiveXStateException" /> class and indicates the name of the member that referenced the ActiveX control and the kind of reference it made.</summary>
			/// <param name="name">The name of the member that referenced the ActiveX control while it was in an invalid state. </param>
			/// <param name="kind">One of the <see cref="T:System.Windows.Forms.AxHost.ActiveXInvokeKind" /> values. </param>
			// Token: 0x0600032B RID: 811 RVA: 0x00011D84 File Offset: 0x0000FF84
			public InvalidActiveXStateException(string name, AxHost.ActiveXInvokeKind kind)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>Creates and returns a string representation of the current exception.</summary>
			/// <returns>A string representation of the current exception.</returns>
			// Token: 0x0600032C RID: 812 RVA: 0x00011D98 File Offset: 0x0000FF98
			public override string ToString()
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Encapsulates the persisted state of an ActiveX control.</summary>
		// Token: 0x02000053 RID: 83
		[TypeConverter("System.ComponentModel.TypeConverter, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		public class State : ISerializable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.State" /> class for serializing a state. </summary>
			/// <param name="ms">A <see cref="T:System.IO.Stream" /> in which the state is stored. </param>
			/// <param name="storageType">An <see cref="T:System.Int32" /> indicating the storage type.</param>
			/// <param name="manualUpdate">true for manual updates; otherwise, false.</param>
			/// <param name="licKey">The license key of the control.</param>
			// Token: 0x0600032D RID: 813 RVA: 0x00011DA4 File Offset: 0x0000FFA4
			public State(Stream ms, int storageType, bool manualUpdate, string licKey)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.State" /> class for deserializing a state. </summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
			// Token: 0x0600032E RID: 814 RVA: 0x00011DAC File Offset: 0x0000FFAC
			protected State(SerializationInfo info, StreamingContext context)
			{
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
			/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
			/// <param name="context">The destination for this serialization.</param>
			/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
			// Token: 0x0600032F RID: 815 RVA: 0x00011DB4 File Offset: 0x0000FFB4
			void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
			{
			}
		}

		/// <summary>Specifies a date and time associated with the type library of an ActiveX control hosted by an <see cref="T:System.Windows.Forms.AxHost" /> control.</summary>
		// Token: 0x02000054 RID: 84
		[AttributeUsage(1, Inherited = false)]
		public sealed class TypeLibraryTimeStampAttribute : Attribute
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.TypeLibraryTimeStampAttribute" /> class. </summary>
			/// <param name="timestamp">A <see cref="T:System.DateTime" /> value representing the date and time to associate with the type library.</param>
			// Token: 0x06000330 RID: 816 RVA: 0x00011DB8 File Offset: 0x0000FFB8
			public TypeLibraryTimeStampAttribute(string timestamp)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>The date and time to associate with the type library.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value representing the date and time to associate with the type library.</returns>
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000331 RID: 817 RVA: 0x00011DCC File Offset: 0x0000FFCC
			public DateTime Value
			{
				get
				{
					throw new NotImplementedException("COM/ActiveX support is not implemented");
				}
			}
		}

		/// <summary>Converts <see cref="T:System.Windows.Forms.AxHost.State" /> objects from one data type to another. </summary>
		// Token: 0x02000055 RID: 85
		public class StateConverter : TypeConverter
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> class. </summary>
			// Token: 0x06000332 RID: 818 RVA: 0x00011DD8 File Offset: 0x0000FFD8
			public StateConverter()
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>Returns whether the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can convert an object of the specified type to an <see cref="T:System.Windows.Forms.AxHost.State" />, using the specified context.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can perform the conversion; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which to convert.</param>
			// Token: 0x06000333 RID: 819 RVA: 0x00011DEC File Offset: 0x0000FFEC
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>Returns whether the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can convert an object to the given destination type using the context.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can perform the conversion; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type from which to convert.</param>
			// Token: 0x06000334 RID: 820 RVA: 0x00011DF8 File Offset: 0x0000FFF8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>This member overrides <see cref="M:System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)" />.</summary>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
			// Token: 0x06000335 RID: 821 RVA: 0x00011E04 File Offset: 0x00010004
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}

			/// <summary>This member overrides <see cref="M:System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)" />.</summary>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
			/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="destinationType" /> is null.</exception>
			// Token: 0x06000336 RID: 822 RVA: 0x00011E10 File Offset: 0x00010010
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Represents the method that will display an ActiveX control's About dialog box.</summary>
		// Token: 0x02000635 RID: 1589
		// (Invoke) Token: 0x06005086 RID: 20614
		protected delegate void AboutBoxDelegate();
	}
}
