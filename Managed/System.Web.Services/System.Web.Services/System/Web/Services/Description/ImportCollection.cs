using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Provides a collection of instances of the <see cref="T:System.Web.Services.Description.Import" /> class representing documents to be imported into the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x02000100 RID: 256
	public sealed class ImportCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal ImportCollection(ServiceDescription serviceDescription)
			: base(serviceDescription)
		{
		}

		/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.Import" /> at the specified zero-based index.</summary>
		/// <returns>An Import.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Import" /> whose value is modified or returned. </param>
		// Token: 0x170001F7 RID: 503
		public Import this[int index]
		{
			get
			{
				return (Import)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Import" /> to the end of the <see cref="T:System.Web.Services.Description.ImportCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="import" /> parameter has been added.</returns>
		/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> to add to the collection. </param>
		// Token: 0x060006DB RID: 1755 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(Import import)
		{
			return base.List.Add(import);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Import" /> instance to the <see cref="T:System.Web.Services.Description.ImportCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="import" /> parameter. </param>
		/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x060006DC RID: 1756 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, Import import)
		{
			base.List.Insert(index, import);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Import" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> for which to search in the collection. </param>
		// Token: 0x060006DD RID: 1757 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(Import import)
		{
			return base.List.IndexOf(import);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Import" /> is a member of the <see cref="T:System.Web.Services.Description.ImportCollection" />.</summary>
		/// <returns>true if the <paramref name="import" /> parameter is a member of the <see cref="T:System.Web.Services.Description.ImportCollection" />; otherwise, false.</returns>
		/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> for which to check collection membership. </param>
		// Token: 0x060006DE RID: 1758 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(Import import)
		{
			return base.List.Contains(import);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Import" /> from the <see cref="T:System.Web.Services.Description.ImportCollection" />.</summary>
		/// <param name="import">The <see cref="T:System.Web.Services.Description.Import" /> to remove from the collection. </param>
		// Token: 0x060006DF RID: 1759 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(Import import)
		{
			base.List.Remove(import);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ImportCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Import" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Import" /> serving as the destination of the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x060006E0 RID: 1760 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(Import[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		protected override void SetParent(object value, object parent)
		{
			((Import)value).SetParent((ServiceDescription)parent);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00003846 File Offset: 0x00001A46
		internal ImportCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
