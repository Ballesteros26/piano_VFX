using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model validator for a specified validation type.</summary>
	/// <typeparam name="TAttribute">The type of the validation attribute.</typeparam>
	// Token: 0x02000712 RID: 1810
	public class DataAnnotationsModelValidator<TAttribute> : DataAnnotationsModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidator`1" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attribute">The validation attribute.</param>
		// Token: 0x06004BE2 RID: 19426 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DataAnnotationsModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context, TAttribute attribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the validation attribute.</summary>
		/// <returns>The validation attribute.</returns>
		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x000CAC78 File Offset: 0x000C8E78
		protected new TAttribute Attribute
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TAttribute);
			}
		}
	}
}
