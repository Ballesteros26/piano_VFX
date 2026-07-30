using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the reader.</summary>
	// Token: 0x020000BA RID: 186
	public enum ReadState
	{
		/// <summary>The Read method has not been called.</summary>
		// Token: 0x040003BF RID: 959
		Initial,
		/// <summary>The Read method has been called. Additional methods may be called on the reader.</summary>
		// Token: 0x040003C0 RID: 960
		Interactive,
		/// <summary>An error occurred that prevents the read operation from continuing.</summary>
		// Token: 0x040003C1 RID: 961
		Error,
		/// <summary>The end of the file has been reached successfully.</summary>
		// Token: 0x040003C2 RID: 962
		EndOfFile,
		/// <summary>The <see cref="M:System.Xml.XmlReader.Close" /> method has been called.</summary>
		// Token: 0x040003C3 RID: 963
		Closed
	}
}
