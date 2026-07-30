using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000286 RID: 646
	public class PaintEventArgs : EventArgs, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PaintEventArgs" /> class with the specified graphics and clipping rectangle.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item. </param>
		/// <param name="clipRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint. </param>
		// Token: 0x060029F7 RID: 10743 RVA: 0x000A2F74 File Offset: 0x000A1174
		public PaintEventArgs(Graphics graphics, Rectangle clipRect)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.graphics = graphics;
			this.clip_rectangle = clipRect;
		}

		/// <summary>Gets the rectangle in which to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> in which to paint.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000A2F9C File Offset: 0x000A119C
		public Rectangle ClipRectangle
		{
			get
			{
				return this.clip_rectangle;
			}
		}

		/// <summary>Gets the graphics used to paint.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> object used to paint. The <see cref="T:System.Drawing.Graphics" /> object provides methods for drawing objects on the display device.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000A2FA4 File Offset: 0x000A11A4
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060029FA RID: 10746 RVA: 0x000A2FAC File Offset: 0x000A11AC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000A2FBC File Offset: 0x000A11BC
		internal Graphics SetGraphics(Graphics g)
		{
			Graphics graphics = this.graphics;
			this.graphics = g;
			return graphics;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000A2FD8 File Offset: 0x000A11D8
		internal void SetClip(Rectangle clip)
		{
			this.clip_rectangle = clip;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000A2FE4 File Offset: 0x000A11E4
		~PaintEventArgs()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060029FE RID: 10750 RVA: 0x000A3020 File Offset: 0x000A1220
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x040014EA RID: 5354
		private Graphics graphics;

		// Token: 0x040014EB RID: 5355
		private Rectangle clip_rectangle;

		// Token: 0x040014EC RID: 5356
		internal bool Handled;

		// Token: 0x040014ED RID: 5357
		private bool disposed;
	}
}
