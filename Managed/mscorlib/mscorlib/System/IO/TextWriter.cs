using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Represents a writer that can write a sequential series of characters. This class is abstract.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003C0 RID: 960
	[ComVisible(true)]
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x0009FF89 File Offset: 0x0009E189
		private static string InitialNewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class.</summary>
		// Token: 0x06002C98 RID: 11416 RVA: 0x0009FF90 File Offset: 0x0009E190
		protected TextWriter()
		{
			this.InternalFormatProvider = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class with the specified format provider.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		// Token: 0x06002C99 RID: 11417 RVA: 0x0009FFAF File Offset: 0x0009E1AF
		protected TextWriter(IFormatProvider formatProvider)
		{
			this.InternalFormatProvider = formatProvider;
		}

		/// <summary>Gets an object that controls formatting.</summary>
		/// <returns>An <see cref="T:System.IFormatProvider" /> object for a specific culture, or the formatting of the current culture if no other culture is specified.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x0009FFCE File Offset: 0x0009E1CE
		public virtual IFormatProvider FormatProvider
		{
			get
			{
				if (this.InternalFormatProvider == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return this.InternalFormatProvider;
			}
		}

		/// <summary>Closes the current writer and releases any system resources associated with the writer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C9B RID: 11419 RVA: 0x0009DC35 File Offset: 0x0009BE35
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002C9C RID: 11420 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextWriter" /> object.</summary>
		// Token: 0x06002C9D RID: 11421 RVA: 0x0009DC35 File Offset: 0x0009BE35
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C9E RID: 11422 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void Flush()
		{
		}

		/// <summary>When overridden in a derived class, returns the character encoding in which the output is written.</summary>
		/// <returns>The character encoding in which the output is written.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002C9F RID: 11423
		public abstract Encoding Encoding { get; }

		/// <summary>Gets or sets the line terminator string used by the current TextWriter.</summary>
		/// <returns>The line terminator string for the current TextWriter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x0009FFE9 File Offset: 0x0009E1E9
		// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x0009FFF6 File Offset: 0x0009E1F6
		public virtual string NewLine
		{
			get
			{
				return new string(this.CoreNewLine);
			}
			set
			{
				if (value == null)
				{
					value = TextWriter.InitialNewLine;
				}
				this.CoreNewLine = value.ToCharArray();
			}
		}

		/// <summary>Creates a thread-safe wrapper around the specified TextWriter.</summary>
		/// <returns>A thread-safe wrapper.</returns>
		/// <param name="writer">The TextWriter to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002CA2 RID: 11426 RVA: 0x000A000E File Offset: 0x0009E20E
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer is TextWriter.SyncTextWriter)
			{
				return writer;
			}
			return new TextWriter.SyncTextWriter(writer);
		}

		/// <summary>Writes a character to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA3 RID: 11427 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void Write(char value)
		{
		}

		/// <summary>Writes a character array to the text string or stream.</summary>
		/// <param name="buffer">The character array to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA4 RID: 11428 RVA: 0x000A002E File Offset: 0x0009E22E
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		/// <summary>Writes a subarray of characters to the text string or stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The character position in the buffer at which to start retrieving data. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA5 RID: 11429 RVA: 0x000A0040 File Offset: 0x0009E240
		public virtual void Write(char[] buffer, int index, int count)
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
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		/// <summary>Writes the text representation of a Boolean value to the text string or stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA6 RID: 11430 RVA: 0x000A00C6 File Offset: 0x0009E2C6
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		/// <summary>Writes the text representation of a 4-byte signed integer to the text string or stream.</summary>
		/// <param name="value">The 4-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA7 RID: 11431 RVA: 0x000A00DD File Offset: 0x0009E2DD
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer to the text string or stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA8 RID: 11432 RVA: 0x000A00F2 File Offset: 0x0009E2F2
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte signed integer to the text string or stream.</summary>
		/// <param name="value">The 8-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CA9 RID: 11433 RVA: 0x000A0107 File Offset: 0x0009E307
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer to the text string or stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CAA RID: 11434 RVA: 0x000A011C File Offset: 0x0009E31C
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value to the text string or stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CAB RID: 11435 RVA: 0x000A0131 File Offset: 0x0009E331
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte floating-point value to the text string or stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CAC RID: 11436 RVA: 0x000A0146 File Offset: 0x0009E346
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a decimal value to the text string or stream.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CAD RID: 11437 RVA: 0x000A015B File Offset: 0x0009E35B
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes a string to the text string or stream.</summary>
		/// <param name="value">The string to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CAE RID: 11438 RVA: 0x000A0170 File Offset: 0x0009E370
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		/// <summary>Writes the text representation of an object to the text string or stream by calling the ToString method on that object.</summary>
		/// <param name="value">The object to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CAF RID: 11439 RVA: 0x000A0184 File Offset: 0x0009E384
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is one). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000A01BE File Offset: 0x0009E3BE
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero) or greater than or equal to the number of objects to be formatted (which, for this method overload, is two). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB1 RID: 11441 RVA: 0x000A01D3 File Offset: 0x0009E3D3
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <param name="arg2">The third object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is three). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x000A01E9 File Offset: 0x0009E3E9
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object[])" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg">An object array that contains zero or more objects to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="arg" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB3 RID: 11443 RVA: 0x000A0201 File Offset: 0x0009E401
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		/// <summary>Writes a line terminator to the text string or stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB4 RID: 11444 RVA: 0x000A0216 File Offset: 0x0009E416
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		/// <summary>Writes a character followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB5 RID: 11445 RVA: 0x000A0224 File Offset: 0x0009E424
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes an array of characters followed by a line terminator to the text string or stream.</summary>
		/// <param name="buffer">The character array from which data is read. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB6 RID: 11446 RVA: 0x000A0233 File Offset: 0x0009E433
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		/// <summary>Writes a subarray of characters followed by a line terminator to the text string or stream.</summary>
		/// <param name="buffer">The character array from which data is read. </param>
		/// <param name="index">The character position in <paramref name="buffer" /> at which to start reading data. </param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB7 RID: 11447 RVA: 0x000A0242 File Offset: 0x0009E442
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a Boolean value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB8 RID: 11448 RVA: 0x000A0253 File Offset: 0x0009E453
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte signed integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CB9 RID: 11449 RVA: 0x000A0262 File Offset: 0x0009E462
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CBA RID: 11450 RVA: 0x000A0271 File Offset: 0x0009E471
		[CLSCompliant(false)]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte signed integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CBB RID: 11451 RVA: 0x000A0280 File Offset: 0x0009E480
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CBC RID: 11452 RVA: 0x000A028F File Offset: 0x0009E48F
		[CLSCompliant(false)]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CBD RID: 11453 RVA: 0x000A029E File Offset: 0x0009E49E
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CBE RID: 11454 RVA: 0x000A02AD File Offset: 0x0009E4AD
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a decimal value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CBF RID: 11455 RVA: 0x000A02BC File Offset: 0x0009E4BC
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes a string followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, only the line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CC0 RID: 11456 RVA: 0x000A02CC File Offset: 0x0009E4CC
		public virtual void WriteLine(string value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			int length = value.Length;
			int num = this.CoreNewLine.Length;
			char[] array = new char[length + num];
			value.CopyTo(0, array, 0, length);
			if (num == 2)
			{
				array[length] = this.CoreNewLine[0];
				array[length + 1] = this.CoreNewLine[1];
			}
			else if (num == 1)
			{
				array[length] = this.CoreNewLine[0];
			}
			else
			{
				Buffer.InternalBlockCopy(this.CoreNewLine, 0, array, length * 2, num * 2);
			}
			this.Write(array, 0, length + num);
		}

		/// <summary>Writes the text representation of an object by calling the ToString method on that object, followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The object to write. If <paramref name="value" /> is null, only the line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000A0354 File Offset: 0x0009E554
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		/// <summary>Writes a formatted string and a new line to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is one). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CC2 RID: 11458 RVA: 0x000A0395 File Offset: 0x0009E595
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		/// <summary>Writes a formatted string and a new line to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is two). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CC3 RID: 11459 RVA: 0x000A03AA File Offset: 0x0009E5AA
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <param name="arg2">The third object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is three). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CC4 RID: 11460 RVA: 0x000A03C0 File Offset: 0x0009E5C0
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg">An object array that contains zero or more objects to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">A string or object is passed in as null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="arg" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002CC5 RID: 11461 RVA: 0x000A03D8 File Offset: 0x0009E5D8
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		/// <summary>Writes a character to the text string or stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CC6 RID: 11462 RVA: 0x000A03F0 File Offset: 0x0009E5F0
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteCharDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a string to the text string or stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation. </returns>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, nothing is written to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CC7 RID: 11463 RVA: 0x000A0420 File Offset: 0x0009E620
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteStringDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a character array to the text string or stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write to the text stream. If <paramref name="buffer" /> is null, nothing is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CC8 RID: 11464 RVA: 0x000A0450 File Offset: 0x0009E650
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteAsync(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a subarray of characters to the text string or stream asynchronously. </summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The character position in the buffer at which to start retrieving data. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CC9 RID: 11465 RVA: 0x000A0468 File Offset: 0x0009E668
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteCharArrayRangeDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CCA RID: 11466 RVA: 0x000A049C File Offset: 0x0009E69C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineCharDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the text string or stream. </summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write. If the value is null, only a line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CCB RID: 11467 RVA: 0x000A04CC File Offset: 0x0009E6CC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineStringDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes an array of characters followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write to the text stream. If the character array is null, only the line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CCC RID: 11468 RVA: 0x000A04FC File Offset: 0x0009E6FC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteLineAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteLineAsync(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The character position in the buffer at which to start retrieving data. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CCD RID: 11469 RVA: 0x000A0514 File Offset: 0x0009E714
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteLineCharArrayRangeDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a line terminator asynchronously to the text string or stream.</summary>
		/// <returns>A task that represents the asynchronous write operation. </returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CCE RID: 11470 RVA: 0x000A0546 File Offset: 0x0009E746
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync()
		{
			return this.WriteAsync(this.CoreNewLine);
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device. </summary>
		/// <returns>A task that represents the asynchronous flush operation. </returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is currently in use by a previous write operation. </exception>
		// Token: 0x06002CCF RID: 11471 RVA: 0x000A0554 File Offset: 0x0009E754
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(TextWriter._FlushDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Provides a TextWriter with no backing store that can be written to, but not read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001776 RID: 6006
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x04001777 RID: 6007
		[NonSerialized]
		private static Action<object> _WriteCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
			tuple.Item1.Write(tuple.Item2);
		};

		// Token: 0x04001778 RID: 6008
		[NonSerialized]
		private static Action<object> _WriteStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple2 = (Tuple<TextWriter, string>)state;
			tuple2.Item1.Write(tuple2.Item2);
		};

		// Token: 0x04001779 RID: 6009
		[NonSerialized]
		private static Action<object> _WriteCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple3 = (Tuple<TextWriter, char[], int, int>)state;
			tuple3.Item1.Write(tuple3.Item2, tuple3.Item3, tuple3.Item4);
		};

		// Token: 0x0400177A RID: 6010
		[NonSerialized]
		private static Action<object> _WriteLineCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple4 = (Tuple<TextWriter, char>)state;
			tuple4.Item1.WriteLine(tuple4.Item2);
		};

		// Token: 0x0400177B RID: 6011
		[NonSerialized]
		private static Action<object> _WriteLineStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple5 = (Tuple<TextWriter, string>)state;
			tuple5.Item1.WriteLine(tuple5.Item2);
		};

		// Token: 0x0400177C RID: 6012
		[NonSerialized]
		private static Action<object> _WriteLineCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple6 = (Tuple<TextWriter, char[], int, int>)state;
			tuple6.Item1.WriteLine(tuple6.Item2, tuple6.Item3, tuple6.Item4);
		};

		// Token: 0x0400177D RID: 6013
		[NonSerialized]
		private static Action<object> _FlushDelegate = delegate(object state)
		{
			((TextWriter)state).Flush();
		};

		/// <summary>Stores the newline characters used for this TextWriter.</summary>
		// Token: 0x0400177E RID: 6014
		protected char[] CoreNewLine = TextWriter.InitialNewLine.ToCharArray();

		// Token: 0x0400177F RID: 6015
		private IFormatProvider InternalFormatProvider;

		// Token: 0x020003C1 RID: 961
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x06002CD1 RID: 11473 RVA: 0x000A061E File Offset: 0x0009E81E
			internal NullTextWriter()
				: base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000A062B File Offset: 0x0009E82B
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x06002CD3 RID: 11475 RVA: 0x00002194 File Offset: 0x00000394
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x06002CD4 RID: 11476 RVA: 0x00002194 File Offset: 0x00000394
			public override void Write(string value)
			{
			}

			// Token: 0x06002CD5 RID: 11477 RVA: 0x00002194 File Offset: 0x00000394
			public override void WriteLine()
			{
			}

			// Token: 0x06002CD6 RID: 11478 RVA: 0x00002194 File Offset: 0x00000394
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06002CD7 RID: 11479 RVA: 0x00002194 File Offset: 0x00000394
			public override void WriteLine(object value)
			{
			}
		}

		// Token: 0x020003C2 RID: 962
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x06002CD8 RID: 11480 RVA: 0x000A0632 File Offset: 0x0009E832
			internal SyncTextWriter(TextWriter t)
				: base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000A0647 File Offset: 0x0009E847
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000A0654 File Offset: 0x0009E854
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000A0661 File Offset: 0x0009E861
			// (set) Token: 0x06002CDC RID: 11484 RVA: 0x000A066E File Offset: 0x0009E86E
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x06002CDD RID: 11485 RVA: 0x000A067C File Offset: 0x0009E87C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06002CDE RID: 11486 RVA: 0x000A0689 File Offset: 0x0009E889
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06002CDF RID: 11487 RVA: 0x000A0699 File Offset: 0x0009E899
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x06002CE0 RID: 11488 RVA: 0x000A06A6 File Offset: 0x0009E8A6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE1 RID: 11489 RVA: 0x000A06B4 File Offset: 0x0009E8B4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x06002CE2 RID: 11490 RVA: 0x000A06C2 File Offset: 0x0009E8C2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x06002CE3 RID: 11491 RVA: 0x000A06D2 File Offset: 0x0009E8D2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE4 RID: 11492 RVA: 0x000A06E0 File Offset: 0x0009E8E0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE5 RID: 11493 RVA: 0x000A06EE File Offset: 0x0009E8EE
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE6 RID: 11494 RVA: 0x000A06FC File Offset: 0x0009E8FC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE7 RID: 11495 RVA: 0x000A070A File Offset: 0x0009E90A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE8 RID: 11496 RVA: 0x000A0718 File Offset: 0x0009E918
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CE9 RID: 11497 RVA: 0x000A0726 File Offset: 0x0009E926
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CEA RID: 11498 RVA: 0x000A0734 File Offset: 0x0009E934
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CEB RID: 11499 RVA: 0x000A0742 File Offset: 0x0009E942
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CEC RID: 11500 RVA: 0x000A0750 File Offset: 0x0009E950
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x06002CED RID: 11501 RVA: 0x000A075E File Offset: 0x0009E95E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06002CEE RID: 11502 RVA: 0x000A076D File Offset: 0x0009E96D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x06002CEF RID: 11503 RVA: 0x000A077D File Offset: 0x0009E97D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06002CF0 RID: 11504 RVA: 0x000A078F File Offset: 0x0009E98F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, params object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x06002CF1 RID: 11505 RVA: 0x000A079E File Offset: 0x0009E99E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x06002CF2 RID: 11506 RVA: 0x000A07AB File Offset: 0x0009E9AB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CF3 RID: 11507 RVA: 0x000A07B9 File Offset: 0x0009E9B9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CF4 RID: 11508 RVA: 0x000A07C7 File Offset: 0x0009E9C7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x06002CF5 RID: 11509 RVA: 0x000A07D5 File Offset: 0x0009E9D5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x06002CF6 RID: 11510 RVA: 0x000A07E5 File Offset: 0x0009E9E5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CF7 RID: 11511 RVA: 0x000A07F3 File Offset: 0x0009E9F3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CF8 RID: 11512 RVA: 0x000A0801 File Offset: 0x0009EA01
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CF9 RID: 11513 RVA: 0x000A080F File Offset: 0x0009EA0F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CFA RID: 11514 RVA: 0x000A081D File Offset: 0x0009EA1D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CFB RID: 11515 RVA: 0x000A082B File Offset: 0x0009EA2B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CFC RID: 11516 RVA: 0x000A0839 File Offset: 0x0009EA39
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CFD RID: 11517 RVA: 0x000A0847 File Offset: 0x0009EA47
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CFE RID: 11518 RVA: 0x000A0855 File Offset: 0x0009EA55
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06002CFF RID: 11519 RVA: 0x000A0863 File Offset: 0x0009EA63
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x06002D00 RID: 11520 RVA: 0x000A0872 File Offset: 0x0009EA72
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06002D01 RID: 11521 RVA: 0x000A0882 File Offset: 0x0009EA82
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06002D02 RID: 11522 RVA: 0x000A0894 File Offset: 0x0009EA94
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x06002D03 RID: 11523 RVA: 0x0009F76A File Offset: 0x0009D96A
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06002D04 RID: 11524 RVA: 0x0009F778 File Offset: 0x0009D978
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06002D05 RID: 11525 RVA: 0x0009F786 File Offset: 0x0009D986
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06002D06 RID: 11526 RVA: 0x0009F796 File Offset: 0x0009D996
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06002D07 RID: 11527 RVA: 0x0009F7A4 File Offset: 0x0009D9A4
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(string value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06002D08 RID: 11528 RVA: 0x0009F7B2 File Offset: 0x0009D9B2
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char[] buffer, int index, int count)
			{
				this.WriteLine(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06002D09 RID: 11529 RVA: 0x000A08A3 File Offset: 0x0009EAA3
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x04001780 RID: 6016
			private TextWriter _out;
		}
	}
}
