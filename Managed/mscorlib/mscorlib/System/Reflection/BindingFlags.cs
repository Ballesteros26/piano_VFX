using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</summary>
	// Token: 0x020002D9 RID: 729
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum BindingFlags
	{
		/// <summary>Specifies no binding flag.</summary>
		// Token: 0x04001187 RID: 4487
		Default = 0,
		/// <summary>Specifies that the case of the member name should not be considered when binding.</summary>
		// Token: 0x04001188 RID: 4488
		IgnoreCase = 1,
		/// <summary>Specifies that only members declared at the level of the supplied type's hierarchy should be considered. Inherited members are not considered.</summary>
		// Token: 0x04001189 RID: 4489
		DeclaredOnly = 2,
		/// <summary>Specifies that instance members are to be included in the search.</summary>
		// Token: 0x0400118A RID: 4490
		Instance = 4,
		/// <summary>Specifies that static members are to be included in the search.</summary>
		// Token: 0x0400118B RID: 4491
		Static = 8,
		/// <summary>Specifies that public members are to be included in the search.</summary>
		// Token: 0x0400118C RID: 4492
		Public = 16,
		/// <summary>Specifies that non-public members are to be included in the search.</summary>
		// Token: 0x0400118D RID: 4493
		NonPublic = 32,
		/// <summary>Specifies that public and protected static members up the hierarchy should be returned. Private static members in inherited classes are not returned. Static members include fields, methods, events, and properties. Nested types are not returned.</summary>
		// Token: 0x0400118E RID: 4494
		FlattenHierarchy = 64,
		/// <summary>Specifies that a method is to be invoked. This must not be a constructor or a type initializer.</summary>
		// Token: 0x0400118F RID: 4495
		InvokeMethod = 256,
		/// <summary>Specifies that Reflection should create an instance of the specified type. Calls the constructor that matches the given arguments. The supplied member name is ignored. If the type of lookup is not specified, (Instance | Public) will apply. It is not possible to call a type initializer.</summary>
		// Token: 0x04001190 RID: 4496
		CreateInstance = 512,
		/// <summary>Specifies that the value of the specified field should be returned.</summary>
		// Token: 0x04001191 RID: 4497
		GetField = 1024,
		/// <summary>Specifies that the value of the specified field should be set.</summary>
		// Token: 0x04001192 RID: 4498
		SetField = 2048,
		/// <summary>Specifies that the value of the specified property should be returned.</summary>
		// Token: 0x04001193 RID: 4499
		GetProperty = 4096,
		/// <summary>Specifies that the value of the specified property should be set. For COM properties, specifying this binding flag is equivalent to specifying PutDispProperty and PutRefDispProperty.</summary>
		// Token: 0x04001194 RID: 4500
		SetProperty = 8192,
		/// <summary>Specifies that the PROPPUT member on a COM object should be invoked. PROPPUT specifies a property-setting function that uses a value. Use PutDispProperty if a property has both PROPPUT and PROPPUTREF and you need to distinguish which one is called.</summary>
		// Token: 0x04001195 RID: 4501
		PutDispProperty = 16384,
		/// <summary>Specifies that the PROPPUTREF member on a COM object should be invoked. PROPPUTREF specifies a property-setting function that uses a reference instead of a value. Use PutRefDispProperty if a property has both PROPPUT and PROPPUTREF and you need to distinguish which one is called.</summary>
		// Token: 0x04001196 RID: 4502
		PutRefDispProperty = 32768,
		/// <summary>Specifies that types of the supplied arguments must exactly match the types of the corresponding formal parameters. Reflection throws an exception if the caller supplies a non-null Binder object, since that implies that the caller is supplying BindToXXX implementations that will pick the appropriate method.</summary>
		// Token: 0x04001197 RID: 4503
		ExactBinding = 65536,
		/// <summary>Not implemented.</summary>
		// Token: 0x04001198 RID: 4504
		SuppressChangeType = 131072,
		/// <summary>Returns the set of members whose parameter count matches the number of supplied arguments. This binding flag is used for methods with parameters that have default values and methods with variable arguments (varargs). This flag should only be used with <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		// Token: 0x04001199 RID: 4505
		OptionalParamBinding = 262144,
		/// <summary>Used in COM interop to specify that the return value of the member can be ignored.</summary>
		// Token: 0x0400119A RID: 4506
		IgnoreReturn = 16777216
	}
}
