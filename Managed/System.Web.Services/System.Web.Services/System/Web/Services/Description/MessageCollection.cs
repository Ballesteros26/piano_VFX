using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Message" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000101 RID: 257
	public sealed class MessageCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal MessageCollection(ServiceDescription serviceDescription)
			: base(serviceDescription)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.Message" /> at the specified zero-based index.</summary>
		/// <returns>A Message.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Message" /> whose value is modified or returned. </param>
		// Token: 0x170001F8 RID: 504
		public Message this[int index]
		{
			get
			{
				return (Message)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Message" /> to the end of the <see cref="T:System.Web.Services.Description.MessageCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="message" /> parameter has been added.</returns>
		/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> to add to the collection. </param>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(Message message)
		{
			return base.List.Add(message);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Message" /> to the <see cref="T:System.Web.Services.Description.MessageCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="message" /> parameter. </param>
		/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> to add to the collection. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, Message message)
		{
			base.List.Insert(index, message);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Message" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> for which to search in the collection. </param>
		// Token: 0x060006E8 RID: 1768 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(Message message)
		{
			return base.List.IndexOf(message);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Message" /> is a member of the <see cref="T:System.Web.Services.Description.MessageCollection" />.</summary>
		/// <returns>true if the <paramref name="message" /> parameter is a member of the <see cref="T:System.Web.Services.Description.MessageCollection" />; otherwise, false.</returns>
		/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> for which to check collection membership. </param>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(Message message)
		{
			return base.List.Contains(message);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Message" /> from the <see cref="T:System.Web.Services.Description.MessageCollection" />.</summary>
		/// <param name="message">The <see cref="T:System.Web.Services.Description.Message" /> to remove from the collection. </param>
		// Token: 0x060006EA RID: 1770 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(Message message)
		{
			base.List.Remove(message);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MessageCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Message" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Message" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x060006EB RID: 1771 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(Message[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.Message" /> specified by its name.</summary>
		/// <returns>A Message.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.Message" /> returned. </param>
		// Token: 0x170001F9 RID: 505
		public Message this[string name]
		{
			get
			{
				return (Message)this.Table[name];
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001D011 File Offset: 0x0001B211
		protected override string GetKey(object value)
		{
			return ((Message)value).Name;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001D01E File Offset: 0x0001B21E
		protected override void SetParent(object value, object parent)
		{
			((Message)value).SetParent((ServiceDescription)parent);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00003846 File Offset: 0x00001A46
		internal MessageCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
