using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignSurfaceManager.ActiveDesignSurfaceChanged" /> event.</summary>
	// Token: 0x020000F0 RID: 240
	public class ActiveDesignSurfaceChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignSurfaceChangedEventArgs" /> class.</summary>
		/// <param name="oldSurface">The design surface that is losing activation.</param>
		/// <param name="newSurface">The design surface that is gaining activation.</param>
		// Token: 0x060006D6 RID: 1750 RVA: 0x0000A6BE File Offset: 0x000088BE
		public ActiveDesignSurfaceChangedEventArgs(DesignSurface oldSurface, DesignSurface newSurface)
		{
			this._newSurface = newSurface;
			this._oldSurface = oldSurface;
		}

		/// <summary>Gets the design surface that is losing activation.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignSurface" /> that is losing activation.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000A6D4 File Offset: 0x000088D4
		public DesignSurface OldSurface
		{
			get
			{
				return this._oldSurface;
			}
		}

		/// <summary>Gets the design surface that is gaining activation.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignSurface" /> that is gaining activation.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000A6DC File Offset: 0x000088DC
		public DesignSurface NewSurface
		{
			get
			{
				return this._newSurface;
			}
		}

		// Token: 0x04000164 RID: 356
		private DesignSurface _oldSurface;

		// Token: 0x04000165 RID: 357
		private DesignSurface _newSurface;
	}
}
