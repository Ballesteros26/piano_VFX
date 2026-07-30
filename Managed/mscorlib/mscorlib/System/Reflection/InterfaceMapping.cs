using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Retrieves the mapping of an interface into the actual methods on a class that implements that interface.</summary>
	// Token: 0x020002E0 RID: 736
	[ComVisible(true)]
	public struct InterfaceMapping
	{
		/// <summary>Represents the type that was used to create the interface mapping.</summary>
		// Token: 0x040011C4 RID: 4548
		[ComVisible(true)]
		public Type TargetType;

		/// <summary>Shows the type that represents the interface.</summary>
		// Token: 0x040011C5 RID: 4549
		[ComVisible(true)]
		public Type InterfaceType;

		/// <summary>Shows the methods that implement the interface.</summary>
		// Token: 0x040011C6 RID: 4550
		[ComVisible(true)]
		public MethodInfo[] TargetMethods;

		/// <summary>Shows the methods that are defined on the interface.</summary>
		// Token: 0x040011C7 RID: 4551
		[ComVisible(true)]
		public MethodInfo[] InterfaceMethods;
	}
}
