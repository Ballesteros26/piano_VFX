using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Unity;

namespace System.Web.Security
{
	/// <summary>Validates whether a password field meets the current password requirements for the membership provider.</summary>
	// Token: 0x020004C6 RID: 1222
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MembershipPasswordAttribute : ValidationAttribute
	{
		/// <summary>Gets or sets the MinNonAlphanumericCharactersError attribute.</summary>
		/// <returns>The MinNonAlphanumericCharactersError attribute.</returns>
		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000902F8 File Offset: 0x0008E4F8
		// (set) Token: 0x06003706 RID: 14086 RVA: 0x00090300 File Offset: 0x0008E500
		public string MinNonAlphanumericCharactersError { get; set; }

		/// <summary>Gets or sets the MinPasswordLengthError attribute.</summary>
		/// <returns>The MinPasswordLengthError attribute.</returns>
		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x00090309 File Offset: 0x0008E509
		// (set) Token: 0x06003708 RID: 14088 RVA: 0x00090311 File Offset: 0x0008E511
		public string MinPasswordLengthError { get; set; }

		/// <summary>Gets or sets the minimum required non-alpha numeric characters the attribute uses for validation.</summary>
		/// <returns>The minimum required non-alpha numeric characters the attribute uses for validation.</returns>
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x0009031A File Offset: 0x0008E51A
		// (set) Token: 0x0600370A RID: 14090 RVA: 0x00090322 File Offset: 0x0008E522
		public int MinRequiredNonAlphanumericCharacters { get; set; }

		/// <summary>Gets or sets the minimum required password length this attribute uses for validation.</summary>
		/// <returns>The minimum required password length this attribute uses for validation.</returns>
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x0009032B File Offset: 0x0008E52B
		// (set) Token: 0x0600370C RID: 14092 RVA: 0x00090333 File Offset: 0x0008E533
		public int MinRequiredPasswordLength { get; set; }

		/// <summary>Gets or sets the PasswordStrengthError attribute.</summary>
		/// <returns>The PasswordStrengthError attribute.</returns>
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x0600370D RID: 14093 RVA: 0x0009033C File Offset: 0x0008E53C
		// (set) Token: 0x0600370E RID: 14094 RVA: 0x00090344 File Offset: 0x0008E544
		public string PasswordStrengthError { get; set; }

		/// <summary>Gets or sets the regular expression string representing the password strength the attribute uses for validation.</summary>
		/// <returns>The regular expression string representing the password strength the attribute uses for validation.</returns>
		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x0009034D File Offset: 0x0008E54D
		// (set) Token: 0x06003710 RID: 14096 RVA: 0x00090355 File Offset: 0x0008E555
		public string PasswordStrengthRegularExpression { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Type" /> that contains the resources for the <see cref="P:System.Web.Security.MembershipPasswordAttribute.MinPasswordLengthError" /> property, the <see cref="P:System.Web.Security.MembershipPasswordAttribute.MinNonAlphaNumericCharactersError" /> property, and the <see cref="P:System.Web.Security.MembershipPasswordAttribute.PasswordStrengthError" /> property.</summary>
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x0009035E File Offset: 0x0008E55E
		// (set) Token: 0x06003712 RID: 14098 RVA: 0x00090366 File Offset: 0x0008E566
		public Type ResourceType { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordAttribute" /> class.</summary>
		// Token: 0x06003713 RID: 14099 RVA: 0x00090370 File Offset: 0x0008E570
		public MembershipPasswordAttribute()
		{
			if (Membership.Provider != null)
			{
				this.MinRequiredNonAlphanumericCharacters = Membership.Provider.MinRequiredNonAlphanumericCharacters;
				this.MinRequiredPasswordLength = Membership.Provider.MinRequiredPasswordLength;
				this.PasswordStrengthRegularExpression = Membership.Provider.PasswordStrengthRegularExpression;
			}
			else
			{
				this.MinRequiredPasswordLength = 7;
				this.MinRequiredNonAlphanumericCharacters = 1;
				this.PasswordStrengthRegularExpression = "(?=.{6,})(?=(.*\\d){1,})(?=(.*\\W){1,})";
			}
			this.MinNonAlphanumericCharactersError = "The '{0}' field is an invalid password. Password must have {1} or more non-alphanumeric characters.";
			this.MinPasswordLengthError = "The '{0}' field is an invalid password. Password must have {1} or more characters.";
			this.PasswordStrengthError = "The '{0}' field is an invalid password. It does not meet the password strength requirements";
			base.ErrorMessage = "The field {0} is invalid.";
		}

		/// <summary>Validates the specified value with respect to the current validation attribute.</summary>
		/// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		// Token: 0x06003714 RID: 14100 RVA: 0x00090404 File Offset: 0x0008E604
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			string text = value as string;
			bool flag = false;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string text2 = string.Empty;
			int num = 0;
			Regex regex = new Regex("\\W|_");
			if (this.MinRequiredPasswordLength > 0 && text.Length < this.MinRequiredPasswordLength)
			{
				text2 = this.MinPasswordLengthError;
				num = this.MinRequiredPasswordLength;
				flag = true;
			}
			if (!flag && this.MinRequiredNonAlphanumericCharacters > 0 && regex.Matches(text).Count < this.MinRequiredNonAlphanumericCharacters)
			{
				text2 = this.MinNonAlphanumericCharactersError;
				num = this.MinRequiredNonAlphanumericCharacters;
				flag = true;
			}
			if (!flag && !string.IsNullOrEmpty(this.PasswordStrengthRegularExpression) && new Regex(this.PasswordStrengthRegularExpression).IsMatch(text))
			{
				text2 = this.PasswordStrengthError;
				flag = true;
			}
			if (!flag)
			{
				return ValidationResult.Success;
			}
			if (validationContext == null)
			{
				return new ValidationResult("error");
			}
			return new ValidationResult(string.Format(text2, validationContext.DisplayName, num), new string[] { validationContext.MemberName });
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000904FC File Offset: 0x0008E6FC
		// (set) Token: 0x06003716 RID: 14102 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int? PasswordStrengthRegexTimeout
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
