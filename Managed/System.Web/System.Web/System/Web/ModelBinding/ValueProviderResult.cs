using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the result of retrieving a value from a value provider. </summary>
	// Token: 0x02000527 RID: 1319
	[Serializable]
	public class ValueProviderResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class.</summary>
		// Token: 0x06003A20 RID: 14880 RVA: 0x00002050 File Offset: 0x00000250
		protected ValueProviderResult()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class by using the specified raw value, attempted value, and culture information.</summary>
		/// <param name="rawValue">The raw value.</param>
		/// <param name="attemptedValue">The attempted value.</param>
		/// <param name="culture">The culture information.</param>
		// Token: 0x06003A21 RID: 14881 RVA: 0x0009D3F5 File Offset: 0x0009B5F5
		public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
		{
			this.RawValue = rawValue;
			this.AttemptedValue = attemptedValue;
			this.Culture = culture;
		}

		/// <summary>Gets or sets the raw value that is converted to a string for display.</summary>
		/// <returns>A string representation of the raw value.</returns>
		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06003A22 RID: 14882 RVA: 0x0009D412 File Offset: 0x0009B612
		// (set) Token: 0x06003A23 RID: 14883 RVA: 0x0009D41A File Offset: 0x0009B61A
		public string AttemptedValue { get; protected set; }

		/// <summary>Gets or sets the culture.</summary>
		/// <returns>The culture.</returns>
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x0009D423 File Offset: 0x0009B623
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x0009D43E File Offset: 0x0009B63E
		public CultureInfo Culture
		{
			get
			{
				if (this._instanceCulture == null)
				{
					this._instanceCulture = ValueProviderResult._staticCulture;
				}
				return this._instanceCulture;
			}
			protected set
			{
				this._instanceCulture = value;
			}
		}

		/// <summary>Gets or sets the raw value that is supplied by the value provider.</summary>
		/// <returns>The raw value.</returns>
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x0009D447 File Offset: 0x0009B647
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x0009D44F File Offset: 0x0009B64F
		public object RawValue { get; protected set; }

		// Token: 0x06003A28 RID: 14888 RVA: 0x0009D458 File Offset: 0x0009B658
		private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (text != null && text.Trim().Length == 0)
			{
				return null;
			}
			TypeConverter typeConverter = TypeDescriptor.GetConverter(destinationType);
			bool flag = typeConverter.CanConvertFrom(value.GetType());
			if (!flag)
			{
				typeConverter = TypeDescriptor.GetConverter(value.GetType());
			}
			if (flag || typeConverter.CanConvertTo(destinationType))
			{
				object obj;
				try
				{
					obj = (flag ? typeConverter.ConvertFrom(null, culture, value) : typeConverter.ConvertTo(null, culture, value, destinationType));
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The parameter conversion from type '{0}' to type '{1}' failed. See the inner exception for more information."), value.GetType().FullName, destinationType.FullName), ex);
				}
				return obj;
			}
			if (destinationType.IsEnum && value is int)
			{
				return Enum.ToObject(destinationType, (int)value);
			}
			Type underlyingType = Nullable.GetUnderlyingType(destinationType);
			if (underlyingType != null)
			{
				return ValueProviderResult.ConvertSimpleType(culture, value, underlyingType);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The parameter conversion from type '{0}' to type '{1}' failed because no type converter can convert between these types."), value.GetType().FullName, destinationType.FullName));
		}

		/// <summary>Converts a value that is encapsulated by this result to the specified type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="type">The type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">Conversion was unsuccessful.</exception>
		// Token: 0x06003A29 RID: 14889 RVA: 0x0009D57C File Offset: 0x0009B77C
		public object ConvertTo(Type type)
		{
			return this.ConvertTo(type, null);
		}

		/// <summary>Converts the value that is encapsulated by this result to the specified type by using the specified culture information.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="type">The type.</param>
		/// <param name="culture">The culture information.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">Conversion was unsuccessful.</exception>
		// Token: 0x06003A2A RID: 14890 RVA: 0x0009D586 File Offset: 0x0009B786
		public virtual object ConvertTo(Type type, CultureInfo culture)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return ValueProviderResult.UnwrapPossibleArrayType(culture ?? this.Culture, this.RawValue, type);
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
		private static object UnwrapPossibleArrayType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			Array array = value as Array;
			if (destinationType.IsArray)
			{
				Type elementType = destinationType.GetElementType();
				if (array != null)
				{
					IList list = Array.CreateInstance(elementType, array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						list[i] = ValueProviderResult.ConvertSimpleType(culture, array.GetValue(i), elementType);
					}
					return list;
				}
				object obj = ValueProviderResult.ConvertSimpleType(culture, value, elementType);
				Array array2 = Array.CreateInstance(elementType, 1);
				((IList)array2)[0] = obj;
				return array2;
			}
			else
			{
				if (array == null)
				{
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				if (array.Length > 0)
				{
					value = array.GetValue(0);
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				return null;
			}
		}

		// Token: 0x04001F5D RID: 8029
		private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

		// Token: 0x04001F5E RID: 8030
		private CultureInfo _instanceCulture;
	}
}
