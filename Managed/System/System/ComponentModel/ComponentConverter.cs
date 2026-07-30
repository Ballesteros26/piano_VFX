using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert components to and from various other representations.</summary>
	// Token: 0x02000247 RID: 583
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ComponentConverter : ReferenceConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentConverter" /> class.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to associate with this component converter. </param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x0004E96B File Offset: 0x0004CB6B
		public ComponentConverter(Type type)
			: base(type)
		{
		}

		/// <summary>Gets a collection of properties for the type of component specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x060012D7 RID: 4823 RVA: 0x0004E974 File Offset: 0x0004CB74
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x060012D8 RID: 4824 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
