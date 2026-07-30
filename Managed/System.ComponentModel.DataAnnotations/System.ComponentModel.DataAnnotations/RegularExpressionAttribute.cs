using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that a data field value in ASP.NET Dynamic Data must match the specified regular expression.</summary>
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class RegularExpressionAttribute : ValidationAttribute
	{
		/// <summary>Gets the regular expression pattern.</summary>
		/// <returns>The pattern to match.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004037 File Offset: 0x00002237
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000403F File Offset: 0x0000223F
		public string Pattern { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004048 File Offset: 0x00002248
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004050 File Offset: 0x00002250
		public int MatchTimeoutInMilliseconds
		{
			get
			{
				return this._matchTimeoutInMilliseconds;
			}
			set
			{
				this._matchTimeoutInMilliseconds = value;
				this._matchTimeoutSet = true;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004060 File Offset: 0x00002260
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004068 File Offset: 0x00002268
		private Regex Regex { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.RegularExpressionAttribute" /> class.</summary>
		/// <param name="pattern">The regular expression that is used to validate the data field value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		// Token: 0x060000F0 RID: 240 RVA: 0x00004071 File Offset: 0x00002271
		public RegularExpressionAttribute(string pattern)
			: base(() => "The field {0} must match the regular expression '{1}'.")
		{
			this.Pattern = pattern;
		}

		/// <summary>Checks whether the value entered by the user matches the regular expression pattern. </summary>
		/// <returns>true if validation is successful; otherwise, false.</returns>
		/// <param name="value">The data field value to validate.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The data field value did not match the regular expression pattern.</exception>
		// Token: 0x060000F1 RID: 241 RVA: 0x000040A0 File Offset: 0x000022A0
		public override bool IsValid(object value)
		{
			this.SetupRegex();
			string text = Convert.ToString(value, CultureInfo.CurrentCulture);
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			Match match = this.Regex.Match(text);
			return match.Success && match.Index == 0 && match.Length == text.Length;
		}

		/// <summary>Formats the error message to display if the regular expression validation fails.</summary>
		/// <returns>The formatted error message.</returns>
		/// <param name="name">The name of the field that caused the validation failure.</param>
		// Token: 0x060000F2 RID: 242 RVA: 0x000040F6 File Offset: 0x000022F6
		public override string FormatErrorMessage(string name)
		{
			this.SetupRegex();
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.Pattern);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004118 File Offset: 0x00002318
		private void SetupRegex()
		{
			if (this.Regex == null)
			{
				if (string.IsNullOrEmpty(this.Pattern))
				{
					throw new InvalidOperationException("The pattern must be set to a valid regular expression.");
				}
				if (!this._matchTimeoutSet)
				{
					this.MatchTimeoutInMilliseconds = RegularExpressionAttribute.GetDefaultTimeout();
				}
				this.Regex = ((this.MatchTimeoutInMilliseconds == -1) ? new Regex(this.Pattern) : (this.Regex = new Regex(this.Pattern, RegexOptions.None, TimeSpan.FromMilliseconds((double)this.MatchTimeoutInMilliseconds))));
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004195 File Offset: 0x00002395
		private static int GetDefaultTimeout()
		{
			return 2000;
		}

		// Token: 0x04000093 RID: 147
		private int _matchTimeoutInMilliseconds;

		// Token: 0x04000094 RID: 148
		private bool _matchTimeoutSet;
	}
}
