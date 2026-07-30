using System;
using System.Collections.Specialized;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the parameters used to invoke a compiler.</summary>
	// Token: 0x020007AD RID: 1965
	[Serializable]
	public class CompilerParameters
	{
		/// <summary>Specifies an evidence object that represents the security policy permissions to grant the compiled assembly.</summary>
		/// <returns>An  object that represents the security policy permissions to grant the compiled assembly.</returns>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000DF9DE File Offset: 0x000DDBDE
		// (set) Token: 0x06003F4C RID: 16204 RVA: 0x000DF9F1 File Offset: 0x000DDBF1
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			get
			{
				Evidence evidence = this._evidence;
				if (evidence == null)
				{
					return null;
				}
				return evidence.Clone();
			}
			set
			{
				this._evidence = ((value != null) ? value.Clone() : null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class.</summary>
		// Token: 0x06003F4D RID: 16205 RVA: 0x000DFA05 File Offset: 0x000DDC05
		public CompilerParameters()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		// Token: 0x06003F4E RID: 16206 RVA: 0x000DFA0F File Offset: 0x000DDC0F
		public CompilerParameters(string[] assemblyNames)
			: this(assemblyNames, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names and output file name.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		// Token: 0x06003F4F RID: 16207 RVA: 0x000DFA1A File Offset: 0x000DDC1A
		public CompilerParameters(string[] assemblyNames, string outputName)
			: this(assemblyNames, outputName, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names, output name, and a value indicating whether to include debug information.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		/// <param name="includeDebugInformation">true to include debug information; false to exclude debug information. </param>
		// Token: 0x06003F50 RID: 16208 RVA: 0x000DFA28 File Offset: 0x000DDC28
		public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation)
		{
			if (assemblyNames != null)
			{
				this.ReferencedAssemblies.AddRange(assemblyNames);
			}
			this.OutputAssembly = outputName;
			this.IncludeDebugInformation = includeDebugInformation;
		}

		/// <summary>Gets or sets the name of the core or standard assembly that contains basic types such as <see cref="T:System.Object" />, <see cref="T:System.String" />, or <see cref="T:System.Int32" />.</summary>
		/// <returns>The name of the core assembly that contains basic types.</returns>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x000DFA8B File Offset: 0x000DDC8B
		// (set) Token: 0x06003F52 RID: 16210 RVA: 0x000DFA93 File Offset: 0x000DDC93
		public string CoreAssemblyFileName { get; set; } = string.Empty;

		/// <summary>Gets or sets a value indicating whether to generate an executable.</summary>
		/// <returns>true if an executable should be generated; otherwise, false.</returns>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x000DFA9C File Offset: 0x000DDC9C
		// (set) Token: 0x06003F54 RID: 16212 RVA: 0x000DFAA4 File Offset: 0x000DDCA4
		public bool GenerateExecutable { get; set; }

		/// <summary>Gets or sets a value indicating whether to generate the output in memory.</summary>
		/// <returns>true if the compiler should generate the output in memory; otherwise, false.</returns>
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000DFAAD File Offset: 0x000DDCAD
		// (set) Token: 0x06003F56 RID: 16214 RVA: 0x000DFAB5 File Offset: 0x000DDCB5
		public bool GenerateInMemory { get; set; }

		/// <summary>Gets the assemblies referenced by the current project.</summary>
		/// <returns>A collection that contains the assembly names that are referenced by the source to compile.</returns>
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x000DFABE File Offset: 0x000DDCBE
		public StringCollection ReferencedAssemblies
		{
			get
			{
				return this._assemblyNames;
			}
		}

		/// <summary>Gets or sets the name of the main class.</summary>
		/// <returns>The name of the main class.</returns>
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x000DFAC6 File Offset: 0x000DDCC6
		// (set) Token: 0x06003F59 RID: 16217 RVA: 0x000DFACE File Offset: 0x000DDCCE
		public string MainClass { get; set; }

		/// <summary>Gets or sets the name of the output assembly.</summary>
		/// <returns>The name of the output assembly.</returns>
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x000DFAD7 File Offset: 0x000DDCD7
		// (set) Token: 0x06003F5B RID: 16219 RVA: 0x000DFADF File Offset: 0x000DDCDF
		public string OutputAssembly { get; set; }

		/// <summary>Gets or sets the collection that contains the temporary files.</summary>
		/// <returns>A collection that contains the temporary files.</returns>
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x000DFAE8 File Offset: 0x000DDCE8
		// (set) Token: 0x06003F5D RID: 16221 RVA: 0x000DFB0D File Offset: 0x000DDD0D
		public TempFileCollection TempFiles
		{
			get
			{
				TempFileCollection tempFileCollection;
				if ((tempFileCollection = this._tempFiles) == null)
				{
					tempFileCollection = (this._tempFiles = new TempFileCollection());
				}
				return tempFileCollection;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to include debug information in the compiled executable.</summary>
		/// <returns>true if debug information should be generated; otherwise, false.</returns>
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x000DFB16 File Offset: 0x000DDD16
		// (set) Token: 0x06003F5F RID: 16223 RVA: 0x000DFB1E File Offset: 0x000DDD1E
		public bool IncludeDebugInformation { get; set; }

		/// <summary>Gets or sets a value indicating whether to treat warnings as errors.</summary>
		/// <returns>true if warnings should be treated as errors; otherwise, false.</returns>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000DFB27 File Offset: 0x000DDD27
		// (set) Token: 0x06003F61 RID: 16225 RVA: 0x000DFB2F File Offset: 0x000DDD2F
		public bool TreatWarningsAsErrors { get; set; }

		/// <summary>Gets or sets the warning level at which the compiler aborts compilation.</summary>
		/// <returns>The warning level at which the compiler aborts compilation.</returns>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000DFB38 File Offset: 0x000DDD38
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x000DFB40 File Offset: 0x000DDD40
		public int WarningLevel { get; set; } = -1;

		/// <summary>Gets or sets optional command-line arguments to use when invoking the compiler.</summary>
		/// <returns>Any additional command-line arguments for the compiler.</returns>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000DFB49 File Offset: 0x000DDD49
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x000DFB51 File Offset: 0x000DDD51
		public string CompilerOptions { get; set; }

		/// <summary>Gets or sets the file name of a Win32 resource file to link into the compiled assembly.</summary>
		/// <returns>A Win32 resource file that will be linked into the compiled assembly.</returns>
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000DFB5A File Offset: 0x000DDD5A
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x000DFB62 File Offset: 0x000DDD62
		public string Win32Resource { get; set; }

		/// <summary>Gets the .NET Framework resource files to include when compiling the assembly output.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources to include in the generated assembly.</returns>
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000DFB6B File Offset: 0x000DDD6B
		public StringCollection EmbeddedResources
		{
			get
			{
				return this._embeddedResources;
			}
		}

		/// <summary>Gets the .NET Framework resource files that are referenced in the current source.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources that are referenced by the source.</returns>
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000DFB73 File Offset: 0x000DDD73
		public StringCollection LinkedResources
		{
			get
			{
				return this._linkedResources;
			}
		}

		/// <summary>Gets or sets the user token to use when creating the compiler process.</summary>
		/// <returns>The user token to use.</returns>
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x000DFB7B File Offset: 0x000DDD7B
		// (set) Token: 0x06003F6B RID: 16235 RVA: 0x000DFB83 File Offset: 0x000DDD83
		public IntPtr UserToken { get; set; }

		// Token: 0x04002E3E RID: 11838
		private Evidence _evidence;

		// Token: 0x04002E3F RID: 11839
		private readonly StringCollection _assemblyNames = new StringCollection();

		// Token: 0x04002E40 RID: 11840
		private readonly StringCollection _embeddedResources = new StringCollection();

		// Token: 0x04002E41 RID: 11841
		private readonly StringCollection _linkedResources = new StringCollection();

		// Token: 0x04002E42 RID: 11842
		private TempFileCollection _tempFiles;
	}
}
