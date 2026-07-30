using System;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as <see cref="T:System.IO.FileStream" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C1 RID: 449
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class TextWriterTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with <see cref="T:System.IO.TextWriter" /> as the output recipient.</summary>
		// Token: 0x06000D47 RID: 3399 RVA: 0x0003F83C File Offset: 0x0003DA3C
		public TextWriterTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <exception cref="T:System.ArgumentNullException">The stream is null. </exception>
		// Token: 0x06000D48 RID: 3400 RVA: 0x0003F844 File Offset: 0x0003DA44
		public TextWriterTraceListener(Stream stream)
			: this(stream, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The stream is null. </exception>
		// Token: 0x06000D49 RID: 3401 RVA: 0x0003F852 File Offset: 0x0003DA52
		public TextWriterTraceListener(Stream stream, string name)
			: base(name)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.writer = new StreamWriter(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The writer is null. </exception>
		// Token: 0x06000D4A RID: 3402 RVA: 0x0003F875 File Offset: 0x0003DA75
		public TextWriterTraceListener(TextWriter writer)
			: this(writer, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The writer is null. </exception>
		// Token: 0x06000D4B RID: 3403 RVA: 0x0003F883 File Offset: 0x0003DA83
		public TextWriterTraceListener(TextWriter writer, string name)
			: base(name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <exception cref="T:System.ArgumentNullException">The file is null. </exception>
		// Token: 0x06000D4C RID: 3404 RVA: 0x0003F8A1 File Offset: 0x0003DAA1
		public TextWriterTraceListener(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The stream is null. </exception>
		// Token: 0x06000D4D RID: 3405 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
		public TextWriterTraceListener(string fileName, string name)
			: base(name)
		{
			this.fileName = fileName;
		}

		/// <summary>Gets or sets the text writer that receives the tracing or debugging output.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the writer that receives the tracing or debugging output.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0003F8C0 File Offset: 0x0003DAC0
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0003F8CF File Offset: 0x0003DACF
		public TextWriter Writer
		{
			get
			{
				this.EnsureWriter();
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> so that it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D50 RID: 3408 RVA: 0x0003F8D8 File Offset: 0x0003DAD8
		public override void Close()
		{
			if (this.writer != null)
			{
				try
				{
					this.writer.Close();
				}
				catch (ObjectDisposedException)
				{
				}
			}
			this.writer = null;
		}

		/// <summary>Disposes this <see cref="T:System.Diagnostics.TextWriterTraceListener" /> object.</summary>
		/// <param name="disposing">true to release managed resources; if false, <see cref="M:System.Diagnostics.TextWriterTraceListener.Dispose(System.Boolean)" /> has no effect.</param>
		// Token: 0x06000D51 RID: 3409 RVA: 0x0003F914 File Offset: 0x0003DB14
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				else
				{
					if (this.writer != null)
					{
						try
						{
							this.writer.Close();
						}
						catch (ObjectDisposedException)
						{
						}
					}
					this.writer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Flushes the output buffer for the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D52 RID: 3410 RVA: 0x0003F974 File Offset: 0x0003DB74
		public override void Flush()
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			try
			{
				this.writer.Flush();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D53 RID: 3411 RVA: 0x0003F9AC File Offset: 0x0003DBAC
		public override void Write(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.Write(message);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> followed by a line terminator. The default line terminator is a carriage return followed by a line feed (\r\n).</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D54 RID: 3412 RVA: 0x0003F9F4 File Offset: 0x0003DBF4
		public override void WriteLine(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.WriteLine(message);
				base.NeedIndent = true;
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003FA44 File Offset: 0x0003DC44
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003FA68 File Offset: 0x0003DC68
		internal bool EnsureWriter()
		{
			bool flag = true;
			if (this.writer == null)
			{
				flag = false;
				if (this.fileName == null)
				{
					return flag;
				}
				Encoding encodingWithFallback = TextWriterTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
				string text = Path.GetFullPath(this.fileName);
				string directoryName = Path.GetDirectoryName(text);
				string text2 = Path.GetFileName(text);
				for (int i = 0; i < 2; i++)
				{
					try
					{
						this.writer = new StreamWriter(text, true, encodingWithFallback, 4096);
						flag = true;
						break;
					}
					catch (IOException)
					{
						text2 = Guid.NewGuid().ToString() + text2;
						text = Path.Combine(directoryName, text2);
					}
					catch (UnauthorizedAccessException)
					{
						break;
					}
					catch (Exception)
					{
						break;
					}
				}
				if (!flag)
				{
					this.fileName = null;
				}
			}
			return flag;
		}

		// Token: 0x0400104D RID: 4173
		internal TextWriter writer;

		// Token: 0x0400104E RID: 4174
		private string fileName;
	}
}
