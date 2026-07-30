using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x0200012A RID: 298
	internal class SoapParameter
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0003CD30 File Offset: 0x0003AF30
		internal bool IsOut
		{
			get
			{
				return (this.codeFlags & CodeFlags.IsOut) > (CodeFlags)0;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0003CD3E File Offset: 0x0003AF3E
		internal bool IsByRef
		{
			get
			{
				return (this.codeFlags & CodeFlags.IsByRef) > (CodeFlags)0;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0003CD4C File Offset: 0x0003AF4C
		internal static string[] GetTypeFullNames(IList parameters, int specifiedCount, CodeDomProvider codeProvider)
		{
			string[] array = new string[parameters.Count + specifiedCount];
			SoapParameter.GetTypeFullNames(parameters, array, 0, specifiedCount, codeProvider);
			return array;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0003CD74 File Offset: 0x0003AF74
		internal static void GetTypeFullNames(IList parameters, string[] typeFullNames, int start, int specifiedCount, CodeDomProvider codeProvider)
		{
			int num = 0;
			for (int i = 0; i < parameters.Count; i++)
			{
				typeFullNames[i + start + num] = WebCodeGenerator.FullTypeName(((SoapParameter)parameters[i]).mapping, codeProvider);
				if (((SoapParameter)parameters[i]).mapping.CheckSpecified)
				{
					num++;
					typeFullNames[i + start + num] = typeof(bool).FullName;
				}
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		internal static string[] GetNames(IList parameters, int specifiedCount)
		{
			string[] array = new string[parameters.Count + specifiedCount];
			SoapParameter.GetNames(parameters, array, 0, specifiedCount);
			return array;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003CE10 File Offset: 0x0003B010
		internal static void GetNames(IList parameters, string[] names, int start, int specifiedCount)
		{
			int num = 0;
			for (int i = 0; i < parameters.Count; i++)
			{
				names[i + start + num] = ((SoapParameter)parameters[i]).name;
				if (((SoapParameter)parameters[i]).mapping.CheckSpecified)
				{
					num++;
					names[i + start + num] = ((SoapParameter)parameters[i]).specifiedName;
				}
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003CE7C File Offset: 0x0003B07C
		internal static CodeFlags[] GetCodeFlags(IList parameters, int specifiedCount)
		{
			CodeFlags[] array = new CodeFlags[parameters.Count + specifiedCount];
			SoapParameter.GetCodeFlags(parameters, array, 0, specifiedCount);
			return array;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		internal static void GetCodeFlags(IList parameters, CodeFlags[] codeFlags, int start, int specifiedCount)
		{
			int num = 0;
			for (int i = 0; i < parameters.Count; i++)
			{
				codeFlags[i + start + num] = ((SoapParameter)parameters[i]).codeFlags;
				if (((SoapParameter)parameters[i]).mapping.CheckSpecified)
				{
					num++;
					codeFlags[i + start + num] = ((SoapParameter)parameters[i]).codeFlags;
				}
			}
		}

		// Token: 0x04000554 RID: 1364
		internal CodeFlags codeFlags;

		// Token: 0x04000555 RID: 1365
		internal string name;

		// Token: 0x04000556 RID: 1366
		internal XmlMemberMapping mapping;

		// Token: 0x04000557 RID: 1367
		internal string specifiedName;
	}
}
