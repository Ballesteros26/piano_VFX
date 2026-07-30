using System;
using System.Globalization;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides CodeDOM resource serialization services. This class cannot be inherited.</summary>
	// Token: 0x02000149 RID: 329
	public sealed class CodeDomLocalizationProvider : IDisposable, IDesignerSerializationProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationProvider" /> class. </summary>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> used by the localization provider to add its extender properties.</param>
		/// <param name="model">A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationModel" /> value indicating the localization model to be used by the CodeDOM resource adapter </param>
		// Token: 0x060009F2 RID: 2546 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationProvider" /> class. </summary>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> used by the localization provider to add its extender properties.</param>
		/// <param name="model">A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationModel" /> value indicating the localization model to be used by the CodeDOM resource adapter </param>
		/// <param name="supportedCultures">An array of cultures that this resource adapter should support.</param>
		// Token: 0x060009F3 RID: 2547 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model, CultureInfo[] supportedCultures)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationProvider" />.</summary>
		// Token: 0x060009F4 RID: 2548 RVA: 0x00002432 File Offset: 0x00000632
		[MonoTODO]
		public void Dispose()
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationProvider.GetSerializer(System.ComponentModel.Design.Serialization.IDesignerSerializationManager,System.Object,System.Type,System.Type)" />.</summary>
		/// <returns>An instance of a serializer of the type requested, or null if the request cannot be satisfied.</returns>
		/// <param name="manager">The serialization manager requesting the serializer. </param>
		/// <param name="currentSerializer">An instance of the current serializer of the specified type. This can be null if no serializer of the specified type exists. </param>
		/// <param name="objectType">The data type of the object to serialize. </param>
		/// <param name="serializerType">The data type of the serializer to create. </param>
		// Token: 0x060009F5 RID: 2549 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		object IDesignerSerializationProvider.GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			throw new NotImplementedException();
		}
	}
}
