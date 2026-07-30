using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that a data field value is required.</summary>
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class RequiredAttribute : ValidationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> class.</summary>
		// Token: 0x060000F8 RID: 248 RVA: 0x000041AF File Offset: 0x000023AF
		public RequiredAttribute()
			: base(() => "The {0} field is required.")
		{
		}

		/// <summary>Gets or sets a value that indicates whether an empty string is allowed.</summary>
		/// <returns>true if an empty string is allowed; otherwise, false. The default value is false.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000041D6 File Offset: 0x000023D6
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000041DE File Offset: 0x000023DE
		public bool AllowEmptyStrings { get; set; }

		/// <summary>Checks that the value of the required data field is not empty.</summary>
		/// <returns>true if validation is successful; otherwise, false.</returns>
		/// <param name="value">The data field value to validate.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The data field value was null.</exception>
		// Token: 0x060000FB RID: 251 RVA: 0x000041E8 File Offset: 0x000023E8
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return false;
			}
			string text = value as string;
			return text == null || this.AllowEmptyStrings || text.Trim().Length != 0;
		}
	}
}
