using System;

namespace System.Web.ModelBinding
{
	/// <summary>Specifies the binder type for a model type.</summary>
	// Token: 0x0200051B RID: 1307
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class ExtensibleModelBinderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ExtensibleModelBinderAttribute" /> class.</summary>
		/// <param name="binderType">The model binder type.</param>
		// Token: 0x060039E0 RID: 14816 RVA: 0x0009CF37 File Offset: 0x0009B137
		public ExtensibleModelBinderAttribute(Type binderType)
		{
			this.BinderType = binderType;
		}

		/// <summary>Gets the model binder type.</summary>
		/// <returns>The model binder type.</returns>
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x0009CF46 File Offset: 0x0009B146
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x0009CF4E File Offset: 0x0009B14E
		public Type BinderType { get; private set; }

		/// <summary>Gets or sets a value that specifies whether the prefix check should be suppressed.</summary>
		/// <returns>true if the prefix check should be suppressed; otherwise, false.</returns>
		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x0009CF57 File Offset: 0x0009B157
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x0009CF5F File Offset: 0x0009B15F
		public bool SuppressPrefixCheck { get; set; }
	}
}
