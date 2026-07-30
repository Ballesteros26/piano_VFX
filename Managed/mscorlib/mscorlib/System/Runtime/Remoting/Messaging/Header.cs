using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the out-of-band data for a call.</summary>
	// Token: 0x02000807 RID: 2055
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name and value.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		// Token: 0x06005247 RID: 21063 RVA: 0x00122B21 File Offset: 0x00120D21
		public Header(string _Name, object _Value)
			: this(_Name, _Value, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name, value, and additional configuration information.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand the out-of-band data. </param>
		// Token: 0x06005248 RID: 21064 RVA: 0x00122B2C File Offset: 0x00120D2C
		public Header(string _Name, object _Value, bool _MustUnderstand)
			: this(_Name, _Value, _MustUnderstand, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_Value">The object that contains the value of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand out-of-band data. </param>
		/// <param name="_HeaderNamespace">The <see cref="T:System.Runtime.Remoting.Messaging.Header" /> XML namespace. </param>
		// Token: 0x06005249 RID: 21065 RVA: 0x00122B38 File Offset: 0x00120D38
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		/// <summary>Indicates the XML namespace that the current <see cref="T:System.Runtime.Remoting.Messaging.Header" /> belongs to.</summary>
		// Token: 0x04002B05 RID: 11013
		public string HeaderNamespace;

		/// <summary>Indicates whether the receiving end must understand the out-of-band data.</summary>
		// Token: 0x04002B06 RID: 11014
		public bool MustUnderstand;

		/// <summary>Contains the name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x04002B07 RID: 11015
		public string Name;

		/// <summary>Contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x04002B08 RID: 11016
		public object Value;
	}
}
