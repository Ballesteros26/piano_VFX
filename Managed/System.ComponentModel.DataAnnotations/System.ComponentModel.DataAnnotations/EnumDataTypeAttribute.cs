using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Enables a .NET Framework enumeration to be mapped to a data column.</summary>
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class EnumDataTypeAttribute : DataTypeAttribute
	{
		/// <summary>Gets or sets the enumeration type.</summary>
		/// <returns>The enumeration type.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000031A8 File Offset: 0x000013A8
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000031B0 File Offset: 0x000013B0
		public Type EnumType { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.EnumDataTypeAttribute" /> class.</summary>
		/// <param name="enumType">The type of the enumeration.</param>
		// Token: 0x06000085 RID: 133 RVA: 0x000031B9 File Offset: 0x000013B9
		public EnumDataTypeAttribute(Type enumType)
			: base("Enumeration")
		{
			this.EnumType = enumType;
		}

		/// <summary>Checks that the value of the data field is valid.</summary>
		/// <returns>true if the data field value is valid; otherwise, false.</returns>
		/// <param name="value">The data field value to validate.</param>
		// Token: 0x06000086 RID: 134 RVA: 0x000031D0 File Offset: 0x000013D0
		public override bool IsValid(object value)
		{
			if (this.EnumType == null)
			{
				throw new InvalidOperationException("The type provided for EnumDataTypeAttribute cannot be null.");
			}
			if (!this.EnumType.IsEnum)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The type '{0}' needs to represent an enumeration type.", this.EnumType.FullName));
			}
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			if (text != null && string.IsNullOrEmpty(text))
			{
				return true;
			}
			Type type = value.GetType();
			if (type.IsEnum && this.EnumType != type)
			{
				return false;
			}
			if (!type.IsValueType && type != typeof(string))
			{
				return false;
			}
			if (type == typeof(bool) || type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(char))
			{
				return false;
			}
			object obj;
			if (type.IsEnum)
			{
				obj = value;
			}
			else
			{
				try
				{
					if (text != null)
					{
						obj = Enum.Parse(this.EnumType, text, false);
					}
					else
					{
						obj = Enum.ToObject(this.EnumType, value);
					}
				}
				catch (ArgumentException)
				{
					return false;
				}
			}
			if (EnumDataTypeAttribute.IsEnumTypeInFlagsMode(this.EnumType))
			{
				string underlyingTypeValueString = EnumDataTypeAttribute.GetUnderlyingTypeValueString(this.EnumType, obj);
				string text2 = obj.ToString();
				return !underlyingTypeValueString.Equals(text2);
			}
			return Enum.IsDefined(this.EnumType, obj);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003354 File Offset: 0x00001554
		private static bool IsEnumTypeInFlagsMode(Type enumType)
		{
			return enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length != 0;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000336B File Offset: 0x0000156B
		private static string GetUnderlyingTypeValueString(Type enumType, object enumValue)
		{
			return Convert.ChangeType(enumValue, Enum.GetUnderlyingType(enumType), CultureInfo.InvariantCulture).ToString();
		}
	}
}
