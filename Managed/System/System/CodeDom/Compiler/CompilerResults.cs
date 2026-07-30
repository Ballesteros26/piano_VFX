using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the results of compilation that are returned from a compiler.</summary>
	// Token: 0x020007AE RID: 1966
	[Serializable]
	public class CompilerResults
	{
		/// <summary>Indicates the evidence object that represents the security policy permissions of the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object that represents the security policy permissions of the compiled assembly.</returns>
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000DFB8C File Offset: 0x000DDD8C
		// (set) Token: 0x06003F6D RID: 16237 RVA: 0x000DFB9F File Offset: 0x000DDD9F
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

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerResults" /> class that uses the specified temporary files.</summary>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06003F6E RID: 16238 RVA: 0x000DFBB3 File Offset: 0x000DDDB3
		public CompilerResults(TempFileCollection tempFiles)
		{
			this._tempFiles = tempFiles;
		}

		/// <summary>Gets or sets the temporary file collection to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</returns>
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000DFBD8 File Offset: 0x000DDDD8
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x000DFBE0 File Offset: 0x000DDDE0
		public TempFileCollection TempFiles
		{
			get
			{
				return this._tempFiles;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that indicates the compiled assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x000DFBE9 File Offset: 0x000DDDE9
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x000DFC23 File Offset: 0x000DDE23
		public Assembly CompiledAssembly
		{
			get
			{
				if (this._compiledAssembly == null && this.PathToAssembly != null)
				{
					this._compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.PathToAssembly
					});
				}
				return this._compiledAssembly;
			}
			set
			{
				this._compiledAssembly = value;
			}
		}

		/// <summary>Gets the collection of compiler errors and warnings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that indicates the errors and warnings resulting from compilation, if any.</returns>
		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000DFC2C File Offset: 0x000DDE2C
		public CompilerErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the compiler output messages.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the output messages.</returns>
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000DFC34 File Offset: 0x000DDE34
		public StringCollection Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets or sets the path of the compiled assembly.</summary>
		/// <returns>The path of the assembly, or null if the assembly was generated in memory.</returns>
		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000DFC3C File Offset: 0x000DDE3C
		// (set) Token: 0x06003F76 RID: 16246 RVA: 0x000DFC44 File Offset: 0x000DDE44
		public string PathToAssembly { get; set; }

		/// <summary>Gets or sets the compiler's return value.</summary>
		/// <returns>The compiler's return value.</returns>
		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000DFC4D File Offset: 0x000DDE4D
		// (set) Token: 0x06003F78 RID: 16248 RVA: 0x000DFC55 File Offset: 0x000DDE55
		public int NativeCompilerReturnValue { get; set; }

		// Token: 0x04002E4E RID: 11854
		private Evidence _evidence;

		// Token: 0x04002E4F RID: 11855
		private readonly CompilerErrorCollection _errors = new CompilerErrorCollection();

		// Token: 0x04002E50 RID: 11856
		private readonly StringCollection _output = new StringCollection();

		// Token: 0x04002E51 RID: 11857
		private Assembly _compiledAssembly;

		// Token: 0x04002E52 RID: 11858
		private TempFileCollection _tempFiles;
	}
}
