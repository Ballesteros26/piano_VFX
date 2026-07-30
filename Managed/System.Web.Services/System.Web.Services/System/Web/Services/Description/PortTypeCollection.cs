using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.PortType" /> class; that is, a collection of sets of operations supported by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x02000103 RID: 259
	public sealed class PortTypeCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal PortTypeCollection(ServiceDescription serviceDescription)
			: base(serviceDescription)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.PortType" /> at the specified zero-based index.</summary>
		/// <returns>A PortType.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.PortType" /> whose value is modified or returned. </param>
		// Token: 0x170001FC RID: 508
		public PortType this[int index]
		{
			get
			{
				return (PortType)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.PortType" /> to the end of the <see cref="T:System.Web.Services.Description.PortTypeCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="portType" /> parameter has been added.</returns>
		/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> to add to the collection. </param>
		// Token: 0x06000700 RID: 1792 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(PortType portType)
		{
			return base.List.Add(portType);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.PortType" /> to the <see cref="T:System.Web.Services.Description.PortTypeCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="portType" /> parameter. </param>
		/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x06000701 RID: 1793 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, PortType portType)
		{
			base.List.Insert(index, portType);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.PortType" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> for which to search in the collection. </param>
		// Token: 0x06000702 RID: 1794 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(PortType portType)
		{
			return base.List.IndexOf(portType);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.PortType" /> is a member of the <see cref="T:System.Web.Services.Description.PortTypeCollection" />.</summary>
		/// <returns>true if the <paramref name="portType" /> parameter is a member of the <see cref="T:System.Web.Services.Description.PortTypeCollection" />; otherwise, false.</returns>
		/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> for which to check for collection membership. </param>
		// Token: 0x06000703 RID: 1795 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(PortType portType)
		{
			return base.List.Contains(portType);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.PortType" /> from the <see cref="T:System.Web.Services.Description.PortTypeCollection" />.</summary>
		/// <param name="portType">The <see cref="T:System.Web.Services.Description.PortType" /> to remove from the collection. </param>
		// Token: 0x06000704 RID: 1796 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(PortType portType)
		{
			base.List.Remove(portType);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.PortTypeCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.PortType" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.PortType" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x06000705 RID: 1797 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(PortType[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.PortType" /> specified by its name.</summary>
		/// <returns>The name of the <paramref name="value" /> parameter.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.PortType" /> returned. </param>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter cannot be explicitly cast to type <see cref="T:System.Web.Services.Description.PortType" />. </exception>
		// Token: 0x170001FD RID: 509
		public PortType this[string name]
		{
			get
			{
				return (PortType)this.Table[name];
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001D09D File Offset: 0x0001B29D
		protected override string GetKey(object value)
		{
			return ((PortType)value).Name;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001D0AA File Offset: 0x0001B2AA
		protected override void SetParent(object value, object parent)
		{
			((PortType)value).SetParent((ServiceDescription)parent);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00003846 File Offset: 0x00001A46
		internal PortTypeCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
