using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a method, specifies that the method is during serialization of an object in an object graph. The order of serialization relative to other objects in the graph is non-deterministic.</summary>
	// Token: 0x020006E6 RID: 1766
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class OnSerializingAttribute : Attribute
	{
	}
}
