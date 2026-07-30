using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000235 RID: 565
	internal class ListViewSubItemConverter : ExpandableObjectConverter
	{
		// Token: 0x0600252A RID: 9514 RVA: 0x0008C9A0 File Offset: 0x0008ABA0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x0008C9BC File Offset: 0x0008ABBC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewItem.ListViewSubItem)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)value;
				Type[] array = new Type[]
				{
					typeof(ListViewItem),
					typeof(string),
					typeof(Color),
					typeof(Color),
					typeof(Font)
				};
				ConstructorInfo constructor = typeof(ListViewItem.ListViewSubItem).GetConstructor(array);
				if (constructor != null)
				{
					object[] array2 = new object[] { listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font };
					return new InstanceDescriptor(constructor, array2, true);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
