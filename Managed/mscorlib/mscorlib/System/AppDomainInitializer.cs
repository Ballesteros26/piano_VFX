using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the callback method to invoke when the application domain is initialized.</summary>
	/// <param name="args">An array of strings to pass as arguments to the callback method.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000201 RID: 513
	// (Invoke) Token: 0x060017D4 RID: 6100
	[ComVisible(true)]
	[Serializable]
	public delegate void AppDomainInitializer(string[] args);
}
