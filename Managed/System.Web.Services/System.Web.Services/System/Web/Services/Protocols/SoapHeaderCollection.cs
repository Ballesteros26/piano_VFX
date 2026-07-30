using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.Services.Protocols
{
	/// <summary>Contains a collection of instances of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> class.</summary>
	// Token: 0x0200006D RID: 109
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class SoapHeaderCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> at the specified index of the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameteris not a valid index in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </exception>
		// Token: 0x170000C5 RID: 197
		public SoapHeader this[int index]
		{
			get
			{
				return (SoapHeader)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
		/// <returns>The position at which the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> was inserted.</returns>
		/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to add to the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(SoapHeader header)
		{
			return base.List.Add(header);
		}

		/// <summary>Inserts a <see cref="T:System.Web.Services.Protocols.SoapHeader" /> into the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> into the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
		/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to insert into the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameteris not a valid index in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </exception>
		// Token: 0x060002D1 RID: 721 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, SoapHeader header)
		{
			base.List.Insert(index, header);
		}

		/// <summary>Determines the index of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
		/// <returns>The index of the <paramref name="header" /> parameter, if found in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />; otherwise, -1.</returns>
		/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to locate in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
		// Token: 0x060002D2 RID: 722 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(SoapHeader header)
		{
			return base.List.IndexOf(header);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> contains a specific <see cref="T:System.Web.Services.Protocols.SoapHeader" />.</summary>
		/// <returns>true if the value of the <paramref name="header" /> parameter is found in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />; otherwise, false.</returns>
		/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to locate in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(SoapHeader header)
		{
			return base.List.Contains(header);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Web.Services.Protocols.SoapHeader" /> from the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
		/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to remove from the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
		// Token: 0x060002D4 RID: 724 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(SoapHeader header)
		{
			base.List.Remove(header);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> to an <see cref="T:System.Array" />, starting at a particular index of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the <paramref name="array" /> parameter at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.-or- The number of elements in the source SoapHeaderCollection is greater than the available space from the <paramref name="index" /> parameter to the end of the destination array. </exception>
		// Token: 0x060002D5 RID: 725 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(SoapHeader[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
