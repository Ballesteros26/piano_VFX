using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Dynamic.Utils
{
	// Token: 0x02000346 RID: 838
	internal static class TypeUtils
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x00053238 File Offset: 0x00051438
		public static Type GetNonNullableType(this Type type)
		{
			if (!type.IsNullableType())
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005324C File Offset: 0x0005144C
		public static Type GetNullableType(this Type type)
		{
			if (type.IsValueType && !type.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00053279 File Offset: 0x00051479
		public static bool IsNullableType(this Type type)
		{
			return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005329A File Offset: 0x0005149A
		public static bool IsNullableOrReferenceType(this Type type)
		{
			return !type.IsValueType || type.IsNullableType();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000532AC File Offset: 0x000514AC
		public static bool IsBool(this Type type)
		{
			return type.GetNonNullableType() == typeof(bool);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000532C4 File Offset: 0x000514C4
		public static bool IsNumeric(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Char <= 10)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000532F4 File Offset: 0x000514F4
		public static bool IsInteger(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00053324 File Offset: 0x00051524
		public static bool IsInteger64(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Int64 <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00053354 File Offset: 0x00051554
		public static bool IsArithmetic(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Int16 <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00053384 File Offset: 0x00051584
		public static bool IsUnsignedInt(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				switch (type.GetTypeCode())
				{
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000533CC File Offset: 0x000515CC
		public static bool IsIntegerOrBool(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode == TypeCode.Boolean || typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000533FD File Offset: 0x000515FD
		public static bool IsNumericOrBool(this Type type)
		{
			return type.IsNumeric() || type.IsBool();
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00053410 File Offset: 0x00051610
		public static bool IsValidInstanceType(MemberInfo member, Type instanceType)
		{
			Type declaringType = member.DeclaringType;
			if (TypeUtils.AreReferenceAssignable(declaringType, instanceType))
			{
				return true;
			}
			if (declaringType == null)
			{
				return false;
			}
			if (instanceType.IsValueType)
			{
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(object)))
				{
					return true;
				}
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(ValueType)))
				{
					return true;
				}
				if (instanceType.IsEnum && TypeUtils.AreReferenceAssignable(declaringType, typeof(Enum)))
				{
					return true;
				}
				if (declaringType.IsInterface)
				{
					foreach (Type type in instanceType.GetTypeInfo().ImplementedInterfaces)
					{
						if (TypeUtils.AreReferenceAssignable(declaringType, type))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000534E0 File Offset: 0x000516E0
		public static bool HasIdentityPrimitiveOrNullableConversionTo(this Type source, Type dest)
		{
			return TypeUtils.AreEquivalent(source, dest) || (source.IsNullableType() && TypeUtils.AreEquivalent(dest, source.GetNonNullableType())) || (dest.IsNullableType() && TypeUtils.AreEquivalent(source, dest.GetNonNullableType())) || (source.IsConvertible() && dest.IsConvertible() && (dest.GetNonNullableType() != typeof(bool) || (source.IsEnum && source.GetEnumUnderlyingType() == typeof(bool))));
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00053574 File Offset: 0x00051774
		public static bool HasReferenceConversionTo(this Type source, Type dest)
		{
			if (source == typeof(void) || dest == typeof(void))
			{
				return false;
			}
			Type nonNullableType = source.GetNonNullableType();
			Type nonNullableType2 = dest.GetNonNullableType();
			return nonNullableType.IsAssignableFrom(nonNullableType2) || nonNullableType2.IsAssignableFrom(nonNullableType) || (source.IsInterface || dest.IsInterface) || TypeUtils.IsLegalExplicitVariantDelegateConversion(source, dest) || source == typeof(object) || dest == typeof(object);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0005360C File Offset: 0x0005180C
		private static bool IsCovariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Covariant) > GenericParameterAttributes.None;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00053619 File Offset: 0x00051819
		private static bool IsContravariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Contravariant) > GenericParameterAttributes.None;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00053626 File Offset: 0x00051826
		private static bool IsInvariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.VarianceMask) == GenericParameterAttributes.None;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00053633 File Offset: 0x00051833
		private static bool IsDelegate(Type t)
		{
			return t.IsSubclassOf(typeof(MulticastDelegate));
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00053648 File Offset: 0x00051848
		public static bool IsLegalExplicitVariantDelegateConversion(Type source, Type dest)
		{
			if (!TypeUtils.IsDelegate(source) || !TypeUtils.IsDelegate(dest) || !source.IsGenericType || !dest.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = source.GetGenericTypeDefinition();
			if (dest.GetGenericTypeDefinition() != genericTypeDefinition)
			{
				return false;
			}
			Type[] genericArguments = genericTypeDefinition.GetGenericArguments();
			Type[] genericArguments2 = source.GetGenericArguments();
			Type[] genericArguments3 = dest.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type = genericArguments2[i];
				Type type2 = genericArguments3[i];
				if (!TypeUtils.AreEquivalent(type, type2))
				{
					Type type3 = genericArguments[i];
					if (TypeUtils.IsInvariant(type3))
					{
						return false;
					}
					if (TypeUtils.IsCovariant(type3))
					{
						if (!type.HasReferenceConversionTo(type2))
						{
							return false;
						}
					}
					else if (TypeUtils.IsContravariant(type3) && (type.IsValueType || type2.IsValueType))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00053714 File Offset: 0x00051914
		public static bool IsConvertible(this Type type)
		{
			type = type.GetNonNullableType();
			if (type.IsEnum)
			{
				return true;
			}
			TypeCode typeCode = type.GetTypeCode();
			return typeCode - TypeCode.Boolean <= 11;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00053744 File Offset: 0x00051944
		public static bool HasReferenceEquality(Type left, Type right)
		{
			return !left.IsValueType && !right.IsValueType && (left.IsInterface || right.IsInterface || TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left));
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0005377C File Offset: 0x0005197C
		public static bool HasBuiltInEqualityOperator(Type left, Type right)
		{
			if (left.IsInterface && !right.IsValueType)
			{
				return true;
			}
			if (right.IsInterface && !left.IsValueType)
			{
				return true;
			}
			if (!left.IsValueType && !right.IsValueType && (TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left)))
			{
				return true;
			}
			if (!TypeUtils.AreEquivalent(left, right))
			{
				return false;
			}
			Type nonNullableType = left.GetNonNullableType();
			return nonNullableType == typeof(bool) || nonNullableType.IsNumeric() || nonNullableType.IsEnum;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00053805 File Offset: 0x00051A05
		public static bool IsImplicitlyConvertibleTo(this Type source, Type destination)
		{
			return TypeUtils.AreEquivalent(source, destination) || TypeUtils.IsImplicitNumericConversion(source, destination) || TypeUtils.IsImplicitReferenceConversion(source, destination) || TypeUtils.IsImplicitBoxingConversion(source, destination) || TypeUtils.IsImplicitNullableConversion(source, destination);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00053834 File Offset: 0x00051A34
		public static MethodInfo GetUserDefinedCoercionMethod(Type convertFrom, Type convertToType)
		{
			Type nonNullableType = convertFrom.GetNonNullableType();
			Type nonNullableType2 = convertToType.GetNonNullableType();
			MethodInfo[] methods = nonNullableType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			MethodInfo methodInfo = TypeUtils.FindConversionOperator(methods, convertFrom, convertToType);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			MethodInfo[] methods2 = nonNullableType2.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			methodInfo = TypeUtils.FindConversionOperator(methods2, convertFrom, convertToType);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (TypeUtils.AreEquivalent(nonNullableType, convertFrom) && TypeUtils.AreEquivalent(nonNullableType2, convertToType))
			{
				return null;
			}
			MethodInfo methodInfo2;
			if ((methodInfo2 = TypeUtils.FindConversionOperator(methods, nonNullableType, nonNullableType2)) == null && (methodInfo2 = TypeUtils.FindConversionOperator(methods2, nonNullableType, nonNullableType2)) == null)
			{
				methodInfo2 = TypeUtils.FindConversionOperator(methods, nonNullableType, convertToType) ?? TypeUtils.FindConversionOperator(methods2, nonNullableType, convertToType);
			}
			return methodInfo2;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000538D0 File Offset: 0x00051AD0
		private static MethodInfo FindConversionOperator(MethodInfo[] methods, Type typeFrom, Type typeTo)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if ((methodInfo.Name == "op_Implicit" || methodInfo.Name == "op_Explicit") && TypeUtils.AreEquivalent(methodInfo.ReturnType, typeTo))
				{
					ParameterInfo[] parametersCached = methodInfo.GetParametersCached();
					if (parametersCached.Length == 1 && TypeUtils.AreEquivalent(parametersCached[0].ParameterType, typeFrom))
					{
						return methodInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00053944 File Offset: 0x00051B44
		private static bool IsImplicitNumericConversion(Type source, Type destination)
		{
			TypeCode typeCode = source.GetTypeCode();
			TypeCode typeCode2 = destination.GetTypeCode();
			switch (typeCode)
			{
			case TypeCode.Char:
				if (typeCode2 - TypeCode.UInt16 <= 7)
				{
					return true;
				}
				break;
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Byte:
				if (typeCode2 - TypeCode.Int16 <= 8)
				{
					return true;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt16:
				if (typeCode2 - TypeCode.Int32 <= 6)
				{
					return true;
				}
				break;
			case TypeCode.Int32:
				if (typeCode2 == TypeCode.Int64 || typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.UInt32:
				if (typeCode2 - TypeCode.Int64 <= 4)
				{
					return true;
				}
				break;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				if (typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.Single:
				return typeCode2 == TypeCode.Double;
			}
			return false;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00053A2D File Offset: 0x00051C2D
		private static bool IsImplicitReferenceConversion(Type source, Type destination)
		{
			return destination.IsAssignableFrom(source);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00053A38 File Offset: 0x00051C38
		private static bool IsImplicitBoxingConversion(Type source, Type destination)
		{
			return (source.IsValueType && (destination == typeof(object) || destination == typeof(ValueType))) || (source.IsEnum && destination == typeof(Enum));
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00053A8D File Offset: 0x00051C8D
		private static bool IsImplicitNullableConversion(Type source, Type destination)
		{
			return destination.IsNullableType() && source.GetNonNullableType().IsImplicitlyConvertibleTo(destination.GetNonNullableType());
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00053AAC File Offset: 0x00051CAC
		public static Type FindGenericType(Type definition, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsConstructedGenericType && TypeUtils.AreEquivalent(type.GetGenericTypeDefinition(), definition))
				{
					return type;
				}
				if (definition.IsInterface)
				{
					foreach (Type type2 in type.GetTypeInfo().ImplementedInterfaces)
					{
						Type type3 = TypeUtils.FindGenericType(definition, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00053B50 File Offset: 0x00051D50
		public static MethodInfo GetBooleanOperator(Type type, string name)
		{
			MethodInfo anyStaticMethodValidated;
			for (;;)
			{
				anyStaticMethodValidated = type.GetAnyStaticMethodValidated(name, new Type[] { type });
				if (anyStaticMethodValidated != null && anyStaticMethodValidated.IsSpecialName && !anyStaticMethodValidated.ContainsGenericParameters)
				{
					break;
				}
				type = type.BaseType;
				if (!(type != null))
				{
					goto Block_3;
				}
			}
			return anyStaticMethodValidated;
			Block_3:
			return null;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00053B9C File Offset: 0x00051D9C
		public static Type GetNonRefType(this Type type)
		{
			if (!type.IsByRef)
			{
				return type;
			}
			return type.GetElementType();
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00053BAE File Offset: 0x00051DAE
		public static bool AreEquivalent(Type t1, Type t2)
		{
			return t1 != null && t1.IsEquivalentTo(t2);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00053BC2 File Offset: 0x00051DC2
		public static bool AreReferenceAssignable(Type dest, Type src)
		{
			return TypeUtils.AreEquivalent(dest, src) || (!dest.IsValueType && !src.IsValueType && dest.IsAssignableFrom(src));
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00053BE8 File Offset: 0x00051DE8
		public static bool IsSameOrSubclass(Type type, Type subType)
		{
			return TypeUtils.AreEquivalent(type, subType) || subType.IsSubclassOf(type);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00053BFC File Offset: 0x00051DFC
		public static void ValidateType(Type type, string paramName)
		{
			TypeUtils.ValidateType(type, paramName, false, false);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00053C07 File Offset: 0x00051E07
		public static void ValidateType(Type type, string paramName, bool allowByRef, bool allowPointer)
		{
			if (TypeUtils.ValidateType(type, paramName, -1))
			{
				if (!allowByRef && type.IsByRef)
				{
					throw Error.TypeMustNotBeByRef(paramName);
				}
				if (!allowPointer && type.IsPointer)
				{
					throw Error.TypeMustNotBePointer(paramName);
				}
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00053C37 File Offset: 0x00051E37
		public static bool ValidateType(Type type, string paramName, int index)
		{
			if (type == typeof(void))
			{
				return false;
			}
			if (type.ContainsGenericParameters)
			{
				throw type.IsGenericTypeDefinition ? Error.TypeIsGeneric(type, paramName, index) : Error.TypeContainsGenericParameters(type, paramName, index);
			}
			return true;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00053C71 File Offset: 0x00051E71
		private static Assembly MsCorLib
		{
			get
			{
				Assembly assembly;
				if ((assembly = TypeUtils.s_mscorlib) == null)
				{
					assembly = (TypeUtils.s_mscorlib = typeof(object).Assembly);
				}
				return assembly;
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00053C94 File Offset: 0x00051E94
		public static bool CanCache(this Type t)
		{
			if (t.Assembly != TypeUtils.MsCorLib)
			{
				return false;
			}
			if (t.IsGenericType)
			{
				Type[] genericArguments = t.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (!genericArguments[i].CanCache())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00053CDF File Offset: 0x00051EDF
		public static MethodInfo GetInvokeMethod(this Type delegateType)
		{
			return delegateType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00053CEE File Offset: 0x00051EEE
		internal static bool IsUnsigned(this Type type)
		{
			return type.GetNonNullableType().GetTypeCode().IsUnsigned();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00053D00 File Offset: 0x00051F00
		internal static bool IsUnsigned(this TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00053D33 File Offset: 0x00051F33
		internal static bool IsFloatingPoint(this Type type)
		{
			return type.GetNonNullableType().GetTypeCode().IsFloatingPoint();
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00053D45 File Offset: 0x00051F45
		internal static bool IsFloatingPoint(this TypeCode typeCode)
		{
			return typeCode - TypeCode.Single <= 1;
		}

		// Token: 0x04000B55 RID: 2901
		private static Assembly s_mscorlib;
	}
}
