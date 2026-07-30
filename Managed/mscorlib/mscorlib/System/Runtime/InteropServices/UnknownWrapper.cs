using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_UNKNOWN.</summary>
	// Token: 0x0200090A RID: 2314
	[ComVisible(true)]
	[Serializable]
	public sealed class UnknownWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> class with the object to be wrapped.</summary>
		/// <param name="obj">The object being wrapped. </param>
		// Token: 0x060055CB RID: 21963 RVA: 0x00129297 File Offset: 0x00127497
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object contained by this wrapper.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060055CC RID: 21964 RVA: 0x001292A6 File Offset: 0x001274A6
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002D83 RID: 11651
		private object m_WrappedObject;
	}
}
