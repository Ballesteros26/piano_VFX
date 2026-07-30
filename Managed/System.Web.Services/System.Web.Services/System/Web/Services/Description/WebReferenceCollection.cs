using System;
using System.Collections;

namespace System.Web.Services.Description
{
	/// <summary>Describes a collection of <see cref="T:System.Web.Services.Description.WebReference" /> objects.</summary>
	// Token: 0x02000134 RID: 308
	public sealed class WebReferenceCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.WebReference" /> instance at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.WebReference" /> instance at the specified index.</returns>
		/// <param name="index">The index of the Web reference.</param>
		// Token: 0x1700026A RID: 618
		public WebReference this[int index]
		{
			get
			{
				return (WebReference)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Appends a <see cref="T:System.Web.Services.Description.WebReference" /> instance to the collection.</summary>
		/// <returns>The index of the appended Web reference.</returns>
		/// <param name="webReference">The Web reference to append.</param>
		// Token: 0x0600095E RID: 2398 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(WebReference webReference)
		{
			return base.List.Add(webReference);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.Services.Description.WebReference" /> instance at the specified index.</summary>
		/// <param name="index">The index at which to insert the specified Web reference.</param>
		/// <param name="webReference">The Web reference to insert.</param>
		// Token: 0x0600095F RID: 2399 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, WebReference webReference)
		{
			base.List.Insert(index, webReference);
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.Services.Description.WebReference" /> instance.</summary>
		/// <returns>The index of the specified Web reference, or -1 if the collection does not contain the specified Web reference.</returns>
		/// <param name="webReference">The Web reference to search for.</param>
		// Token: 0x06000960 RID: 2400 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(WebReference webReference)
		{
			return base.List.IndexOf(webReference);
		}

		/// <summary>Determines whether the collection contains a given <see cref="T:System.Web.Services.Description.WebReference" /> instance.</summary>
		/// <returns>true if the collections contains the given Web reference instance; otherwise, false.</returns>
		/// <param name="webReference">The Web reference to search for.</param>
		// Token: 0x06000961 RID: 2401 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(WebReference webReference)
		{
			return base.List.Contains(webReference);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Services.Description.WebReference" /> instance from the collection.</summary>
		/// <param name="webReference">The Web reference to remove.</param>
		// Token: 0x06000962 RID: 2402 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(WebReference webReference)
		{
			base.List.Remove(webReference);
		}

		/// <summary>Copies members of the collection to a specified array, starting at the specified array index.</summary>
		/// <param name="array">An array of Web references into which the collection members are copied.</param>
		/// <param name="index">The array index at which to begin copying.</param>
		// Token: 0x06000963 RID: 2403 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(WebReference[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
