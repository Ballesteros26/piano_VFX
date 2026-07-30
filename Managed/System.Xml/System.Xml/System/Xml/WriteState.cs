using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the <see cref="T:System.Xml.XmlWriter" />.</summary>
	// Token: 0x020001D9 RID: 473
	public enum WriteState
	{
		/// <summary>Indicates that a Write method has not yet been called.</summary>
		// Token: 0x04000BE8 RID: 3048
		Start,
		/// <summary>Indicates that the prolog is being written.</summary>
		// Token: 0x04000BE9 RID: 3049
		Prolog,
		/// <summary>Indicates that an element start tag is being written.</summary>
		// Token: 0x04000BEA RID: 3050
		Element,
		/// <summary>Indicates that an attribute value is being written.</summary>
		// Token: 0x04000BEB RID: 3051
		Attribute,
		/// <summary>Indicates that element content is being written.</summary>
		// Token: 0x04000BEC RID: 3052
		Content,
		/// <summary>Indicates that the <see cref="M:System.Xml.XmlWriter.Close" /> method has been called.</summary>
		// Token: 0x04000BED RID: 3053
		Closed,
		/// <summary>An exception has been thrown, which has left the <see cref="T:System.Xml.XmlWriter" /> in an invalid state. You can call the <see cref="M:System.Xml.XmlWriter.Close" /> method to put the <see cref="T:System.Xml.XmlWriter" /> in the <see cref="F:System.Xml.WriteState.Closed" /> state. Any other <see cref="T:System.Xml.XmlWriter" /> method calls results in an <see cref="T:System.InvalidOperationException" />.</summary>
		// Token: 0x04000BEE RID: 3054
		Error
	}
}
