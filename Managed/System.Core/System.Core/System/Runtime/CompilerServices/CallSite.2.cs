using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Dynamic site type.</summary>
	/// <typeparam name="T">The delegate type.</typeparam>
	// Token: 0x020002F3 RID: 755
	public class CallSite<T> : CallSite where T : class
	{
		/// <summary>The update delegate. Called when the dynamic site experiences cache miss.</summary>
		/// <returns>The update delegate.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0004B52A File Offset: 0x0004972A
		public T Update
		{
			get
			{
				if (this._match)
				{
					return CallSite<T>.s_cachedNoMatch;
				}
				return CallSite<T>.s_cachedUpdate;
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0004B541 File Offset: 0x00049741
		private CallSite(CallSiteBinder binder)
			: base(binder)
		{
			this.Target = this.GetUpdateDelegate();
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0004B556 File Offset: 0x00049756
		private CallSite()
			: base(null)
		{
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0004B55F File Offset: 0x0004975F
		internal CallSite<T> CreateMatchMaker()
		{
			return new CallSite<T>();
		}

		/// <summary>Creates an instance of the dynamic call site, initialized with the binder responsible for the runtime binding of the dynamic operations at this call site.</summary>
		/// <returns>The new instance of dynamic call site.</returns>
		/// <param name="binder">The binder responsible for the runtime binding of the dynamic operations at this call site.</param>
		// Token: 0x06001704 RID: 5892 RVA: 0x0004B566 File Offset: 0x00049766
		public static CallSite<T> Create(CallSiteBinder binder)
		{
			if (!typeof(T).IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			ContractUtils.RequiresNotNull(binder, "binder");
			return new CallSite<T>(binder);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0004B59A File Offset: 0x0004979A
		private T GetUpdateDelegate()
		{
			return this.GetUpdateDelegate(ref CallSite<T>.s_cachedUpdate);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0004B5A7 File Offset: 0x000497A7
		private T GetUpdateDelegate(ref T addr)
		{
			if (addr == null)
			{
				addr = this.MakeUpdateDelegate();
			}
			return addr;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0004B5C8 File Offset: 0x000497C8
		private void ClearRuleCache()
		{
			base.Binder.GetRuleCache<T>();
			Dictionary<Type, object> cache = base.Binder.Cache;
			if (cache != null)
			{
				Dictionary<Type, object> dictionary = cache;
				lock (dictionary)
				{
					cache.Clear();
				}
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0004B620 File Offset: 0x00049820
		internal void AddRule(T newRule)
		{
			T[] rules = this.Rules;
			if (rules == null)
			{
				this.Rules = new T[] { newRule };
				return;
			}
			T[] array;
			if (rules.Length < 9)
			{
				array = new T[rules.Length + 1];
				Array.Copy(rules, 0, array, 1, rules.Length);
			}
			else
			{
				array = new T[10];
				Array.Copy(rules, 0, array, 1, 9);
			}
			array[0] = newRule;
			this.Rules = array;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0004B690 File Offset: 0x00049890
		internal void MoveRule(int i)
		{
			if (i > 1)
			{
				T[] rules = this.Rules;
				T t = rules[i];
				rules[i] = rules[i - 1];
				rules[i - 1] = rules[i - 2];
				rules[i - 2] = t;
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0004B6DC File Offset: 0x000498DC
		internal T MakeUpdateDelegate()
		{
			Type typeFromHandle = typeof(T);
			MethodInfo invokeMethod = typeFromHandle.GetInvokeMethod();
			Type[] array;
			if (typeFromHandle.IsGenericType && CallSite<T>.IsSimpleSignature(invokeMethod, out array))
			{
				MethodInfo methodInfo = null;
				MethodInfo methodInfo2 = null;
				if (invokeMethod.ReturnType == typeof(void))
				{
					if (typeFromHandle == DelegateHelpers.GetActionType(array.AddFirst(typeof(CallSite))))
					{
						methodInfo = typeof(UpdateDelegates).GetMethod("UpdateAndExecuteVoid" + array.Length, BindingFlags.Static | BindingFlags.NonPublic);
						methodInfo2 = typeof(UpdateDelegates).GetMethod("NoMatchVoid" + array.Length, BindingFlags.Static | BindingFlags.NonPublic);
					}
				}
				else if (typeFromHandle == DelegateHelpers.GetFuncType(array.AddFirst(typeof(CallSite))))
				{
					methodInfo = typeof(UpdateDelegates).GetMethod("UpdateAndExecute" + (array.Length - 1), BindingFlags.Static | BindingFlags.NonPublic);
					methodInfo2 = typeof(UpdateDelegates).GetMethod("NoMatch" + (array.Length - 1), BindingFlags.Static | BindingFlags.NonPublic);
				}
				if (methodInfo != null)
				{
					CallSite<T>.s_cachedNoMatch = (T)((object)CallSite<T>.CreateDelegateHelper(typeFromHandle, methodInfo2.MakeGenericMethod(array)));
					return (T)((object)CallSite<T>.CreateDelegateHelper(typeFromHandle, methodInfo.MakeGenericMethod(array)));
				}
			}
			CallSite<T>.s_cachedNoMatch = this.CreateCustomNoMatchDelegate(invokeMethod);
			return this.CreateCustomUpdateDelegate(invokeMethod);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004B84F File Offset: 0x00049A4F
		[SecuritySafeCritical]
		private static Delegate CreateDelegateHelper(Type delegateType, MethodInfo method)
		{
			return method.CreateDelegate(delegateType);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0004B858 File Offset: 0x00049A58
		private static bool IsSimpleSignature(MethodInfo invoke, out Type[] sig)
		{
			ParameterInfo[] parametersCached = invoke.GetParametersCached();
			ContractUtils.Requires(parametersCached.Length != 0 && parametersCached[0].ParameterType == typeof(CallSite), "T");
			Type[] array = new Type[(invoke.ReturnType != typeof(void)) ? parametersCached.Length : (parametersCached.Length - 1)];
			bool flag = true;
			for (int i = 1; i < parametersCached.Length; i++)
			{
				ParameterInfo parameterInfo = parametersCached[i];
				if (parameterInfo.IsByRefParameter())
				{
					flag = false;
				}
				array[i - 1] = parameterInfo.ParameterType;
			}
			if (invoke.ReturnType != typeof(void))
			{
				array[array.Length - 1] = invoke.ReturnType;
			}
			sig = array;
			return flag;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0004B910 File Offset: 0x00049B10
		private T CreateCustomUpdateDelegate(MethodInfo invoke)
		{
			Type returnType = invoke.GetReturnType();
			bool flag = returnType == typeof(void);
			ArrayBuilder<Expression> arrayBuilder = new ArrayBuilder<Expression>(13);
			ArrayBuilder<ParameterExpression> arrayBuilder2 = new ArrayBuilder<ParameterExpression>(8 + (flag ? 0 : 1));
			ParameterExpression[] array = Array.ConvertAll<ParameterInfo, ParameterExpression>(invoke.GetParametersCached(), (ParameterInfo p) => Expression.Parameter(p.ParameterType, p.Name));
			LabelTarget labelTarget = Expression.Label(returnType);
			Type[] array2 = new Type[] { typeof(T) };
			ParameterExpression parameterExpression = array[0];
			ParameterExpression[] array3 = array.RemoveFirst<ParameterExpression>();
			ParameterExpression parameterExpression2 = Expression.Variable(typeof(CallSite<T>), "this");
			arrayBuilder2.UncheckedAdd(parameterExpression2);
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression2, Expression.Convert(parameterExpression, parameterExpression2.Type)));
			ParameterExpression parameterExpression3 = Expression.Variable(typeof(T[]), "applicable");
			arrayBuilder2.UncheckedAdd(parameterExpression3);
			ParameterExpression parameterExpression4 = Expression.Variable(typeof(T), "rule");
			arrayBuilder2.UncheckedAdd(parameterExpression4);
			ParameterExpression parameterExpression5 = Expression.Variable(typeof(T), "originalRule");
			arrayBuilder2.UncheckedAdd(parameterExpression5);
			Expression expression = Expression.Field(parameterExpression2, "Target");
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression5, expression));
			ParameterExpression parameterExpression6 = null;
			if (!flag)
			{
				arrayBuilder2.UncheckedAdd(parameterExpression6 = Expression.Variable(labelTarget.Type, "result"));
			}
			ParameterExpression parameterExpression7 = Expression.Variable(typeof(int), "count");
			arrayBuilder2.UncheckedAdd(parameterExpression7);
			ParameterExpression parameterExpression8 = Expression.Variable(typeof(int), "index");
			arrayBuilder2.UncheckedAdd(parameterExpression8);
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression, Expression.Call(CachedReflectionInfo.CallSiteOps_CreateMatchmaker.MakeGenericMethod(array2), parameterExpression2)));
			Expression expression2 = Expression.Call(CachedReflectionInfo.CallSiteOps_GetMatch, parameterExpression);
			Expression expression3 = Expression.Call(CachedReflectionInfo.CallSiteOps_ClearMatch, parameterExpression);
			Expression expression4 = Expression.Invoke(parameterExpression4, new TrueReadOnlyCollection<Expression>(array));
			Expression expression5 = Expression.Call(CachedReflectionInfo.CallSiteOps_UpdateRules.MakeGenericMethod(array2), parameterExpression2, parameterExpression8);
			Expression expression6;
			if (flag)
			{
				expression6 = Expression.Block(expression4, Expression.IfThen(expression2, Expression.Block(expression5, Expression.Return(labelTarget))));
			}
			else
			{
				expression6 = Expression.Block(Expression.Assign(parameterExpression6, expression4), Expression.IfThen(expression2, Expression.Block(expression5, Expression.Return(labelTarget, parameterExpression6))));
			}
			Expression expression7 = Expression.Assign(parameterExpression4, Expression.ArrayAccess(parameterExpression3, new TrueReadOnlyCollection<Expression>(new Expression[] { parameterExpression8 })));
			Expression expression8 = expression7;
			LabelTarget labelTarget2 = Expression.Label();
			Expression expression9 = Expression.IfThen(Expression.Equal(parameterExpression8, parameterExpression7), Expression.Break(labelTarget2));
			Expression expression10 = Expression.PreIncrementAssign(parameterExpression8);
			arrayBuilder.UncheckedAdd(Expression.IfThen(Expression.NotEqual(Expression.Assign(parameterExpression3, Expression.Call(CachedReflectionInfo.CallSiteOps_GetRules.MakeGenericMethod(array2), parameterExpression2)), Expression.Constant(null, parameterExpression3.Type)), Expression.Block(Expression.Assign(parameterExpression7, Expression.ArrayLength(parameterExpression3)), Expression.Assign(parameterExpression8, Utils.Constant(0)), Expression.Loop(Expression.Block(expression9, expression8, Expression.IfThen(Expression.NotEqual(Expression.Convert(parameterExpression4, typeof(object)), Expression.Convert(parameterExpression5, typeof(object))), Expression.Block(Expression.Assign(expression, parameterExpression4), expression6, expression3)), expression10), labelTarget2, null))));
			ParameterExpression parameterExpression9 = Expression.Variable(typeof(RuleCache<T>), "cache");
			arrayBuilder2.UncheckedAdd(parameterExpression9);
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression9, Expression.Call(CachedReflectionInfo.CallSiteOps_GetRuleCache.MakeGenericMethod(array2), parameterExpression2)));
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression3, Expression.Call(CachedReflectionInfo.CallSiteOps_GetCachedRules.MakeGenericMethod(array2), parameterExpression9)));
			if (flag)
			{
				expression6 = Expression.Block(expression4, Expression.IfThen(expression2, Expression.Return(labelTarget)));
			}
			else
			{
				expression6 = Expression.Block(Expression.Assign(parameterExpression6, expression4), Expression.IfThen(expression2, Expression.Return(labelTarget, parameterExpression6)));
			}
			Expression expression11 = Expression.TryFinally(expression6, Expression.IfThen(expression2, Expression.Block(Expression.Call(CachedReflectionInfo.CallSiteOps_AddRule.MakeGenericMethod(array2), parameterExpression2, parameterExpression4), Expression.Call(CachedReflectionInfo.CallSiteOps_MoveRule.MakeGenericMethod(array2), parameterExpression9, parameterExpression4, parameterExpression8))));
			expression8 = Expression.Assign(expression, expression7);
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression8, Utils.Constant(0)));
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression7, Expression.ArrayLength(parameterExpression3)));
			arrayBuilder.UncheckedAdd(Expression.Loop(Expression.Block(expression9, expression8, expression11, expression3, expression10), labelTarget2, null));
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression4, Expression.Constant(null, parameterExpression4.Type)));
			ParameterExpression parameterExpression10 = Expression.Variable(typeof(object[]), "args");
			Expression[] array4 = Array.ConvertAll<ParameterExpression, Expression>(array3, (ParameterExpression p) => CallSite<T>.Convert(p, typeof(object)));
			arrayBuilder2.UncheckedAdd(parameterExpression10);
			arrayBuilder.UncheckedAdd(Expression.Assign(parameterExpression10, Expression.NewArrayInit(typeof(object), new TrueReadOnlyCollection<Expression>(array4))));
			Expression expression12 = Expression.Assign(expression, parameterExpression5);
			expression8 = Expression.Assign(expression, Expression.Assign(parameterExpression4, Expression.Call(CachedReflectionInfo.CallSiteOps_Bind.MakeGenericMethod(array2), Expression.Property(parameterExpression2, "Binder"), parameterExpression2, parameterExpression10)));
			expression11 = Expression.TryFinally(expression6, Expression.IfThen(expression2, Expression.Call(CachedReflectionInfo.CallSiteOps_AddRule.MakeGenericMethod(array2), parameterExpression2, parameterExpression4)));
			arrayBuilder.UncheckedAdd(Expression.Loop(Expression.Block(expression12, expression8, expression11, expression3), null, null));
			arrayBuilder.UncheckedAdd(Expression.Default(labelTarget.Type));
			return Expression.Lambda<T>(Expression.Label(labelTarget, Expression.Block(arrayBuilder2.ToReadOnly<ParameterExpression>(), arrayBuilder.ToReadOnly<Expression>())), "CallSite.Target", true, new TrueReadOnlyCollection<ParameterExpression>(array)).Compile();
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0004BECC File Offset: 0x0004A0CC
		private T CreateCustomNoMatchDelegate(MethodInfo invoke)
		{
			ParameterExpression[] array = Array.ConvertAll<ParameterInfo, ParameterExpression>(invoke.GetParametersCached(), (ParameterInfo p) => Expression.Parameter(p.ParameterType, p.Name));
			return Expression.Lambda<T>(Expression.Block(Expression.Call(typeof(CallSiteOps).GetMethod("SetNotMatched"), array[0]), Expression.Default(invoke.GetReturnType())), new TrueReadOnlyCollection<ParameterExpression>(array)).Compile();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0004BF40 File Offset: 0x0004A140
		private static Expression Convert(Expression arg, Type type)
		{
			if (TypeUtils.AreReferenceAssignable(type, arg.Type))
			{
				return arg;
			}
			return Expression.Convert(arg, type);
		}

		/// <summary>The Level 0 cache - a delegate specialized based on the site history.</summary>
		// Token: 0x04000AB2 RID: 2738
		public T Target;

		// Token: 0x04000AB3 RID: 2739
		internal T[] Rules;

		// Token: 0x04000AB4 RID: 2740
		private static T s_cachedUpdate;

		// Token: 0x04000AB5 RID: 2741
		private static volatile T s_cachedNoMatch;

		// Token: 0x04000AB6 RID: 2742
		private const int MaxRules = 10;
	}
}
