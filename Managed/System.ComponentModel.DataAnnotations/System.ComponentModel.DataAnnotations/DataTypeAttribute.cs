using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the name of an additional type to associate with a data field.</summary>
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class DataTypeAttribute : ValidationAttribute
	{
		/// <summary>Gets the type that is associated with the data field.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DataAnnotations.DataType" /> values.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B43 File Offset: 0x00000D43
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002B4B File Offset: 0x00000D4B
		public DataType DataType { get; private set; }

		/// <summary>Gets the name of custom field template that is associated with the data field.</summary>
		/// <returns>The name of the custom field template that is associated with the data field.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002B54 File Offset: 0x00000D54
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002B5C File Offset: 0x00000D5C
		public string CustomDataType { get; private set; }

		/// <summary>Returns the name of the type that is associated with the data field.</summary>
		/// <returns>The name of the type associated with the data field.</returns>
		// Token: 0x06000043 RID: 67 RVA: 0x00002B65 File Offset: 0x00000D65
		public virtual string GetDataTypeName()
		{
			this.EnsureValidDataType();
			if (this.DataType == DataType.Custom)
			{
				return this.CustomDataType;
			}
			return DataTypeAttribute._dataTypeStrings[(int)this.DataType];
		}

		/// <summary>Gets a data-field display format.</summary>
		/// <returns>The data-field display format.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B88 File Offset: 0x00000D88
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002B90 File Offset: 0x00000D90
		public DisplayFormatAttribute DisplayFormat { get; protected set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DataTypeTypeAttribute" /> class by using the specified type name.</summary>
		/// <param name="dataType">The name of the type to associate with the data field.</param>
		// Token: 0x06000046 RID: 70 RVA: 0x00002B9C File Offset: 0x00000D9C
		public DataTypeAttribute(DataType dataType)
		{
			this.DataType = dataType;
			switch (dataType)
			{
			case DataType.Date:
				this.DisplayFormat = new DisplayFormatAttribute();
				this.DisplayFormat.DataFormatString = "{0:d}";
				this.DisplayFormat.ApplyFormatInEditMode = true;
				return;
			case DataType.Time:
				this.DisplayFormat = new DisplayFormatAttribute();
				this.DisplayFormat.DataFormatString = "{0:t}";
				this.DisplayFormat.ApplyFormatInEditMode = true;
				return;
			case DataType.Duration:
			case DataType.PhoneNumber:
				break;
			case DataType.Currency:
				this.DisplayFormat = new DisplayFormatAttribute();
				this.DisplayFormat.DataFormatString = "{0:C}";
				break;
			default:
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DataTypeTypeAttribute" /> class by using the specified field template name.</summary>
		/// <param name="customDataType">The name of the custom field template to associate with the data field.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="customDataType" /> is null or an empty string (""). </exception>
		// Token: 0x06000047 RID: 71 RVA: 0x00002C3E File Offset: 0x00000E3E
		public DataTypeAttribute(string customDataType)
			: this(DataType.Custom)
		{
			this.CustomDataType = customDataType;
		}

		/// <summary>Checks that the value of the data field is valid.</summary>
		/// <returns>true always.</returns>
		/// <param name="value">The data field value to validate.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x00002C4E File Offset: 0x00000E4E
		public override bool IsValid(object value)
		{
			this.EnsureValidDataType();
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C57 File Offset: 0x00000E57
		private void EnsureValidDataType()
		{
			if (this.DataType == DataType.Custom && string.IsNullOrEmpty(this.CustomDataType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The custom DataType string cannot be null or empty.", Array.Empty<object>()));
			}
		}

		// Token: 0x0400005C RID: 92
		private static string[] _dataTypeStrings = Enum.GetNames(typeof(DataType));
	}
}
