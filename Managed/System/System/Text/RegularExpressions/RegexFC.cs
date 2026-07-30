using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000147 RID: 327
	internal sealed class RegexFC
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0003143C File Offset: 0x0002F63C
		internal RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00031458 File Offset: 0x0002F658
		internal RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '\uffff')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this._caseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000314C7 File Offset: 0x0002F6C7
		internal RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this._caseInsensitive = caseInsensitive;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000314EC File Offset: 0x0002F6EC
		internal bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this._caseInsensitive |= fc._caseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00031567 File Offset: 0x0002F767
		internal string GetFirstChars(CultureInfo culture)
		{
			if (this._caseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00031588 File Offset: 0x0002F788
		internal bool IsCaseInsensitive()
		{
			return this._caseInsensitive;
		}

		// Token: 0x04000EB1 RID: 3761
		internal RegexCharClass _cc;

		// Token: 0x04000EB2 RID: 3762
		internal bool _nullable;

		// Token: 0x04000EB3 RID: 3763
		internal bool _caseInsensitive;
	}
}
