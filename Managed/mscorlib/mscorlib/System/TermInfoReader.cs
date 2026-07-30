using System;
using System.IO;
using System.Text;

namespace System
{
	// Token: 0x02000238 RID: 568
	internal class TermInfoReader
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x000667D0 File Offset: 0x000649D0
		public TermInfoReader(string term, string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				long length = fileStream.Length;
				if (length > 4096L)
				{
					throw new Exception("File must be smaller than 4K");
				}
				this.buffer = new byte[(int)length];
				if (fileStream.Read(this.buffer, 0, this.buffer.Length) != this.buffer.Length)
				{
					throw new Exception("Short read");
				}
				this.ReadHeader(this.buffer, ref this.booleansOffset);
				this.ReadNames(this.buffer, ref this.booleansOffset);
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0006687C File Offset: 0x00064A7C
		public TermInfoReader(string term, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = buffer;
			this.ReadHeader(buffer, ref this.booleansOffset);
			this.ReadNames(buffer, ref this.booleansOffset);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000668B4 File Offset: 0x00064AB4
		private void ReadHeader(byte[] buffer, ref int position)
		{
			short @int = this.GetInt16(buffer, position);
			position += 2;
			if (@int != 282)
			{
				throw new Exception(string.Format("Magic number is wrong: {0}", @int));
			}
			this.GetInt16(buffer, position);
			position += 2;
			this.boolSize = this.GetInt16(buffer, position);
			position += 2;
			this.numSize = this.GetInt16(buffer, position);
			position += 2;
			this.strOffsets = this.GetInt16(buffer, position);
			position += 2;
			this.GetInt16(buffer, position);
			position += 2;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00066950 File Offset: 0x00064B50
		private void ReadNames(byte[] buffer, ref int position)
		{
			string @string = this.GetString(buffer, position);
			position += @string.Length + 1;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00066974 File Offset: 0x00064B74
		public bool Get(TermInfoBooleans boolean)
		{
			if (boolean < TermInfoBooleans.AutoLeftMargin || boolean >= TermInfoBooleans.Last || boolean >= (TermInfoBooleans)this.boolSize)
			{
				return false;
			}
			int num = this.booleansOffset;
			num = (int)(num + boolean);
			return this.buffer[num] > 0;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000669B0 File Offset: 0x00064BB0
		public int Get(TermInfoNumbers number)
		{
			if (number < TermInfoNumbers.Columns || number >= TermInfoNumbers.Last || number > (TermInfoNumbers)this.numSize)
			{
				return -1;
			}
			int num = this.booleansOffset + (int)this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num = (int)(num + number * TermInfoNumbers.Lines);
			return (int)this.GetInt16(this.buffer, num);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00066A00 File Offset: 0x00064C00
		public string Get(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + (int)this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += (int)(this.numSize * 2);
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetString(this.buffer, num + (int)(this.strOffsets * 2) + @int);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00066A7C File Offset: 0x00064C7C
		public byte[] GetStringBytes(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + (int)this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += (int)(this.numSize * 2);
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetStringBytes(this.buffer, num + (int)(this.strOffsets * 2) + @int);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00066AF8 File Offset: 0x00064CF8
		private short GetInt16(byte[] buffer, int offset)
		{
			int num = (int)buffer[offset];
			int num2 = (int)buffer[offset + 1];
			if (num == 255 && num2 == 255)
			{
				return -1;
			}
			return (short)(num + num2 * 256);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00066B2C File Offset: 0x00064D2C
		private string GetString(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			return Encoding.ASCII.GetString(buffer, offset, num);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00066B5C File Offset: 0x00064D5C
		private byte[] GetStringBytes(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Buffer.InternalBlockCopy(buffer, offset, array, 0, num);
			return array;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00066B90 File Offset: 0x00064D90
		internal static string Escape(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (char.IsControl(c))
				{
					stringBuilder.AppendFormat("\\x{0:X2}", (int)c);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000DA7 RID: 3495
		private short boolSize;

		// Token: 0x04000DA8 RID: 3496
		private short numSize;

		// Token: 0x04000DA9 RID: 3497
		private short strOffsets;

		// Token: 0x04000DAA RID: 3498
		private byte[] buffer;

		// Token: 0x04000DAB RID: 3499
		private int booleansOffset;
	}
}
