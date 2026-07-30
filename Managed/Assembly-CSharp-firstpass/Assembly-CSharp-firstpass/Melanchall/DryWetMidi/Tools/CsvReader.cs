using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000015 RID: 21
	internal sealed class CsvReader : IDisposable
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000479F File Offset: 0x0000299F
		public CsvReader(Stream stream, CsvSettings settings)
		{
			this._streamReader = new StreamReader(stream, Encoding.UTF8, true, settings.IoBufferSize, true);
			this._buffer = new char[settings.IoBufferSize];
			this._delimiter = settings.CsvDelimiter;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000047E0 File Offset: 0x000029E0
		public CsvRecord ReadRecord()
		{
			int currentLineNumber = this._currentLineNumber;
			string text = this.GetFirstLine();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string[] array;
			for (;;)
			{
				array = CsvReader.SplitValues(text, this._delimiter).ToArray<string>();
				if (array.All(new Func<string, bool>(CsvReader.IsValueClosed)))
				{
					break;
				}
				string nextLine = this.GetNextLine();
				if (nextLine == null)
				{
					break;
				}
				text += nextLine;
			}
			return new CsvRecord(currentLineNumber, this._currentLineNumber - currentLineNumber, array);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004850 File Offset: 0x00002A50
		private string GetFirstLine()
		{
			string nextLine;
			do
			{
				nextLine = this.GetNextLine();
			}
			while (((nextLine != null) ? nextLine.Trim() : null) == string.Empty);
			return nextLine;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004880 File Offset: 0x00002A80
		private string GetNextLine()
		{
			this._currentLineNumber++;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			do
			{
				if (this._indexInBuffer < this._bufferLength)
				{
					char c = this._buffer[this._indexInBuffer];
					if (c == '\r' || c == '\n')
					{
						flag = true;
					}
					else if (flag)
					{
						goto IL_005B;
					}
					stringBuilder.Append(c);
					this._indexInBuffer++;
					continue;
				}
				IL_005B:
				if (this._indexInBuffer < this._bufferLength)
				{
					break;
				}
				this.FillBuffer();
			}
			while (this._bufferLength != 0);
			if (stringBuilder.Length <= 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004918 File Offset: 0x00002B18
		private void FillBuffer()
		{
			int num = 0;
			int i = this._buffer.Length;
			while (i > 0)
			{
				int num2 = this._streamReader.ReadBlock(this._buffer, num, i);
				if (num2 == 0)
				{
					break;
				}
				i -= num2;
				num += num2;
			}
			this._bufferLength = this._buffer.Length - i;
			this._indexInBuffer = 0;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000496C File Offset: 0x00002B6C
		private static IEnumerable<string> SplitValues(string input, char delimiter)
		{
			StringBuilder valueBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in input)
			{
				if (c == delimiter && (!flag || flag2))
				{
					yield return valueBuilder.ToString().Trim();
					valueBuilder.Clear();
					flag2 = false;
					flag = false;
				}
				else
				{
					if (c == '"')
					{
						if (!flag)
						{
							flag = true;
						}
						else
						{
							flag2 = !flag2;
						}
					}
					valueBuilder.Append(c);
				}
			}
			string text = null;
			yield return valueBuilder.ToString().Trim();
			yield break;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004984 File Offset: 0x00002B84
		private static bool IsValueClosed(string value)
		{
			if (string.IsNullOrEmpty(value) || value[0] != '"')
			{
				return true;
			}
			if (value.Length == 1)
			{
				return false;
			}
			return value.Skip(1).Reverse<char>().TakeWhile((char c) => c == '"')
				.Count<char>() % 2 == 1;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000049EB File Offset: 0x00002BEB
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000049F4 File Offset: 0x00002BF4
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._streamReader.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x04000078 RID: 120
		private const char Quote = '"';

		// Token: 0x04000079 RID: 121
		private readonly StreamReader _streamReader;

		// Token: 0x0400007A RID: 122
		private readonly char _delimiter;

		// Token: 0x0400007B RID: 123
		private readonly char[] _buffer;

		// Token: 0x0400007C RID: 124
		private int _bufferLength;

		// Token: 0x0400007D RID: 125
		private int _indexInBuffer;

		// Token: 0x0400007E RID: 126
		private bool _disposed;

		// Token: 0x0400007F RID: 127
		private int _currentLineNumber;
	}
}
