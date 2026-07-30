using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000018 RID: 24
	internal sealed class CsvWriter : IDisposable
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00004A96 File Offset: 0x00002C96
		public CsvWriter(Stream stream, CsvSettings settings)
		{
			this._streamWriter = new StreamWriter(stream, new UTF8Encoding(false, true), 1024, true);
			this._delimiter = settings.CsvDelimiter;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public void WriteRecord(IEnumerable<object> values)
		{
			this._streamWriter.WriteLine(string.Join<object>(this._delimiter.ToString(), values));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004AF0 File Offset: 0x00002CF0
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._streamWriter.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004B10 File Offset: 0x00002D10
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000085 RID: 133
		private readonly StreamWriter _streamWriter;

		// Token: 0x04000086 RID: 134
		private readonly char _delimiter;

		// Token: 0x04000087 RID: 135
		private bool _disposed;
	}
}
