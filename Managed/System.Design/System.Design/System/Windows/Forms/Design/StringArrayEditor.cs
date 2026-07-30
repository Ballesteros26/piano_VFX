using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200003A RID: 58
	internal class StringArrayEditor : StringCollectionEditor
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00005139 File Offset: 0x00003339
		public StringArrayEditor(Type type)
			: base(type)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000755C File Offset: 0x0000575C
		protected override object[] GetItems(object editValue)
		{
			Array array = editValue as Array;
			if (array == null)
			{
				return new object[0];
			}
			object[] array2 = new object[array.GetLength(0)];
			Array.Copy(array, array2, array2.Length);
			return array2;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007594 File Offset: 0x00005794
		protected override object SetItems(object editValue, object[] value)
		{
			if (!(editValue is Array))
			{
				return editValue;
			}
			Array array = Array.CreateInstance(base.CollectionItemType, value.Length);
			Array.Copy(value, array, value.Length);
			return array;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000075C5 File Offset: 0x000057C5
		protected override Type CreateCollectionItemType()
		{
			return base.CollectionType.GetElementType();
		}
	}
}
