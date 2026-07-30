using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F4 RID: 500
	[ComVisible(true)]
	public class KeyEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyEventArgs" /> class.</summary>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> representing the key that was pressed, combined with any modifier flags that indicate which CTRL, SHIFT, and ALT keys were pressed at the same time. Possible values are obtained be applying the bitwise OR (|) operator to constants from the <see cref="T:System.Windows.Forms.Keys" /> enumeration. </param>
		// Token: 0x06001F06 RID: 7942 RVA: 0x00074F10 File Offset: 0x00073110
		public KeyEventArgs(Keys keyData)
		{
			this.key_data = keyData | XplatUI.State.ModifierKeys;
			this.event_handled = false;
		}

		/// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
		/// <returns>true if the ALT key was pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00074F2C File Offset: 0x0007312C
		public virtual bool Alt
		{
			get
			{
				return (this.key_data & Keys.Alt) != Keys.None;
			}
		}

		/// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
		/// <returns>true if the CTRL key was pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00074F44 File Offset: 0x00073144
		public bool Control
		{
			get
			{
				return (this.key_data & Keys.Control) != Keys.None;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>true to bypass the control's default handling; otherwise, false to also pass the event along to the default control handler.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00074F5C File Offset: 0x0007315C
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x00074F64 File Offset: 0x00073164
		public bool Handled
		{
			get
			{
				return this.event_handled;
			}
			set
			{
				this.event_handled = value;
			}
		}

		/// <summary>Gets the keyboard code for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> value that is the key code for the event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00074F70 File Offset: 0x00073170
		public Keys KeyCode
		{
			get
			{
				return this.key_data & Keys.KeyCode;
			}
		}

		/// <summary>Gets the key data for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> representing the key code for the key that was pressed, combined with modifier flags that indicate which combination of CTRL, SHIFT, and ALT keys was pressed at the same time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00074F80 File Offset: 0x00073180
		public Keys KeyData
		{
			get
			{
				return this.key_data;
			}
		}

		/// <summary>Gets the keyboard value for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>The integer representation of the <see cref="P:System.Windows.Forms.KeyEventArgs.KeyCode" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00074F88 File Offset: 0x00073188
		public int KeyValue
		{
			get
			{
				return Convert.ToInt32(this.key_data);
			}
		}

		/// <summary>Gets the modifier flags for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event. The flags indicate which combination of CTRL, SHIFT, and ALT keys was pressed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> value representing one or more modifier flags.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00074F9C File Offset: 0x0007319C
		public Keys Modifiers
		{
			get
			{
				return this.key_data & Keys.Modifiers;
			}
		}

		/// <summary>Gets a value indicating whether the SHIFT key was pressed.</summary>
		/// <returns>true if the SHIFT key was pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00074FAC File Offset: 0x000731AC
		public virtual bool Shift
		{
			get
			{
				return (this.key_data & Keys.Shift) != Keys.None;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key event should be passed on to the underlying control.</summary>
		/// <returns>true if the key event should not be sent to the control; otherwise, false.</returns>
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00074FC4 File Offset: 0x000731C4
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00074FCC File Offset: 0x000731CC
		public bool SuppressKeyPress
		{
			get
			{
				return this.supress_key_press;
			}
			set
			{
				this.supress_key_press = value;
				this.event_handled = value;
			}
		}

		// Token: 0x0400104C RID: 4172
		private Keys key_data;

		// Token: 0x0400104D RID: 4173
		private bool event_handled;

		// Token: 0x0400104E RID: 4174
		private bool supress_key_press;
	}
}
