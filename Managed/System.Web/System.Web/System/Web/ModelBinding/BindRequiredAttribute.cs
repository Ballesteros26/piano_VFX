using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an attribute that specifies that a property is required for model binding.</summary>
	// Token: 0x02000516 RID: 1302
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class BindRequiredAttribute : BindingBehaviorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindRequiredAttribute" /> class.</summary>
		// Token: 0x060039CF RID: 14799 RVA: 0x0009CCB6 File Offset: 0x0009AEB6
		public BindRequiredAttribute()
			: base(BindingBehavior.Required)
		{
		}
	}
}
