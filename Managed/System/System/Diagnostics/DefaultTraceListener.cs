using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides the default output methods and behavior for tracing.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001E9 RID: 489
	public class DefaultTraceListener : TraceListener
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x00047184 File Offset: 0x00045384
		static DefaultTraceListener()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_TRACE_LISTENER");
				if (environmentVariable != null)
				{
					string text;
					string text2;
					if (environmentVariable.StartsWith("Console.Out"))
					{
						text = "Console.Out";
						text2 = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Out");
					}
					else if (environmentVariable.StartsWith("Console.Error"))
					{
						text = "Console.Error";
						text2 = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Error");
					}
					else
					{
						text = environmentVariable;
						text2 = "";
					}
					DefaultTraceListener.MonoTraceFile = text;
					DefaultTraceListener.MonoTracePrefix = text2;
				}
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004720E File Offset: 0x0004540E
		private static string GetPrefix(string var, string target)
		{
			if (var.Length > target.Length)
			{
				return var.Substring(target.Length + 1);
			}
			return "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DefaultTraceListener" /> class with "Default" as its <see cref="P:System.Diagnostics.TraceListener.Name" /> property value.</summary>
		// Token: 0x06000F6C RID: 3948 RVA: 0x00047232 File Offset: 0x00045432
		public DefaultTraceListener()
			: base("Default")
		{
		}

		/// <summary>Gets or sets a value indicating whether the application is running in user-interface mode.</summary>
		/// <returns>true if user-interface mode is enabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0004723F File Offset: 0x0004543F
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x00047247 File Offset: 0x00045447
		[MonoTODO("AssertUiEnabled defaults to False; should follow Environment.UserInteractive.")]
		public bool AssertUiEnabled
		{
			get
			{
				return this.assertUiEnabled;
			}
			set
			{
				this.assertUiEnabled = value;
			}
		}

		/// <summary>Gets or sets the name of a log file to write trace or debug messages to.</summary>
		/// <returns>The name of a log file to write trace or debug messages to.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00047250 File Offset: 0x00045450
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x00047258 File Offset: 0x00045458
		[MonoTODO]
		public string LogFileName
		{
			get
			{
				return this.logFileName;
			}
			set
			{
				this.logFileName = value;
			}
		}

		/// <summary>Emits or displays a message and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F71 RID: 3953 RVA: 0x00047261 File Offset: 0x00045461
		public override void Fail(string message)
		{
			base.Fail(message);
		}

		/// <summary>Emits or displays detailed messages and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <param name="detailMessage">The detailed message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F72 RID: 3954 RVA: 0x0004726A File Offset: 0x0004546A
		public override void Fail(string message, string detailMessage)
		{
			base.Fail(message, detailMessage);
			if (this.ProcessUI(message, detailMessage) == DefaultTraceListener.DialogResult.Abort)
			{
				Thread.CurrentThread.Abort();
			}
			this.WriteLine(new StackTrace().ToString());
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0004729C File Offset: 0x0004549C
		private DefaultTraceListener.DialogResult ProcessUI(string message, string detailMessage)
		{
			if (!this.AssertUiEnabled)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			object obj;
			MethodInfo method;
			try
			{
				Assembly assembly = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				if (assembly == null)
				{
					return DefaultTraceListener.DialogResult.None;
				}
				Type type = assembly.GetType("System.Windows.Forms.MessageBoxButtons");
				obj = Enum.Parse(type, "AbortRetryIgnore");
				method = assembly.GetType("System.Windows.Forms.MessageBox").GetMethod("Show", new Type[]
				{
					typeof(string),
					typeof(string),
					type
				});
			}
			catch
			{
				return DefaultTraceListener.DialogResult.None;
			}
			if (method == null || obj == null)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			string text = string.Format("Assertion Failed: {0} to quit, {1} to debug, {2} to continue", "Abort", "Retry", "Ignore");
			string text2 = string.Format("{0}{1}{2}{1}{1}{3}", new object[]
			{
				message,
				Environment.NewLine,
				detailMessage,
				new StackTrace()
			});
			string text3 = method.Invoke(null, new object[] { text2, text, obj }).ToString();
			if (text3 == "Ignore")
			{
				return DefaultTraceListener.DialogResult.Ignore;
			}
			if (!(text3 == "Abort"))
			{
				return DefaultTraceListener.DialogResult.Retry;
			}
			return DefaultTraceListener.DialogResult.Abort;
		}

		// Token: 0x06000F74 RID: 3956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteWindowsDebugString(string message);

		// Token: 0x06000F75 RID: 3957 RVA: 0x000473DC File Offset: 0x000455DC
		private void WriteDebugString(string message)
		{
			if (DefaultTraceListener.OnWin32)
			{
				DefaultTraceListener.WriteWindowsDebugString(message);
				return;
			}
			this.WriteMonoTrace(message);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000473F4 File Offset: 0x000455F4
		private void WriteMonoTrace(string message)
		{
			string monoTraceFile = DefaultTraceListener.MonoTraceFile;
			if (monoTraceFile == "Console.Out")
			{
				Console.Out.Write(message);
				return;
			}
			if (!(monoTraceFile == "Console.Error"))
			{
				this.WriteLogFile(message, DefaultTraceListener.MonoTraceFile);
				return;
			}
			Console.Error.Write(message);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00047447 File Offset: 0x00045647
		private void WritePrefix()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				this.WriteMonoTrace(DefaultTraceListener.MonoTracePrefix);
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0004745B File Offset: 0x0004565B
		private void WriteImpl(string message)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
				this.WritePrefix();
			}
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
			}
			else
			{
				this.WriteDebugString(message);
			}
			this.WriteLogFile(message, this.LogFileName);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00047498 File Offset: 0x00045698
		private void WriteLogFile(string message, string logFile)
		{
			if (logFile != null && logFile.Length != 0)
			{
				FileInfo fileInfo = new FileInfo(logFile);
				StreamWriter streamWriter = null;
				try
				{
					if (fileInfo.Exists)
					{
						streamWriter = fileInfo.AppendText();
					}
					else
					{
						streamWriter = fileInfo.CreateText();
					}
				}
				catch
				{
					return;
				}
				using (streamWriter)
				{
					streamWriter.Write(message);
					streamWriter.Flush();
				}
			}
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method.</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000F7A RID: 3962 RVA: 0x00047510 File Offset: 0x00045710
		public override void Write(string message)
		{
			this.WriteImpl(message);
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method, followed by a carriage return and line feed (\r\n).</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000F7B RID: 3963 RVA: 0x0004751C File Offset: 0x0004571C
		public override void WriteLine(string message)
		{
			string text = message + Environment.NewLine;
			this.WriteImpl(text);
			base.NeedIndent = true;
		}

		// Token: 0x04001113 RID: 4371
		private static readonly bool OnWin32 = Path.DirectorySeparatorChar == '\\';

		// Token: 0x04001114 RID: 4372
		private const string ConsoleOutTrace = "Console.Out";

		// Token: 0x04001115 RID: 4373
		private const string ConsoleErrorTrace = "Console.Error";

		// Token: 0x04001116 RID: 4374
		private static readonly string MonoTracePrefix;

		// Token: 0x04001117 RID: 4375
		private static readonly string MonoTraceFile;

		// Token: 0x04001118 RID: 4376
		private string logFileName;

		// Token: 0x04001119 RID: 4377
		private bool assertUiEnabled;

		// Token: 0x020001EA RID: 490
		private enum DialogResult
		{
			// Token: 0x0400111B RID: 4379
			None,
			// Token: 0x0400111C RID: 4380
			Retry,
			// Token: 0x0400111D RID: 4381
			Ignore,
			// Token: 0x0400111E RID: 4382
			Abort
		}
	}
}
