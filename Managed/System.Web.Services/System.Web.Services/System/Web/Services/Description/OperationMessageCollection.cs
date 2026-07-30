using System;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Services.Description.OperationInput" /> and <see cref="T:System.Web.Services.Description.OperationOutput" /> messages related to an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000FF RID: 255
	public sealed class OperationMessageCollection : ServiceDescriptionBaseCollection
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal OperationMessageCollection(Operation operation)
			: base(operation)
		{
		}

		/// <summary>Gets or sets the value of an <see cref="T:System.Web.Services.Description.OperationMessage" /> at the specified zero-based index.</summary>
		/// <returns>An OperationMessage at the specified zero-based index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.OperationMessage" /> whose value is modified or returned.</param>
		// Token: 0x170001F3 RID: 499
		public OperationMessage this[int index]
		{
			get
			{
				return (OperationMessage)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> to the end of the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
		/// <returns>The zero-based index where the <paramref name="operationMessage" /> parameter has been added.</returns>
		/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> to add to the collection.</param>
		// Token: 0x060006CA RID: 1738 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(OperationMessage operationMessage)
		{
			return base.List.Add(operationMessage);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> to the <see cref="T:System.Web.Services.Description.OperationMessageCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="operationMessage" /> parameter.</param>
		/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> to add to the collection.</param>
		// Token: 0x060006CB RID: 1739 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, OperationMessage operationMessage)
		{
			base.List.Insert(index, operationMessage);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The zero-based index of the specified operation message, or -1 if the element was not found in the collection.</returns>
		/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> for which to search in the collection.</param>
		// Token: 0x060006CC RID: 1740 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(OperationMessage operationMessage)
		{
			return base.List.IndexOf(operationMessage);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> is a member of the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
		/// <returns>true if the <paramref name="operationMessage" /> parameter is a member of the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />; otherwise, false.</returns>
		/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> for which to check collection membership.</param>
		// Token: 0x060006CD RID: 1741 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(OperationMessage operationMessage)
		{
			return base.List.Contains(operationMessage);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.OperationMessage" /> from the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
		/// <param name="operationMessage">The <see cref="T:System.Web.Services.Description.OperationMessage" /> to remove from the collection.</param>
		// Token: 0x060006CE RID: 1742 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(OperationMessage operationMessage)
		{
			base.List.Remove(operationMessage);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.OperationMessageCollection" /> to a compatible one-dimensional array of type <see cref="T:System.Web.Services.Description.OperationMessage" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.OperationMessage" /> serving as the destination for the copy action.</param>
		/// <param name="index">The zero-based index at which to start placing the copied collection.</param>
		// Token: 0x060006CF RID: 1743 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(OperationMessage[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the first occurrence of an <see cref="T:System.Web.Services.Description.OperationInput" /> within the collection.</summary>
		/// <returns>An <see cref="T:System.Web.Services.Description.OperationInput" /> within the collection.</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001CE30 File Offset: 0x0001B030
		public OperationInput Input
		{
			get
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					OperationInput operationInput = base.List[i] as OperationInput;
					if (operationInput != null)
					{
						return operationInput;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the first occurrence of an <see cref="T:System.Web.Services.Description.OperationOutput" /> within the collection.</summary>
		/// <returns>An <see cref="T:System.Web.Services.Description.OperationOutput" /> within the collection.</returns>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001CE6C File Offset: 0x0001B06C
		public OperationOutput Output
		{
			get
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					OperationOutput operationOutput = base.List[i] as OperationOutput;
					if (operationOutput != null)
					{
						return operationOutput;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the type of transmission supported by the <see cref="T:System.Web.Services.Description.OperationMessageCollection" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.OperationFlow" /> values. The default is SolicitResponse.</returns>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
		public OperationFlow Flow
		{
			get
			{
				if (base.List.Count == 0)
				{
					return OperationFlow.None;
				}
				if (base.List.Count == 1)
				{
					if (base.List[0] is OperationInput)
					{
						return OperationFlow.OneWay;
					}
					return OperationFlow.Notification;
				}
				else
				{
					if (base.List[0] is OperationInput)
					{
						return OperationFlow.RequestResponse;
					}
					return OperationFlow.SolicitResponse;
				}
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001CEFF File Offset: 0x0001B0FF
		protected override void SetParent(object value, object parent)
		{
			((OperationMessage)value).SetParent((Operation)parent);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001CF14 File Offset: 0x0001B114
		protected override void OnInsert(int index, object value)
		{
			if (base.Count > 1 || (base.Count == 1 && value.GetType() == base.List[0].GetType()))
			{
				throw new InvalidOperationException(Res.GetString("WebDescriptionTooManyMessages"));
			}
			base.OnInsert(index, value);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001CF69 File Offset: 0x0001B169
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			if (oldValue.GetType() != newValue.GetType())
			{
				throw new InvalidOperationException(Res.GetString("WebDescriptionTooManyMessages"));
			}
			base.OnSet(index, oldValue, newValue);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001CF97 File Offset: 0x0001B197
		protected override void OnValidate(object value)
		{
			if (!(value is OperationInput) && !(value is OperationOutput))
			{
				throw new ArgumentException(Res.GetString("OnlyOperationInputOrOperationOutputTypes"), "value");
			}
			base.OnValidate(value);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00003846 File Offset: 0x00001A46
		internal OperationMessageCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
