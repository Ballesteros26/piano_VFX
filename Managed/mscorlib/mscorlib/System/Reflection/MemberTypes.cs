using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Marks each type of member that is defined as a derived class of MemberInfo.</summary>
	// Token: 0x020002EF RID: 751
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum MemberTypes
	{
		/// <summary>Specifies that the member is a constructor, representing a <see cref="T:System.Reflection.ConstructorInfo" /> member. Hexadecimal value of 0x01.</summary>
		// Token: 0x04001244 RID: 4676
		Constructor = 1,
		/// <summary>Specifies that the member is an event, representing an <see cref="T:System.Reflection.EventInfo" /> member. Hexadecimal value of 0x02.</summary>
		// Token: 0x04001245 RID: 4677
		Event = 2,
		/// <summary>Specifies that the member is a field, representing a <see cref="T:System.Reflection.FieldInfo" /> member. Hexadecimal value of 0x04.</summary>
		// Token: 0x04001246 RID: 4678
		Field = 4,
		/// <summary>Specifies that the member is a method, representing a <see cref="T:System.Reflection.MethodInfo" /> member. Hexadecimal value of 0x08.</summary>
		// Token: 0x04001247 RID: 4679
		Method = 8,
		/// <summary>Specifies that the member is a property, representing a <see cref="T:System.Reflection.PropertyInfo" /> member. Hexadecimal value of 0x10.</summary>
		// Token: 0x04001248 RID: 4680
		Property = 16,
		/// <summary>Specifies that the member is a type, representing a <see cref="F:System.Reflection.MemberTypes.TypeInfo" /> member. Hexadecimal value of 0x20.</summary>
		// Token: 0x04001249 RID: 4681
		TypeInfo = 32,
		/// <summary>Specifies that the member is a custom member type. Hexadecimal value of 0x40.</summary>
		// Token: 0x0400124A RID: 4682
		Custom = 64,
		/// <summary>Specifies that the member is a nested type, extending <see cref="T:System.Reflection.MemberInfo" />.</summary>
		// Token: 0x0400124B RID: 4683
		NestedType = 128,
		/// <summary>Specifies all member types.</summary>
		// Token: 0x0400124C RID: 4684
		All = 191
	}
}
