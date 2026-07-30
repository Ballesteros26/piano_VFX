using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the events defined on <see cref="T:System.Windows.Forms.HtmlDocument" /> and <see cref="T:System.Windows.Forms.HtmlElement" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BA RID: 442
	public sealed class HtmlElementEventArgs : EventArgs
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x0006EC04 File Offset: 0x0006CE04
		internal HtmlElementEventArgs()
		{
			this.alt_key_pressed = false;
			this.bubble_event = false;
			this.client_mouse_position = Point.Empty;
			this.ctrl_key_pressed = false;
			this.event_type = null;
			this.from_element = null;
			this.key_pressed_code = 0;
			this.mouse_buttons_pressed = MouseButtons.None;
			this.mouse_position = Point.Empty;
			this.offset_mouse_position = Point.Empty;
			this.return_value = false;
			this.shift_key_pressed = false;
			this.to_element = null;
		}

		/// <summary>Indicates whether the ALT key was pressed when this event occurred.</summary>
		/// <returns>true is the ALT key was pressed; otherwise, false.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0006EC80 File Offset: 0x0006CE80
		public bool AltKeyPressed
		{
			get
			{
				return this.alt_key_pressed;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current event bubbles up through the element hierarchy of the HTML Document Object Model.</summary>
		/// <returns>true if the event bubbles; false if it does not. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0006EC88 File Offset: 0x0006CE88
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x0006EC90 File Offset: 0x0006CE90
		public bool BubbleEvent
		{
			get
			{
				return this.bubble_event;
			}
			set
			{
				this.bubble_event = value;
			}
		}

		/// <summary>Gets or sets the position of the mouse cursor in the document's client area. </summary>
		/// <returns>The current position of the mouse cursor. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0006EC9C File Offset: 0x0006CE9C
		public Point ClientMousePosition
		{
			get
			{
				return this.client_mouse_position;
			}
		}

		/// <summary>Indicates whether the CTRL key was pressed when this event occurred.</summary>
		/// <returns>true if the CTRL key was pressed; otherwise, false.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0006ECA4 File Offset: 0x0006CEA4
		public bool CtrlKeyPressed
		{
			get
			{
				return this.ctrl_key_pressed;
			}
		}

		/// <summary>Gets the name of the event that was raised.</summary>
		/// <returns>The name of the event. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0006ECAC File Offset: 0x0006CEAC
		public string EventType
		{
			get
			{
				return this.event_type;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> the mouse pointer is moving away from.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> the mouse pointer is moving away from.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0006ECB4 File Offset: 0x0006CEB4
		[Browsable(false)]
		[EditorBrowsable(2)]
		public HtmlElement FromElement
		{
			get
			{
				return this.from_element;
			}
		}

		/// <summary>Gets the ASCII value of the keyboard character typed in a <see cref="E:System.Windows.Forms.HtmlElement.KeyPress" />, <see cref="E:System.Windows.Forms.HtmlElement.KeyDown" />, or <see cref="E:System.Windows.Forms.HtmlElement.KeyUp" /> event.</summary>
		/// <returns>The ASCII value of the composed keyboard entry.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0006ECBC File Offset: 0x0006CEBC
		public int KeyPressedCode
		{
			get
			{
				return this.key_pressed_code;
			}
		}

		/// <summary>Gets the mouse button that was clicked during a <see cref="E:System.Windows.Forms.HtmlElement.MouseDown" /> or <see cref="E:System.Windows.Forms.HtmlElement.MouseUp" /> event.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0006ECC4 File Offset: 0x0006CEC4
		public MouseButtons MouseButtonsPressed
		{
			get
			{
				return this.mouse_buttons_pressed;
			}
		}

		/// <summary>Gets or sets the position of the mouse cursor relative to a relatively positioned parent element.</summary>
		/// <returns>The position of the mouse cursor relative to the upper-left corner of the parent of the element that raised the event, if the parent element is relatively positioned. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0006ECCC File Offset: 0x0006CECC
		public Point MousePosition
		{
			get
			{
				return this.mouse_position;
			}
		}

		/// <summary>Gets or sets the position of the mouse cursor relative to the element that raises the event.</summary>
		/// <returns>The mouse position relative to the element that raises the event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0006ECD4 File Offset: 0x0006CED4
		public Point OffsetMousePosition
		{
			get
			{
				return this.offset_mouse_position;
			}
		}

		/// <summary>Gets or sets the return value of the handled event. </summary>
		/// <returns>true if the event has been handled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0006ECDC File Offset: 0x0006CEDC
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0006ECE4 File Offset: 0x0006CEE4
		public bool ReturnValue
		{
			get
			{
				return this.return_value;
			}
			set
			{
				this.return_value = value;
			}
		}

		/// <summary>Indicates whether the SHIFT key was pressed when this event occurred.</summary>
		/// <returns>true if the SHIFT key was pressed; otherwise, false.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0006ECF0 File Offset: 0x0006CEF0
		public bool ShiftKeyPressed
		{
			get
			{
				return this.shift_key_pressed;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> toward which the user is moving the mouse pointer.</summary>
		/// <returns>The element toward which the mouse pointer is moving. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0006ECF8 File Offset: 0x0006CEF8
		[EditorBrowsable(2)]
		[Browsable(false)]
		public HtmlElement ToElement
		{
			get
			{
				return this.to_element;
			}
		}

		// Token: 0x04000F72 RID: 3954
		private bool alt_key_pressed;

		// Token: 0x04000F73 RID: 3955
		private bool bubble_event;

		// Token: 0x04000F74 RID: 3956
		private Point client_mouse_position;

		// Token: 0x04000F75 RID: 3957
		private bool ctrl_key_pressed;

		// Token: 0x04000F76 RID: 3958
		private string event_type;

		// Token: 0x04000F77 RID: 3959
		private HtmlElement from_element;

		// Token: 0x04000F78 RID: 3960
		private int key_pressed_code;

		// Token: 0x04000F79 RID: 3961
		private MouseButtons mouse_buttons_pressed;

		// Token: 0x04000F7A RID: 3962
		private Point mouse_position;

		// Token: 0x04000F7B RID: 3963
		private Point offset_mouse_position;

		// Token: 0x04000F7C RID: 3964
		private bool return_value;

		// Token: 0x04000F7D RID: 3965
		private bool shift_key_pressed;

		// Token: 0x04000F7E RID: 3966
		private HtmlElement to_element;
	}
}
