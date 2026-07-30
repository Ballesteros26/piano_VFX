using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200008E RID: 142
	internal class CharEntityEncoderFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x0001643A File Offset: 0x0001463A
		internal CharEntityEncoderFallbackBuffer(CharEntityEncoderFallback parent)
		{
			this.parent = parent;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001645C File Offset: 0x0001465C
		public override bool Fallback(char charUnknown, int index)
		{
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[] { (int)charUnknown });
				this.charEntityIndex = 0;
				return true;
			}
			new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			return false;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000164D4 File Offset: 0x000146D4
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsSurrogatePair(charUnknownHigh, charUnknownLow))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(charUnknownHigh, charUnknownLow);
			}
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[] { this.SurrogateCharToUtf32(charUnknownHigh, charUnknownLow) });
				this.charEntityIndex = 0;
				return true;
			}
			new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			return false;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00016564 File Offset: 0x00014764
		public override char GetNextChar()
		{
			if (this.charEntityIndex == this.charEntity.Length)
			{
				this.charEntityIndex = -1;
			}
			if (this.charEntityIndex == -1)
			{
				return '\0';
			}
			string text = this.charEntity;
			int num = this.charEntityIndex;
			this.charEntityIndex = num + 1;
			return text[num];
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000165B2 File Offset: 0x000147B2
		public override bool MovePrevious()
		{
			if (this.charEntityIndex == -1)
			{
				return false;
			}
			if (this.charEntityIndex > 0)
			{
				this.charEntityIndex--;
				return true;
			}
			return false;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000165D9 File Offset: 0x000147D9
		public override int Remaining
		{
			get
			{
				if (this.charEntityIndex == -1)
				{
					return 0;
				}
				return this.charEntity.Length - this.charEntityIndex;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000165F8 File Offset: 0x000147F8
		public override void Reset()
		{
			this.charEntityIndex = -1;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00016601 File Offset: 0x00014801
		private int SurrogateCharToUtf32(char highSurrogate, char lowSurrogate)
		{
			return XmlCharType.CombineSurrogateChar((int)lowSurrogate, (int)highSurrogate);
		}

		// Token: 0x0400030A RID: 778
		private CharEntityEncoderFallback parent;

		// Token: 0x0400030B RID: 779
		private string charEntity = string.Empty;

		// Token: 0x0400030C RID: 780
		private int charEntityIndex = -1;
	}
}
