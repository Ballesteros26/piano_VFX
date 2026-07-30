using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for control values.</summary>
	// Token: 0x02000707 RID: 1799
	public sealed class ControlValueProvider : SimpleValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ControlValueProvider" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="propertyName">The name of the control property to get the value from.</param>
		// Token: 0x06004BAE RID: 19374 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ControlValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the control property to get the value from.</summary>
		/// <returns>The property name.</returns>
		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object FetchValue(string controlId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
