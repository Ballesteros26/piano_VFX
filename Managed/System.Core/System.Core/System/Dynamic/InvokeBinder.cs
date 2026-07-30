using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the invoke dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000333 RID: 819
	public abstract class InvokeBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.InvokeBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060018B4 RID: 6324 RVA: 0x0004FF6A File Offset: 0x0004E16A
		protected InvokeBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0004FF84 File Offset: 0x0004E184
		public CallInfo CallInfo { get; }

		/// <summary>Performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		// Token: 0x060018B7 RID: 6327 RVA: 0x0004FF8C File Offset: 0x0004E18C
		public DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvoke(target, args, null);
		}

		/// <summary>Performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018B8 RID: 6328
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic invoke operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">An array of arguments of the dynamic invoke operation.</param>
		// Token: 0x060018B9 RID: 6329 RVA: 0x0004FF97 File Offset: 0x0004E197
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvoke(this, args);
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}
	}
}
