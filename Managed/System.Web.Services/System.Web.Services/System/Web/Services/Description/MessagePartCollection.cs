using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.MessagePart" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000106 RID: 262
	public sealed class MessagePartCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal MessagePartCollection(Message message)
			: base(message)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.MessagePart" /> at the specified zero-based index.</summary>
		/// <returns>A MessagePart.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.MessagePart" /> whose value is modified or returned. </param>
		// Token: 0x17000202 RID: 514
		public MessagePart this[int index]
		{
			get
			{
				return (MessagePart)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MessagePart" /> to the end of the <see cref="T:System.Web.Services.Description.MessagePartCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="messagePart" /> parameter has been added.</returns>
		/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> to add to the collection. </param>
		// Token: 0x06000727 RID: 1831 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(MessagePart messagePart)
		{
			return base.List.Add(messagePart);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.MessagePart" /> to the <see cref="T:System.Web.Services.Description.MessagePartCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="messagePart" /> parameter. </param>
		/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> to add to the collection. </param>
		// Token: 0x06000728 RID: 1832 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, MessagePart messagePart)
		{
			base.List.Insert(index, messagePart);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.MessagePart" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> for which to search in the collection. </param>
		// Token: 0x06000729 RID: 1833 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(MessagePart messagePart)
		{
			return base.List.IndexOf(messagePart);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.MessagePart" /> is a member of the MessagePartCollection.</summary>
		/// <returns>true if the <paramref name="messagePart" /> parameter is a member of the <see cref="T:System.Web.Services.Description.MessagePartCollection" />; otherwise, false.</returns>
		/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> for which to check collection membership. </param>
		// Token: 0x0600072A RID: 1834 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(MessagePart messagePart)
		{
			return base.List.Contains(messagePart);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.MessagePart" /> from the <see cref="T:System.Web.Services.Description.MessagePartCollection" />.</summary>
		/// <param name="messagePart">The <see cref="T:System.Web.Services.Description.MessagePart" /> to remove from the collection. </param>
		// Token: 0x0600072B RID: 1835 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(MessagePart messagePart)
		{
			base.List.Remove(messagePart);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.MessagePartCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.MessagePart" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.MessagePart" /> serving as the destination of the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x0600072C RID: 1836 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(MessagePart[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.MessagePart" /> specified by its name.</summary>
		/// <returns>A MessagePart.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.MessagePart" /> returned. </param>
		// Token: 0x17000203 RID: 515
		public MessagePart this[string name]
		{
			get
			{
				return (MessagePart)this.Table[name];
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001D16F File Offset: 0x0001B36F
		protected override string GetKey(object value)
		{
			return ((MessagePart)value).Name;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001D17C File Offset: 0x0001B37C
		protected override void SetParent(object value, object parent)
		{
			((MessagePart)value).SetParent((Message)parent);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00003846 File Offset: 0x00001A46
		internal MessagePartCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
