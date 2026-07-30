using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed type was previously defined in COM.</summary>
	// Token: 0x020008BF RID: 2239
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x06005504 RID: 21764 RVA: 0x00128586 File Offset: 0x00126786
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0012859D File Offset: 0x0012679D
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}
	}
}
