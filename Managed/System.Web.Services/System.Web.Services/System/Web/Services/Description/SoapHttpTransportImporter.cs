using System;
using System.CodeDom;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x02000128 RID: 296
	internal class SoapHttpTransportImporter : SoapTransportImporter
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0003C720 File Offset: 0x0003A920
		public override bool IsSupportedTransport(string transport)
		{
			return transport == "http://schemas.xmlsoap.org/soap/http";
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0003C730 File Offset: 0x0003A930
		public override void ImportClass()
		{
			SoapAddressBinding soapAddressBinding = ((base.ImportContext.Port == null) ? null : ((SoapAddressBinding)base.ImportContext.Port.Extensions.Find(typeof(SoapAddressBinding))));
			if (base.ImportContext.Style == ServiceDescriptionImportStyle.Client)
			{
				base.ImportContext.CodeTypeDeclaration.BaseTypes.Add(typeof(SoapHttpClientProtocol).FullName);
				CodeConstructor codeConstructor = WebCodeGenerator.AddConstructor(base.ImportContext.CodeTypeDeclaration, new string[0], new string[0], null, CodeFlags.IsPublic);
				codeConstructor.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
				bool flag = true;
				if (base.ImportContext is Soap12ProtocolImporter)
				{
					flag = false;
					CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapProtocolVersion)), Enum.Format(typeof(SoapProtocolVersion), SoapProtocolVersion.Soap12, "G"));
					CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "SoapVersion"), codeFieldReferenceExpression);
					codeConstructor.Statements.Add(codeAssignStatement);
				}
				ServiceDescription serviceDescription = base.ImportContext.Binding.ServiceDescription;
				string text = ((soapAddressBinding != null) ? soapAddressBinding.Location : null);
				string appSettingUrlKey = serviceDescription.AppSettingUrlKey;
				string appSettingBaseUrl = serviceDescription.AppSettingBaseUrl;
				ProtocolImporterUtil.GenerateConstructorStatements(codeConstructor, text, appSettingUrlKey, appSettingBaseUrl, flag && !base.ImportContext.IsEncodedBinding);
				return;
			}
			if (base.ImportContext.Style == ServiceDescriptionImportStyle.Server)
			{
				base.ImportContext.CodeTypeDeclaration.BaseTypes.Add(typeof(WebService).FullName);
			}
		}
	}
}
