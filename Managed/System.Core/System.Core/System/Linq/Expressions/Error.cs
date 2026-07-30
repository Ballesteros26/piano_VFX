using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200026E RID: 622
	internal static class Error
	{
		// Token: 0x06001151 RID: 4433 RVA: 0x0003898B File Offset: 0x00036B8B
		internal static Exception ReducibleMustOverrideReduce()
		{
			return new ArgumentException(Strings.ReducibleMustOverrideReduce);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00038997 File Offset: 0x00036B97
		internal static Exception ArgCntMustBeGreaterThanNameCnt()
		{
			return new ArgumentException(Strings.ArgCntMustBeGreaterThanNameCnt);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000389A3 File Offset: 0x00036BA3
		internal static Exception InvalidMetaObjectCreated(object p0)
		{
			return new InvalidOperationException(Strings.InvalidMetaObjectCreated(p0));
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000389B0 File Offset: 0x00036BB0
		internal static Exception AmbiguousMatchInExpandoObject(object p0)
		{
			return new AmbiguousMatchException(Strings.AmbiguousMatchInExpandoObject(p0));
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000389BD File Offset: 0x00036BBD
		internal static Exception SameKeyExistsInExpando(object key)
		{
			return new ArgumentException(Strings.SameKeyExistsInExpando(key), "key");
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000389CF File Offset: 0x00036BCF
		internal static Exception KeyDoesNotExistInExpando(object p0)
		{
			return new KeyNotFoundException(Strings.KeyDoesNotExistInExpando(p0));
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000389DC File Offset: 0x00036BDC
		internal static Exception CollectionModifiedWhileEnumerating()
		{
			return new InvalidOperationException(Strings.CollectionModifiedWhileEnumerating);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000389E8 File Offset: 0x00036BE8
		internal static Exception CollectionReadOnly()
		{
			return new NotSupportedException(Strings.CollectionReadOnly);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000389F4 File Offset: 0x00036BF4
		internal static Exception MustReduceToDifferent()
		{
			return new ArgumentException(Strings.MustReduceToDifferent);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00038A00 File Offset: 0x00036C00
		internal static Exception BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinderNotCompatibleWithCallSite(p0, p1, p2));
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00038A0F File Offset: 0x00036C0F
		internal static Exception DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DynamicBindingNeedsRestrictions(p0, p1));
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00038A1D File Offset: 0x00036C1D
		internal static Exception DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return new InvalidCastException(Strings.DynamicObjectResultNotAssignable(p0, p1, p2, p3));
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00038A2D File Offset: 0x00036C2D
		internal static Exception DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return new InvalidCastException(Strings.DynamicBinderResultNotAssignable(p0, p1, p2));
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00038A3C File Offset: 0x00036C3C
		internal static Exception BindingCannotBeNull()
		{
			return new InvalidOperationException(Strings.BindingCannotBeNull);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00038A48 File Offset: 0x00036C48
		internal static Exception ReducedNotCompatible()
		{
			return new ArgumentException(Strings.ReducedNotCompatible);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00038A54 File Offset: 0x00036C54
		internal static Exception SetterHasNoParams(string paramName)
		{
			return new ArgumentException(Strings.SetterHasNoParams, paramName);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00038A61 File Offset: 0x00036C61
		internal static Exception PropertyCannotHaveRefType(string paramName)
		{
			return new ArgumentException(Strings.PropertyCannotHaveRefType, paramName);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00038A6E File Offset: 0x00036C6E
		internal static Exception IndexesOfSetGetMustMatch(string paramName)
		{
			return new ArgumentException(Strings.IndexesOfSetGetMustMatch, paramName);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00038A7B File Offset: 0x00036C7B
		internal static Exception TypeParameterIsNotDelegate(object p0)
		{
			return new InvalidOperationException(Strings.TypeParameterIsNotDelegate(p0));
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00038A88 File Offset: 0x00036C88
		internal static Exception FirstArgumentMustBeCallSite()
		{
			return new ArgumentException(Strings.FirstArgumentMustBeCallSite);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00038A94 File Offset: 0x00036C94
		internal static Exception AccessorsCannotHaveVarArgs(string paramName)
		{
			return new ArgumentException(Strings.AccessorsCannotHaveVarArgs, paramName);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00038AA1 File Offset: 0x00036CA1
		private static Exception AccessorsCannotHaveByRefArgs(string paramName)
		{
			return new ArgumentException(Strings.AccessorsCannotHaveByRefArgs, paramName);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00038AAE File Offset: 0x00036CAE
		internal static Exception AccessorsCannotHaveByRefArgs(string paramName, int index)
		{
			return Error.AccessorsCannotHaveByRefArgs(Error.GetParamName(paramName, index));
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00038ABC File Offset: 0x00036CBC
		internal static Exception TypeMustBeDerivedFromSystemDelegate()
		{
			return new ArgumentException(Strings.TypeMustBeDerivedFromSystemDelegate);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00038AC8 File Offset: 0x00036CC8
		internal static Exception NoOrInvalidRuleProduced()
		{
			return new InvalidOperationException(Strings.NoOrInvalidRuleProduced);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00038AD4 File Offset: 0x00036CD4
		internal static Exception BoundsCannotBeLessThanOne(string paramName)
		{
			return new ArgumentException(Strings.BoundsCannotBeLessThanOne, paramName);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00038AE1 File Offset: 0x00036CE1
		internal static Exception TypeMustNotBeByRef(string paramName)
		{
			return new ArgumentException(Strings.TypeMustNotBeByRef, paramName);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00038AEE File Offset: 0x00036CEE
		internal static Exception TypeMustNotBePointer(string paramName)
		{
			return new ArgumentException(Strings.TypeMustNotBePointer, paramName);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00038AFB File Offset: 0x00036CFB
		internal static Exception SetterMustBeVoid(string paramName)
		{
			return new ArgumentException(Strings.SetterMustBeVoid, paramName);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00038B08 File Offset: 0x00036D08
		internal static Exception PropertyTypeMustMatchGetter(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeMustMatchGetter, paramName);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00038B15 File Offset: 0x00036D15
		internal static Exception PropertyTypeMustMatchSetter(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeMustMatchSetter, paramName);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00038B22 File Offset: 0x00036D22
		internal static Exception BothAccessorsMustBeStatic(string paramName)
		{
			return new ArgumentException(Strings.BothAccessorsMustBeStatic, paramName);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00038B2F File Offset: 0x00036D2F
		internal static Exception OnlyStaticFieldsHaveNullInstance(string paramName)
		{
			return new ArgumentException(Strings.OnlyStaticFieldsHaveNullInstance, paramName);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00038B3C File Offset: 0x00036D3C
		internal static Exception OnlyStaticPropertiesHaveNullInstance(string paramName)
		{
			return new ArgumentException(Strings.OnlyStaticPropertiesHaveNullInstance, paramName);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00038B49 File Offset: 0x00036D49
		internal static Exception OnlyStaticMethodsHaveNullInstance()
		{
			return new ArgumentException(Strings.OnlyStaticMethodsHaveNullInstance);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00038B55 File Offset: 0x00036D55
		internal static Exception PropertyTypeCannotBeVoid(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeCannotBeVoid, paramName);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00038B62 File Offset: 0x00036D62
		internal static Exception InvalidUnboxType(string paramName)
		{
			return new ArgumentException(Strings.InvalidUnboxType, paramName);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00038B6F File Offset: 0x00036D6F
		internal static Exception ExpressionMustBeWriteable(string paramName)
		{
			return new ArgumentException(Strings.ExpressionMustBeWriteable, paramName);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00038B7C File Offset: 0x00036D7C
		internal static Exception ArgumentMustNotHaveValueType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustNotHaveValueType, paramName);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00038B89 File Offset: 0x00036D89
		internal static Exception MustBeReducible()
		{
			return new ArgumentException(Strings.MustBeReducible);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00038B95 File Offset: 0x00036D95
		internal static Exception AllTestValuesMustHaveSameType(string paramName)
		{
			return new ArgumentException(Strings.AllTestValuesMustHaveSameType, paramName);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00038BA2 File Offset: 0x00036DA2
		internal static Exception AllCaseBodiesMustHaveSameType(string paramName)
		{
			return new ArgumentException(Strings.AllCaseBodiesMustHaveSameType, paramName);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00038BAF File Offset: 0x00036DAF
		internal static Exception DefaultBodyMustBeSupplied(string paramName)
		{
			return new ArgumentException(Strings.DefaultBodyMustBeSupplied, paramName);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00038BBC File Offset: 0x00036DBC
		internal static Exception LabelMustBeVoidOrHaveExpression(string paramName)
		{
			return new ArgumentException(Strings.LabelMustBeVoidOrHaveExpression, paramName);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00038BC9 File Offset: 0x00036DC9
		internal static Exception LabelTypeMustBeVoid(string paramName)
		{
			return new ArgumentException(Strings.LabelTypeMustBeVoid, paramName);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00038BD6 File Offset: 0x00036DD6
		internal static Exception QuotedExpressionMustBeLambda(string paramName)
		{
			return new ArgumentException(Strings.QuotedExpressionMustBeLambda, paramName);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00038BE3 File Offset: 0x00036DE3
		internal static Exception VariableMustNotBeByRef(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.VariableMustNotBeByRef(p0, p1), paramName);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00038BF2 File Offset: 0x00036DF2
		internal static Exception VariableMustNotBeByRef(object p0, object p1, string paramName, int index)
		{
			return Error.VariableMustNotBeByRef(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00038C02 File Offset: 0x00036E02
		private static Exception DuplicateVariable(object p0, string paramName)
		{
			return new ArgumentException(Strings.DuplicateVariable(p0), paramName);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00038C10 File Offset: 0x00036E10
		internal static Exception DuplicateVariable(object p0, string paramName, int index)
		{
			return Error.DuplicateVariable(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00038C1F File Offset: 0x00036E1F
		internal static Exception StartEndMustBeOrdered()
		{
			return new ArgumentException(Strings.StartEndMustBeOrdered);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00038C2B File Offset: 0x00036E2B
		internal static Exception FaultCannotHaveCatchOrFinally(string paramName)
		{
			return new ArgumentException(Strings.FaultCannotHaveCatchOrFinally, paramName);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00038C38 File Offset: 0x00036E38
		internal static Exception TryMustHaveCatchFinallyOrFault()
		{
			return new ArgumentException(Strings.TryMustHaveCatchFinallyOrFault);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00038C44 File Offset: 0x00036E44
		internal static Exception BodyOfCatchMustHaveSameTypeAsBodyOfTry()
		{
			return new ArgumentException(Strings.BodyOfCatchMustHaveSameTypeAsBodyOfTry);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00038C50 File Offset: 0x00036E50
		internal static Exception ExtensionNodeMustOverrideProperty(object p0)
		{
			return new InvalidOperationException(Strings.ExtensionNodeMustOverrideProperty(p0));
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00038C5D File Offset: 0x00036E5D
		internal static Exception UserDefinedOperatorMustBeStatic(object p0, string paramName)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustBeStatic(p0), paramName);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00038C6B File Offset: 0x00036E6B
		internal static Exception UserDefinedOperatorMustNotBeVoid(object p0, string paramName)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustNotBeVoid(p0), paramName);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00038C79 File Offset: 0x00036E79
		internal static Exception CoercionOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CoercionOperatorNotDefined(p0, p1));
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00038C87 File Offset: 0x00036E87
		internal static Exception UnaryOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.UnaryOperatorNotDefined(p0, p1));
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00038C95 File Offset: 0x00036E95
		internal static Exception BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinaryOperatorNotDefined(p0, p1, p2));
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00038CA4 File Offset: 0x00036EA4
		internal static Exception ReferenceEqualityNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ReferenceEqualityNotDefined(p0, p1));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00038CB2 File Offset: 0x00036EB2
		internal static Exception OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OperandTypesDoNotMatchParameters(p0, p1));
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00038CC0 File Offset: 0x00036EC0
		internal static Exception OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OverloadOperatorTypeDoesNotMatchConversionType(p0, p1));
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00038CCE File Offset: 0x00036ECE
		internal static Exception ConversionIsNotSupportedForArithmeticTypes()
		{
			return new InvalidOperationException(Strings.ConversionIsNotSupportedForArithmeticTypes);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00038CDA File Offset: 0x00036EDA
		internal static Exception ArgumentTypeCannotBeVoid()
		{
			return new ArgumentException(Strings.ArgumentTypeCannotBeVoid);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00038CE6 File Offset: 0x00036EE6
		internal static Exception ArgumentMustBeArray(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeArray, paramName);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00038CF3 File Offset: 0x00036EF3
		internal static Exception ArgumentMustBeBoolean(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeBoolean, paramName);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00038D00 File Offset: 0x00036F00
		internal static Exception EqualityMustReturnBoolean(object p0, string paramName)
		{
			return new ArgumentException(Strings.EqualityMustReturnBoolean(p0), paramName);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00038D0E File Offset: 0x00036F0E
		internal static Exception ArgumentMustBeFieldInfoOrPropertyInfo(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertyInfo, paramName);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00038D1B File Offset: 0x00036F1B
		private static Exception ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod, paramName);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00038D28 File Offset: 0x00036F28
		internal static Exception ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(string paramName, int index)
		{
			return Error.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(Error.GetParamName(paramName, index));
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00038D36 File Offset: 0x00036F36
		private static Exception ArgumentMustBeInstanceMember(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeInstanceMember, paramName);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00038D43 File Offset: 0x00036F43
		internal static Exception ArgumentMustBeInstanceMember(string paramName, int index)
		{
			return Error.ArgumentMustBeInstanceMember(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00038D51 File Offset: 0x00036F51
		private static Exception ArgumentMustBeInteger(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeInteger, paramName);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00038D5E File Offset: 0x00036F5E
		internal static Exception ArgumentMustBeInteger(string paramName, int index)
		{
			return Error.ArgumentMustBeInteger(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00038D6C File Offset: 0x00036F6C
		internal static Exception ArgumentMustBeArrayIndexType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeArrayIndexType, paramName);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00038D79 File Offset: 0x00036F79
		internal static Exception ArgumentMustBeArrayIndexType(string paramName, int index)
		{
			return Error.ArgumentMustBeArrayIndexType(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00038D87 File Offset: 0x00036F87
		internal static Exception ArgumentMustBeSingleDimensionalArrayType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeSingleDimensionalArrayType, paramName);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00038D94 File Offset: 0x00036F94
		internal static Exception ArgumentTypesMustMatch()
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00038DA0 File Offset: 0x00036FA0
		internal static Exception ArgumentTypesMustMatch(string paramName)
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch, paramName);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00038DAD File Offset: 0x00036FAD
		internal static Exception CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeElementThroughProperty(p0));
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00038DBA File Offset: 0x00036FBA
		internal static Exception CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeMemberThroughProperty(p0));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00038DC7 File Offset: 0x00036FC7
		internal static Exception IncorrectTypeForTypeAs(object p0, string paramName)
		{
			return new ArgumentException(Strings.IncorrectTypeForTypeAs(p0), paramName);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00038DD5 File Offset: 0x00036FD5
		internal static Exception CoalesceUsedOnNonNullType()
		{
			return new InvalidOperationException(Strings.CoalesceUsedOnNonNullType);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00038DE1 File Offset: 0x00036FE1
		internal static Exception ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ExpressionTypeCannotInitializeArrayType(p0, p1));
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00038DEF File Offset: 0x00036FEF
		private static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ArgumentTypeDoesNotMatchMember(p0, p1), paramName);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00038DFE File Offset: 0x00036FFE
		internal static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1, string paramName, int index)
		{
			return Error.ArgumentTypeDoesNotMatchMember(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00038E0E File Offset: 0x0003700E
		private static Exception ArgumentMemberNotDeclOnType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ArgumentMemberNotDeclOnType(p0, p1), paramName);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00038E1D File Offset: 0x0003701D
		internal static Exception ArgumentMemberNotDeclOnType(object p0, object p1, string paramName, int index)
		{
			return Error.ArgumentMemberNotDeclOnType(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00038E2D File Offset: 0x0003702D
		internal static Exception ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchReturn(p0, p1));
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00038E3B File Offset: 0x0003703B
		internal static Exception ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchAssignment(p0, p1));
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00038E49 File Offset: 0x00037049
		internal static Exception ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchLabel(p0, p1));
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00038E57 File Offset: 0x00037057
		internal static Exception ExpressionTypeNotInvocable(object p0, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeNotInvocable(p0), paramName);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00038E65 File Offset: 0x00037065
		internal static Exception FieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.FieldNotDefinedForType(p0, p1));
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00038E73 File Offset: 0x00037073
		internal static Exception InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstanceFieldNotDefinedForType(p0, p1));
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00038E81 File Offset: 0x00037081
		internal static Exception FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.FieldInfoNotDefinedForType(p0, p1, p2));
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00038E90 File Offset: 0x00037090
		internal static Exception IncorrectNumberOfIndexes()
		{
			return new ArgumentException(Strings.IncorrectNumberOfIndexes);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00038E9C File Offset: 0x0003709C
		internal static Exception IncorrectNumberOfLambdaDeclarationParameters()
		{
			return new ArgumentException(Strings.IncorrectNumberOfLambdaDeclarationParameters);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00038EA8 File Offset: 0x000370A8
		internal static Exception IncorrectNumberOfMembersForGivenConstructor()
		{
			return new ArgumentException(Strings.IncorrectNumberOfMembersForGivenConstructor);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00038EB4 File Offset: 0x000370B4
		internal static Exception IncorrectNumberOfArgumentsForMembers()
		{
			return new ArgumentException(Strings.IncorrectNumberOfArgumentsForMembers);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00038EC0 File Offset: 0x000370C0
		internal static Exception LambdaTypeMustBeDerivedFromSystemDelegate(string paramName)
		{
			return new ArgumentException(Strings.LambdaTypeMustBeDerivedFromSystemDelegate, paramName);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00038ECD File Offset: 0x000370CD
		internal static Exception MemberNotFieldOrProperty(object p0, string paramName)
		{
			return new ArgumentException(Strings.MemberNotFieldOrProperty(p0), paramName);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00038EDB File Offset: 0x000370DB
		internal static Exception MethodContainsGenericParameters(object p0, string paramName)
		{
			return new ArgumentException(Strings.MethodContainsGenericParameters(p0), paramName);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00038EE9 File Offset: 0x000370E9
		internal static Exception MethodIsGeneric(object p0, string paramName)
		{
			return new ArgumentException(Strings.MethodIsGeneric(p0), paramName);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00038EF7 File Offset: 0x000370F7
		private static Exception MethodNotPropertyAccessor(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.MethodNotPropertyAccessor(p0, p1), paramName);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00038F06 File Offset: 0x00037106
		internal static Exception MethodNotPropertyAccessor(object p0, object p1, string paramName, int index)
		{
			return Error.MethodNotPropertyAccessor(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00038F16 File Offset: 0x00037116
		internal static Exception PropertyDoesNotHaveGetter(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveGetter(p0), paramName);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00038F24 File Offset: 0x00037124
		internal static Exception PropertyDoesNotHaveGetter(object p0, string paramName, int index)
		{
			return Error.PropertyDoesNotHaveGetter(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00038F33 File Offset: 0x00037133
		internal static Exception PropertyDoesNotHaveSetter(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveSetter(p0), paramName);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00038F41 File Offset: 0x00037141
		internal static Exception PropertyDoesNotHaveAccessor(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveAccessor(p0), paramName);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00038F4F File Offset: 0x0003714F
		internal static Exception NotAMemberOfType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.NotAMemberOfType(p0, p1), paramName);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00038F5E File Offset: 0x0003715E
		internal static Exception NotAMemberOfType(object p0, object p1, string paramName, int index)
		{
			return Error.NotAMemberOfType(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00038F6E File Offset: 0x0003716E
		internal static Exception NotAMemberOfAnyType(object p0, string paramName)
		{
			return new ArgumentException(Strings.NotAMemberOfAnyType(p0), paramName);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00038F7C File Offset: 0x0003717C
		internal static Exception ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return new ArgumentException(Strings.ParameterExpressionNotValidAsDelegate(p0, p1));
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00038F8A File Offset: 0x0003718A
		internal static Exception PropertyNotDefinedForType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.PropertyNotDefinedForType(p0, p1), paramName);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00038F99 File Offset: 0x00037199
		internal static Exception InstancePropertyNotDefinedForType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.InstancePropertyNotDefinedForType(p0, p1), paramName);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00038FA8 File Offset: 0x000371A8
		internal static Exception InstancePropertyWithoutParameterNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstancePropertyWithoutParameterNotDefinedForType(p0, p1));
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00038FB6 File Offset: 0x000371B6
		internal static Exception InstancePropertyWithSpecifiedParametersNotDefinedForType(object p0, object p1, object p2, string paramName)
		{
			return new ArgumentException(Strings.InstancePropertyWithSpecifiedParametersNotDefinedForType(p0, p1, p2), paramName);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00038FC6 File Offset: 0x000371C6
		internal static Exception InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.InstanceAndMethodTypeMismatch(p0, p1, p2));
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00038FD5 File Offset: 0x000371D5
		internal static Exception TypeMissingDefaultConstructor(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeMissingDefaultConstructor(p0), paramName);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00038FE3 File Offset: 0x000371E3
		internal static Exception ElementInitializerMethodNotAdd(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNotAdd, paramName);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00038FF0 File Offset: 0x000371F0
		internal static Exception ElementInitializerMethodNoRefOutParam(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNoRefOutParam(p0, p1), paramName);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00038FFF File Offset: 0x000371FF
		internal static Exception ElementInitializerMethodWithZeroArgs(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodWithZeroArgs, paramName);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003900C File Offset: 0x0003720C
		internal static Exception ElementInitializerMethodStatic(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodStatic, paramName);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00039019 File Offset: 0x00037219
		internal static Exception TypeNotIEnumerable(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeNotIEnumerable(p0), paramName);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00039027 File Offset: 0x00037227
		internal static Exception UnhandledBinary(object p0, string paramName)
		{
			return new ArgumentException(Strings.UnhandledBinary(p0), paramName);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00039035 File Offset: 0x00037235
		internal static Exception UnhandledBinding()
		{
			return new ArgumentException(Strings.UnhandledBinding);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00039041 File Offset: 0x00037241
		internal static Exception UnhandledBindingType(object p0)
		{
			return new ArgumentException(Strings.UnhandledBindingType(p0));
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003904E File Offset: 0x0003724E
		internal static Exception UnhandledUnary(object p0, string paramName)
		{
			return new ArgumentException(Strings.UnhandledUnary(p0), paramName);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003905C File Offset: 0x0003725C
		internal static Exception UnknownBindingType(int index)
		{
			return new ArgumentException(Strings.UnknownBindingType, string.Format("bindings[{0}]", index));
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00039078 File Offset: 0x00037278
		internal static Exception UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveConsistentTypes(p0, p1));
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00039086 File Offset: 0x00037286
		internal static Exception UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveValidReturnType(p0, p1));
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00039094 File Offset: 0x00037294
		internal static Exception LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return new ArgumentException(Strings.LogicalOperatorMustHaveBooleanOperators(p0, p1));
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000390A2 File Offset: 0x000372A2
		internal static Exception MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000390B0 File Offset: 0x000372B0
		internal static Exception GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.GenericMethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000390BE File Offset: 0x000372BE
		internal static Exception MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000390CC File Offset: 0x000372CC
		internal static Exception PropertyWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.PropertyWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000390DA File Offset: 0x000372DA
		internal static Exception IncorrectNumberOfTypeArgsForFunc(string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForFunc, paramName);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000390E7 File Offset: 0x000372E7
		internal static Exception IncorrectNumberOfTypeArgsForAction(string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForAction, paramName);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000390F4 File Offset: 0x000372F4
		internal static Exception ArgumentCannotBeOfTypeVoid(string paramName)
		{
			return new ArgumentException(Strings.ArgumentCannotBeOfTypeVoid, paramName);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00039101 File Offset: 0x00037301
		internal static Exception OutOfRange(string paramName, object p1)
		{
			return new ArgumentOutOfRangeException(paramName, Strings.OutOfRange(paramName, p1));
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00039110 File Offset: 0x00037310
		internal static Exception LabelTargetAlreadyDefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetAlreadyDefined(p0));
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003911D File Offset: 0x0003731D
		internal static Exception LabelTargetUndefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetUndefined(p0));
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0003912A File Offset: 0x0003732A
		internal static Exception ControlCannotLeaveFinally()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFinally);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00039136 File Offset: 0x00037336
		internal static Exception ControlCannotLeaveFilterTest()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFilterTest);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00039142 File Offset: 0x00037342
		internal static Exception AmbiguousJump(object p0)
		{
			return new InvalidOperationException(Strings.AmbiguousJump(p0));
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003914F File Offset: 0x0003734F
		internal static Exception ControlCannotEnterTry()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterTry);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003915B File Offset: 0x0003735B
		internal static Exception ControlCannotEnterExpression()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterExpression);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00039167 File Offset: 0x00037367
		internal static Exception NonLocalJumpWithValue(object p0)
		{
			return new InvalidOperationException(Strings.NonLocalJumpWithValue(p0));
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00039174 File Offset: 0x00037374
		internal static Exception CannotCompileConstant(object p0)
		{
			return new InvalidOperationException(Strings.CannotCompileConstant(p0));
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00039181 File Offset: 0x00037381
		internal static Exception CannotCompileDynamic()
		{
			return new NotSupportedException(Strings.CannotCompileDynamic);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0003918D File Offset: 0x0003738D
		internal static Exception MethodBuilderDoesNotHaveTypeBuilder()
		{
			return new ArgumentException(Strings.MethodBuilderDoesNotHaveTypeBuilder);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00039199 File Offset: 0x00037399
		internal static Exception InvalidLvalue(ExpressionType p0)
		{
			return new InvalidOperationException(Strings.InvalidLvalue(p0));
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000391AB File Offset: 0x000373AB
		internal static Exception UndefinedVariable(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.UndefinedVariable(p0, p1, p2));
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000391BA File Offset: 0x000373BA
		internal static Exception CannotCloseOverByRef(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CannotCloseOverByRef(p0, p1));
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000391C8 File Offset: 0x000373C8
		internal static Exception UnexpectedVarArgsCall(object p0)
		{
			return new InvalidOperationException(Strings.UnexpectedVarArgsCall(p0));
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000391D5 File Offset: 0x000373D5
		internal static Exception RethrowRequiresCatch()
		{
			return new InvalidOperationException(Strings.RethrowRequiresCatch);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000391E1 File Offset: 0x000373E1
		internal static Exception TryNotAllowedInFilter()
		{
			return new InvalidOperationException(Strings.TryNotAllowedInFilter);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000391ED File Offset: 0x000373ED
		internal static Exception MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteToSameNode(p0, p1, p2));
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000391FC File Offset: 0x000373FC
		internal static Exception MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteChildToSameType(p0, p1, p2));
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003920B File Offset: 0x0003740B
		internal static Exception MustRewriteWithoutMethod(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MustRewriteWithoutMethod(p0, p1));
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00039219 File Offset: 0x00037419
		internal static Exception TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForMethodsWithRefArgs(p0));
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00039226 File Offset: 0x00037426
		internal static Exception TryNotSupportedForValueTypeInstances(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForValueTypeInstances(p0));
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00039233 File Offset: 0x00037433
		internal static Exception TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.TestValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00039241 File Offset: 0x00037441
		internal static Exception SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.SwitchValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003924F File Offset: 0x0003744F
		internal static Exception PdbGeneratorNeedsExpressionCompiler()
		{
			return new NotSupportedException(Strings.PdbGeneratorNeedsExpressionCompiler);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0000D23D File Offset: 0x0000B43D
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0000D275 File Offset: 0x0000B475
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0003925B File Offset: 0x0003745B
		internal static Exception NonStaticConstructorRequired(string paramName)
		{
			return new ArgumentException(Strings.NonStaticConstructorRequired, paramName);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00039268 File Offset: 0x00037468
		internal static Exception NonAbstractConstructorRequired()
		{
			return new InvalidOperationException(Strings.NonAbstractConstructorRequired);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00039274 File Offset: 0x00037474
		internal static Exception InvalidProgram()
		{
			return new InvalidProgramException();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0003927B File Offset: 0x0003747B
		internal static Exception EnumerationIsDone()
		{
			return new InvalidOperationException(Strings.EnumerationIsDone);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00039287 File Offset: 0x00037487
		private static Exception TypeContainsGenericParameters(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeContainsGenericParameters(p0), paramName);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00039295 File Offset: 0x00037495
		internal static Exception TypeContainsGenericParameters(object p0, string paramName, int index)
		{
			return Error.TypeContainsGenericParameters(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000392A4 File Offset: 0x000374A4
		internal static Exception TypeIsGeneric(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeIsGeneric(p0), paramName);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000392B2 File Offset: 0x000374B2
		internal static Exception TypeIsGeneric(object p0, string paramName, int index)
		{
			return Error.TypeIsGeneric(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000392C1 File Offset: 0x000374C1
		internal static Exception IncorrectNumberOfConstructorArguments()
		{
			return new ArgumentException(Strings.IncorrectNumberOfConstructorArguments);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000392CD File Offset: 0x000374CD
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2), paramName);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000392DD File Offset: 0x000374DD
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000392EF File Offset: 0x000374EF
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchParameter(p0, p1), paramName);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000392FE File Offset: 0x000374FE
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchParameter(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003930E File Offset: 0x0003750E
		internal static Exception IncorrectNumberOfLambdaArguments()
		{
			return new InvalidOperationException(Strings.IncorrectNumberOfLambdaArguments);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0003931A File Offset: 0x0003751A
		internal static Exception IncorrectNumberOfMethodCallArguments(object p0, string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfMethodCallArguments(p0), paramName);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00039328 File Offset: 0x00037528
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1), paramName);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00039337 File Offset: 0x00037537
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00039347 File Offset: 0x00037547
		internal static Exception ExpressionMustBeReadable(string paramName)
		{
			return new ArgumentException(Strings.ExpressionMustBeReadable, paramName);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00039354 File Offset: 0x00037554
		internal static Exception ExpressionMustBeReadable(string paramName, int index)
		{
			return Error.ExpressionMustBeReadable(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00039362 File Offset: 0x00037562
		internal static Exception InvalidArgumentValue(string paramName)
		{
			return new ArgumentException(Strings.InvalidArgumentValue, paramName);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0003936F File Offset: 0x0003756F
		internal static Exception NonEmptyCollectionRequired(string paramName)
		{
			return new ArgumentException(Strings.NonEmptyCollectionRequired, paramName);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003937C File Offset: 0x0003757C
		internal static Exception InvalidNullValue(Type type, string paramName)
		{
			return new ArgumentException(Strings.InvalidNullValue(type), paramName);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0003938A File Offset: 0x0003758A
		internal static Exception InvalidTypeException(object value, Type type, string paramName)
		{
			return new ArgumentException(Strings.InvalidObjectType(((value != null) ? value.GetType() : null) ?? "null", type), paramName);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000393AD File Offset: 0x000375AD
		private static string GetParamName(string paramName, int index)
		{
			if (index >= 0)
			{
				return string.Format("{0}[{1}]", paramName, index);
			}
			return paramName;
		}
	}
}
