using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> method.</summary>
	// Token: 0x02000120 RID: 288
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PaintValueEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> class using the specified values.</summary>
		/// <param name="context">The context in which the value appears. </param>
		/// <param name="value">The value to paint. </param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object with which drawing is to be done. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which drawing is to be done. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.</exception>
		// Token: 0x06000D67 RID: 3431 RVA: 0x0001D8EE File Offset: 0x0001BAEE
		public PaintValueEventArgs(ITypeDescriptorContext context, object value, Graphics graphics, Rectangle bounds)
		{
			this.context = context;
			this.valueToPaint = value;
			this.graphics = graphics;
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.bounds = bounds;
		}

		/// <summary>Gets the rectangle that indicates the area in which the painting should be done.</summary>
		/// <returns>The rectangle that indicates the area in which the painting should be done.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0001D921 File Offset: 0x0001BB21
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface to be used to gain additional information about the context this value appears in.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the event.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0001D929 File Offset: 0x0001BB29
		public ITypeDescriptorContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object with which painting should be done.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> object to use for painting.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0001D931 File Offset: 0x0001BB31
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the value to paint.</summary>
		/// <returns>An object indicating what to paint.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0001D939 File Offset: 0x0001BB39
		public object Value
		{
			get
			{
				return this.valueToPaint;
			}
		}

		// Token: 0x04000A7D RID: 2685
		private readonly ITypeDescriptorContext context;

		// Token: 0x04000A7E RID: 2686
		private readonly object valueToPaint;

		// Token: 0x04000A7F RID: 2687
		private readonly Graphics graphics;

		// Token: 0x04000A80 RID: 2688
		private readonly Rectangle bounds;
	}
}
