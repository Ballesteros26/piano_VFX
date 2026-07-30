using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000311 RID: 785
	internal class TextBoxAutoCompleteSourceConverter : EnumConverter
	{
		// Token: 0x0600343F RID: 13375 RVA: 0x000C5FE8 File Offset: 0x000C41E8
		public TextBoxAutoCompleteSourceConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000C5FF4 File Offset: 0x000C41F4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
			AutoCompleteSource[] array = new AutoCompleteSource[standardValues.Count];
			standardValues.CopyTo(array, 0);
			AutoCompleteSource[] array2 = Array.FindAll<AutoCompleteSource>(array, (AutoCompleteSource value) => value != AutoCompleteSource.ListItems);
			return new TypeConverter.StandardValuesCollection(array2);
		}
	}
}
