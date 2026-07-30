using System;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> interface.</summary>
	// Token: 0x020007A3 RID: 1955
	public abstract class CodeCompiler : CodeGenerator, ICodeCompiler
	{
		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit)" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the source to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06003DF2 RID: 15858 RVA: 0x000DB064 File Offset: 0x000D9264
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromDom(options, e);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters,System.String)" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileName">The file name to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06003DF3 RID: 15859 RVA: 0x000DB0A8 File Offset: 0x000D92A8
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromFile(options, fileName);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters,System.String)" />.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="source">A string that indicates the source code to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000DB0EC File Offset: 0x000D92EC
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSource(options, source);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSourceBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="sources">An array of strings that indicates the source code to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000DB130 File Offset: 0x000D9330
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFileBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileNames">An array of strings that indicates the file names to compile. </param>
		// Token: 0x06003DF6 RID: 15862 RVA: 0x000DB174 File Offset: 0x000D9374
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults compilerResults;
			try
			{
				for (int i = 0; i < fileNames.Length; i++)
				{
					File.OpenRead(fileNames[i]).Dispose();
				}
				compilerResults = this.FromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDomBatch(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit[])" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile. </param>
		// Token: 0x06003DF7 RID: 15863 RVA: 0x000DB1E4 File Offset: 0x000D93E4
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>Gets the file name extension to use for source files.</summary>
		/// <returns>The file name extension to use for source files.</returns>
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003DF8 RID: 15864
		protected abstract string FileExtension { get; }

		/// <summary>Gets the name of the compiler executable.</summary>
		/// <returns>The name of the compiler executable.</returns>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003DF9 RID: 15865
		protected abstract string CompilerName { get; }

		/// <summary>Compiles the specified compile unit using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> object that indicates the source to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06003DFA RID: 15866 RVA: 0x000DB228 File Offset: 0x000D9428
		protected virtual CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromDomBatch(options, new CodeCompileUnit[] { e });
		}

		/// <summary>Compiles the specified file using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileName">The file name to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null. -or-<paramref name="fileName" /> is null.</exception>
		// Token: 0x06003DFB RID: 15867 RVA: 0x000DB249 File Offset: 0x000D9449
		protected virtual CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			File.OpenRead(fileName).Dispose();
			return this.FromFileBatch(options, new string[] { fileName });
		}

		/// <summary>Compiles the specified source code string using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="source">The source code string to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06003DFC RID: 15868 RVA: 0x000DB283 File Offset: 0x000D9483
		protected virtual CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromSourceBatch(options, new string[] { source });
		}

		/// <summary>Compiles the specified compile units using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.-or-<paramref name="ea" /> is null.</exception>
		// Token: 0x06003DFD RID: 15869 RVA: 0x000DB2A4 File Offset: 0x000D94A4
		protected virtual CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			string[] array = new string[ea.Length];
			for (int i = 0; i < ea.Length; i++)
			{
				if (ea[i] != null)
				{
					this.ResolveReferencedAssemblies(options, ea[i]);
					array[i] = options.TempFiles.AddExtension(i + this.FileExtension);
					using (FileStream fileStream = new FileStream(array[i], FileMode.Create, FileAccess.Write, FileShare.Read))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
						{
							((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, base.Options);
							streamWriter.Flush();
						}
					}
				}
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000DB37C File Offset: 0x000D957C
		private void ResolveReferencedAssemblies(CompilerParameters options, CodeCompileUnit e)
		{
			if (e.ReferencedAssemblies.Count > 0)
			{
				foreach (string text in e.ReferencedAssemblies)
				{
					if (!options.ReferencedAssemblies.Contains(text))
					{
						options.ReferencedAssemblies.Add(text);
					}
				}
			}
		}

		/// <summary>Compiles the specified files using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileNames">An array of strings that indicates the file names of the files to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.-or-<paramref name="fileNames" /> is null.</exception>
		// Token: 0x06003DFF RID: 15871 RVA: 0x000DB3F4 File Offset: 0x000D95F4
		protected virtual CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>Processes the specified line from the specified <see cref="T:System.CodeDom.Compiler.CompilerResults" />.</summary>
		/// <param name="results">A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> that indicates the results of compilation. </param>
		/// <param name="line">The line to process. </param>
		// Token: 0x06003E00 RID: 15872
		protected abstract void ProcessCompilerOutputLine(CompilerResults results, string line);

		/// <summary>Gets the command arguments to be passed to the compiler from the specified <see cref="T:System.CodeDom.Compiler.CompilerParameters" />.</summary>
		/// <returns>The command arguments.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> that indicates the compiler options. </param>
		// Token: 0x06003E01 RID: 15873
		protected abstract string CmdArgsFromParameters(CompilerParameters options);

		/// <summary>Gets the command arguments to use when invoking the compiler to generate a response file.</summary>
		/// <returns>The command arguments to use to generate a response file, or null if there are no response file arguments.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="cmdArgs">A command arguments string. </param>
		// Token: 0x06003E02 RID: 15874 RVA: 0x000DB418 File Offset: 0x000D9618
		protected virtual string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			string text = options.TempFiles.AddExtension("cmdline");
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					streamWriter.Write(cmdArgs);
					streamWriter.Flush();
				}
			}
			return "@\"" + text + "\"";
		}

		/// <summary>Compiles the specified source code strings using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="sources">An array of strings containing the source code to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.-or-<paramref name="sources" /> is null.</exception>
		// Token: 0x06003E03 RID: 15875 RVA: 0x000DB49C File Offset: 0x000D969C
		protected virtual CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			string[] array = new string[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				string text = options.TempFiles.AddExtension(i + this.FileExtension);
				using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						streamWriter.Write(sources[i]);
						streamWriter.Flush();
					}
				}
				array[i] = text;
			}
			return this.FromFileBatch(options, array);
		}

		/// <summary>Joins the specified string arrays.</summary>
		/// <returns>The concatenated string.</returns>
		/// <param name="sa">The array of strings to join. </param>
		/// <param name="separator">The separator to use. </param>
		// Token: 0x06003E04 RID: 15876 RVA: 0x000DB564 File Offset: 0x000D9764
		protected static string JoinStringArray(string[] sa, string separator)
		{
			if (sa == null || sa.Length == 0)
			{
				return string.Empty;
			}
			if (sa.Length == 1)
			{
				return "\"" + sa[0] + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sa.Length - 1; i++)
			{
				stringBuilder.Append('"');
				stringBuilder.Append(sa[i]);
				stringBuilder.Append('"');
				stringBuilder.Append(separator);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(sa[sa.Length - 1]);
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}
	}
}
