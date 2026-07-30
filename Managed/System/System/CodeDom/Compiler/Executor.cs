using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides command execution functions for invoking compilers. This class cannot be inherited.</summary>
	// Token: 0x020007BD RID: 1981
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public static class Executor
	{
		/// <summary>Executes the command using the specified temporary files and waits for the call to return.</summary>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06003FFD RID: 16381 RVA: 0x000E0A30 File Offset: 0x000DEC30
		public static void ExecWait(string cmd, TempFileCollection tempFiles)
		{
			string text = null;
			string text2 = null;
			Executor.ExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref text, ref text2);
		}

		/// <summary>Executes the specified command using the specified user token, current directory, and temporary files; then waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="userToken">The token to start the compiler process with. </param>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="currentDir">The directory to start the process in. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06003FFE RID: 16382 RVA: 0x000E0A54 File Offset: 0x000DEC54
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			int num;
			using (WindowsIdentity.Impersonate(userToken))
			{
				num = Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
			}
			return num;
		}

		/// <summary>Executes the specified command using the specified user token and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="userToken">The token to start the compiler process with. </param>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06003FFF RID: 16383 RVA: 0x000E0A94 File Offset: 0x000DEC94
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(userToken, cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified current directory and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="currentDir">The current directory. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06004000 RID: 16384 RVA: 0x000E0AA6 File Offset: 0x000DECA6
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified temporary files and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06004001 RID: 16385 RVA: 0x000E0AB3 File Offset: 0x000DECB3
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000E0AC4 File Offset: 0x000DECC4
		private static int InternalExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			if (cmd == null || cmd.Length == 0)
			{
				throw new ExternalException(global::Locale.GetText("No command provided for execution."));
			}
			if (outputName == null)
			{
				outputName = tempFiles.AddExtension("out");
			}
			if (errorName == null)
			{
				errorName = tempFiles.AddExtension("err");
			}
			int num = -1;
			Process process = new Process();
			process.StartInfo.FileName = cmd;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.WorkingDirectory = currentDir;
			try
			{
				process.Start();
				Executor.ProcessResultReader processResultReader = new Executor.ProcessResultReader(process.StandardOutput, outputName);
				Thread thread = new Thread(new ThreadStart(new Executor.ProcessResultReader(process.StandardError, errorName).Read));
				thread.Start();
				processResultReader.Read();
				thread.Join();
				process.WaitForExit();
			}
			finally
			{
				num = process.ExitCode;
				process.Close();
			}
			return num;
		}

		// Token: 0x020007BE RID: 1982
		private class ProcessResultReader
		{
			// Token: 0x06004003 RID: 16387 RVA: 0x000E0BCC File Offset: 0x000DEDCC
			public ProcessResultReader(StreamReader reader, string file)
			{
				this.reader = reader;
				this.file = file;
			}

			// Token: 0x06004004 RID: 16388 RVA: 0x000E0BE4 File Offset: 0x000DEDE4
			public void Read()
			{
				StreamWriter streamWriter = new StreamWriter(this.file);
				try
				{
					string text;
					while ((text = this.reader.ReadLine()) != null)
					{
						streamWriter.WriteLine(text);
					}
				}
				finally
				{
					streamWriter.Close();
				}
			}

			// Token: 0x04002E91 RID: 11921
			private StreamReader reader;

			// Token: 0x04002E92 RID: 11922
			private string file;
		}
	}
}
