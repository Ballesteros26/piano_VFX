using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Port" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000102 RID: 258
	public sealed class PortCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal PortCollection(Service service)
			: base(service)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Port" /> at the specified zero-based index.</summary>
		/// <returns>The value of a port at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Port" /> whose value is modified or returned. </param>
		// Token: 0x170001FA RID: 506
		public Port this[int index]
		{
			get
			{
				return (Port)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Port" /> to the end of the <see cref="T:System.Web.Services.Description.PortCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="port" /> parameter has been added.</returns>
		/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> to add to the collection. </param>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(Port port)
		{
			return base.List.Add(port);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Port" /> instance to the <see cref="T:System.Web.Services.Description.PortCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="port" /> parameter.</param>
		/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x060006F4 RID: 1780 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, Port port)
		{
			base.List.Insert(index, port);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Port" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> for which to search in the collection.</param>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(Port port)
		{
			return base.List.IndexOf(port);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Port" /> is a member of the <see cref="T:System.Web.Services.Description.PortCollection" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.Services.Description.Port" /> is a member of the <see cref="T:System.Web.Services.Description.PortCollection" />; otherwise, false.</returns>
		/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> for which to check collection membership.</param>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(Port port)
		{
			return base.List.Contains(port);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Port" /> from the <see cref="T:System.Web.Services.Description.PortCollection" />.</summary>
		/// <param name="port">The <see cref="T:System.Web.Services.Description.Port" /> to remove from the collection. </param>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(Port port)
		{
			base.List.Remove(port);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.PortCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.Port" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Port" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(Port[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.Port" /> specified by its name.</summary>
		/// <returns>A port specified by its name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Port" /> returned. </param>
		// Token: 0x170001FB RID: 507
		public Port this[string name]
		{
			get
			{
				return (Port)this.Table[name];
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001D057 File Offset: 0x0001B257
		protected override string GetKey(object value)
		{
			return ((Port)value).Name;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001D064 File Offset: 0x0001B264
		protected override void SetParent(object value, object parent)
		{
			((Port)value).SetParent((Service)parent);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00003846 File Offset: 0x00001A46
		internal PortCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
