using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EC RID: 492
	public class InvalidateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> class.</summary>
		/// <param name="invalidRect">The <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area. </param>
		// Token: 0x06001EF4 RID: 7924 RVA: 0x00074E4C File Offset: 0x0007304C
		public InvalidateEventArgs(Rectangle invalidRect)
		{
			this.invalidated_rectangle = invalidRect;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area.</summary>
		/// <returns>The invalidated window area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00074E5C File Offset: 0x0007305C
		public Rectangle InvalidRect
		{
			get
			{
				return this.invalidated_rectangle;
			}
		}

		// Token: 0x0400103B RID: 4155
		private Rectangle invalidated_rectangle;
	}
}
