using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Popup" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200028A RID: 650
	public class PopupEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.PopupEventArgs" /> class.</summary>
		/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
		/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
		/// <param name="isBalloon">true to indicate that the associated ToolTip window has a balloon-style appearance; otherwise, false to indicate that the ToolTip window has a standard rectangular appearance.</param>
		/// <param name="size">The <see cref="T:System.Drawing.Size" /> of the ToolTip.</param>
		// Token: 0x06002A7B RID: 10875 RVA: 0x000A3E00 File Offset: 0x000A2000
		public PopupEventArgs(IWin32Window associatedWindow, Control associatedControl, bool isBalloon, Size size)
		{
			this.associated_window = associatedWindow;
			this.associated_control = associatedControl;
			this.is_balloon = isBalloon;
			this.tool_tip_size = size;
		}

		/// <summary>Gets the control for which the <see cref="T:System.Windows.Forms.ToolTip" /> is being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is associated with the <see cref="T:System.Windows.Forms.ToolTip" />, or null if the ToolTip is not associated with a control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000A3E28 File Offset: 0x000A2028
		public Control AssociatedControl
		{
			get
			{
				return this.associated_control;
			}
		}

		/// <summary>Gets the window to which this <see cref="T:System.Windows.Forms.ToolTip" /> is bound.</summary>
		/// <returns>The window which owns the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x000A3E30 File Offset: 0x000A2030
		public IWin32Window AssociatedWindow
		{
			get
			{
				return this.associated_window;
			}
		}

		/// <summary>Gets a value indicating whether the ToolTip is displayed as a standard rectangular or a balloon window.</summary>
		/// <returns>true if the ToolTip is displayed in a balloon window; otherwise, false if a standard rectangular window is used.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000A3E38 File Offset: 0x000A2038
		public bool IsBalloon
		{
			get
			{
				return this.is_balloon;
			}
		}

		/// <summary>Gets or sets the size of the ToolTip.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolTip" /> window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000A3E40 File Offset: 0x000A2040
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x000A3E48 File Offset: 0x000A2048
		public Size ToolTipSize
		{
			get
			{
				return this.tool_tip_size;
			}
			set
			{
				this.tool_tip_size = value;
			}
		}

		// Token: 0x04001501 RID: 5377
		private Control associated_control;

		// Token: 0x04001502 RID: 5378
		private IWin32Window associated_window;

		// Token: 0x04001503 RID: 5379
		private bool is_balloon;

		// Token: 0x04001504 RID: 5380
		private Size tool_tip_size;
	}
}
