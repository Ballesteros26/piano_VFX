using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a collection of interfaces for use in Web Parts connections.</summary>
	// Token: 0x0200047E RID: 1150
	public sealed class ConnectionInterfaceCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class. </summary>
		// Token: 0x06003443 RID: 13379 RVA: 0x0008A99C File Offset: 0x00088B9C
		public ConnectionInterfaceCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class with the specified collection. </summary>
		/// <param name="connectionInterfaces">A collection of objects to convert into a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">An object in <paramref name="connectionInterfaces" /> cannot be added to a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> collection.</exception>
		// Token: 0x06003444 RID: 13380 RVA: 0x0008A9A4 File Offset: 0x00088BA4
		public ConnectionInterfaceCollection(ICollection connectionInterfaces)
		{
			base.InnerList.AddRange(connectionInterfaces);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class by combining the two specified collections. </summary>
		/// <param name="existingConnectionInterfaces">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> to combine with the <paramref name="connectionInterfaces" /> object.</param>
		/// <param name="connectionInterfaces">A collection to combine with the <paramref name="existingConnectionInterfaces" /> object.</param>
		/// <exception cref="T:System.ArgumentException">An object in <paramref name="connectionInterfaces" /> cannot be added to a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> collection.</exception>
		// Token: 0x06003445 RID: 13381 RVA: 0x0008A9B8 File Offset: 0x00088BB8
		public ConnectionInterfaceCollection(ConnectionInterfaceCollection existingConnectionInterfaces, ICollection connectionInterfaces)
			: this()
		{
			base.InnerList.AddRange(existingConnectionInterfaces);
			base.InnerList.AddRange(connectionInterfaces);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> object contains a specific value.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />; otherwise, false.</returns>
		/// <param name="value">The type to locate in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />.</param>
		// Token: 0x06003446 RID: 13382 RVA: 0x0008A9D8 File Offset: 0x00088BD8
		public bool Contains(Type value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ReadOnlyCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003447 RID: 13383 RVA: 0x0008A9E6 File Offset: 0x00088BE6
		public void CopyTo(Type[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> collection. </summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The type to locate in the collection.</param>
		// Token: 0x06003448 RID: 13384 RVA: 0x0008A9F5 File Offset: 0x00088BF5
		public int IndexOf(Type value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		// Token: 0x1700106A RID: 4202
		public Type this[int index]
		{
			get
			{
				return (Type)base.InnerList[index];
			}
		}

		/// <summary>References a static, read-only instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class.</summary>
		// Token: 0x04001CFF RID: 7423
		public static readonly ConnectionInterfaceCollection Empty = new ConnectionInterfaceCollection();
	}
}
