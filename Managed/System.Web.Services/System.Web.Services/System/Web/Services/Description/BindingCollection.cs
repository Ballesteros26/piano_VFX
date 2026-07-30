using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Binding" /> class supported by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x02000104 RID: 260
	public sealed class BindingCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal BindingCollection(ServiceDescription serviceDescription)
			: base(serviceDescription)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Binding" /> at the specified zero-based index.</summary>
		/// <returns>A Binding.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Binding" /> whose value is modified or returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x170001FE RID: 510
		public Binding this[int index]
		{
			get
			{
				return (Binding)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Binding" /> to the end of the <see cref="T:System.Web.Services.Description.BindingCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="binding" /> parameter has been added.</returns>
		/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> to add to the collection. </param>
		// Token: 0x0600070D RID: 1805 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(Binding binding)
		{
			return base.List.Add(binding);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Binding" /> to the <see cref="T:System.Web.Services.Description.BindingCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="binding" /> parameter. </param>
		/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> to be added to the collection. </param>
		// Token: 0x0600070E RID: 1806 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, Binding binding)
		{
			base.List.Insert(index, binding);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Binding" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> for which to search in the collection. </param>
		// Token: 0x0600070F RID: 1807 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(Binding binding)
		{
			return base.List.IndexOf(binding);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Binding" /> is a member of the <see cref="T:System.Web.Services.Description.BindingCollection" />.</summary>
		/// <returns>true if the <paramref name="binding" /> parameter is a member of the <see cref="T:System.Web.Services.Description.BindingCollection" />; otherwise, false.</returns>
		/// <param name="binding">A <see cref="T:System.Web.Services.Description.Binding" /> for which to check collection membership. </param>
		// Token: 0x06000710 RID: 1808 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(Binding binding)
		{
			return base.List.Contains(binding);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Binding" /> from the <see cref="T:System.Web.Services.Description.BindingCollection" />.</summary>
		/// <param name="binding">The <see cref="T:System.Web.Services.Description.Binding" /> to remove from the collection. </param>
		// Token: 0x06000711 RID: 1809 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(Binding binding)
		{
			base.List.Remove(binding);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.BindingCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Binding" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Binding" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x06000712 RID: 1810 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(Binding[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.Binding" /> specified by its name.</summary>
		/// <returns>A Binding.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Binding" /> returned. </param>
		// Token: 0x170001FF RID: 511
		public Binding this[string name]
		{
			get
			{
				return (Binding)this.Table[name];
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001D0E3 File Offset: 0x0001B2E3
		protected override string GetKey(object value)
		{
			return ((Binding)value).Name;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		protected override void SetParent(object value, object parent)
		{
			((Binding)value).SetParent((ServiceDescription)parent);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00003846 File Offset: 0x00001A46
		internal BindingCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
