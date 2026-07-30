using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the configuration settings of a language provider. This class cannot be inherited.</summary>
	// Token: 0x020007AC RID: 1964
	public sealed class CompilerInfo
	{
		// Token: 0x06003F3A RID: 16186 RVA: 0x000DF6DC File Offset: 0x000DD8DC
		private CompilerInfo()
		{
		}

		/// <summary>Gets the language names supported by the language provider.</summary>
		/// <returns>An array of language names supported by the language provider.</returns>
		// Token: 0x06003F3B RID: 16187 RVA: 0x000DF6EF File Offset: 0x000DD8EF
		public string[] GetLanguages()
		{
			return this.CloneCompilerLanguages();
		}

		/// <summary>Returns the file name extensions supported by the language provider.</summary>
		/// <returns>An array of file name extensions supported by the language provider.</returns>
		// Token: 0x06003F3C RID: 16188 RVA: 0x000DF6F7 File Offset: 0x000DD8F7
		public string[] GetExtensions()
		{
			return this.CloneCompilerExtensions();
		}

		/// <summary>Gets the type of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> instance that represents the configured language provider type.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The language provider is not configured on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Cannot locate the type because it is a null or empty string.-or-Cannot locate the type because the name for the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> cannot be found in the configuration file.</exception>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000DF700 File Offset: 0x000DD900
		public Type CodeDomProviderType
		{
			get
			{
				if (this._type == null)
				{
					lock (this)
					{
						if (this._type == null)
						{
							this._type = Type.GetType(this._codeDomProviderTypeName);
						}
					}
				}
				return this._type;
			}
		}

		/// <summary>Returns a value indicating whether the language provider implementation is configured on the computer.</summary>
		/// <returns>true if the language provider implementation type is configured on the computer; otherwise, false.</returns>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x000DF768 File Offset: 0x000DD968
		public bool IsCodeDomProviderTypeValid
		{
			get
			{
				return Type.GetType(this._codeDomProviderTypeName) != null;
			}
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings.</summary>
		/// <returns>A CodeDOM provider associated with the language provider configuration. </returns>
		// Token: 0x06003F3F RID: 16191 RVA: 0x000DF77C File Offset: 0x000DD97C
		public CodeDomProvider CreateProvider()
		{
			if (this._providerOptions.Count > 0)
			{
				ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[] { typeof(IDictionary<string, string>) });
				if (constructor != null)
				{
					return (CodeDomProvider)constructor.Invoke(new object[] { this._providerOptions });
				}
			}
			return (CodeDomProvider)Activator.CreateInstance(this.CodeDomProviderType);
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings and specified options.</summary>
		/// <returns>A CodeDOM provider associated with the language provider configuration and specified options.</returns>
		/// <param name="providerOptions">A collection of provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The provider does not support options.</exception>
		// Token: 0x06003F40 RID: 16192 RVA: 0x000DF7EC File Offset: 0x000DD9EC
		public CodeDomProvider CreateProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[] { typeof(IDictionary<string, string>) });
			if (constructor != null)
			{
				return (CodeDomProvider)constructor.Invoke(new object[] { providerOptions });
			}
			throw new InvalidOperationException(global::SR.Format("This CodeDomProvider type does not have a constructor that takes providerOptions - \"{0}\"", this.CodeDomProviderType.ToString()));
		}

		/// <summary>Gets the configured compiler settings for the language provider implementation.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> instance that contains the compiler options and settings configured for the language provider. </returns>
		// Token: 0x06003F41 RID: 16193 RVA: 0x000DF85F File Offset: 0x000DDA5F
		public CompilerParameters CreateDefaultCompilerParameters()
		{
			return this.CloneCompilerParameters();
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000DF867 File Offset: 0x000DDA67
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName, string[] compilerLanguages, string[] compilerExtensions)
		{
			this._compilerLanguages = compilerLanguages;
			this._compilerExtensions = compilerExtensions;
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			this._compilerParams = compilerParams ?? new CompilerParameters();
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000DF8A0 File Offset: 0x000DDAA0
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName)
		{
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			this._compilerParams = compilerParams ?? new CompilerParameters();
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> instance, suitable for use in hashing algorithms and data structures such as a hash table. </returns>
		// Token: 0x06003F44 RID: 16196 RVA: 0x000DF8CA File Offset: 0x000DDACA
		public override int GetHashCode()
		{
			return this._codeDomProviderTypeName.GetHashCode();
		}

		/// <summary>Determines whether the specified object represents the same language provider and compiler settings as the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />. </param>
		// Token: 0x06003F45 RID: 16197 RVA: 0x000DF8D8 File Offset: 0x000DDAD8
		public override bool Equals(object o)
		{
			CompilerInfo compilerInfo = o as CompilerInfo;
			return compilerInfo != null && (this.CodeDomProviderType == compilerInfo.CodeDomProviderType && this.CompilerParams.WarningLevel == compilerInfo.CompilerParams.WarningLevel && this.CompilerParams.IncludeDebugInformation == compilerInfo.CompilerParams.IncludeDebugInformation) && this.CompilerParams.CompilerOptions == compilerInfo.CompilerParams.CompilerOptions;
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000DF954 File Offset: 0x000DDB54
		private CompilerParameters CloneCompilerParameters()
		{
			return new CompilerParameters
			{
				IncludeDebugInformation = this._compilerParams.IncludeDebugInformation,
				TreatWarningsAsErrors = this._compilerParams.TreatWarningsAsErrors,
				WarningLevel = this._compilerParams.WarningLevel,
				CompilerOptions = this._compilerParams.CompilerOptions
			};
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000DF9AA File Offset: 0x000DDBAA
		private string[] CloneCompilerLanguages()
		{
			return (string[])this._compilerLanguages.Clone();
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000DF9BC File Offset: 0x000DDBBC
		private string[] CloneCompilerExtensions()
		{
			return (string[])this._compilerExtensions.Clone();
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000DF9CE File Offset: 0x000DDBCE
		internal CompilerParameters CompilerParams
		{
			get
			{
				return this._compilerParams;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000DF9D6 File Offset: 0x000DDBD6
		internal IDictionary<string, string> ProviderOptions
		{
			get
			{
				return this._providerOptions;
			}
		}

		// Token: 0x04002E38 RID: 11832
		internal readonly IDictionary<string, string> _providerOptions = new Dictionary<string, string>();

		// Token: 0x04002E39 RID: 11833
		internal string _codeDomProviderTypeName;

		// Token: 0x04002E3A RID: 11834
		internal CompilerParameters _compilerParams;

		// Token: 0x04002E3B RID: 11835
		internal string[] _compilerLanguages;

		// Token: 0x04002E3C RID: 11836
		internal string[] _compilerExtensions;

		// Token: 0x04002E3D RID: 11837
		private Type _type;
	}
}
