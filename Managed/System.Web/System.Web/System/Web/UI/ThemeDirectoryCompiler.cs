using System;
using System.IO;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001A0 RID: 416
	internal sealed class ThemeDirectoryCompiler
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x0002B8B4 File Offset: 0x00029AB4
		public static Type GetCompiledType(string theme, HttpContext context)
		{
			string text = "~/App_Themes/" + theme + "/";
			string text2 = context.Request.MapPath(text);
			if (!Directory.Exists(text2))
			{
				throw new HttpException(string.Format("Theme '{0}' cannot be found in the application or global theme directories.", theme));
			}
			string[] files = Directory.GetFiles(text2, "*.skin");
			PageThemeParser pageThemeParser = new PageThemeParser(new VirtualPath(text), context);
			string[] files2 = Directory.GetFiles(text2, "*.css");
			string[] array = new string[files2.Length];
			for (int i = 0; i < files2.Length; i++)
			{
				pageThemeParser.AddDependency(files2[i]);
				array[i] = text + Path.GetFileName(files2[i]);
			}
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			pageThemeParser.LinkedStyleSheets = array;
			AspComponentFoundry aspComponentFoundry = new AspComponentFoundry();
			pageThemeParser.RootBuilder = new RootBuilder();
			for (int j = 0; j < files.Length; j++)
			{
				PageThemeFileParser pageThemeFileParser = new PageThemeFileParser(new VirtualPath(VirtualPathUtility.Combine(text, Path.GetFileName(files[j]))), files[j], context);
				pageThemeParser.AddDependency(files[j]);
				AspGenerator aspGenerator = new AspGenerator(pageThemeFileParser);
				pageThemeFileParser.RootBuilder.Foundry = aspComponentFoundry;
				aspGenerator.Parse();
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
				foreach (string text3 in pageThemeFileParser.Assemblies)
				{
					if (!pageThemeParser.Assemblies.Contains(text3))
					{
						pageThemeParser.AddAssemblyByFileName(text3);
					}
				}
			}
			return new PageThemeCompiler(pageThemeParser).GetCompiledType();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0002BAB4 File Offset: 0x00029CB4
		public static PageTheme GetCompiledInstance(string theme, HttpContext context)
		{
			Type compiledType = ThemeDirectoryCompiler.GetCompiledType(theme, context);
			if (compiledType == null)
			{
				return null;
			}
			return (PageTheme)Activator.CreateInstance(compiledType);
		}
	}
}
