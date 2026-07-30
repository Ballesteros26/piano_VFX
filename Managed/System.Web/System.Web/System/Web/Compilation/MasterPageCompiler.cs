using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200065E RID: 1630
	internal class MasterPageCompiler : UserControlCompiler
	{
		// Token: 0x060045CE RID: 17870 RVA: 0x000BF04D File Offset: 0x000BD24D
		public MasterPageCompiler(MasterPageParser parser)
			: base(parser)
		{
			this.parser = parser;
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x000BF060 File Offset: 0x000BD260
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			Type masterType = this.parser.MasterType;
			if (masterType != null)
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = "Master";
				codeMemberProperty.Type = new CodeTypeReference(this.parser.MasterType);
				codeMemberProperty.Attributes = (MemberAttributes)24592;
				CodeExpression codeExpression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), "Master");
				codeExpression = new CodeCastExpression(this.parser.MasterType, codeExpression);
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
				this.mainClass.Members.Add(codeMemberProperty);
				base.AddReferencedAssembly(masterType.Assembly);
			}
		}

		// Token: 0x0400250A RID: 9482
		private MasterPageParser parser;
	}
}
