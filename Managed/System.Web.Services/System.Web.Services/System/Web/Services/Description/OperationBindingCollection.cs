using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.OperationBinding" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000107 RID: 263
	public sealed class OperationBindingCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal OperationBindingCollection(Binding binding)
			: base(binding)
		{
		}

		/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.OperationBinding" /> at the specified zero-based index.</summary>
		/// <returns>An OperationBinding.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.OperationBinding" /> whose value is modified or returned. </param>
		// Token: 0x17000204 RID: 516
		public OperationBinding this[int index]
		{
			get
			{
				return (OperationBinding)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> to the end of the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="bindingOperation" /> parameter has been added.</returns>
		/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> to add to the collection. </param>
		// Token: 0x06000734 RID: 1844 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(OperationBinding bindingOperation)
		{
			return base.List.Add(bindingOperation);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> instance to the <see cref="T:System.Web.Services.Description.OperationBindingCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="bindingOperation" /> parameter. </param>
		/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> to add to the collection. </param>
		// Token: 0x06000735 RID: 1845 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, OperationBinding bindingOperation)
		{
			base.List.Insert(index, bindingOperation);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> for which to search in the collection. </param>
		// Token: 0x06000736 RID: 1846 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(OperationBinding bindingOperation)
		{
			return base.List.IndexOf(bindingOperation);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> is a member of the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />.</summary>
		/// <returns>true if the <paramref name="bindingOperation" /> parameter is a member of the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />; otherwise, false.</returns>
		/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> for which to check collection membership. </param>
		// Token: 0x06000737 RID: 1847 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(OperationBinding bindingOperation)
		{
			return base.List.Contains(bindingOperation);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> from the <see cref="T:System.Web.Services.Description.OperationBindingCollection" />.</summary>
		/// <param name="bindingOperation">The <see cref="T:System.Web.Services.Description.OperationBinding" /> to remove from the collection. </param>
		// Token: 0x06000738 RID: 1848 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(OperationBinding bindingOperation)
		{
			base.List.Remove(bindingOperation);
		}

		/// <summary>Copies the entire OperationBindingCollection to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.OperationBinding" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.OperationBinding" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x06000739 RID: 1849 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(OperationBinding[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001D1A2 File Offset: 0x0001B3A2
		protected override void SetParent(object value, object parent)
		{
			((OperationBinding)value).SetParent((Binding)parent);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00003846 File Offset: 0x00001A46
		internal OperationBindingCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
