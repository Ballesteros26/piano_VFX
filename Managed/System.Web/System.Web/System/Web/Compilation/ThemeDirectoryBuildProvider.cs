using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000671 RID: 1649
	internal class ThemeDirectoryBuildProvider : TemplateBuildProvider
	{
		// Token: 0x060046B8 RID: 18104 RVA: 0x000C65A4 File Offset: 0x000C47A4
		protected override void OverrideAssemblyPrefix(TemplateParser parser, AssemblyBuilder assemblyBuilder)
		{
			if (parser == null || assemblyBuilder == null)
			{
				return;
			}
			string text = assemblyBuilder.OutputFilesPrefix + parser.ClassName + ".";
			assemblyBuilder.OutputFilesPrefix = text;
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000C65D6 File Offset: 0x000C47D6
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new PageThemeCompiler(parser as PageThemeParser);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000C65E3 File Offset: 0x000C47E3
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			return this.CreateParser(virtualPath, inputFile, context);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000C65F0 File Offset: 0x000C47F0
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string inputFile, HttpContext context)
		{
			string text = VirtualPathUtility.AppendTrailingSlash(virtualPath.Original);
			string physicalPath = virtualPath.PhysicalPath;
			if (!Directory.Exists(physicalPath))
			{
				throw new HttpException("Theme '" + virtualPath.Original + "' cannot be found in the application or global theme directories.");
			}
			PageThemeParser pageThemeParser = new PageThemeParser(virtualPath, context);
			string[] files = Directory.GetFiles(physicalPath, "*.css");
			string[] array = new string[files.Length];
			for (int i = 0; i < files.Length; i++)
			{
				array[i] = VirtualPathUtility.Combine(text, Path.GetFileName(files[i]));
				pageThemeParser.AddDependency(array[i]);
			}
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			pageThemeParser.LinkedStyleSheets = array;
			AspComponentFoundry aspComponentFoundry = new AspComponentFoundry();
			pageThemeParser.RootBuilder = new RootBuilder();
			foreach (string text2 in Directory.GetFiles(physicalPath, "*.skin"))
			{
				string text3 = VirtualPathUtility.Combine(text, Path.GetFileName(text2));
				PageThemeFileParser pageThemeFileParser = new PageThemeFileParser(new VirtualPath(text3), text2, context);
				pageThemeParser.AddDependency(text3);
				new AspGenerator(pageThemeFileParser, aspComponentFoundry).Parse();
				if (pageThemeFileParser.RootBuilder.Children != null)
				{
					foreach (object obj in pageThemeFileParser.RootBuilder.Children)
					{
						if (obj is ControlBuilder)
						{
							pageThemeParser.RootBuilder.AppendSubBuilder((ControlBuilder)obj);
						}
					}
				}
				foreach (string text4 in pageThemeFileParser.Assemblies)
				{
					if (!pageThemeParser.Assemblies.Contains(text4))
					{
						pageThemeParser.AddAssemblyByFileName(text4);
					}
				}
			}
			return pageThemeParser;
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x060046BC RID: 18108 RVA: 0x00008B66 File Offset: 0x00006D66
		protected override bool IsDirectoryBuilder
		{
			get
			{
				return true;
			}
		}
	}
}
