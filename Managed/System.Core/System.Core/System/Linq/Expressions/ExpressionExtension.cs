using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200026B RID: 619
	internal static class ExpressionExtension
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x00037BC2 File Offset: 0x00035DC2
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arguments);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00038128 File Offset: 0x00036328
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			IReadOnlyList<Expression> readOnlyList = (arguments as IReadOnlyList<Expression>) ?? arguments.ToReadOnly<Expression>();
			switch (readOnlyList.Count)
			{
			case 1:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0]);
			case 2:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0], readOnlyList[1]);
			case 3:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0], readOnlyList[1], readOnlyList[2]);
			case 4:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3]);
			default:
			{
				ContractUtils.RequiresNotNull(delegateType, "delegateType");
				ContractUtils.RequiresNotNull(binder, "binder");
				if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
				{
					throw Error.TypeMustBeDerivedFromSystemDelegate();
				}
				MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
				ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
				ExpressionUtils.ValidateArgumentTypes(validMethodForDynamic, ExpressionType.Dynamic, ref readOnlyCollection, "delegateType");
				return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, readOnlyCollection);
			}
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00038224 File Offset: 0x00036424
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 2, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000382A8 File Offset: 0x000364A8
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 3, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2], "delegateType", "arg1", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00038350 File Offset: 0x00036550
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 4, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2], "delegateType", "arg1", -1);
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg2, parametersCached[3], "delegateType", "arg2", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00038420 File Offset: 0x00036620
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 5, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2], "delegateType", "arg1", -1);
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg2, parametersCached[3], "delegateType", "arg2", -1);
			ExpressionExtension.ValidateDynamicArgument(arg3, "arg3");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg3, parametersCached[4], "delegateType", "arg3", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00038514 File Offset: 0x00036714
		private static MethodInfo GetValidMethodForDynamic(Type delegateType)
		{
			MethodInfo invokeMethod = delegateType.GetInvokeMethod();
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length == 0 || parametersCached[0].ParameterType != typeof(CallSite))
			{
				throw Error.FirstArgumentMustBeCallSite();
			}
			return invokeMethod;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00037B87 File Offset: 0x00035D87
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arguments);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00038554 File Offset: 0x00036754
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[] { arg0 });
			}
			Type type2 = type;
			return DynamicExpression.Make(returnType, type2, binder, arg0);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000385C0 File Offset: 0x000367C0
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite)))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[] { arg0, arg1 });
			}
			Type type2 = type;
			return DynamicExpression.Make(returnType, type2, binder, arg0, arg1);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00038644 File Offset: 0x00036844
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg2.Type, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite))))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[] { arg0, arg1, arg2 });
			}
			Type type2 = type;
			return DynamicExpression.Make(returnType, type2, binder, arg0, arg1, arg2);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000386E8 File Offset: 0x000368E8
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			ExpressionExtension.ValidateDynamicArgument(arg3, "arg3");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg3.Type, DelegateHelpers.GetNextTypeInfo(arg2.Type, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite)))))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[] { arg0, arg1, arg2, arg3 });
			}
			Type type2 = type;
			return DynamicExpression.Make(returnType, type2, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000387AC File Offset: 0x000369AC
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(arguments, "arguments");
			ContractUtils.RequiresNotNull(returnType, "returnType");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "arguments");
			return ExpressionExtension.MakeDynamic(binder, returnType, readOnlyCollection);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000387EC File Offset: 0x000369EC
		private static DynamicExpression MakeDynamic(CallSiteBinder binder, Type returnType, ReadOnlyCollection<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			int count = arguments.Count;
			for (int i = 0; i < count; i++)
			{
				ExpressionExtension.ValidateDynamicArgument(arguments[i], "arguments", i);
			}
			Type type = DelegateHelpers.MakeCallSiteDelegate(arguments, returnType);
			switch (count)
			{
			case 1:
				return DynamicExpression.Make(returnType, type, binder, arguments[0]);
			case 2:
				return DynamicExpression.Make(returnType, type, binder, arguments[0], arguments[1]);
			case 3:
				return DynamicExpression.Make(returnType, type, binder, arguments[0], arguments[1], arguments[2]);
			case 4:
				return DynamicExpression.Make(returnType, type, binder, arguments[0], arguments[1], arguments[2], arguments[3]);
			default:
				return DynamicExpression.Make(returnType, type, binder, arguments);
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000388BE File Offset: 0x00036ABE
		private static void ValidateDynamicArgument(Expression arg, string paramName)
		{
			ExpressionExtension.ValidateDynamicArgument(arg, paramName, -1);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000388C8 File Offset: 0x00036AC8
		private static void ValidateDynamicArgument(Expression arg, string paramName, int index)
		{
			ExpressionUtils.RequiresCanRead(arg, paramName, index);
			Type type = arg.Type;
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type", true, true);
			if (type == typeof(void))
			{
				throw Error.ArgumentTypeCannotBeVoid();
			}
		}
	}
}
