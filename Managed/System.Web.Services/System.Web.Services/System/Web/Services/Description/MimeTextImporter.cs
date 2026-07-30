using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x020000DC RID: 220
	internal class MimeTextImporter : MimeImporter
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal override MimeParameterCollection ImportParameters()
		{
			return null;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00018DC4 File Offset: 0x00016FC4
		internal override MimeReturn ImportReturn()
		{
			MimeTextBinding mimeTextBinding = (MimeTextBinding)base.ImportContext.OperationBinding.Output.Extensions.Find(typeof(MimeTextBinding));
			if (mimeTextBinding == null)
			{
				return null;
			}
			if (mimeTextBinding.Matches.Count == 0)
			{
				base.ImportContext.UnsupportedOperationBindingWarning(Res.GetString("MissingMatchElement0"));
				return null;
			}
			this.methodName = CodeIdentifier.MakeValid(base.ImportContext.OperationBinding.Name);
			return new MimeTextReturn
			{
				TypeName = base.ImportContext.ClassNames.AddUnique(this.methodName + "Matches", mimeTextBinding),
				TextBinding = mimeTextBinding,
				ReaderType = typeof(TextReturnReader)
			};
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00018E84 File Offset: 0x00017084
		internal override void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
		{
			for (int i = 0; i < importedReturns.Length; i++)
			{
				if (importedReturns[i] is MimeTextReturn)
				{
					this.GenerateCode((MimeTextReturn)importedReturns[i], base.ImportContext.ServiceImporter.CodeGenerationOptions);
				}
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018EC7 File Offset: 0x000170C7
		private void GenerateCode(MimeTextReturn importedReturn, CodeGenerationOptions options)
		{
			this.GenerateCode(importedReturn.TypeName, importedReturn.TextBinding.Matches, options);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018EE4 File Offset: 0x000170E4
		private void GenerateCode(string typeName, MimeTextMatchCollection matches, CodeGenerationOptions options)
		{
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			CodeTypeDeclaration codeTypeDeclaration = WebCodeGenerator.AddClass(base.ImportContext.CodeNamespace, typeName, string.Empty, new string[0], null, CodeFlags.IsPublic, base.ImportContext.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes));
			string[] array = new string[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				MimeTextMatch mimeTextMatch = matches[i];
				string text = codeIdentifiers.AddUnique(CodeIdentifier.MakeValid((mimeTextMatch.Name.Length == 0) ? (this.methodName + "Match") : mimeTextMatch.Name), mimeTextMatch);
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
				if (mimeTextMatch.Pattern.Length == 0)
				{
					throw new ArgumentException(Res.GetString("WebTextMatchMissingPattern"));
				}
				CodeExpression codeExpression = new CodePrimitiveExpression(mimeTextMatch.Pattern);
				int num = 0;
				if (mimeTextMatch.Group != 1)
				{
					num++;
				}
				if (mimeTextMatch.Capture != 0)
				{
					num++;
				}
				if (mimeTextMatch.IgnoreCase)
				{
					num++;
				}
				if (mimeTextMatch.Repeats != 1 && mimeTextMatch.Repeats != 2147483647)
				{
					num++;
				}
				CodeExpression[] array2 = new CodeExpression[num];
				string[] array3 = new string[array2.Length];
				num = 0;
				if (mimeTextMatch.Group != 1)
				{
					array2[num] = new CodePrimitiveExpression(mimeTextMatch.Group);
					array3[num] = "Group";
					num++;
				}
				if (mimeTextMatch.Capture != 0)
				{
					array2[num] = new CodePrimitiveExpression(mimeTextMatch.Capture);
					array3[num] = "Capture";
					num++;
				}
				if (mimeTextMatch.IgnoreCase)
				{
					array2[num] = new CodePrimitiveExpression(mimeTextMatch.IgnoreCase);
					array3[num] = "IgnoreCase";
					num++;
				}
				if (mimeTextMatch.Repeats != 1 && mimeTextMatch.Repeats != 2147483647)
				{
					array2[num] = new CodePrimitiveExpression(mimeTextMatch.Repeats);
					array3[num] = "MaxRepeats";
					num++;
				}
				WebCodeGenerator.AddCustomAttribute(codeAttributeDeclarationCollection, typeof(MatchAttribute), new CodeExpression[] { codeExpression }, array3, array2);
				string text2;
				if (mimeTextMatch.Matches.Count > 0)
				{
					text2 = base.ImportContext.ClassNames.AddUnique(CodeIdentifier.MakeValid((mimeTextMatch.Type.Length == 0) ? text : mimeTextMatch.Type), mimeTextMatch);
					array[i] = text2;
				}
				else
				{
					text2 = typeof(string).FullName;
				}
				if (mimeTextMatch.Repeats != 1)
				{
					text2 += "[]";
				}
				CodeTypeMember codeTypeMember = WebCodeGenerator.AddMember(codeTypeDeclaration, text2, text, null, codeAttributeDeclarationCollection, CodeFlags.IsPublic, options);
				if (mimeTextMatch.Matches.Count == 0 && mimeTextMatch.Type.Length > 0)
				{
					base.ImportContext.Warnings |= ServiceDescriptionImportWarnings.OptionalExtensionsIgnored;
					ProtocolImporter.AddWarningComment(codeTypeMember.Comments, Res.GetString("WebTextMatchIgnoredTypeWarning"));
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				string text3 = array[j];
				if (text3 != null)
				{
					this.GenerateCode(text3, matches[j].Matches, options);
				}
			}
		}

		// Token: 0x0400039E RID: 926
		private string methodName;
	}
}
