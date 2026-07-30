using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic set index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000335 RID: 821
	public abstract class SetIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.SetIndexBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060018C5 RID: 6341 RVA: 0x0005002D File Offset: 0x0004E22D
		protected SetIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00050047 File Offset: 0x0004E247
		public CallInfo CallInfo { get; }

		/// <summary>Performs the binding of the dynamic set index operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="args">An array of arguments of the dynamic set index operation.</param>
		// Token: 0x060018C8 RID: 6344 RVA: 0x00050050 File Offset: 0x0004E250
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length >= 2, "args");
			DynamicMetaObject dynamicMetaObject = args[args.Length - 1];
			DynamicMetaObject[] array = args.RemoveLast<DynamicMetaObject>();
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(array, "args");
			return target.BindSetIndex(this, array, dynamicMetaObject);
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic set index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="indexes">The arguments of the dynamic set index operation.</param>
		/// <param name="value">The value to set to the collection.</param>
		// Token: 0x060018CA RID: 6346 RVA: 0x000500B4 File Offset: 0x0004E2B4
		public DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			return this.FallbackSetIndex(target, indexes, value, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic set index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="indexes">The arguments of the dynamic set index operation.</param>
		/// <param name="value">The value to set to the collection.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018CB RID: 6347
		public abstract DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value, DynamicMetaObject errorSuggestion);
	}
}
