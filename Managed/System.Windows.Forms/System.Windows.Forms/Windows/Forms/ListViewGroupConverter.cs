using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200022B RID: 555
	internal class ListViewGroupConverter : TypeConverter
	{
		// Token: 0x06002452 RID: 9298 RVA: 0x00089308 File Offset: 0x00087508
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0008930C File Offset: 0x0008750C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}
	}
}
