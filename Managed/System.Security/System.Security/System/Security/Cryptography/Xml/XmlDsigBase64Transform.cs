using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the Base64 decoding transform as defined in Section 6.6.2 of the XMLDSIG specification.</summary>
	// Token: 0x0200007C RID: 124
	public class XmlDsigBase64Transform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> class.</summary>
		// Token: 0x06000382 RID: 898 RVA: 0x0000E368 File Offset: 0x0000C568
		public XmlDsigBase64Transform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#base64";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000E3D2 File Offset: 0x0000C5D2
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object. </param>
		// Token: 0x06000385 RID: 901 RVA: 0x00004938 File Offset: 0x00002B38
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x06000386 RID: 902 RVA: 0x00003BEE File Offset: 0x00001DEE
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> and it is null. </exception>
		// Token: 0x06000387 RID: 903 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlNodeList)
			{
				this.LoadXmlNodeListInput((XmlNodeList)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlNodeListInput(((XmlDocument)obj).SelectNodes("//."));
				return;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E43C File Offset: 0x0000C63C
		private void LoadStreamInput(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentException("obj");
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[1024];
			int num;
			do
			{
				num = inputStream.Read(array, 0, 1024);
				if (num > 0)
				{
					int i = 0;
					while (i < num && !char.IsWhiteSpace((char)array[i]))
					{
						i++;
					}
					int num2 = i;
					for (i++; i < num; i++)
					{
						if (!char.IsWhiteSpace((char)array[i]))
						{
							array[num2] = array[i];
							num2++;
						}
					}
					memoryStream.Write(array, 0, num2);
				}
			}
			while (num > 0);
			memoryStream.Position = 0L;
			this._cs = new CryptoStream(memoryStream, new FromBase64Transform(), CryptoStreamMode.Read);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = ((XmlNode)obj).SelectSingleNode("self::text()");
				if (xmlNode != null)
				{
					stringBuilder.Append(xmlNode.OuterXml);
				}
			}
			byte[] bytes = new UTF8Encoding(false).GetBytes(stringBuilder.ToString());
			int i = 0;
			while (i < bytes.Length && !char.IsWhiteSpace((char)bytes[i]))
			{
				i++;
			}
			int num = i;
			for (i++; i < bytes.Length; i++)
			{
				if (!char.IsWhiteSpace((char)bytes[i]))
				{
					bytes[num] = bytes[i];
					num++;
				}
			}
			MemoryStream memoryStream = new MemoryStream(bytes, 0, num);
			this._cs = new CryptoStream(memoryStream, new FromBase64Transform(), CryptoStreamMode.Read);
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		// Token: 0x0600038A RID: 906 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public override object GetOutput()
		{
			return this._cs;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object. </exception>
		// Token: 0x0600038B RID: 907 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return this._cs;
		}

		// Token: 0x040001BC RID: 444
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		// Token: 0x040001BD RID: 445
		private Type[] _outputTypes = new Type[] { typeof(Stream) };

		// Token: 0x040001BE RID: 446
		private CryptoStream _cs;
	}
}
