using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x02000020 RID: 32
	internal class NullRefHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003BDB File Offset: 0x00001DDB
		public NullRefHandler(string _dataString)
		{
			this.dataString = _dataString;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return null;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003BED File Offset: 0x00001DED
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return typeof(object).AssemblyQualifiedName;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BED File Offset: 0x00001DED
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			return typeof(object).AssemblyQualifiedName;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003BFE File Offset: 0x00001DFE
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x04000D68 RID: 3432
		private string dataString;
	}
}
