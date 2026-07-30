using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.Operation" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000109 RID: 265
	public sealed class OperationCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal OperationCollection(PortType portType)
			: base(portType)
		{
		}

		/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.Operation" /> at the specified zero-based index.</summary>
		/// <returns>An Operation.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.Operation" /> whose value is modified or returned. </param>
		// Token: 0x17000207 RID: 519
		public Operation this[int index]
		{
			get
			{
				return (Operation)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Operation" /> to the end of the <see cref="T:System.Web.Services.Description.OperationCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="operation" /> parameter has been added.</returns>
		/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> to add to the collection. </param>
		// Token: 0x0600074C RID: 1868 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(Operation operation)
		{
			return base.List.Add(operation);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.Operation" /> to the <see cref="T:System.Web.Services.Description.OperationCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="operation" /> parameter. </param>
		/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> to add to the collection. </param>
		// Token: 0x0600074D RID: 1869 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, Operation operation)
		{
			base.List.Insert(index, operation);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.Operation" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>A 32-bit signed integer.</returns>
		/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> for which to search in the collection. </param>
		// Token: 0x0600074E RID: 1870 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(Operation operation)
		{
			return base.List.IndexOf(operation);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.Operation" /> is a member of the <see cref="T:System.Web.Services.Description.OperationCollection" />.</summary>
		/// <returns>true if <paramref name="operation" /> is a member of the <see cref="T:System.Web.Services.Description.OperationCollection" />; otherwise, false.</returns>
		/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> for which to check collection membership. </param>
		// Token: 0x0600074F RID: 1871 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(Operation operation)
		{
			return base.List.Contains(operation);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.Operation" /> from the <see cref="T:System.Web.Services.Description.OperationCollection" />.</summary>
		/// <param name="operation">The <see cref="T:System.Web.Services.Description.Operation" /> to remove from the collection. </param>
		// Token: 0x06000750 RID: 1872 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(Operation operation)
		{
			base.List.Remove(operation);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.OperationCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.Operation" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.Operation" /> serving as the destination for the copy action. </param>
		/// <param name="index">The zero-based index at which to start placing the copied collection. </param>
		// Token: 0x06000751 RID: 1873 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(Operation[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001D20E File Offset: 0x0001B40E
		protected override void SetParent(object value, object parent)
		{
			((Operation)value).SetParent((PortType)parent);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00003846 File Offset: 0x00001A46
		internal OperationCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
