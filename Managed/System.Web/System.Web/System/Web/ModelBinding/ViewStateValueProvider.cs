using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for view state values.</summary>
	// Token: 0x02000740 RID: 1856
	public sealed class ViewStateValueProvider : SimpleValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ViewStateValueProvider" /> class by using the execution context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C7D RID: 19581 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ViewStateValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object FetchValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
