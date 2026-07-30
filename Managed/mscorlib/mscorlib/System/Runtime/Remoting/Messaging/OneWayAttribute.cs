using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Marks a method as one way, without a return value and out or ref parameters.</summary>
	// Token: 0x02000820 RID: 2080
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public class OneWayAttribute : Attribute
	{
	}
}
