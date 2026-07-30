using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using Mono;

namespace System
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeType : global::System.Reflection.TypeInfo, ISerializable, ICloneable
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00049EA8 File Offset: 0x000480A8
		private static void ThrowIfTypeNeverValidGenericArgument(RuntimeType type)
		{
			if (type.IsPointer || type.IsByRef || type == typeof(void))
			{
				throw new ArgumentException(Environment.GetResourceString("The type '{0}' may not be used as a type argument.", new object[] { type.ToString() }));
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00049EF8 File Offset: 0x000480F8
		internal static void SanityCheckGenericArguments(RuntimeType[] genericArguments, RuntimeType[] genericParamters)
		{
			if (genericArguments == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType.ThrowIfTypeNeverValidGenericArgument(genericArguments[i]);
			}
			if (genericArguments.Length != genericParamters.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The type or method has {1} generic parameter(s), but {0} generic argument(s) were provided. A generic argument must be provided for each generic parameter.", new object[] { genericArguments.Length, genericParamters.Length }));
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00049F6C File Offset: 0x0004816C
		private static void SplitName(string fullname, out string name, out string ns)
		{
			name = null;
			ns = null;
			if (fullname == null)
			{
				return;
			}
			int num = fullname.LastIndexOf(".", StringComparison.Ordinal);
			if (num == -1)
			{
				name = fullname;
				return;
			}
			ns = fullname.Substring(0, num);
			int num2 = fullname.Length - ns.Length - 1;
			if (num2 != 0)
			{
				name = fullname.Substring(num + 1, num2);
				return;
			}
			name = "";
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00049FCC File Offset: 0x000481CC
		internal static BindingFlags FilterPreCalculate(bool isPublic, bool isInherited, bool isStatic)
		{
			BindingFlags bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			if (isInherited)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
				if (isStatic)
				{
					bindingFlags |= BindingFlags.Static | BindingFlags.FlattenHierarchy;
				}
				else
				{
					bindingFlags |= BindingFlags.Instance;
				}
			}
			else if (isStatic)
			{
				bindingFlags |= BindingFlags.Static;
			}
			else
			{
				bindingFlags |= BindingFlags.Instance;
			}
			return bindingFlags;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004A008 File Offset: 0x00048208
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, bool allowPrefixLookup, out bool prefixLookup, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			prefixLookup = false;
			ignoreCase = false;
			if (name != null)
			{
				if ((bindingFlags & BindingFlags.IgnoreCase) != BindingFlags.Default)
				{
					name = name.ToLower(CultureInfo.InvariantCulture);
					ignoreCase = true;
					listType = RuntimeType.MemberListType.CaseInsensitive;
				}
				else
				{
					listType = RuntimeType.MemberListType.CaseSensitive;
				}
				if (allowPrefixLookup && name.EndsWith("*", StringComparison.Ordinal))
				{
					name = name.Substring(0, name.Length - 1);
					prefixLookup = true;
					listType = RuntimeType.MemberListType.All;
					return;
				}
			}
			else
			{
				listType = RuntimeType.MemberListType.All;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0004A074 File Offset: 0x00048274
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			bool flag;
			RuntimeType.FilterHelper(bindingFlags, ref name, false, out flag, out ignoreCase, out listType);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0004A08D File Offset: 0x0004828D
		private static bool FilterApplyPrefixLookup(MemberInfo memberInfo, string name, bool ignoreCase)
		{
			if (ignoreCase)
			{
				if (!memberInfo.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else if (!memberInfo.Name.StartsWith(name, StringComparison.Ordinal))
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0004A0B8 File Offset: 0x000482B8
		private static bool FilterApplyBase(MemberInfo memberInfo, BindingFlags bindingFlags, bool isPublic, bool isNonProtectedInternal, bool isStatic, string name, bool prefixLookup)
		{
			if (isPublic)
			{
				if ((bindingFlags & BindingFlags.Public) == BindingFlags.Default)
				{
					return false;
				}
			}
			else if ((bindingFlags & BindingFlags.NonPublic) == BindingFlags.Default)
			{
				return false;
			}
			bool flag = memberInfo.DeclaringType != memberInfo.ReflectedType;
			if ((bindingFlags & BindingFlags.DeclaredOnly) > BindingFlags.Default && flag)
			{
				return false;
			}
			if (memberInfo.MemberType != MemberTypes.TypeInfo && memberInfo.MemberType != MemberTypes.NestedType)
			{
				if (isStatic)
				{
					if ((bindingFlags & BindingFlags.FlattenHierarchy) == BindingFlags.Default && flag)
					{
						return false;
					}
					if ((bindingFlags & BindingFlags.Static) == BindingFlags.Default)
					{
						return false;
					}
				}
				else if ((bindingFlags & BindingFlags.Instance) == BindingFlags.Default)
				{
					return false;
				}
			}
			if (prefixLookup && !RuntimeType.FilterApplyPrefixLookup(memberInfo, name, (bindingFlags & BindingFlags.IgnoreCase) > BindingFlags.Default))
			{
				return false;
			}
			if ((bindingFlags & BindingFlags.DeclaredOnly) == BindingFlags.Default && flag && isNonProtectedInternal && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.Default && !isStatic && (bindingFlags & BindingFlags.Instance) != BindingFlags.Default)
			{
				MethodInfo methodInfo = memberInfo as MethodInfo;
				if (methodInfo == null)
				{
					return false;
				}
				if (!methodInfo.IsVirtual && !methodInfo.IsAbstract)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004A184 File Offset: 0x00048384
		private static bool FilterApplyType(Type type, BindingFlags bindingFlags, string name, bool prefixLookup, string ns)
		{
			bool flag = type.IsNestedPublic || type.IsPublic;
			bool flag2 = false;
			return RuntimeType.FilterApplyBase(type, bindingFlags, flag, type.IsNestedAssembly, flag2, name, prefixLookup) && (ns == null || type.Namespace.Equals(ns));
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004A1D0 File Offset: 0x000483D0
		private static bool FilterApplyMethodInfo(RuntimeMethodInfo method, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(method, method.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004A1E1 File Offset: 0x000483E1
		private static bool FilterApplyConstructorInfo(RuntimeConstructorInfo constructor, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(constructor, constructor.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004A1F4 File Offset: 0x000483F4
		private static bool FilterApplyMethodBase(MethodBase methodBase, BindingFlags methodFlags, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			bindingFlags ^= BindingFlags.DeclaredOnly;
			if ((callConv & CallingConventions.Any) == (CallingConventions)0)
			{
				if ((callConv & CallingConventions.VarArgs) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
				{
					return false;
				}
				if ((callConv & CallingConventions.Standard) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.Standard) == (CallingConventions)0)
				{
					return false;
				}
			}
			if (argumentTypes != null)
			{
				ParameterInfo[] parametersNoCopy = methodBase.GetParametersNoCopy();
				if (argumentTypes.Length != parametersNoCopy.Length)
				{
					if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
					{
						return false;
					}
					bool flag = false;
					if (argumentTypes.Length > parametersNoCopy.Length)
					{
						if ((methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
						{
							flag = true;
						}
					}
					else if ((bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						flag = true;
					}
					else if (!parametersNoCopy[argumentTypes.Length].IsOptional)
					{
						flag = true;
					}
					if (flag)
					{
						if (parametersNoCopy.Length == 0)
						{
							return false;
						}
						if (argumentTypes.Length < parametersNoCopy.Length - 1)
						{
							return false;
						}
						ParameterInfo parameterInfo = parametersNoCopy[parametersNoCopy.Length - 1];
						if (!parameterInfo.ParameterType.IsArray)
						{
							return false;
						}
						if (!parameterInfo.IsDefined(typeof(ParamArrayAttribute), false))
						{
							return false;
						}
					}
				}
				else if ((bindingFlags & BindingFlags.ExactBinding) != BindingFlags.Default && (bindingFlags & BindingFlags.InvokeMethod) == BindingFlags.Default)
				{
					for (int i = 0; i < parametersNoCopy.Length; i++)
					{
						if (argumentTypes[i] != null && parametersNoCopy[i].ParameterType != argumentTypes[i])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0004A304 File Offset: 0x00048504
		internal RuntimeType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004A314 File Offset: 0x00048514
		internal bool IsSpecialSerializableType()
		{
			RuntimeType runtimeType = this;
			while (!(runtimeType == RuntimeType.DelegateType) && !(runtimeType == RuntimeType.EnumType))
			{
				runtimeType = runtimeType.GetBaseType();
				if (!(runtimeType != null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004A350 File Offset: 0x00048550
		private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeMethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, flag2, this);
			RuntimeType.ListBuilder<MethodInfo> listBuilder = new RuntimeType.ListBuilder<MethodInfo>(methodsByName.Length);
			foreach (RuntimeMethodInfo runtimeMethodInfo in methodsByName)
			{
				if (RuntimeType.FilterApplyMethodInfo(runtimeMethodInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeMethodInfo, name, flag2)))
				{
					listBuilder.Add(runtimeMethodInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004A3C0 File Offset: 0x000485C0
		private RuntimeType.ListBuilder<ConstructorInfo> GetConstructorCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			if (name != null && name != ConstructorInfo.ConstructorName && name != ConstructorInfo.TypeConstructorName)
			{
				return new RuntimeType.ListBuilder<ConstructorInfo>(0);
			}
			RuntimeConstructorInfo[] constructors_internal = this.GetConstructors_internal(bindingAttr, this);
			RuntimeType.ListBuilder<ConstructorInfo> listBuilder = new RuntimeType.ListBuilder<ConstructorInfo>(constructors_internal.Length);
			foreach (RuntimeConstructorInfo runtimeConstructorInfo in constructors_internal)
			{
				if (RuntimeType.FilterApplyConstructorInfo(runtimeConstructorInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeConstructorInfo, name, flag2)))
				{
					listBuilder.Add(runtimeConstructorInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0004A454 File Offset: 0x00048654
		private RuntimeType.ListBuilder<PropertyInfo> GetPropertyCandidates(string name, BindingFlags bindingAttr, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimePropertyInfo[] propertiesByName = this.GetPropertiesByName(name, bindingAttr, flag2, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<PropertyInfo> listBuilder = new RuntimeType.ListBuilder<PropertyInfo>(propertiesByName.Length);
			foreach (RuntimePropertyInfo runtimePropertyInfo in propertiesByName)
			{
				if ((bindingAttr & runtimePropertyInfo.BindingFlags) == runtimePropertyInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimePropertyInfo, name, flag2)) && (types == null || runtimePropertyInfo.GetIndexParameters().Length == types.Length))
				{
					listBuilder.Add(runtimePropertyInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004A4E0 File Offset: 0x000486E0
		private RuntimeType.ListBuilder<EventInfo> GetEventCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeEventInfo[] events_internal = this.GetEvents_internal(name, bindingAttr, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<EventInfo> listBuilder = new RuntimeType.ListBuilder<EventInfo>(events_internal.Length);
			foreach (RuntimeEventInfo runtimeEventInfo in events_internal)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeEventInfo, name, flag2)))
				{
					listBuilder.Add(runtimeEventInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0004A558 File Offset: 0x00048758
		private RuntimeType.ListBuilder<FieldInfo> GetFieldCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeFieldInfo[] fields_internal = this.GetFields_internal(name, bindingAttr, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<FieldInfo> listBuilder = new RuntimeType.ListBuilder<FieldInfo>(fields_internal.Length);
			foreach (RuntimeFieldInfo runtimeFieldInfo in fields_internal)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeFieldInfo, name, flag2)))
				{
					listBuilder.Add(runtimeFieldInfo);
				}
			}
			return listBuilder;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004A5D0 File Offset: 0x000487D0
		private RuntimeType.ListBuilder<Type> GetNestedTypeCandidates(string fullname, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bindingAttr &= ~BindingFlags.Static;
			string text;
			string text2;
			RuntimeType.SplitName(fullname, out text, out text2);
			bool flag;
			bool flag2;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref text, allowPrefixLookup, out flag, out flag2, out memberListType);
			RuntimeType[] nestedTypes_internal = this.GetNestedTypes_internal(text, bindingAttr);
			RuntimeType.ListBuilder<Type> listBuilder = new RuntimeType.ListBuilder<Type>(nestedTypes_internal.Length);
			foreach (RuntimeType runtimeType in nestedTypes_internal)
			{
				if (RuntimeType.FilterApplyType(runtimeType, bindingAttr, text, flag, text2))
				{
					listBuilder.Add(runtimeType);
				}
			}
			return listBuilder;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004A644 File Offset: 0x00048844
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodCandidates(null, bindingAttr, CallingConventions.Any, null, false).ToArray();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0004A664 File Offset: 0x00048864
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false).ToArray();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0004A684 File Offset: 0x00048884
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetPropertyCandidates(null, bindingAttr, null, false).ToArray();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0004A6A4 File Offset: 0x000488A4
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetEventCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004A6C4 File Offset: 0x000488C4
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetFieldCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004A6E4 File Offset: 0x000488E4
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.GetNestedTypeCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004A704 File Offset: 0x00048904
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(null, bindingAttr, CallingConventions.Any, null, false);
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false);
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(null, bindingAttr, null, false);
			RuntimeType.ListBuilder<EventInfo> eventCandidates = this.GetEventCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<FieldInfo> fieldCandidates = this.GetFieldCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<Type> nestedTypeCandidates = this.GetNestedTypeCandidates(null, bindingAttr, false);
			MemberInfo[] array = new MemberInfo[methodCandidates.Count + constructorCandidates.Count + propertyCandidates.Count + eventCandidates.Count + fieldCandidates.Count + nestedTypeCandidates.Count];
			int num = 0;
			methodCandidates.CopyTo(array, num);
			num += methodCandidates.Count;
			constructorCandidates.CopyTo(array, num);
			num += constructorCandidates.Count;
			propertyCandidates.CopyTo(array, num);
			num += propertyCandidates.Count;
			eventCandidates.CopyTo(array, num);
			num += eventCandidates.Count;
			fieldCandidates.CopyTo(array, num);
			num += fieldCandidates.Count;
			nestedTypeCandidates.CopyTo(array, num);
			num += nestedTypeCandidates.Count;
			return array;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004A81C File Offset: 0x00048A1C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(name, bindingAttr, callConv, types, false);
			if (methodCandidates.Count == 0)
			{
				return null;
			}
			if (types == null || types.Length == 0)
			{
				MethodInfo methodInfo = methodCandidates[0];
				if (methodCandidates.Count == 1)
				{
					return methodInfo;
				}
				if (types == null)
				{
					for (int i = 1; i < methodCandidates.Count; i++)
					{
						if (!global::System.DefaultBinder.CompareMethodSigAndName(methodCandidates[i], methodInfo))
						{
							throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
						}
					}
					return global::System.DefaultBinder.FindMostDerivedNewSlotMeth(methodCandidates.ToArray(), methodCandidates.Count) as MethodInfo;
				}
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			return binder.SelectMethod(bindingAttr, methodCandidates.ToArray(), types, modifiers) as MethodInfo;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004A8D0 File Offset: 0x00048AD0
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, types, false);
			if (constructorCandidates.Count == 0)
			{
				return null;
			}
			if (types.Length == 0 && constructorCandidates.Count == 1)
			{
				ConstructorInfo constructorInfo = constructorCandidates[0];
				ParameterInfo[] parametersNoCopy = constructorInfo.GetParametersNoCopy();
				if (parametersNoCopy == null || parametersNoCopy.Length == 0)
				{
					return constructorInfo;
				}
			}
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				return global::System.DefaultBinder.ExactBinding(constructorCandidates.ToArray(), types, modifiers) as ConstructorInfo;
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			return binder.SelectMethod(bindingAttr, constructorCandidates.ToArray(), types, modifiers) as ConstructorInfo;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004A960 File Offset: 0x00048B60
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(name, bindingAttr, types, false);
			if (propertyCandidates.Count == 0)
			{
				return null;
			}
			if (types == null || types.Length == 0)
			{
				if (propertyCandidates.Count == 1)
				{
					PropertyInfo propertyInfo = propertyCandidates[0];
					if (returnType != null && !returnType.IsEquivalentTo(propertyInfo.PropertyType))
					{
						return null;
					}
					return propertyInfo;
				}
				else if (returnType == null)
				{
					throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
				}
			}
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				return global::System.DefaultBinder.ExactPropertyBinding(propertyCandidates.ToArray(), returnType, types, modifiers);
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			return binder.SelectProperty(bindingAttr, propertyCandidates.ToArray(), returnType, types, modifiers);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004AA10 File Offset: 0x00048C10
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out memberListType);
			RuntimeEventInfo[] events_internal = this.GetEvents_internal(name, bindingAttr, this);
			EventInfo eventInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			foreach (RuntimeEventInfo runtimeEventInfo in events_internal)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags)
				{
					if (eventInfo != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					eventInfo = runtimeEventInfo;
				}
			}
			return eventInfo;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004AA8C File Offset: 0x00048C8C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out memberListType);
			RuntimeFieldInfo[] fields_internal = this.GetFields_internal(name, bindingAttr, this);
			FieldInfo fieldInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			bool flag2 = false;
			foreach (RuntimeFieldInfo runtimeFieldInfo in fields_internal)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags)
				{
					if (fieldInfo != null)
					{
						if (runtimeFieldInfo.DeclaringType == fieldInfo.DeclaringType)
						{
							throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
						}
						if (fieldInfo.DeclaringType.IsInterface && runtimeFieldInfo.DeclaringType.IsInterface)
						{
							flag2 = true;
						}
					}
					if (fieldInfo == null || runtimeFieldInfo.DeclaringType.IsSubclassOf(fieldInfo.DeclaringType) || fieldInfo.DeclaringType.IsInterface)
					{
						fieldInfo = runtimeFieldInfo;
					}
				}
			}
			if (flag2 && fieldInfo.DeclaringType.IsInterface)
			{
				throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
			}
			return fieldInfo;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004AB88 File Offset: 0x00048D88
		public override Type GetInterface(string fullname, bool ignoreCase)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException();
			}
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			bindingFlags &= ~BindingFlags.Static;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			string text;
			string text2;
			RuntimeType.SplitName(fullname, out text, out text2);
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingFlags, ref text, out ignoreCase, out memberListType);
			List<RuntimeType> list = null;
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			foreach (RuntimeType runtimeType in this.GetInterfaces())
			{
				if (string.Equals(runtimeType.Name, text, stringComparison))
				{
					if (list == null)
					{
						list = new List<RuntimeType>(2);
					}
					list.Add(runtimeType);
				}
			}
			if (list == null)
			{
				return null;
			}
			RuntimeType[] array = list.ToArray();
			RuntimeType runtimeType2 = null;
			foreach (RuntimeType runtimeType3 in array)
			{
				if (RuntimeType.FilterApplyType(runtimeType3, bindingFlags, text, false, text2))
				{
					if (runtimeType2 != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					runtimeType2 = runtimeType3;
				}
			}
			return runtimeType2;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004AC78 File Offset: 0x00048E78
		public override Type GetNestedType(string fullname, BindingFlags bindingAttr)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException();
			}
			bindingAttr &= ~BindingFlags.Static;
			string text;
			string text2;
			RuntimeType.SplitName(fullname, out text, out text2);
			bool flag;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref text, out flag, out memberListType);
			RuntimeType[] nestedTypes_internal = this.GetNestedTypes_internal(text, bindingAttr);
			RuntimeType runtimeType = null;
			foreach (RuntimeType runtimeType2 in nestedTypes_internal)
			{
				if (RuntimeType.FilterApplyType(runtimeType2, bindingAttr, text, false, text2))
				{
					if (runtimeType != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					runtimeType = runtimeType2;
				}
			}
			return runtimeType;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004AD00 File Offset: 0x00048F00
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			RuntimeType.ListBuilder<MethodInfo> listBuilder = default(RuntimeType.ListBuilder<MethodInfo>);
			RuntimeType.ListBuilder<ConstructorInfo> listBuilder2 = default(RuntimeType.ListBuilder<ConstructorInfo>);
			RuntimeType.ListBuilder<PropertyInfo> listBuilder3 = default(RuntimeType.ListBuilder<PropertyInfo>);
			RuntimeType.ListBuilder<EventInfo> listBuilder4 = default(RuntimeType.ListBuilder<EventInfo>);
			RuntimeType.ListBuilder<FieldInfo> listBuilder5 = default(RuntimeType.ListBuilder<FieldInfo>);
			RuntimeType.ListBuilder<Type> listBuilder6 = default(RuntimeType.ListBuilder<Type>);
			int num = 0;
			if ((type & MemberTypes.Method) != (MemberTypes)0)
			{
				listBuilder = this.GetMethodCandidates(name, bindingAttr, CallingConventions.Any, null, true);
				if (type == MemberTypes.Method)
				{
					return listBuilder.ToArray();
				}
				num += listBuilder.Count;
			}
			if ((type & MemberTypes.Constructor) != (MemberTypes)0)
			{
				listBuilder2 = this.GetConstructorCandidates(name, bindingAttr, CallingConventions.Any, null, true);
				if (type == MemberTypes.Constructor)
				{
					return listBuilder2.ToArray();
				}
				num += listBuilder2.Count;
			}
			if ((type & MemberTypes.Property) != (MemberTypes)0)
			{
				listBuilder3 = this.GetPropertyCandidates(name, bindingAttr, null, true);
				if (type == MemberTypes.Property)
				{
					return listBuilder3.ToArray();
				}
				num += listBuilder3.Count;
			}
			if ((type & MemberTypes.Event) != (MemberTypes)0)
			{
				listBuilder4 = this.GetEventCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Event)
				{
					return listBuilder4.ToArray();
				}
				num += listBuilder4.Count;
			}
			if ((type & MemberTypes.Field) != (MemberTypes)0)
			{
				listBuilder5 = this.GetFieldCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Field)
				{
					return listBuilder5.ToArray();
				}
				num += listBuilder5.Count;
			}
			if ((type & (MemberTypes.TypeInfo | MemberTypes.NestedType)) != (MemberTypes)0)
			{
				listBuilder6 = this.GetNestedTypeCandidates(name, bindingAttr, true);
				if (type == MemberTypes.NestedType || type == MemberTypes.TypeInfo)
				{
					return listBuilder6.ToArray();
				}
				num += listBuilder6.Count;
			}
			MemberInfo[] array = ((type == (MemberTypes.Constructor | MemberTypes.Method)) ? new MethodBase[num] : new MemberInfo[num]);
			int num2 = 0;
			listBuilder.CopyTo(array, num2);
			num2 += listBuilder.Count;
			listBuilder2.CopyTo(array, num2);
			num2 += listBuilder2.Count;
			listBuilder3.CopyTo(array, num2);
			num2 += listBuilder3.Count;
			listBuilder4.CopyTo(array, num2);
			num2 += listBuilder4.Count;
			listBuilder5.CopyTo(array, num2);
			num2 += listBuilder5.Count;
			listBuilder6.CopyTo(array, num2);
			num2 += listBuilder6.Count;
			return array;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004AEEF File Offset: 0x000490EF
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0004AEF7 File Offset: 0x000490F7
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004AEFF File Offset: 0x000490FF
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004AF07 File Offset: 0x00049107
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return RuntimeTypeHandle.GetAssembly(this);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004AF0F File Offset: 0x0004910F
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return new RuntimeTypeHandle(this);
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004AF0F File Offset: 0x0004910F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal sealed override RuntimeTypeHandle GetTypeHandleInternal()
		{
			return new RuntimeTypeHandle(this);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004AF17 File Offset: 0x00049117
		[SecuritySafeCritical]
		public override bool IsInstanceOfType(object o)
		{
			return RuntimeTypeHandle.IsInstanceOfType(this, o);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004AF20 File Offset: 0x00049120
		[ComVisible(true)]
		public override bool IsSubclassOf(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				return false;
			}
			RuntimeType runtimeType2 = this.GetBaseType();
			while (runtimeType2 != null)
			{
				if (runtimeType2 == runtimeType)
				{
					return true;
				}
				runtimeType2 = runtimeType2.GetBaseType();
			}
			return runtimeType == RuntimeType.ObjectType && runtimeType != this;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004AF8A File Offset: 0x0004918A
		public override bool IsAssignableFrom(global::System.Reflection.TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004AFA4 File Offset: 0x000491A4
		public override bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return true;
			}
			RuntimeType runtimeType = c.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return RuntimeTypeHandle.CanCastTo(runtimeType, this);
			}
			if (c is TypeBuilder)
			{
				if (c.IsSubclassOf(this))
				{
					return true;
				}
				if (base.IsInterface)
				{
					return c.ImplementInterface(this);
				}
				if (this.IsGenericParameter)
				{
					Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
					for (int i = 0; i < genericParameterConstraints.Length; i++)
					{
						if (!genericParameterConstraints[i].IsAssignableFrom(c))
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004B02C File Offset: 0x0004922C
		public override bool IsEquivalentTo(Type other)
		{
			RuntimeType runtimeType = other as RuntimeType;
			return runtimeType != null && (runtimeType == this || RuntimeTypeHandle.IsEquivalentTo(this, runtimeType));
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0004B057 File Offset: 0x00049257
		public override Type BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0004B060 File Offset: 0x00049260
		private RuntimeType GetBaseType()
		{
			if (base.IsInterface)
			{
				return null;
			}
			if (RuntimeTypeHandle.IsGenericVariable(this))
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				RuntimeType runtimeType = RuntimeType.ObjectType;
				foreach (RuntimeType runtimeType2 in genericParameterConstraints)
				{
					if (!runtimeType2.IsInterface)
					{
						if (runtimeType2.IsGenericParameter)
						{
							GenericParameterAttributes genericParameterAttributes = runtimeType2.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
							if ((genericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.None && (genericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.None)
							{
								goto IL_0055;
							}
						}
						runtimeType = runtimeType2;
					}
					IL_0055:;
				}
				if (runtimeType == RuntimeType.ObjectType && (this.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
				{
					runtimeType = RuntimeType.ValueType;
				}
				return runtimeType;
			}
			return RuntimeTypeHandle.GetBaseType(this);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00002119 File Offset: 0x00000319
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004B0F4 File Offset: 0x000492F4
		[SecuritySafeCritical]
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return RuntimeTypeHandle.GetAttributes(this);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004B0FC File Offset: 0x000492FC
		[SecuritySafeCritical]
		protected override bool IsContextfulImpl()
		{
			return RuntimeTypeHandle.IsContextful(this);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004B104 File Offset: 0x00049304
		protected override bool IsByRefImpl()
		{
			return RuntimeTypeHandle.IsByRef(this);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004B10C File Offset: 0x0004930C
		protected override bool IsPrimitiveImpl()
		{
			return RuntimeTypeHandle.IsPrimitive(this);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004B114 File Offset: 0x00049314
		protected override bool IsPointerImpl()
		{
			return RuntimeTypeHandle.IsPointer(this);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004B11C File Offset: 0x0004931C
		[SecuritySafeCritical]
		protected override bool IsCOMObjectImpl()
		{
			return RuntimeTypeHandle.IsComObject(this, false);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004B125 File Offset: 0x00049325
		[SecuritySafeCritical]
		internal override bool IsWindowsRuntimeObjectImpl()
		{
			return RuntimeType.IsWindowsRuntimeObjectType(this);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004B12D File Offset: 0x0004932D
		[SecuritySafeCritical]
		internal override bool IsExportedToWindowsRuntimeImpl()
		{
			return RuntimeType.IsTypeExportedToWindowsRuntime(this);
		}

		// Token: 0x0600125F RID: 4703
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsWindowsRuntimeObjectType(RuntimeType type);

		// Token: 0x06001260 RID: 4704
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTypeExportedToWindowsRuntime(RuntimeType type);

		// Token: 0x06001261 RID: 4705 RVA: 0x0004B135 File Offset: 0x00049335
		[SecuritySafeCritical]
		internal override bool HasProxyAttributeImpl()
		{
			return RuntimeTypeHandle.HasProxyAttribute(this);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004B13D File Offset: 0x0004933D
		internal bool IsDelegate()
		{
			return this.GetBaseType() == typeof(MulticastDelegate);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004B154 File Offset: 0x00049354
		protected override bool IsValueTypeImpl()
		{
			return !(this == typeof(ValueType)) && !(this == typeof(Enum)) && this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004B18C File Offset: 0x0004938C
		public override bool IsEnum
		{
			get
			{
				return this.GetBaseType() == RuntimeType.EnumType;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004B19E File Offset: 0x0004939E
		protected override bool HasElementTypeImpl()
		{
			return RuntimeTypeHandle.HasElementType(this);
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004B1A6 File Offset: 0x000493A6
		public override GenericParameterAttributes GenericParameterAttributes
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
				}
				return this.GetGenericParameterAttributes();
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0004B1C6 File Offset: 0x000493C6
		internal override bool IsSzArray
		{
			get
			{
				return RuntimeTypeHandle.IsSzArray(this);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004B1CE File Offset: 0x000493CE
		protected override bool IsArrayImpl()
		{
			return RuntimeTypeHandle.IsArray(this);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004B1D6 File Offset: 0x000493D6
		[SecuritySafeCritical]
		public override int GetArrayRank()
		{
			if (!this.IsArrayImpl())
			{
				throw new ArgumentException(Environment.GetResourceString("Must be an array type."));
			}
			return RuntimeTypeHandle.GetArrayRank(this);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004B1F6 File Offset: 0x000493F6
		public override Type GetElementType()
		{
			return RuntimeTypeHandle.GetElementType(this);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004B200 File Offset: 0x00049400
		public override string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			string[] array = Enum.InternalGetNames(this);
			string[] array2 = new string[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004B248 File Offset: 0x00049448
		[SecuritySafeCritical]
		public override Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			ulong[] array = Enum.InternalGetValues(this);
			Array array2 = Array.UnsafeCreateInstance(this, new int[] { array.Length });
			for (int i = 0; i < array.Length; i++)
			{
				object obj = Enum.ToObject(this, array[i]);
				array2.SetValue(obj, i);
			}
			return array2;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004B2AD File Offset: 0x000494AD
		public override Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			return Enum.InternalGetUnderlyingType(this);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004B2D4 File Offset: 0x000494D4
		public override bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			RuntimeType runtimeType = (RuntimeType)value.GetType();
			if (runtimeType.IsEnum)
			{
				if (!runtimeType.IsEquivalentTo(this))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						this.ToString()
					}));
				}
				runtimeType = (RuntimeType)runtimeType.GetEnumUnderlyingType();
			}
			if (runtimeType == RuntimeType.StringType)
			{
				return Array.IndexOf<object>(Enum.InternalGetNames(this), value) >= 0;
			}
			if (Type.IsIntegerType(runtimeType))
			{
				RuntimeType runtimeType2 = Enum.InternalGetUnderlyingType(this);
				if (runtimeType2 != runtimeType)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						runtimeType2.ToString()
					}));
				}
				ulong[] array = Enum.InternalGetValues(this);
				ulong num = Enum.ToUInt64(value);
				return Array.BinarySearch<ulong>(array, num) >= 0;
			}
			else
			{
				if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						this.GetEnumUnderlyingType()
					}));
				}
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004B3F8 File Offset: 0x000495F8
		public override string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
			ulong[] array = Enum.InternalGetValues(this);
			ulong num = Enum.ToUInt64(value);
			int num2 = Array.BinarySearch<ulong>(array, num);
			if (num2 >= 0)
			{
				return Enum.InternalGetNames(this)[num2];
			}
			return null;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004B461 File Offset: 0x00049661
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return (RuntimeType[])this.GetGenericArgumentsInternal(true);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004B470 File Offset: 0x00049670
		public override Type[] GetGenericArguments()
		{
			Type[] array = this.GetGenericArgumentsInternal(false);
			if (array == null)
			{
				array = EmptyArray<Type>.Value;
			}
			return array;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004B490 File Offset: 0x00049690
		[SecuritySafeCritical]
		public override Type MakeGenericType(params Type[] instantiation)
		{
			if (instantiation == null)
			{
				throw new ArgumentNullException("instantiation");
			}
			RuntimeType[] array = new RuntimeType[instantiation.Length];
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("{0} is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true.", new object[] { this }));
			}
			if (this.GetGenericArguments().Length != instantiation.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The number of generic arguments provided doesn't equal the arity of the generic type definition."), "instantiation");
			}
			for (int i = 0; i < instantiation.Length; i++)
			{
				Type type = instantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					Type[] array2 = new Type[instantiation.Length];
					for (int j = 0; j < instantiation.Length; j++)
					{
						array2[j] = instantiation[j];
					}
					instantiation = array2;
					return TypeBuilderInstantiation.MakeGenericType(this, instantiation);
				}
				array[i] = runtimeType;
			}
			RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
			RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
			Type type2 = RuntimeType.MakeGenericType(this, array);
			if (type2 == null)
			{
				throw new TypeLoadException();
			}
			return type2;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004B580 File Offset: 0x00049780
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return RuntimeTypeHandle.IsGenericTypeDefinition(this);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0004B588 File Offset: 0x00049788
		public override bool IsGenericParameter
		{
			get
			{
				return RuntimeTypeHandle.IsGenericVariable(this);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x0004B590 File Offset: 0x00049790
		public override int GenericParameterPosition
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
				}
				return this.GetGenericParameterPosition();
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004B5B0 File Offset: 0x000497B0
		public override Type GetGenericTypeDefinition()
		{
			if (!this.IsGenericType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This operation is only valid on generic types."));
			}
			return RuntimeTypeHandle.GetGenericTypeDefinition(this);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0004B5D0 File Offset: 0x000497D0
		public override bool IsGenericType
		{
			get
			{
				return RuntimeTypeHandle.HasInstantiation(this);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0004B5D8 File Offset: 0x000497D8
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.IsGenericType && !this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0004B5F0 File Offset: 0x000497F0
		public override MemberInfo[] GetDefaultMembers()
		{
			MemberInfo[] array = null;
			string defaultMemberName = this.GetDefaultMemberName();
			if (defaultMemberName != null)
			{
				array = base.GetMember(defaultMemberName);
			}
			if (array == null)
			{
				array = EmptyArray<MemberInfo>.Value;
			}
			return array;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004B61C File Offset: 0x0004981C
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object InvokeMember(string name, BindingFlags bindingFlags, Binder binder, object target, object[] providedArgs, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParams)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method must be called on a Type for which Type.IsGenericParameter is false."));
			}
			if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
			{
				throw new ArgumentException(Environment.GetResourceString("Must specify binding flags describing the invoke operation required (BindingFlags.InvokeMethod CreateInstance GetField SetField GetProperty SetProperty)."), "bindingFlags");
			}
			if ((bindingFlags & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingFlags |= BindingFlags.Instance | BindingFlags.Public;
				if ((bindingFlags & BindingFlags.CreateInstance) == BindingFlags.Default)
				{
					bindingFlags |= BindingFlags.Static;
				}
			}
			if (namedParams != null)
			{
				if (providedArgs != null)
				{
					if (namedParams.Length > providedArgs.Length)
					{
						throw new ArgumentException(Environment.GetResourceString("Named parameter array cannot be bigger than argument array."), "namedParams");
					}
				}
				else if (namedParams.Length != 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Named parameter array cannot be bigger than argument array."), "namedParams");
				}
			}
			if (target != null && target.GetType().IsCOMObject)
			{
				if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Must specify property Set or Get or method call for a COM Object."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a property."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a property and Invoke on a method."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.SetProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutRefDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutRefDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if (RemotingServices.IsTransparentProxy(target))
				{
					throw new NotImplementedException();
				}
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				throw new NotImplementedException();
			}
			else
			{
				if (namedParams != null && Array.IndexOf<string>(namedParams, null) != -1)
				{
					throw new ArgumentException(Environment.GetResourceString("Named parameter value must not be null."), "namedParams");
				}
				int num = ((providedArgs != null) ? providedArgs.Length : 0);
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default)
				{
					if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
					{
						throw new ArgumentException(Environment.GetResourceString("Cannot specify both CreateInstance and another access type."), "bindingFlags");
					}
					return Activator.CreateInstance(this, bindingFlags, binder, providedArgs, culture);
				}
				else
				{
					if ((bindingFlags & (BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) != BindingFlags.Default)
					{
						bindingFlags |= BindingFlags.SetProperty;
					}
					if (name == null)
					{
						throw new ArgumentNullException("name");
					}
					if (name.Length == 0 || name.Equals("[DISPID=0]"))
					{
						name = this.GetDefaultMemberName();
						if (name == null)
						{
							name = "ToString";
						}
					}
					bool flag = (bindingFlags & BindingFlags.GetField) > BindingFlags.Default;
					bool flag2 = (bindingFlags & BindingFlags.SetField) > BindingFlags.Default;
					if (flag || flag2)
					{
						if (flag)
						{
							if (flag2)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a field."), "bindingFlags");
							}
							if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both GetField and SetProperty."), "bindingFlags");
							}
						}
						else
						{
							if (providedArgs == null)
							{
								throw new ArgumentNullException("providedArgs");
							}
							if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both SetField and GetProperty."), "bindingFlags");
							}
							if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a Field and Invoke on a method."), "bindingFlags");
							}
						}
						FieldInfo fieldInfo = null;
						FieldInfo[] array = this.GetMember(name, MemberTypes.Field, bindingFlags) as FieldInfo[];
						if (array.Length == 1)
						{
							fieldInfo = array[0];
						}
						else if (array.Length != 0)
						{
							fieldInfo = binder.BindToField(bindingFlags, array, flag ? Empty.Value : providedArgs[0], culture);
						}
						if (fieldInfo != null)
						{
							if (fieldInfo.FieldType.IsArray || fieldInfo.FieldType == typeof(Array))
							{
								int num2;
								if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
								{
									num2 = num;
								}
								else
								{
									num2 = num - 1;
								}
								if (num2 > 0)
								{
									int[] array2 = new int[num2];
									for (int i = 0; i < num2; i++)
									{
										try
										{
											array2[i] = ((IConvertible)providedArgs[i]).ToInt32(null);
										}
										catch (InvalidCastException)
										{
											throw new ArgumentException(Environment.GetResourceString("All indexes must be of type Int32."));
										}
									}
									Array array3 = (Array)fieldInfo.GetValue(target);
									if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
									{
										return array3.GetValue(array2);
									}
									array3.SetValue(providedArgs[num2], array2);
									return null;
								}
							}
							if (flag)
							{
								if (num != 0)
								{
									throw new ArgumentException(Environment.GetResourceString("No arguments can be provided to Get a field value."), "bindingFlags");
								}
								return fieldInfo.GetValue(target);
							}
							else
							{
								if (num != 1)
								{
									throw new ArgumentException(Environment.GetResourceString("Only the field value can be specified to set a field value."), "bindingFlags");
								}
								fieldInfo.SetValue(target, providedArgs[0], bindingFlags, binder, culture);
								return null;
							}
						}
						else if ((bindingFlags & (BindingFlags)16773888) == BindingFlags.Default)
						{
							throw new MissingFieldException(this.FullName, name);
						}
					}
					bool flag3 = (bindingFlags & BindingFlags.GetProperty) > BindingFlags.Default;
					bool flag4 = (bindingFlags & BindingFlags.SetProperty) > BindingFlags.Default;
					if (flag3 || flag4)
					{
						if (flag3)
						{
							if (flag4)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a property."), "bindingFlags");
							}
						}
						else if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
						{
							throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a property and Invoke on a method."), "bindingFlags");
						}
					}
					MethodInfo[] array4 = null;
					MethodInfo methodInfo = null;
					if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
					{
						MethodInfo[] array5 = this.GetMember(name, MemberTypes.Method, bindingFlags) as MethodInfo[];
						List<MethodInfo> list = null;
						foreach (MethodInfo methodInfo2 in array5)
						{
							if (RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo2, bindingFlags, CallingConventions.Any, new Type[num]))
							{
								if (methodInfo == null)
								{
									methodInfo = methodInfo2;
								}
								else
								{
									if (list == null)
									{
										list = new List<MethodInfo>(array5.Length);
										list.Add(methodInfo);
									}
									list.Add(methodInfo2);
								}
							}
						}
						if (list != null)
						{
							array4 = new MethodInfo[list.Count];
							list.CopyTo(array4);
						}
					}
					if ((methodInfo == null && flag3) || flag4)
					{
						PropertyInfo[] array6 = this.GetMember(name, MemberTypes.Property, bindingFlags) as PropertyInfo[];
						List<MethodInfo> list2 = null;
						for (int k = 0; k < array6.Length; k++)
						{
							MethodInfo methodInfo3;
							if (flag4)
							{
								methodInfo3 = array6[k].GetSetMethod(true);
							}
							else
							{
								methodInfo3 = array6[k].GetGetMethod(true);
							}
							if (!(methodInfo3 == null) && RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo3, bindingFlags, CallingConventions.Any, new Type[num]))
							{
								if (methodInfo == null)
								{
									methodInfo = methodInfo3;
								}
								else
								{
									if (list2 == null)
									{
										list2 = new List<MethodInfo>(array6.Length);
										list2.Add(methodInfo);
									}
									list2.Add(methodInfo3);
								}
							}
						}
						if (list2 != null)
						{
							array4 = new MethodInfo[list2.Count];
							list2.CopyTo(array4);
						}
					}
					if (!(methodInfo != null))
					{
						throw new MissingMethodException(this.FullName, name);
					}
					if (array4 == null && num == 0 && methodInfo.GetParametersNoCopy().Length == 0 && (bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						return methodInfo.Invoke(target, bindingFlags, binder, providedArgs, culture);
					}
					if (array4 == null)
					{
						array4 = new MethodInfo[] { methodInfo };
					}
					if (providedArgs == null)
					{
						providedArgs = EmptyArray<object>.Value;
					}
					object obj = null;
					MethodBase methodBase = null;
					try
					{
						methodBase = binder.BindToMethod(bindingFlags, array4, ref providedArgs, modifiers, culture, namedParams, out obj);
					}
					catch (MissingMethodException)
					{
					}
					if (methodBase == null)
					{
						throw new MissingMethodException(this.FullName, name);
					}
					object obj2 = ((MethodInfo)methodBase).Invoke(target, bindingFlags, binder, providedArgs, culture);
					if (obj != null)
					{
						binder.ReorderArgumentArray(ref providedArgs, obj);
					}
					return obj2;
				}
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004BD58 File Offset: 0x00049F58
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		public static bool operator ==(RuntimeType left, RuntimeType right)
		{
			return left == right;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		public static bool operator !=(RuntimeType left, RuntimeType right)
		{
			return left != right;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00002119 File Offset: 0x00000319
		public object Clone()
		{
			return this;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004BD5E File Offset: 0x00049F5E
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, this);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004BD75 File Offset: 0x00049F75
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, RuntimeType.ObjectType, inherit);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004BD84 File Offset: 0x00049F84
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "attributeType");
			}
			return MonoCustomAttrs.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004BDD4 File Offset: 0x00049FD4
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "attributeType");
			}
			return MonoCustomAttrs.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004BE21 File Offset: 0x0004A021
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004BE2C File Offset: 0x0004A02C
		internal override string FormatTypeName(bool serialization)
		{
			if (serialization)
			{
				return this.GetCachedName(TypeNameKind.SerializationName);
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType.IsNested)
			{
				return this.Name;
			}
			string text = this.ToString();
			if (rootElementType.IsPrimitive || rootElementType == typeof(void) || rootElementType == typeof(TypedReference))
			{
				text = text.Substring("System.".Length);
			}
			return text;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0004BE9F File Offset: 0x0004A09F
		public override MemberTypes MemberType
		{
			get
			{
				if (base.IsPublic || base.IsNotPublic)
				{
					return MemberTypes.TypeInfo;
				}
				return MemberTypes.NestedType;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004BEB9 File Offset: 0x0004A0B9
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004BEC1 File Offset: 0x0004A0C1
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeTypeHandle.GetToken(this);
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004BECC File Offset: 0x0004A0CC
		private void CreateInstanceCheckThis()
		{
			if (this is ReflectionOnlyType)
			{
				throw new ArgumentException(Environment.GetResourceString("It is illegal to invoke a method on a Type loaded via ReflectionOnlyGetType."));
			}
			if (this.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Cannot create an instance of {0} because Type.ContainsGenericParameters is true.", new object[] { this }));
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType == typeof(ArgIterator))
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot dynamically create an instance of ArgIterator."));
			}
			if (rootElementType == typeof(void))
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot dynamically create an instance of System.Void."));
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004BF54 File Offset: 0x0004A154
		[SecurityCritical]
		internal object CreateInstanceImpl(BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, ref StackCrawlMark stackMark)
		{
			this.CreateInstanceCheckThis();
			object obj = null;
			try
			{
				try
				{
					if (activationAttributes != null)
					{
						ActivationServices.PushActivationAttributes(this, activationAttributes);
					}
					if (args == null)
					{
						args = EmptyArray<object>.Value;
					}
					int num = args.Length;
					if (binder == null)
					{
						binder = Type.DefaultBinder;
					}
					if (num == 0 && (bindingAttr & BindingFlags.Public) != BindingFlags.Default && (bindingAttr & BindingFlags.Instance) != BindingFlags.Default && (this.IsGenericCOMObjectImpl() || base.IsValueType))
					{
						obj = this.CreateInstanceDefaultCtor((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default, false, true, ref stackMark);
					}
					else
					{
						ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
						List<MethodBase> list = new List<MethodBase>(constructors.Length);
						Type[] array = new Type[num];
						for (int i = 0; i < num; i++)
						{
							if (args[i] != null)
							{
								array[i] = args[i].GetType();
							}
						}
						for (int j = 0; j < constructors.Length; j++)
						{
							if (RuntimeType.FilterApplyConstructorInfo((RuntimeConstructorInfo)constructors[j], bindingAttr, CallingConventions.Any, array))
							{
								list.Add(constructors[j]);
							}
						}
						MethodBase[] array2 = new MethodBase[list.Count];
						list.CopyTo(array2);
						if (array2 != null && array2.Length == 0)
						{
							array2 = null;
						}
						if (array2 == null)
						{
							if (activationAttributes != null)
							{
								ActivationServices.PopActivationAttributes(this);
								activationAttributes = null;
							}
							throw new MissingMethodException(Environment.GetResourceString("Constructor on type '{0}' not found.", new object[] { this.FullName }));
						}
						object obj2 = null;
						MethodBase methodBase;
						try
						{
							methodBase = binder.BindToMethod(bindingAttr, array2, ref args, null, culture, null, out obj2);
						}
						catch (MissingMethodException)
						{
							methodBase = null;
						}
						if (methodBase == null)
						{
							if (activationAttributes != null)
							{
								ActivationServices.PopActivationAttributes(this);
								activationAttributes = null;
							}
							throw new MissingMethodException(Environment.GetResourceString("Constructor on type '{0}' not found.", new object[] { this.FullName }));
						}
						if (methodBase.GetParametersNoCopy().Length == 0)
						{
							if (args.Length != 0)
							{
								throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Vararg calling convention not supported."), Array.Empty<object>()));
							}
							if (activationAttributes != null && activationAttributes.Length != 0)
							{
								obj = this.ActivationCreateInstance(methodBase, bindingAttr, binder, args, culture, activationAttributes);
							}
							else
							{
								obj = Activator.CreateInstance(this, true);
							}
						}
						else
						{
							if (activationAttributes != null && activationAttributes.Length != 0)
							{
								obj = this.ActivationCreateInstance(methodBase, bindingAttr, binder, args, culture, activationAttributes);
							}
							else
							{
								obj = ((ConstructorInfo)methodBase).Invoke(bindingAttr, binder, args, culture);
							}
							if (obj2 != null)
							{
								binder.ReorderArgumentArray(ref args, obj2);
							}
						}
					}
				}
				finally
				{
					if (activationAttributes != null)
					{
						ActivationServices.PopActivationAttributes(this);
						activationAttributes = null;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return obj;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
		private object ActivationCreateInstance(MethodBase invokeMethod, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			object obj = ActivationServices.CreateProxyFromAttributes(this, activationAttributes);
			if (obj != null)
			{
				invokeMethod.Invoke(obj, bindingAttr, binder, args, culture);
			}
			return obj;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0004C1EC File Offset: 0x0004A3EC
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object CreateInstanceDefaultCtor(bool publicOnly, bool skipCheckThis, bool fillCache, ref StackCrawlMark stackMark)
		{
			if (base.GetType() == typeof(ReflectionOnlyType))
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
			return this.CreateInstanceSlow(publicOnly, skipCheckThis, fillCache, ref stackMark);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0004C220 File Offset: 0x0004A420
		internal RuntimeType(object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0004C230 File Offset: 0x0004A430
		internal MonoCMethod GetDefaultConstructor()
		{
			MonoCMethod monoCMethod = null;
			if (this.type_info == null)
			{
				this.type_info = new MonoTypeInfo();
			}
			else
			{
				monoCMethod = this.type_info.default_ctor;
			}
			if (monoCMethod == null)
			{
				ConstructorInfo[] constructors = this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < constructors.Length; i++)
				{
					if (constructors[i].GetParametersCount() == 0)
					{
						monoCMethod = (this.type_info.default_ctor = (MonoCMethod)constructors[i]);
						break;
					}
				}
			}
			return monoCMethod;
		}

		// Token: 0x0600128E RID: 4750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetCorrespondingInflatedMethod(MethodInfo generic);

		// Token: 0x0600128F RID: 4751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ConstructorInfo GetCorrespondingInflatedConstructor(ConstructorInfo generic);

		// Token: 0x06001290 RID: 4752 RVA: 0x0004C2A2 File Offset: 0x0004A4A2
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedMethod(fromNoninstanciated);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0004C2BF File Offset: 0x0004A4BF
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedConstructor(fromNoninstanciated);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004C2DC File Offset: 0x0004A4DC
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			BindingFlags bindingFlags = (fromNoninstanciated.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
			bindingFlags |= (fromNoninstanciated.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return this.GetField(fromNoninstanciated.Name, bindingFlags);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004C314 File Offset: 0x0004A514
		private string GetDefaultMemberName()
		{
			object[] customAttributes = this.GetCustomAttributes(typeof(DefaultMemberAttribute), true);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return ((DefaultMemberAttribute)customAttributes[0]).MemberName;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0004C348 File Offset: 0x0004A548
		internal RuntimeConstructorInfo GetSerializationCtor()
		{
			if (this.m_serializationCtor == null)
			{
				Type[] array = new Type[]
				{
					typeof(SerializationInfo),
					typeof(StreamingContext)
				};
				this.m_serializationCtor = base.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, array, null) as RuntimeConstructorInfo;
			}
			return this.m_serializationCtor;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004C3A1 File Offset: 0x0004A5A1
		internal object CreateInstanceSlow(bool publicOnly, bool skipCheckThis, bool fillCache, ref StackCrawlMark stackMark)
		{
			if (!skipCheckThis)
			{
				this.CreateInstanceCheckThis();
			}
			return this.CreateInstanceMono(!publicOnly);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
		private object CreateInstanceMono(bool nonPublic)
		{
			MonoCMethod monoCMethod = this.GetDefaultConstructor();
			if (!nonPublic && monoCMethod != null && !monoCMethod.IsPublic)
			{
				monoCMethod = null;
			}
			if (monoCMethod == null)
			{
				Type rootElementType = base.GetRootElementType();
				if (rootElementType == typeof(TypedReference) || rootElementType == typeof(RuntimeArgumentHandle))
				{
					throw new NotSupportedException(Environment.GetResourceString("Cannot create boxed TypedReference, ArgIterator, or RuntimeArgumentHandle Objects."));
				}
				if (base.IsValueType)
				{
					return RuntimeType.CreateInstanceInternal(this);
				}
				throw new MissingMethodException(Locale.GetText("Default constructor not found for type " + this.FullName));
			}
			else
			{
				if (base.IsAbstract)
				{
					throw new MissingMethodException(Locale.GetText("Cannot create an abstract class '{0}'.", new object[] { this.FullName }));
				}
				return monoCMethod.InternalInvoke(null, null);
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004C478 File Offset: 0x0004A678
		internal object CheckValue(object value, Binder binder, CultureInfo culture, BindingFlags invokeAttr)
		{
			bool flag = false;
			object obj = this.TryConvertToType(value, ref flag);
			if (!flag)
			{
				return obj;
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.ExactBinding)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Object of type '{0}' cannot be converted to type '{1}'."), value.GetType(), this));
			}
			if (binder != null && binder != Type.DefaultBinder)
			{
				return binder.ChangeType(value, this, culture);
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Object of type '{0}' cannot be converted to type '{1}'."), value.GetType(), this));
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004C4FC File Offset: 0x0004A6FC
		private object TryConvertToType(object value, ref bool failed)
		{
			if (this.IsInstanceOfType(value))
			{
				return value;
			}
			if (base.IsByRef)
			{
				Type elementType = this.GetElementType();
				if (value == null || elementType.IsInstanceOfType(value))
				{
					return value;
				}
			}
			if (value == null)
			{
				return value;
			}
			if (this.IsEnum)
			{
				if (Enum.GetUnderlyingType(this) == value.GetType())
				{
					return value;
				}
				object obj = RuntimeType.IsConvertibleToPrimitiveType(value, this);
				if (obj != null)
				{
					return obj;
				}
			}
			else if (base.IsPrimitive)
			{
				object obj2 = RuntimeType.IsConvertibleToPrimitiveType(value, this);
				if (obj2 != null)
				{
					return obj2;
				}
			}
			else if (base.IsPointer)
			{
				Type type = value.GetType();
				if (type == typeof(IntPtr) || type == typeof(UIntPtr))
				{
					return value;
				}
			}
			failed = true;
			return null;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004C5B0 File Offset: 0x0004A7B0
		private static object IsConvertibleToPrimitiveType(object value, Type targetType)
		{
			Type type = value.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
				if (type == targetType)
				{
					return value;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (Type.GetTypeCode(targetType))
			{
			case TypeCode.Char:
				if (typeCode == TypeCode.Byte)
				{
					return (char)((byte)value);
				}
				if (typeCode == TypeCode.UInt16)
				{
					return value;
				}
				break;
			case TypeCode.Int16:
				if (typeCode == TypeCode.SByte)
				{
					return (short)((sbyte)value);
				}
				if (typeCode == TypeCode.Byte)
				{
					return (short)((byte)value);
				}
				break;
			case TypeCode.UInt16:
				if (typeCode == TypeCode.Char)
				{
					return value;
				}
				if (typeCode == TypeCode.Byte)
				{
					return (ushort)((byte)value);
				}
				break;
			case TypeCode.Int32:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (int)((char)value);
				case TypeCode.SByte:
					return (int)((sbyte)value);
				case TypeCode.Byte:
					return (int)((byte)value);
				case TypeCode.Int16:
					return (int)((short)value);
				case TypeCode.UInt16:
					return (int)((ushort)value);
				}
				break;
			case TypeCode.UInt32:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (uint)((char)value);
				case TypeCode.Byte:
					return (uint)((byte)value);
				case TypeCode.UInt16:
					return (uint)((ushort)value);
				}
				break;
			case TypeCode.Int64:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (long)((ulong)((char)value));
				case TypeCode.SByte:
					return (long)((sbyte)value);
				case TypeCode.Byte:
					return (long)((ulong)((byte)value));
				case TypeCode.Int16:
					return (long)((short)value);
				case TypeCode.UInt16:
					return (long)((ulong)((ushort)value));
				case TypeCode.Int32:
					return (long)((int)value);
				case TypeCode.UInt32:
					return (long)((ulong)((uint)value));
				}
				break;
			case TypeCode.UInt64:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (ulong)((char)value);
				case TypeCode.Byte:
					return (ulong)((byte)value);
				case TypeCode.UInt16:
					return (ulong)((ushort)value);
				case TypeCode.UInt32:
					return (ulong)((uint)value);
				}
				break;
			case TypeCode.Single:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (float)((char)value);
				case TypeCode.SByte:
					return (float)((sbyte)value);
				case TypeCode.Byte:
					return (float)((byte)value);
				case TypeCode.Int16:
					return (float)((short)value);
				case TypeCode.UInt16:
					return (float)((ushort)value);
				case TypeCode.Int32:
					return (float)((int)value);
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (float)((long)value);
				case TypeCode.UInt64:
					return (ulong)value;
				}
				break;
			case TypeCode.Double:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (double)((char)value);
				case TypeCode.SByte:
					return (double)((sbyte)value);
				case TypeCode.Byte:
					return (double)((byte)value);
				case TypeCode.Int16:
					return (double)((short)value);
				case TypeCode.UInt16:
					return (double)((ushort)value);
				case TypeCode.Int32:
					return (double)((int)value);
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (double)((long)value);
				case TypeCode.UInt64:
					return (ulong)value;
				case TypeCode.Single:
					return (double)((float)value);
				}
				break;
			}
			return null;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004C961 File Offset: 0x0004AB61
		private string GetCachedName(TypeNameKind kind)
		{
			if (kind == TypeNameKind.SerializationName)
			{
				return this.ToString();
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600129B RID: 4763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_array_type(int rank);

		// Token: 0x0600129C RID: 4764 RVA: 0x0004C973 File Offset: 0x0004AB73
		public override Type MakeArrayType()
		{
			return this.make_array_type(0);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004C97C File Offset: 0x0004AB7C
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1 || rank > 255)
			{
				throw new IndexOutOfRangeException();
			}
			return this.make_array_type(rank);
		}

		// Token: 0x0600129E RID: 4766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_byref_type();

		// Token: 0x0600129F RID: 4767 RVA: 0x0004C997 File Offset: 0x0004AB97
		public override Type MakeByRefType()
		{
			if (base.IsByRef)
			{
				throw new TypeLoadException("Can not call MakeByRefType on a ByRef type");
			}
			return this.make_byref_type();
		}

		// Token: 0x060012A0 RID: 4768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakePointerType(Type type);

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004C9B2 File Offset: 0x0004ABB2
		public override Type MakePointerType()
		{
			return RuntimeType.MakePointerType(this);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0004C9BA File Offset: 0x0004ABBA
		public override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				return StructLayoutAttribute.GetCustomAttribute(this);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0004C9C4 File Offset: 0x0004ABC4
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.IsGenericType)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return base.HasElementType && this.GetElementType().ContainsGenericParameters;
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004CA1C File Offset: 0x0004AC1C
		public override Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
			}
			RuntimeGenericParamInfoHandle runtimeGenericParamInfoHandle = new RuntimeGenericParamInfoHandle(RuntimeTypeHandle.GetGenericParameterInfo(this));
			Type[] array = runtimeGenericParamInfoHandle.Constraints;
			if (array == null)
			{
				array = EmptyArray<Type>.Value;
			}
			return array;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004CA60 File Offset: 0x0004AC60
		internal static object CreateInstanceForAnotherGenericParameter(Type genericType, RuntimeType genericArgument)
		{
			return ((RuntimeType)RuntimeType.MakeGenericType(genericType, new Type[] { genericArgument })).GetDefaultConstructor().InternalInvoke(null, null);
		}

		// Token: 0x060012A6 RID: 4774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakeGenericType(Type gt, Type[] types);

		// Token: 0x060012A7 RID: 4775
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetMethodsByName_native(IntPtr namePtr, BindingFlags bindingAttr, bool ignoreCase);

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004CA84 File Offset: 0x0004AC84
		internal RuntimeMethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeMethodInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetMethodsByName_native(safeStringMarshal.Value, bindingAttr, ignoreCase)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeMethodInfo[] array = new RuntimeMethodInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeMethodHandle runtimeMethodHandle = new RuntimeMethodHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeMethodInfo)MethodBase.GetMethodFromHandleNoGenericCheck(runtimeMethodHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x060012A9 RID: 4777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetPropertiesByName_native(IntPtr name, BindingFlags bindingAttr, bool icase);

		// Token: 0x060012AA RID: 4778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetConstructors_native(BindingFlags bindingAttr);

		// Token: 0x060012AB RID: 4779 RVA: 0x0004CB3C File Offset: 0x0004AD3C
		private RuntimeConstructorInfo[] GetConstructors_internal(BindingFlags bindingAttr, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeConstructorInfo[] array2;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetConstructors_native(bindingAttr)))
			{
				int length = safeGPtrArrayHandle.Length;
				RuntimeConstructorInfo[] array = new RuntimeConstructorInfo[length];
				for (int i = 0; i < length; i++)
				{
					RuntimeMethodHandle runtimeMethodHandle = new RuntimeMethodHandle(safeGPtrArrayHandle[i]);
					array[i] = (RuntimeConstructorInfo)MethodBase.GetMethodFromHandleNoGenericCheck(runtimeMethodHandle, runtimeTypeHandle);
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		private RuntimePropertyInfo[] GetPropertiesByName(string name, BindingFlags bindingAttr, bool icase, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimePropertyInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetPropertiesByName_native(safeStringMarshal.Value, bindingAttr, icase)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimePropertyInfo[] array = new RuntimePropertyInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimePropertyHandle runtimePropertyHandle = new RuntimePropertyHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimePropertyInfo)PropertyInfo.GetPropertyFromHandle(runtimePropertyHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004CC7C File Offset: 0x0004AE7C
		public override InterfaceMapping GetInterfaceMap(Type ifaceType)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method must be called on a Type for which Type.IsGenericParameter is false."));
			}
			if (ifaceType == null)
			{
				throw new ArgumentNullException("ifaceType");
			}
			if (ifaceType as RuntimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."), "ifaceType");
			}
			if (!ifaceType.IsInterface)
			{
				throw new ArgumentException(Locale.GetText("Argument must be an interface."), "ifaceType");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("'this' type cannot be an interface itself");
			}
			InterfaceMapping interfaceMapping;
			interfaceMapping.TargetType = this;
			interfaceMapping.InterfaceType = ifaceType;
			RuntimeType.GetInterfaceMapData(this, ifaceType, out interfaceMapping.TargetMethods, out interfaceMapping.InterfaceMethods);
			if (interfaceMapping.TargetMethods == null)
			{
				throw new ArgumentException(Locale.GetText("Interface not found"), "ifaceType");
			}
			return interfaceMapping;
		}

		// Token: 0x060012AE RID: 4782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterfaceMapData(Type t, Type iface, out MethodInfo[] targets, out MethodInfo[] methods);

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004CD48 File Offset: 0x0004AF48
		public override Guid GUID
		{
			get
			{
				object[] customAttributes = this.GetCustomAttributes(typeof(GuidAttribute), true);
				if (customAttributes.Length == 0)
				{
					return Guid.Empty;
				}
				return new Guid(((GuidAttribute)customAttributes[0]).Value);
			}
		}

		// Token: 0x060012B0 RID: 4784
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPacking(out int packing, out int size);

		// Token: 0x060012B1 RID: 4785 RVA: 0x0004CD84 File Offset: 0x0004AF84
		internal static Type GetTypeFromCLSIDImpl(Guid clsid, string server, bool throwOnError)
		{
			if (RuntimeType.clsid_types == null)
			{
				Dictionary<Guid, Type> dictionary = new Dictionary<Guid, Type>();
				Interlocked.CompareExchange<Dictionary<Guid, Type>>(ref RuntimeType.clsid_types, dictionary, null);
			}
			Dictionary<Guid, Type> dictionary2 = RuntimeType.clsid_types;
			Type type2;
			lock (dictionary2)
			{
				Type type;
				if (RuntimeType.clsid_types.TryGetValue(clsid, out type))
				{
					type2 = type;
				}
				else
				{
					if (RuntimeType.clsid_assemblybuilder == null)
					{
						AssemblyName assemblyName = new AssemblyName();
						assemblyName.Name = "GetTypeFromCLSIDDummyAssembly";
						RuntimeType.clsid_assemblybuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
					}
					TypeBuilder typeBuilder = RuntimeType.clsid_assemblybuilder.DefineDynamicModule(clsid.ToString()).DefineType("System.__ComObject", TypeAttributes.Public, typeof(__ComObject));
					Type[] array = new Type[] { typeof(string) };
					CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(typeof(GuidAttribute).GetConstructor(array), new object[] { clsid.ToString() });
					typeBuilder.SetCustomAttribute(customAttributeBuilder);
					customAttributeBuilder = new CustomAttributeBuilder(typeof(ComImportAttribute).GetConstructor(Type.EmptyTypes), new object[0]);
					typeBuilder.SetCustomAttribute(customAttributeBuilder);
					type = typeBuilder.CreateType();
					RuntimeType.clsid_types.Add(clsid, type);
					type2 = type;
				}
			}
			return type2;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004CEEC File Offset: 0x0004B0EC
		protected override TypeCode GetTypeCodeImpl()
		{
			return RuntimeType.GetTypeCodeImplInternal(this);
		}

		// Token: 0x060012B3 RID: 4787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TypeCode GetTypeCodeImplInternal(Type type);

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004CEF4 File Offset: 0x0004B0F4
		internal static Type GetTypeFromProgIDImpl(string progID, string server, bool throwOnError)
		{
			throw new NotImplementedException("Unmanaged activation is not supported");
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0004CF00 File Offset: 0x0004B100
		public override string ToString()
		{
			return this.getFullName(false, false);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00015ED5 File Offset: 0x000140D5
		private bool IsGenericCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x060012B7 RID: 4791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object CreateInstanceInternal(Type type);

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060012B8 RID: 4792
		public override extern MethodBase DeclaringMethod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060012B9 RID: 4793
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string getFullName(bool full_name, bool assembly_qualified);

		// Token: 0x060012BA RID: 4794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetGenericArgumentsInternal(bool runtimeArray);

		// Token: 0x060012BB RID: 4795 RVA: 0x0004CF0C File Offset: 0x0004B10C
		private GenericParameterAttributes GetGenericParameterAttributes()
		{
			return new RuntimeGenericParamInfoHandle(RuntimeTypeHandle.GetGenericParameterInfo(this)).Attributes;
		}

		// Token: 0x060012BC RID: 4796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetGenericParameterPosition();

		// Token: 0x060012BD RID: 4797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetEvents_native(IntPtr name, BindingFlags bindingAttr);

		// Token: 0x060012BE RID: 4798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetFields_native(IntPtr name, BindingFlags bindingAttr);

		// Token: 0x060012BF RID: 4799 RVA: 0x0004CF2C File Offset: 0x0004B12C
		private RuntimeFieldInfo[] GetFields_internal(string name, BindingFlags bindingAttr, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeFieldInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetFields_native(safeStringMarshal.Value, bindingAttr)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeFieldInfo[] array = new RuntimeFieldInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeFieldInfo)FieldInfo.GetFieldFromHandle(runtimeFieldHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004CFE0 File Offset: 0x0004B1E0
		private RuntimeEventInfo[] GetEvents_internal(string name, BindingFlags bindingAttr, RuntimeType reflectedType)
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(reflectedType);
			RuntimeEventInfo[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetEvents_native(safeStringMarshal.Value, bindingAttr)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeEventInfo[] array = new RuntimeEventInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeEventHandle runtimeEventHandle = new RuntimeEventHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeEventInfo)EventInfo.GetEventFromHandle(runtimeEventHandle, runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x060012C1 RID: 4801
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetInterfaces();

		// Token: 0x060012C2 RID: 4802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetNestedTypes_native(IntPtr name, BindingFlags bindingAttr);

		// Token: 0x060012C3 RID: 4803 RVA: 0x0004D094 File Offset: 0x0004B294
		private RuntimeType[] GetNestedTypes_internal(string displayName, BindingFlags bindingAttr)
		{
			string text = null;
			if (displayName != null)
			{
				text = TypeIdentifiers.FromDisplay(displayName).InternalName;
			}
			RuntimeType[] array2;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(text))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetNestedTypes_native(safeStringMarshal.Value, bindingAttr)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeType[] array = new RuntimeType[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeType)Type.GetTypeFromHandle(runtimeTypeHandle);
					}
					array2 = array;
				}
			}
			return array2;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004D150 File Offset: 0x0004B350
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.getFullName(true, true);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060012C5 RID: 4805
		public override extern Type DeclaringType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060012C6 RID: 4806
		public override extern string Name
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060012C7 RID: 4807
		public override extern string Namespace
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060012C8 RID: 4808
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004D15A File Offset: 0x0004B35A
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0004D165 File Offset: 0x0004B365
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0004D170 File Offset: 0x0004B370
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0004D17C File Offset: 0x0004B37C
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != null && underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return (int)this._impl.Value;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0004D1BC File Offset: 0x0004B3BC
		public override string FullName
		{
			get
			{
				if (this.IsGenericType && this.ContainsGenericParameters && !this.IsGenericTypeDefinition)
				{
					return null;
				}
				if (this.type_info == null)
				{
					this.type_info = new MonoTypeInfo();
				}
				string text;
				if ((text = this.type_info.full_name) == null)
				{
					text = (this.type_info.full_name = this.getFullName(true, false));
				}
				return text;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004D21D File Offset: 0x0004B41D
		public override bool IsSZArray
		{
			get
			{
				return base.IsArray && this == this.GetElementType().MakeArrayType();
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000AAE RID: 2734
		internal static readonly RuntimeType ValueType = (RuntimeType)typeof(ValueType);

		// Token: 0x04000AAF RID: 2735
		internal static readonly RuntimeType EnumType = (RuntimeType)typeof(Enum);

		// Token: 0x04000AB0 RID: 2736
		private static readonly RuntimeType ObjectType = (RuntimeType)typeof(object);

		// Token: 0x04000AB1 RID: 2737
		private static readonly RuntimeType StringType = (RuntimeType)typeof(string);

		// Token: 0x04000AB2 RID: 2738
		private static readonly RuntimeType DelegateType = (RuntimeType)typeof(Delegate);

		// Token: 0x04000AB3 RID: 2739
		private static Type[] s_SICtorParamTypes;

		// Token: 0x04000AB4 RID: 2740
		private const BindingFlags MemberBindingMask = (BindingFlags)255;

		// Token: 0x04000AB5 RID: 2741
		private const BindingFlags InvocationMask = BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x04000AB6 RID: 2742
		private const BindingFlags BinderNonCreateInstance = BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x04000AB7 RID: 2743
		private const BindingFlags BinderGetSetProperty = BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x04000AB8 RID: 2744
		private const BindingFlags BinderSetInvokeProperty = BindingFlags.InvokeMethod | BindingFlags.SetProperty;

		// Token: 0x04000AB9 RID: 2745
		private const BindingFlags BinderGetSetField = BindingFlags.GetField | BindingFlags.SetField;

		// Token: 0x04000ABA RID: 2746
		private const BindingFlags BinderSetInvokeField = BindingFlags.InvokeMethod | BindingFlags.SetField;

		// Token: 0x04000ABB RID: 2747
		private const BindingFlags BinderNonFieldGetSet = (BindingFlags)16773888;

		// Token: 0x04000ABC RID: 2748
		private const BindingFlags ClassicBindingMask = BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x04000ABD RID: 2749
		private static RuntimeType s_typedRef = (RuntimeType)typeof(TypedReference);

		// Token: 0x04000ABE RID: 2750
		[NonSerialized]
		private MonoTypeInfo type_info;

		// Token: 0x04000ABF RID: 2751
		internal object GenericCache;

		// Token: 0x04000AC0 RID: 2752
		private RuntimeConstructorInfo m_serializationCtor;

		// Token: 0x04000AC1 RID: 2753
		private static Dictionary<Guid, Type> clsid_types;

		// Token: 0x04000AC2 RID: 2754
		private static AssemblyBuilder clsid_assemblybuilder;

		// Token: 0x020001BA RID: 442
		internal enum MemberListType
		{
			// Token: 0x04000AC4 RID: 2756
			All,
			// Token: 0x04000AC5 RID: 2757
			CaseSensitive,
			// Token: 0x04000AC6 RID: 2758
			CaseInsensitive,
			// Token: 0x04000AC7 RID: 2759
			HandleToInfo
		}

		// Token: 0x020001BB RID: 443
		private struct ListBuilder<T> where T : class
		{
			// Token: 0x060012D1 RID: 4817 RVA: 0x0004D2BD File Offset: 0x0004B4BD
			public ListBuilder(int capacity)
			{
				this._items = null;
				this._item = default(T);
				this._count = 0;
				this._capacity = capacity;
			}

			// Token: 0x17000235 RID: 565
			public T this[int index]
			{
				get
				{
					if (this._items == null)
					{
						return this._item;
					}
					return this._items[index];
				}
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x0004D300 File Offset: 0x0004B500
			public T[] ToArray()
			{
				if (this._count == 0)
				{
					return EmptyArray<T>.Value;
				}
				if (this._count == 1)
				{
					return new T[] { this._item };
				}
				Array.Resize<T>(ref this._items, this._count);
				this._capacity = this._count;
				return this._items;
			}

			// Token: 0x060012D4 RID: 4820 RVA: 0x0004D35B File Offset: 0x0004B55B
			public void CopyTo(object[] array, int index)
			{
				if (this._count == 0)
				{
					return;
				}
				if (this._count == 1)
				{
					array[index] = this._item;
					return;
				}
				Array.Copy(this._items, 0, array, index, this._count);
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004D392 File Offset: 0x0004B592
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x060012D6 RID: 4822 RVA: 0x0004D39C File Offset: 0x0004B59C
			public void Add(T item)
			{
				if (this._count == 0)
				{
					this._item = item;
				}
				else
				{
					if (this._count == 1)
					{
						if (this._capacity < 2)
						{
							this._capacity = 4;
						}
						this._items = new T[this._capacity];
						this._items[0] = this._item;
					}
					else if (this._capacity == this._count)
					{
						int num = 2 * this._capacity;
						Array.Resize<T>(ref this._items, num);
						this._capacity = num;
					}
					this._items[this._count] = item;
				}
				this._count++;
			}

			// Token: 0x04000AC8 RID: 2760
			private T[] _items;

			// Token: 0x04000AC9 RID: 2761
			private T _item;

			// Token: 0x04000ACA RID: 2762
			private int _count;

			// Token: 0x04000ACB RID: 2763
			private int _capacity;
		}
	}
}
