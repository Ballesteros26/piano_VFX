using System;
using System.ComponentModel;
using System.Text;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" />.</summary>
	// Token: 0x02000122 RID: 290
	[XmlFormatExtension("body", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(InputBinding), typeof(OutputBinding), typeof(MimePart))]
	public class SoapBodyBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Indicates whether the message parts are encoded using specified encoding rules, or define the concrete schema of the message.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> values. The default is Default.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0003C49F File Offset: 0x0003A69F
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0003C4A7 File Offset: 0x0003A6A7
		[DefaultValue(SoapBindingUse.Default)]
		[XmlAttribute("use")]
		public SoapBindingUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		/// <summary>Get or sets the URI representing the location of the specifications for encoding of content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapBodyBinding.Encoding" /> property.</summary>
		/// <returns>A string containing a URI.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0003C4B0 File Offset: 0x0003A6B0
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0003C4C6 File Offset: 0x0003A6C6
		[XmlAttribute("namespace")]
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				if (this.ns != null)
				{
					return this.ns;
				}
				return string.Empty;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets a string containing a list of space-delimited URIs. The URIs represent the encoding style (or styles) to be used to encode messages within the SOAP body.</summary>
		/// <returns>A string containing a list of URIs. The default value is an empty string ("").</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003C4CF File Offset: 0x0003A6CF
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0003C4E5 File Offset: 0x0003A6E5
		[DefaultValue("")]
		[XmlAttribute("encodingStyle")]
		public string Encoding
		{
			get
			{
				if (this.encoding != null)
				{
					return this.encoding;
				}
				return string.Empty;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating which parts of the transmitted message appear within the SOAP body portion of the transmission.</summary>
		/// <returns>A space-delimited string containing the appropriate message parts.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0003C4F0 File Offset: 0x0003A6F0
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0003C542 File Offset: 0x0003A742
		[XmlAttribute("parts")]
		public string PartsString
		{
			get
			{
				if (this.parts == null)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.parts.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(this.parts[i]);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					this.parts = null;
					return;
				}
				this.parts = value.Split(new char[] { ' ' });
			}
		}

		/// <summary>Gets or sets a value indicating which parts of the transmitted message appear within the SOAP body portion of the transmission.</summary>
		/// <returns>A string array containing the names of the appropriate message parts.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0003C566 File Offset: 0x0003A766
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0003C56E File Offset: 0x0003A76E
		[XmlIgnore]
		public string[] Parts
		{
			get
			{
				return this.parts;
			}
			set
			{
				this.parts = value;
			}
		}

		// Token: 0x04000534 RID: 1332
		private SoapBindingUse use;

		// Token: 0x04000535 RID: 1333
		private string ns;

		// Token: 0x04000536 RID: 1334
		private string encoding;

		// Token: 0x04000537 RID: 1335
		private string[] parts;
	}
}
