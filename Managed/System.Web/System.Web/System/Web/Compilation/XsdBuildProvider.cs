using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data.Design;
using System.IO;

namespace System.Web.Compilation
{
	// Token: 0x02000678 RID: 1656
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
	internal sealed class XsdBuildProvider : BuildProvider
	{
		// Token: 0x060046D8 RID: 18136 RVA: 0x000C6D14 File Offset: 0x000C4F14
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace(null);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			TextReader textReader = new StreamReader(HttpContext.Current.Request.MapPath(base.VirtualPath));
			CodeDomProvider codeDomProvider = assemblyBuilder.CodeDomProvider;
			if (codeDomProvider == null)
			{
				throw new HttpException("Assembly builder has no code provider");
			}
			TypedDataSetGenerator.Generate(textReader.ReadToEnd(), codeCompileUnit, codeNamespace, codeDomProvider);
			assemblyBuilder.AddCodeCompileUnit(codeCompileUnit);
		}
	}
}
