using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.DesignSurfaceManager.ActiveDesignSurfaceChanged" /> event of a <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" />. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event, the <see cref="T:System.ComponentModel.Design.DesignSurfaceManager" />.</param>
	/// <param name="e">An <see cref="T:System.ComponentModel.Design.ActiveDesignSurfaceChangedEventArgs" />    that contains the event data.</param>
	// Token: 0x020000F1 RID: 241
	// (Invoke) Token: 0x060006DA RID: 1754
	public delegate void ActiveDesignSurfaceChangedEventHandler(object sender, ActiveDesignSurfaceChangedEventArgs e);
}
