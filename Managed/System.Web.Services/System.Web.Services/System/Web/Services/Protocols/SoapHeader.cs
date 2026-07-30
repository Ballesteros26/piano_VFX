using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>When overridden in a derived class, represents the content of a SOAP header.</summary>
	// Token: 0x02000067 RID: 103
	[XmlType(IncludeInSchema = false)]
	[SoapType(IncludeInSchema = false)]
	public abstract class SoapHeader
	{
		/// <summary>Gets or sets the value of the mustUnderstand XML attribute for the SOAP header when communicating with SOAP protocol version 1.1.</summary>
		/// <returns>The value of the mustUnderstand attribute. The default is "0".</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value other than: "0", "1", "true", or "false". </exception>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C0B1 File Offset: 0x0000A2B1
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
		[DefaultValue("0")]
		[XmlAttribute("mustUnderstand", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		[SoapAttribute("mustUnderstand", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public string EncodedMustUnderstand
		{
			get
			{
				if (this.version == SoapProtocolVersion.Soap12 || !this.MustUnderstand)
				{
					return "0";
				}
				return "1";
			}
			set
			{
				if (value == "false" || value == "0")
				{
					this.MustUnderstand = false;
					return;
				}
				if (!(value == "true") && !(value == "1"))
				{
					throw new ArgumentException(Res.GetString("WebHeaderInvalidMustUnderstand", new object[] { value }));
				}
				this.MustUnderstand = true;
			}
		}

		/// <summary>Gets or sets the value of the mustUnderstand XML attribute for the SOAP header when communicating with SOAP protocol version 1.2.</summary>
		/// <returns>The value of the mustUnderstand XML attribute of a SOAP header. The default is "0".</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value other than: "0", "1", "true", or "false". </exception>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C13C File Offset: 0x0000A33C
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000C15A File Offset: 0x0000A35A
		[SoapAttribute("mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		[ComVisible(false)]
		[DefaultValue("0")]
		[XmlAttribute("mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		public string EncodedMustUnderstand12
		{
			get
			{
				if (this.version == SoapProtocolVersion.Soap11 || !this.MustUnderstand)
				{
					return "0";
				}
				return "1";
			}
			set
			{
				this.EncodedMustUnderstand = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> must be understood.</summary>
		/// <returns>true if the XML Web service must properly interpret and process the <see cref="T:System.Web.Services.Protocols.SoapHeader" />; otherwise, false. The default is false.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C163 File Offset: 0x0000A363
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000C16B File Offset: 0x0000A36B
		[XmlIgnore]
		[SoapIgnore]
		public bool MustUnderstand
		{
			get
			{
				return this.InternalMustUnderstand;
			}
			set
			{
				this.InternalMustUnderstand = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000C17C File Offset: 0x0000A37C
		internal virtual bool InternalMustUnderstand
		{
			[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
			get
			{
				return this.mustUnderstand;
			}
			[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
			set
			{
				this.mustUnderstand = value;
			}
		}

		/// <summary>Gets or sets the recipient of the SOAP header.</summary>
		/// <returns>The recipient of the SOAP header. The default is an empty string ("").</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000C185 File Offset: 0x0000A385
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000C19C File Offset: 0x0000A39C
		[XmlAttribute("actor", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		[SoapAttribute("actor", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		[DefaultValue("")]
		public string Actor
		{
			get
			{
				if (this.version == SoapProtocolVersion.Soap12)
				{
					return "";
				}
				return this.InternalActor;
			}
			set
			{
				this.InternalActor = value;
			}
		}

		/// <summary>Gets or sets the recipient of the SOAP header.</summary>
		/// <returns>A URI that represents the recipient of the SOAP header. The default is an empty string ("").</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000C1A5 File Offset: 0x0000A3A5
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000C19C File Offset: 0x0000A39C
		[ComVisible(false)]
		[SoapAttribute("role", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		[DefaultValue("")]
		[XmlAttribute("role", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		public string Role
		{
			get
			{
				if (this.version == SoapProtocolVersion.Soap11)
				{
					return "";
				}
				return this.InternalActor;
			}
			set
			{
				this.InternalActor = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
		internal virtual string InternalActor
		{
			[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
			get
			{
				if (this.actor != null)
				{
					return this.actor;
				}
				return string.Empty;
			}
			[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
			set
			{
				this.actor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an XML Web service method properly processed a SOAP header.</summary>
		/// <returns>true if the SOAP header was properly processed; otherwise false.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000C1DB File Offset: 0x0000A3DB
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
		[XmlIgnore]
		[SoapIgnore]
		public bool DidUnderstand
		{
			get
			{
				return this.didUnderstand;
			}
			set
			{
				this.didUnderstand = value;
			}
		}

		/// <summary>Gets or sets the relay attribute of the SOAP 1.2 header.</summary>
		/// <returns>Either "0", "false", "1", or "true".</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000C20C File Offset: 0x0000A40C
		[XmlAttribute("relay", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		[ComVisible(false)]
		[SoapAttribute("relay", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		[DefaultValue("0")]
		public string EncodedRelay
		{
			get
			{
				if (this.version == SoapProtocolVersion.Soap11 || !this.Relay)
				{
					return "0";
				}
				return "1";
			}
			set
			{
				if (value == "false" || value == "0")
				{
					this.Relay = false;
					return;
				}
				if (!(value == "true") && !(value == "1"))
				{
					throw new ArgumentException(Res.GetString("WebHeaderInvalidRelay", new object[] { value }));
				}
				this.Relay = true;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the SOAP header is to be relayed to the next SOAP node if the current node does not understand the header.</summary>
		/// <returns>true if the SOAP header has a "relay" attribute set to "true"; otherwise, false.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000C278 File Offset: 0x0000A478
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000C280 File Offset: 0x0000A480
		[ComVisible(false)]
		[SoapIgnore]
		[XmlIgnore]
		public bool Relay
		{
			get
			{
				return this.InternalRelay;
			}
			set
			{
				this.InternalRelay = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000C289 File Offset: 0x0000A489
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000C291 File Offset: 0x0000A491
		internal virtual bool InternalRelay
		{
			[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
			get
			{
				return this.relay;
			}
			[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
			set
			{
				this.relay = value;
			}
		}

		// Token: 0x0400027F RID: 639
		private string actor;

		// Token: 0x04000280 RID: 640
		private bool mustUnderstand;

		// Token: 0x04000281 RID: 641
		private bool didUnderstand;

		// Token: 0x04000282 RID: 642
		private bool relay;

		// Token: 0x04000283 RID: 643
		internal SoapProtocolVersion version;
	}
}
