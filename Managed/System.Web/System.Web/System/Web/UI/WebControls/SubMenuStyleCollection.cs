using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> objects in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	// Token: 0x02000416 RID: 1046
	public class SubMenuStyleCollection : StateManagedCollection
	{
		// Token: 0x06002F2E RID: 12078 RVA: 0x00064F65 File Offset: 0x00063165
		internal SubMenuStyleCollection()
		{
		}

		/// <summary>Adds a submenu style to the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</summary>
		/// <returns>The position in the collection at which the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance was inserted.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance to add to the collection.</param>
		// Token: 0x06002F2F RID: 12079 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(SubMenuStyle style)
		{
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether a <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection contains a specific <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance is found in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection; otherwise, false. If null is passed as the <paramref name="style" /> parameter, false is returned.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> instance to locate in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
		// Token: 0x06002F30 RID: 12080 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(SubMenuStyle style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies the contents of a <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection to an array, starting at a specified array index.</summary>
		/// <param name="styleArray">The one-dimensional array that is the destination of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> objects copied from the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection. The <paramref name="array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional- or -<paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.- or -The number of <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> objects in the source <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06002F31 RID: 12081 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(SubMenuStyle[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		/// <summary>Creates an <see cref="T:System.Object" /> of the data type that corresponds to the specified index.</summary>
		/// <returns>Always returns an empty <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object.</returns>
		/// <param name="index">The index of the data type to create. This parameter is not used in this implementation of the method; therefore, you should always pass in null.</param>
		// Token: 0x06002F32 RID: 12082 RVA: 0x0007CD9F File Offset: 0x0007AF9F
		protected override object CreateKnownType(int index)
		{
			return new SubMenuStyle();
		}

		/// <summary>Creates an array of <see cref="T:System.Type" /> objects that contains the supported data types of the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> class.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that contains the data types supported by the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> class.</returns>
		// Token: 0x06002F33 RID: 12083 RVA: 0x0007CDA6 File Offset: 0x0007AFA6
		protected override Type[] GetKnownTypes()
		{
			return SubMenuStyleCollection.types;
		}

		/// <summary>Determines the location of a specified <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object if it is found in the list; otherwise, -1.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to locate in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
		// Token: 0x06002F34 RID: 12084 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(SubMenuStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object into the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object should be inserted.</param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to insert into the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is outside the range of the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="style" /> is null.</exception>
		// Token: 0x06002F35 RID: 12085 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, SubMenuStyle style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object from the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is read-only.</exception>
		// Token: 0x06002F36 RID: 12086 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(SubMenuStyle style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object at the specified location.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Web.UI.StateManagedCollection.Count" />.</exception>
		// Token: 0x06002F37 RID: 12087 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection object.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object.</returns>
		/// <param name="i">The location of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object in the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than zero or greater than or equal to the <see cref="P:System.Web.UI.StateManagedCollection.Count" /> property value.</exception>
		// Token: 0x17000EFA RID: 3834
		public SubMenuStyle this[int i]
		{
			get
			{
				return (SubMenuStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		/// <summary>Instructs a <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object contained by the <see cref="T:System.Web.UI.WebControls.SubMenuStyleCollection" /> collection to record its entire state to view state.</summary>
		/// <param name="o">The object that should serialize itself completely.</param>
		// Token: 0x06002F3A RID: 12090 RVA: 0x0007CDBB File Offset: 0x0007AFBB
		protected override void SetDirtyObject(object o)
		{
			((SubMenuStyle)o).SetDirty();
		}

		// Token: 0x04001BE7 RID: 7143
		private static Type[] types = new Type[] { typeof(SubMenuStyle) };
	}
}
