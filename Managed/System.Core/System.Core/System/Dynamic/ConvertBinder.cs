using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	/// <summary>Represents the convert dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200030F RID: 783
	public abstract class ConvertBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.ConvertBinder" />.</summary>
		/// <param name="type">The type to convert to.</param>
		/// <param name="explicit">Is true if the conversion should consider explicit conversions; otherwise, false.</param>
		// Token: 0x060017A7 RID: 6055 RVA: 0x0004D53C File Offset: 0x0004B73C
		protected ConvertBinder(Type type, bool @explicit)
		{
			ContractUtils.RequiresNotNull(type, "type");
			this.Type = type;
			this.Explicit = @explicit;
		}

		/// <summary>The type to convert to.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that represents the type to convert to.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0004D55D File Offset: 0x0004B75D
		public Type Type { get; }

		/// <summary>Gets the value indicating if the conversion should consider explicit conversions.</summary>
		/// <returns>True if there is an explicit conversion, otherwise false.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0004D565 File Offset: 0x0004B765
		public bool Explicit { get; }

		/// <summary>Performs the binding of the dynamic convert operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic convert operation.</param>
		// Token: 0x060017AA RID: 6058 RVA: 0x0004D56D File Offset: 0x0004B76D
		public DynamicMetaObject FallbackConvert(DynamicMetaObject target)
		{
			return this.FallbackConvert(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic convert operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		// Token: 0x060017AB RID: 6059
		public abstract DynamicMetaObject FallbackConvert(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic convert operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <param name="args">An array of arguments of the dynamic convert operation.</param>
		// Token: 0x060017AC RID: 6060 RVA: 0x0004D577 File Offset: 0x0004B777
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindConvert(this);
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0004D5A0 File Offset: 0x0004B7A0
		public sealed override Type ReturnType
		{
			get
			{
				return this.Type;
			}
		}
	}
}
