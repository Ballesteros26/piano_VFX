using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies how data fields are displayed and formatted by ASP.NET Dynamic Data.</summary>
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class DisplayFormatAttribute : Attribute
	{
		/// <summary>Gets or sets the display format for the field value.</summary>
		/// <returns>A formatting string that specifies the display format for the value of the data field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002FEF File Offset: 0x000011EF
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002FF7 File Offset: 0x000011F7
		public string DataFormatString { get; set; }

		/// <summary>Gets or sets the text that is displayed for a field when the field's value is null.</summary>
		/// <returns>The text that is displayed for a field when the field's value is null. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003000 File Offset: 0x00001200
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003008 File Offset: 0x00001208
		public string NullDisplayText { get; set; }

		/// <summary>Gets or sets a value that indicates whether empty string values ("") are automatically converted to null when the data field is updated in the data source.</summary>
		/// <returns>true if empty string values are automatically converted to null; otherwise, false. The default is true.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003011 File Offset: 0x00001211
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003019 File Offset: 0x00001219
		public bool ConvertEmptyStringToNull { get; set; }

		/// <summary>Gets or sets a value that indicates whether the formatting string that is specified by the <see cref="P:System.ComponentModel.DataAnnotations.DisplayFormatAttribute.DataFormatString" /> property is applied to the field value when the data field is in edit mode.</summary>
		/// <returns>true if the formatting string applies to the field value in edit mode; otherwise, false. The default is false.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003022 File Offset: 0x00001222
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000302A File Offset: 0x0000122A
		public bool ApplyFormatInEditMode { get; set; }

		/// <summary>Gets or sets a value that indicates whether the field should be HTML-encoded.</summary>
		/// <returns>true if the field should be HTML-encoded; otherwise, false.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003033 File Offset: 0x00001233
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000303B File Offset: 0x0000123B
		public bool HtmlEncode { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DisplayFormatAttribute" /> class. </summary>
		// Token: 0x06000079 RID: 121 RVA: 0x00003044 File Offset: 0x00001244
		public DisplayFormatAttribute()
		{
			this.ConvertEmptyStringToNull = true;
			this.HtmlEncode = true;
		}
	}
}
