using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help debug your code. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AC RID: 428
	public static class Debug
	{
		/// <summary>Gets the collection of listeners that is monitoring the debug output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> representing a collection of type <see cref="T:System.Diagnostics.TraceListener" /> that monitors the debug output.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0003D4AE File Offset: 0x0003B6AE
		public static TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Diagnostics.Debug.Flush" /> should be called on the <see cref="P:System.Diagnostics.Debug.Listeners" /> after every write.</summary>
		/// <returns>true if <see cref="M:System.Diagnostics.Debug.Flush" /> is called on the <see cref="P:System.Diagnostics.Debug.Listeners" /> after every write; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0003D4B5 File Offset: 0x0003B6B5
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0003D4BC File Offset: 0x0003B6BC
		public static bool AutoFlush
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.AutoFlush;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.AutoFlush = value;
			}
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0003D4C4 File Offset: 0x0003B6C4
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0003D4CB File Offset: 0x0003B6CB
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.IndentLevel;
			}
			set
			{
				TraceInternal.IndentLevel = value;
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0003D4D3 File Offset: 0x0003B6D3
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0003D4DA File Offset: 0x0003B6DA
		public static int IndentSize
		{
			get
			{
				return TraceInternal.IndentSize;
			}
			set
			{
				TraceInternal.IndentSize = value;
			}
		}

		/// <summary>Flushes the output buffer and causes buffered data to write to the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C83 RID: 3203 RVA: 0x0003D4E2 File Offset: 0x0003B6E2
		[Conditional("DEBUG")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		/// <summary>Flushes the output buffer and then calls the Close method on each of the <see cref="P:System.Diagnostics.Debug.Listeners" />.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C84 RID: 3204 RVA: 0x0003D4E9 File Offset: 0x0003B6E9
		[Conditional("DEBUG")]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Close()
		{
			TraceInternal.Close();
		}

		/// <summary>Checks for a condition; if the condition is false, displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, a failure message is not sent and the message box is not displayed.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C85 RID: 3205 RVA: 0x0003D4F0 File Offset: 0x0003B6F0
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		/// <summary>Checks for a condition; if the condition is false, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the specified message is not sent and the message box is not displayed.  </param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C86 RID: 3206 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		/// <summary>Checks for a condition; if the condition is false, outputs two specified messages and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the specified messages are not sent and the message box is not displayed.  </param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <param name="detailMessage">The detailed message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C87 RID: 3207 RVA: 0x0003D501 File Offset: 0x0003B701
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		/// <summary>Checks for a condition; if the condition is false, outputs two messages (simple and formatted) and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the specified messages are not sent and the message box is not displayed.  </param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <param name="detailMessageFormat">The composite format string (see Remarks) to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. This message contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x06000C88 RID: 3208 RVA: 0x0003D50B File Offset: 0x0003B70B
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
			TraceInternal.Assert(condition, message, string.Format(CultureInfo.InvariantCulture, detailMessageFormat, args));
		}

		/// <summary>Emits the specified error message.</summary>
		/// <param name="message">A message to emit. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C89 RID: 3209 RVA: 0x0003D520 File Offset: 0x0003B720
		[Conditional("DEBUG")]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		/// <summary>Emits an error message and a detailed error message.</summary>
		/// <param name="message">A message to emit. </param>
		/// <param name="detailMessage">A detailed message to emit. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C8A RID: 3210 RVA: 0x0003D528 File Offset: 0x0003B728
		[Conditional("DEBUG")]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		/// <summary>Writes a message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">The message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C8B RID: 3211 RVA: 0x0003D531 File Offset: 0x0003B731
		[Conditional("DEBUG")]
		public static void Print(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes a formatted string followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="format">A composite format string (see Remarks) that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C8C RID: 3212 RVA: 0x0003D539 File Offset: 0x0003B739
		[Conditional("DEBUG")]
		public static void Print(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C8D RID: 3213 RVA: 0x0003D54C File Offset: 0x0003B74C
		[Conditional("DEBUG")]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C8E RID: 3214 RVA: 0x0003D554 File Offset: 0x0003B754
		[Conditional("DEBUG")]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C8F RID: 3215 RVA: 0x0003D55C File Offset: 0x0003B75C
		[Conditional("DEBUG")]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C90 RID: 3216 RVA: 0x0003D565 File Offset: 0x0003B765
		[Conditional("DEBUG")]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		/// <summary>Writes a message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C91 RID: 3217 RVA: 0x0003D531 File Offset: 0x0003B731
		[Conditional("DEBUG")]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C92 RID: 3218 RVA: 0x0003D56E File Offset: 0x0003B76E
		[Conditional("DEBUG")]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C93 RID: 3219 RVA: 0x0003D576 File Offset: 0x0003B776
		[Conditional("DEBUG")]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C94 RID: 3220 RVA: 0x0003D57F File Offset: 0x0003B77F
		[Conditional("DEBUG")]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		/// <summary>Writes a formatted message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="format">A composite format string (see Remarks) that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array that contains zero or more objects to format. </param>
		// Token: 0x06000C95 RID: 3221 RVA: 0x0003D539 File Offset: 0x0003B739
		[Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the message is written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C96 RID: 3222 RVA: 0x0003D588 File Offset: 0x0003B788
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the value is written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C97 RID: 3223 RVA: 0x0003D591 File Offset: 0x0003B791
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the category name and message are written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C98 RID: 3224 RVA: 0x0003D59A File Offset: 0x0003B79A
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the category name and value are written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C99 RID: 3225 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the message is written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9A RID: 3226 RVA: 0x0003D5AE File Offset: 0x0003B7AE
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the value is written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9B RID: 3227 RVA: 0x0003D5B7 File Offset: 0x0003B7B7
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9C RID: 3228 RVA: 0x0003D5C0 File Offset: 0x0003B7C0
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the category name and value are written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9D RID: 3229 RVA: 0x0003D5CA File Offset: 0x0003B7CA
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		/// <summary>Increases the current <see cref="P:System.Diagnostics.Debug.IndentLevel" /> by one.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9E RID: 3230 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
		[Conditional("DEBUG")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		/// <summary>Decreases the current <see cref="P:System.Diagnostics.Debug.IndentLevel" /> by one.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C9F RID: 3231 RVA: 0x0003D5DB File Offset: 0x0003B7DB
		[Conditional("DEBUG")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}
	}
}
