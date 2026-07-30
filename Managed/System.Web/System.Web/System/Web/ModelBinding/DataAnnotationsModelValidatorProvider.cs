using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Implements the default validator provider.</summary>
	// Token: 0x02000710 RID: 1808
	public class DataAnnotationsModelValidatorProvider : AssociatedValidatorProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidatorProvider" /> class.</summary>
		// Token: 0x06004BD2 RID: 19410 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public DataAnnotationsModelValidatorProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a value that indicates whether non-nullable value types are required.</summary>
		/// <returns>true if non-nullable value types are required; otherwise, false.</returns>
		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x000CAC5C File Offset: 0x000C8E5C
		// (set) Token: 0x06004BD4 RID: 19412 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static bool AddImplicitRequiredAttributeForValueTypes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection of validators for the model.</summary>
		/// <returns>The collection of validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attributes">The validation attributes.</param>
		// Token: 0x06004BD5 RID: 19413 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context, IEnumerable<Attribute> attributes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Registers an adapter for client-side validation.</summary>
		/// <param name="attributeType">The type of the validation attribute.</param>
		/// <param name="adapterType">The type of the adapter.</param>
		// Token: 0x06004BD6 RID: 19414 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterAdapter(Type attributeType, Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter factory for the validation provider.</summary>
		/// <param name="attributeType">The type of the attribute.</param>
		/// <param name="factory">The factory that will be used to create the validator object for the specified attribute.</param>
		// Token: 0x06004BD7 RID: 19415 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterAdapterFactory(Type attributeType, DataAnnotationsModelValidationFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers the default adapter.</summary>
		/// <param name="adapterType">The type of the adapter.</param>
		// Token: 0x06004BD8 RID: 19416 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterDefaultAdapter(Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers the default adapter factory.</summary>
		/// <param name="factory">The factory that will be used to create the validator object for the default adapter.</param>
		// Token: 0x06004BD9 RID: 19417 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterDefaultAdapterFactory(DataAnnotationsModelValidationFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter for default object validation.</summary>
		/// <param name="adapterType">The type of the adapter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not derive from <see cref="T:System.Web.ModelBinding.ModelValidator" />.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not have a public constructor that accepts two parameters that are typed as <see cref="T:System.Web.ModelBinding.ModelMetadata" /> and <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" />.</exception>
		// Token: 0x06004BDA RID: 19418 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterDefaultValidatableObjectAdapter(Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter factory for the default object validation provider.</summary>
		/// <param name="factory">The factory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="factory" /> is null.</exception>
		// Token: 0x06004BDB RID: 19419 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterDefaultValidatableObjectAdapterFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter for object validation.</summary>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="adapterType">The type of the adapter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="adapterType" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The model type does not implement the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not implement <see cref="T:System.Web.ModelBinding.ModelValidator" />.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not have a public constructor that accepts two parameters that are typed <see cref="T:System.Web.ModelBinding.ModelMetadata" /> and <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" />.</exception>
		// Token: 0x06004BDC RID: 19420 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterValidatableObjectAdapter(Type modelType, Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter factory for the object validation provider.</summary>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="factory">The factory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="factory" /> or <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The model type does not implement the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</exception>
		// Token: 0x06004BDD RID: 19421 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void RegisterValidatableObjectAdapterFactory(Type modelType, DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
