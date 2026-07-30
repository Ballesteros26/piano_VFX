using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000982 RID: 2434
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x060059E7 RID: 23015
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060059E8 RID: 23016
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x060059E9 RID: 23017
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060059EA RID: 23018
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060059EB RID: 23019
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060059EC RID: 23020
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x060059ED RID: 23021
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060059EE RID: 23022
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x060059EF RID: 23023
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x060059F0 RID: 23024
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060059F1 RID: 23025
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060059F2 RID: 23026
		Type UnderlyingSystemType { get; }
	}
}
