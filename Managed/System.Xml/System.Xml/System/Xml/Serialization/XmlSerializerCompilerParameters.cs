using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x020002D7 RID: 727
	internal sealed class XmlSerializerCompilerParameters
	{
		// Token: 0x06001B5A RID: 7002 RVA: 0x000984CD File Offset: 0x000966CD
		private XmlSerializerCompilerParameters(CompilerParameters parameters, bool needTempDirAccess)
		{
			this.needTempDirAccess = needTempDirAccess;
			this.parameters = parameters;
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x000984E3 File Offset: 0x000966E3
		internal bool IsNeedTempDirAccess
		{
			get
			{
				return this.needTempDirAccess;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000984EB File Offset: 0x000966EB
		internal CompilerParameters CodeDomParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000984F4 File Offset: 0x000966F4
		internal static XmlSerializerCompilerParameters Create(string location)
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = true;
			if (string.IsNullOrEmpty(location))
			{
				XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
				location = ((xmlSerializerSection == null) ? location : xmlSerializerSection.TempFilesLocation);
				if (!string.IsNullOrEmpty(location))
				{
					location = location.Trim();
				}
			}
			compilerParameters.TempFiles = new TempFileCollection(location);
			return new XmlSerializerCompilerParameters(compilerParameters, string.IsNullOrEmpty(location));
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0009855A File Offset: 0x0009675A
		internal static XmlSerializerCompilerParameters Create(CompilerParameters parameters, bool needTempDirAccess)
		{
			return new XmlSerializerCompilerParameters(parameters, needTempDirAccess);
		}

		// Token: 0x040015E1 RID: 5601
		private bool needTempDirAccess;

		// Token: 0x040015E2 RID: 5602
		private CompilerParameters parameters;
	}
}
