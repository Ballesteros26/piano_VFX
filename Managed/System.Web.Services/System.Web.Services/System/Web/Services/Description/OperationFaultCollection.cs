using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.OperationFault" /> class. This class cannot be inherited.</summary>
	// Token: 0x0200010A RID: 266
	public sealed class OperationFaultCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal OperationFaultCollection(Operation operation)
			: base(operation)
		{
		}

		/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.OperationFault" /> at the specified zero-based index.</summary>
		/// <returns>An OperationFault.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.OperationFault" /> whose value is modified or returned. </param>
		// Token: 0x17000208 RID: 520
		public OperationFault this[int index]
		{
			get
			{
				return (OperationFault)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationFault" /> to the end of the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="operationFaultMessage" /> parameter has been added.</returns>
		/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> to add to the collection. </param>
		// Token: 0x06000757 RID: 1879 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(OperationFault operationFaultMessage)
		{
			return base.List.Add(operationFaultMessage);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationFault" /> to the <see cref="T:System.Web.Services.Description.OperationFaultCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="operationFaultMessage" /> parameter. </param>
		/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> to add to the collection. </param>
		// Token: 0x06000758 RID: 1880 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, OperationFault operationFaultMessage)
		{
			base.List.Insert(index, operationFaultMessage);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.OperationFault" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> for which to search in the collection. </param>
		// Token: 0x06000759 RID: 1881 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(OperationFault operationFaultMessage)
		{
			return base.List.IndexOf(operationFaultMessage);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.OperationFault" /> is a member of the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />.</summary>
		/// <returns>true if the <paramref name="operationFaultMessage" /> parameter is a member of the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />; otherwise, false.</returns>
		/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> for which to check collection membership. </param>
		// Token: 0x0600075A RID: 1882 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(OperationFault operationFaultMessage)
		{
			return base.List.Contains(operationFaultMessage);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.OperationFault" /> from the <see cref="T:System.Web.Services.Description.OperationFaultCollection" />.</summary>
		/// <param name="operationFaultMessage">The <see cref="T:System.Web.Services.Description.OperationFault" /> to remove from the collection. </param>
		// Token: 0x0600075B RID: 1883 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(OperationFault operationFaultMessage)
		{
			base.List.Remove(operationFaultMessage);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.OperationFaultCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.OperationFault" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.OperationFault" /> serving as the destination of the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x0600075C RID: 1884 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(OperationFault[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Web.Services.Description.OperationFault" /> by its name.</summary>
		/// <returns>An OperationFault.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Services.Description.OperationFault" /> returned. </param>
		// Token: 0x17000209 RID: 521
		public OperationFault this[string name]
		{
			get
			{
				return (OperationFault)this.Table[name];
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001D247 File Offset: 0x0001B447
		protected override string GetKey(object value)
		{
			return ((OperationFault)value).Name;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001D254 File Offset: 0x0001B454
		protected override void SetParent(object value, object parent)
		{
			((OperationFault)value).SetParent((Operation)parent);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00003846 File Offset: 0x00001A46
		internal OperationFaultCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
