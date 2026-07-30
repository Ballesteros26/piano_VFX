using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents an area within a <see cref="T:System.Windows.Forms.LinkLabel" /> control that represents a hyperlink within the control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000205 RID: 517
	[TypeConverter(typeof(LinkArea.LinkAreaConverter))]
	[Serializable]
	public struct LinkArea
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkArea" /> class.</summary>
		/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />. </param>
		/// <param name="length">The number of characters, after the starting character, to include in the link area. </param>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x00077FC4 File Offset: 0x000761C4
		public LinkArea(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		/// <summary>Gets or sets the starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>The location within the text of the <see cref="T:System.Windows.Forms.LinkLabel" /> control where the link starts.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x00077FD4 File Offset: 0x000761D4
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x00077FDC File Offset: 0x000761DC
		public int Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		/// <summary>Gets or sets the number of characters in the link area.</summary>
		/// <returns>The number of characters, including spaces, in the link area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x00077FE8 File Offset: 0x000761E8
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x00077FF0 File Offset: 0x000761F0
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.LinkArea" /> is empty.</summary>
		/// <returns>true if the specified start and length return an empty link area; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00077FFC File Offset: 0x000761FC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.start == 0 && this.length == 0;
			}
		}

		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
		/// <param name="o"></param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001FFC RID: 8188 RVA: 0x00078018 File Offset: 0x00076218
		public override bool Equals(object o)
		{
			if (!(o is LinkArea))
			{
				return false;
			}
			LinkArea linkArea = (LinkArea)o;
			return linkArea.Start == this.start && linkArea.Length == this.length;
		}

		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001FFD RID: 8189 RVA: 0x00078060 File Offset: 0x00076260
		public override int GetHashCode()
		{
			return (this.start << 4) | this.length;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00078074 File Offset: 0x00076274
		public override string ToString()
		{
			return string.Format("{{Start={0}, Length={1}}}", this.start.ToString(), this.length.ToString());
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are equal.</summary>
		/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are equal; otherwise, false.</returns>
		/// <param name="linkArea1">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		/// <param name="linkArea2">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		// Token: 0x06001FFF RID: 8191 RVA: 0x000780A4 File Offset: 0x000762A4
		public static bool operator ==(LinkArea linkArea1, LinkArea linkArea2)
		{
			return linkArea1.Length == linkArea2.Length && linkArea1.Start == linkArea2.Start;
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are not equal.</summary>
		/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are not equal; otherwise, false.</returns>
		/// <param name="linkArea1">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		/// <param name="linkArea2">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
		// Token: 0x06002000 RID: 8192 RVA: 0x000780D8 File Offset: 0x000762D8
		public static bool operator !=(LinkArea linkArea1, LinkArea linkArea2)
		{
			return !(linkArea1 == linkArea2);
		}

		// Token: 0x04001169 RID: 4457
		private int start;

		// Token: 0x0400116A RID: 4458
		private int length;

		/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.LinkArea.LinkAreaConverter" /> objects to and from various other representations.</summary>
		// Token: 0x02000206 RID: 518
		public class LinkAreaConverter : TypeConverter
		{
			/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
			/// <returns>True if this object can perform the conversion.</returns>
			/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
			/// <param name="sourceType">The type you wish to convert from. </param>
			// Token: 0x06002002 RID: 8194 RVA: 0x000780EC File Offset: 0x000762EC
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
			/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
			// Token: 0x06002003 RID: 8195 RVA: 0x00078108 File Offset: 0x00076308
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
			}

			/// <summary>Converts the given object to the converter's native type.</summary>
			/// <returns>The converted object. This will throw an exception if the conversion could not be performed.</returns>
			/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
			/// <param name="culture">An optional culture info. If not supplied, the current culture is assumed. </param>
			/// <param name="value">The object to convert. </param>
			// Token: 0x06002004 RID: 8196 RVA: 0x00078124 File Offset: 0x00076324
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value == null || !(value is string))
				{
					return base.ConvertFrom(context, culture, value);
				}
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string[] array = ((string)value).Split(culture.TextInfo.ListSeparator.ToCharArray());
				int num = int.Parse(array[0].Trim());
				int num2 = int.Parse(array[1].Trim());
				return new LinkArea(num, num2);
			}

			/// <summary>Converts the given object to another type. The most common types to convert are to and from a string object. The default implementation will make a call to <see cref="M:System.Windows.Forms.LinkArea.ToString" /> on the object if the object is valid and if the destination type is string. If this cannot convert to the destination type, this will throw a <see cref="T:System.NotSupportedException" />.</summary>
			/// <returns>The converted object.</returns>
			/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
			/// <param name="culture">An optional culture info. If not supplied the current culture is assumed. </param>
			/// <param name="value">The object to convert. </param>
			/// <param name="destinationType">The type to convert the object to. </param>
			// Token: 0x06002005 RID: 8197 RVA: 0x000781A0 File Offset: 0x000763A0
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (value == null || !(value is LinkArea) || destinationType != typeof(string))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				LinkArea linkArea = (LinkArea)value;
				return linkArea.Start.ToString() + culture.TextInfo.ListSeparator + linkArea.Length.ToString();
			}

			/// <summary>Creates an instance of this type, given a set of property values for the object. This is useful for objects that are immutable, but still want to provide changeable properties.</summary>
			/// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.</returns>
			/// <param name="context">A type descriptor through which additional context may be provided. </param>
			/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" />. </param>
			// Token: 0x06002006 RID: 8198 RVA: 0x00078220 File Offset: 0x00076420
			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				return new LinkArea((int)propertyValues["Start"], (int)propertyValues["Length"]);
			}

			/// <summary>Determines if changing a value on this object should require a call to <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</summary>
			/// <returns>Returns true if <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> should be called when a change is made to one or more properties of this object.</returns>
			/// <param name="context">A type descriptor through which additional context may be provided. </param>
			// Token: 0x06002007 RID: 8199 RVA: 0x00078258 File Offset: 0x00076458
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			/// <summary>Retrieves the set of properties for this type. </summary>
			/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this might return null. The default implementation always returns null.</returns>
			/// <param name="context">A type descriptor through which additional context may be provided. </param>
			/// <param name="value">The value of the object to get the properties for. </param>
			/// <param name="attributes">The attributes of the object to get the properties for. </param>
			// Token: 0x06002008 RID: 8200 RVA: 0x0007825C File Offset: 0x0007645C
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				return TypeDescriptor.GetProperties(typeof(LinkArea), attributes);
			}

			/// <summary>Determines if this object supports properties. By default, this is false.</summary>
			/// <returns>Returns true if <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object.</returns>
			/// <param name="context">A type descriptor through which additional context may be provided. </param>
			// Token: 0x06002009 RID: 8201 RVA: 0x00078270 File Offset: 0x00076470
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}
