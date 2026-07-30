using System;
using System.Collections;

namespace System.Web.Services.Description
{
	/// <summary>Provides a collection of instances of the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> class. This class cannot be inherited.</summary>
	// Token: 0x020000D5 RID: 213
	public sealed class MimeTextMatchCollection : CollectionBase
	{
		/// <summary>Gets or sets the value of the member of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> at the specified zero-based index.</summary>
		/// <returns>A MimeTextMatch.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> whose value is returned or modified. </param>
		// Token: 0x17000166 RID: 358
		public MimeTextMatch this[int index]
		{
			get
			{
				return (MimeTextMatch)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to the end of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="match" /> parameter has been added.</returns>
		/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to add to the collection. </param>
		// Token: 0x06000568 RID: 1384 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(MimeTextMatch match)
		{
			return base.List.Add(match);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="match" /> parameter. </param>
		/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x06000569 RID: 1385 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, MimeTextMatch match)
		{
			base.List.Insert(index, match);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> for which to search in the collection. </param>
		// Token: 0x0600056A RID: 1386 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(MimeTextMatch match)
		{
			return base.List.IndexOf(match);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> is a member of the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" />.</summary>
		/// <returns>true if the <paramref name="match" /> parameter is a member of the MimeTextMatchCollection; otherwise, false.</returns>
		/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> for which to check collection membership. </param>
		// Token: 0x0600056B RID: 1387 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(MimeTextMatch match)
		{
			return base.List.Contains(match);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.MimeTextMatch" /> from the <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" />.</summary>
		/// <param name="match">The <see cref="T:System.Web.Services.Description.MimeTextMatch" /> to remove from the collection. </param>
		// Token: 0x0600056C RID: 1388 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(MimeTextMatch match)
		{
			base.List.Remove(match);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.MimeTextMatch" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">The array of type <see cref="T:System.Web.Services.Description.MimeTextMatch" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x0600056D RID: 1389 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(MimeTextMatch[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
