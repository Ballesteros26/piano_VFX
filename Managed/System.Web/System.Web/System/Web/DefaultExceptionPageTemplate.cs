using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x02000067 RID: 103
	internal sealed class DefaultExceptionPageTemplate : ExceptionPageTemplate
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x000087AC File Offset: 0x000069AC
		public override void Init()
		{
			List<ExceptionPageTemplateFragment> fragments = base.Fragments;
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "PageTop",
				ResourceName = "ErrorTemplateCommon_Top.html",
				MacroNames = new List<string> { "Title", "ExceptionType", "ExceptionMessage", "Description", "Details" }
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "PageCustomErrorDefault",
				ResourceName = "DefaultErrorTemplate_CustomErrorDefault.html",
				ValidForPageType = ExceptionPageTemplateType.CustomErrorDefault
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "PageStandard",
				ResourceName = "DefaultErrorTemplate_StandardPage.html",
				ValidForPageType = ExceptionPageTemplateType.Standard,
				MacroNames = new List<string> { "StackTrace" }
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "PageHtmlizedException",
				ResourceName = "HtmlizedExceptionPage_Top.html",
				ValidForPageType = ExceptionPageTemplateType.Htmlized,
				MacroNames = new List<string> { "StackTrace", "HtmlizedExceptionOrigin", "HtmlizedExceptionSourceFile" }
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "File Short Source",
				ResourceName = "HtmlizedExceptionPage_FileShortSource.html",
				ValidForPageType = ExceptionPageTemplateType.SourceError,
				MacroNames = new List<string> { "HtmlizedExceptionShortSource", "HtmlizedExceptionSourceFile", "HtmlizedExceptionErrorLines" }
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "File Long Source",
				ResourceName = "HtmlizedExceptionPage_FileLongSource.html",
				ValidForPageType = ExceptionPageTemplateType.SourceError,
				MacroNames = new List<string> { "HtmlizedExceptionLongSource", "HtmlizedExceptionSourceFile", "HtmlizedExceptionErrorLines" },
				RequiredMacros = new List<string> { "HtmlizedExceptionLongSource" }
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "Compiler Output",
				ResourceName = "HtmlizedExceptionPage_CompilerOutput.html",
				ValidForPageType = ExceptionPageTemplateType.SourceError,
				MacroNames = new List<string> { "HtmlizedExceptionCompilerOutput", "HtmlizedExceptionSourceFile", "HtmlizedExceptionErrorLines" },
				RequiredMacros = new List<string> { "HtmlizedExceptionCompilerOutput" }
			});
			fragments.Add(new ExceptionPageTemplateFragment
			{
				Name = "PageBottom",
				ResourceName = "ErrorTemplateCommon_Bottom.html",
				MacroNames = new List<string> { "RuntimeVersionInformation", "AspNetVersionInformation", "FullStackTrace" }
			});
		}
	}
}
