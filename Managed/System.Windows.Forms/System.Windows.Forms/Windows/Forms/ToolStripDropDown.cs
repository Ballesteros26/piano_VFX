using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a control that allows the user to select a single item from a list that is displayed when the user clicks a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> and <see cref="T:System.Windows.Forms.ToolStripDropDown" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000347 RID: 839
	[Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[ComVisible(true)]
	public class ToolStripDropDown : ToolStrip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> class. </summary>
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000F269C File Offset: 0x000F089C
		public ToolStripDropDown()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.auto_close = true;
			this.is_visible = false;
			this.DefaultDropDownDirection = ToolStripDropDownDirection.Right;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.is_toplevel = true;
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000F2700 File Offset: 0x000F0900
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripDropDown()
		{
			ToolStripDropDown.ClosedEvent = new object();
			ToolStripDropDown.ClosingEvent = new object();
			ToolStripDropDown.OpenedEvent = new object();
			ToolStripDropDown.OpeningEvent = new object();
			ToolStripDropDown.ScrollEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000387 RID: 903
		// (add) Token: 0x06003BEA RID: 15338 RVA: 0x000F2740 File Offset: 0x000F0940
		// (remove) Token: 0x06003BEB RID: 15339 RVA: 0x000F274C File Offset: 0x000F094C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000388 RID: 904
		// (add) Token: 0x06003BEC RID: 15340 RVA: 0x000F2758 File Offset: 0x000F0958
		// (remove) Token: 0x06003BED RID: 15341 RVA: 0x000F2764 File Offset: 0x000F0964
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.BindingContext" /> property changes.</summary>
		// Token: 0x14000389 RID: 905
		// (add) Token: 0x06003BEE RID: 15342 RVA: 0x000F2770 File Offset: 0x000F0970
		// (remove) Token: 0x06003BEF RID: 15343 RVA: 0x000F277C File Offset: 0x000F097C
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

		/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
		// Token: 0x1400038A RID: 906
		// (add) Token: 0x06003BF0 RID: 15344 RVA: 0x000F2788 File Offset: 0x000F0988
		// (remove) Token: 0x06003BF1 RID: 15345 RVA: 0x000F2794 File Offset: 0x000F0994
		[Browsable(false)]
		[EditorBrowsable(0)]
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is closed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400038B RID: 907
		// (add) Token: 0x06003BF2 RID: 15346 RVA: 0x000F27A0 File Offset: 0x000F09A0
		// (remove) Token: 0x06003BF3 RID: 15347 RVA: 0x000F27B4 File Offset: 0x000F09B4
		public event ToolStripDropDownClosedEventHandler Closed
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDown.ClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDown.ClosedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is about to close.</summary>
		// Token: 0x1400038C RID: 908
		// (add) Token: 0x06003BF4 RID: 15348 RVA: 0x000F27C8 File Offset: 0x000F09C8
		// (remove) Token: 0x06003BF5 RID: 15349 RVA: 0x000F27DC File Offset: 0x000F09DC
		public event ToolStripDropDownClosingEventHandler Closing
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDown.ClosingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDown.ClosingEvent, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400038D RID: 909
		// (add) Token: 0x06003BF6 RID: 15350 RVA: 0x000F27F0 File Offset: 0x000F09F0
		// (remove) Token: 0x06003BF7 RID: 15351 RVA: 0x000F27FC File Offset: 0x000F09FC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400038E RID: 910
		// (add) Token: 0x06003BF8 RID: 15352 RVA: 0x000F2808 File Offset: 0x000F0A08
		// (remove) Token: 0x06003BF9 RID: 15353 RVA: 0x000F2814 File Offset: 0x000F0A14
		[EditorBrowsable(0)]
		[Browsable(false)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400038F RID: 911
		// (add) Token: 0x06003BFA RID: 15354 RVA: 0x000F2820 File Offset: 0x000F0A20
		// (remove) Token: 0x06003BFB RID: 15355 RVA: 0x000F282C File Offset: 0x000F0A2C
		[EditorBrowsable(0)]
		[Browsable(false)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>Occurs when the focus enters the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		// Token: 0x14000390 RID: 912
		// (add) Token: 0x06003BFC RID: 15356 RVA: 0x000F2838 File Offset: 0x000F0A38
		// (remove) Token: 0x06003BFD RID: 15357 RVA: 0x000F2844 File Offset: 0x000F0A44
		[Browsable(false)]
		[EditorBrowsable(0)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripDropDown.Font" /> property changes.</summary>
		// Token: 0x14000391 RID: 913
		// (add) Token: 0x06003BFE RID: 15358 RVA: 0x000F2850 File Offset: 0x000F0A50
		// (remove) Token: 0x06003BFF RID: 15359 RVA: 0x000F285C File Offset: 0x000F0A5C
		[EditorBrowsable(0)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.ForeColor" /> property changes.</summary>
		// Token: 0x14000392 RID: 914
		// (add) Token: 0x06003C00 RID: 15360 RVA: 0x000F2868 File Offset: 0x000F0A68
		// (remove) Token: 0x06003C01 RID: 15361 RVA: 0x000F2874 File Offset: 0x000F0A74
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000393 RID: 915
		// (add) Token: 0x06003C02 RID: 15362 RVA: 0x000F2880 File Offset: 0x000F0A80
		// (remove) Token: 0x06003C03 RID: 15363 RVA: 0x000F288C File Offset: 0x000F0A8C
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

		/// <summary>Occurs when the user requests help for a control.</summary>
		// Token: 0x14000394 RID: 916
		// (add) Token: 0x06003C04 RID: 15364 RVA: 0x000F2898 File Offset: 0x000F0A98
		// (remove) Token: 0x06003C05 RID: 15365 RVA: 0x000F28A4 File Offset: 0x000F0AA4
		[EditorBrowsable(0)]
		[Browsable(false)]
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

		/// <summary>Occurs when the <see cref="E:System.Windows.Forms.ToolStripDropDown.ImeModeChanged" /> property has changed.</summary>
		// Token: 0x14000395 RID: 917
		// (add) Token: 0x06003C06 RID: 15366 RVA: 0x000F28B0 File Offset: 0x000F0AB0
		// (remove) Token: 0x06003C07 RID: 15367 RVA: 0x000F28BC File Offset: 0x000F0ABC
		[EditorBrowsable(0)]
		[Browsable(false)]
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

		/// <summary>Occurs when a key is pressed and held down while the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> has focus.</summary>
		// Token: 0x14000396 RID: 918
		// (add) Token: 0x06003C08 RID: 15368 RVA: 0x000F28C8 File Offset: 0x000F0AC8
		// (remove) Token: 0x06003C09 RID: 15369 RVA: 0x000F28D4 File Offset: 0x000F0AD4
		[Browsable(false)]
		[EditorBrowsable(0)]
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

		/// <summary>Occurs when a key is pressed while the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> has focus.</summary>
		// Token: 0x14000397 RID: 919
		// (add) Token: 0x06003C0A RID: 15370 RVA: 0x000F28E0 File Offset: 0x000F0AE0
		// (remove) Token: 0x06003C0B RID: 15371 RVA: 0x000F28EC File Offset: 0x000F0AEC
		[EditorBrowsable(0)]
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

		/// <summary>Occurs when a key is released while the control has focus.</summary>
		// Token: 0x14000398 RID: 920
		// (add) Token: 0x06003C0C RID: 15372 RVA: 0x000F28F8 File Offset: 0x000F0AF8
		// (remove) Token: 0x06003C0D RID: 15373 RVA: 0x000F2904 File Offset: 0x000F0B04
		[EditorBrowsable(0)]
		[Browsable(false)]
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

		/// <summary>Occurs when the input focus leaves the control.</summary>
		// Token: 0x14000399 RID: 921
		// (add) Token: 0x06003C0E RID: 15374 RVA: 0x000F2910 File Offset: 0x000F0B10
		// (remove) Token: 0x06003C0F RID: 15375 RVA: 0x000F291C File Offset: 0x000F0B1C
		[EditorBrowsable(0)]
		[Browsable(false)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is opened.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400039A RID: 922
		// (add) Token: 0x06003C10 RID: 15376 RVA: 0x000F2928 File Offset: 0x000F0B28
		// (remove) Token: 0x06003C11 RID: 15377 RVA: 0x000F293C File Offset: 0x000F0B3C
		public event EventHandler Opened
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDown.OpenedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDown.OpenedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is opening.</summary>
		// Token: 0x1400039B RID: 923
		// (add) Token: 0x06003C12 RID: 15378 RVA: 0x000F2950 File Offset: 0x000F0B50
		// (remove) Token: 0x06003C13 RID: 15379 RVA: 0x000F2964 File Offset: 0x000F0B64
		public event CancelEventHandler Opening
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDown.OpeningEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDown.OpeningEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripDropDown.Region" /> property changes.</summary>
		// Token: 0x1400039C RID: 924
		// (add) Token: 0x06003C14 RID: 15380 RVA: 0x000F2978 File Offset: 0x000F0B78
		// (remove) Token: 0x06003C15 RID: 15381 RVA: 0x000F2984 File Offset: 0x000F0B84
		[Browsable(false)]
		[EditorBrowsable(0)]
		public new event EventHandler RegionChanged
		{
			add
			{
				base.RegionChanged += value;
			}
			remove
			{
				base.RegionChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400039D RID: 925
		// (add) Token: 0x06003C16 RID: 15382 RVA: 0x000F2990 File Offset: 0x000F0B90
		// (remove) Token: 0x06003C17 RID: 15383 RVA: 0x000F29A4 File Offset: 0x000F0BA4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDown.ScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDown.ScrollEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> style changes.</summary>
		// Token: 0x1400039E RID: 926
		// (add) Token: 0x06003C18 RID: 15384 RVA: 0x000F29B8 File Offset: 0x000F0BB8
		// (remove) Token: 0x06003C19 RID: 15385 RVA: 0x000F29C4 File Offset: 0x000F0BC4
		[Browsable(false)]
		[EditorBrowsable(0)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400039F RID: 927
		// (add) Token: 0x06003C1A RID: 15386 RVA: 0x000F29D0 File Offset: 0x000F0BD0
		// (remove) Token: 0x06003C1B RID: 15387 RVA: 0x000F29DC File Offset: 0x000F0BDC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003A0 RID: 928
		// (add) Token: 0x06003C1C RID: 15388 RVA: 0x000F29E8 File Offset: 0x000F0BE8
		// (remove) Token: 0x06003C1D RID: 15389 RVA: 0x000F29F4 File Offset: 0x000F0BF4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003A1 RID: 929
		// (add) Token: 0x06003C1E RID: 15390 RVA: 0x000F2A00 File Offset: 0x000F0C00
		// (remove) Token: 0x06003C1F RID: 15391 RVA: 0x000F2A0C File Offset: 0x000F0C0C
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003A2 RID: 930
		// (add) Token: 0x06003C20 RID: 15392 RVA: 0x000F2A18 File Offset: 0x000F0C18
		// (remove) Token: 0x06003C21 RID: 15393 RVA: 0x000F2A24 File Offset: 0x000F0C24
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler Validated
		{
			add
			{
				base.Validated += value;
			}
			remove
			{
				base.Validated -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003A3 RID: 931
		// (add) Token: 0x06003C22 RID: 15394 RVA: 0x000F2A30 File Offset: 0x000F0C30
		// (remove) Token: 0x06003C23 RID: 15395 RVA: 0x000F2A3C File Offset: 0x000F0C3C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event CancelEventHandler Validating
		{
			add
			{
				base.Validating += value;
			}
			remove
			{
				base.Validating -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true to enable item reordering; otherwise, false.</returns>
		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000F2A48 File Offset: 0x000F0C48
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000F2A50 File Offset: 0x000F0C50
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool AllowItemReorder
		{
			get
			{
				return base.AllowItemReorder;
			}
			set
			{
				base.AllowItemReorder = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ToolStripDropDown.Opacity" /> of the form can be adjusted.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.ToolStripDropDown.Opacity" /> of the form can be adjusted; otherwise, false. </returns>
		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000F2A5C File Offset: 0x000F0C5C
		// (set) Token: 0x06003C27 RID: 15399 RVA: 0x000F2A64 File Offset: 0x000F0C64
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool AllowTransparency
		{
			get
			{
				return this.allow_transparency;
			}
			set
			{
				if (value == this.allow_transparency)
				{
					return;
				}
				if ((XplatUI.SupportsTransparency() & TransparencySupport.Set) != TransparencySupport.None)
				{
					this.allow_transparency = value;
					if (base.IsHandleCreated)
					{
						if (value)
						{
							XplatUI.SetWindowTransparency(this.Handle, this.Opacity, Color.Empty);
						}
						else
						{
							base.UpdateStyles();
						}
					}
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000F2AC4 File Offset: 0x000F0CC4
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x000F2ACC File Offset: 0x000F0CCC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control should automatically close when it has lost activation.  </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control automatically closes; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000F2AD8 File Offset: 0x000F0CD8
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x000F2AE0 File Offset: 0x000F0CE0
		[DefaultValue(true)]
		public bool AutoClose
		{
			get
			{
				return this.auto_close;
			}
			set
			{
				this.auto_close = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> automatically adjusts its size when the form is resized. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control automatically resizes; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000F2AEC File Offset: 0x000F0CEC
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x000F2AF4 File Offset: 0x000F0CF4
		[DefaultValue(true)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> can be sent to an overflow menu.</summary>
		/// <returns>true to send <see cref="T:System.Windows.Forms.ToolStripDropDown" /> items to an overflow menu; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000F2B00 File Offset: 0x000F0D00
		// (set) Token: 0x06003C2F RID: 15407 RVA: 0x000F2B08 File Offset: 0x000F0D08
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DefaultValue(false)]
		public new bool CanOverflow
		{
			get
			{
				return this.can_overflow;
			}
			set
			{
				this.can_overflow = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" />.</returns>
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x000F2B14 File Offset: 0x000F0D14
		// (set) Token: 0x06003C31 RID: 15409 RVA: 0x000F2B18 File Offset: 0x000F0D18
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new ContextMenu ContextMenu
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</returns>
		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000F2B1C File Offset: 0x000F0D1C
		// (set) Token: 0x06003C33 RID: 15411 RVA: 0x000F2B20 File Offset: 0x000F0D20
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x000F2B24 File Offset: 0x000F0D24
		// (set) Token: 0x06003C35 RID: 15413 RVA: 0x000F2B2C File Offset: 0x000F0D2C
		public override ToolStripDropDownDirection DefaultDropDownDirection
		{
			get
			{
				return base.DefaultDropDownDirection;
			}
			set
			{
				base.DefaultDropDownDirection = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000F2B38 File Offset: 0x000F0D38
		// (set) Token: 0x06003C37 RID: 15415 RVA: 0x000F2B40 File Offset: 0x000F0D40
		[Browsable(false)]
		[DefaultValue(DockStyle.None)]
		[EditorBrowsable(0)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a three-dimensional shadow effect appears when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed. </summary>
		/// <returns>true to enable the shadow effect; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000F2B4C File Offset: 0x000F0D4C
		// (set) Token: 0x06003C39 RID: 15417 RVA: 0x000F2B54 File Offset: 0x000F0D54
		public bool DropShadowEnabled
		{
			get
			{
				return this.drop_shadow_enabled;
			}
			set
			{
				if (this.drop_shadow_enabled == value)
				{
					return;
				}
				this.drop_shadow_enabled = value;
				base.UpdateStyles();
			}
		}

		/// <summary>Gets or sets the font of the text displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x000F2B70 File Offset: 0x000F0D70
		// (set) Token: 0x06003C3B RID: 15419 RVA: 0x000F2B78 File Offset: 0x000F0D78
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> the values.</returns>
		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x000F2B84 File Offset: 0x000F0D84
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return ToolStripGripDisplayStyle.Vertical;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000F2B88 File Offset: 0x000F0D88
		// (set) Token: 0x06003C3E RID: 15422 RVA: 0x000F2B90 File Offset: 0x000F0D90
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Padding GripMargin
		{
			get
			{
				return Padding.Empty;
			}
			set
			{
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000F2B94 File Offset: 0x000F0D94
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Rectangle GripRectangle
		{
			get
			{
				return Rectangle.Empty;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x000F2B9C File Offset: 0x000F0D9C
		// (set) Token: 0x06003C41 RID: 15425 RVA: 0x000F2BA4 File Offset: 0x000F0DA4
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Windows.Forms.ToolStripDropDown" /> was automatically generated. </summary>
		/// <returns>true if this <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is generated automatically; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x000F2BB0 File Offset: 0x000F0DB0
		[Browsable(false)]
		public bool IsAutoGenerated
		{
			get
			{
				return this is ToolStripOverflow;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" />.</returns>
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000F2BBC File Offset: 0x000F0DBC
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000F2BC4 File Offset: 0x000F0DC4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Determines the opacity of the form.</summary>
		/// <returns>The level of opacity for the form. The default is 1.00.</returns>
		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000F2BD0 File Offset: 0x000F0DD0
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x000F2BD8 File Offset: 0x000F0DD8
		[TypeConverter(typeof(OpacityConverter))]
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return this.opacity;
			}
			set
			{
				if (this.opacity == value)
				{
					return;
				}
				this.opacity = value;
				this.allow_transparency = true;
				if (base.IsHandleCreated)
				{
					base.UpdateStyles();
					XplatUI.SetWindowTransparency(this.Handle, this.opacity, Color.Empty);
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000F2C28 File Offset: 0x000F0E28
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new ToolStripOverflowButton OverflowButton
		{
			get
			{
				return base.OverflowButton;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown" />. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000F2C30 File Offset: 0x000F0E30
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x000F2C38 File Offset: 0x000F0E38
		[DefaultValue(null)]
		[Browsable(false)]
		public ToolStripItem OwnerItem
		{
			get
			{
				return this.owner_item;
			}
			set
			{
				this.owner_item = value;
				if (this.owner_item != null)
				{
					if (this.owner_item.Owner != null && this.owner_item.Owner.RenderMode != ToolStripRenderMode.ManagerRenderMode)
					{
						base.Renderer = this.owner_item.Owner.Renderer;
					}
					this.Font = this.owner_item.Font;
				}
			}
		}

		/// <summary>Gets or sets the window region associated with the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The window <see cref="T:System.Drawing.Region" /> associated with the control.</returns>
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000F2CA4 File Offset: 0x000F0EA4
		// (set) Token: 0x06003C4B RID: 15435 RVA: 0x000F2CAC File Offset: 0x000F0EAC
		[EditorBrowsable(0)]
		[Browsable(false)]
		public new Region Region
		{
			get
			{
				return base.Region;
			}
			set
			{
				base.Region = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x000F2CB8 File Offset: 0x000F0EB8
		// (set) Token: 0x06003C4D RID: 15437 RVA: 0x000F2CC0 File Offset: 0x000F0EC0
		[AmbientValue(RightToLeft.Inherit)]
		[Localizable(true)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true to enable stretching; otherwise, false.</returns>
		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x000F2CCC File Offset: 0x000F0ECC
		// (set) Token: 0x06003C4F RID: 15439 RVA: 0x000F2CD0 File Offset: 0x000F0ED0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool Stretch
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x000F2CD4 File Offset: 0x000F0ED4
		// (set) Token: 0x06003C51 RID: 15441 RVA: 0x000F2CD8 File Offset: 0x000F0ED8
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new int TabIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>Specifies the direction in which to draw the text on the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal" />.</returns>
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x000F2CDC File Offset: 0x000F0EDC
		// (set) Token: 0x06003C53 RID: 15443 RVA: 0x000F2CE4 File Offset: 0x000F0EE4
		[Browsable(false)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is a top-level control.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is a top-level control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x000F2CF0 File Offset: 0x000F0EF0
		// (set) Token: 0x06003C55 RID: 15445 RVA: 0x000F2CF8 File Offset: 0x000F0EF8
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool TopLevel
		{
			get
			{
				return base.GetTopLevel();
			}
			set
			{
				base.SetTopLevel(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is visible or hidden. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is visible; otherwise, false. The default is false.</returns>
		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000F2D04 File Offset: 0x000F0F04
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x000F2D0C File Offset: 0x000F0F0C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[DefaultValue(false)]
		[Localizable(true)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets parameters of a new window.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.CreateParams" /> used when creating a new window.</returns>
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000F2D18 File Offset: 0x000F0F18
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style = -2113929216;
				createParams.ClassStyle |= 131072;
				createParams.ExStyle |= 136;
				if (this.Opacity < 1.0 && this.allow_transparency)
				{
					createParams.ExStyle |= 524288;
				}
				if (this.TopMost)
				{
					createParams.ExStyle |= 8;
				}
				return createParams;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000F2DA8 File Offset: 0x000F0FA8
		protected override DockStyle DefaultDock
		{
			get
			{
				return DockStyle.None;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000F2DAC File Offset: 0x000F0FAC
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(1, 2, 1, 2);
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000F2DB8 File Offset: 0x000F0FB8
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />, in pixels.</returns>
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000F2DBC File Offset: 0x000F0FBC
		protected internal override Size MaxItemSize
		{
			get
			{
				return new Size(Screen.PrimaryScreen.Bounds.Width - 2, Screen.PrimaryScreen.Bounds.Height - 34);
			}
		}

		/// <summary>Gets or sets a value indicating whether the form should be displayed as a topmost form.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000F2DF8 File Offset: 0x000F0FF8
		protected virtual bool TopMost
		{
			get
			{
				return true;
			}
		}

		/// <summary>Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
		// Token: 0x06003C5E RID: 15454 RVA: 0x000F2DFC File Offset: 0x000F0FFC
		public void Close()
		{
			this.Close(ToolStripDropDownCloseReason.CloseCalled);
		}

		/// <summary>Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control for the specified reason.</summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x06003C5F RID: 15455 RVA: 0x000F2E08 File Offset: 0x000F1008
		public void Close(ToolStripDropDownCloseReason reason)
		{
			if (!this.Visible)
			{
				return;
			}
			ToolStripDropDownClosingEventArgs toolStripDropDownClosingEventArgs = new ToolStripDropDownClosingEventArgs(reason);
			this.OnClosing(toolStripDropDownClosingEventArgs);
			if (toolStripDropDownClosingEventArgs.Cancel)
			{
				return;
			}
			if (!this.auto_close && reason != ToolStripDropDownCloseReason.CloseCalled)
			{
				return;
			}
			ToolStripManager.AppClicked -= new EventHandler(this.ToolStripMenuTracker_AppClicked);
			ToolStripManager.AppFocusChange -= new EventHandler(this.ToolStripMenuTracker_AppFocusChange);
			base.Hide();
			if (this.owner_item != null)
			{
				this.owner_item.Invalidate();
			}
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				toolStripItem.Dismiss(reason);
			}
			this.OnClosed(new ToolStripDropDownClosedEventArgs(reason));
		}

		/// <summary>Displays the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control in its default position.</summary>
		// Token: 0x06003C60 RID: 15456 RVA: 0x000F2EFC File Offset: 0x000F10FC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new void Show()
		{
			this.Show(this.Location, this.DefaultDropDownDirection);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified screen location.</summary>
		/// <param name="screenLocation">The horizontal and vertical location of the screen's upper-left corner, in pixels.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003C61 RID: 15457 RVA: 0x000F2F10 File Offset: 0x000F1110
		public void Show(Point screenLocation)
		{
			this.Show(screenLocation, this.DefaultDropDownDirection);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control location.</summary>
		/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
		/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
		/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003C62 RID: 15458 RVA: 0x000F2F20 File Offset: 0x000F1120
		public void Show(Control control, Point position)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			XplatUI.SetOwner(this.Handle, control.Handle);
			this.Show(control.PointToScreen(position), this.DefaultDropDownDirection);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified screen coordinates.</summary>
		/// <param name="x">The horizontal screen coordinate, in pixels.</param>
		/// <param name="y">The vertical screen coordinate, in pixels.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003C63 RID: 15459 RVA: 0x000F2F64 File Offset: 0x000F1164
		public void Show(int x, int y)
		{
			this.Show(new Point(x, y), this.DefaultDropDownDirection);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control location and with the specified direction relative to the parent control.</summary>
		/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
		/// <param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</param>
		// Token: 0x06003C64 RID: 15460 RVA: 0x000F2F7C File Offset: 0x000F117C
		public void Show(Point position, ToolStripDropDownDirection direction)
		{
			base.PerformLayout();
			Point point = position;
			Point point2;
			point2..ctor(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
			if (this is ContextMenuStrip)
			{
				switch (direction)
				{
				case ToolStripDropDownDirection.AboveLeft:
					if (point.X - base.Width < 0)
					{
						direction = ToolStripDropDownDirection.AboveRight;
					}
					break;
				case ToolStripDropDownDirection.AboveRight:
					if (point.X + base.Width > point2.X)
					{
						direction = ToolStripDropDownDirection.AboveLeft;
					}
					break;
				case ToolStripDropDownDirection.BelowLeft:
					if (point.X - base.Width < 0)
					{
						direction = ToolStripDropDownDirection.BelowRight;
					}
					break;
				case ToolStripDropDownDirection.BelowRight:
				case ToolStripDropDownDirection.Default:
					if (point.X + base.Width > point2.X)
					{
						direction = ToolStripDropDownDirection.BelowLeft;
					}
					break;
				case ToolStripDropDownDirection.Left:
					if (point.X - base.Width < 0)
					{
						direction = ToolStripDropDownDirection.Right;
					}
					break;
				case ToolStripDropDownDirection.Right:
					if (point.X + base.Width > point2.X)
					{
						direction = ToolStripDropDownDirection.Left;
					}
					break;
				}
				switch (direction)
				{
				case ToolStripDropDownDirection.AboveLeft:
					if (point.Y - base.Height < 0)
					{
						direction = ToolStripDropDownDirection.BelowLeft;
					}
					break;
				case ToolStripDropDownDirection.AboveRight:
					if (point.Y - base.Height < 0)
					{
						direction = ToolStripDropDownDirection.BelowRight;
					}
					break;
				case ToolStripDropDownDirection.BelowLeft:
					if (point.Y + base.Height > point2.Y)
					{
						direction = ToolStripDropDownDirection.AboveLeft;
					}
					break;
				case ToolStripDropDownDirection.BelowRight:
				case ToolStripDropDownDirection.Default:
					if (point.Y + base.Height > point2.Y)
					{
						direction = ToolStripDropDownDirection.AboveRight;
					}
					break;
				case ToolStripDropDownDirection.Left:
					if (point.Y + base.Height > point2.Y)
					{
						direction = ToolStripDropDownDirection.AboveLeft;
					}
					break;
				case ToolStripDropDownDirection.Right:
					if (point.Y + base.Height > point2.Y)
					{
						direction = ToolStripDropDownDirection.AboveRight;
					}
					break;
				}
			}
			switch (direction)
			{
			case ToolStripDropDownDirection.AboveLeft:
				point.Y -= base.Height;
				point.X -= base.Width;
				break;
			case ToolStripDropDownDirection.AboveRight:
				point.Y -= base.Height;
				break;
			case ToolStripDropDownDirection.BelowLeft:
				point.X -= base.Width;
				break;
			case ToolStripDropDownDirection.Left:
				point.X -= base.Width;
				break;
			}
			if (point.X + base.Width > point2.X)
			{
				point.X = point2.X - base.Width;
			}
			if (point.X < 0)
			{
				point.X = 0;
			}
			if (this.Location != point)
			{
				this.Location = point;
			}
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnOpening(cancelEventArgs);
			if (cancelEventArgs.Cancel)
			{
				return;
			}
			ToolStripManager.AppClicked += new EventHandler(this.ToolStripMenuTracker_AppClicked);
			ToolStripManager.AppFocusChange += new EventHandler(this.ToolStripMenuTracker_AppFocusChange);
			base.Show();
			ToolStripManager.SetActiveToolStrip(this, ToolStripManager.ActivatedByKeyboard);
			this.OnOpened(EventArgs.Empty);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control's horizontal and vertical screen coordinates.</summary>
		/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
		/// <param name="x">The horizontal screen coordinate of the control, in pixels.</param>
		/// <param name="y">The vertical screen coordinate of the control, in pixels.</param>
		/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003C65 RID: 15461 RVA: 0x000F32F0 File Offset: 0x000F14F0
		public void Show(Control control, int x, int y)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.Show(control, new Point(x, y));
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control at the specified location and with the specified direction relative to the parent control.</summary>
		/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
		/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
		/// <param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		// Token: 0x06003C66 RID: 15462 RVA: 0x000F3314 File Offset: 0x000F1514
		public void Show(Control control, Point position, ToolStripDropDownDirection direction)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			XplatUI.SetOwner(this.Handle, control.Handle);
			this.Show(control.PointToScreen(position), direction);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripDropDown" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06003C67 RID: 15463 RVA: 0x000F3354 File Offset: 0x000F1554
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripDropDown.ToolStripDropDownAccessibleObject(this);
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x000F335C File Offset: 0x000F155C
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Applies various layout options to the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.LayoutSettings" /> for this <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The possibilities are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow" />.</param>
		// Token: 0x06003C69 RID: 15465 RVA: 0x000F3364 File Offset: 0x000F1564
		protected override LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle style)
		{
			return base.CreateLayoutSettings(style);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003C6A RID: 15466 RVA: 0x000F3370 File Offset: 0x000F1570
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C6B RID: 15467 RVA: 0x000F337C File Offset: 0x000F157C
		protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
		{
			ToolStripDropDownClosedEventHandler toolStripDropDownClosedEventHandler = (ToolStripDropDownClosedEventHandler)base.Events[ToolStripDropDown.ClosedEvent];
			if (toolStripDropDownClosedEventHandler != null)
			{
				toolStripDropDownClosedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C6C RID: 15468 RVA: 0x000F33B0 File Offset: 0x000F15B0
		protected virtual void OnClosing(ToolStripDropDownClosingEventArgs e)
		{
			ToolStripDropDownClosingEventHandler toolStripDropDownClosingEventHandler = (ToolStripDropDownClosingEventHandler)base.Events[ToolStripDropDown.ClosingEvent];
			if (toolStripDropDownClosingEventHandler != null)
			{
				toolStripDropDownClosingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003C6D RID: 15469 RVA: 0x000F33E4 File Offset: 0x000F15E4
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Application.MWFThread.Current.Context != null && Application.MWFThread.Current.Context.MainForm != null)
			{
				XplatUI.SetOwner(this.Handle, Application.MWFThread.Current.Context.MainForm.Handle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C6E RID: 15470 RVA: 0x000F343C File Offset: 0x000F163C
		protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			base.OnItemClicked(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C6F RID: 15471 RVA: 0x000F3448 File Offset: 0x000F1648
		protected override void OnLayout(LayoutEventArgs e)
		{
			int num = 0;
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Available)
				{
					toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
					num = Math.Max(num, toolStripItem.GetPreferredSize(Size.Empty).Width + toolStripItem.Margin.Horizontal);
				}
			}
			num += base.Padding.Horizontal;
			int left = base.Padding.Left;
			int num2 = base.Padding.Top;
			foreach (object obj2 in this.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				if (toolStripItem2.Available)
				{
					num2 += toolStripItem2.Margin.Top;
					Size preferredSize = toolStripItem2.GetPreferredSize(Size.Empty);
					int num3;
					if (preferredSize.Height > 22)
					{
						num3 = preferredSize.Height;
					}
					else if (toolStripItem2 is ToolStripSeparator)
					{
						num3 = 7;
					}
					else
					{
						num3 = 22;
					}
					toolStripItem2.SetBounds(new Rectangle(left, num2, preferredSize.Width, num3));
					num2 += num3 + toolStripItem2.Margin.Bottom;
				}
			}
			base.Size = new Size(num, num2 + base.Padding.Bottom);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C70 RID: 15472 RVA: 0x000F365C File Offset: 0x000F185C
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003C71 RID: 15473 RVA: 0x000F3668 File Offset: 0x000F1868
		protected virtual void OnOpened(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDown.OpenedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C72 RID: 15474 RVA: 0x000F369C File Offset: 0x000F189C
		protected virtual void OnOpening(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[ToolStripDropDown.OpeningEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003C73 RID: 15475 RVA: 0x000F36D0 File Offset: 0x000F18D0
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (base.Parent is ToolStrip)
			{
				base.Renderer = (base.Parent as ToolStrip).Renderer;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003C74 RID: 15476 RVA: 0x000F370C File Offset: 0x000F190C
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (this.owner_item != null && this.owner_item is ToolStripDropDownItem)
			{
				ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)this.owner_item;
				if (this.Visible)
				{
					toolStripDropDownItem.OnDropDownOpened(EventArgs.Empty);
				}
				else
				{
					toolStripDropDownItem.OnDropDownClosed(EventArgs.Empty);
				}
			}
		}

		/// <summary>Processes a dialog box character.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x06003C75 RID: 15477 RVA: 0x000F3770 File Offset: 0x000F1970
		[EditorBrowsable(2)]
		protected override bool ProcessDialogChar(char charCode)
		{
			return base.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x06003C76 RID: 15478 RVA: 0x000F377C File Offset: 0x000F197C
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return keyData == (Keys.LButton | Keys.Back | Keys.Control) || keyData == (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control) || base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x06003C77 RID: 15479 RVA: 0x000F37B0 File Offset: 0x000F19B0
		protected override bool ProcessMnemonic(char charCode)
		{
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000F37BC File Offset: 0x000F19BC
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06003C79 RID: 15481 RVA: 0x000F37C8 File Offset: 0x000F19C8
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000F37D4 File Offset: 0x000F19D4
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Adjusts the size of the owner <see cref="T:System.Windows.Forms.ToolStrip" /> to accommodate the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed, or clears and resets active <see cref="T:System.Windows.Forms.ToolStripDropDown" /> child controls of the <see cref="T:System.Windows.Forms.ToolStrip" /> if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not currently displayed.</summary>
		/// <param name="visible">true if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed; otherwise, false. </param>
		// Token: 0x06003C7B RID: 15483 RVA: 0x000F37E4 File Offset: 0x000F19E4
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003C7C RID: 15484 RVA: 0x000F37F0 File Offset: 0x000F19F0
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 33)
			{
				m.Result = (IntPtr)3;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x000F3820 File Offset: 0x000F1A20
		internal override void Dismiss(ToolStripDropDownCloseReason reason)
		{
			this.Close(reason);
			base.Dismiss(reason);
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x000F3830 File Offset: 0x000F1A30
		internal override ToolStrip GetTopLevelToolStrip()
		{
			if (this.OwnerItem == null)
			{
				return this;
			}
			return this.OwnerItem.GetTopLevelToolStrip();
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x000F384C File Offset: 0x000F1A4C
		internal override bool ProcessArrowKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Left:
				goto IL_0070;
			case Keys.Up:
				goto IL_004B;
			case Keys.Right:
				this.GetTopLevelToolStrip().SelectNextToolStripItem(this.TopLevelOwnerItem, true);
				return true;
			case Keys.Down:
				break;
			default:
				if (keyData != Keys.Tab)
				{
					if (keyData == Keys.Escape)
					{
						goto IL_0070;
					}
					if (keyData != (Keys.LButton | Keys.Back | Keys.Shift))
					{
						return false;
					}
					goto IL_004B;
				}
				break;
			}
			this.SelectNextToolStripItem(base.GetCurrentlySelectedItem(), true);
			return true;
			IL_004B:
			this.SelectNextToolStripItem(base.GetCurrentlySelectedItem(), false);
			return true;
			IL_0070:
			this.Dismiss(ToolStripDropDownCloseReason.Keyboard);
			if (this.OwnerItem == null)
			{
				return true;
			}
			ToolStrip parent = this.OwnerItem.Parent;
			ToolStripManager.SetActiveToolStrip(parent, true);
			if (parent is MenuStrip && keyData == Keys.Left)
			{
				parent.SelectNextToolStripItem(this.TopLevelOwnerItem, false);
				this.TopLevelOwnerItem.Invalidate();
			}
			else if (parent is MenuStrip && keyData == Keys.Escape)
			{
				(parent as MenuStrip).MenuDroppedDown = false;
				this.TopLevelOwnerItem.Select();
			}
			return true;
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000F3950 File Offset: 0x000F1B50
		internal override ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
		{
			ToolStripItem nextItem = this.GetNextItem(start, (!forward) ? ArrowDirection.Up : ArrowDirection.Down);
			if (nextItem != null)
			{
				base.ChangeSelection(nextItem);
			}
			return nextItem;
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x000F3984 File Offset: 0x000F1B84
		private void ToolStripMenuTracker_AppFocusChange(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x000F3994 File Offset: 0x000F1B94
		private void ToolStripMenuTracker_AppClicked(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppClicked);
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x000F39A4 File Offset: 0x000F1BA4
		internal override bool ActivateOnShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000F39A8 File Offset: 0x000F1BA8
		internal ToolStripItem TopLevelOwnerItem
		{
			get
			{
				ToolStrip owner;
				for (ToolStripItem toolStripItem = this.OwnerItem; toolStripItem != null; toolStripItem = (owner as ToolStripDropDown).OwnerItem)
				{
					owner = toolStripItem.Owner;
					if (owner == null || !(owner is ToolStripDropDown))
					{
						return toolStripItem;
					}
				}
				return null;
			}
		}

		// Token: 0x04001A64 RID: 6756
		private bool allow_transparency;

		// Token: 0x04001A65 RID: 6757
		private bool auto_close;

		// Token: 0x04001A66 RID: 6758
		private bool can_overflow;

		// Token: 0x04001A67 RID: 6759
		private bool drop_shadow_enabled = true;

		// Token: 0x04001A68 RID: 6760
		private double opacity = 1.0;

		// Token: 0x04001A69 RID: 6761
		private ToolStripItem owner_item;

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control to accessibility client applications.</summary>
		// Token: 0x02000348 RID: 840
		[ComVisible(true)]
		public class ToolStripDropDownAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that owns the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" />.</param>
			// Token: 0x06003C85 RID: 15493 RVA: 0x000F39F8 File Offset: 0x000F1BF8
			public ToolStripDropDownAccessibleObject(ToolStripDropDown owner)
				: base(owner)
			{
			}

			/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" />.</summary>
			/// <returns>The string representing the name.</returns>
			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x06003C86 RID: 15494 RVA: 0x000F3A04 File Offset: 0x000F1C04
			// (set) Token: 0x06003C87 RID: 15495 RVA: 0x000F3A0C File Offset: 0x000F1C0C
			public override string Name
			{
				get
				{
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Table" /> value.</returns>
			// Token: 0x17000FCE RID: 4046
			// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000F3A18 File Offset: 0x000F1C18
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.MenuPopup;
				}
			}
		}
	}
}
