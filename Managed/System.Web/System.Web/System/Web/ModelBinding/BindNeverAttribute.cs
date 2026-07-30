using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an attribute that specifies that model binding should exclude a property.</summary>
	// Token: 0x02000515 RID: 1301
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class BindNeverAttribute : BindingBehaviorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindNeverAttribute" /> class.</summary>
		// Token: 0x060039CE RID: 14798 RVA: 0x0009CCAD File Offset: 0x0009AEAD
		public BindNeverAttribute()
			: base(BindingBehavior.Never)
		{
		}
	}
}
