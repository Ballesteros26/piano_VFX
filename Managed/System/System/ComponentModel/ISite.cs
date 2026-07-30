using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by sites.</summary>
	// Token: 0x0200028A RID: 650
	[ComVisible(true)]
	public interface ISite : IServiceProvider
	{
		/// <summary>Gets the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001478 RID: 5240
		IComponent Component { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001479 RID: 5241
		IContainer Container { get; }

		/// <summary>Determines whether the component is in design mode when implemented by a class.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600147A RID: 5242
		bool DesignMode { get; }

		/// <summary>Gets or sets the name of the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The name of the component associated with the <see cref="T:System.ComponentModel.ISite" />; or null, if no name is assigned to the component.</returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600147B RID: 5243
		// (set) Token: 0x0600147C RID: 5244
		string Name { get; set; }
	}
}
