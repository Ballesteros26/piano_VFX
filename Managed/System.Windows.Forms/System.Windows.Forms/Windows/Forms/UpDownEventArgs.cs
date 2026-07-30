using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for controls that derive from the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A2 RID: 930
	public class UpDownEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UpDownEventArgs" /> class</summary>
		/// <param name="buttonPushed">The button that was clicked on the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</param>
		// Token: 0x060043F7 RID: 17399 RVA: 0x0010BD70 File Offset: 0x00109F70
		public UpDownEventArgs(int buttonPushed)
		{
			this.button_id = buttonPushed;
		}

		/// <summary>Gets a value that represents which button the user clicked.</summary>
		/// <returns>A value that represents which button the user clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x0010BD80 File Offset: 0x00109F80
		public int ButtonID
		{
			get
			{
				return this.button_id;
			}
		}

		// Token: 0x04001C86 RID: 7302
		private int button_id;
	}
}
