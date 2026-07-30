using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextReader" /> that reads characters from a byte stream in a particular encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003AD RID: 941
	[ComVisible(true)]
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0009BBBE File Offset: 0x00099DBE
		internal static int DefaultBufferSize
		{
			get
			{
				return 1024;
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x0009BBC8 File Offset: 0x00099DC8
		private void CheckAsyncTaskInProgress()
		{
			Task asyncReadTask = this._asyncReadTask;
			if (asyncReadTask != null && !asyncReadTask.IsCompleted)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The stream is currently in use by a previous operation on the stream."));
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0009BBF9 File Offset: 0x00099DF9
		internal StreamReader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06002BCB RID: 11211 RVA: 0x0009BC01 File Offset: 0x00099E01
		public StreamReader(Stream stream)
			: this(stream, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06002BCC RID: 11212 RVA: 0x0009BC0B File Offset: 0x00099E0B
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
			: this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06002BCD RID: 11213 RVA: 0x0009BC20 File Offset: 0x00099E20
		public StreamReader(Stream stream, Encoding encoding)
			: this(stream, encoding, true, StreamReader.DefaultBufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06002BCE RID: 11214 RVA: 0x0009BC31 File Offset: 0x00099E31
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <param name="bufferSize">The minimum buffer size. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		// Token: 0x06002BCF RID: 11215 RVA: 0x0009BC42 File Offset: 0x00099E42
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream based on the specified character encoding, byte order mark detection option, and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">true to look for byte order marks at the beginning of the file; otherwise, false.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.StreamReader" /> object is disposed; otherwise, false.</param>
		// Token: 0x06002BD0 RID: 11216 RVA: 0x0009BC50 File Offset: 0x00099E50
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not readable."));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06002BD1 RID: 11217 RVA: 0x0009BCBD File Offset: 0x00099EBD
		public StreamReader(string path)
			: this(path, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06002BD2 RID: 11218 RVA: 0x0009BCC7 File Offset: 0x00099EC7
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
			: this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06002BD3 RID: 11219 RVA: 0x0009BCDB File Offset: 0x00099EDB
		public StreamReader(string path, Encoding encoding)
			: this(path, encoding, true, StreamReader.DefaultBufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06002BD4 RID: 11220 RVA: 0x0009BCEB File Offset: 0x00099EEB
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(path, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is less than or equal to zero. </exception>
		// Token: 0x06002BD5 RID: 11221 RVA: 0x0009BCFB File Offset: 0x00099EFB
		[SecuritySafeCritical]
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(path, encoding, detectEncodingFromByteOrderMarks, bufferSize, true)
		{
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x0009BD0C File Offset: 0x00099F0C
		[SecurityCritical]
		internal StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool checkHost)
		{
			if (path == null || encoding == null)
			{
				throw new ArgumentNullException((path == null) ? "path" : "encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty path name is not legal."));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x0009BD98 File Offset: 0x00099F98
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			this.stream = stream;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.byteLen = 0;
			this.bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._preamble = encoding.GetPreamble();
			this._checkPreamble = this._preamble.Length != 0;
			this._isBlocked = false;
			this._closable = !leaveOpen;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x0009BE3E File Offset: 0x0009A03E
		internal void Init(Stream stream)
		{
			this.stream = stream;
			this._closable = true;
		}

		/// <summary>Closes the <see cref="T:System.IO.StreamReader" /> object and the underlying stream, and releases any system resources associated with the reader.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x0009BE4E File Offset: 0x0009A04E
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Closes the underlying stream, releases the unmanaged resources used by the <see cref="T:System.IO.StreamReader" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002BDA RID: 11226 RVA: 0x0009BE58 File Offset: 0x0009A058
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.LeaveOpen && disposing && this.stream != null)
				{
					this.stream.Close();
				}
			}
			finally
			{
				if (!this.LeaveOpen && this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
					this.charPos = 0;
					this.charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Gets the current character encoding that the current <see cref="T:System.IO.StreamReader" /> object is using.</summary>
		/// <returns>The current character encoding used by the current reader. The value can be different after the first call to any <see cref="Overload:System.IO.StreamReader.Read" /> method of <see cref="T:System.IO.StreamReader" />, since encoding autodetection is not done until the first call to a <see cref="Overload:System.IO.StreamReader.Read" /> method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x0009BEE0 File Offset: 0x0009A0E0
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		/// <summary>Returns the underlying stream.</summary>
		/// <returns>The underlying stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x0009BEE8 File Offset: 0x0009A0E8
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x0009BEF0 File Offset: 0x0009A0F0
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		/// <summary>Clears the internal buffer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002BDE RID: 11230 RVA: 0x0009BEFB File Offset: 0x0009A0FB
		public void DiscardBufferedData()
		{
			this.CheckAsyncTaskInProgress();
			this.byteLen = 0;
			this.charLen = 0;
			this.charPos = 0;
			if (this.encoding != null)
			{
				this.decoder = this.encoding.GetDecoder();
			}
			this._isBlocked = false;
		}

		/// <summary>Gets a value that indicates whether the current stream position is at the end of the stream.</summary>
		/// <returns>true if the current stream position is at the end of the stream; otherwise false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream has been disposed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x0009BF38 File Offset: 0x0009A138
		public bool EndOfStream
		{
			get
			{
				if (this.stream == null)
				{
					__Error.ReaderClosed();
				}
				this.CheckAsyncTaskInProgress();
				return this.charPos >= this.charLen && this.ReadBuffer() == 0;
			}
		}

		/// <summary>Returns the next available character but does not consume it.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if there are no characters to be read or if the stream does not support seeking.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002BE0 RID: 11232 RVA: 0x0009BF68 File Offset: 0x0009A168
		public override int Peek()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this.charBuffer[this.charPos];
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0009BFB5 File Offset: 0x0009A1B5
		internal bool DataAvailable()
		{
			return this.charPos < this.charLen;
		}

		/// <summary>Reads the next character from the input stream and advances the character position by one character.</summary>
		/// <returns>The next character from the input stream represented as an <see cref="T:System.Int32" /> object, or -1 if no more characters are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002BE2 RID: 11234 RVA: 0x0009BFC8 File Offset: 0x0009A1C8
		public override int Read()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int num = (int)this.charBuffer[this.charPos];
			this.charPos++;
			return num;
		}

		/// <summary>Reads a specified maximum of characters from the current stream into a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read, or 0 if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="count" /> parameter, depending on whether the data is available within the stream.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index + count - 1" />) replaced by the characters read from the current source. </param>
		/// <param name="index">The index of <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The maximum number of characters to read. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002BE3 RID: 11235 RVA: 0x0009C01C File Offset: 0x0009A21C
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			while (count > 0)
			{
				int num2 = this.charLen - this.charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer, index + num, count, out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > count)
				{
					num2 = count;
				}
				if (!flag)
				{
					Buffer.InternalBlockCopy(this.charBuffer, this.charPos * 2, buffer, (index + num) * 2, num2 * 2);
					this.charPos += num2;
				}
				num += num2;
				count -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		/// <summary>Reads all characters from the current position to the end of the stream.</summary>
		/// <returns>The rest of the stream as a string, from the current position to the end. If the current position is at the end of the stream, returns an empty string ("").</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002BE4 RID: 11236 RVA: 0x0009C108 File Offset: 0x0009A308
		public override string ReadToEnd()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this.charLen - this.charPos);
			do
			{
				stringBuilder.Append(this.charBuffer, this.charPos, this.charLen - this.charPos);
				this.charPos = this.charLen;
				this.ReadBuffer();
			}
			while (this.charLen > 0);
			return stringBuilder.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current stream and writes the data to a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether all input characters have been read.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index + count - 1" />) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.StreamReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		// Token: 0x06002BE5 RID: 11237 RVA: 0x0009C180 File Offset: 0x0009A380
		public override int ReadBlock([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			return base.ReadBlock(buffer, index, count);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0009C201 File Offset: 0x0009A401
		private void CompressBuffer(int n)
		{
			Buffer.InternalBlockCopy(this.byteBuffer, n, this.byteBuffer, 0, this.byteLen - n);
			this.byteLen -= n;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0009C230 File Offset: 0x0009A430
		private void DetectEncoding()
		{
			if (this.byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this.byteBuffer[0] == 254 && this.byteBuffer[1] == 255)
			{
				this.encoding = new UnicodeEncoding(true, true);
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this.byteBuffer[0] == 255 && this.byteBuffer[1] == 254)
			{
				if (this.byteLen < 4 || this.byteBuffer[2] != 0 || this.byteBuffer[3] != 0)
				{
					this.encoding = new UnicodeEncoding(false, true);
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this.encoding = new UTF32Encoding(false, true);
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this.byteLen >= 3 && this.byteBuffer[0] == 239 && this.byteBuffer[1] == 187 && this.byteBuffer[2] == 191)
			{
				this.encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this.byteLen >= 4 && this.byteBuffer[0] == 0 && this.byteBuffer[1] == 0 && this.byteBuffer[2] == 254 && this.byteBuffer[3] == 255)
			{
				this.encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this.byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this.decoder = this.encoding.GetDecoder();
				this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
				this.charBuffer = new char[this._maxCharsPerBuffer];
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0009C3E8 File Offset: 0x0009A5E8
		private bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			int num = ((this.byteLen >= this._preamble.Length) ? (this._preamble.Length - this.bytePos) : (this.byteLen - this.bytePos));
			int i = 0;
			while (i < num)
			{
				if (this.byteBuffer[this.bytePos] != this._preamble[this.bytePos])
				{
					this.bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this.bytePos++;
			}
			if (this._checkPreamble && this.bytePos == this._preamble.Length)
			{
				this.CompressBuffer(this._preamble.Length);
				this.bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0009C4BC File Offset: 0x0009A6BC
		internal virtual int ReadBuffer()
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num == 0)
					{
						break;
					}
					this.byteLen += num;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this.byteLen < this.byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				}
				if (this.charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this.byteLen > 0)
			{
				this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				this.bytePos = (this.byteLen = 0);
			}
			return this.charLen;
			Block_5:
			return this.charLen;
			Block_9:
			return this.charLen;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0009C624 File Offset: 0x0009A824
		private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num2 == 0)
					{
						break;
					}
					this.byteLen += num2;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto IL_01B1;
					}
				}
				this._isBlocked = this.byteLen < this.byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
					}
					this.charPos = 0;
					if (readToUserBuffer)
					{
						num += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
						this.charLen = 0;
					}
					else
					{
						num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
						this.charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_01B1;
				}
			}
			if (this.byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
					this.charLen = 0;
				}
				else
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
					this.charLen += num;
				}
			}
			return num;
			IL_01B1:
			this._isBlocked &= num < desiredChars;
			return num;
		}

		/// <summary>Reads a line of characters from the current stream and returns the data as a string.</summary>
		/// <returns>The next line from the input stream, or null if the end of the input stream is reached.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002BEB RID: 11243 RVA: 0x0009C7F4 File Offset: 0x0009A9F4
		public override string ReadLine()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this.charPos;
				do
				{
					c = this.charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_004A;
					}
					num++;
				}
				while (num < this.charLen);
				num = this.charLen - this.charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this.charBuffer, this.charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_004A:
			string text;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this.charBuffer, this.charPos, num - this.charPos);
				text = stringBuilder.ToString();
			}
			else
			{
				text = new string(this.charBuffer, this.charPos, num - this.charPos);
			}
			this.charPos = num + 1;
			if (c == '\r' && (this.charPos < this.charLen || this.ReadBuffer() > 0) && this.charBuffer[this.charPos] == '\n')
			{
				this.charPos++;
			}
			return text;
			Block_11:
			return stringBuilder.ToString();
		}

		/// <summary>Reads a line of characters asynchronously from the current stream and returns the data as a string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the next line from the stream, or is null if all the characters have been read.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002BEC RID: 11244 RVA: 0x0009C924 File Offset: 0x0009AB24
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<string> ReadLineAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadLineAsync();
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadLineAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x0009C974 File Offset: 0x0009AB74
		private async Task<string> ReadLineAsyncInternal()
		{
			bool flag = this.CharPos_Prop == this.CharLen_Prop;
			ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (flag)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() == 0;
			}
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				StringBuilder sb = null;
				char[] tmpCharBuffer;
				int tmpCharLen;
				int tmpCharPos;
				int i;
				char c;
				for (;;)
				{
					tmpCharBuffer = this.CharBuffer_Prop;
					tmpCharLen = this.CharLen_Prop;
					tmpCharPos = this.CharPos_Prop;
					i = tmpCharPos;
					do
					{
						c = tmpCharBuffer[i];
						if (c == '\r' || c == '\n')
						{
							goto IL_00FD;
						}
						i++;
					}
					while (i < tmpCharLen);
					i = tmpCharLen - tmpCharPos;
					if (sb == null)
					{
						sb = new StringBuilder(i + 80);
					}
					sb.Append(tmpCharBuffer, tmpCharPos, i);
					tmpCharBuffer = null;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() <= 0)
					{
						goto Block_14;
					}
				}
				IL_00FD:
				string s;
				if (sb != null)
				{
					sb.Append(tmpCharBuffer, tmpCharPos, i - tmpCharPos);
					s = sb.ToString();
				}
				else
				{
					s = new string(tmpCharBuffer, tmpCharPos, i - tmpCharPos);
				}
				tmpCharPos = (this.CharPos_Prop = i + 1);
				flag = c == '\r';
				if (flag)
				{
					bool flag2 = tmpCharPos < tmpCharLen;
					if (!flag2)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						flag2 = configuredTaskAwaiter.GetResult() > 0;
					}
					flag = flag2;
				}
				if (flag)
				{
					tmpCharPos = this.CharPos_Prop;
					if (this.CharBuffer_Prop[tmpCharPos] == '\n')
					{
						tmpCharPos = (this.CharPos_Prop = tmpCharPos + 1);
					}
				}
				return s;
				Block_14:
				text = sb.ToString();
			}
			return text;
		}

		/// <summary>Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains a string with the characters from the current position to the end of the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002BEE RID: 11246 RVA: 0x0009C9BC File Offset: 0x0009ABBC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<string> ReadToEndAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadToEndAsync();
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadToEndAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0009CA0C File Offset: 0x0009AC0C
		private async Task<string> ReadToEndAsyncInternal()
		{
			StringBuilder sb = new StringBuilder(this.CharLen_Prop - this.CharPos_Prop);
			do
			{
				int charPos_Prop = this.CharPos_Prop;
				sb.Append(this.CharBuffer_Prop, charPos_Prop, this.CharLen_Prop - charPos_Prop);
				this.CharPos_Prop = this.CharLen_Prop;
				await this.ReadBufferAsync().ConfigureAwait(false);
			}
			while (this.CharLen_Prop > 0);
			return sb.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index. </summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the current method returns.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002BF0 RID: 11248 RVA: 0x0009CA54 File Offset: 0x0009AC54
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0009CB04 File Offset: 0x0009AD04
		internal override async Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
		{
			bool flag = this.CharPos_Prop == this.CharLen_Prop;
			if (flag)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() == 0;
			}
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int charsRead = 0;
				bool readToUserBuffer = false;
				byte[] tmpByteBuffer = this.ByteBuffer_Prop;
				Stream tmpStream = this.Stream_Prop;
				while (count > 0)
				{
					int i = this.CharLen_Prop - this.CharPos_Prop;
					if (i == 0)
					{
						this.CharLen_Prop = 0;
						this.CharPos_Prop = 0;
						if (!this.CheckPreamble_Prop)
						{
							this.ByteLen_Prop = 0;
						}
						readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
						do
						{
							if (this.CheckPreamble_Prop)
							{
								int bytePos_Prop = this.BytePos_Prop;
								int num2 = await tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false);
								if (num2 == 0)
								{
									goto Block_7;
								}
								this.ByteLen_Prop += num2;
							}
							else
							{
								this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
								if (this.ByteLen_Prop == 0)
								{
									goto Block_10;
								}
							}
							this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
							if (!this.IsPreamble())
							{
								if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
								{
									this.DetectEncoding();
									readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
								}
								this.CharPos_Prop = 0;
								if (readToUserBuffer)
								{
									i += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
									this.CharLen_Prop = 0;
								}
								else
								{
									i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
									this.CharLen_Prop += i;
								}
							}
						}
						while (i == 0);
						IL_03E0:
						if (i != 0)
						{
							goto IL_03EB;
						}
						break;
						Block_10:
						this.IsBlocked_Prop = true;
						goto IL_03E0;
						Block_7:
						if (this.ByteLen_Prop > 0)
						{
							if (readToUserBuffer)
							{
								i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
								this.CharLen_Prop = 0;
							}
							else
							{
								i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
								this.CharLen_Prop += i;
							}
						}
						this.IsBlocked_Prop = true;
						goto IL_03E0;
					}
					IL_03EB:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						Buffer.InternalBlockCopy(this.CharBuffer_Prop, this.CharPos_Prop * 2, buffer, (index + charsRead) * 2, i * 2);
						this.CharPos_Prop += i;
					}
					charsRead += i;
					count -= i;
					if (this.IsBlocked_Prop)
					{
						break;
					}
				}
				num = charsRead;
			}
			return num;
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the method returns.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002BF2 RID: 11250 RVA: 0x0009CB64 File Offset: 0x0009AD64
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = base.ReadBlockAsync(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x0009CC11 File Offset: 0x0009AE11
		// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x0009CC19 File Offset: 0x0009AE19
		private int CharLen_Prop
		{
			get
			{
				return this.charLen;
			}
			set
			{
				this.charLen = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x0009CC22 File Offset: 0x0009AE22
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x0009CC2A File Offset: 0x0009AE2A
		private int CharPos_Prop
		{
			get
			{
				return this.charPos;
			}
			set
			{
				this.charPos = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x0009CC33 File Offset: 0x0009AE33
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x0009CC3B File Offset: 0x0009AE3B
		private int ByteLen_Prop
		{
			get
			{
				return this.byteLen;
			}
			set
			{
				this.byteLen = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x0009CC44 File Offset: 0x0009AE44
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x0009CC4C File Offset: 0x0009AE4C
		private int BytePos_Prop
		{
			get
			{
				return this.bytePos;
			}
			set
			{
				this.bytePos = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x0009CC55 File Offset: 0x0009AE55
		private byte[] Preamble_Prop
		{
			get
			{
				return this._preamble;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x0009CC5D File Offset: 0x0009AE5D
		private bool CheckPreamble_Prop
		{
			get
			{
				return this._checkPreamble;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x0009CC65 File Offset: 0x0009AE65
		private Decoder Decoder_Prop
		{
			get
			{
				return this.decoder;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		private bool DetectEncoding_Prop
		{
			get
			{
				return this._detectEncoding;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x0009CC75 File Offset: 0x0009AE75
		private char[] CharBuffer_Prop
		{
			get
			{
				return this.charBuffer;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x0009CC7D File Offset: 0x0009AE7D
		private byte[] ByteBuffer_Prop
		{
			get
			{
				return this.byteBuffer;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x0009CC85 File Offset: 0x0009AE85
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x0009CC8D File Offset: 0x0009AE8D
		private bool IsBlocked_Prop
		{
			get
			{
				return this._isBlocked;
			}
			set
			{
				this._isBlocked = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x0009BEE8 File Offset: 0x0009A0E8
		private Stream Stream_Prop
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x0009CC96 File Offset: 0x0009AE96
		private int MaxCharsPerBuffer_Prop
		{
			get
			{
				return this._maxCharsPerBuffer;
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0009CCA0 File Offset: 0x0009AEA0
		private async Task<int> ReadBufferAsync()
		{
			this.CharLen_Prop = 0;
			this.CharPos_Prop = 0;
			byte[] tmpByteBuffer = this.ByteBuffer_Prop;
			Stream tmpStream = this.Stream_Prop;
			if (!this.CheckPreamble_Prop)
			{
				this.ByteLen_Prop = 0;
			}
			for (;;)
			{
				if (this.CheckPreamble_Prop)
				{
					int bytePos_Prop = this.BytePos_Prop;
					int num = await tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					this.ByteLen_Prop += num;
				}
				else
				{
					this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
					if (this.ByteLen_Prop == 0)
					{
						goto Block_5;
					}
				}
				this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
					{
						this.DetectEncoding();
					}
					this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
				}
				if (this.CharLen_Prop != 0)
				{
					goto Block_9;
				}
			}
			if (this.ByteLen_Prop > 0)
			{
				this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
				this.BytePos_Prop = 0;
				this.ByteLen_Prop = 0;
			}
			return this.CharLen_Prop;
			Block_5:
			return this.CharLen_Prop;
			Block_9:
			return this.CharLen_Prop;
		}

		/// <summary>A <see cref="T:System.IO.StreamReader" /> object around an empty stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040016E2 RID: 5858
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x040016E3 RID: 5859
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x040016E4 RID: 5860
		private const int MinBufferSize = 128;

		// Token: 0x040016E5 RID: 5861
		private Stream stream;

		// Token: 0x040016E6 RID: 5862
		private Encoding encoding;

		// Token: 0x040016E7 RID: 5863
		private Decoder decoder;

		// Token: 0x040016E8 RID: 5864
		private byte[] byteBuffer;

		// Token: 0x040016E9 RID: 5865
		private char[] charBuffer;

		// Token: 0x040016EA RID: 5866
		private byte[] _preamble;

		// Token: 0x040016EB RID: 5867
		private int charPos;

		// Token: 0x040016EC RID: 5868
		private int charLen;

		// Token: 0x040016ED RID: 5869
		private int byteLen;

		// Token: 0x040016EE RID: 5870
		private int bytePos;

		// Token: 0x040016EF RID: 5871
		private int _maxCharsPerBuffer;

		// Token: 0x040016F0 RID: 5872
		private bool _detectEncoding;

		// Token: 0x040016F1 RID: 5873
		private bool _checkPreamble;

		// Token: 0x040016F2 RID: 5874
		private bool _isBlocked;

		// Token: 0x040016F3 RID: 5875
		private bool _closable;

		// Token: 0x040016F4 RID: 5876
		[NonSerialized]
		private volatile Task _asyncReadTask;

		// Token: 0x020003AE RID: 942
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06002C07 RID: 11271 RVA: 0x0009CCF1 File Offset: 0x0009AEF1
			internal NullStreamReader()
			{
				base.Init(Stream.Null);
			}

			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x06002C08 RID: 11272 RVA: 0x0009CD04 File Offset: 0x0009AF04
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x06002C09 RID: 11273 RVA: 0x0009CD0B File Offset: 0x0009AF0B
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06002C0A RID: 11274 RVA: 0x00002194 File Offset: 0x00000394
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06002C0B RID: 11275 RVA: 0x0009B163 File Offset: 0x00099363
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06002C0C RID: 11276 RVA: 0x0009B163 File Offset: 0x00099363
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06002C0D RID: 11277 RVA: 0x00015ED5 File Offset: 0x000140D5
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06002C0E RID: 11278 RVA: 0x0000A42E File Offset: 0x0000862E
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06002C0F RID: 11279 RVA: 0x0003A514 File Offset: 0x00038714
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x06002C10 RID: 11280 RVA: 0x00015ED5 File Offset: 0x000140D5
			internal override int ReadBuffer()
			{
				return 0;
			}
		}
	}
}
