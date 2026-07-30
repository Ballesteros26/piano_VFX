using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000097 RID: 151
	internal sealed class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x0002A3AB File Offset: 0x000285AB
		public DataTableTypeConverter()
			: base(typeof(DataTable))
		{
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
