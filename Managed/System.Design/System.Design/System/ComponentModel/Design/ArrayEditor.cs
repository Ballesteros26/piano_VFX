using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a user interface for editing arrays at design time.</summary>
	// Token: 0x020000F2 RID: 242
	public class ArrayEditor : CollectionEditor
	{
		/// <summary>Initializes a new instance of <see cref="T:System.ComponentModel.Design.ArrayEditor" /> using the specified data type for the array.</summary>
		/// <param name="type">The data type of the items in the array. </param>
		// Token: 0x060006DD RID: 1757 RVA: 0x00005128 File Offset: 0x00003328
		public ArrayEditor(Type type)
			: base(type)
		{
		}

		/// <summary>Gets the data type that this collection is designed to contain.</summary>
		/// <returns>A <see cref="T:System.Type" /> that indicates the data type that the collection is designed to contain.</returns>
		// Token: 0x060006DE RID: 1758 RVA: 0x000075C5 File Offset: 0x000057C5
		protected override Type CreateCollectionItemType()
		{
			return base.CollectionType.GetElementType();
		}

		/// <summary>Gets the items in the array.</summary>
		/// <returns>An array consisting of the items within the specified array. If the object specified in the <paramref name="editValue" /> parameter is not an array, a new empty object is returned.</returns>
		/// <param name="editValue">The array from which to retrieve the items. </param>
		// Token: 0x060006DF RID: 1759 RVA: 0x0000A6E4 File Offset: 0x000088E4
		protected override object[] GetItems(object editValue)
		{
			if (editValue == null)
			{
				return null;
			}
			if (!(editValue is Array))
			{
				return new object[0];
			}
			Array array = (Array)editValue;
			object[] array2 = new object[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		/// <summary>Sets the items in the array.</summary>
		/// <returns>An instance of the new array. If the object specified by the <paramref name="editValue" /> parameter is not an array, the object specified by the <paramref name="editValue" /> parameter is returned.</returns>
		/// <param name="editValue">The array to set the items to. </param>
		/// <param name="value">The array of objects to set as the items of the array. </param>
		// Token: 0x060006E0 RID: 1760 RVA: 0x0000A720 File Offset: 0x00008920
		protected override object SetItems(object editValue, object[] value)
		{
			if (editValue == null)
			{
				return null;
			}
			Array array = Array.CreateInstance(base.CollectionItemType, value.Length);
			value.CopyTo(array, 0);
			return array;
		}
	}
}
