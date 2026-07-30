using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Used by <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> for cross-application domain calls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020E RID: 526
	// (Invoke) Token: 0x060018F2 RID: 6386
	[ComVisible(true)]
	public delegate void CrossAppDomainDelegate();
}
