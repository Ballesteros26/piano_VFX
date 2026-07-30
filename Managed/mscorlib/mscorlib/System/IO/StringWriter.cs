using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing information to a string. The information is stored in an underlying <see cref="T:System.Text.StringBuilder" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003B9 RID: 953
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class.</summary>
		// Token: 0x06002C59 RID: 11353 RVA: 0x0009F603 File Offset: 0x0009D803
		public StringWriter()
			: this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class with the specified format control.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		// Token: 0x06002C5A RID: 11354 RVA: 0x0009F615 File Offset: 0x0009D815
		public StringWriter(IFormatProvider formatProvider)
			: this(new StringBuilder(), formatProvider)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is null. </exception>
		// Token: 0x06002C5B RID: 11355 RVA: 0x0009F623 File Offset: 0x0009D823
		public StringWriter(StringBuilder sb)
			: this(sb, CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" /> and has the specified format provider.</summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is null. </exception>
		// Token: 0x06002C5C RID: 11356 RVA: 0x0009F631 File Offset: 0x0009D831
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
			: base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", Environment.GetResourceString("Buffer cannot be null."));
			}
			this._sb = sb;
			this._isOpen = true;
		}

		/// <summary>Closes the current <see cref="T:System.IO.StringWriter" /> and the underlying stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C5D RID: 11357 RVA: 0x0009F660 File Offset: 0x0009D860
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StringWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002C5E RID: 11358 RVA: 0x0009F669 File Offset: 0x0009D869
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The Encoding in which the output is written.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x0009F679 File Offset: 0x0009D879
		public override Encoding Encoding
		{
			get
			{
				if (StringWriter.m_encoding == null)
				{
					StringWriter.m_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.m_encoding;
			}
		}

		/// <summary>Returns the underlying <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <returns>The underlying StringBuilder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C60 RID: 11360 RVA: 0x0009F699 File Offset: 0x0009D899
		public virtual StringBuilder GetStringBuilder()
		{
			return this._sb;
		}

		/// <summary>Writes a character to the string.</summary>
		/// <param name="value">The character to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C61 RID: 11361 RVA: 0x0009F6A1 File Offset: 0x0009D8A1
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(value);
		}

		/// <summary>Writes a subarray of characters to the string.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">(<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />. Length. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C62 RID: 11362 RVA: 0x0009F6C0 File Offset: 0x0009D8C0
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
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(buffer, index, count);
		}

		/// <summary>Writes a string to the current string.</summary>
		/// <param name="value">The string to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C63 RID: 11363 RVA: 0x0009F74B File Offset: 0x0009D94B
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		/// <summary>Writes a character to the string asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the string.</param>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C64 RID: 11364 RVA: 0x0009F76A File Offset: 0x0009D96A
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string to the current string asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, nothing is written to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C65 RID: 11365 RVA: 0x0009F778 File Offset: 0x0009D978
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters to the string asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C66 RID: 11366 RVA: 0x0009F786 File Offset: 0x0009D986
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the string.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the string.</param>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C67 RID: 11367 RVA: 0x0009F796 File Offset: 0x0009D996
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the current string.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write. If the value is null, only a line terminator is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C68 RID: 11368 RVA: 0x0009F7A4 File Offset: 0x0009D9A4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(string value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the string.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002C69 RID: 11369 RVA: 0x0009F7B2 File Offset: 0x0009D9B2
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			this.WriteLine(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device. </summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		// Token: 0x06002C6A RID: 11370 RVA: 0x0009F7C2 File Offset: 0x0009D9C2
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		/// <summary>Returns a string containing the characters written to the current StringWriter so far.</summary>
		/// <returns>The string containing the characters written to the current StringWriter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C6B RID: 11371 RVA: 0x0009F7C9 File Offset: 0x0009D9C9
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x0400175F RID: 5983
		private static volatile UnicodeEncoding m_encoding;

		// Token: 0x04001760 RID: 5984
		private StringBuilder _sb;

		// Token: 0x04001761 RID: 5985
		private bool _isOpen;
	}
}
