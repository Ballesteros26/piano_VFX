using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using Unity;

namespace System.Runtime.CompilerServices
{
	/// <summary>A dynamic call site base class. This type is used as a parameter type to the dynamic site targets.</summary>
	// Token: 0x020002F2 RID: 754
	public class CallSite
	{
		// Token: 0x060016FC RID: 5884 RVA: 0x0004B445 File Offset: 0x00049645
		internal CallSite(CallSiteBinder binder)
		{
			this._binder = binder;
		}

		/// <summary>Class responsible for binding dynamic operations on the dynamic site.</summary>
		/// <returns>The <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> object responsible for binding dynamic operations.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0004B454 File Offset: 0x00049654
		public CallSiteBinder Binder
		{
			get
			{
				return this._binder;
			}
		}

		/// <summary>Creates a call site with the given delegate type and binder.</summary>
		/// <returns>The new call site.</returns>
		/// <param name="delegateType">The call site delegate type.</param>
		/// <param name="binder">The call site binder.</param>
		// Token: 0x060016FE RID: 5886 RVA: 0x0004B45C File Offset: 0x0004965C
		public static CallSite Create(Type delegateType, CallSiteBinder binder)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			CacheDict<Type, Func<CallSiteBinder, CallSite>> cacheDict = CallSite.s_siteCtors;
			if (cacheDict == null)
			{
				cacheDict = (CallSite.s_siteCtors = new CacheDict<Type, Func<CallSiteBinder, CallSite>>(100));
			}
			MethodInfo methodInfo = null;
			Func<CallSiteBinder, CallSite> func;
			if (!cacheDict.TryGetValue(delegateType, out func))
			{
				methodInfo = typeof(CallSite<>).MakeGenericType(new Type[] { delegateType }).GetMethod("Create");
				if (delegateType.CanCache())
				{
					func = (Func<CallSiteBinder, CallSite>)methodInfo.CreateDelegate(typeof(Func<CallSiteBinder, CallSite>));
					cacheDict.Add(delegateType, func);
				}
			}
			if (func != null)
			{
				return func(binder);
			}
			return (CallSite)methodInfo.Invoke(null, new object[] { binder });
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0000220F File Offset: 0x0000040F
		internal CallSite()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AAE RID: 2734
		internal const string CallSiteTargetMethodName = "CallSite.Target";

		// Token: 0x04000AAF RID: 2735
		private static volatile CacheDict<Type, Func<CallSiteBinder, CallSite>> s_siteCtors;

		// Token: 0x04000AB0 RID: 2736
		internal readonly CallSiteBinder _binder;

		// Token: 0x04000AB1 RID: 2737
		internal bool _match;
	}
}
