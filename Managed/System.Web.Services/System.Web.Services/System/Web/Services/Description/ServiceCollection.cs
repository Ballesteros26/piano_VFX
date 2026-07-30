using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Service" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000105 RID: 261
	public sealed class ServiceCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal ServiceCollection(ServiceDescription serviceDescription)
			: base(serviceDescription)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Service" /> at the specified zero-based index.</summary>
		/// <returns>A Service.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Service" /> to be modified or returned. </param>
		// Token: 0x17000200 RID: 512
		public Service this[int index]
		{
			get
			{
				return (Service)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Service" /> to the end of the <see cref="T:System.Web.Services.Description.ServiceCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="service" /> parameter has been added.</returns>
		/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> instance to add to the collection. </param>
		// Token: 0x0600071A RID: 1818 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(Service service)
		{
			return base.List.Add(service);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Service" /> instance to the <see cref="T:System.Web.Services.Description.ServiceCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="service" /> parameter. </param>
		/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x0600071B RID: 1819 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, Service service)
		{
			base.List.Insert(index, service);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Service" /> and returns the zero-based index of the first occurrence within the ServiceCollection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> for which to search in the collection. </param>
		// Token: 0x0600071C RID: 1820 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(Service service)
		{
			return base.List.IndexOf(service);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Service" /> instance is a member of the <see cref="T:System.Web.Services.Description.ServiceCollection" />.</summary>
		/// <returns>true if the <paramref name="service" /> parameter is a member of the <see cref="T:System.Web.Services.Description.ServiceCollection" />; otherwise, false.</returns>
		/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> for which to check collection membership. </param>
		// Token: 0x0600071D RID: 1821 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(Service service)
		{
			return base.List.Contains(service);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Service" /> from the <see cref="T:System.Web.Services.Description.ServiceCollection" />.</summary>
		/// <param name="service">The <see cref="T:System.Web.Services.Description.Service" /> to remove from the collection. </param>
		// Token: 0x0600071E RID: 1822 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(Service service)
		{
			base.List.Remove(service);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ServiceCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.Service" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Service" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x0600071F RID: 1823 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(Service[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.Service" /> specified by its name.</summary>
		/// <returns>A Service.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Service" /> returned. </param>
		// Token: 0x17000201 RID: 513
		public Service this[string name]
		{
			get
			{
				return (Service)this.Table[name];
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001D129 File Offset: 0x0001B329
		protected override string GetKey(object value)
		{
			return ((Service)value).Name;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001D136 File Offset: 0x0001B336
		protected override void SetParent(object value, object parent)
		{
			((Service)value).SetParent((ServiceDescription)parent);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00003846 File Offset: 0x00001A46
		internal ServiceCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
