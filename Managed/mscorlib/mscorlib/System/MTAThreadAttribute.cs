using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is multithreaded apartment (MTA). </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C9 RID: 457
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class MTAThreadAttribute : Attribute
	{
	}
}
