using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic delete index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000311 RID: 785
	public abstract class DeleteIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060017B6 RID: 6070 RVA: 0x0004D5F5 File Offset: 0x0004B7F5
		protected DeleteIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00035F1E File Offset: 0x0003411E
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(void);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0004D60F File Offset: 0x0004B80F
		public CallInfo CallInfo { get; }

		/// <summary>Performs the binding of the dynamic delete index operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="args">An array of arguments of the dynamic delete index operation.</param>
		// Token: 0x060017B9 RID: 6073 RVA: 0x0004D617 File Offset: 0x0004B817
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindDeleteIndex(this, args);
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic delete index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="indexes">The arguments of the dynamic delete index operation.</param>
		// Token: 0x060017BB RID: 6075 RVA: 0x0004D637 File Offset: 0x0004B837
		public DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackDeleteIndex(target, indexes, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic delete index operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="indexes">The arguments of the dynamic delete index operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060017BC RID: 6076
		public abstract DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);
	}
}
