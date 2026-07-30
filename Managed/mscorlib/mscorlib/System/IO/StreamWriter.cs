using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing characters to a stream in a particular encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003B3 RID: 947
	[ComVisible(true)]
	[Serializable]
	public class StreamWriter : TextWriter
	{
		// Token: 0x06002C19 RID: 11289 RVA: 0x0009D9B4 File Offset: 0x0009BBB4
		private void CheckAsyncTaskInProgress()
		{
			Task asyncWriteTask = this._asyncWriteTask;
			if (asyncWriteTask != null && !asyncWriteTask.IsCompleted)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The stream is currently in use by a previous operation on the stream."));
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x0009D9E5 File Offset: 0x0009BBE5
		internal static Encoding UTF8NoBOM
		{
			[FriendAccessAllowed]
			get
			{
				if (StreamWriter._UTF8NoBOM == null)
				{
					Encoding encoding = new UTF8Encoding(false, true);
					Thread.MemoryBarrier();
					StreamWriter._UTF8NoBOM = encoding;
				}
				return StreamWriter._UTF8NoBOM;
			}
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0009DA0A File Offset: 0x0009BC0A
		internal StreamWriter()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using UTF-8 encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06002C1C RID: 11292 RVA: 0x0009DA13 File Offset: 0x0009BC13
		public StreamWriter(Stream stream)
			: this(stream, StreamWriter.UTF8NoBOM, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06002C1D RID: 11293 RVA: 0x0009DA27 File Offset: 0x0009BC27
		public StreamWriter(Stream stream, Encoding encoding)
			: this(stream, encoding, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="bufferSize">The buffer size, in bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06002C1E RID: 11294 RVA: 0x0009DA37 File Offset: 0x0009BC37
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
			: this(stream, encoding, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">The buffer size, in bytes.</param>
		/// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.StreamWriter" /> object is disposed; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06002C1F RID: 11295 RVA: 0x0009DA44 File Offset: 0x0009BC44
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
			: base(null)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			this.Init(stream, encoding, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the default encoding and buffer size.</summary>
		/// <param name="path">The complete file path to write to. <paramref name="path" /> can be a file name. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002C20 RID: 11296 RVA: 0x0009DAAF File Offset: 0x0009BCAF
		public StreamWriter(string path)
			: this(path, false, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the default encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty. -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002C21 RID: 11297 RVA: 0x0009DAC3 File Offset: 0x0009BCC3
		public StreamWriter(string path, bool append)
			: this(path, append, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the specified encoding and default buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty. -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002C22 RID: 11298 RVA: 0x0009DAD7 File Offset: 0x0009BCD7
		public StreamWriter(string path, bool append, Encoding encoding)
			: this(path, append, encoding, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the specified encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="bufferSize">The buffer size, in bytes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		// Token: 0x06002C23 RID: 11299 RVA: 0x0009DAE7 File Offset: 0x0009BCE7
		[SecuritySafeCritical]
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
			: this(path, append, encoding, bufferSize, true)
		{
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0009DAF8 File Offset: 0x0009BCF8
		[SecurityCritical]
		internal StreamWriter(string path, bool append, Encoding encoding, int bufferSize, bool checkHost)
			: base(null)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty path name is not legal."));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			Stream stream = StreamWriter.CreateFile(path, append, checkHost);
			this.Init(stream, encoding, bufferSize, false);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0009DB70 File Offset: 0x0009BD70
		[SecuritySafeCritical]
		private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
		{
			this.stream = streamArg;
			this.encoding = encodingArg;
			this.encoder = this.encoding.GetEncoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.charBuffer = new char[bufferSize];
			this.byteBuffer = new byte[this.encoding.GetMaxByteCount(bufferSize)];
			this.charLen = bufferSize;
			if (this.stream.CanSeek && this.stream.Position > 0L)
			{
				this.haveWrittenPreamble = true;
			}
			this.closable = !shouldLeaveOpen;
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0009DC04 File Offset: 0x0009BE04
		[SecurityCritical]
		private static Stream CreateFile(string path, bool append, bool checkHost)
		{
			FileMode fileMode = (append ? FileMode.Append : FileMode.Create);
			return new FileStream(path, fileMode, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
		}

		/// <summary>Closes the current StreamWriter object and the underlying stream.</summary>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C27 RID: 11303 RVA: 0x0009DC35 File Offset: 0x0009BE35
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StreamWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		// Token: 0x06002C28 RID: 11304 RVA: 0x0009DC44 File Offset: 0x0009BE44
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.stream != null)
				{
					if (!disposing)
					{
						if (this.LeaveOpen)
						{
							Stream stream = this.stream;
						}
					}
					else
					{
						this.CheckAsyncTaskInProgress();
						this.Flush(true, true);
					}
				}
			}
			finally
			{
				if (!this.LeaveOpen && this.stream != null)
				{
					try
					{
						if (disposing)
						{
							this.stream.Close();
						}
					}
					finally
					{
						this.stream = null;
						this.byteBuffer = null;
						this.charBuffer = null;
						this.encoding = null;
						this.encoder = null;
						this.charLen = 0;
						base.Dispose(disposing);
					}
				}
			}
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current writer is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C29 RID: 11305 RVA: 0x0009DCEC File Offset: 0x0009BEEC
		public override void Flush()
		{
			this.CheckAsyncTaskInProgress();
			this.Flush(true, true);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0009DCFC File Offset: 0x0009BEFC
		private void Flush(bool flushStream, bool flushEncoder)
		{
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			if (this.charPos == 0 && ((!flushStream && !flushEncoder) || CompatibilitySwitches.IsAppEarlierThanWindowsPhone8))
			{
				return;
			}
			if (!this.haveWrittenPreamble)
			{
				this.haveWrittenPreamble = true;
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					this.stream.Write(preamble, 0, preamble.Length);
				}
			}
			int bytes = this.encoder.GetBytes(this.charBuffer, 0, this.charPos, this.byteBuffer, 0, flushEncoder);
			this.charPos = 0;
			if (bytes > 0)
			{
				this.stream.Write(this.byteBuffer, 0, bytes);
			}
			if (flushStream)
			{
				this.stream.Flush();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.IO.StreamWriter" /> will flush its buffer to the underlying stream after every call to <see cref="M:System.IO.StreamWriter.Write(System.Char)" />.</summary>
		/// <returns>true to force <see cref="T:System.IO.StreamWriter" /> to flush its buffer; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x0009DDA8 File Offset: 0x0009BFA8
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x0009DDB0 File Offset: 0x0009BFB0
		public virtual bool AutoFlush
		{
			get
			{
				return this.autoFlush;
			}
			set
			{
				this.CheckAsyncTaskInProgress();
				this.autoFlush = value;
				if (value)
				{
					this.Flush(true, false);
				}
			}
		}

		/// <summary>Gets the underlying stream that interfaces with a backing store.</summary>
		/// <returns>The stream this StreamWriter is writing to.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x0009DDCA File Offset: 0x0009BFCA
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x0009DDD2 File Offset: 0x0009BFD2
		internal bool LeaveOpen
		{
			get
			{
				return !this.closable;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x0009DDDD File Offset: 0x0009BFDD
		internal bool HaveWrittenPreamble
		{
			set
			{
				this.haveWrittenPreamble = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> specified in the constructor for the current instance, or <see cref="T:System.Text.UTF8Encoding" /> if an encoding was not specified.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x0009DDE6 File Offset: 0x0009BFE6
		public override Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		/// <summary>Writes a character to the stream.</summary>
		/// <param name="value">The character to write to the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C31 RID: 11313 RVA: 0x0009DDF0 File Offset: 0x0009BFF0
		public override void Write(char value)
		{
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen)
			{
				this.Flush(false, false);
			}
			this.charBuffer[this.charPos] = value;
			this.charPos++;
			if (this.autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a character array to the stream.</summary>
		/// <param name="buffer">A character array containing the data to write. If <paramref name="buffer" /> is null, nothing is written. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C32 RID: 11314 RVA: 0x0009DE48 File Offset: 0x0009C048
		public override void Write(char[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			int num2;
			for (int i = buffer.Length; i > 0; i -= num2)
			{
				if (this.charPos == this.charLen)
				{
					this.Flush(false, false);
				}
				num2 = this.charLen - this.charPos;
				if (num2 > i)
				{
					num2 = i;
				}
				Buffer.InternalBlockCopy(buffer, num * 2, this.charBuffer, this.charPos * 2, num2 * 2);
				this.charPos += num2;
				num += num2;
			}
			if (this.autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a subarray of characters to the stream.</summary>
		/// <param name="buffer">A character array that contains the data to write. </param>
		/// <param name="index">The character position in the buffer at which to start reading data. </param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C33 RID: 11315 RVA: 0x0009DED8 File Offset: 0x0009C0D8
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			this.CheckAsyncTaskInProgress();
			while (count > 0)
			{
				if (this.charPos == this.charLen)
				{
					this.Flush(false, false);
				}
				int num = this.charLen - this.charPos;
				if (num > count)
				{
					num = count;
				}
				Buffer.InternalBlockCopy(buffer, index * 2, this.charBuffer, this.charPos * 2, num * 2);
				this.charPos += num;
				index += num;
				count -= num;
			}
			if (this.autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a string to the stream.</summary>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is null, nothing is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C34 RID: 11316 RVA: 0x0009DFC0 File Offset: 0x0009C1C0
		public override void Write(string value)
		{
			if (value != null)
			{
				this.CheckAsyncTaskInProgress();
				int i = value.Length;
				int num = 0;
				while (i > 0)
				{
					if (this.charPos == this.charLen)
					{
						this.Flush(false, false);
					}
					int num2 = this.charLen - this.charPos;
					if (num2 > i)
					{
						num2 = i;
					}
					value.CopyTo(num, this.charBuffer, this.charPos, num2);
					this.charPos += num2;
					num += num2;
					i -= num2;
				}
				if (this.autoFlush)
				{
					this.Flush(true, false);
				}
			}
		}

		/// <summary>Writes a character to the stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06002C35 RID: 11317 RVA: 0x0009E04C File Offset: 0x0009C24C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0009E0BC File Offset: 0x0009C2BC
		private static async Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			if (charPos == charLen)
			{
				await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			charBuffer[charPos] = value;
			charPos++;
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		/// <summary>Writes a string to the stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is null, nothing is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06002C37 RID: 11319 RVA: 0x0009E140 File Offset: 0x0009C340
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (value != null)
			{
				if (this.stream == null)
				{
					__Error.WriterClosed();
				}
				this.CheckAsyncTaskInProgress();
				Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
				this._asyncWriteTask = task;
				return task;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0009E1BC File Offset: 0x0009C3BC
		private static async Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			int count = value.Length;
			int index = 0;
			while (count > 0)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
					charPos = 0;
				}
				int num = charLen - charPos;
				if (num > count)
				{
					num = count;
				}
				value.CopyTo(index, charBuffer, charPos, num);
				charPos += num;
				index += num;
				count -= num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		/// <summary>Writes a subarray of characters to the stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">A character array that contains the data to write.</param>
		/// <param name="index">The character position in the buffer at which to begin reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C39 RID: 11321 RVA: 0x0009E240 File Offset: 0x0009C440
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0009E318 File Offset: 0x0009C518
		private static async Task WriteAsyncInternal(StreamWriter _this, char[] buffer, int index, int count, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			while (count > 0)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
					charPos = 0;
				}
				int num = charLen - charPos;
				if (num > count)
				{
					num = count;
				}
				Buffer.InternalBlockCopy(buffer, index * 2, charBuffer, charPos * 2, num * 2);
				charPos += num;
				index += num;
				count -= num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		/// <summary>Writes a line terminator asynchronously to the stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06002C3B RID: 11323 RVA: 0x0009E3AC File Offset: 0x0009C5AC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync();
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, null, 0, 0, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06002C3C RID: 11324 RVA: 0x0009E420 File Offset: 0x0009C620
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write. If the value is null, only a line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06002C3D RID: 11325 RVA: 0x0009E490 File Offset: 0x0009C690
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value ?? "", this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The character position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C3E RID: 11326 RVA: 0x0009E50C File Offset: 0x0009C70C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Clears all buffers for this stream asynchronously and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002C3F RID: 11327 RVA: 0x0009E5E4 File Offset: 0x0009C7E4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.FlushAsync();
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = this.FlushAsyncInternal(true, true, this.charBuffer, this.charPos);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x1700070F RID: 1807
		// (set) Token: 0x06002C40 RID: 11328 RVA: 0x0009E641 File Offset: 0x0009C841
		private int CharPos_Prop
		{
			set
			{
				this.charPos = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x0009DDDD File Offset: 0x0009BFDD
		private bool HaveWrittenPreamble_Prop
		{
			set
			{
				this.haveWrittenPreamble = value;
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0009E64C File Offset: 0x0009C84C
		private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos)
		{
			if (sCharPos == 0 && !flushStream && !flushEncoder)
			{
				return Task.CompletedTask;
			}
			Task task = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this.haveWrittenPreamble, this.encoding, this.encoder, this.byteBuffer, this.stream);
			this.charPos = 0;
			return task;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0009E69C File Offset: 0x0009C89C
		private static async Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream)
		{
			if (!haveWrittenPreamble)
			{
				_this.HaveWrittenPreamble_Prop = true;
				byte[] preamble = encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					await stream.WriteAsync(preamble, 0, preamble.Length).ConfigureAwait(false);
				}
			}
			int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
			if (bytes > 0)
			{
				await stream.WriteAsync(byteBuffer, 0, bytes).ConfigureAwait(false);
			}
			if (flushStream)
			{
				await stream.FlushAsync().ConfigureAwait(false);
			}
		}

		// Token: 0x04001716 RID: 5910
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04001717 RID: 5911
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x04001718 RID: 5912
		private const int MinBufferSize = 128;

		// Token: 0x04001719 RID: 5913
		private const int DontCopyOnWriteLineThreshold = 512;

		/// <summary>Provides a StreamWriter with no backing store that can be written to, but not read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400171A RID: 5914
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, new UTF8Encoding(false, true), 128, true);

		// Token: 0x0400171B RID: 5915
		private Stream stream;

		// Token: 0x0400171C RID: 5916
		private Encoding encoding;

		// Token: 0x0400171D RID: 5917
		private Encoder encoder;

		// Token: 0x0400171E RID: 5918
		private byte[] byteBuffer;

		// Token: 0x0400171F RID: 5919
		private char[] charBuffer;

		// Token: 0x04001720 RID: 5920
		private int charPos;

		// Token: 0x04001721 RID: 5921
		private int charLen;

		// Token: 0x04001722 RID: 5922
		private bool autoFlush;

		// Token: 0x04001723 RID: 5923
		private bool haveWrittenPreamble;

		// Token: 0x04001724 RID: 5924
		private bool closable;

		// Token: 0x04001725 RID: 5925
		[NonSerialized]
		private volatile Task _asyncWriteTask;

		// Token: 0x04001726 RID: 5926
		private static volatile Encoding _UTF8NoBOM;
	}
}
