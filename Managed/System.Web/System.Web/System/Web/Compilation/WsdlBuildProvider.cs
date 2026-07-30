using System;
using System.CodeDom;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Discovery;

namespace System.Web.Compilation
{
	// Token: 0x02000677 RID: 1655
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web | BuildProviderAppliesTo.Code)]
	internal sealed class WsdlBuildProvider : BuildProvider
	{
		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x000C6C48 File Offset: 0x000C4E48
		public override CompilerType CodeCompilerType
		{
			get
			{
				if (this._compilerType == null)
				{
					CompilationSection compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
					if (compilationSection == null)
					{
						throw new HttpException("Unable to determine default compilation language.");
					}
					this._compilerType = BuildManager.GetDefaultCompilerTypeForLanguage(compilationSection.DefaultLanguage, compilationSection);
				}
				return this._compilerType;
			}
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000C6C94 File Offset: 0x000C4E94
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace();
			codeCompileUnit.Namespaces.Add(codeNamespace);
			ServiceDescription serviceDescription = ServiceDescription.Read(base.OpenReader());
			DiscoveryClientDocumentCollection discoveryClientDocumentCollection = new DiscoveryClientDocumentCollection { { base.VirtualPath, serviceDescription } };
			WebReferenceCollection webReferenceCollection = new WebReferenceCollection();
			webReferenceCollection.Add(new WebReference(discoveryClientDocumentCollection, codeNamespace));
			WebReferenceOptions webReferenceOptions = new WebReferenceOptions();
			webReferenceOptions.Style = ServiceDescriptionImportStyle.Client;
			ServiceDescriptionImporter.GenerateWebReferences(webReferenceCollection, assemblyBuilder.CodeDomProvider, codeCompileUnit, webReferenceOptions);
			assemblyBuilder.AddCodeCompileUnit(codeCompileUnit);
		}

		// Token: 0x04002555 RID: 9557
		private CompilerType _compilerType;
	}
}
