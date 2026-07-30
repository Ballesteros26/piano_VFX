using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000158 RID: 344
	public class DrawListViewItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
		/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection. </param>
		/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw. </param>
		// Token: 0x0600175C RID: 5980 RVA: 0x0005613C File Offset: 0x0005433C
		public DrawListViewItemEventArgs(Graphics graphics, ListViewItem item, Rectangle bounds, int itemIndex, ListViewItemStates state)
		{
			this.graphics = graphics;
			this.item = item;
			this.bounds = bounds;
			this.itemIndex = itemIndex;
			this.state = state;
		}

		/// <summary>Gets or sets a property indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control will use the default drawing for the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>true if the system draws the item; false if the event handler draws the item. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0005616C File Offset: 0x0005436C
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x00056174 File Offset: 0x00054374
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
			set
			{
				this.drawDefault = value;
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00056180 File Offset: 0x00054380
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00056188 File Offset: 0x00054388
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00056190 File Offset: 0x00054390
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection of the containing <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00056198 File Offset: 0x00054398
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x000561A0 File Offset: 0x000543A0
		public ListViewItemStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current background color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001764 RID: 5988 RVA: 0x000561A8 File Offset: 0x000543A8
		public void DrawBackground()
		{
			this.graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.item.BackColor), this.bounds);
		}

		/// <summary>Draws a focus rectangle for the <see cref="T:System.Windows.Forms.ListViewItem" /> if it has focus.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001765 RID: 5989 RVA: 0x000561E0 File Offset: 0x000543E0
		public void DrawFocusRectangle()
		{
			if ((this.state & ListViewItemStates.Focused) != (ListViewItemStates)0)
			{
				ThemeEngine.Current.CPDrawFocusRectangle(this.graphics, this.bounds, this.item.ListView.ForeColor, this.item.ListView.BackColor);
			}
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current foreground color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001766 RID: 5990 RVA: 0x00056234 File Offset: 0x00054434
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.Left);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current foreground color and formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
		// Token: 0x06001767 RID: 5991 RVA: 0x00056240 File Offset: 0x00054440
		public void DrawText(TextFormatFlags flags)
		{
			TextRenderer.DrawText(this.graphics, this.item.Text, this.item.Font, this.bounds, this.item.ForeColor, flags);
		}

		// Token: 0x04000CE4 RID: 3300
		private Rectangle bounds;

		// Token: 0x04000CE5 RID: 3301
		private bool drawDefault;

		// Token: 0x04000CE6 RID: 3302
		private Graphics graphics;

		// Token: 0x04000CE7 RID: 3303
		private ListViewItem item;

		// Token: 0x04000CE8 RID: 3304
		private int itemIndex;

		// Token: 0x04000CE9 RID: 3305
		private ListViewItemStates state;
	}
}
