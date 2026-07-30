using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Mono.Xml;

namespace System.Security.Cryptography
{
	/// <summary>Accesses the cryptography configuration information.</summary>
	// Token: 0x02000698 RID: 1688
	[ComVisible(true)]
	public class CryptoConfig
	{
		/// <summary>Encodes the specified object identifier (OID).</summary>
		/// <returns>A byte array containing the encoded OID.</returns>
		/// <param name="str">The OID to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">An error occurred while encoding the OID. </exception>
		// Token: 0x0600483F RID: 18495 RVA: 0x00101CA0 File Offset: 0x000FFEA0
		public static byte[] EncodeOID(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] array = new char[] { '.' };
			string[] array2 = str.Split(array);
			if (array2.Length < 2)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID must have at least two parts"));
			}
			byte[] array3 = new byte[str.Length];
			try
			{
				byte b = Convert.ToByte(array2[0]);
				byte b2 = Convert.ToByte(array2[1]);
				array3[2] = Convert.ToByte((int)(b * 40 + b2));
			}
			catch
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Invalid OID"));
			}
			int num = 3;
			for (int i = 2; i < array2.Length; i++)
			{
				long num2 = Convert.ToInt64(array2[i]);
				if (num2 > 127L)
				{
					byte[] array4 = CryptoConfig.EncodeLongNumber(num2);
					Buffer.BlockCopy(array4, 0, array3, num, array4.Length);
					num += array4.Length;
				}
				else
				{
					array3[num++] = Convert.ToByte(num2);
				}
			}
			int num3 = 2;
			byte[] array5 = new byte[num];
			array5[0] = 6;
			if (num > 127)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID > 127 bytes"));
			}
			array5[1] = Convert.ToByte(num - 2);
			Buffer.BlockCopy(array3, num3, array5, num3, num - num3);
			return array5;
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00101DD0 File Offset: 0x000FFFD0
		private static byte[] EncodeLongNumber(long x)
		{
			if (x > 2147483647L || x < -2147483648L)
			{
				throw new OverflowException(Locale.GetText("Part of OID doesn't fit in Int32"));
			}
			long num = x;
			int num2 = 1;
			while (num > 127L)
			{
				num >>= 7;
				num2++;
			}
			byte[] array = new byte[num2];
			for (int i = 0; i < num2; i++)
			{
				num = x >> 7 * i;
				num &= 127L;
				if (i != 0)
				{
					num += 128L;
				}
				array[num2 - i - 1] = Convert.ToByte(num);
			}
			return array;
		}

		/// <summary>Indicates whether the runtime should enforce the policy to create only Federal Information Processing Standard (FIPS) certified algorithms.</summary>
		/// <returns>true to enforce the policy; otherwise, false. </returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoLimitation("nothing is FIPS certified so it never make sense to restrict to this (empty) subset")]
		public static bool AllowOnlyFipsAlgorithms
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00101FE0 File Offset: 0x001001E0
		private static void Initialize()
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("SHA", CryptoConfig.defaultSHA1);
			dictionary.Add("SHA1", CryptoConfig.defaultSHA1);
			dictionary.Add("System.Security.Cryptography.SHA1", CryptoConfig.defaultSHA1);
			dictionary.Add("System.Security.Cryptography.HashAlgorithm", CryptoConfig.defaultSHA1);
			dictionary.Add("MD5", CryptoConfig.defaultMD5);
			dictionary.Add("System.Security.Cryptography.MD5", CryptoConfig.defaultMD5);
			dictionary.Add("SHA256", CryptoConfig.defaultSHA256);
			dictionary.Add("SHA-256", CryptoConfig.defaultSHA256);
			dictionary.Add("System.Security.Cryptography.SHA256", CryptoConfig.defaultSHA256);
			dictionary.Add("SHA384", CryptoConfig.defaultSHA384);
			dictionary.Add("SHA-384", CryptoConfig.defaultSHA384);
			dictionary.Add("System.Security.Cryptography.SHA384", CryptoConfig.defaultSHA384);
			dictionary.Add("SHA512", CryptoConfig.defaultSHA512);
			dictionary.Add("SHA-512", CryptoConfig.defaultSHA512);
			dictionary.Add("System.Security.Cryptography.SHA512", CryptoConfig.defaultSHA512);
			dictionary.Add("RSA", CryptoConfig.defaultRSA);
			dictionary.Add("System.Security.Cryptography.RSA", CryptoConfig.defaultRSA);
			dictionary.Add("System.Security.Cryptography.AsymmetricAlgorithm", CryptoConfig.defaultRSA);
			dictionary.Add("DSA", CryptoConfig.defaultDSA);
			dictionary.Add("System.Security.Cryptography.DSA", CryptoConfig.defaultDSA);
			dictionary.Add("DES", CryptoConfig.defaultDES);
			dictionary.Add("System.Security.Cryptography.DES", CryptoConfig.defaultDES);
			dictionary.Add("3DES", CryptoConfig.default3DES);
			dictionary.Add("TripleDES", CryptoConfig.default3DES);
			dictionary.Add("Triple DES", CryptoConfig.default3DES);
			dictionary.Add("System.Security.Cryptography.TripleDES", CryptoConfig.default3DES);
			dictionary.Add("RC2", CryptoConfig.defaultRC2);
			dictionary.Add("System.Security.Cryptography.RC2", CryptoConfig.defaultRC2);
			dictionary.Add("Rijndael", CryptoConfig.defaultAES);
			dictionary.Add("System.Security.Cryptography.Rijndael", CryptoConfig.defaultAES);
			dictionary.Add("System.Security.Cryptography.SymmetricAlgorithm", CryptoConfig.defaultAES);
			dictionary.Add("RandomNumberGenerator", CryptoConfig.defaultRNG);
			dictionary.Add("System.Security.Cryptography.RandomNumberGenerator", CryptoConfig.defaultRNG);
			dictionary.Add("System.Security.Cryptography.KeyedHashAlgorithm", CryptoConfig.defaultHMAC);
			dictionary.Add("HMACSHA1", CryptoConfig.defaultHMAC);
			dictionary.Add("System.Security.Cryptography.HMACSHA1", CryptoConfig.defaultHMAC);
			dictionary.Add("MACTripleDES", CryptoConfig.defaultMAC3DES);
			dictionary.Add("System.Security.Cryptography.MACTripleDES", CryptoConfig.defaultMAC3DES);
			dictionary.Add("RIPEMD160", CryptoConfig.defaultRIPEMD160);
			dictionary.Add("RIPEMD-160", CryptoConfig.defaultRIPEMD160);
			dictionary.Add("System.Security.Cryptography.RIPEMD160", CryptoConfig.defaultRIPEMD160);
			dictionary.Add("System.Security.Cryptography.HMAC", CryptoConfig.defaultHMAC);
			dictionary.Add("HMACMD5", CryptoConfig.defaultHMACMD5);
			dictionary.Add("System.Security.Cryptography.HMACMD5", CryptoConfig.defaultHMACMD5);
			dictionary.Add("HMACRIPEMD160", CryptoConfig.defaultHMACRIPEMD160);
			dictionary.Add("System.Security.Cryptography.HMACRIPEMD160", CryptoConfig.defaultHMACRIPEMD160);
			dictionary.Add("HMACSHA256", CryptoConfig.defaultHMACSHA256);
			dictionary.Add("System.Security.Cryptography.HMACSHA256", CryptoConfig.defaultHMACSHA256);
			dictionary.Add("HMACSHA384", CryptoConfig.defaultHMACSHA384);
			dictionary.Add("System.Security.Cryptography.HMACSHA384", CryptoConfig.defaultHMACSHA384);
			dictionary.Add("HMACSHA512", CryptoConfig.defaultHMACSHA512);
			dictionary.Add("System.Security.Cryptography.HMACSHA512", CryptoConfig.defaultHMACSHA512);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1", CryptoConfig.defaultDSASigDesc);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1", CryptoConfig.defaultRSAPKCS1SHA1SigDesc);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", CryptoConfig.defaultRSAPKCS1SHA256SigDesc);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", CryptoConfig.defaultRSAPKCS1SHA384SigDesc);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", CryptoConfig.defaultRSAPKCS1SHA512SigDesc);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#sha1", CryptoConfig.defaultSHA1);
			dictionary2.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig#base64", "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/TR/1999/REC-xpath-19991116", "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/TR/1999/REC-xslt-19991116", "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature", "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2001/10/xml-exc-c14n#", "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2002/07/decrypt#XML", "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha256", CryptoConfig.defaultSHA256);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#sha384", CryptoConfig.defaultSHA384);
			dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha512", CryptoConfig.defaultSHA512);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", CryptoConfig.defaultHMACSHA256);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", CryptoConfig.defaultHMACSHA384);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", CryptoConfig.defaultHMACSHA512);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160", CryptoConfig.defaultHMACRIPEMD160);
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# X509Data", "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# KeyName", "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue", "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue", "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# RetrievalMethod", "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("2.5.29.14", "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("2.5.29.15", "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("2.5.29.19", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("2.5.29.37", "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("X509Chain", "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("AES", "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.AesCryptoServiceProvider", "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("AesManaged", "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.AesManaged", "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDH", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDiffieHellman", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDiffieHellmanCng", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.ECDiffieHellmanCng", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDsa", "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDsaCng", "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.ECDsaCng", "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA1Cng", "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA256Cng", "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA256CryptoServiceProvider", "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA384Cng", "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA384CryptoServiceProvider", "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA512Cng", "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA512CryptoServiceProvider", "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			dictionary3.Add("System.Security.Cryptography.SHA1CryptoServiceProvider", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.SHA1Managed", "1.3.14.3.2.26");
			dictionary3.Add("SHA1", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.SHA1", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.SHA1Cng", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.MD5CryptoServiceProvider", "1.2.840.113549.2.5");
			dictionary3.Add("MD5", "1.2.840.113549.2.5");
			dictionary3.Add("System.Security.Cryptography.MD5", "1.2.840.113549.2.5");
			dictionary3.Add("System.Security.Cryptography.SHA256Managed", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("SHA256", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA256", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA256Cng", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA256CryptoServiceProvider", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA384Managed", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("SHA384", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA384", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA384Cng", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA384CryptoServiceProvider", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA512Managed", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("SHA512", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.SHA512", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.SHA512Cng", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.SHA512CryptoServiceProvider", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.RIPEMD160Managed", "1.3.36.3.2.1");
			dictionary3.Add("RIPEMD160", "1.3.36.3.2.1");
			dictionary3.Add("System.Security.Cryptography.RIPEMD160", "1.3.36.3.2.1");
			dictionary3.Add("TripleDESKeyWrap", "1.2.840.113549.1.9.16.3.6");
			dictionary3.Add("DES", "1.3.14.3.2.7");
			dictionary3.Add("TripleDES", "1.2.840.113549.3.7");
			dictionary3.Add("RC2", "1.2.840.113549.3.2");
			CryptoConfig.LoadConfig(Environment.GetMachineConfigPath(), dictionary, dictionary3);
			CryptoConfig.algorithms = dictionary;
			CryptoConfig.unresolved_algorithms = dictionary2;
			CryptoConfig.oids = dictionary3;
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0010286C File Offset: 0x00100A6C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void LoadConfig(string filename, IDictionary<string, Type> algorithms, IDictionary<string, string> oid)
		{
			if (!File.Exists(filename))
			{
				return;
			}
			try
			{
				using (TextReader textReader = new StreamReader(filename))
				{
					CryptoConfig.CryptoHandler cryptoHandler = new CryptoConfig.CryptoHandler(algorithms, oid);
					new SmallXmlParser().Parse(textReader, cryptoHandler);
				}
			}
			catch
			{
			}
		}

		/// <summary>Creates a new instance of the specified cryptographic object.</summary>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06004845 RID: 18501 RVA: 0x001028CC File Offset: 0x00100ACC
		public static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name, null);
		}

		/// <summary>Creates a new instance of the specified cryptographic object with the specified arguments.</summary>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance. </param>
		/// <param name="args">The arguments used to create the specified cryptographic object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06004846 RID: 18502 RVA: 0x001028D8 File Offset: 0x00100AD8
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public static object CreateFromName(string name, params object[] args)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.algorithms == null)
				{
					CryptoConfig.Initialize();
				}
			}
			try
			{
				Type type = null;
				if (!CryptoConfig.algorithms.TryGetValue(name, out type))
				{
					string text = null;
					if (!CryptoConfig.unresolved_algorithms.TryGetValue(name, out text))
					{
						text = name;
					}
					type = Type.GetType(text);
				}
				if (type == null)
				{
					obj = null;
				}
				else
				{
					obj = Activator.CreateInstance(type, args);
				}
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00102980 File Offset: 0x00100B80
		internal static string MapNameToOID(string name, OidGroup oidGroup)
		{
			return CryptoConfig.MapNameToOID(name);
		}

		/// <summary>Gets the object identifier (OID) of the algorithm corresponding to the specified simple name.</summary>
		/// <returns>The OID of the specified algorithm.</returns>
		/// <param name="name">The simple name of the algorithm for which to get the OID. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06004848 RID: 18504 RVA: 0x00102988 File Offset: 0x00100B88
		public static string MapNameToOID(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.oids == null)
				{
					CryptoConfig.Initialize();
				}
			}
			string text = null;
			CryptoConfig.oids.TryGetValue(name, out text);
			return text;
		}

		/// <summary>Adds a set of names to algorithm mappings to be used for the current application domain.  </summary>
		/// <param name="algorithm">The algorithm to map to.</param>
		/// <param name="names">An array of names to map to the algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" algorithm" /> or <paramref name="names" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="algorithm" /> cannot be accessed from outside the assembly.-or-One of the entries in the <paramref name="names" /> parameter is empty or null.</exception>
		// Token: 0x06004849 RID: 18505 RVA: 0x001029EC File Offset: 0x00100BEC
		public static void AddAlgorithm(Type algorithm, params string[] names)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			foreach (string text in names)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArithmeticException("names");
				}
				CryptoConfig.algorithms[text] = algorithm;
			}
		}

		/// <summary>Adds a set of names to object identifier (OID) mappings to be used for the current application domain.  </summary>
		/// <param name="oid">The object identifier (OID) to map to.</param>
		/// <param name="names">An array of names to map to the OID.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" oid" /> or <paramref name="names" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the entries in the <paramref name="names" /> parameter is empty or null.</exception>
		// Token: 0x0600484A RID: 18506 RVA: 0x00102A50 File Offset: 0x00100C50
		public static void AddOID(string oid, params string[] names)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			foreach (string text in names)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArithmeticException("names");
				}
				CryptoConfig.oids[oid] = text;
			}
		}

		// Token: 0x0400251F RID: 9503
		private static object lockObject = new object();

		// Token: 0x04002520 RID: 9504
		private static Dictionary<string, Type> algorithms;

		// Token: 0x04002521 RID: 9505
		private static Dictionary<string, string> unresolved_algorithms;

		// Token: 0x04002522 RID: 9506
		private static Dictionary<string, string> oids;

		// Token: 0x04002523 RID: 9507
		private const string defaultNamespace = "System.Security.Cryptography.";

		// Token: 0x04002524 RID: 9508
		private static Type defaultSHA1 = typeof(SHA1CryptoServiceProvider);

		// Token: 0x04002525 RID: 9509
		private static Type defaultMD5 = typeof(MD5CryptoServiceProvider);

		// Token: 0x04002526 RID: 9510
		private static Type defaultSHA256 = typeof(SHA256Managed);

		// Token: 0x04002527 RID: 9511
		private static Type defaultSHA384 = typeof(SHA384Managed);

		// Token: 0x04002528 RID: 9512
		private static Type defaultSHA512 = typeof(SHA512Managed);

		// Token: 0x04002529 RID: 9513
		private static Type defaultRSA = typeof(RSACryptoServiceProvider);

		// Token: 0x0400252A RID: 9514
		private static Type defaultDSA = typeof(DSACryptoServiceProvider);

		// Token: 0x0400252B RID: 9515
		private static Type defaultDES = typeof(DESCryptoServiceProvider);

		// Token: 0x0400252C RID: 9516
		private static Type default3DES = typeof(TripleDESCryptoServiceProvider);

		// Token: 0x0400252D RID: 9517
		private static Type defaultRC2 = typeof(RC2CryptoServiceProvider);

		// Token: 0x0400252E RID: 9518
		private static Type defaultAES = typeof(RijndaelManaged);

		// Token: 0x0400252F RID: 9519
		private static Type defaultRNG = typeof(RNGCryptoServiceProvider);

		// Token: 0x04002530 RID: 9520
		private static Type defaultHMAC = typeof(HMACSHA1);

		// Token: 0x04002531 RID: 9521
		private static Type defaultMAC3DES = typeof(MACTripleDES);

		// Token: 0x04002532 RID: 9522
		private static Type defaultDSASigDesc = typeof(DSASignatureDescription);

		// Token: 0x04002533 RID: 9523
		private static Type defaultRSAPKCS1SHA1SigDesc = typeof(RSAPKCS1SHA1SignatureDescription);

		// Token: 0x04002534 RID: 9524
		private static Type defaultRSAPKCS1SHA256SigDesc = typeof(RSAPKCS1SHA256SignatureDescription);

		// Token: 0x04002535 RID: 9525
		private static Type defaultRSAPKCS1SHA384SigDesc = typeof(RSAPKCS1SHA384SignatureDescription);

		// Token: 0x04002536 RID: 9526
		private static Type defaultRSAPKCS1SHA512SigDesc = typeof(RSAPKCS1SHA512SignatureDescription);

		// Token: 0x04002537 RID: 9527
		private static Type defaultRIPEMD160 = typeof(RIPEMD160Managed);

		// Token: 0x04002538 RID: 9528
		private static Type defaultHMACMD5 = typeof(HMACMD5);

		// Token: 0x04002539 RID: 9529
		private static Type defaultHMACRIPEMD160 = typeof(HMACRIPEMD160);

		// Token: 0x0400253A RID: 9530
		private static Type defaultHMACSHA256 = typeof(HMACSHA256);

		// Token: 0x0400253B RID: 9531
		private static Type defaultHMACSHA384 = typeof(HMACSHA384);

		// Token: 0x0400253C RID: 9532
		private static Type defaultHMACSHA512 = typeof(HMACSHA512);

		// Token: 0x0400253D RID: 9533
		private const string defaultC14N = "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400253E RID: 9534
		private const string defaultC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400253F RID: 9535
		private const string defaultBase64 = "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002540 RID: 9536
		private const string defaultXPath = "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002541 RID: 9537
		private const string defaultXslt = "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002542 RID: 9538
		private const string defaultEnveloped = "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002543 RID: 9539
		private const string defaultXmlDecryption = "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002544 RID: 9540
		private const string defaultExcC14N = "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002545 RID: 9541
		private const string defaultExcC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002546 RID: 9542
		private const string defaultX509Data = "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002547 RID: 9543
		private const string defaultKeyName = "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002548 RID: 9544
		private const string defaultKeyValueDSA = "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002549 RID: 9545
		private const string defaultKeyValueRSA = "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400254A RID: 9546
		private const string defaultRetrievalMethod = "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400254B RID: 9547
		private const string managedSHA1 = "System.Security.Cryptography.SHA1Managed";

		// Token: 0x0400254C RID: 9548
		private const string oidSHA1 = "1.3.14.3.2.26";

		// Token: 0x0400254D RID: 9549
		private const string oidMD5 = "1.2.840.113549.2.5";

		// Token: 0x0400254E RID: 9550
		private const string oidSHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x0400254F RID: 9551
		private const string oidSHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x04002550 RID: 9552
		private const string oidSHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x04002551 RID: 9553
		private const string oidRIPEMD160 = "1.3.36.3.2.1";

		// Token: 0x04002552 RID: 9554
		private const string oidDES = "1.3.14.3.2.7";

		// Token: 0x04002553 RID: 9555
		private const string oid3DES = "1.2.840.113549.3.7";

		// Token: 0x04002554 RID: 9556
		private const string oidRC2 = "1.2.840.113549.3.2";

		// Token: 0x04002555 RID: 9557
		private const string oid3DESKeyWrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04002556 RID: 9558
		private const string nameSHA1 = "System.Security.Cryptography.SHA1CryptoServiceProvider";

		// Token: 0x04002557 RID: 9559
		private const string nameSHA1a = "SHA";

		// Token: 0x04002558 RID: 9560
		private const string nameSHA1b = "SHA1";

		// Token: 0x04002559 RID: 9561
		private const string nameSHA1c = "System.Security.Cryptography.SHA1";

		// Token: 0x0400255A RID: 9562
		private const string nameSHA1d = "System.Security.Cryptography.HashAlgorithm";

		// Token: 0x0400255B RID: 9563
		private const string nameMD5 = "System.Security.Cryptography.MD5CryptoServiceProvider";

		// Token: 0x0400255C RID: 9564
		private const string nameMD5a = "MD5";

		// Token: 0x0400255D RID: 9565
		private const string nameMD5b = "System.Security.Cryptography.MD5";

		// Token: 0x0400255E RID: 9566
		private const string nameSHA256 = "System.Security.Cryptography.SHA256Managed";

		// Token: 0x0400255F RID: 9567
		private const string nameSHA256a = "SHA256";

		// Token: 0x04002560 RID: 9568
		private const string nameSHA256b = "SHA-256";

		// Token: 0x04002561 RID: 9569
		private const string nameSHA256c = "System.Security.Cryptography.SHA256";

		// Token: 0x04002562 RID: 9570
		private const string nameSHA384 = "System.Security.Cryptography.SHA384Managed";

		// Token: 0x04002563 RID: 9571
		private const string nameSHA384a = "SHA384";

		// Token: 0x04002564 RID: 9572
		private const string nameSHA384b = "SHA-384";

		// Token: 0x04002565 RID: 9573
		private const string nameSHA384c = "System.Security.Cryptography.SHA384";

		// Token: 0x04002566 RID: 9574
		private const string nameSHA512 = "System.Security.Cryptography.SHA512Managed";

		// Token: 0x04002567 RID: 9575
		private const string nameSHA512a = "SHA512";

		// Token: 0x04002568 RID: 9576
		private const string nameSHA512b = "SHA-512";

		// Token: 0x04002569 RID: 9577
		private const string nameSHA512c = "System.Security.Cryptography.SHA512";

		// Token: 0x0400256A RID: 9578
		private const string nameRSAa = "RSA";

		// Token: 0x0400256B RID: 9579
		private const string nameRSAb = "System.Security.Cryptography.RSA";

		// Token: 0x0400256C RID: 9580
		private const string nameRSAc = "System.Security.Cryptography.AsymmetricAlgorithm";

		// Token: 0x0400256D RID: 9581
		private const string nameDSAa = "DSA";

		// Token: 0x0400256E RID: 9582
		private const string nameDSAb = "System.Security.Cryptography.DSA";

		// Token: 0x0400256F RID: 9583
		private const string nameDESa = "DES";

		// Token: 0x04002570 RID: 9584
		private const string nameDESb = "System.Security.Cryptography.DES";

		// Token: 0x04002571 RID: 9585
		private const string name3DESa = "3DES";

		// Token: 0x04002572 RID: 9586
		private const string name3DESb = "TripleDES";

		// Token: 0x04002573 RID: 9587
		private const string name3DESc = "Triple DES";

		// Token: 0x04002574 RID: 9588
		private const string name3DESd = "System.Security.Cryptography.TripleDES";

		// Token: 0x04002575 RID: 9589
		private const string nameRC2a = "RC2";

		// Token: 0x04002576 RID: 9590
		private const string nameRC2b = "System.Security.Cryptography.RC2";

		// Token: 0x04002577 RID: 9591
		private const string nameAESa = "Rijndael";

		// Token: 0x04002578 RID: 9592
		private const string nameAESb = "System.Security.Cryptography.Rijndael";

		// Token: 0x04002579 RID: 9593
		private const string nameAESc = "System.Security.Cryptography.SymmetricAlgorithm";

		// Token: 0x0400257A RID: 9594
		private const string nameRNGa = "RandomNumberGenerator";

		// Token: 0x0400257B RID: 9595
		private const string nameRNGb = "System.Security.Cryptography.RandomNumberGenerator";

		// Token: 0x0400257C RID: 9596
		private const string nameKeyHasha = "System.Security.Cryptography.KeyedHashAlgorithm";

		// Token: 0x0400257D RID: 9597
		private const string nameHMACSHA1a = "HMACSHA1";

		// Token: 0x0400257E RID: 9598
		private const string nameHMACSHA1b = "System.Security.Cryptography.HMACSHA1";

		// Token: 0x0400257F RID: 9599
		private const string nameMAC3DESa = "MACTripleDES";

		// Token: 0x04002580 RID: 9600
		private const string nameMAC3DESb = "System.Security.Cryptography.MACTripleDES";

		// Token: 0x04002581 RID: 9601
		private const string name3DESKeyWrap = "TripleDESKeyWrap";

		// Token: 0x04002582 RID: 9602
		private const string nameRIPEMD160 = "System.Security.Cryptography.RIPEMD160Managed";

		// Token: 0x04002583 RID: 9603
		private const string nameRIPEMD160a = "RIPEMD160";

		// Token: 0x04002584 RID: 9604
		private const string nameRIPEMD160b = "RIPEMD-160";

		// Token: 0x04002585 RID: 9605
		private const string nameRIPEMD160c = "System.Security.Cryptography.RIPEMD160";

		// Token: 0x04002586 RID: 9606
		private const string nameHMACb = "System.Security.Cryptography.HMAC";

		// Token: 0x04002587 RID: 9607
		private const string nameHMACMD5a = "HMACMD5";

		// Token: 0x04002588 RID: 9608
		private const string nameHMACMD5b = "System.Security.Cryptography.HMACMD5";

		// Token: 0x04002589 RID: 9609
		private const string nameHMACRIPEMD160a = "HMACRIPEMD160";

		// Token: 0x0400258A RID: 9610
		private const string nameHMACRIPEMD160b = "System.Security.Cryptography.HMACRIPEMD160";

		// Token: 0x0400258B RID: 9611
		private const string nameHMACSHA256a = "HMACSHA256";

		// Token: 0x0400258C RID: 9612
		private const string nameHMACSHA256b = "System.Security.Cryptography.HMACSHA256";

		// Token: 0x0400258D RID: 9613
		private const string nameHMACSHA384a = "HMACSHA384";

		// Token: 0x0400258E RID: 9614
		private const string nameHMACSHA384b = "System.Security.Cryptography.HMACSHA384";

		// Token: 0x0400258F RID: 9615
		private const string nameHMACSHA512a = "HMACSHA512";

		// Token: 0x04002590 RID: 9616
		private const string nameHMACSHA512b = "System.Security.Cryptography.HMACSHA512";

		// Token: 0x04002591 RID: 9617
		private const string urlXmlDsig = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04002592 RID: 9618
		private const string urlDSASHA1 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x04002593 RID: 9619
		private const string urlRSASHA1 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x04002594 RID: 9620
		private const string urlRSASHA256 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04002595 RID: 9621
		private const string urlRSASHA384 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		// Token: 0x04002596 RID: 9622
		private const string urlRSASHA512 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		// Token: 0x04002597 RID: 9623
		private const string urlSHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04002598 RID: 9624
		private const string urlC14N = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04002599 RID: 9625
		private const string urlC14NWithComments = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x0400259A RID: 9626
		private const string urlBase64 = "http://www.w3.org/2000/09/xmldsig#base64";

		// Token: 0x0400259B RID: 9627
		private const string urlXPath = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x0400259C RID: 9628
		private const string urlXslt = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		// Token: 0x0400259D RID: 9629
		private const string urlEnveloped = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x0400259E RID: 9630
		private const string urlXmlDecryption = "http://www.w3.org/2002/07/decrypt#XML";

		// Token: 0x0400259F RID: 9631
		private const string urlExcC14NWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x040025A0 RID: 9632
		private const string urlExcC14N = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x040025A1 RID: 9633
		private const string urlSHA256 = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040025A2 RID: 9634
		private const string urlSHA384 = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		// Token: 0x040025A3 RID: 9635
		private const string urlSHA512 = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x040025A4 RID: 9636
		private const string urlHMACSHA256 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x040025A5 RID: 9637
		private const string urlHMACSHA384 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x040025A6 RID: 9638
		private const string urlHMACSHA512 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x040025A7 RID: 9639
		private const string urlHMACRIPEMD160 = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		// Token: 0x040025A8 RID: 9640
		private const string urlX509Data = "http://www.w3.org/2000/09/xmldsig# X509Data";

		// Token: 0x040025A9 RID: 9641
		private const string urlKeyName = "http://www.w3.org/2000/09/xmldsig# KeyName";

		// Token: 0x040025AA RID: 9642
		private const string urlKeyValueDSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue";

		// Token: 0x040025AB RID: 9643
		private const string urlKeyValueRSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue";

		// Token: 0x040025AC RID: 9644
		private const string urlRetrievalMethod = "http://www.w3.org/2000/09/xmldsig# RetrievalMethod";

		// Token: 0x040025AD RID: 9645
		private const string oidX509SubjectKeyIdentifier = "2.5.29.14";

		// Token: 0x040025AE RID: 9646
		private const string oidX509KeyUsage = "2.5.29.15";

		// Token: 0x040025AF RID: 9647
		private const string oidX509BasicConstraints = "2.5.29.19";

		// Token: 0x040025B0 RID: 9648
		private const string oidX509EnhancedKeyUsage = "2.5.29.37";

		// Token: 0x040025B1 RID: 9649
		private const string nameX509SubjectKeyIdentifier = "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025B2 RID: 9650
		private const string nameX509KeyUsage = "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025B3 RID: 9651
		private const string nameX509BasicConstraints = "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025B4 RID: 9652
		private const string nameX509EnhancedKeyUsage = "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025B5 RID: 9653
		private const string nameX509Chain = "X509Chain";

		// Token: 0x040025B6 RID: 9654
		private const string defaultX509Chain = "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025B7 RID: 9655
		private const string system_core_assembly = ", System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025B8 RID: 9656
		private const string nameAES_1 = "AES";

		// Token: 0x040025B9 RID: 9657
		private const string nameAES_2 = "System.Security.Cryptography.AesCryptoServiceProvider";

		// Token: 0x040025BA RID: 9658
		private const string defaultAES_1 = "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025BB RID: 9659
		private const string nameAESManaged_1 = "AesManaged";

		// Token: 0x040025BC RID: 9660
		private const string nameAESManaged_2 = "System.Security.Cryptography.AesManaged";

		// Token: 0x040025BD RID: 9661
		private const string defaultAESManaged = "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025BE RID: 9662
		private const string nameECDiffieHellman_1 = "ECDH";

		// Token: 0x040025BF RID: 9663
		private const string nameECDiffieHellman_2 = "ECDiffieHellman";

		// Token: 0x040025C0 RID: 9664
		private const string nameECDiffieHellman_3 = "ECDiffieHellmanCng";

		// Token: 0x040025C1 RID: 9665
		private const string nameECDiffieHellman_4 = "System.Security.Cryptography.ECDiffieHellmanCng";

		// Token: 0x040025C2 RID: 9666
		private const string defaultECDiffieHellman = "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025C3 RID: 9667
		private const string nameECDsa_1 = "ECDsa";

		// Token: 0x040025C4 RID: 9668
		private const string nameECDsa_2 = "ECDsaCng";

		// Token: 0x040025C5 RID: 9669
		private const string nameECDsa_3 = "System.Security.Cryptography.ECDsaCng";

		// Token: 0x040025C6 RID: 9670
		private const string defaultECDsa = "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025C7 RID: 9671
		private const string nameSHA1Cng = "System.Security.Cryptography.SHA1Cng";

		// Token: 0x040025C8 RID: 9672
		private const string defaultSHA1Cng = "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025C9 RID: 9673
		private const string nameSHA256Cng = "System.Security.Cryptography.SHA256Cng";

		// Token: 0x040025CA RID: 9674
		private const string defaultSHA256Cng = "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025CB RID: 9675
		private const string nameSHA256Provider = "System.Security.Cryptography.SHA256CryptoServiceProvider";

		// Token: 0x040025CC RID: 9676
		private const string defaultSHA256Provider = "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025CD RID: 9677
		private const string nameSHA384Cng = "System.Security.Cryptography.SHA384Cng";

		// Token: 0x040025CE RID: 9678
		private const string defaultSHA384Cng = "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025CF RID: 9679
		private const string nameSHA384Provider = "System.Security.Cryptography.SHA384CryptoServiceProvider";

		// Token: 0x040025D0 RID: 9680
		private const string defaultSHA384Provider = "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025D1 RID: 9681
		private const string nameSHA512Cng = "System.Security.Cryptography.SHA512Cng";

		// Token: 0x040025D2 RID: 9682
		private const string defaultSHA512Cng = "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040025D3 RID: 9683
		private const string nameSHA512Provider = "System.Security.Cryptography.SHA512CryptoServiceProvider";

		// Token: 0x040025D4 RID: 9684
		private const string defaultSHA512Provider = "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x02000699 RID: 1689
		private class CryptoHandler : SmallXmlParser.IContentHandler
		{
			// Token: 0x0600484C RID: 18508 RVA: 0x00102AAC File Offset: 0x00100CAC
			public CryptoHandler(IDictionary<string, Type> algorithms, IDictionary<string, string> oid)
			{
				this.algorithms = algorithms;
				this.oid = oid;
				this.names = new Dictionary<string, string>();
				this.classnames = new Dictionary<string, string>();
			}

			// Token: 0x0600484D RID: 18509 RVA: 0x00002194 File Offset: 0x00000394
			public void OnStartParsing(SmallXmlParser parser)
			{
			}

			// Token: 0x0600484E RID: 18510 RVA: 0x00102AD8 File Offset: 0x00100CD8
			public void OnEndParsing(SmallXmlParser parser)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.names)
				{
					try
					{
						this.algorithms[keyValuePair.Key] = Type.GetType(this.classnames[keyValuePair.Value]);
					}
					catch
					{
					}
				}
				this.names.Clear();
				this.classnames.Clear();
			}

			// Token: 0x0600484F RID: 18511 RVA: 0x00102B74 File Offset: 0x00100D74
			private string Get(SmallXmlParser.IAttrList attrs, string name)
			{
				for (int i = 0; i < attrs.Names.Length; i++)
				{
					if (attrs.Names[i] == name)
					{
						return attrs.Values[i];
					}
				}
				return string.Empty;
			}

			// Token: 0x06004850 RID: 18512 RVA: 0x00102BB4 File Offset: 0x00100DB4
			public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
			{
				switch (this.level)
				{
				case 0:
					if (name == "configuration")
					{
						this.level++;
						return;
					}
					break;
				case 1:
					if (name == "mscorlib")
					{
						this.level++;
						return;
					}
					break;
				case 2:
					if (name == "cryptographySettings")
					{
						this.level++;
						return;
					}
					break;
				case 3:
					if (name == "oidMap")
					{
						this.level++;
						return;
					}
					if (name == "cryptoNameMapping")
					{
						this.level++;
						return;
					}
					break;
				case 4:
					if (name == "oidEntry")
					{
						this.oid[this.Get(attrs, "name")] = this.Get(attrs, "OID");
						return;
					}
					if (name == "nameEntry")
					{
						this.names[this.Get(attrs, "name")] = this.Get(attrs, "class");
						return;
					}
					if (name == "cryptoClasses")
					{
						this.level++;
						return;
					}
					break;
				case 5:
					if (name == "cryptoClass")
					{
						this.classnames[attrs.Names[0]] = attrs.Values[0];
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x06004851 RID: 18513 RVA: 0x00102D28 File Offset: 0x00100F28
			public void OnEndElement(string name)
			{
				switch (this.level)
				{
				case 1:
					if (name == "configuration")
					{
						this.level--;
						return;
					}
					break;
				case 2:
					if (name == "mscorlib")
					{
						this.level--;
						return;
					}
					break;
				case 3:
					if (name == "cryptographySettings")
					{
						this.level--;
						return;
					}
					break;
				case 4:
					if (name == "oidMap" || name == "cryptoNameMapping")
					{
						this.level--;
						return;
					}
					break;
				case 5:
					if (name == "cryptoClasses")
					{
						this.level--;
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x06004852 RID: 18514 RVA: 0x00002194 File Offset: 0x00000394
			public void OnProcessingInstruction(string name, string text)
			{
			}

			// Token: 0x06004853 RID: 18515 RVA: 0x00002194 File Offset: 0x00000394
			public void OnChars(string text)
			{
			}

			// Token: 0x06004854 RID: 18516 RVA: 0x00002194 File Offset: 0x00000394
			public void OnIgnorableWhitespace(string text)
			{
			}

			// Token: 0x040025D5 RID: 9685
			private IDictionary<string, Type> algorithms;

			// Token: 0x040025D6 RID: 9686
			private IDictionary<string, string> oid;

			// Token: 0x040025D7 RID: 9687
			private Dictionary<string, string> names;

			// Token: 0x040025D8 RID: 9688
			private Dictionary<string, string> classnames;

			// Token: 0x040025D9 RID: 9689
			private int level;
		}
	}
}
