using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Holds the value, <see cref="T:System.Type" />, and name of a serialized object. </summary>
	// Token: 0x020006F0 RID: 1776
	[ComVisible(true)]
	public struct SerializationEntry
	{
		/// <summary>Gets the value contained in the object.</summary>
		/// <returns>The value contained in the object.</returns>
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x0010C132 File Offset: 0x0010A332
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Gets the name of the object.</summary>
		/// <returns>The name of the object.</returns>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x0010C13A File Offset: 0x0010A33A
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object.</returns>
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x0010C142 File Offset: 0x0010A342
		public Type ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x0010C14A File Offset: 0x0010A34A
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this.m_value = entryValue;
			this.m_name = entryName;
			this.m_type = entryType;
		}

		// Token: 0x04002717 RID: 10007
		private Type m_type;

		// Token: 0x04002718 RID: 10008
		private object m_value;

		// Token: 0x04002719 RID: 10009
		private string m_name;
	}
}
