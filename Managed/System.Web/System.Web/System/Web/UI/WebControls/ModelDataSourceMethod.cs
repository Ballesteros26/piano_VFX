using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the information about a data method used by a <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object.</summary>
	// Token: 0x020006B3 RID: 1715
	public class ModelDataSourceMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataSourceMethod" /> class.</summary>
		/// <param name="instance">The instance on which the method will be invoked.</param>
		/// <param name="methodInfo">The method to be invoked.</param>
		// Token: 0x06004871 RID: 18545 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelDataSourceMethod(object instance, MethodInfo methodInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the instance on which the method will be invoked.</summary>
		/// <returns>The instance on which the method will be invoked. For static methods, this will be null.</returns>
		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object Instance
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the method to be invoked</summary>
		/// <returns>The method to be invoked.</returns>
		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public MethodInfo MethodInfo
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the method parameter values</summary>
		/// <returns>The method parameter values.</returns>
		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public OrderedDictionary Parameters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
