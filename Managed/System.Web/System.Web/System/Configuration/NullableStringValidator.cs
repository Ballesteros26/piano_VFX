using System;

namespace System.Configuration
{
	// Token: 0x02000017 RID: 23
	internal class NullableStringValidator : ConfigurationValidatorBase
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002F7B File Offset: 0x0000117B
		public NullableStringValidator(int minLength)
		{
			this.minLength = minLength;
			this.maxLength = int.MaxValue;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F95 File Offset: 0x00001195
		public NullableStringValidator(int minLength, int maxLength)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002FAB File Offset: 0x000011AB
		public NullableStringValidator(int minLength, int maxLength, string invalidCharacters)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
			if (invalidCharacters != null)
			{
				this.invalidCharacters = invalidCharacters.ToCharArray();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FD0 File Offset: 0x000011D0
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002FE4 File Offset: 0x000011E4
		public override void Validate(object value)
		{
			if (value == null)
			{
				return;
			}
			string text = (string)value;
			if (text == null || text.Length < this.minLength)
			{
				throw new ArgumentException("The string must be at least " + this.minLength + " characters long.");
			}
			if (text.Length > this.maxLength)
			{
				throw new ArgumentException("The string must be no more than " + this.maxLength + " characters long.");
			}
			if (this.invalidCharacters != null && text.IndexOfAny(this.invalidCharacters) != -1)
			{
				throw new ArgumentException(string.Format("The string cannot contain any of the following characters: '{0}'.", this.invalidCharacters));
			}
		}

		// Token: 0x04000D4A RID: 3402
		private char[] invalidCharacters;

		// Token: 0x04000D4B RID: 3403
		private int maxLength;

		// Token: 0x04000D4C RID: 3404
		private int minLength;
	}
}
