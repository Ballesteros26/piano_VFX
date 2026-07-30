using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help you trace the execution of your code. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C2 RID: 450
	public sealed class Trace
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x000020EB File Offset: 0x000002EB
		private Trace()
		{
		}

		/// <summary>Gets the collection of listeners that is monitoring the trace output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that represents a collection of type <see cref="T:System.Diagnostics.TraceListener" /> monitoring the trace output.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0003D4AE File Offset: 0x0003B6AE
		public static TraceListenerCollection Listeners
		{
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets whether <see cref="M:System.Diagnostics.Trace.Flush" /> should be called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write.</summary>
		/// <returns>true if <see cref="M:System.Diagnostics.Trace.Flush" /> is called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0003D4B5 File Offset: 0x0003B6B5
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0003D4BC File Offset: 0x0003B6BC
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

		/// <summary>Gets or sets a value indicating whether the global lock should be used.  </summary>
		/// <returns>true if the global lock is to be used; otherwise, false. The default is true.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0003FB40 File Offset: 0x0003DD40
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0003FB47 File Offset: 0x0003DD47
		public static bool UseGlobalLock
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.UseGlobalLock;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.UseGlobalLock = value;
			}
		}

		/// <summary>Gets the correlation manager for the thread for this trace.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.CorrelationManager" /> object associated with the thread for this trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003FB4F File Offset: 0x0003DD4F
		public static CorrelationManager CorrelationManager
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (Trace.correlationManager == null)
				{
					Trace.correlationManager = new CorrelationManager();
				}
				return Trace.correlationManager;
			}
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003D4C4 File Offset: 0x0003B6C4
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0003D4CB File Offset: 0x0003B6CB
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
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003D4D3 File Offset: 0x0003B6D3
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0003D4DA File Offset: 0x0003B6DA
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

		/// <summary>Flushes the output buffer, and causes buffered data to be written to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D62 RID: 3426 RVA: 0x0003D4E2 File Offset: 0x0003B6E2
		[Conditional("TRACE")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		/// <summary>Flushes the output buffer, and then closes the <see cref="P:System.Diagnostics.Trace.Listeners" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D63 RID: 3427 RVA: 0x0003D4E9 File Offset: 0x0003B6E9
		[Conditional("TRACE")]
		public static void Close()
		{
			TraceInternal.Close();
		}

		/// <summary>Checks for a condition; if the condition is false, displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, a failure message is not sent and the message box is not displayed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D64 RID: 3428 RVA: 0x0003D4F0 File Offset: 0x0003B6F0
		[Conditional("TRACE")]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		/// <summary>Checks for a condition; if the condition is false, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the specified message is not sent and the message box is not displayed.  </param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D65 RID: 3429 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		/// <summary>Checks for a condition; if the condition is false, outputs two specified messages and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the specified messages are not sent and the message box is not displayed.  </param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <param name="detailMessage">The detailed message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D66 RID: 3430 RVA: 0x0003D501 File Offset: 0x0003B701
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		/// <summary>Emits the specified error message.</summary>
		/// <param name="message">A message to emit. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D67 RID: 3431 RVA: 0x0003D520 File Offset: 0x0003B720
		[Conditional("TRACE")]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		/// <summary>Emits an error message, and a detailed error message.</summary>
		/// <param name="message">A message to emit. </param>
		/// <param name="detailMessage">A detailed message to emit. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D68 RID: 3432 RVA: 0x0003D528 File Offset: 0x0003B728
		[Conditional("TRACE")]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		/// <summary>Refreshes the trace configuration data.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D69 RID: 3433 RVA: 0x0003FB6D File Offset: 0x0003DD6D
		public static void Refresh()
		{
			DiagnosticsConfiguration.Refresh();
			Switch.RefreshAll();
			TraceSource.RefreshAll();
			TraceInternal.Refresh();
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D6A RID: 3434 RVA: 0x0003FB83 File Offset: 0x0003DD83
		[Conditional("TRACE")]
		public static void TraceInformation(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, message, null);
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D6B RID: 3435 RVA: 0x0003FB8E File Offset: 0x0003DD8E
		[Conditional("TRACE")]
		public static void TraceInformation(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		/// <summary>Writes a warning message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D6C RID: 3436 RVA: 0x0003FB99 File Offset: 0x0003DD99
		[Conditional("TRACE")]
		public static void TraceWarning(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, message, null);
		}

		/// <summary>Writes a warning message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D6D RID: 3437 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		[Conditional("TRACE")]
		public static void TraceWarning(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, format, args);
		}

		/// <summary>Writes an error message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D6E RID: 3438 RVA: 0x0003FBAF File Offset: 0x0003DDAF
		[Conditional("TRACE")]
		public static void TraceError(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, message, null);
		}

		/// <summary>Writes an error message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D6F RID: 3439 RVA: 0x0003FBBA File Offset: 0x0003DDBA
		[Conditional("TRACE")]
		public static void TraceError(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, format, args);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D70 RID: 3440 RVA: 0x0003D54C File Offset: 0x0003B74C
		[Conditional("TRACE")]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D71 RID: 3441 RVA: 0x0003D554 File Offset: 0x0003B754
		[Conditional("TRACE")]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		/// <summary>Writes a category name and a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D72 RID: 3442 RVA: 0x0003D55C File Offset: 0x0003B75C
		[Conditional("TRACE")]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D73 RID: 3443 RVA: 0x0003D565 File Offset: 0x0003B765
		[Conditional("TRACE")]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D74 RID: 3444 RVA: 0x0003D531 File Offset: 0x0003B731
		[Conditional("TRACE")]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D75 RID: 3445 RVA: 0x0003D56E File Offset: 0x0003B76E
		[Conditional("TRACE")]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D76 RID: 3446 RVA: 0x0003D576 File Offset: 0x0003B776
		[Conditional("TRACE")]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D77 RID: 3447 RVA: 0x0003D57F File Offset: 0x0003B77F
		[Conditional("TRACE")]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D78 RID: 3448 RVA: 0x0003D588 File Offset: 0x0003B788
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D79 RID: 3449 RVA: 0x0003D591 File Offset: 0x0003B791
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D7A RID: 3450 RVA: 0x0003D59A File Offset: 0x0003B79A
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D7B RID: 3451 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D7C RID: 3452 RVA: 0x0003D5AE File Offset: 0x0003B7AE
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D7D RID: 3453 RVA: 0x0003D5B7 File Offset: 0x0003B7B7
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D7E RID: 3454 RVA: 0x0003D5C0 File Offset: 0x0003B7C0
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is true.</summary>
		/// <param name="condition">true to cause a message to be written; otherwise, false. </param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D7F RID: 3455 RVA: 0x0003D5CA File Offset: 0x0003B7CA
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		/// <summary>Increases the current <see cref="P:System.Diagnostics.Trace.IndentLevel" /> by one.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D80 RID: 3456 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
		[Conditional("TRACE")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		/// <summary>Decreases the current <see cref="P:System.Diagnostics.Trace.IndentLevel" /> by one.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D81 RID: 3457 RVA: 0x0003D5DB File Offset: 0x0003B7DB
		[Conditional("TRACE")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}

		// Token: 0x0400104F RID: 4175
		private static volatile CorrelationManager correlationManager;
	}
}
