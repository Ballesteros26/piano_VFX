using System;
using System.Collections;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.MimePart" /> class. This class cannot be inherited.</summary>
	// Token: 0x020000D2 RID: 210
	public sealed class MimePartCollection : CollectionBase
	{
		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.MimePart" /> at the specified zero-based index.</summary>
		/// <returns>A MimePart.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.MimePart" /> whose value is modified or returned. </param>
		// Token: 0x1700015B RID: 347
		public MimePart this[int index]
		{
			get
			{
				return (MimePart)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimePart" /> to the end of the <see cref="T:System.Web.Services.Description.MimePartCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="mimePart" /> parameter has been added.</returns>
		/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to add to the collection. </param>
		// Token: 0x0600054B RID: 1355 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(MimePart mimePart)
		{
			return base.List.Add(mimePart);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimePart" /> to the <see cref="T:System.Web.Services.Description.MimePartCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="mimePart" /> parameter. </param>
		/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x0600054C RID: 1356 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, MimePart mimePart)
		{
			base.List.Insert(index, mimePart);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.MimePart" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> for which to search the <see cref="T:System.Web.Services.Description.MimePartCollection" />. </param>
		// Token: 0x0600054D RID: 1357 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(MimePart mimePart)
		{
			return base.List.IndexOf(mimePart);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.MimePart" /> is a member of the <see cref="T:System.Web.Services.Description.MimePartCollection" />.</summary>
		/// <returns>true if the <paramref name="mimePart" /> parameter is a member of the MimePartCollection; otherwise, false.</returns>
		/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to check for collection membership. </param>
		// Token: 0x0600054E RID: 1358 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(MimePart mimePart)
		{
			return base.List.Contains(mimePart);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.MimePart" /> from the <see cref="T:System.Web.Services.Description.MimePartCollection" />.</summary>
		/// <param name="mimePart">The <see cref="T:System.Web.Services.Description.MimePart" /> to remove from the collection. </param>
		// Token: 0x0600054F RID: 1359 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(MimePart mimePart)
		{
			base.List.Remove(mimePart);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MimePartCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.MimePart" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.MimePart" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x06000550 RID: 1360 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(MimePart[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
