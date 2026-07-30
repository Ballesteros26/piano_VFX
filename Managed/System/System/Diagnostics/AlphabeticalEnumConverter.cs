using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020001E2 RID: 482
	internal sealed class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x000469A0 File Offset: 0x00044BA0
		public AlphabeticalEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000469A9 File Offset: 0x00044BA9
		[MonoTODO("Create sorted standart values")]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return base.Values;
		}
	}
}
