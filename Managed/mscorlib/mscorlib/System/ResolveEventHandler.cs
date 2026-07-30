using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a method that handles the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, or <see cref="E:System.AppDomain.AssemblyResolve" /> event of an <see cref="T:System.AppDomain" />.</summary>
	/// <returns>The assembly that resolves the type, assembly, or resource; or null if the assembly cannot be resolved.</returns>
	/// <param name="sender">The source of the event. </param>
	/// <param name="args">The event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022B RID: 555
	// (Invoke) Token: 0x06001A60 RID: 6752
	[ComVisible(true)]
	[Serializable]
	public delegate Assembly ResolveEventHandler(object sender, ResolveEventArgs args);
}
