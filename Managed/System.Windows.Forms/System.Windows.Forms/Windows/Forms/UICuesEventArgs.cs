using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039D RID: 925
	public class UICuesEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UICuesEventArgs" /> class with the specified <see cref="T:System.Windows.Forms.UICues" />.</summary>
		/// <param name="uicues">A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values. </param>
		// Token: 0x06004387 RID: 17287 RVA: 0x0010AD48 File Offset: 0x00108F48
		public UICuesEventArgs(UICues uicues)
		{
			this.cues = uicues;
		}

		/// <summary>Gets the bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values. The default is <see cref="F:System.Windows.Forms.UICues.Changed" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x0010AD58 File Offset: 0x00108F58
		public UICues Changed
		{
			get
			{
				return this.cues & UICues.Changed;
			}
		}

		/// <summary>Gets a value indicating whether the state of the focus cues has changed.</summary>
		/// <returns>true if the state of the focus cues has changed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x0010AD64 File Offset: 0x00108F64
		public bool ChangeFocus
		{
			get
			{
				return (this.cues & UICues.ChangeFocus) != UICues.None;
			}
		}

		/// <summary>Gets a value indicating whether the state of the keyboard cues has changed.</summary>
		/// <returns>true if the state of the keyboard cues has changed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x0010AD78 File Offset: 0x00108F78
		public bool ChangeKeyboard
		{
			get
			{
				return (this.cues & UICues.ChangeKeyboard) != UICues.None;
			}
		}

		/// <summary>Gets a value indicating whether focus rectangles are shown after the change.</summary>
		/// <returns>true if focus rectangles are shown after the change; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x0010AD8C File Offset: 0x00108F8C
		public bool ShowFocus
		{
			get
			{
				return (this.cues & UICues.ShowFocus) != UICues.None;
			}
		}

		/// <summary>Gets a value indicating whether keyboard cues are underlined after the change.</summary>
		/// <returns>true if keyboard cues are underlined after the change; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x0010ADA0 File Offset: 0x00108FA0
		public bool ShowKeyboard
		{
			get
			{
				return (this.cues & UICues.ShowKeyboard) != UICues.None;
			}
		}

		// Token: 0x04001C6C RID: 7276
		private UICues cues;
	}
}
