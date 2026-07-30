using System;
using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that a data field value is a  well-formed phone number using a regular expression for phone numbers.</summary>
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class PhoneAttribute : DataTypeAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.PhoneAttribute" /> class.</summary>
		// Token: 0x060000CD RID: 205 RVA: 0x00003A1C File Offset: 0x00001C1C
		public PhoneAttribute()
			: base(DataType.PhoneNumber)
		{
			base.DefaultErrorMessage = "The {0} field is not a valid phone number.";
		}

		/// <summary>Determines whether the specified phone number is in a valid phone number format. </summary>
		/// <returns>true if the phone number is valid; otherwise, false.</returns>
		/// <param name="value">The value to validate.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x00003A30 File Offset: 0x00001C30
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			if (PhoneAttribute._regex != null)
			{
				return text != null && PhoneAttribute._regex.Match(text).Length > 0;
			}
			if (text == null)
			{
				return false;
			}
			text = text.Replace("+", string.Empty).TrimEnd(Array.Empty<char>());
			text = PhoneAttribute.RemoveExtension(text);
			bool flag = false;
			string text2 = text;
			for (int i = 0; i < text2.Length; i++)
			{
				if (char.IsDigit(text2[i]))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			foreach (char c in text)
			{
				if (!char.IsDigit(c) && !char.IsWhiteSpace(c) && "-.()".IndexOf(c) == -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003AFC File Offset: 0x00001CFC
		private static Regex CreateRegEx()
		{
			if (AppSettings.DisableRegEx)
			{
				return null;
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds(2.0);
			try
			{
				if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
				{
					return new Regex("^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, timeSpan);
				}
			}
			catch
			{
			}
			return new Regex("^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003B68 File Offset: 0x00001D68
		private static string RemoveExtension(string potentialPhoneNumber)
		{
			int num = potentialPhoneNumber.LastIndexOf("ext.", StringComparison.InvariantCultureIgnoreCase);
			if (num >= 0 && PhoneAttribute.MatchesExtension(potentialPhoneNumber.Substring(num + 4)))
			{
				return potentialPhoneNumber.Substring(0, num);
			}
			num = potentialPhoneNumber.LastIndexOf("ext", StringComparison.InvariantCultureIgnoreCase);
			if (num >= 0 && PhoneAttribute.MatchesExtension(potentialPhoneNumber.Substring(num + 3)))
			{
				return potentialPhoneNumber.Substring(0, num);
			}
			num = potentialPhoneNumber.LastIndexOf("x", StringComparison.InvariantCultureIgnoreCase);
			if (num >= 0 && PhoneAttribute.MatchesExtension(potentialPhoneNumber.Substring(num + 1)))
			{
				return potentialPhoneNumber.Substring(0, num);
			}
			return potentialPhoneNumber;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003BF4 File Offset: 0x00001DF4
		private static bool MatchesExtension(string potentialExtension)
		{
			potentialExtension = potentialExtension.TrimStart(Array.Empty<char>());
			if (potentialExtension.Length == 0)
			{
				return false;
			}
			string text = potentialExtension;
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsDigit(text[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000086 RID: 134
		private static Regex _regex = PhoneAttribute.CreateRegEx();

		// Token: 0x04000087 RID: 135
		private const string _additionalPhoneNumberCharacters = "-.()";
	}
}
