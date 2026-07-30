using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is single-threaded apartment (STA). </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C8 RID: 456
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class STAThreadAttribute : Attribute
	{
	}
}
