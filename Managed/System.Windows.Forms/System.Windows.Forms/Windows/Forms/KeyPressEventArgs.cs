using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F5 RID: 501
	[ComVisible(true)]
	public class KeyPressEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> class.</summary>
		/// <param name="keyChar">The ASCII character corresponding to the key the user pressed. </param>
		// Token: 0x06001F12 RID: 7954 RVA: 0x00074FDC File Offset: 0x000731DC
		public KeyPressEventArgs(char keyChar)
		{
			this.key_char = keyChar;
			this.event_handled = false;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event was handled.</summary>
		/// <returns>true if the event is handled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x00074FF4 File Offset: 0x000731F4
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x00074FFC File Offset: 0x000731FC
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

		/// <summary>Gets or sets the character corresponding to the key pressed.</summary>
		/// <returns>The ASCII character that is composed. For example, if the user presses SHIFT + K, this property returns an uppercase K.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00075008 File Offset: 0x00073208
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x00075010 File Offset: 0x00073210
		public char KeyChar
		{
			get
			{
				return this.key_char;
			}
			set
			{
				this.key_char = value;
			}
		}

		// Token: 0x0400104F RID: 4175
		private char key_char;

		// Token: 0x04001050 RID: 4176
		private bool event_handled;
	}
}
