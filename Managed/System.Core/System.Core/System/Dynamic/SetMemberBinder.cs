using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic set member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000336 RID: 822
	public abstract class SetMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.SetMemberBinder" />.</summary>
		/// <param name="name">The name of the member to obtain.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x060018CC RID: 6348 RVA: 0x000500C0 File Offset: 0x0004E2C0
		protected SetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to obtain.</summary>
		/// <returns>The name of the member to obtain.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x000500E1 File Offset: 0x0004E2E1
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the case is ignored, otherwise false.</returns>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x000500E9 File Offset: 0x0004E2E9
		public bool IgnoreCase { get; }

		/// <summary>Performs the binding of the dynamic set member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set member operation.</param>
		/// <param name="args">An array of arguments of the dynamic set member operation.</param>
		// Token: 0x060018D0 RID: 6352 RVA: 0x000500F4 File Offset: 0x0004E2F4
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject dynamicMetaObject = args[0];
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			return target.BindSetMember(this, dynamicMetaObject);
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic set member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set member operation.</param>
		/// <param name="value">The value to set to the member.</param>
		// Token: 0x060018D2 RID: 6354 RVA: 0x0005013E File Offset: 0x0004E33E
		public DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value)
		{
			return this.FallbackSetMember(target, value, null);
		}

		/// <summary>Performs the binding of the dynamic set member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic set member operation.</param>
		/// <param name="value">The value to set to the member.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018D3 RID: 6355
		public abstract DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion);
	}
}
