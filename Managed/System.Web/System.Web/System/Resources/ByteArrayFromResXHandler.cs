using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200001D RID: 29
	internal class ByteArrayFromResXHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003A61 File Offset: 0x00001C61
		public ByteArrayFromResXHandler(string data)
		{
			this.dataString = data;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A70 File Offset: 0x00001C70
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return Convert.FromBase64String(this.dataString);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A70 File Offset: 0x00001C70
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return Convert.FromBase64String(this.dataString);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A7D File Offset: 0x00001C7D
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return base.ResolveType(typeof(byte[]).AssemblyQualifiedName, typeResolver).AssemblyQualifiedName;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A9A File Offset: 0x00001C9A
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			return typeof(byte[]).AssemblyQualifiedName;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003AAB File Offset: 0x00001CAB
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x04000D65 RID: 3429
		private string dataString;
	}
}
