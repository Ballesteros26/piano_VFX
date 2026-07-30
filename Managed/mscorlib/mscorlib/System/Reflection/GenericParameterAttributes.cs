using System;

namespace System.Reflection
{
	/// <summary>Describes the constraints on a generic type parameter of a generic type or method.</summary>
	// Token: 0x020002DE RID: 734
	[Flags]
	public enum GenericParameterAttributes
	{
		/// <summary>There are no special flags.</summary>
		// Token: 0x040011BC RID: 4540
		None = 0,
		/// <summary>Selects the combination of all variance flags. This value is the result of using logical OR to combine the following flags: <see cref="F:System.Reflection.GenericParameterAttributes.Contravariant" /> and <see cref="F:System.Reflection.GenericParameterAttributes.Covariant" />.</summary>
		// Token: 0x040011BD RID: 4541
		VarianceMask = 3,
		/// <summary>The generic type parameter is covariant. A covariant type parameter can appear as the result type of a method, the type of a read-only field, a declared base type, or an implemented interface.</summary>
		// Token: 0x040011BE RID: 4542
		Covariant = 1,
		/// <summary>The generic type parameter is contravariant. A contravariant type parameter can appear as a parameter type in method signatures. </summary>
		// Token: 0x040011BF RID: 4543
		Contravariant = 2,
		/// <summary>Selects the combination of all special constraint flags. This value is the result of using logical OR to combine the following flags: <see cref="F:System.Reflection.GenericParameterAttributes.DefaultConstructorConstraint" />, <see cref="F:System.Reflection.GenericParameterAttributes.ReferenceTypeConstraint" />, and <see cref="F:System.Reflection.GenericParameterAttributes.NotNullableValueTypeConstraint" />.</summary>
		// Token: 0x040011C0 RID: 4544
		SpecialConstraintMask = 28,
		/// <summary>A type can be substituted for the generic type parameter only if it is a reference type.</summary>
		// Token: 0x040011C1 RID: 4545
		ReferenceTypeConstraint = 4,
		/// <summary>A type can be substituted for the generic type parameter only if it is a value type and is not nullable.</summary>
		// Token: 0x040011C2 RID: 4546
		NotNullableValueTypeConstraint = 8,
		/// <summary>A type can be substituted for the generic type parameter only if it has a parameterless constructor.</summary>
		// Token: 0x040011C3 RID: 4547
		DefaultConstructorConstraint = 16
	}
}
