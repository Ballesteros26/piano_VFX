using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic delete member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000312 RID: 786
	public abstract class DeleteMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" />.</summary>
		/// <param name="name">The name of the member to delete.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x060017BD RID: 6077 RVA: 0x0004D642 File Offset: 0x0004B842
		protected DeleteMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>Gets the name of the member to delete.</summary>
		/// <returns>The name of the member to delete.</returns>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0004D663 File Offset: 0x0004B863
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the string comparison should ignore the case, otherwise false.</returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0004D66B File Offset: 0x0004B86B
		public bool IgnoreCase { get; }

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00035F1E File Offset: 0x0003411E
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(void);
			}
		}

		/// <summary>Performs the binding of the dynamic delete member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		// Token: 0x060017C1 RID: 6081 RVA: 0x0004D673 File Offset: 0x0004B873
		public DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target)
		{
			return this.FallbackDeleteMember(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic delete member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060017C2 RID: 6082
		public abstract DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic delete member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <param name="args">An array of arguments of the dynamic delete member operation.</param>
		// Token: 0x060017C3 RID: 6083 RVA: 0x0004D67D File Offset: 0x0004B87D
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindDeleteMember(this);
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}
	}
}
