using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic create operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000310 RID: 784
	public abstract class CreateInstanceBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new intsance of the <see cref="T:System.Dynamic.CreateInstanceBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060017AF RID: 6063 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		protected CreateInstanceBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0004D5C2 File Offset: 0x0004B7C2
		public CallInfo CallInfo { get; }

		/// <summary>Performs the binding of the dynamic create operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">The arguments of the dynamic create operation.</param>
		// Token: 0x060017B2 RID: 6066 RVA: 0x0004D5CA File Offset: 0x0004B7CA
		public DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackCreateInstance(target, args, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic create operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">The arguments of the dynamic create operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060017B3 RID: 6067
		public abstract DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic create operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">An array of arguments of the dynamic create operation.</param>
		// Token: 0x060017B4 RID: 6068 RVA: 0x0004D5D5 File Offset: 0x0004B7D5
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindCreateInstance(this, args);
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}
	}
}
