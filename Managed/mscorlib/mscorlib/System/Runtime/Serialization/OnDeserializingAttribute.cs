using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a method, specifies that the method is called during deserialization of an object in an object graph. The order of deserialization relative to other objects in the graph is non-deterministic.</summary>
	// Token: 0x020006E8 RID: 1768
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class OnDeserializingAttribute : Attribute
	{
	}
}
