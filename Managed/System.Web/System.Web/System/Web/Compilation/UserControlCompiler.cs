using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000673 RID: 1651
	internal class UserControlCompiler : TemplateControlCompiler
	{
		// Token: 0x060046C1 RID: 18113 RVA: 0x000C67F5 File Offset: 0x000C49F5
		public UserControlCompiler(UserControlParser parser)
			: base(parser)
		{
			this.parser = parser;
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000C6805 File Offset: 0x000C4A05
		public static Type CompileUserControlType(UserControlParser parser)
		{
			return new UserControlCompiler(parser).GetCompiledType();
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x000C6812 File Offset: 0x000C4A12
		protected override void AddClassAttributes()
		{
			if (this.parser.OutputCache)
			{
				this.AddOutputCacheAttribute();
			}
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000C6827 File Offset: 0x000C4A27
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			base.CreateProfileProperty();
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000C6838 File Offset: 0x000C4A38
		private void AddOutputCacheAttribute()
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.UI.PartialCachingAttribute");
			CodeAttributeArgumentCollection arguments = codeAttributeDeclaration.Arguments;
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheDuration);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheVaryByParam);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheVaryByControls);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheVaryByCustom);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheSqlDependency);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheShared);
			arguments.Add(new CodeAttributeArgument("ProviderName", new CodePrimitiveExpression(this.parser.ProviderName)));
			this.mainClass.CustomAttributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000C6900 File Offset: 0x000C4B00
		private void AddPrimitiveArgument(CodeAttributeArgumentCollection arguments, object obj)
		{
			arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(obj)));
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x000C6914 File Offset: 0x000C4B14
		protected override void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
		{
			base.AddStatementsToInitMethodTop(builder, method);
			if (this.parser.MasterPageFile != null)
			{
				CodeExpression codeExpression = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("__ctrl"), "MasterPageFile");
				CodeExpression codeExpression2 = new CodePrimitiveExpression(this.parser.MasterPageFile);
				method.Statements.Add(base.AddLinePragma(new CodeAssignStatement(codeExpression, codeExpression2), this.parser.DirectiveLocation));
			}
		}

		// Token: 0x04002552 RID: 9554
		private UserControlParser parser;
	}
}
