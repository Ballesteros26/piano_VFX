using System;

namespace System.Windows.Forms.Layout
{
	/// <summary>Provides the base class for implementing layout engines.</summary>
	// Token: 0x020001FF RID: 511
	public abstract class LayoutEngine
	{
		/// <summary>Initializes the layout engine.</summary>
		/// <param name="child">The container on which the layout engine will operate.</param>
		/// <param name="specified">The bounds defining the container's size and position.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="child" /> is not a type on which <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> can perform layout.</exception>
		// Token: 0x06001F8F RID: 8079 RVA: 0x00076388 File Offset: 0x00074588
		public virtual void InitLayout(object child, BoundsSpecified specified)
		{
		}

		/// <summary>Requests that the layout engine perform a layout operation.</summary>
		/// <returns>true if layout should be performed again by the parent of <paramref name="container" />; otherwise, false.</returns>
		/// <param name="container">The container on which the layout engine will operate.</param>
		/// <param name="layoutEventArgs">An event argument from a <see cref="E:System.Windows.Forms.Control.Layout" /> event.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="container" /> is not a type on which <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> can perform layout.</exception>
		// Token: 0x06001F90 RID: 8080 RVA: 0x0007638C File Offset: 0x0007458C
		public virtual bool Layout(object container, LayoutEventArgs layoutEventArgs)
		{
			return false;
		}
	}
}
