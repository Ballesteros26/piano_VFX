using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.FaultBinding" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000108 RID: 264
	public sealed class FaultBindingCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal FaultBindingCollection(OperationBinding operationBinding)
			: base(operationBinding)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.FaultBinding" /> at the specified zero-based index.</summary>
		/// <returns>A FaultBinding.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.FaultBinding" /> whose value is modified or returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x17000205 RID: 517
		public FaultBinding this[int index]
		{
			get
			{
				return (FaultBinding)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> to the end of the <see cref="T:System.Web.Services.Description.FaultBindingCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="bindingOperationFault" /> parameter has been added.</returns>
		/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> to add to the collection. </param>
		// Token: 0x0600073F RID: 1855 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(FaultBinding bindingOperationFault)
		{
			return base.List.Add(bindingOperationFault);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> to the FaultBindingCollection at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="bindingOperationFault" /> parameter. </param>
		/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> to add to the collection. </param>
		// Token: 0x06000740 RID: 1856 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, FaultBinding bindingOperationFault)
		{
			base.List.Insert(index, bindingOperationFault);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> for which to search in the collection. </param>
		// Token: 0x06000741 RID: 1857 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(FaultBinding bindingOperationFault)
		{
			return base.List.IndexOf(bindingOperationFault);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> is a member of the <see cref="T:System.Web.Services.Description.FaultBindingCollection" />.</summary>
		/// <returns>true if the <paramref name="bindingOperationFault" /> parameter is a member of the FaultBindingCollection; otherwise, false.</returns>
		/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> for which to check collection membership. </param>
		// Token: 0x06000742 RID: 1858 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(FaultBinding bindingOperationFault)
		{
			return base.List.Contains(bindingOperationFault);
		}

		/// <summary>Removes the first occurrence the specified <see cref="T:System.Web.Services.Description.FaultBinding" /> from the <see cref="T:System.Web.Services.Description.FaultBindingCollection" />.</summary>
		/// <param name="bindingOperationFault">The <see cref="T:System.Web.Services.Description.FaultBinding" /> to remove from the collection. </param>
		// Token: 0x06000743 RID: 1859 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(FaultBinding bindingOperationFault)
		{
			base.List.Remove(bindingOperationFault);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.FaultBindingCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.FaultBinding" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.FaultBinding" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x06000744 RID: 1860 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(FaultBinding[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.FaultBinding" /> specified by its name.</summary>
		/// <returns>A FaultBinding.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.FaultBinding" /> returned. </param>
		// Token: 0x17000206 RID: 518
		public FaultBinding this[string name]
		{
			get
			{
				return (FaultBinding)this.Table[name];
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001D1DB File Offset: 0x0001B3DB
		protected override string GetKey(object value)
		{
			return ((FaultBinding)value).Name;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		protected override void SetParent(object value, object parent)
		{
			((FaultBinding)value).SetParent((OperationBinding)parent);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00003846 File Offset: 0x00001A46
		internal FaultBindingCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
