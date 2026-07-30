using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the invoke member dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000334 RID: 820
	public abstract class InvokeMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.InvokeMemberBinder" />.</summary>
		/// <param name="name">The name of the member to invoke.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060018BB RID: 6331 RVA: 0x0004FFB7 File Offset: 0x0004E1B7
		protected InvokeMemberBinder(string name, bool ignoreCase, CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(name, "name");
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to invoke.</summary>
		/// <returns>The name of the member to invoke.</returns>
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0004FFEA File Offset: 0x0004E1EA
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the case is ignored, otherwise false.</returns>
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0004FFF2 File Offset: 0x0004E1F2
		public bool IgnoreCase { get; }

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0004FFFA File Offset: 0x0004E1FA
		public CallInfo CallInfo { get; }

		/// <summary>Performs the binding of the dynamic invoke member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">An array of arguments of the dynamic invoke member operation.</param>
		// Token: 0x060018C0 RID: 6336 RVA: 0x00050002 File Offset: 0x0004E202
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvokeMember(this, args);
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">The arguments of the dynamic invoke member operation.</param>
		// Token: 0x060018C2 RID: 6338 RVA: 0x00050022 File Offset: 0x0004E222
		public DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvokeMember(target, args, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">The arguments of the dynamic invoke member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018C3 RID: 6339
		public abstract DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>When overridden in the derived class, performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018C4 RID: 6340
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);
	}
}
