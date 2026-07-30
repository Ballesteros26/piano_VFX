using System;
using System.Collections.Generic;
using System.Text;

namespace System.Web
{
	// Token: 0x0200006E RID: 110
	internal abstract class ExceptionPageTemplate
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00008D16 File Offset: 0x00006F16
		public List<ExceptionPageTemplateFragment> Fragments
		{
			get
			{
				if (this.fragments == null)
				{
					this.fragments = new List<ExceptionPageTemplateFragment>();
				}
				return this.fragments;
			}
		}

		// Token: 0x06000451 RID: 1105
		public abstract void Init();

		// Token: 0x06000452 RID: 1106 RVA: 0x00008D34 File Offset: 0x00006F34
		private void InitFragments(ExceptionPageTemplateValues values)
		{
			foreach (ExceptionPageTemplateFragment exceptionPageTemplateFragment in this.fragments)
			{
				if (exceptionPageTemplateFragment != null)
				{
					exceptionPageTemplateFragment.Init(values);
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00008D8C File Offset: 0x00006F8C
		public string Render(ExceptionPageTemplateValues values, ExceptionPageTemplateType pageType)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			StringBuilder sb = new StringBuilder();
			this.Render(values, pageType, delegate(string text)
			{
				sb.Append(text);
			});
			return sb.ToString();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public void Render(HttpResponse response, ExceptionPageTemplateValues values, ExceptionPageTemplateType pageType)
		{
			if (response == null)
			{
				return;
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.Render(values, pageType, delegate(string text)
			{
				response.Write(text);
			});
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00008E20 File Offset: 0x00007020
		private void Render(ExceptionPageTemplateValues values, ExceptionPageTemplateType pageType, Action<string> writer)
		{
			if (this.fragments == null || this.fragments.Count == 0 || values.Count == 0)
			{
				return;
			}
			this.InitFragments(values);
			foreach (ExceptionPageTemplateFragment exceptionPageTemplateFragment in this.fragments)
			{
				if (exceptionPageTemplateFragment != null && (exceptionPageTemplateFragment.ValidForPageType & pageType) != (ExceptionPageTemplateType)0)
				{
					string text = values.Get(exceptionPageTemplateFragment.Name);
					if (text != null && exceptionPageTemplateFragment.Visible(values))
					{
						writer(exceptionPageTemplateFragment.ReplaceMacros(text, values));
					}
				}
			}
		}

		// Token: 0x04000E66 RID: 3686
		public const string Template_PageTopName = "PageTop";

		// Token: 0x04000E67 RID: 3687
		public const string Template_PageBottomName = "PageBottom";

		// Token: 0x04000E68 RID: 3688
		public const string Template_PageStandardName = "PageStandard";

		// Token: 0x04000E69 RID: 3689
		public const string Template_PageCustomErrorDefaultName = "PageCustomErrorDefault";

		// Token: 0x04000E6A RID: 3690
		public const string Template_PageHtmlizedExceptionName = "PageHtmlizedException";

		// Token: 0x04000E6B RID: 3691
		public const string Template_PageTitleName = "Title";

		// Token: 0x04000E6C RID: 3692
		public const string Template_ExceptionTypeName = "ExceptionType";

		// Token: 0x04000E6D RID: 3693
		public const string Template_ExceptionMessageName = "ExceptionMessage";

		// Token: 0x04000E6E RID: 3694
		public const string Template_DescriptionName = "Description";

		// Token: 0x04000E6F RID: 3695
		public const string Template_DetailsName = "Details";

		// Token: 0x04000E70 RID: 3696
		public const string Template_RuntimeVersionInformationName = "RuntimeVersionInformation";

		// Token: 0x04000E71 RID: 3697
		public const string Template_AspNetVersionInformationName = "AspNetVersionInformation";

		// Token: 0x04000E72 RID: 3698
		public const string Template_StackTraceName = "StackTrace";

		// Token: 0x04000E73 RID: 3699
		public const string Template_FullStackTraceName = "FullStackTrace";

		// Token: 0x04000E74 RID: 3700
		public const string Template_HtmlizedExceptionOriginName = "HtmlizedExceptionOrigin";

		// Token: 0x04000E75 RID: 3701
		public const string Template_HtmlizedExceptionShortSourceName = "HtmlizedExceptionShortSource";

		// Token: 0x04000E76 RID: 3702
		public const string Template_HtmlizedExceptionLongSourceName = "HtmlizedExceptionLongSource";

		// Token: 0x04000E77 RID: 3703
		public const string Template_HtmlizedExceptionSourceFileName = "HtmlizedExceptionSourceFile";

		// Token: 0x04000E78 RID: 3704
		public const string Template_HtmlizedExceptionErrorLinesName = "HtmlizedExceptionErrorLines";

		// Token: 0x04000E79 RID: 3705
		public const string Template_HtmlizedExceptionCompilerOutputName = "HtmlizedExceptionCompilerOutput";

		// Token: 0x04000E7A RID: 3706
		private List<ExceptionPageTemplateFragment> fragments;
	}
}
