using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the value of a static field is unique for a particular context.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000143 RID: 323
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public class ContextStaticAttribute : Attribute
	{
	}
}
