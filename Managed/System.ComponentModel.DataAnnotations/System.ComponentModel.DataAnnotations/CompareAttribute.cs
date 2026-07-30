using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Provides an attribute that compares two properties.</summary>
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class CompareAttribute : ValidationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.CompareAttribute" /> class.</summary>
		/// <param name="otherProperty">The property to compare with the current property.</param>
		// Token: 0x06000024 RID: 36 RVA: 0x000024D6 File Offset: 0x000006D6
		public CompareAttribute(string otherProperty)
			: base("'{0}' and '{1}' do not match.")
		{
			if (otherProperty == null)
			{
				throw new ArgumentNullException("otherProperty");
			}
			this.OtherProperty = otherProperty;
		}

		/// <summary>Gets the property to compare with the current property.</summary>
		/// <returns>The other property.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002500 File Offset: 0x00000700
		public string OtherProperty { get; private set; }

		/// <summary>Gets the display name of the other property.</summary>
		/// <returns>The display name of the other property.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002509 File Offset: 0x00000709
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002511 File Offset: 0x00000711
		public string OtherPropertyDisplayName { get; internal set; }

		/// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
		/// <returns>The formatted error message.</returns>
		/// <param name="name">The name of the field that caused the validation failure.</param>
		// Token: 0x06000029 RID: 41 RVA: 0x0000251A File Offset: 0x0000071A
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.OtherPropertyDisplayName ?? this.OtherProperty);
		}

		/// <summary>Gets a value that indicates whether the attribute requires validation context.</summary>
		/// <returns>true if the attribute requires validation context; otherwise, false.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000253D File Offset: 0x0000073D
		public override bool RequiresValidationContext
		{
			get
			{
				return true;
			}
		}

		/// <summary>Determines whether a specified object is valid.</summary>
		/// <returns>true if <paramref name="value" /> is valid; otherwise, false.</returns>
		/// <param name="value">The object to validate.</param>
		/// <param name="validationContext">An object that contains information about the validation request.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x00002540 File Offset: 0x00000740
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			PropertyInfo property = validationContext.ObjectType.GetProperty(this.OtherProperty);
			if (property == null)
			{
				return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", this.OtherProperty));
			}
			object value2 = property.GetValue(validationContext.ObjectInstance, null);
			if (!object.Equals(value, value2))
			{
				if (this.OtherPropertyDisplayName == null)
				{
					this.OtherPropertyDisplayName = CompareAttribute.GetDisplayNameForProperty(validationContext.ObjectType, this.OtherProperty);
				}
				return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
			}
			return null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025CC File Offset: 0x000007CC
		private static string GetDisplayNameForProperty(Type containerType, string propertyName)
		{
			PropertyDescriptor propertyDescriptor = CompareAttribute.GetTypeDescriptor(containerType).GetProperties().Find(propertyName, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The property {0}.{1} could not be found.", containerType.FullName, propertyName));
			}
			IEnumerable<Attribute> enumerable = propertyDescriptor.Attributes.Cast<Attribute>();
			DisplayAttribute displayAttribute = enumerable.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			if (displayAttribute != null)
			{
				return displayAttribute.GetName();
			}
			DisplayNameAttribute displayNameAttribute = enumerable.OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			if (displayNameAttribute != null)
			{
				return displayNameAttribute.DisplayName;
			}
			return propertyName;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002642 File Offset: 0x00000842
		private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}
	}
}
