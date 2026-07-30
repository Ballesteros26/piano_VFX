using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert object references to and from other representations.</summary>
	// Token: 0x020002C7 RID: 711
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ReferenceConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReferenceConverter" /> class.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to associate with this reference converter. </param>
		// Token: 0x0600167A RID: 5754 RVA: 0x00057C5A File Offset: 0x00055E5A
		public ReferenceConverter(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a reference object using the specified context.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from. </param>
		// Token: 0x0600167B RID: 5755 RVA: 0x00057C69 File Offset: 0x00055E69
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string) && context != null) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the reference type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture used to represent the font. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600167C RID: 5756 RVA: 0x00057C8C File Offset: 0x00055E8C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (!string.Equals(text, ReferenceConverter.none) && context != null)
				{
					IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (referenceService != null)
					{
						object reference = referenceService.GetReference(text);
						if (reference != null)
						{
							return reference;
						}
					}
					IContainer container = context.Container;
					if (container != null)
					{
						object obj = container.Components[text];
						if (obj != null)
						{
							return obj;
						}
					}
				}
				return null;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given value object to the reference type using the specified context and arguments.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture used to represent the font. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600167D RID: 5757 RVA: 0x00057D10 File Offset: 0x00055F10
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value != null)
			{
				if (context != null)
				{
					IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (referenceService != null)
					{
						string name = referenceService.GetName(value);
						if (name != null)
						{
							return name;
						}
					}
				}
				if (!Marshal.IsComObject(value) && value is IComponent)
				{
					ISite site = ((IComponent)value).Site;
					if (site != null)
					{
						string name2 = site.Name;
						if (name2 != null)
						{
							return name2;
						}
					}
				}
				return string.Empty;
			}
			return ReferenceConverter.none;
		}

		/// <summary>Gets a collection of standard values for the reference data type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600167E RID: 5758 RVA: 0x00057DB4 File Offset: 0x00055FB4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object[] array = null;
			if (context != null)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(null);
				IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
				if (referenceService != null)
				{
					object[] references = referenceService.GetReferences(this.type);
					int num = references.Length;
					for (int i = 0; i < num; i++)
					{
						if (this.IsValueAllowed(context, references[i]))
						{
							arrayList.Add(references[i]);
						}
					}
				}
				else
				{
					IContainer container = context.Container;
					if (container != null)
					{
						foreach (object obj in container.Components)
						{
							IComponent component = (IComponent)obj;
							if (component != null && this.type.IsInstanceOfType(component) && this.IsValueAllowed(context, component))
							{
								arrayList.Add(component);
							}
						}
					}
				}
				array = arrayList.ToArray();
				Array.Sort(array, 0, array.Length, new ReferenceConverter.ReferenceComparer(this));
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		/// <summary>Gets a value indicating whether the list of standard values returned from <see cref="M:System.ComponentModel.ReferenceConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exclusive list.</summary>
		/// <returns>true because the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.ReferenceConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list of possible values. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600167F RID: 5759 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.ReferenceConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> can be called to find a common set of values the object supports. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001680 RID: 5760 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Returns a value indicating whether a particular value can be added to the standard values collection.</summary>
		/// <returns>true if the value is allowed and can be added to the standard values collection; false if the value cannot be added to the standard values collection.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides an additional context. </param>
		/// <param name="value">The value to check. </param>
		// Token: 0x06001681 RID: 5761 RVA: 0x000027E2 File Offset: 0x000009E2
		protected virtual bool IsValueAllowed(ITypeDescriptorContext context, object value)
		{
			return true;
		}

		// Token: 0x040013A3 RID: 5027
		private static readonly string none = global::SR.GetString("(none)");

		// Token: 0x040013A4 RID: 5028
		private Type type;

		// Token: 0x020002C8 RID: 712
		private class ReferenceComparer : IComparer
		{
			// Token: 0x06001683 RID: 5763 RVA: 0x00057EDD File Offset: 0x000560DD
			public ReferenceComparer(ReferenceConverter converter)
			{
				this.converter = converter;
			}

			// Token: 0x06001684 RID: 5764 RVA: 0x00057EEC File Offset: 0x000560EC
			public int Compare(object item1, object item2)
			{
				string text = this.converter.ConvertToString(item1);
				string text2 = this.converter.ConvertToString(item2);
				return string.Compare(text, text2, false, CultureInfo.InvariantCulture);
			}

			// Token: 0x040013A5 RID: 5029
			private ReferenceConverter converter;
		}
	}
}
