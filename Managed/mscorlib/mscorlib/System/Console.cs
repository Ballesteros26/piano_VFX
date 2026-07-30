using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System
{
	/// <summary>Represents the standard input, output, and error streams for console applications. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000208 RID: 520
	public static class Console
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x0005D6FC File Offset: 0x0005B8FC
		static Console()
		{
			if (Environment.IsRunningOnWindows)
			{
				try
				{
					Console.inputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetInputCodePage());
					Console.outputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetOutputCodePage());
					goto IL_008E;
				}
				catch
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
					goto IL_008E;
				}
			}
			int num = 0;
			EncodingHelper.InternalCodePage(ref num);
			if (num != -1 && ((num & 268435455) == 3 || (num & 268435456) != 0))
			{
				Console.inputEncoding = (Console.outputEncoding = EncodingHelper.UTF8Unmarked);
			}
			else
			{
				Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
			}
			IL_008E:
			Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0005D7B8 File Offset: 0x0005B9B8
		private static void SetupStreams(Encoding inputEncoding, Encoding outputEncoding)
		{
			if (!Environment.IsRunningOnWindows && ConsoleDriver.IsConsole)
			{
				Console.stdin = new CStreamReader(Console.OpenStandardInput(0), inputEncoding);
				Console.stdout = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardOutput(0), outputEncoding, true)
				{
					AutoFlush = true
				});
				Console.stderr = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardError(0), outputEncoding, true)
				{
					AutoFlush = true
				});
			}
			else
			{
				Console.stdin = TextReader.Synchronized(new UnexceptionalStreamReader(Console.OpenStandardInput(0), inputEncoding));
				Console.stdout = TextWriter.Synchronized(new UnexceptionalStreamWriter(Console.OpenStandardOutput(0), outputEncoding)
				{
					AutoFlush = true
				});
				Console.stderr = TextWriter.Synchronized(new UnexceptionalStreamWriter(Console.OpenStandardError(0), outputEncoding)
				{
					AutoFlush = true
				});
			}
			GC.SuppressFinalize(Console.stdout);
			GC.SuppressFinalize(Console.stderr);
			GC.SuppressFinalize(Console.stdin);
		}

		/// <summary>Gets the standard error output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard error output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0005D890 File Offset: 0x0005BA90
		public static TextWriter Error
		{
			get
			{
				return Console.stderr;
			}
		}

		/// <summary>Gets the standard output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005D897 File Offset: 0x0005BA97
		public static TextWriter Out
		{
			get
			{
				return Console.stdout;
			}
		}

		/// <summary>Gets the standard input stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextReader" /> that represents the standard input stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0005D89E File Offset: 0x0005BA9E
		public static TextReader In
		{
			get
			{
				return Console.stdin;
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0005D8A8 File Offset: 0x0005BAA8
		private static Stream Open(IntPtr handle, FileAccess access, int bufferSize)
		{
			Stream stream;
			try
			{
				stream = new FileStream(handle, access, false, bufferSize, false, true);
			}
			catch (IOException)
			{
				stream = Stream.Null;
			}
			return stream;
		}

		/// <summary>Acquires the standard error stream.</summary>
		/// <returns>The standard error stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600183B RID: 6203 RVA: 0x0005D8E0 File Offset: 0x0005BAE0
		public static Stream OpenStandardError()
		{
			return Console.OpenStandardError(0);
		}

		/// <summary>Acquires the standard error stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard error stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600183C RID: 6204 RVA: 0x0005D8E8 File Offset: 0x0005BAE8
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static Stream OpenStandardError(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleError, FileAccess.Write, bufferSize);
		}

		/// <summary>Acquires the standard input stream.</summary>
		/// <returns>The standard input stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600183D RID: 6205 RVA: 0x0005D8F6 File Offset: 0x0005BAF6
		public static Stream OpenStandardInput()
		{
			return Console.OpenStandardInput(0);
		}

		/// <summary>Acquires the standard input stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard input stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600183E RID: 6206 RVA: 0x0005D8FE File Offset: 0x0005BAFE
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static Stream OpenStandardInput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleInput, FileAccess.Read, bufferSize);
		}

		/// <summary>Acquires the standard output stream.</summary>
		/// <returns>The standard output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600183F RID: 6207 RVA: 0x0005D90C File Offset: 0x0005BB0C
		public static Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput(0);
		}

		/// <summary>Acquires the standard output stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard output stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001840 RID: 6208 RVA: 0x0005D914 File Offset: 0x0005BB14
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static Stream OpenStandardOutput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleOutput, FileAccess.Write, bufferSize);
		}

		/// <summary>Sets the <see cref="P:System.Console.Error" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newError">A stream that is the new standard error output. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newError" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001841 RID: 6209 RVA: 0x0005D922 File Offset: 0x0005BB22
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetError(TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException("newError");
			}
			Console.stderr = newError;
		}

		/// <summary>Sets the <see cref="P:System.Console.In" /> property to the specified <see cref="T:System.IO.TextReader" /> object.</summary>
		/// <param name="newIn">A stream that is the new standard input. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newIn" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001842 RID: 6210 RVA: 0x0005D938 File Offset: 0x0005BB38
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetIn(TextReader newIn)
		{
			if (newIn == null)
			{
				throw new ArgumentNullException("newIn");
			}
			Console.stdin = newIn;
		}

		/// <summary>Sets the <see cref="P:System.Console.Out" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newOut">A stream that is the new standard output. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newOut" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001843 RID: 6211 RVA: 0x0005D94E File Offset: 0x0005BB4E
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			Console.stdout = newOut;
		}

		/// <summary>Writes the text representation of the specified Boolean value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001844 RID: 6212 RVA: 0x0005D964 File Offset: 0x0005BB64
		public static void Write(bool value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the specified Unicode character value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001845 RID: 6213 RVA: 0x0005D971 File Offset: 0x0005BB71
		public static void Write(char value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the specified array of Unicode characters to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001846 RID: 6214 RVA: 0x0005D97E File Offset: 0x0005BB7E
		public static void Write(char[] buffer)
		{
			Console.stdout.Write(buffer);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001847 RID: 6215 RVA: 0x0005D98B File Offset: 0x0005BB8B
		public static void Write(decimal value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001848 RID: 6216 RVA: 0x0005D998 File Offset: 0x0005BB98
		public static void Write(double value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001849 RID: 6217 RVA: 0x0005D9A5 File Offset: 0x0005BBA5
		public static void Write(int value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600184A RID: 6218 RVA: 0x0005D9B2 File Offset: 0x0005BBB2
		public static void Write(long value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream.</summary>
		/// <param name="value">The value to write, or null. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600184B RID: 6219 RVA: 0x0005D9BF File Offset: 0x0005BBBF
		public static void Write(object value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600184C RID: 6220 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		public static void Write(float value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the specified string value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600184D RID: 6221 RVA: 0x0005D9D9 File Offset: 0x0005BBD9
		public static void Write(string value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600184E RID: 6222 RVA: 0x0005D9E6 File Offset: 0x0005BBE6
		[CLSCompliant(false)]
		public static void Write(uint value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600184F RID: 6223 RVA: 0x0005D9F3 File Offset: 0x0005BBF3
		[CLSCompliant(false)]
		public static void Write(ulong value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001850 RID: 6224 RVA: 0x0005DA00 File Offset: 0x0005BC00
		public static void Write(string format, object arg0)
		{
			Console.stdout.Write(format, arg0);
		}

		/// <summary>Writes the text representation of the specified array of objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001851 RID: 6225 RVA: 0x0005DA0E File Offset: 0x0005BC0E
		public static void Write(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.stdout.Write(format);
				return;
			}
			Console.stdout.Write(format, arg);
		}

		/// <summary>Writes the specified subarray of Unicode characters to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters. </param>
		/// <param name="index">The starting position in <paramref name="buffer" />. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001852 RID: 6226 RVA: 0x0005DA2B File Offset: 0x0005BC2B
		public static void Write(char[] buffer, int index, int count)
		{
			Console.stdout.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001853 RID: 6227 RVA: 0x0005DA3A File Offset: 0x0005BC3A
		public static void Write(string format, object arg0, object arg1)
		{
			Console.stdout.Write(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001854 RID: 6228 RVA: 0x0005DA49 File Offset: 0x0005BC49
		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.Write(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001855 RID: 6229 RVA: 0x0005DA5C File Offset: 0x0005BC5C
		[CLSCompliant(false)]
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.Write(string.Format(format, array));
		}

		/// <summary>Writes the current line terminator to the standard output stream.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001856 RID: 6230 RVA: 0x0005DAC6 File Offset: 0x0005BCC6
		public static void WriteLine()
		{
			Console.stdout.WriteLine();
		}

		/// <summary>Writes the text representation of the specified Boolean value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001857 RID: 6231 RVA: 0x0005DAD2 File Offset: 0x0005BCD2
		public static void WriteLine(bool value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified Unicode character, followed by the current line terminator, value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001858 RID: 6232 RVA: 0x0005DADF File Offset: 0x0005BCDF
		public static void WriteLine(char value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified array of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001859 RID: 6233 RVA: 0x0005DAEC File Offset: 0x0005BCEC
		public static void WriteLine(char[] buffer)
		{
			Console.stdout.WriteLine(buffer);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185A RID: 6234 RVA: 0x0005DAF9 File Offset: 0x0005BCF9
		public static void WriteLine(decimal value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185B RID: 6235 RVA: 0x0005DB06 File Offset: 0x0005BD06
		public static void WriteLine(double value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185C RID: 6236 RVA: 0x0005DB13 File Offset: 0x0005BD13
		public static void WriteLine(int value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185D RID: 6237 RVA: 0x0005DB20 File Offset: 0x0005BD20
		public static void WriteLine(long value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185E RID: 6238 RVA: 0x0005DB2D File Offset: 0x0005BD2D
		public static void WriteLine(object value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185F RID: 6239 RVA: 0x0005DB3A File Offset: 0x0005BD3A
		public static void WriteLine(float value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001860 RID: 6240 RVA: 0x0005DB47 File Offset: 0x0005BD47
		public static void WriteLine(string value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001861 RID: 6241 RVA: 0x0005DB54 File Offset: 0x0005BD54
		[CLSCompliant(false)]
		public static void WriteLine(uint value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001862 RID: 6242 RVA: 0x0005DB61 File Offset: 0x0005BD61
		[CLSCompliant(false)]
		public static void WriteLine(ulong value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">An object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001863 RID: 6243 RVA: 0x0005DB6E File Offset: 0x0005BD6E
		public static void WriteLine(string format, object arg0)
		{
			Console.stdout.WriteLine(format, arg0);
		}

		/// <summary>Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001864 RID: 6244 RVA: 0x0005DB7C File Offset: 0x0005BD7C
		public static void WriteLine(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.stdout.WriteLine(format);
				return;
			}
			Console.stdout.WriteLine(format, arg);
		}

		/// <summary>Writes the specified subarray of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters. </param>
		/// <param name="index">The starting position in <paramref name="buffer" />. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001865 RID: 6245 RVA: 0x0005DB99 File Offset: 0x0005BD99
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.stdout.WriteLine(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001866 RID: 6246 RVA: 0x0005DBA8 File Offset: 0x0005BDA8
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.stdout.WriteLine(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001867 RID: 6247 RVA: 0x0005DBB7 File Offset: 0x0005BDB7
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.WriteLine(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001868 RID: 6248 RVA: 0x0005DBC8 File Offset: 0x0005BDC8
		[CLSCompliant(false)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.WriteLine(string.Format(format, array));
		}

		/// <summary>Reads the next character from the standard input stream.</summary>
		/// <returns>The next character from the input stream, or negative one (-1) if there are currently no more characters to be read.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001869 RID: 6249 RVA: 0x0005DC32 File Offset: 0x0005BE32
		public static int Read()
		{
			if (Console.stdin is CStreamReader && ConsoleDriver.IsConsole)
			{
				return ConsoleDriver.Read();
			}
			return Console.stdin.Read();
		}

		/// <summary>Reads the next line of characters from the standard input stream.</summary>
		/// <returns>The next line of characters from the input stream, or null if no more lines are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600186A RID: 6250 RVA: 0x0005DC57 File Offset: 0x0005BE57
		public static string ReadLine()
		{
			if (Console.stdin is CStreamReader && ConsoleDriver.IsConsole)
			{
				return ConsoleDriver.ReadLine();
			}
			return Console.stdin.ReadLine();
		}

		/// <summary>Gets or sets the encoding the console uses to read input. </summary>
		/// <returns>The encoding used to read console input.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0005DC7C File Offset: 0x0005BE7C
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x0005DC83 File Offset: 0x0005BE83
		public static Encoding InputEncoding
		{
			get
			{
				return Console.inputEncoding;
			}
			set
			{
				Console.inputEncoding = value;
				Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
			}
		}

		/// <summary>Gets or sets the encoding the console uses to write output. </summary>
		/// <returns>The encoding used to write console output.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0005DC9A File Offset: 0x0005BE9A
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0005DCA1 File Offset: 0x0005BEA1
		public static Encoding OutputEncoding
		{
			get
			{
				return Console.outputEncoding;
			}
			set
			{
				Console.outputEncoding = value;
				Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
			}
		}

		/// <summary>Gets or sets the background color of the console.</summary>
		/// <returns>A value that specifies the background color of the console; that is, the color that appears behind each character. The default is black.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x0005DCBF File Offset: 0x0005BEBF
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.BackgroundColor;
			}
			set
			{
				ConsoleDriver.BackgroundColor = value;
			}
		}

		/// <summary>Gets or sets the height of the buffer area.</summary>
		/// <returns>The current height, in rows, of the buffer area.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.-or- The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or- The value in a set operation is less than <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0005DCC7 File Offset: 0x0005BEC7
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x0005DCCE File Offset: 0x0005BECE
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.BufferHeight;
			}
			[MonoLimitation("Implemented only on Windows")]
			set
			{
				ConsoleDriver.BufferHeight = value;
			}
		}

		/// <summary>Gets or sets the width of the buffer area.</summary>
		/// <returns>The current width, in columns, of the buffer area.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.-or- The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or- The value in a set operation is less than <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005DCD6 File Offset: 0x0005BED6
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x0005DCDD File Offset: 0x0005BEDD
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.BufferWidth;
			}
			[MonoLimitation("Implemented only on Windows")]
			set
			{
				ConsoleDriver.BufferWidth = value;
			}
		}

		/// <summary>Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or turned off.</summary>
		/// <returns>true if CAPS LOCK is turned on; false if CAPS LOCK is turned off.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0005DCE5 File Offset: 0x0005BEE5
		[MonoLimitation("Implemented only on Windows")]
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.CapsLock;
			}
		}

		/// <summary>Gets or sets the column position of the cursor within the buffer area.</summary>
		/// <returns>The current position, in columns, of the cursor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0005DCEC File Offset: 0x0005BEEC
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0005DCF3 File Offset: 0x0005BEF3
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.CursorLeft;
			}
			set
			{
				ConsoleDriver.CursorLeft = value;
			}
		}

		/// <summary>Gets or sets the row position of the cursor within the buffer area.</summary>
		/// <returns>The current position, in rows, of the cursor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0005DCFB File Offset: 0x0005BEFB
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x0005DD02 File Offset: 0x0005BF02
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.CursorTop;
			}
			set
			{
				ConsoleDriver.CursorTop = value;
			}
		}

		/// <summary>Gets or sets the height of the cursor within a character cell.</summary>
		/// <returns>The size of the cursor expressed as a percentage of the height of a character cell. The property value ranges from 1 to 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 1 or greater than 100. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0005DD0A File Offset: 0x0005BF0A
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x0005DD11 File Offset: 0x0005BF11
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.CursorSize;
			}
			set
			{
				ConsoleDriver.CursorSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cursor is visible.</summary>
		/// <returns>true if the cursor is visible; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0005DD19 File Offset: 0x0005BF19
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x0005DD20 File Offset: 0x0005BF20
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.CursorVisible;
			}
			set
			{
				ConsoleDriver.CursorVisible = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the console.</summary>
		/// <returns>A <see cref="T:System.ConsoleColor" /> that specifies the foreground color of the console; that is, the color of each character that is displayed. The default is gray.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0005DD28 File Offset: 0x0005BF28
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x0005DD2F File Offset: 0x0005BF2F
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.ForegroundColor;
			}
			set
			{
				ConsoleDriver.ForegroundColor = value;
			}
		}

		/// <summary>Gets a value indicating whether a key press is available in the input stream.</summary>
		/// <returns>true if a key press is available; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.InvalidOperationException">Standard input is redirected to a file instead of the keyboard. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0005DD37 File Offset: 0x0005BF37
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.KeyAvailable;
			}
		}

		/// <summary>Gets the largest possible number of console window rows, based on the current font and screen resolution.</summary>
		/// <returns>The height of the largest possible console window measured in rows.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005DD3E File Offset: 0x0005BF3E
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.LargestWindowHeight;
			}
		}

		/// <summary>Gets the largest possible number of console window columns, based on the current font and screen resolution.</summary>
		/// <returns>The width of the largest possible console window measured in columns.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005DD45 File Offset: 0x0005BF45
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.LargestWindowWidth;
			}
		}

		/// <summary>Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or turned off.</summary>
		/// <returns>true if NUM LOCK is turned on; false if NUM LOCK is turned off.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0005DD4C File Offset: 0x0005BF4C
		[MonoLimitation("Only works on windows")]
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.NumberLock;
			}
		}

		/// <summary>Gets or sets the title to display in the console title bar.</summary>
		/// <returns>The string to be displayed in the title bar of the console. The maximum length of the title string is 24500 characters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a get operation, the retrieved title is longer than 24500 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the specified title is longer than 24500 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the specified title is null. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0005DD53 File Offset: 0x0005BF53
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x0005DD5A File Offset: 0x0005BF5A
		public static string Title
		{
			get
			{
				return ConsoleDriver.Title;
			}
			set
			{
				ConsoleDriver.Title = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the combination of the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and <see cref="F:System.ConsoleKey.C" /> console key (Ctrl+C) is treated as ordinary input or as an interruption that is handled by the operating system.</summary>
		/// <returns>true if Ctrl+C is treated as ordinary input; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">Unable to get or set the input mode of the console input buffer. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0005DD62 File Offset: 0x0005BF62
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0005DD69 File Offset: 0x0005BF69
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.TreatControlCAsInput = value;
			}
		}

		/// <summary>Gets or sets the height of the console window area.</summary>
		/// <returns>The height of the console window measured in rows.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is less than or equal to 0.-or-The value of the <see cref="P:System.Console.WindowHeight" /> property plus the value of the <see cref="P:System.Console.WindowTop" /> property is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or-The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0005DD71 File Offset: 0x0005BF71
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x0005DD78 File Offset: 0x0005BF78
		[MonoLimitation("Only works on windows")]
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.WindowHeight;
			}
			set
			{
				ConsoleDriver.WindowHeight = value;
			}
		}

		/// <summary>Gets or sets the leftmost position of the console window area relative to the screen buffer.</summary>
		/// <returns>The leftmost console window position measured in columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the value to be assigned is less than zero.-or-As a result of the assignment, <see cref="P:System.Console.WindowLeft" /> plus <see cref="P:System.Console.WindowWidth" /> would exceed <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0005DD80 File Offset: 0x0005BF80
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0005DD87 File Offset: 0x0005BF87
		[MonoLimitation("Only works on windows")]
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.WindowLeft;
			}
			set
			{
				ConsoleDriver.WindowLeft = value;
			}
		}

		/// <summary>Gets or sets the top position of the console window area relative to the screen buffer.</summary>
		/// <returns>The uppermost console window position measured in rows.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the value to be assigned is less than zero.-or-As a result of the assignment, <see cref="P:System.Console.WindowTop" /> plus <see cref="P:System.Console.WindowHeight" /> would exceed <see cref="P:System.Console.BufferHeight" />.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0005DD8F File Offset: 0x0005BF8F
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x0005DD96 File Offset: 0x0005BF96
		[MonoLimitation("Only works on windows")]
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.WindowTop;
			}
			set
			{
				ConsoleDriver.WindowTop = value;
			}
		}

		/// <summary>Gets or sets the width of the console window.</summary>
		/// <returns>The width of the console window measured in columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is less than or equal to 0.-or-The value of the <see cref="P:System.Console.WindowHeight" /> property plus the value of the <see cref="P:System.Console.WindowTop" /> property is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or-The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0005DD9E File Offset: 0x0005BF9E
		// (set) Token: 0x0600188F RID: 6287 RVA: 0x0005DDA5 File Offset: 0x0005BFA5
		[MonoLimitation("Only works on windows")]
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.WindowWidth;
			}
			set
			{
				ConsoleDriver.WindowWidth = value;
			}
		}

		/// <summary>Gets a value that indicates whether the error output stream has been redirected from the standard error stream.</summary>
		/// <returns>true if error output is redirected; otherwise, false.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0005DDAD File Offset: 0x0005BFAD
		public static bool IsErrorRedirected
		{
			get
			{
				return ConsoleDriver.IsErrorRedirected;
			}
		}

		/// <summary>Gets a value that indicates whether output has been redirected from the standard output stream.</summary>
		/// <returns>true if output is redirected; otherwise, false.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0005DDB4 File Offset: 0x0005BFB4
		public static bool IsOutputRedirected
		{
			get
			{
				return ConsoleDriver.IsOutputRedirected;
			}
		}

		/// <summary>Gets a value that indicates whether input has been redirected from the standard input stream.</summary>
		/// <returns>true if input is redirected; otherwise, false.</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0005DDBB File Offset: 0x0005BFBB
		public static bool IsInputRedirected
		{
			get
			{
				return ConsoleDriver.IsInputRedirected;
			}
		}

		/// <summary>Plays the sound of a beep through the console speaker.</summary>
		/// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to a user interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001893 RID: 6291 RVA: 0x0005DDC2 File Offset: 0x0005BFC2
		public static void Beep()
		{
			Console.Beep(1000, 500);
		}

		/// <summary>Plays the sound of a beep of a specified frequency and duration through the console speaker.</summary>
		/// <param name="frequency">The frequency of the beep, ranging from 37 to 32767 hertz.</param>
		/// <param name="duration">The duration of the beep measured in milliseconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="frequency" /> is less than 37 or more than 32767 hertz.-or-<paramref name="duration" /> is less than or equal to zero.</exception>
		/// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001894 RID: 6292 RVA: 0x0005DDD3 File Offset: 0x0005BFD3
		public static void Beep(int frequency, int duration)
		{
			if (frequency < 37 || frequency > 32767)
			{
				throw new ArgumentOutOfRangeException("frequency");
			}
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException("duration");
			}
			ConsoleDriver.Beep(frequency, duration);
		}

		/// <summary>Clears the console buffer and corresponding console window of display information.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001895 RID: 6293 RVA: 0x0005DE03 File Offset: 0x0005C003
		public static void Clear()
		{
			ConsoleDriver.Clear();
		}

		/// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
		/// <param name="sourceLeft">The leftmost column of the source area. </param>
		/// <param name="sourceTop">The topmost row of the source area. </param>
		/// <param name="sourceWidth">The number of columns in the source area. </param>
		/// <param name="sourceHeight">The number of rows in the source area. </param>
		/// <param name="targetLeft">The leftmost column of the destination area. </param>
		/// <param name="targetTop">The topmost row of the destination area. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.-or- <paramref name="sourceLeft" /> or <paramref name="targetLeft" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="sourceTop" /> or <paramref name="targetTop" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06001896 RID: 6294 RVA: 0x0005DE0A File Offset: 0x0005C00A
		[MonoLimitation("Implemented only on Windows")]
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
		}

		/// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
		/// <param name="sourceLeft">The leftmost column of the source area. </param>
		/// <param name="sourceTop">The topmost row of the source area. </param>
		/// <param name="sourceWidth">The number of columns in the source area. </param>
		/// <param name="sourceHeight">The number of rows in the source area. </param>
		/// <param name="targetLeft">The leftmost column of the destination area. </param>
		/// <param name="targetTop">The topmost row of the destination area. </param>
		/// <param name="sourceChar">The character used to fill the source area. </param>
		/// <param name="sourceForeColor">The foreground color used to fill the source area. </param>
		/// <param name="sourceBackColor">The background color used to fill the source area. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.-or- <paramref name="sourceLeft" /> or <paramref name="targetLeft" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="sourceTop" /> or <paramref name="targetTop" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.ArgumentException">One or both of the color parameters is not a member of the <see cref="T:System.ConsoleColor" /> enumeration. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06001897 RID: 6295 RVA: 0x0005DE1C File Offset: 0x0005C01C
		[MonoLimitation("Implemented only on Windows")]
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.</summary>
		/// <returns>A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001898 RID: 6296 RVA: 0x0005DE3C File Offset: 0x0005C03C
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(false);
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is optionally displayed in the console window.</summary>
		/// <returns>A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.</returns>
		/// <param name="intercept">Determines whether to display the pressed key in the console window. true to not display the pressed key; otherwise, false. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001899 RID: 6297 RVA: 0x0005DE44 File Offset: 0x0005C044
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.ReadKey(intercept);
		}

		/// <summary>Sets the foreground and background console colors to their defaults.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x0600189A RID: 6298 RVA: 0x0005DE4C File Offset: 0x0005C04C
		public static void ResetColor()
		{
			ConsoleDriver.ResetColor();
		}

		/// <summary>Sets the height and width of the screen buffer area to the specified values.</summary>
		/// <param name="width">The width of the buffer area measured in columns. </param>
		/// <param name="height">The height of the buffer area measured in rows. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="height" /> or <paramref name="width" /> is less than or equal to zero.-or- <paramref name="height" /> or <paramref name="width" /> is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or- <paramref name="width" /> is less than <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />.-or- <paramref name="height" /> is less than <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x0600189B RID: 6299 RVA: 0x0005DE53 File Offset: 0x0005C053
		[MonoLimitation("Only works on windows")]
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.SetBufferSize(width, height);
		}

		/// <summary>Sets the position of the cursor.</summary>
		/// <param name="left">The column position of the cursor. Columns are numbered from left to right starting at 0. </param>
		/// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="left" /> or <paramref name="top" /> is less than zero.-or- <paramref name="left" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="top" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x0600189C RID: 6300 RVA: 0x0005DE5C File Offset: 0x0005C05C
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.SetCursorPosition(left, top);
		}

		/// <summary>Sets the position of the console window relative to the screen buffer.</summary>
		/// <param name="left">The column position of the upper left  corner of the console window. </param>
		/// <param name="top">The row position of the upper left corner of the console window. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="left" /> or <paramref name="top" /> is less than zero.-or- <paramref name="left" /> + <see cref="P:System.Console.WindowWidth" /> is greater than <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="top" /> + <see cref="P:System.Console.WindowHeight" /> is greater than <see cref="P:System.Console.BufferHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x0600189D RID: 6301 RVA: 0x0005DE65 File Offset: 0x0005C065
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.SetWindowPosition(left, top);
		}

		/// <summary>Sets the height and width of the console window to the specified values.</summary>
		/// <param name="width">The width of the console window measured in columns. </param>
		/// <param name="height">The height of the console window measured in rows. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="width" /> or <paramref name="height" /> is less than or equal to zero.-or- <paramref name="width" /> plus <see cref="P:System.Console.WindowLeft" /> or <paramref name="height" /> plus <see cref="P:System.Console.WindowTop" /> is greater than or equal to <see cref="F:System.Int16.MaxValue" />. -or-<paramref name="width" /> or <paramref name="height" /> is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x0600189E RID: 6302 RVA: 0x0005DE6E File Offset: 0x0005C06E
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.SetWindowSize(width, height);
		}

		/// <summary>Occurs when the <see cref="F:System.ConsoleModifiers.Control" /> modifier key (Ctrl) and either the <see cref="F:System.ConsoleKey.C" /> console key (C) or the Break key are pressed simultaneously (Ctrl+C or Ctrl+Break).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600189F RID: 6303 RVA: 0x0005DE77 File Offset: 0x0005C077
		// (remove) Token: 0x060018A0 RID: 6304 RVA: 0x0005DEAD File Offset: 0x0005C0AD
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			add
			{
				if (!ConsoleDriver.Initialized)
				{
					ConsoleDriver.Init();
				}
				Console.cancel_event = (ConsoleCancelEventHandler)Delegate.Combine(Console.cancel_event, value);
				if (Environment.IsRunningOnWindows && !Console.WindowsConsole.ctrlHandlerAdded)
				{
					Console.WindowsConsole.AddCtrlHandler();
				}
			}
			remove
			{
				if (!ConsoleDriver.Initialized)
				{
					ConsoleDriver.Init();
				}
				Console.cancel_event = (ConsoleCancelEventHandler)Delegate.Remove(Console.cancel_event, value);
				if (Console.cancel_event == null && Environment.IsRunningOnWindows && Console.WindowsConsole.ctrlHandlerAdded)
				{
					Console.WindowsConsole.RemoveCtrlHandler();
				}
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0005DEEC File Offset: 0x0005C0EC
		internal static void DoConsoleCancelEvent()
		{
			bool flag = true;
			if (Console.cancel_event != null)
			{
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs(ConsoleSpecialKey.ControlC);
				foreach (ConsoleCancelEventHandler consoleCancelEventHandler in Console.cancel_event.GetInvocationList())
				{
					try
					{
						consoleCancelEventHandler(null, consoleCancelEventArgs);
					}
					catch
					{
					}
				}
				flag = !consoleCancelEventArgs.Cancel;
			}
			if (flag)
			{
				Environment.Exit(58);
			}
		}

		// Token: 0x04000C7F RID: 3199
		internal static TextWriter stdout;

		// Token: 0x04000C80 RID: 3200
		private static TextWriter stderr;

		// Token: 0x04000C81 RID: 3201
		private static TextReader stdin;

		// Token: 0x04000C82 RID: 3202
		private static Encoding inputEncoding;

		// Token: 0x04000C83 RID: 3203
		private static Encoding outputEncoding;

		// Token: 0x04000C84 RID: 3204
		private static ConsoleCancelEventHandler cancel_event;

		// Token: 0x04000C85 RID: 3205
		private static readonly Console.InternalCancelHandler cancel_handler = new Console.InternalCancelHandler(Console.DoConsoleCancelEvent);

		// Token: 0x02000209 RID: 521
		private class WindowsConsole
		{
			// Token: 0x060018A2 RID: 6306
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleCP();

			// Token: 0x060018A3 RID: 6307
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleOutputCP();

			// Token: 0x060018A4 RID: 6308
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern bool SetConsoleCtrlHandler(Console.WindowsConsole.WindowsCancelHandler handler, bool addHandler);

			// Token: 0x060018A5 RID: 6309 RVA: 0x0005DF60 File Offset: 0x0005C160
			private static bool DoWindowsConsoleCancelEvent(int keyCode)
			{
				if (keyCode == 0)
				{
					Console.DoConsoleCancelEvent();
				}
				return keyCode == 0;
			}

			// Token: 0x060018A6 RID: 6310 RVA: 0x0005DF6E File Offset: 0x0005C16E
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetInputCodePage()
			{
				return Console.WindowsConsole.GetConsoleCP();
			}

			// Token: 0x060018A7 RID: 6311 RVA: 0x0005DF75 File Offset: 0x0005C175
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetOutputCodePage()
			{
				return Console.WindowsConsole.GetConsoleOutputCP();
			}

			// Token: 0x060018A8 RID: 6312 RVA: 0x0005DF7C File Offset: 0x0005C17C
			public static void AddCtrlHandler()
			{
				Console.WindowsConsole.SetConsoleCtrlHandler(Console.WindowsConsole.cancelHandler, true);
				Console.WindowsConsole.ctrlHandlerAdded = true;
			}

			// Token: 0x060018A9 RID: 6313 RVA: 0x0005DF90 File Offset: 0x0005C190
			public static void RemoveCtrlHandler()
			{
				Console.WindowsConsole.SetConsoleCtrlHandler(Console.WindowsConsole.cancelHandler, false);
				Console.WindowsConsole.ctrlHandlerAdded = false;
			}

			// Token: 0x04000C86 RID: 3206
			public static bool ctrlHandlerAdded = false;

			// Token: 0x04000C87 RID: 3207
			private static Console.WindowsConsole.WindowsCancelHandler cancelHandler = new Console.WindowsConsole.WindowsCancelHandler(Console.WindowsConsole.DoWindowsConsoleCancelEvent);

			// Token: 0x0200020A RID: 522
			// (Invoke) Token: 0x060018AD RID: 6317
			private delegate bool WindowsCancelHandler(int keyCode);
		}

		// Token: 0x0200020B RID: 523
		// (Invoke) Token: 0x060018B1 RID: 6321
		private delegate void InternalCancelHandler();
	}
}
