using System;
using System.IO;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Defines methods that decrypt an XrML &lt;encryptedGrant&gt; element.</summary>
	// Token: 0x02000061 RID: 97
	public interface IRelDecryptor
	{
		/// <summary>Decrypts an XrML &lt;encryptedGrant&gt; element that is contained within a <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that contains a decrypted &lt;encryptedGrant&gt; element.</returns>
		/// <param name="encryptionMethod">An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that encapsulates the algorithm used for XML encryption.</param>
		/// <param name="keyInfo">A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object that contains an asymmetric key to use for decryption.</param>
		/// <param name="toDecrypt">A stream object that contains an &lt;encryptedGrant&gt; element to decrypt.</param>
		// Token: 0x0600027B RID: 635
		Stream Decrypt(EncryptionMethod encryptionMethod, KeyInfo keyInfo, Stream toDecrypt);
	}
}
