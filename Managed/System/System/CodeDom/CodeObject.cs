using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a common base class for most Code Document Object Model (CodeDOM) objects.</summary>
	// Token: 0x0200074C RID: 1868
	[Serializable]
	public class CodeObject
	{
		/// <summary>Gets the user-definable data for the current object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing user data for the current object.</returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000D7B44 File Offset: 0x000D5D44
		public IDictionary UserData
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._userData) == null)
				{
					dictionary = (this._userData = new ListDictionary());
				}
				return dictionary;
			}
		}

		// Token: 0x04002D44 RID: 11588
		private IDictionary _userData;
	}
}
