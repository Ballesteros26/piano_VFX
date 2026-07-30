using System;
using System.Collections;
using System.Threading;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Forms the basis for the strongly typed collections that are members of the <see cref="N:System.Web.Services.Description" /> namespace.</summary>
	// Token: 0x0200010B RID: 267
	public abstract class ServiceDescriptionBaseCollection : CollectionBase
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0001D267 File Offset: 0x0001B467
		internal ServiceDescriptionBaseCollection(object parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets an interface that implements the association of the keys and values in the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
		/// <returns>An interface that implements the association of the keys and values in the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001D276 File Offset: 0x0001B476
		protected virtual IDictionary Table
		{
			get
			{
				if (this.table == null)
				{
					this.table = new Hashtable();
				}
				return this.table;
			}
		}

		/// <summary>Returns the name of the key associated with the value passed by reference.</summary>
		/// <returns>A null reference.</returns>
		/// <param name="value">An object for which to return the name of the key. </param>
		// Token: 0x06000763 RID: 1891 RVA: 0x00006C2F File Offset: 0x00004E2F
		protected virtual string GetKey(object value)
		{
			return null;
		}

		/// <summary>Sets the parent object of the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" /> instance.</summary>
		/// <param name="value">The object for which to set the parent object. </param>
		/// <param name="parent">The object to set as the parent. </param>
		// Token: 0x06000764 RID: 1892 RVA: 0x0000210D File Offset: 0x0000030D
		protected virtual void SetParent(object value, object parent)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="value" /> parameter. </param>
		/// <param name="value">The element to insert into the collection. </param>
		// Token: 0x06000765 RID: 1893 RVA: 0x0001D291 File Offset: 0x0001B491
		protected override void OnInsertComplete(int index, object value)
		{
			this.AddValue(value);
		}

		/// <summary>Removes an element from the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
		/// <param name="index">The zero-based index of the <paramref name="value" /> parameter to be removed. </param>
		/// <param name="value">The element to remove from the collection. </param>
		// Token: 0x06000766 RID: 1894 RVA: 0x0001D29A File Offset: 0x0001B49A
		protected override void OnRemove(int index, object value)
		{
			this.RemoveValue(value);
		}

		/// <summary>Clears the contents of the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" /> instance.</summary>
		// Token: 0x06000767 RID: 1895 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		protected override void OnClear()
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				this.RemoveValue(base.List[i]);
			}
		}

		/// <summary>Replaces one value with another within the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
		/// <param name="index">The zero-based index where the <paramref name="oldValue" /> parameter can be found. </param>
		/// <param name="oldValue">The object to replace with the <paramref name="newValue" /> parameter. </param>
		/// <param name="newValue">The object that replaces the <paramref name="oldValue" /> parameter. </param>
		// Token: 0x06000768 RID: 1896 RVA: 0x0001D2D9 File Offset: 0x0001B4D9
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.RemoveValue(oldValue);
			this.AddValue(newValue);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		private void AddValue(object value)
		{
			string key = this.GetKey(value);
			if (key != null)
			{
				try
				{
					this.Table.Add(key, value);
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					if (this.Table[key] != null)
					{
						throw new ArgumentException(ServiceDescriptionBaseCollection.GetDuplicateMessage(value.GetType(), key), ex.InnerException);
					}
					throw ex;
				}
			}
			this.SetParent(value, this.parent);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001D374 File Offset: 0x0001B574
		private void RemoveValue(object value)
		{
			string key = this.GetKey(value);
			if (key != null)
			{
				this.Table.Remove(key);
			}
			this.SetParent(value, null);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		private static string GetDuplicateMessage(Type type, string elemName)
		{
			string text;
			if (type == typeof(ServiceDescriptionFormatExtension))
			{
				text = Res.GetString("WebDuplicateFormatExtension", new object[] { elemName });
			}
			else if (type == typeof(OperationMessage))
			{
				text = Res.GetString("WebDuplicateOperationMessage", new object[] { elemName });
			}
			else if (type == typeof(Import))
			{
				text = Res.GetString("WebDuplicateImport", new object[] { elemName });
			}
			else if (type == typeof(Message))
			{
				text = Res.GetString("WebDuplicateMessage", new object[] { elemName });
			}
			else if (type == typeof(Port))
			{
				text = Res.GetString("WebDuplicatePort", new object[] { elemName });
			}
			else if (type == typeof(PortType))
			{
				text = Res.GetString("WebDuplicatePortType", new object[] { elemName });
			}
			else if (type == typeof(Binding))
			{
				text = Res.GetString("WebDuplicateBinding", new object[] { elemName });
			}
			else if (type == typeof(Service))
			{
				text = Res.GetString("WebDuplicateService", new object[] { elemName });
			}
			else if (type == typeof(MessagePart))
			{
				text = Res.GetString("WebDuplicateMessagePart", new object[] { elemName });
			}
			else if (type == typeof(OperationBinding))
			{
				text = Res.GetString("WebDuplicateOperationBinding", new object[] { elemName });
			}
			else if (type == typeof(FaultBinding))
			{
				text = Res.GetString("WebDuplicateFaultBinding", new object[] { elemName });
			}
			else if (type == typeof(Operation))
			{
				text = Res.GetString("WebDuplicateOperation", new object[] { elemName });
			}
			else if (type == typeof(OperationFault))
			{
				text = Res.GetString("WebDuplicateOperationFault", new object[] { elemName });
			}
			else
			{
				text = Res.GetString("WebDuplicateUnknownElement", new object[] { type, elemName });
			}
			return text;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00003846 File Offset: 0x00001A46
		internal ServiceDescriptionBaseCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400041C RID: 1052
		private Hashtable table;

		// Token: 0x0400041D RID: 1053
		private object parent;
	}
}
