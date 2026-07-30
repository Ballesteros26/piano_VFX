using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Form" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019B RID: 411
	public class FormCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets or sets an element in the collection by its numeric index.</summary>
		/// <param name="index">The location of the <see cref="T:System.Windows.Forms.Form" /> within the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066A RID: 1642
		public virtual Form this[int index]
		{
			get
			{
				return (Form)base.InnerList[index];
			}
		}

		/// <summary>Gets or sets an element in the collection by the name of the associated <see cref="T:System.Windows.Forms.Form" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.Form" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066B RID: 1643
		public virtual Form this[string name]
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					Form form = (Form)obj;
					if (form.Name == name)
					{
						return form;
					}
				}
				return null;
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0006962C File Offset: 0x0006782C
		internal void Add(Form form)
		{
			if (base.InnerList.Contains(form))
			{
				return;
			}
			base.InnerList.Add(form);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00069658 File Offset: 0x00067858
		internal void Remove(Form form)
		{
			base.InnerList.Remove(form);
		}
	}
}
