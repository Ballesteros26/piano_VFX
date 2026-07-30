using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
	// Token: 0x0200028F RID: 655
	public class PreviewKeyDownEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> class with the specified key. </summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x06002A87 RID: 10887 RVA: 0x000A3EAC File Offset: 0x000A20AC
		public PreviewKeyDownEventArgs(Keys keyData)
		{
			this.key_data = keyData;
		}

		/// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
		/// <returns>true if the ALT key was pressed; otherwise, false.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000A3EBC File Offset: 0x000A20BC
		public bool Alt
		{
			get
			{
				return (this.key_data & Keys.Alt) != Keys.None;
			}
		}

		/// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
		/// <returns>true if the CTRL key was pressed; otherwise, false.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x000A3ED0 File Offset: 0x000A20D0
		public bool Control
		{
			get
			{
				return (this.key_data & Keys.Control) != Keys.None;
			}
		}

		/// <summary>Gets or sets a value indicating whether a key is a regular input key.</summary>
		/// <returns>true if the key is a regular input key; otherwise, false.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000A3EE4 File Offset: 0x000A20E4
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x000A3EEC File Offset: 0x000A20EC
		public bool IsInputKey
		{
			get
			{
				return this.is_input_key;
			}
			set
			{
				this.is_input_key = value;
			}
		}

		/// <summary>Gets the keyboard code for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x000A3EF8 File Offset: 0x000A20F8
		public Keys KeyCode
		{
			get
			{
				return this.key_data & Keys.KeyCode;
			}
		}

		/// <summary>Gets the key data for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x000A3F08 File Offset: 0x000A2108
		public Keys KeyData
		{
			get
			{
				return this.key_data;
			}
		}

		/// <summary>Gets the keyboard value for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the keyboard value.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000A3F10 File Offset: 0x000A2110
		public int KeyValue
		{
			get
			{
				return Convert.ToInt32(this.key_data);
			}
		}

		/// <summary>Gets the modifier flags for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x000A3F24 File Offset: 0x000A2124
		public Keys Modifiers
		{
			get
			{
				return this.key_data & Keys.Modifiers;
			}
		}

		/// <summary>Gets a value indicating whether the SHIFT key was pressed.</summary>
		/// <returns>true if the SHIFT key was pressed; otherwise, false.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x000A3F34 File Offset: 0x000A2134
		public bool Shift
		{
			get
			{
				return (this.key_data & Keys.Shift) != Keys.None;
			}
		}

		// Token: 0x04001515 RID: 5397
		private Keys key_data;

		// Token: 0x04001516 RID: 5398
		private bool is_input_key;
	}
}
