using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the result of a data method operation.</summary>
	// Token: 0x020006B5 RID: 1717
	public class ModelDataMethodResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataMethodResult" /> class.</summary>
		/// <param name="returnValue">The return value of the data method.</param>
		/// <param name="outputParameters">A dictionary that contains the values of out and ref parameters.</param>
		// Token: 0x0600487C RID: 18556 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelDataMethodResult(object returnValue, OrderedDictionary outputParameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a read-only dictionary that contains the values of out and ref parameters.</summary>
		/// <returns>A dictionary that contains the values of out and ref parameters.</returns>
		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public OrderedDictionary OutputParameters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the return value of the data method.</summary>
		/// <returns>The return value of the data method: an <see cref="T:System.Collections.IEnumerable" /> collection for a select operation, or an integer indicating the number of affected rows for update, delete, and insert operations.</returns>
		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object ReturnValue
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
