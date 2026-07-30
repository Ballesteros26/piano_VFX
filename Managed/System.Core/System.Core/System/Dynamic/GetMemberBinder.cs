using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic get member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000330 RID: 816
	public abstract class GetMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.GetMemberBinder" />.</summary>
		/// <param name="name">The name of the member to obtain.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x060018AA RID: 6314 RVA: 0x0004FF06 File Offset: 0x0004E106
		protected GetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to obtain.</summary>
		/// <returns>The name of the member to obtain.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0004FF27 File Offset: 0x0004E127
		public string Name { get; }

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the case is ignored, otherwise false.</returns>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0004FF2F File Offset: 0x0004E12F
		public bool IgnoreCase { get; }

		/// <summary>Performs the binding of the dynamic get member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get member operation.</param>
		// Token: 0x060018AE RID: 6318 RVA: 0x0004FF37 File Offset: 0x0004E137
		public DynamicMetaObject FallbackGetMember(DynamicMetaObject target)
		{
			return this.FallbackGetMember(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic get member operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060018AF RID: 6319
		public abstract DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic get member operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <param name="args">An array of arguments of the dynamic get member operation.</param>
		// Token: 0x060018B0 RID: 6320 RVA: 0x0004FF41 File Offset: 0x0004E141
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindGetMember(this);
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}
	}
}
