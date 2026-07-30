using System;

namespace System.Xml
{
	/// <summary>Specifies how to handle line breaks.</summary>
	// Token: 0x020000A7 RID: 167
	public enum NewLineHandling
	{
		/// <summary>New line characters are replaced to match the character specified in the <see cref="P:System.Xml.XmlWriterSettings.NewLineChars" />  property.</summary>
		// Token: 0x0400033E RID: 830
		Replace,
		/// <summary>New line characters are entitized. This setting preserves all characters when the output is read by a normalizing <see cref="T:System.Xml.XmlReader" />.</summary>
		// Token: 0x0400033F RID: 831
		Entitize,
		/// <summary>The new line characters are unchanged. The output is the same as the input.</summary>
		// Token: 0x04000340 RID: 832
		None
	}
}
