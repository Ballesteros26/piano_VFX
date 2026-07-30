using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic get index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200032F RID: 815
	public abstract class GetIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.GetIndexBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060018A3 RID: 6307 RVA: 0x0004FEB9 File Offset: 0x0004E0B9
		protected GetIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0004FED3 File Offset: 0x0004E0D3
		public CallInfo CallInfo { get; }

		/// <summary>Performs the binding of the dynamic get index operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get index operation.</param>
		/// <param name="args">An array of arguments of the dynamic get index operation.</param>
		// Token: 0x060018A6 RID: 6310 RVA: 0x0004FEDB File Offset: 0x0004E0DB
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindGetIndex(this, args);
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic get index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get index operation.</param>
		/// <param name="indexes">The arguments of the dynamic get index operation.</param>
		// Token: 0x060018A8 RID: 6312 RVA: 0x0004FEFB File Offset: 0x0004E0FB
		public DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackGetIndex(target, indexes, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic get index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get index operation.</param>
		/// <param name="indexes">The arguments of the dynamic get index operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018A9 RID: 6313
		public abstract DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);
	}
}
