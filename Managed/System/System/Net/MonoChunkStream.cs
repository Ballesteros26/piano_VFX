using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x0200053A RID: 1338
	internal class MonoChunkStream
	{
		// Token: 0x06002972 RID: 10610 RVA: 0x000A01A4 File Offset: 0x0009E3A4
		public MonoChunkStream(byte[] buffer, int offset, int size, WebHeaderCollection headers)
			: this(headers)
		{
			this.Write(buffer, offset, size);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000A01B7 File Offset: 0x0009E3B7
		public MonoChunkStream(WebHeaderCollection headers)
		{
			this.headers = headers;
			this.saved = new StringBuilder();
			this.chunks = new ArrayList();
			this.chunkSize = -1;
			this.totalWritten = 0;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000A01EA File Offset: 0x0009E3EA
		public void ResetBuffer()
		{
			this.chunkSize = -1;
			this.chunkRead = 0;
			this.totalWritten = 0;
			this.chunks.Clear();
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000A020C File Offset: 0x0009E40C
		public void WriteAndReadBack(byte[] buffer, int offset, int size, ref int read)
		{
			if (offset + read > 0)
			{
				this.Write(buffer, offset, offset + read);
			}
			read = this.Read(buffer, offset, size);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000A022F File Offset: 0x0009E42F
		public int Read(byte[] buffer, int offset, int size)
		{
			return this.ReadFromChunks(buffer, offset, size);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000A023C File Offset: 0x0009E43C
		private int ReadFromChunks(byte[] buffer, int offset, int size)
		{
			int count = this.chunks.Count;
			int num = 0;
			List<MonoChunkStream.Chunk> list = new List<MonoChunkStream.Chunk>(count);
			for (int i = 0; i < count; i++)
			{
				MonoChunkStream.Chunk chunk = (MonoChunkStream.Chunk)this.chunks[i];
				if (chunk.Offset == chunk.Bytes.Length)
				{
					list.Add(chunk);
				}
				else
				{
					num += chunk.Read(buffer, offset + num, size - num);
					if (num == size)
					{
						break;
					}
				}
			}
			foreach (MonoChunkStream.Chunk chunk2 in list)
			{
				this.chunks.Remove(chunk2);
			}
			return num;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000A02F8 File Offset: 0x0009E4F8
		public void Write(byte[] buffer, int offset, int size)
		{
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000A0308 File Offset: 0x0009E508
		private void InternalWrite(byte[] buffer, ref int offset, int size)
		{
			if (this.state == MonoChunkStream.State.None || this.state == MonoChunkStream.State.PartialSize)
			{
				this.state = this.GetChunkSize(buffer, ref offset, size);
				if (this.state == MonoChunkStream.State.PartialSize)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (this.state == MonoChunkStream.State.Body && offset < size)
			{
				this.state = this.ReadBody(buffer, ref offset, size);
				if (this.state == MonoChunkStream.State.Body)
				{
					return;
				}
			}
			if (this.state == MonoChunkStream.State.BodyFinished && offset < size)
			{
				this.state = this.ReadCRLF(buffer, ref offset, size);
				if (this.state == MonoChunkStream.State.BodyFinished)
				{
					return;
				}
				this.sawCR = false;
			}
			if (this.state == MonoChunkStream.State.Trailer && offset < size)
			{
				this.state = this.ReadTrailer(buffer, ref offset, size);
				if (this.state == MonoChunkStream.State.Trailer)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000A03FD File Offset: 0x0009E5FD
		public bool WantMore
		{
			get
			{
				return this.chunkRead != this.chunkSize || this.chunkSize != 0 || this.state > MonoChunkStream.State.None;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000A0420 File Offset: 0x0009E620
		public bool DataAvailable
		{
			get
			{
				int count = this.chunks.Count;
				for (int i = 0; i < count; i++)
				{
					MonoChunkStream.Chunk chunk = (MonoChunkStream.Chunk)this.chunks[i];
					if (chunk != null && chunk.Bytes != null && chunk.Bytes.Length != 0 && chunk.Offset < chunk.Bytes.Length)
					{
						return this.state != MonoChunkStream.State.Body;
					}
				}
				return false;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000A0489 File Offset: 0x0009E689
		public int TotalDataSize
		{
			get
			{
				return this.totalWritten;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000A0491 File Offset: 0x0009E691
		public int ChunkLeft
		{
			get
			{
				return this.chunkSize - this.chunkRead;
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000A04A0 File Offset: 0x0009E6A0
		private MonoChunkStream.State ReadBody(byte[] buffer, ref int offset, int size)
		{
			if (this.chunkSize == 0)
			{
				return MonoChunkStream.State.BodyFinished;
			}
			int num = size - offset;
			if (num + this.chunkRead > this.chunkSize)
			{
				num = this.chunkSize - this.chunkRead;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			this.chunks.Add(new MonoChunkStream.Chunk(array));
			offset += num;
			this.chunkRead += num;
			this.totalWritten += num;
			if (this.chunkRead != this.chunkSize)
			{
				return MonoChunkStream.State.Body;
			}
			return MonoChunkStream.State.BodyFinished;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000A0534 File Offset: 0x0009E734
		private MonoChunkStream.State GetChunkSize(byte[] buffer, ref int offset, int size)
		{
			this.chunkRead = 0;
			this.chunkSize = 0;
			char c = '\0';
			while (offset < size)
			{
				int num = offset;
				offset = num + 1;
				c = (char)buffer[num];
				if (c == '\r')
				{
					if (this.sawCR)
					{
						MonoChunkStream.ThrowProtocolViolation("2 CR found");
					}
					this.sawCR = true;
				}
				else
				{
					if (this.sawCR && c == '\n')
					{
						break;
					}
					if (c == ' ')
					{
						this.gotit = true;
					}
					if (!this.gotit)
					{
						this.saved.Append(c);
					}
					if (this.saved.Length > 20)
					{
						MonoChunkStream.ThrowProtocolViolation("chunk size too long.");
					}
				}
			}
			if (!this.sawCR || c != '\n')
			{
				if (offset < size)
				{
					MonoChunkStream.ThrowProtocolViolation("Missing \\n");
				}
				try
				{
					if (this.saved.Length > 0)
					{
						this.chunkSize = int.Parse(MonoChunkStream.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
					}
				}
				catch (Exception)
				{
					MonoChunkStream.ThrowProtocolViolation("Cannot parse chunk size.");
				}
				return MonoChunkStream.State.PartialSize;
			}
			this.chunkRead = 0;
			try
			{
				this.chunkSize = int.Parse(MonoChunkStream.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				MonoChunkStream.ThrowProtocolViolation("Cannot parse chunk size.");
			}
			if (this.chunkSize == 0)
			{
				this.trailerState = 2;
				return MonoChunkStream.State.Trailer;
			}
			return MonoChunkStream.State.Body;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000A068C File Offset: 0x0009E88C
		private static string RemoveChunkExtension(string input)
		{
			int num = input.IndexOf(';');
			if (num == -1)
			{
				return input;
			}
			return input.Substring(0, num);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000A06B0 File Offset: 0x0009E8B0
		private MonoChunkStream.State ReadCRLF(byte[] buffer, ref int offset, int size)
		{
			if (!this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 13)
				{
					MonoChunkStream.ThrowProtocolViolation("Expecting \\r");
				}
				this.sawCR = true;
				if (offset == size)
				{
					return MonoChunkStream.State.BodyFinished;
				}
			}
			if (this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 10)
				{
					MonoChunkStream.ThrowProtocolViolation("Expecting \\n");
				}
			}
			return MonoChunkStream.State.None;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000A0710 File Offset: 0x0009E910
		private MonoChunkStream.State ReadTrailer(byte[] buffer, ref int offset, int size)
		{
			if (this.trailerState == 2 && buffer[offset] == 13 && this.saved.Length == 0)
			{
				offset++;
				if (offset < size && buffer[offset] == 10)
				{
					offset++;
					return MonoChunkStream.State.None;
				}
				offset--;
			}
			int num = this.trailerState;
			string text = "\r\n\r";
			while (offset < size && num < 4)
			{
				int num2 = offset;
				offset = num2 + 1;
				char c = (char)buffer[num2];
				if ((num == 0 || num == 2) && c == '\r')
				{
					num++;
				}
				else if ((num == 1 || num == 3) && c == '\n')
				{
					num++;
				}
				else if (num > 0)
				{
					this.saved.Append(text.Substring(0, (this.saved.Length == 0) ? (num - 2) : num));
					num = 0;
					if (this.saved.Length > 4196)
					{
						MonoChunkStream.ThrowProtocolViolation("Error reading trailer (too long).");
					}
				}
			}
			if (num < 4)
			{
				this.trailerState = num;
				if (offset < size)
				{
					MonoChunkStream.ThrowProtocolViolation("Error reading trailer.");
				}
				return MonoChunkStream.State.Trailer;
			}
			StringReader stringReader = new StringReader(this.saved.ToString());
			string text2;
			while ((text2 = stringReader.ReadLine()) != null && text2 != "")
			{
				this.headers.Add(text2);
			}
			return MonoChunkStream.State.None;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000A084B File Offset: 0x0009EA4B
		private static void ThrowProtocolViolation(string message)
		{
			throw new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x04002286 RID: 8838
		internal WebHeaderCollection headers;

		// Token: 0x04002287 RID: 8839
		private int chunkSize;

		// Token: 0x04002288 RID: 8840
		private int chunkRead;

		// Token: 0x04002289 RID: 8841
		private int totalWritten;

		// Token: 0x0400228A RID: 8842
		private MonoChunkStream.State state;

		// Token: 0x0400228B RID: 8843
		private StringBuilder saved;

		// Token: 0x0400228C RID: 8844
		private bool sawCR;

		// Token: 0x0400228D RID: 8845
		private bool gotit;

		// Token: 0x0400228E RID: 8846
		private int trailerState;

		// Token: 0x0400228F RID: 8847
		private ArrayList chunks;

		// Token: 0x0200053B RID: 1339
		private enum State
		{
			// Token: 0x04002291 RID: 8849
			None,
			// Token: 0x04002292 RID: 8850
			PartialSize,
			// Token: 0x04002293 RID: 8851
			Body,
			// Token: 0x04002294 RID: 8852
			BodyFinished,
			// Token: 0x04002295 RID: 8853
			Trailer
		}

		// Token: 0x0200053C RID: 1340
		private class Chunk
		{
			// Token: 0x06002984 RID: 10628 RVA: 0x000A0857 File Offset: 0x0009EA57
			public Chunk(byte[] chunk)
			{
				this.Bytes = chunk;
			}

			// Token: 0x06002985 RID: 10629 RVA: 0x000A0868 File Offset: 0x0009EA68
			public int Read(byte[] buffer, int offset, int size)
			{
				int num = ((size > this.Bytes.Length - this.Offset) ? (this.Bytes.Length - this.Offset) : size);
				Buffer.BlockCopy(this.Bytes, this.Offset, buffer, offset, num);
				this.Offset += num;
				return num;
			}

			// Token: 0x04002296 RID: 8854
			public byte[] Bytes;

			// Token: 0x04002297 RID: 8855
			public int Offset;
		}
	}
}
