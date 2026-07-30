using System;

namespace System.Web.UI
{
	/// <summary>Provides a utility string class that is used by the <see cref="T:System.Web.UI.ObjectStateFormatter" /> class to optimize object graph serialization. This class cannot be inherited.</summary>
	// Token: 0x0200018D RID: 397
	[Serializable]
	public sealed class IndexedString
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IndexedString" /> class. </summary>
		/// <param name="s">The string.</param>
		/// <exception cref="T:System.ArgumentNullException">The string parameter passed to the constructor is null or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06000FAC RID: 4012 RVA: 0x0002B52F File Offset: 0x0002972F
		public IndexedString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentNullException("s");
			}
			this._value = s;
		}

		/// <summary>Gets the string associated with the <see cref="T:System.Web.UI.IndexedString" /> object.</summary>
		/// <returns>An initialized string.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0002B551 File Offset: 0x00029751
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001316 RID: 4886
		private string _value;
	}
}
