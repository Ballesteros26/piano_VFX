using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	/// <summary>Imports ActiveX controls and generates a wrapper that can be accessed by a designer.</summary>
	// Token: 0x02000004 RID: 4
	[MonoTODO]
	public class AxImporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxImporter" /> class.</summary>
		/// <param name="options">An <see cref="T:System.Windows.Forms.Design.AxImporter.Options" /> that indicates the options for the ActiveX control importer to use. </param>
		// Token: 0x0600000C RID: 12 RVA: 0x0000233C File Offset: 0x0000053C
		[MonoTODO]
		public AxImporter(AxImporter.Options options)
		{
			this.options = options;
		}

		/// <summary>Gets the names of the assemblies that are generated for the control.</summary>
		/// <returns>An array of names of the generated assemblies, or an empty string array if no assemblies have been generated.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string[] GeneratedAssemblies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the names of the source files that were generated.</summary>
		/// <returns>An array of file names of the generated source files, or null if none exist.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string[] GeneratedSources
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the attributes for the generated type library.</summary>
		/// <returns>An array of type <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> that indicates the attributes for the generated type library.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public TYPELIBATTR[] GeneratedTypeLibAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Generates a wrapper for an ActiveX control for use in the design-time environment.</summary>
		/// <returns>An assembly qualified name for the type of ActiveX control for which a wrapper was generated.</returns>
		/// <param name="file">A <see cref="T:System.IO.FileInfo" /> indicating the file that contains the control. </param>
		/// <exception cref="T:System.Exception">A type library could not be loaded from <paramref name="file" />.</exception>
		// Token: 0x06000010 RID: 16 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string GenerateFromFile(FileInfo file)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a wrapper for an ActiveX control for use in the design-time environment.</summary>
		/// <returns>An assembly qualified name for the type of ActiveX control for which a wrapper was generated.</returns>
		/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that indicates the type library to generate the control from. </param>
		/// <exception cref="T:System.Exception">No registered ActiveX control was found in <paramref name="typeLib" />.</exception>
		// Token: 0x06000011 RID: 17 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string GenerateFromTypeLibrary(UCOMITypeLib typeLib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a wrapper for an ActiveX control for use in the design-time environment.</summary>
		/// <returns>An assembly qualified name for the type of ActiveX control for which a wrapper was generated.</returns>
		/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that indicates the type library to generate the control from. </param>
		/// <param name="clsid">The <see cref="T:System.Guid" /> for the control wrapper. </param>
		/// <exception cref="T:System.Exception">No registered ActiveX control was found in <paramref name="typeLib" />.</exception>
		// Token: 0x06000012 RID: 18 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string GenerateFromTypeLibrary(UCOMITypeLib typeLib, Guid clsid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the path and file name to the specified type library.</summary>
		/// <returns>The path and file name to the specified type library, or null if the library could not be located.</returns>
		/// <param name="tlibattr">A <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> that indicates the type library to retrieve the file name of. </param>
		// Token: 0x06000013 RID: 19 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string GetFileOfTypeLib(ref TYPELIBATTR tlibattr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000003 RID: 3
		internal AxImporter.Options options;

		/// <summary>Represents a set of options for an <see cref="T:System.Windows.Forms.Design.AxImporter" />.</summary>
		// Token: 0x02000005 RID: 5
		public sealed class Options
		{
			/// <summary>Specifies whether the generated assembly is strongly named and will be signed later.</summary>
			// Token: 0x04000004 RID: 4
			[MonoTODO]
			public bool delaySign;

			/// <summary>Specifies whether sources for the type library wrapper should be generated.</summary>
			// Token: 0x04000005 RID: 5
			[MonoTODO]
			public bool genSources;

			/// <summary>Specifies the path to the file that contains the strong name key container for the generated assemblies.</summary>
			// Token: 0x04000006 RID: 6
			[MonoTODO]
			public string keyContainer;

			/// <summary>Specifies the path to the file that contains the strong name key for the generated assemblies.</summary>
			// Token: 0x04000007 RID: 7
			[MonoTODO]
			public string keyFile;

			/// <summary>Specifies the strong name used for the generated assemblies.</summary>
			// Token: 0x04000008 RID: 8
			[MonoTODO]
			public StrongNameKeyPair keyPair;

			/// <summary>Indicates whether the ActiveX importer tool logo will be displayed when the control is imported.</summary>
			// Token: 0x04000009 RID: 9
			[MonoTODO]
			public bool noLogo;

			/// <summary>Specifies the path to the directory that the generated assemblies will be created in.</summary>
			// Token: 0x0400000A RID: 10
			[MonoTODO]
			public string outputDirectory;

			/// <summary>Specifies the filename to generate the ActiveX control wrapper to.</summary>
			// Token: 0x0400000B RID: 11
			[MonoTODO]
			public string outputName;

			/// <summary>Specifies whether to overwrite existing files when generating assemblies.</summary>
			// Token: 0x0400000C RID: 12
			[MonoTODO]
			public bool overwriteRCW;

			/// <summary>Specifies the public key used to sign the generated assemblies.</summary>
			// Token: 0x0400000D RID: 13
			[MonoTODO]
			public byte[] publicKey;

			/// <summary>Specifies the <see cref="T:System.Windows.Forms.Design.AxImporter.IReferenceResolver" /> to use to resolve types and references when generating assemblies.</summary>
			// Token: 0x0400000E RID: 14
			[MonoTODO]
			public AxImporter.IReferenceResolver references;

			/// <summary>Specifies whether to compile in silent mode, which generates less displayed information at compile time.</summary>
			// Token: 0x0400000F RID: 15
			[MonoTODO]
			public bool silentMode;

			/// <summary>Specifies whether to compile in verbose mode, which generates more displayed information at compile time.</summary>
			// Token: 0x04000010 RID: 16
			[MonoTODO]
			public bool verboseMode;

			/// <summary>Specifies whether errors are output in the Microsoft Build Engine (MSBuild) format.</summary>
			// Token: 0x04000011 RID: 17
			[MonoTODO]
			public bool msBuildErrors;

			/// <summary>Specifies whether to use only input from the command line instead relying on a registered version.</summary>
			// Token: 0x04000012 RID: 18
			public bool ignoreRegisteredOcx;
		}

		/// <summary>Provides methods to resolve references to ActiveX libraries, COM type libraries or assemblies, or managed assemblies.</summary>
		// Token: 0x02000006 RID: 6
		public interface IReferenceResolver
		{
			/// <summary>Resolves a reference to the specified type library that contains an ActiveX control.</summary>
			/// <returns>A fully qualified path to an assembly.</returns>
			/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> to resolve a reference to. </param>
			// Token: 0x06000015 RID: 21
			string ResolveActiveXReference(UCOMITypeLib typeLib);

			/// <summary>Resolves a reference to the specified assembly that contains a COM component.</summary>
			/// <returns>A fully qualified path to an assembly.</returns>
			/// <param name="name">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly to resolve a reference to. </param>
			// Token: 0x06000016 RID: 22
			string ResolveComReference(AssemblyName name);

			/// <summary>Resolves a reference to the specified type library that contains an COM component.</summary>
			/// <returns>A fully qualified path to an assembly.</returns>
			/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> to resolve a reference to. </param>
			// Token: 0x06000017 RID: 23
			string ResolveComReference(UCOMITypeLib typeLib);

			/// <summary>Resolves a reference to the specified assembly.</summary>
			/// <returns>A fully qualified path to an assembly.</returns>
			/// <param name="assemName">The name of the assembly to resolve a reference to. </param>
			// Token: 0x06000018 RID: 24
			string ResolveManagedReference(string assemName);
		}
	}
}
