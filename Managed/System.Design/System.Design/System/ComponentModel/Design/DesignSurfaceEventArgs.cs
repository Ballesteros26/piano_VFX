using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignSurfaceManager.DesignSurfaceCreated" /> event.</summary>
	// Token: 0x02000108 RID: 264
	public class DesignSurfaceEventArgs : EventArgs
	{
		/// <summary>Gets the design surface that is being created.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignSurface" /> that is being created.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0000CFC6 File Offset: 0x0000B1C6
		public DesignSurface Surface
		{
			get
			{
				return this._surface;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurfaceEventArgs" /> class.</summary>
		/// <param name="surface">The design surface that is being created.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="surface" /> is null.</exception>
		// Token: 0x060007B7 RID: 1975 RVA: 0x0000CFCE File Offset: 0x0000B1CE
		public DesignSurfaceEventArgs(DesignSurface surface)
		{
			this._surface = surface;
		}

		// Token: 0x040001A2 RID: 418
		private DesignSurface _surface;
	}
}
