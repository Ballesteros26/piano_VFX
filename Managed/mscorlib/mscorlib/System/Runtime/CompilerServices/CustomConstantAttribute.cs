using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a constant value that a compiler can persist for a field or method parameter.</summary>
	// Token: 0x0200086F RID: 2159
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		/// <summary>Gets the constant value stored by this attribute.</summary>
		/// <returns>The constant value stored by this attribute.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x0600544A RID: 21578
		public abstract object Value { get; }

		// Token: 0x0600544B RID: 21579 RVA: 0x0012730C File Offset: 0x0012550C
		internal static object GetRawConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return customAttributeNamedArgument.TypedValue.Value;
				}
			}
			return DBNull.Value;
		}
	}
}
