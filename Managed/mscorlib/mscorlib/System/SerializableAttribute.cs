using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that a class can be serialized. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001BE RID: 446
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class SerializableAttribute : Attribute
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x0004D712 File Offset: 0x0004B912
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Serializable) != TypeAttributes.Serializable)
			{
				return null;
			}
			return new SerializableAttribute();
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004D72E File Offset: 0x0004B92E
		internal static bool IsDefined(RuntimeType type)
		{
			return type.IsSerializable;
		}
	}
}
