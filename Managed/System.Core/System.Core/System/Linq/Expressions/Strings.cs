using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002B2 RID: 690
	internal static class Strings
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0003CFFD File Offset: 0x0003B1FD
		internal static string ReducibleMustOverrideReduce
		{
			get
			{
				return "reducible nodes must override Expression.Reduce()";
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0003D004 File Offset: 0x0003B204
		internal static string MustReduceToDifferent
		{
			get
			{
				return "node cannot reduce to itself or null";
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0003D00B File Offset: 0x0003B20B
		internal static string ReducedNotCompatible
		{
			get
			{
				return "cannot assign from the reduced node type to the original node type";
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0003D012 File Offset: 0x0003B212
		internal static string SetterHasNoParams
		{
			get
			{
				return "Setter must have parameters.";
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0003D019 File Offset: 0x0003B219
		internal static string PropertyCannotHaveRefType
		{
			get
			{
				return "Property cannot have a managed pointer type.";
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0003D020 File Offset: 0x0003B220
		internal static string IndexesOfSetGetMustMatch
		{
			get
			{
				return "Indexing parameters of getter and setter must match.";
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0003D027 File Offset: 0x0003B227
		internal static string AccessorsCannotHaveVarArgs
		{
			get
			{
				return "Accessor method should not have VarArgs.";
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0003D02E File Offset: 0x0003B22E
		internal static string AccessorsCannotHaveByRefArgs
		{
			get
			{
				return "Accessor indexes cannot be passed ByRef.";
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0003D035 File Offset: 0x0003B235
		internal static string BoundsCannotBeLessThanOne
		{
			get
			{
				return "Bounds count cannot be less than 1";
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0003D03C File Offset: 0x0003B23C
		internal static string TypeMustNotBeByRef
		{
			get
			{
				return "Type must not be ByRef";
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0003D043 File Offset: 0x0003B243
		internal static string TypeMustNotBePointer
		{
			get
			{
				return "Type must not be a pointer type";
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0003D04A File Offset: 0x0003B24A
		internal static string SetterMustBeVoid
		{
			get
			{
				return "Setter should have void type.";
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0003D051 File Offset: 0x0003B251
		internal static string PropertyTypeMustMatchGetter
		{
			get
			{
				return "Property type must match the value type of getter";
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0003D058 File Offset: 0x0003B258
		internal static string PropertyTypeMustMatchSetter
		{
			get
			{
				return "Property type must match the value type of setter";
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0003D05F File Offset: 0x0003B25F
		internal static string BothAccessorsMustBeStatic
		{
			get
			{
				return "Both accessors must be static.";
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0003D066 File Offset: 0x0003B266
		internal static string OnlyStaticFieldsHaveNullInstance
		{
			get
			{
				return "Static field requires null instance, non-static field requires non-null instance.";
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0003D06D File Offset: 0x0003B26D
		internal static string OnlyStaticPropertiesHaveNullInstance
		{
			get
			{
				return "Static property requires null instance, non-static property requires non-null instance.";
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0003D074 File Offset: 0x0003B274
		internal static string OnlyStaticMethodsHaveNullInstance
		{
			get
			{
				return "Static method requires null instance, non-static method requires non-null instance.";
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0003D07B File Offset: 0x0003B27B
		internal static string PropertyTypeCannotBeVoid
		{
			get
			{
				return "Property cannot have a void type.";
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0003D082 File Offset: 0x0003B282
		internal static string InvalidUnboxType
		{
			get
			{
				return "Can only unbox from an object or interface type to a value type.";
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0003D089 File Offset: 0x0003B289
		internal static string ExpressionMustBeWriteable
		{
			get
			{
				return "Expression must be writeable";
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0003D090 File Offset: 0x0003B290
		internal static string ArgumentMustNotHaveValueType
		{
			get
			{
				return "Argument must not have a value type.";
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0003D097 File Offset: 0x0003B297
		internal static string MustBeReducible
		{
			get
			{
				return "must be reducible node";
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0003D09E File Offset: 0x0003B29E
		internal static string AllTestValuesMustHaveSameType
		{
			get
			{
				return "All test values must have the same type.";
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0003D0A5 File Offset: 0x0003B2A5
		internal static string AllCaseBodiesMustHaveSameType
		{
			get
			{
				return "All case bodies and the default body must have the same type.";
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		internal static string DefaultBodyMustBeSupplied
		{
			get
			{
				return "Default body must be supplied if case bodies are not System.Void.";
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003D0B3 File Offset: 0x0003B2B3
		internal static string LabelMustBeVoidOrHaveExpression
		{
			get
			{
				return "Label type must be System.Void if an expression is not supplied";
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0003D0BA File Offset: 0x0003B2BA
		internal static string LabelTypeMustBeVoid
		{
			get
			{
				return "Type must be System.Void for this label argument";
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003D0C1 File Offset: 0x0003B2C1
		internal static string QuotedExpressionMustBeLambda
		{
			get
			{
				return "Quoted expression must be a lambda";
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
		internal static string CollectionModifiedWhileEnumerating
		{
			get
			{
				return "Collection was modified; enumeration operation may not execute.";
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003D0CF File Offset: 0x0003B2CF
		internal static string VariableMustNotBeByRef(object p0, object p1)
		{
			return global::SR.Format("Variable '{0}' uses unsupported type '{1}'. Reference types are not supported for variables.", p0, p1);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0003D0DD File Offset: 0x0003B2DD
		internal static string CollectionReadOnly
		{
			get
			{
				return "Collection is read-only.";
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0003D0E4 File Offset: 0x0003B2E4
		internal static string AmbiguousMatchInExpandoObject(object p0)
		{
			return global::SR.Format("More than one key matching '{0}' was found in the ExpandoObject.", p0);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0003D0F1 File Offset: 0x0003B2F1
		internal static string SameKeyExistsInExpando(object p0)
		{
			return global::SR.Format("An element with the same key '{0}' already exists in the ExpandoObject.", p0);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003D0FE File Offset: 0x0003B2FE
		internal static string KeyDoesNotExistInExpando(object p0)
		{
			return global::SR.Format("The specified key '{0}' does not exist in the ExpandoObject.", p0);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0003D10B File Offset: 0x0003B30B
		internal static string ArgCntMustBeGreaterThanNameCnt
		{
			get
			{
				return "Argument count must be greater than number of named arguments.";
			}
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0003D112 File Offset: 0x0003B312
		internal static string InvalidMetaObjectCreated(object p0)
		{
			return global::SR.Format("An IDynamicMetaObjectProvider {0} created an invalid DynamicMetaObject instance.", p0);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0003D11F File Offset: 0x0003B31F
		internal static string BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return global::SR.Format("The result type '{0}' of the binder '{1}' is not compatible with the result type '{2}' expected by the call site.", p0, p1, p2);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0003D12E File Offset: 0x0003B32E
		internal static string DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return global::SR.Format("The result of the dynamic binding produced by the object with type '{0}' for the binder '{1}' needs at least one restriction.", p0, p1);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0003D13C File Offset: 0x0003B33C
		internal static string DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return global::SR.Format("The result type '{0}' of the dynamic binding produced by the object with type '{1}' for the binder '{2}' is not compatible with the result type '{3}' expected by the call site.", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0003D15E File Offset: 0x0003B35E
		internal static string DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return global::SR.Format("The result type '{0}' of the dynamic binding produced by binder '{1}' is not compatible with the result type '{2}' expected by the call site.", p0, p1, p2);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0003D16D File Offset: 0x0003B36D
		internal static string BindingCannotBeNull
		{
			get
			{
				return "Bind cannot return null.";
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0003D174 File Offset: 0x0003B374
		internal static string DuplicateVariable(object p0)
		{
			return global::SR.Format("Found duplicate parameter '{0}'. Each ParameterExpression in the list must be a unique object.", p0);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0003D181 File Offset: 0x0003B381
		internal static string ArgumentTypeCannotBeVoid
		{
			get
			{
				return "Argument type cannot be void";
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0003D188 File Offset: 0x0003B388
		internal static string TypeParameterIsNotDelegate(object p0)
		{
			return global::SR.Format("Type parameter is {0}. Expected a delegate.", p0);
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0003D195 File Offset: 0x0003B395
		internal static string NoOrInvalidRuleProduced
		{
			get
			{
				return "No or Invalid rule produced";
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0003D19C File Offset: 0x0003B39C
		internal static string TypeMustBeDerivedFromSystemDelegate
		{
			get
			{
				return "Type must be derived from System.Delegate";
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0003D1A3 File Offset: 0x0003B3A3
		internal static string FirstArgumentMustBeCallSite
		{
			get
			{
				return "First argument of delegate must be CallSite";
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0003D1AA File Offset: 0x0003B3AA
		internal static string StartEndMustBeOrdered
		{
			get
			{
				return "Start and End must be well ordered";
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0003D1B1 File Offset: 0x0003B3B1
		internal static string FaultCannotHaveCatchOrFinally
		{
			get
			{
				return "fault cannot be used with catch or finally clauses";
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		internal static string TryMustHaveCatchFinallyOrFault
		{
			get
			{
				return "try must have at least one catch, finally, or fault clause";
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0003D1BF File Offset: 0x0003B3BF
		internal static string BodyOfCatchMustHaveSameTypeAsBodyOfTry
		{
			get
			{
				return "Body of catch must have the same type as body of try.";
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0003D1C6 File Offset: 0x0003B3C6
		internal static string ExtensionNodeMustOverrideProperty(object p0)
		{
			return global::SR.Format("Extension node must override the property {0}.", p0);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0003D1D3 File Offset: 0x0003B3D3
		internal static string UserDefinedOperatorMustBeStatic(object p0)
		{
			return global::SR.Format("User-defined operator method '{0}' must be static.", p0);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0003D1E0 File Offset: 0x0003B3E0
		internal static string UserDefinedOperatorMustNotBeVoid(object p0)
		{
			return global::SR.Format("User-defined operator method '{0}' must not be void.", p0);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0003D1ED File Offset: 0x0003B3ED
		internal static string CoercionOperatorNotDefined(object p0, object p1)
		{
			return global::SR.Format("No coercion operator is defined between types '{0}' and '{1}'.", p0, p1);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0003D1FB File Offset: 0x0003B3FB
		internal static string UnaryOperatorNotDefined(object p0, object p1)
		{
			return global::SR.Format("The unary operator {0} is not defined for the type '{1}'.", p0, p1);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003D209 File Offset: 0x0003B409
		internal static string BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return global::SR.Format("The binary operator {0} is not defined for the types '{1}' and '{2}'.", p0, p1, p2);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0003D218 File Offset: 0x0003B418
		internal static string ReferenceEqualityNotDefined(object p0, object p1)
		{
			return global::SR.Format("Reference equality is not defined for the types '{0}' and '{1}'.", p0, p1);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0003D226 File Offset: 0x0003B426
		internal static string OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return global::SR.Format("The operands for operator '{0}' do not match the parameters of method '{1}'.", p0, p1);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0003D234 File Offset: 0x0003B434
		internal static string OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return global::SR.Format("The return type of overload method for operator '{0}' does not match the parameter type of conversion method '{1}'.", p0, p1);
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0003D242 File Offset: 0x0003B442
		internal static string ConversionIsNotSupportedForArithmeticTypes
		{
			get
			{
				return "Conversion is not supported for arithmetic types without operator overloading.";
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0003D249 File Offset: 0x0003B449
		internal static string ArgumentMustBeArray
		{
			get
			{
				return "Argument must be array";
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0003D250 File Offset: 0x0003B450
		internal static string ArgumentMustBeBoolean
		{
			get
			{
				return "Argument must be boolean";
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0003D257 File Offset: 0x0003B457
		internal static string EqualityMustReturnBoolean(object p0)
		{
			return global::SR.Format("The user-defined equality method '{0}' must return a boolean value.", p0);
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0003D264 File Offset: 0x0003B464
		internal static string ArgumentMustBeFieldInfoOrPropertyInfo
		{
			get
			{
				return "Argument must be either a FieldInfo or PropertyInfo";
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003D26B File Offset: 0x0003B46B
		internal static string ArgumentMustBeFieldInfoOrPropertyInfoOrMethod
		{
			get
			{
				return "Argument must be either a FieldInfo, PropertyInfo or MethodInfo";
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0003D272 File Offset: 0x0003B472
		internal static string ArgumentMustBeInstanceMember
		{
			get
			{
				return "Argument must be an instance member";
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0003D279 File Offset: 0x0003B479
		internal static string ArgumentMustBeInteger
		{
			get
			{
				return "Argument must be of an integer type";
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0003D280 File Offset: 0x0003B480
		internal static string ArgumentMustBeArrayIndexType
		{
			get
			{
				return "Argument for array index must be of type Int32";
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0003D287 File Offset: 0x0003B487
		internal static string ArgumentMustBeSingleDimensionalArrayType
		{
			get
			{
				return "Argument must be single-dimensional, zero-based array type";
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0003D28E File Offset: 0x0003B48E
		internal static string ArgumentTypesMustMatch
		{
			get
			{
				return "Argument types do not match";
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0003D295 File Offset: 0x0003B495
		internal static string CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return global::SR.Format("Cannot auto initialize elements of value type through property '{0}', use assignment instead", p0);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0003D2A2 File Offset: 0x0003B4A2
		internal static string CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return global::SR.Format("Cannot auto initialize members of value type through property '{0}', use assignment instead", p0);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0003D2AF File Offset: 0x0003B4AF
		internal static string IncorrectTypeForTypeAs(object p0)
		{
			return global::SR.Format("The type used in TypeAs Expression must be of reference or nullable type, {0} is neither", p0);
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0003D2BC File Offset: 0x0003B4BC
		internal static string CoalesceUsedOnNonNullType
		{
			get
			{
				return "Coalesce used with type that cannot be null";
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003D2C3 File Offset: 0x0003B4C3
		internal static string ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return global::SR.Format("An expression of type '{0}' cannot be used to initialize an array of type '{1}'", p0, p1);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003D2D1 File Offset: 0x0003B4D1
		internal static string ArgumentTypeDoesNotMatchMember(object p0, object p1)
		{
			return global::SR.Format(" Argument type '{0}' does not match the corresponding member type '{1}'", p0, p1);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003D2DF File Offset: 0x0003B4DF
		internal static string ArgumentMemberNotDeclOnType(object p0, object p1)
		{
			return global::SR.Format(" The member '{0}' is not declared on type '{1}' being created", p0, p1);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003D2ED File Offset: 0x0003B4ED
		internal static string ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for return type '{1}'", p0, p1);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0003D2FB File Offset: 0x0003B4FB
		internal static string ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for assignment to type '{1}'", p0, p1);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0003D309 File Offset: 0x0003B509
		internal static string ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for label of type '{1}'", p0, p1);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0003D317 File Offset: 0x0003B517
		internal static string ExpressionTypeNotInvocable(object p0)
		{
			return global::SR.Format("Expression of type '{0}' cannot be invoked", p0);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0003D324 File Offset: 0x0003B524
		internal static string FieldNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Field '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003D332 File Offset: 0x0003B532
		internal static string InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Instance field '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0003D340 File Offset: 0x0003B540
		internal static string FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return global::SR.Format("Field '{0}.{1}' is not defined for type '{2}'", p0, p1, p2);
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0003D34F File Offset: 0x0003B54F
		internal static string IncorrectNumberOfIndexes
		{
			get
			{
				return "Incorrect number of indexes";
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0003D356 File Offset: 0x0003B556
		internal static string IncorrectNumberOfLambdaDeclarationParameters
		{
			get
			{
				return "Incorrect number of parameters supplied for lambda declaration";
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0003D35D File Offset: 0x0003B55D
		internal static string IncorrectNumberOfMembersForGivenConstructor
		{
			get
			{
				return " Incorrect number of members for constructor";
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0003D364 File Offset: 0x0003B564
		internal static string IncorrectNumberOfArgumentsForMembers
		{
			get
			{
				return "Incorrect number of arguments for the given members ";
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0003D36B File Offset: 0x0003B56B
		internal static string LambdaTypeMustBeDerivedFromSystemDelegate
		{
			get
			{
				return "Lambda type parameter must be derived from System.MulticastDelegate";
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0003D372 File Offset: 0x0003B572
		internal static string MemberNotFieldOrProperty(object p0)
		{
			return global::SR.Format("Member '{0}' not field or property", p0);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0003D37F File Offset: 0x0003B57F
		internal static string MethodContainsGenericParameters(object p0)
		{
			return global::SR.Format("Method {0} contains generic parameters", p0);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0003D38C File Offset: 0x0003B58C
		internal static string MethodIsGeneric(object p0)
		{
			return global::SR.Format("Method {0} is a generic method definition", p0);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0003D399 File Offset: 0x0003B599
		internal static string MethodNotPropertyAccessor(object p0, object p1)
		{
			return global::SR.Format("The method '{0}.{1}' is not a property accessor", p0, p1);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0003D3A7 File Offset: 0x0003B5A7
		internal static string PropertyDoesNotHaveGetter(object p0)
		{
			return global::SR.Format("The property '{0}' has no 'get' accessor", p0);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		internal static string PropertyDoesNotHaveSetter(object p0)
		{
			return global::SR.Format("The property '{0}' has no 'set' accessor", p0);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0003D3C1 File Offset: 0x0003B5C1
		internal static string PropertyDoesNotHaveAccessor(object p0)
		{
			return global::SR.Format("The property '{0}' has no 'get' or 'set' accessors", p0);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0003D3CE File Offset: 0x0003B5CE
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return global::SR.Format("'{0}' is not a member of type '{1}'", p0, p1);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0003D3DC File Offset: 0x0003B5DC
		internal static string NotAMemberOfAnyType(object p0)
		{
			return global::SR.Format("'{0}' is not a member of any type", p0);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0003D3E9 File Offset: 0x0003B5E9
		internal static string ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return global::SR.Format("ParameterExpression of type '{0}' cannot be used for delegate parameter of type '{1}'", p0, p1);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0003D3F7 File Offset: 0x0003B5F7
		internal static string PropertyNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Property '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0003D405 File Offset: 0x0003B605
		internal static string InstancePropertyNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Instance property '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0003D413 File Offset: 0x0003B613
		internal static string InstancePropertyWithoutParameterNotDefinedForType(object p0, object p1)
		{
			return global::SR.Format("Instance property '{0}' that takes no argument is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0003D421 File Offset: 0x0003B621
		internal static string InstancePropertyWithSpecifiedParametersNotDefinedForType(object p0, object p1, object p2)
		{
			return global::SR.Format("Instance property '{0}{1}' is not defined for type '{2}'", p0, p1, p2);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0003D430 File Offset: 0x0003B630
		internal static string InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return global::SR.Format("Method '{0}' declared on type '{1}' cannot be called with instance of type '{2}'", p0, p1, p2);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0003D43F File Offset: 0x0003B63F
		internal static string TypeMissingDefaultConstructor(object p0)
		{
			return global::SR.Format("Type '{0}' does not have a default constructor", p0);
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0003D44C File Offset: 0x0003B64C
		internal static string ElementInitializerMethodNotAdd
		{
			get
			{
				return "Element initializer method must be named 'Add'";
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0003D453 File Offset: 0x0003B653
		internal static string ElementInitializerMethodNoRefOutParam(object p0, object p1)
		{
			return global::SR.Format("Parameter '{0}' of element initializer method '{1}' must not be a pass by reference parameter", p0, p1);
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0003D461 File Offset: 0x0003B661
		internal static string ElementInitializerMethodWithZeroArgs
		{
			get
			{
				return "Element initializer method must have at least 1 parameter";
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0003D468 File Offset: 0x0003B668
		internal static string ElementInitializerMethodStatic
		{
			get
			{
				return "Element initializer method must be an instance method";
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0003D46F File Offset: 0x0003B66F
		internal static string TypeNotIEnumerable(object p0)
		{
			return global::SR.Format("Type '{0}' is not IEnumerable", p0);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0003D47C File Offset: 0x0003B67C
		internal static string UnhandledBinary(object p0)
		{
			return global::SR.Format("Unhandled binary: {0}", p0);
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0003D489 File Offset: 0x0003B689
		internal static string UnhandledBinding
		{
			get
			{
				return "Unhandled binding ";
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0003D490 File Offset: 0x0003B690
		internal static string UnhandledBindingType(object p0)
		{
			return global::SR.Format("Unhandled Binding Type: {0}", p0);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0003D49D File Offset: 0x0003B69D
		internal static string UnhandledUnary(object p0)
		{
			return global::SR.Format("Unhandled unary: {0}", p0);
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0003D4AA File Offset: 0x0003B6AA
		internal static string UnknownBindingType
		{
			get
			{
				return "Unknown binding type";
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0003D4B1 File Offset: 0x0003B6B1
		internal static string UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return global::SR.Format("The user-defined operator method '{1}' for operator '{0}' must have identical parameter and return types.", p0, p1);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0003D4BF File Offset: 0x0003B6BF
		internal static string UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return global::SR.Format("The user-defined operator method '{1}' for operator '{0}' must return the same type as its parameter or a derived type.", p0, p1);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0003D4CD File Offset: 0x0003B6CD
		internal static string LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return global::SR.Format("The user-defined operator method '{1}' for operator '{0}' must have associated boolean True and False operators.", p0, p1);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003D4DB File Offset: 0x0003B6DB
		internal static string MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return global::SR.Format("No method '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0003D4E9 File Offset: 0x0003B6E9
		internal static string GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return global::SR.Format("No generic method '{0}' on type '{1}' is compatible with the supplied type arguments and arguments. No type arguments should be provided if the method is non-generic. ", p0, p1);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0003D4F7 File Offset: 0x0003B6F7
		internal static string MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return global::SR.Format("More than one method '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0003D505 File Offset: 0x0003B705
		internal static string PropertyWithMoreThanOneMatch(object p0, object p1)
		{
			return global::SR.Format("More than one property '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0003D513 File Offset: 0x0003B713
		internal static string IncorrectNumberOfTypeArgsForFunc
		{
			get
			{
				return "An incorrect number of type arguments were specified for the declaration of a Func type.";
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0003D51A File Offset: 0x0003B71A
		internal static string IncorrectNumberOfTypeArgsForAction
		{
			get
			{
				return "An incorrect number of type arguments were specified for the declaration of an Action type.";
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0003D521 File Offset: 0x0003B721
		internal static string ArgumentCannotBeOfTypeVoid
		{
			get
			{
				return "Argument type cannot be System.Void.";
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0003D528 File Offset: 0x0003B728
		internal static string OutOfRange(object p0, object p1)
		{
			return global::SR.Format("{0} must be greater than or equal to {1}", p0, p1);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0003D536 File Offset: 0x0003B736
		internal static string LabelTargetAlreadyDefined(object p0)
		{
			return global::SR.Format("Cannot redefine label '{0}' in an inner block.", p0);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003D543 File Offset: 0x0003B743
		internal static string LabelTargetUndefined(object p0)
		{
			return global::SR.Format("Cannot jump to undefined label '{0}'.", p0);
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0003D550 File Offset: 0x0003B750
		internal static string ControlCannotLeaveFinally
		{
			get
			{
				return "Control cannot leave a finally block.";
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0003D557 File Offset: 0x0003B757
		internal static string ControlCannotLeaveFilterTest
		{
			get
			{
				return "Control cannot leave a filter test.";
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0003D55E File Offset: 0x0003B75E
		internal static string AmbiguousJump(object p0)
		{
			return global::SR.Format("Cannot jump to ambiguous label '{0}'.", p0);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0003D56B File Offset: 0x0003B76B
		internal static string ControlCannotEnterTry
		{
			get
			{
				return "Control cannot enter a try block.";
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0003D572 File Offset: 0x0003B772
		internal static string ControlCannotEnterExpression
		{
			get
			{
				return "Control cannot enter an expression--only statements can be jumped into.";
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0003D579 File Offset: 0x0003B779
		internal static string NonLocalJumpWithValue(object p0)
		{
			return global::SR.Format("Cannot jump to non-local label '{0}' with a value. Only jumps to labels defined in outer blocks can pass values.", p0);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0003D586 File Offset: 0x0003B786
		internal static string CannotCompileConstant(object p0)
		{
			return global::SR.Format("CompileToMethod cannot compile constant '{0}' because it is a non-trivial value, such as a live object. Instead, create an expression tree that can construct this value.", p0);
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0003D593 File Offset: 0x0003B793
		internal static string CannotCompileDynamic
		{
			get
			{
				return "Dynamic expressions are not supported by CompileToMethod. Instead, create an expression tree that uses System.Runtime.CompilerServices.CallSite.";
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0003D59A File Offset: 0x0003B79A
		internal static string MethodBuilderDoesNotHaveTypeBuilder
		{
			get
			{
				return "MethodBuilder does not have a valid TypeBuilder";
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0003D5A1 File Offset: 0x0003B7A1
		internal static string InvalidLvalue(object p0)
		{
			return global::SR.Format("Invalid lvalue for assignment: {0}.", p0);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0003D5AE File Offset: 0x0003B7AE
		internal static string UndefinedVariable(object p0, object p1, object p2)
		{
			return global::SR.Format("variable '{0}' of type '{1}' referenced from scope '{2}', but it is not defined", p0, p1, p2);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0003D5BD File Offset: 0x0003B7BD
		internal static string CannotCloseOverByRef(object p0, object p1)
		{
			return global::SR.Format("Cannot close over byref parameter '{0}' referenced in lambda '{1}'", p0, p1);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0003D5CB File Offset: 0x0003B7CB
		internal static string UnexpectedVarArgsCall(object p0)
		{
			return global::SR.Format("Unexpected VarArgs call to method '{0}'", p0);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		internal static string RethrowRequiresCatch
		{
			get
			{
				return "Rethrow statement is valid only inside a Catch block.";
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0003D5DF File Offset: 0x0003B7DF
		internal static string TryNotAllowedInFilter
		{
			get
			{
				return "Try expression is not allowed inside a filter body.";
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0003D5E6 File Offset: 0x0003B7E6
		internal static string MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return global::SR.Format("When called from '{0}', rewriting a node of type '{1}' must return a non-null value of the same type. Alternatively, override '{2}' and change it to not visit children of this type.", p0, p1, p2);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0003D5F5 File Offset: 0x0003B7F5
		internal static string MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return global::SR.Format("Rewriting child expression from type '{0}' to type '{1}' is not allowed, because it would change the meaning of the operation. If this is intentional, override '{2}' and change it to allow this rewrite.", p0, p1, p2);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0003D604 File Offset: 0x0003B804
		internal static string MustRewriteWithoutMethod(object p0, object p1)
		{
			return global::SR.Format("Rewritten expression calls operator method '{0}', but the original node had no operator method. If this is intentional, override '{1}' and change it to allow this rewrite.", p0, p1);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0003D612 File Offset: 0x0003B812
		internal static string TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return global::SR.Format("TryExpression is not supported as an argument to method '{0}' because it has an argument with by-ref type. Construct the tree so the TryExpression is not nested inside of this expression.", p0);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0003D61F File Offset: 0x0003B81F
		internal static string TryNotSupportedForValueTypeInstances(object p0)
		{
			return global::SR.Format("TryExpression is not supported as a child expression when accessing a member on type '{0}' because it is a value type. Construct the tree so the TryExpression is not nested inside of this expression.", p0);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0003D62C File Offset: 0x0003B82C
		internal static string TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return global::SR.Format("Test value of type '{0}' cannot be used for the comparison method parameter of type '{1}'", p0, p1);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003D63A File Offset: 0x0003B83A
		internal static string SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return global::SR.Format("Switch value of type '{0}' cannot be used for the comparison method parameter of type '{1}'", p0, p1);
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0003D648 File Offset: 0x0003B848
		internal static string PdbGeneratorNeedsExpressionCompiler
		{
			get
			{
				return "DebugInfoGenerator created by CreatePdbGenerator can only be used with LambdaExpression.CompileToMethod.";
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0003D64F File Offset: 0x0003B84F
		internal static string NonStaticConstructorRequired
		{
			get
			{
				return "The constructor should not be static";
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0003D656 File Offset: 0x0003B856
		internal static string NonAbstractConstructorRequired
		{
			get
			{
				return "Can't compile a NewExpression with a constructor declared on an abstract class";
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0003D65D File Offset: 0x0003B85D
		internal static string ExpressionMustBeReadable
		{
			get
			{
				return "Expression must be readable";
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0003D664 File Offset: 0x0003B864
		internal static string ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for constructor parameter of type '{1}'", p0, p1);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0003D672 File Offset: 0x0003B872
		internal static string EnumerationIsDone
		{
			get
			{
				return "Enumeration has either not started or has already finished.";
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0003D679 File Offset: 0x0003B879
		internal static string TypeContainsGenericParameters(object p0)
		{
			return global::SR.Format("Type {0} contains generic parameters", p0);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0003D686 File Offset: 0x0003B886
		internal static string TypeIsGeneric(object p0)
		{
			return global::SR.Format("Type {0} is a generic type definition", p0);
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0003D693 File Offset: 0x0003B893
		internal static string InvalidArgumentValue
		{
			get
			{
				return "Invalid argument value";
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0003D69A File Offset: 0x0003B89A
		internal static string NonEmptyCollectionRequired
		{
			get
			{
				return "Non-empty collection required";
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0003D6A1 File Offset: 0x0003B8A1
		internal static string InvalidNullValue(object p0)
		{
			return global::SR.Format("The value null is not of type '{0}' and cannot be used in this collection.", p0);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0003D6AE File Offset: 0x0003B8AE
		internal static string InvalidObjectType(object p0, object p1)
		{
			return global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this collection.", p0, p1);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0003D6BC File Offset: 0x0003B8BC
		internal static string ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for parameter of type '{1}' of method '{2}'", p0, p1, p2);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0003D6CB File Offset: 0x0003B8CB
		internal static string ExpressionTypeDoesNotMatchParameter(object p0, object p1)
		{
			return global::SR.Format("Expression of type '{0}' cannot be used for parameter of type '{1}'", p0, p1);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0003D6D9 File Offset: 0x0003B8D9
		internal static string IncorrectNumberOfMethodCallArguments(object p0)
		{
			return global::SR.Format("Incorrect number of arguments supplied for call to method '{0}'", p0);
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0003D6E6 File Offset: 0x0003B8E6
		internal static string IncorrectNumberOfLambdaArguments
		{
			get
			{
				return "Incorrect number of arguments supplied for lambda invocation";
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0003D6ED File Offset: 0x0003B8ED
		internal static string IncorrectNumberOfConstructorArguments
		{
			get
			{
				return "Incorrect number of arguments for constructor";
			}
		}
	}
}
