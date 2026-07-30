using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Represents a reader that can read a sequential series of characters.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003BA RID: 954
	[ComVisible(true)]
	[Serializable]
	public abstract class TextReader : MarshalByRefObject, IDisposable
	{
		/// <summary>Closes the <see cref="T:System.IO.TextReader" /> and releases any system resources associated with the TextReader.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C6E RID: 11374 RVA: 0x0009F7D6 File Offset: 0x0009D9D6
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextReader" /> object.</summary>
		// Token: 0x06002C6F RID: 11375 RVA: 0x0009F7D6 File Offset: 0x0009D9D6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002C70 RID: 11376 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Reads the next character without changing the state of the reader or the character source. Returns the next available character without actually reading it from the reader.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if no more characters are available or the reader does not support seeking.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C71 RID: 11377 RVA: 0x0009B163 File Offset: 0x00099363
		public virtual int Peek()
		{
			return -1;
		}

		/// <summary>Reads the next character from the text reader and advances the character position by one character.</summary>
		/// <returns>The next character from the text reader, or -1 if no more characters are available. The default implementation returns -1.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C72 RID: 11378 RVA: 0x0009B163 File Offset: 0x00099363
		public virtual int Read()
		{
			return -1;
		}

		/// <summary>Reads a specified maximum number of characters from the current reader and writes the data to a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether the data is available within the reader. This method returns 0 (zero) if it is called when no more characters are left to read.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source. </param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The maximum number of characters to read. If the end of the reader is reached before the specified number of characters is read into the buffer, the method returns. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C73 RID: 11379 RVA: 0x0009F7E8 File Offset: 0x0009D9E8
		public virtual int Read([In] [Out] char[] buffer, int index, int count)
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
			int num = 0;
			do
			{
				int num2 = this.Read();
				if (num2 == -1)
				{
					break;
				}
				buffer[index + num++] = (char)num2;
			}
			while (num < count);
			return num;
		}

		/// <summary>Reads all characters from the current position to the end of the text reader and returns them as one string.</summary>
		/// <returns>A string that contains all characters from the current position to the end of the text reader.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C74 RID: 11380 RVA: 0x0009F874 File Offset: 0x0009DA74
		public virtual string ReadToEnd()
		{
			char[] array = new char[4096];
			StringBuilder stringBuilder = new StringBuilder(4096);
			int num;
			while ((num = this.Read(array, 0, array.Length)) != 0)
			{
				stringBuilder.Append(array, 0, num);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader and writes the data to a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether all input characters have been read.</returns>
		/// <param name="buffer">When this method returns, this parameter contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> -1) replaced by the characters read from the current source. </param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C75 RID: 11381 RVA: 0x0009F8B8 File Offset: 0x0009DAB8
		public virtual int ReadBlock([In] [Out] char[] buffer, int index, int count)
		{
			int num = 0;
			int num2;
			do
			{
				num += (num2 = this.Read(buffer, index + num, count - num));
			}
			while (num2 > 0 && num < count);
			return num;
		}

		/// <summary>Reads a line of characters from the text reader and returns the data as a string.</summary>
		/// <returns>The next line from the reader, or null if all characters have been read.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C76 RID: 11382 RVA: 0x0009F8E4 File Offset: 0x0009DAE4
		public virtual string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				num = this.Read();
				if (num == -1)
				{
					goto IL_0043;
				}
				if (num == 13 || num == 10)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			if (num == 13 && this.Peek() == 10)
			{
				this.Read();
			}
			return stringBuilder.ToString();
			IL_0043:
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		/// <summary>Reads a line of characters asynchronously and returns the data as a string. </summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the next line from the text reader, or is null if all of the characters have been read. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002C77 RID: 11383 RVA: 0x0009F945 File Offset: 0x0009DB45
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<string> ReadLineAsync()
		{
			return Task<string>.Factory.StartNew(TextReader._ReadLineDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Reads all characters from the current position to the end of the text reader asynchronously and returns them as one string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains a string with the characters from the current position to the end of the text reader. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002C78 RID: 11384 RVA: 0x0009F964 File Offset: 0x0009DB64
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual async Task<string> ReadToEndAsync()
		{
			char[] chars = new char[4096];
			StringBuilder sb = new StringBuilder(4096);
			for (;;)
			{
				int num = await this.ReadAsyncInternal(chars, 0, chars.Length).ConfigureAwait(false);
				int len;
				if ((len = num) == 0)
				{
					break;
				}
				sb.Append(chars, 0, len);
			}
			return sb.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index. </summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002C79 RID: 11385 RVA: 0x0009F9AC File Offset: 0x0009DBAC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
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
			return this.ReadAsyncInternal(buffer, index, count);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x0009FA1C File Offset: 0x0009DC1C
		internal virtual Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
		{
			Tuple<TextReader, char[], int, int> tuple = new Tuple<TextReader, char[], int, int>(this, buffer, index, count);
			return Task<int>.Factory.StartNew(TextReader._ReadDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06002C7B RID: 11387 RVA: 0x0009FA50 File Offset: 0x0009DC50
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
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
			return this.ReadBlockAsyncInternal(buffer, index, count);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0009FAC0 File Offset: 0x0009DCC0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		private async Task<int> ReadBlockAsyncInternal(char[] buffer, int index, int count)
		{
			int i = 0;
			int num;
			do
			{
				num = await this.ReadAsyncInternal(buffer, index + i, count - i).ConfigureAwait(false);
				i += num;
			}
			while (num > 0 && i < count);
			return i;
		}

		/// <summary>Creates a thread-safe wrapper around the specified TextReader.</summary>
		/// <returns>A thread-safe <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="reader">The TextReader to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C7D RID: 11389 RVA: 0x0009FB1D File Offset: 0x0009DD1D
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader is TextReader.SyncTextReader)
			{
				return reader;
			}
			return new TextReader.SyncTextReader(reader);
		}

		// Token: 0x04001762 RID: 5986
		[NonSerialized]
		private static Func<object, string> _ReadLineDelegate = (object state) => ((TextReader)state).ReadLine();

		// Token: 0x04001763 RID: 5987
		[NonSerialized]
		private static Func<object, int> _ReadDelegate = delegate(object state)
		{
			Tuple<TextReader, char[], int, int> tuple = (Tuple<TextReader, char[], int, int>)state;
			return tuple.Item1.Read(tuple.Item2, tuple.Item3, tuple.Item4);
		};

		/// <summary>Provides a TextReader with no data to read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001764 RID: 5988
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x020003BB RID: 955
		[Serializable]
		private sealed class NullTextReader : TextReader
		{
			// Token: 0x06002C80 RID: 11392 RVA: 0x00015ED5 File Offset: 0x000140D5
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06002C81 RID: 11393 RVA: 0x0000A42E File Offset: 0x0000862E
			public override string ReadLine()
			{
				return null;
			}
		}

		// Token: 0x020003BC RID: 956
		[Serializable]
		internal sealed class SyncTextReader : TextReader
		{
			// Token: 0x06002C82 RID: 11394 RVA: 0x0009FB73 File Offset: 0x0009DD73
			internal SyncTextReader(TextReader t)
			{
				this._in = t;
			}

			// Token: 0x06002C83 RID: 11395 RVA: 0x0009FB82 File Offset: 0x0009DD82
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._in.Close();
			}

			// Token: 0x06002C84 RID: 11396 RVA: 0x0009FB8F File Offset: 0x0009DD8F
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._in).Dispose();
				}
			}

			// Token: 0x06002C85 RID: 11397 RVA: 0x0009FB9F File Offset: 0x0009DD9F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Peek()
			{
				return this._in.Peek();
			}

			// Token: 0x06002C86 RID: 11398 RVA: 0x0009FBAC File Offset: 0x0009DDAC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read()
			{
				return this._in.Read();
			}

			// Token: 0x06002C87 RID: 11399 RVA: 0x0009FBB9 File Offset: 0x0009DDB9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read([In] [Out] char[] buffer, int index, int count)
			{
				return this._in.Read(buffer, index, count);
			}

			// Token: 0x06002C88 RID: 11400 RVA: 0x0009FBC9 File Offset: 0x0009DDC9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int ReadBlock([In] [Out] char[] buffer, int index, int count)
			{
				return this._in.ReadBlock(buffer, index, count);
			}

			// Token: 0x06002C89 RID: 11401 RVA: 0x0009FBD9 File Offset: 0x0009DDD9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadLine()
			{
				return this._in.ReadLine();
			}

			// Token: 0x06002C8A RID: 11402 RVA: 0x0009FBE6 File Offset: 0x0009DDE6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadToEnd()
			{
				return this._in.ReadToEnd();
			}

			// Token: 0x06002C8B RID: 11403 RVA: 0x0009F4FF File Offset: 0x0009D6FF
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadLineAsync()
			{
				return Task.FromResult<string>(this.ReadLine());
			}

			// Token: 0x06002C8C RID: 11404 RVA: 0x0009F50C File Offset: 0x0009D70C
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadToEndAsync()
			{
				return Task.FromResult<string>(this.ReadToEnd());
			}

			// Token: 0x06002C8D RID: 11405 RVA: 0x0009FBF4 File Offset: 0x0009DDF4
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
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
				return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
			}

			// Token: 0x06002C8E RID: 11406 RVA: 0x0009FC68 File Offset: 0x0009DE68
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
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
				return Task.FromResult<int>(this.Read(buffer, index, count));
			}

			// Token: 0x04001765 RID: 5989
			internal TextReader _in;
		}
	}
}
