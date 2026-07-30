using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that a field of a serializable class should not be serialized. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A2 RID: 418
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class NonSerializedAttribute : Attribute
	{
		// Token: 0x0600119D RID: 4509 RVA: 0x00048453 File Offset: 0x00046653
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope)
			{
				return null;
			}
			return new NonSerializedAttribute();
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004846A File Offset: 0x0004666A
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return (field.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
		}
	}
}
