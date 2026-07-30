using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000092 RID: 146
	public abstract class DiffieHellman : AsymmetricAlgorithm
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x00018983 File Offset: 0x00016B83
		public new static DiffieHellman Create()
		{
			return DiffieHellman.Create("Mono.Security.Cryptography.DiffieHellman");
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001898F File Offset: 0x00016B8F
		public new static DiffieHellman Create(string algName)
		{
			return (DiffieHellman)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06000542 RID: 1346
		public abstract byte[] CreateKeyExchange();

		// Token: 0x06000543 RID: 1347
		public abstract byte[] DecryptKeyExchange(byte[] keyex);

		// Token: 0x06000544 RID: 1348
		public abstract DHParameters ExportParameters(bool includePrivate);

		// Token: 0x06000545 RID: 1349
		public abstract void ImportParameters(DHParameters parameters);

		// Token: 0x06000546 RID: 1350 RVA: 0x0001899C File Offset: 0x00016B9C
		private byte[] GetNamedParam(SecurityElement se, string param)
		{
			SecurityElement securityElement = se.SearchForChildByTag(param);
			if (securityElement == null)
			{
				return null;
			}
			return Convert.FromBase64String(securityElement.Text);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000189C4 File Offset: 0x00016BC4
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DHParameters dhparameters = default(DHParameters);
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xmlString);
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement.Tag != "DHKeyValue")
				{
					throw new CryptographicException();
				}
				dhparameters.P = this.GetNamedParam(securityElement, "P");
				dhparameters.G = this.GetNamedParam(securityElement, "G");
				dhparameters.X = this.GetNamedParam(securityElement, "X");
				this.ImportParameters(dhparameters);
			}
			finally
			{
				if (dhparameters.P != null)
				{
					Array.Clear(dhparameters.P, 0, dhparameters.P.Length);
				}
				if (dhparameters.G != null)
				{
					Array.Clear(dhparameters.G, 0, dhparameters.G.Length);
				}
				if (dhparameters.X != null)
				{
					Array.Clear(dhparameters.X, 0, dhparameters.X.Length);
				}
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DHParameters dhparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<DHKeyValue>");
				stringBuilder.Append("<P>");
				stringBuilder.Append(Convert.ToBase64String(dhparameters.P));
				stringBuilder.Append("</P>");
				stringBuilder.Append("<G>");
				stringBuilder.Append(Convert.ToBase64String(dhparameters.G));
				stringBuilder.Append("</G>");
				if (includePrivateParameters)
				{
					stringBuilder.Append("<X>");
					stringBuilder.Append(Convert.ToBase64String(dhparameters.X));
					stringBuilder.Append("</X>");
				}
				stringBuilder.Append("</DHKeyValue>");
			}
			finally
			{
				Array.Clear(dhparameters.P, 0, dhparameters.P.Length);
				Array.Clear(dhparameters.G, 0, dhparameters.G.Length);
				if (dhparameters.X != null)
				{
					Array.Clear(dhparameters.X, 0, dhparameters.X.Length);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
