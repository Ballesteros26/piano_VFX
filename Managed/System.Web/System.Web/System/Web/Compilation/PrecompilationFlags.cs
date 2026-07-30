using System;

namespace System.Web.Compilation
{
	/// <summary>Provides flags that determine precompilation behavior.</summary>
	// Token: 0x02000663 RID: 1635
	[Flags]
	public enum PrecompilationFlags
	{
		/// <summary>The default value; no special behavior specified for precompilation.</summary>
		// Token: 0x04002511 RID: 9489
		Default = 0,
		/// <summary>The deployed application will be updatable. This field corresponds to the -u switch on Aspnet_compiler.exe.</summary>
		// Token: 0x04002512 RID: 9490
		Updatable = 1,
		/// <summary>The target directory can be overwritten. This field corresponds to the -f switch on Aspnet_compiler.exe for a previously precompiled target.</summary>
		// Token: 0x04002513 RID: 9491
		OverwriteTarget = 2,
		/// <summary>The compiler will emit debug information. This field corresponds to the -d switch on Aspnet_compiler.exe.</summary>
		// Token: 0x04002514 RID: 9492
		ForceDebug = 4,
		/// <summary>The application will be built "clean": Any previously compiled components will be recompiled. This field corresponds to the -c switch on Aspnet_compiler.exe.</summary>
		// Token: 0x04002515 RID: 9493
		Clean = 8,
		/// <summary>The /define:CodeAnalysis flag will be added as a compilation symbol.</summary>
		// Token: 0x04002516 RID: 9494
		CodeAnalysis = 16,
		/// <summary>An <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute is generated for the assemblies, which means the assemblies can be called by partially trusted code. The /aptca flag will be added as a compilation symbol.</summary>
		// Token: 0x04002517 RID: 9495
		AllowPartiallyTrustedCallers = 32,
		/// <summary>The assembly is not fully signed when created. The assembly can be signed later by a signing tool such as Sn.exe. The /delaysign flag will be added as a compilation symbol.</summary>
		// Token: 0x04002518 RID: 9496
		DelaySign = 64,
		/// <summary>The assembly is generated with fixed names for the Web pages. The files are not batched during compilation and instead are compiled individually to produce the fixed names. </summary>
		// Token: 0x04002519 RID: 9497
		FixedNames = 128,
		/// <summary>The compiler will ignore bad image format exceptions.</summary>
		// Token: 0x0400251A RID: 9498
		IgnoreBadImageFormatException = 256
	}
}
