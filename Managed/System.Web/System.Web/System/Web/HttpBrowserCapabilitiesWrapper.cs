using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that enables the server to gather information about the capabilities of the browser that has made the current request.</summary>
	// Token: 0x02000035 RID: 53
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpBrowserCapabilitiesWrapper : HttpBrowserCapabilitiesBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpBrowserCapabilitiesWrapper" /> class. </summary>
		/// <param name="httpBrowserCapabilities">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpBrowserCapabilities" /> is null.</exception>
		// Token: 0x060001D2 RID: 466 RVA: 0x000067D1 File Offset: 0x000049D1
		public HttpBrowserCapabilitiesWrapper(HttpBrowserCapabilities httpBrowserCapabilities)
		{
			if (httpBrowserCapabilities == null)
			{
				throw new ArgumentNullException("httpBrowserCapabilities");
			}
			this._browser = httpBrowserCapabilities;
		}

		/// <summary>Gets the browser string (if any) that was sent by the browser in the User-Agent request header.</summary>
		/// <returns>The contents of the User-Agent request header sent by the browser.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000067EE File Offset: 0x000049EE
		public override string Browser
		{
			get
			{
				return this._browser.Browser;
			}
		}

		/// <summary>Gets the version number of ECMAScript (JavaScript) that the browser supports.</summary>
		/// <returns>The version number of ECMAScript (JavaScript) that the browser supports.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000067FB File Offset: 0x000049FB
		public override Version EcmaScriptVersion
		{
			get
			{
				return this._browser.EcmaScriptVersion;
			}
		}

		/// <summary>Gets the JScript version that the browser supports.</summary>
		/// <returns>The version of JScript that the browser supports.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006808 File Offset: 0x00004A08
		public override Version JScriptVersion
		{
			get
			{
				return this._browser.JScriptVersion;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports callback scripts.</summary>
		/// <returns>true if the browser supports callback scripts; otherwise, false. The default is false.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006815 File Offset: 0x00004A15
		public override bool SupportsCallback
		{
			get
			{
				return this._browser.SupportsCallback;
			}
		}

		/// <summary>Gets the version of the World Wide Web Consortium (W3C) XML Document Object Model (DOM) that the browser supports.</summary>
		/// <returns>The number of the W3C XML DOM version number that the browser supports.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006822 File Offset: 0x00004A22
		public override Version W3CDomVersion
		{
			get
			{
				return this._browser.W3CDomVersion;
			}
		}

		/// <summary>Gets a value that indicates whether the browser is capable of supporting ActiveX controls.</summary>
		/// <returns>true if the browser can support ActiveX controls; otherwise, false. The default is false.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000682F File Offset: 0x00004A2F
		public override bool ActiveXControls
		{
			get
			{
				return this._browser.ActiveXControls;
			}
		}

		/// <summary>Gets the collection of available control adapters.</summary>
		/// <returns>The collection of registered control adapters for the browser.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000683C File Offset: 0x00004A3C
		public override IDictionary Adapters
		{
			get
			{
				return this._browser.Adapters;
			}
		}

		/// <summary>Gets a value that indicates whether the client is an America Online (AOL) browser.</summary>
		/// <returns>true if the browser is an AOL browser; otherwise, false. The default is false.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006849 File Offset: 0x00004A49
		public override bool AOL
		{
			get
			{
				return this._browser.AOL;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports playing background sounds by using the bgsounds HTML element.</summary>
		/// <returns>true if the browser supports playing background sounds; otherwise, false. The default is false.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006856 File Offset: 0x00004A56
		public override bool BackgroundSounds
		{
			get
			{
				return this._browser.BackgroundSounds;
			}
		}

		/// <summary>Gets a value that indicates whether the browser is a beta version.</summary>
		/// <returns>true if the browser is a beta version; otherwise, false. The default is false.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006863 File Offset: 0x00004A63
		public override bool Beta
		{
			get
			{
				return this._browser.Beta;
			}
		}

		/// <summary>Gets a collection of browsers for which capabilities are recognized.</summary>
		/// <returns>The browsers for which capabilities are recognized.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006870 File Offset: 0x00004A70
		public override ArrayList Browsers
		{
			get
			{
				return this._browser.Browsers;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports decks that contain multiple forms, such as separate cards.</summary>
		/// <returns>true if the browser supports decks that contain multiple forms; otherwise, false. The default is true.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000687D File Offset: 0x00004A7D
		public override bool CanCombineFormsInDeck
		{
			get
			{
				return this._browser.CanCombineFormsInDeck;
			}
		}

		/// <summary>Gets a value that indicates whether the browser device is capable of initiating a voice call.</summary>
		/// <returns>true if the browser device is capable of initiating a voice call; otherwise, false. The default is false.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000688A File Offset: 0x00004A8A
		public override bool CanInitiateVoiceCall
		{
			get
			{
				return this._browser.CanInitiateVoiceCall;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports page content that follows WML select or input elements.</summary>
		/// <returns>true if the browser supports page content that follows HTML select or input elements; otherwise, false. The default is true.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006897 File Offset: 0x00004A97
		public override bool CanRenderAfterInputOrSelectElement
		{
			get
			{
				return this._browser.CanRenderAfterInputOrSelectElement;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports empty HTML select elements.</summary>
		/// <returns>true if the browser supports empty HTML select elements; otherwise, false. The default is true.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000068A4 File Offset: 0x00004AA4
		public override bool CanRenderEmptySelects
		{
			get
			{
				return this._browser.CanRenderEmptySelects;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports WML input and select elements together in the same card.</summary>
		/// <returns>true if the browser supports WML input and select elements together; otherwise, false. The default is false.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000068B1 File Offset: 0x00004AB1
		public override bool CanRenderInputAndSelectElementsTogether
		{
			get
			{
				return this._browser.CanRenderInputAndSelectElementsTogether;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports WML option elements that specify both onpick and value attributes.</summary>
		/// <returns>true if the browser supports WML option elements that specify both onpick and value attributes; otherwise, false. The default is true.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000068BE File Offset: 0x00004ABE
		public override bool CanRenderMixedSelects
		{
			get
			{
				return this._browser.CanRenderMixedSelects;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports WML onevent and prev elements in the same card.</summary>
		/// <returns>true if the browser supports WML onevent and prev elements in the same card; otherwise, false. The default is true.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000068CB File Offset: 0x00004ACB
		public override bool CanRenderOneventAndPrevElementsTogether
		{
			get
			{
				return this._browser.CanRenderOneventAndPrevElementsTogether;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports WML cards for postback.</summary>
		/// <returns>true if the browser supports WML cards for postback; otherwise, false. The default is true.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000068D8 File Offset: 0x00004AD8
		public override bool CanRenderPostBackCards
		{
			get
			{
				return this._browser.CanRenderPostBackCards;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports WML setvar elements that have a value attribute of 0.</summary>
		/// <returns>true if the browser supports WML setvar elements that have a value attribute of 0; otherwise, false. The default is true.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000068E5 File Offset: 0x00004AE5
		public override bool CanRenderSetvarZeroWithMultiSelectionList
		{
			get
			{
				return this._browser.CanRenderSetvarZeroWithMultiSelectionList;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports sending e-mail messages by using the HTML mailto scheme.</summary>
		/// <returns>true if the browser supports sending e-mail message by using the HTML mailto scheme; otherwise, false. The default is true.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000068F2 File Offset: 0x00004AF2
		public override bool CanSendMail
		{
			get
			{
				return this._browser.CanSendMail;
			}
		}

		/// <summary>Used internally to get the defined capabilities of the browser.</summary>
		/// <returns>The defined capabilities of the browser.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000068FF File Offset: 0x00004AFF
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000690C File Offset: 0x00004B0C
		public override IDictionary Capabilities
		{
			get
			{
				return this._browser.Capabilities;
			}
			set
			{
				this._browser.Capabilities = value;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports Channel Definition Format (CDF) for webcasting.</summary>
		/// <returns>true if the browser supports CDF; otherwise, false. The default is false.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000691A File Offset: 0x00004B1A
		public override bool CDF
		{
			get
			{
				return this._browser.CDF;
			}
		}

		/// <summary>Gets the version of the .NET Framework that is installed on the client.</summary>
		/// <returns>The common language runtime (CLRS) version.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006927 File Offset: 0x00004B27
		public override Version ClrVersion
		{
			get
			{
				return this._browser.ClrVersion;
			}
		}

		/// <summary>Gets a value that indicates whether the browser is capable of supporting cookies.</summary>
		/// <returns>true if the browser can support cookies; otherwise, false. The default is false.NoteThis property does not indicate whether cookies are currently enabled in the browser, only whether the browser can support cookies.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006934 File Offset: 0x00004B34
		public override bool Cookies
		{
			get
			{
				return this._browser.Cookies;
			}
		}

		/// <summary>Gets a value that indicates whether the browser is a search-engine Web crawler.</summary>
		/// <returns>true if the browser is a search-engine crawler; otherwise, false. The default is false.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006941 File Offset: 0x00004B41
		public override bool Crawler
		{
			get
			{
				return this._browser.Crawler;
			}
		}

		/// <summary>Gets the maximum number of submit buttons that are allowed for a form.</summary>
		/// <returns>The maximum number of submit buttons that are allowed for a form.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000694E File Offset: 0x00004B4E
		public override int DefaultSubmitButtonLimit
		{
			get
			{
				return this._browser.DefaultSubmitButtonLimit;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports HTML frames.</summary>
		/// <returns>true if the browser supports frames; otherwise, false. The default is false.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000695B File Offset: 0x00004B5B
		public override bool Frames
		{
			get
			{
				return this._browser.Frames;
			}
		}

		/// <summary>Gets the major version number of the wireless gateway that is used to access the server, if known. </summary>
		/// <returns>The major version number of the wireless gateway that is used to access the server, if known. The default is 0.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006968 File Offset: 0x00004B68
		public override int GatewayMajorVersion
		{
			get
			{
				return this._browser.GatewayMajorVersion;
			}
		}

		/// <summary>Gets the minor version number of the wireless gateway that is used to access the server, if known. </summary>
		/// <returns>The minor version number of the wireless gateway that is used to access the server, if known. The default is 0.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006975 File Offset: 0x00004B75
		public override double GatewayMinorVersion
		{
			get
			{
				return this._browser.GatewayMinorVersion;
			}
		}

		/// <summary>Gets the version of the wireless gateway that is used to access the server, if known.</summary>
		/// <returns>The version number of the wireless gateway that is used to access the server, if known. The default is "None".</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006982 File Offset: 0x00004B82
		public override string GatewayVersion
		{
			get
			{
				return this._browser.GatewayVersion;
			}
		}

		/// <summary>Gets a value that indicates whether the browser has a dedicated Back button.</summary>
		/// <returns>true if the browser has a dedicated Back button; otherwise, false. The default is true.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000698F File Offset: 0x00004B8F
		public override bool HasBackButton
		{
			get
			{
				return this._browser.HasBackButton;
			}
		}

		/// <summary>Gets a value that indicates whether the scrollbar of an HTML select multiple element that has an align attribute value of right is obscured upon rendering.</summary>
		/// <returns>true if the scrollbar of an HTML select multiple element that has an align attribute value of right is obscured upon rendering; otherwise, false. The default is false.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000699C File Offset: 0x00004B9C
		public override bool HidesRightAlignedMultiselectScrollbars
		{
			get
			{
				return this._browser.HidesRightAlignedMultiselectScrollbars;
			}
		}

		/// <summary>Gets or sets the fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</summary>
		/// <returns>The fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000069A9 File Offset: 0x00004BA9
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000069B6 File Offset: 0x00004BB6
		public override string HtmlTextWriter
		{
			get
			{
				return this._browser.HtmlTextWriter;
			}
			set
			{
				this._browser.HtmlTextWriter = value;
			}
		}

		/// <summary>Gets the internal identifier of the browser as specified in the browser definition file.</summary>
		/// <returns>The internal identifier of the browser as specified in the browser definition file.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000069C4 File Offset: 0x00004BC4
		public override string Id
		{
			get
			{
				return this._browser.Id;
			}
		}

		/// <summary>Gets the type of input that is supported by the browser.</summary>
		/// <returns>The type of input supported by the browser. The default is "telephoneKeypad".</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000069D1 File Offset: 0x00004BD1
		public override string InputType
		{
			get
			{
				return this._browser.InputType;
			}
		}

		/// <summary>Gets a value that indicates whether the browser has a color display.</summary>
		/// <returns>true if the browser has a color display; otherwise, false. The default is false.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000069DE File Offset: 0x00004BDE
		public override bool IsColor
		{
			get
			{
				return this._browser.IsColor;
			}
		}

		/// <summary>Gets a value that indicates whether the browser is a recognized mobile device.</summary>
		/// <returns>true if the browser is a recognized mobile device; otherwise, false. The default is true.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000069EB File Offset: 0x00004BEB
		public override bool IsMobileDevice
		{
			get
			{
				return this._browser.IsMobileDevice;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports Java.</summary>
		/// <returns>true if the browser supports Java; otherwise, false. The default is false.NoteThis property does not indicate whether Java is currently enabled in the browser, only whether the browser can support Java.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000069F8 File Offset: 0x00004BF8
		public override bool JavaApplets
		{
			get
			{
				return this._browser.JavaApplets;
			}
		}

		/// <summary>Gets the major (integer) version number of the browser.</summary>
		/// <returns>The major version number of the browser.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006A05 File Offset: 0x00004C05
		public override int MajorVersion
		{
			get
			{
				return this._browser.MajorVersion;
			}
		}

		/// <summary>Gets the maximum length in characters for the href attribute of an HTML a (anchor) element.</summary>
		/// <returns>The maximum length in characters for the href attribute of an HTML a (anchor) element. The default value is the value in the <see cref="P:System.Web.HttpBrowserCapabilitiesWrapper.Item(System.String)" /> property with the key name of "maximumHrefLength".</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006A12 File Offset: 0x00004C12
		public override int MaximumHrefLength
		{
			get
			{
				return this._browser.MaximumHrefLength;
			}
		}

		/// <summary>Gets the maximum length of the page, in bytes, that the browser can display. </summary>
		/// <returns>The maximum length of the page, in bytes, that the browser can display. The default is 2000.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006A1F File Offset: 0x00004C1F
		public override int MaximumRenderedPageSize
		{
			get
			{
				return this._browser.MaximumRenderedPageSize;
			}
		}

		/// <summary>Gets the maximum length of the text that a soft-key label can display.</summary>
		/// <returns>The maximum length of the text that a soft-key label can display. The default is 5.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006A2C File Offset: 0x00004C2C
		public override int MaximumSoftkeyLabelLength
		{
			get
			{
				return this._browser.MaximumSoftkeyLabelLength;
			}
		}

		/// <summary>Gets the minor (decimal) version number of the browser.</summary>
		/// <returns>The minor version number of the browser.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00006A39 File Offset: 0x00004C39
		public override double MinorVersion
		{
			get
			{
				return this._browser.MinorVersion;
			}
		}

		/// <summary>Gets the minor (decimal) version number of the browser as a string.</summary>
		/// <returns>A string that represents the minor version number of the browser.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00006A46 File Offset: 0x00004C46
		public override string MinorVersionString
		{
			get
			{
				return this._browser.MinorVersionString;
			}
		}

		/// <summary>Gets the name of the manufacturer of a mobile device, if known.</summary>
		/// <returns>The name of the manufacturer of a mobile device, if known. The default is "Unknown".</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00006A53 File Offset: 0x00004C53
		public override string MobileDeviceManufacturer
		{
			get
			{
				return this._browser.MobileDeviceManufacturer;
			}
		}

		/// <summary>Gets the model name of a mobile device, if known.</summary>
		/// <returns>The model name of a mobile device, if known. The default is "Unknown".</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006A60 File Offset: 0x00004C60
		public override string MobileDeviceModel
		{
			get
			{
				return this._browser.MobileDeviceModel;
			}
		}

		/// <summary>Gets the version of the Microsoft HTML (MSHTML) Document Object Model (DOM) that the browser supports.</summary>
		/// <returns>The MSHTML DOM version that the browser supports. The default is 0.0.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006A6D File Offset: 0x00004C6D
		public override Version MSDomVersion
		{
			get
			{
				return this._browser.MSDomVersion;
			}
		}

		/// <summary>Gets the number of softkeys on a mobile device.</summary>
		/// <returns>The number of softkeys supported on a mobile device. The default is 0.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006A7A File Offset: 0x00004C7A
		public override int NumberOfSoftkeys
		{
			get
			{
				return this._browser.NumberOfSoftkeys;
			}
		}

		/// <summary>Gets the name of the operating system that the client is using, if known.</summary>
		/// <returns>The operating system that the client is using, if known, otherwise the value is set to "Unknown".</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006A87 File Offset: 0x00004C87
		public override string Platform
		{
			get
			{
				return this._browser.Platform;
			}
		}

		/// <summary>Gets the MIME type of the type of image content that the browser typically prefers.</summary>
		/// <returns>The MIME type of the type of image content that the browser typically prefers. The default is "image/gif".</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006A94 File Offset: 0x00004C94
		public override string PreferredImageMime
		{
			get
			{
				return this._browser.PreferredImageMime;
			}
		}

		/// <summary>Gets the MIME type of the type of content that the browser typically prefers.</summary>
		/// <returns>The MIME type of the type of content that the browser typically prefers. The default is "text/html".</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006AA1 File Offset: 0x00004CA1
		public override string PreferredRenderingMime
		{
			get
			{
				return this._browser.PreferredRenderingMime;
			}
		}

		/// <summary>Gets the general name for the type of content that the browser prefers.</summary>
		/// <returns>The values "html32" or "chtml10". The default is "html32".</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006AAE File Offset: 0x00004CAE
		public override string PreferredRenderingType
		{
			get
			{
				return this._browser.PreferredRenderingType;
			}
		}

		/// <summary>Gets the request encoding that the browser prefers.</summary>
		/// <returns>The request encoding preferred by the browser.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006ABB File Offset: 0x00004CBB
		public override string PreferredRequestEncoding
		{
			get
			{
				return this._browser.PreferredRequestEncoding;
			}
		}

		/// <summary>Gets the response encoding that the browser prefers.</summary>
		/// <returns>The response encoding preferred by the browser.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public override string PreferredResponseEncoding
		{
			get
			{
				return this._browser.PreferredResponseEncoding;
			}
		}

		/// <summary>Gets a value that indicates whether the browser renders a line break before select or input elements.</summary>
		/// <returns>true if the browser renders a line break before select or input elements; otherwise, false. The default is false.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006AD5 File Offset: 0x00004CD5
		public override bool RendersBreakBeforeWmlSelectAndInput
		{
			get
			{
				return this._browser.RendersBreakBeforeWmlSelectAndInput;
			}
		}

		/// <summary>Gets a value that indicates whether the browser renders a line break after list-item elements.</summary>
		/// <returns>true if the browser renders a line break after list-item elements; otherwise, false. The default is true.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00006AE2 File Offset: 0x00004CE2
		public override bool RendersBreaksAfterHtmlLists
		{
			get
			{
				return this._browser.RendersBreaksAfterHtmlLists;
			}
		}

		/// <summary>Gets a value that indicates whether the browser renders a line break after a standalone WML a (anchor) element.</summary>
		/// <returns>true if the browser renders a line break after a standalone WML a (anchor) element; otherwise, false. The default is false.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006AEF File Offset: 0x00004CEF
		public override bool RendersBreaksAfterWmlAnchor
		{
			get
			{
				return this._browser.RendersBreaksAfterWmlAnchor;
			}
		}

		/// <summary>Gets a value that indicates whether the browser renders a line break after a WML input element.</summary>
		/// <returns>true if the browser renders a line break after a WML input element; otherwise, false. The default is false.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00006AFC File Offset: 0x00004CFC
		public override bool RendersBreaksAfterWmlInput
		{
			get
			{
				return this._browser.RendersBreaksAfterWmlInput;
			}
		}

		/// <summary>Gets a value that indicates whether the mobile-device browser renders a WML do form accept construct as an inline button instead of as a softkey.</summary>
		/// <returns>true if the mobile-device browser renders a WML do form-accept construct as an inline button; otherwise, false. The default is true.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00006B09 File Offset: 0x00004D09
		public override bool RendersWmlDoAcceptsInline
		{
			get
			{
				return this._browser.RendersWmlDoAcceptsInline;
			}
		}

		/// <summary>Gets a value that indicates whether the browser renders WML select elements as menu cards, instead of as a combo box.</summary>
		/// <returns>true if the browser renders WML select elements as menu cards; otherwise, false. The default is false.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00006B16 File Offset: 0x00004D16
		public override bool RendersWmlSelectsAsMenuCards
		{
			get
			{
				return this._browser.RendersWmlSelectsAsMenuCards;
			}
		}

		/// <summary>Used internally to produce a meta-tag that is required by some browsers.</summary>
		/// <returns>A meta-tag that is required by some browsers.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006B23 File Offset: 0x00004D23
		public override string RequiredMetaTagNameValue
		{
			get
			{
				return this._browser.RequiredMetaTagNameValue;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires colons in element attribute values to be replaced with a different character.</summary>
		/// <returns>true if the browser requires colons in element attribute values to be replaced with a different character; otherwise, false. The default is false.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00006B30 File Offset: 0x00004D30
		public override bool RequiresAttributeColonSubstitution
		{
			get
			{
				return this._browser.RequiresAttributeColonSubstitution;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires an HTML meta element for which the content-type attribute is specified.</summary>
		/// <returns>true if the browser requires an HTML meta element for which the content-type attribute is specified; otherwise, false. The default is false.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006B3D File Offset: 0x00004D3D
		public override bool RequiresContentTypeMetaTag
		{
			get
			{
				return this._browser.RequiresContentTypeMetaTag;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires control state to be maintained in sessions.</summary>
		/// <returns>true if the browser requires control state to be maintained in sessions; otherwise, false. The default is false.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00006B4A File Offset: 0x00004D4A
		public override bool RequiresControlStateInSession
		{
			get
			{
				return this._browser.RequiresControlStateInSession;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires a double-byte character set.</summary>
		/// <returns>true if the browser requires a double-byte character set; otherwise, false. The default is false.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006B57 File Offset: 0x00004D57
		public override bool RequiresDBCSCharacter
		{
			get
			{
				return this._browser.RequiresDBCSCharacter;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires nonstandard error messages.</summary>
		/// <returns>true if the browser requires nonstandard error messages; otherwise, false. The default is false.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006B64 File Offset: 0x00004D64
		public override bool RequiresHtmlAdaptiveErrorReporting
		{
			get
			{
				return this._browser.RequiresHtmlAdaptiveErrorReporting;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires the first element in the body of a Web page to be an HTML br element.</summary>
		/// <returns>true if the browser requires the first element in the body of a Web page to be an HTML br element; otherwise, false. The default is false.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006B71 File Offset: 0x00004D71
		public override bool RequiresLeadingPageBreak
		{
			get
			{
				return this._browser.RequiresLeadingPageBreak;
			}
		}

		/// <summary>Gets a value that indicates whether the browser does not support HTML br elements to format line breaks.</summary>
		/// <returns>true if the browser does not support HTML br elements; otherwise, false. The default is false.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00006B7E File Offset: 0x00004D7E
		public override bool RequiresNoBreakInFormatting
		{
			get
			{
				return this._browser.RequiresNoBreakInFormatting;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires pages to contain a size-optimized form of markup language tags.</summary>
		/// <returns>true if the browser requires pages to contain a size-optimized form of markup language tags; otherwise, false. The default is false.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006B8B File Offset: 0x00004D8B
		public override bool RequiresOutputOptimization
		{
			get
			{
				return this._browser.RequiresOutputOptimization;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports telephone dialing based on plain text, or whether it requires special markup.</summary>
		/// <returns>true if the browser supports telephone dialing based on plain text; otherwise, false. The default is false.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006B98 File Offset: 0x00004D98
		public override bool RequiresPhoneNumbersAsPlainText
		{
			get
			{
				return this._browser.RequiresPhoneNumbersAsPlainText;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires view-state values to be specially encoded.</summary>
		/// <returns>true if the browser requires view-state values to be specially encoded; otherwise, false. The default is false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006BA5 File Offset: 0x00004DA5
		public override bool RequiresSpecialViewStateEncoding
		{
			get
			{
				return this._browser.RequiresSpecialViewStateEncoding;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires unique form-action URLs.</summary>
		/// <returns>true if the browser requires unique form-action URLs; otherwise, false. The default is false.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006BB2 File Offset: 0x00004DB2
		public override bool RequiresUniqueFilePathSuffix
		{
			get
			{
				return this._browser.RequiresUniqueFilePathSuffix;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires unique name attribute values for multiple HTML input type="checkbox" elements.</summary>
		/// <returns>true if the browser requires unique name attribute values for multiple HTML input type="checkbox" elements; otherwise, false. The default is false.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006BBF File Offset: 0x00004DBF
		public override bool RequiresUniqueHtmlCheckboxNames
		{
			get
			{
				return this._browser.RequiresUniqueHtmlCheckboxNames;
			}
		}

		/// <summary>Gets a value that indicates whether the browser requires unique name attribute values for multiple HTML input elements.</summary>
		/// <returns>true if the browser requires unique name attribute values for multiple HTML input elements; otherwise, false. The default is false.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006BCC File Offset: 0x00004DCC
		public override bool RequiresUniqueHtmlInputNames
		{
			get
			{
				return this._browser.RequiresUniqueHtmlInputNames;
			}
		}

		/// <summary>Gets a value that indicates whether postback data that is sent by the browser will be URL-encoded.</summary>
		/// <returns>true if postback data that is sent by the browser will be URL-encoded; otherwise, false. The default is false.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006BD9 File Offset: 0x00004DD9
		public override bool RequiresUrlEncodedPostfieldValues
		{
			get
			{
				return this._browser.RequiresUrlEncodedPostfieldValues;
			}
		}

		/// <summary>Gets the depth of the display, in bits per pixel.</summary>
		/// <returns>The depth of the display, in bits per pixel. The default is 1.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00006BE6 File Offset: 0x00004DE6
		public override int ScreenBitDepth
		{
			get
			{
				return this._browser.ScreenBitDepth;
			}
		}

		/// <summary>Gets the approximate height of the display, in character lines.</summary>
		/// <returns>The approximate height of the display, in character lines. The default is 6.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006BF3 File Offset: 0x00004DF3
		public override int ScreenCharactersHeight
		{
			get
			{
				return this._browser.ScreenCharactersHeight;
			}
		}

		/// <summary>Gets the approximate width of the display, in characters.</summary>
		/// <returns>The approximate width of the display, in characters. The default is 12.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00006C00 File Offset: 0x00004E00
		public override int ScreenCharactersWidth
		{
			get
			{
				return this._browser.ScreenCharactersWidth;
			}
		}

		/// <summary>Gets the approximate height of the display, in pixels.</summary>
		/// <returns>The approximate height of the display, in pixels. The default is 72.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006C0D File Offset: 0x00004E0D
		public override int ScreenPixelsHeight
		{
			get
			{
				return this._browser.ScreenPixelsHeight;
			}
		}

		/// <summary>Gets the approximate width of the display, in pixels.</summary>
		/// <returns>The approximate width of the display, in pixels. The default is 96.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006C1A File Offset: 0x00004E1A
		public override int ScreenPixelsWidth
		{
			get
			{
				return this._browser.ScreenPixelsWidth;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the accesskey attribute of HTML a (anchor) and input elements.</summary>
		/// <returns>true if the browser supports the accesskey attribute of HTML a (anchor) and input elements; otherwise, false. The default is false.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006C27 File Offset: 0x00004E27
		public override bool SupportsAccesskeyAttribute
		{
			get
			{
				return this._browser.SupportsAccesskeyAttribute;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the bgcolor attribute of the HTML body element.</summary>
		/// <returns>true if the browser supports the bgcolor attribute of the HTML body element; otherwise, false. The default is true.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006C34 File Offset: 0x00004E34
		public override bool SupportsBodyColor
		{
			get
			{
				return this._browser.SupportsBodyColor;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports HTML b elements to format bold text.</summary>
		/// <returns>true if the browser supports HTML b elements to format bold text; otherwise, false. The default is false.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006C41 File Offset: 0x00004E41
		public override bool SupportsBold
		{
			get
			{
				return this._browser.SupportsBold;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the cache-control value for the http-equiv attribute of HTML meta elements.</summary>
		/// <returns>true if the browser supports the cache-control value for the http-equiv attribute of HTML meta elements; otherwise, false. The default is true.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00006C4E File Offset: 0x00004E4E
		public override bool SupportsCacheControlMetaTag
		{
			get
			{
				return this._browser.SupportsCacheControlMetaTag;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports cascading style sheets (CSS).</summary>
		/// <returns>true if the browser supports CSS; otherwise, false. The default is false.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006C5B File Offset: 0x00004E5B
		public override bool SupportsCss
		{
			get
			{
				return this._browser.SupportsCss;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the align attribute of HTML div elements.</summary>
		/// <returns>true if the browser supports the align attribute of HTML div elements; otherwise, false. The default is true.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006C68 File Offset: 0x00004E68
		public override bool SupportsDivAlign
		{
			get
			{
				return this._browser.SupportsDivAlign;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the nowrap attribute of HTML div elements.</summary>
		/// <returns>true if the browser supports the nowrap HTML div elements; otherwise, false. The default is false.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006C75 File Offset: 0x00004E75
		public override bool SupportsDivNoWrap
		{
			get
			{
				return this._browser.SupportsDivNoWrap;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports empty strings in cookie values.</summary>
		/// <returns>true if the browser supports empty strings in cookie values; otherwise, false. The default is false.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00006C82 File Offset: 0x00004E82
		public override bool SupportsEmptyStringInCookieValue
		{
			get
			{
				return this._browser.SupportsEmptyStringInCookieValue;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the color attribute of HTML font elements.</summary>
		/// <returns>true if the browser supports the color attribute of HTML font elements; otherwise, false. The default is true.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006C8F File Offset: 0x00004E8F
		public override bool SupportsFontColor
		{
			get
			{
				return this._browser.SupportsFontColor;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the name attribute of HTML font elements.</summary>
		/// <returns>true if the browser supports the name attribute of HTML font elements; otherwise, false. The default is false.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00006C9C File Offset: 0x00004E9C
		public override bool SupportsFontName
		{
			get
			{
				return this._browser.SupportsFontName;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the size attribute of HTML font elements.</summary>
		/// <returns>true if the browser supports the size attribute of HTML font elements; otherwise, false. The default is false.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00006CA9 File Offset: 0x00004EA9
		public override bool SupportsFontSize
		{
			get
			{
				return this._browser.SupportsFontSize;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the use of a custom image in place of a standard form submit button.</summary>
		/// <returns>true if the browser supports the use of a custom image in place of a standard form submit button; otherwise, false. The default is false.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00006CB6 File Offset: 0x00004EB6
		public override bool SupportsImageSubmit
		{
			get
			{
				return this._browser.SupportsImageSubmit;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports i-mode symbols.</summary>
		/// <returns>true if the browser supports i-mode symbols; otherwise, false. The default is false.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00006CC3 File Offset: 0x00004EC3
		public override bool SupportsIModeSymbols
		{
			get
			{
				return this._browser.SupportsIModeSymbols;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the istyle attribute of HTML input elements.</summary>
		/// <returns>true if the browser supports the istyle attribute of HTML input elements; otherwise, false. The default is false.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public override bool SupportsInputIStyle
		{
			get
			{
				return this._browser.SupportsInputIStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the mode attribute of HTML input elements.</summary>
		/// <returns>true if the browser supports the mode attribute of HTML input elements; otherwise, false. The default is false.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00006CDD File Offset: 0x00004EDD
		public override bool SupportsInputMode
		{
			get
			{
				return this._browser.SupportsInputMode;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports HTML i elements to format italic text.</summary>
		/// <returns>true if the browser supports HTML i elements to format italic text; otherwise, false. The default is false.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00006CEA File Offset: 0x00004EEA
		public override bool SupportsItalic
		{
			get
			{
				return this._browser.SupportsItalic;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports J-Phone multimedia attributes.</summary>
		/// <returns>true if the browser supports J-Phone multimedia attributes; otherwise, false. The default is false.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00006CF7 File Offset: 0x00004EF7
		public override bool SupportsJPhoneMultiMediaAttributes
		{
			get
			{
				return this._browser.SupportsJPhoneMultiMediaAttributes;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports J-Phone–specific picture symbols.</summary>
		/// <returns>true if the browser supports J-Phone–specific picture symbols; otherwise, false. The default is false.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006D04 File Offset: 0x00004F04
		public override bool SupportsJPhoneSymbols
		{
			get
			{
				return this._browser.SupportsJPhoneSymbols;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports a query string in the action attribute value of HTML form elements.</summary>
		/// <returns>true if the browser supports a query string in the action attribute value of HTML form elements; otherwise, false. The default is true.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00006D11 File Offset: 0x00004F11
		public override bool SupportsQueryStringInFormAction
		{
			get
			{
				return this._browser.SupportsQueryStringInFormAction;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports cookies on redirection.</summary>
		/// <returns>true if the browser supports cookies on redirection; otherwise, false. The default is true.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00006D1E File Offset: 0x00004F1E
		public override bool SupportsRedirectWithCookie
		{
			get
			{
				return this._browser.SupportsRedirectWithCookie;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports the multiple attribute of HTML select elements.</summary>
		/// <returns>true if the browser supports the multiple attribute of HTML select elements; otherwise, false. The default is true.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00006D2B File Offset: 0x00004F2B
		public override bool SupportsSelectMultiple
		{
			get
			{
				return this._browser.SupportsSelectMultiple;
			}
		}

		/// <summary>Gets a value that indicates whether clearing a checked HTML input type="checkbox" element is reflected in postback data.</summary>
		/// <returns>true if clearing a checked HTML input type="checkbox" element is reflected in postback data; otherwise, false. The default is true.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006D38 File Offset: 0x00004F38
		public override bool SupportsUncheck
		{
			get
			{
				return this._browser.SupportsUncheck;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports receiving XML over HTTP.</summary>
		/// <returns>true if the browser supports receiving XML over HTTP; otherwise, false. The default is false.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00006D45 File Offset: 0x00004F45
		public override bool SupportsXmlHttp
		{
			get
			{
				return this._browser.SupportsXmlHttp;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports HTML table elements.</summary>
		/// <returns>true if the browser supports HTML table elements; otherwise, false. The default is false.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006D52 File Offset: 0x00004F52
		public override bool Tables
		{
			get
			{
				return this._browser.Tables;
			}
		}

		/// <summary>Used internally to get the type of the object that is used to write tags for the browser.</summary>
		/// <returns>The type of the object that is used to write tags for the browser.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00006D5F File Offset: 0x00004F5F
		public override Type TagWriter
		{
			get
			{
				return this._browser.TagWriter;
			}
		}

		/// <summary>Gets the name and major (integer) version number of the browser.</summary>
		/// <returns>The name and major version number of the browser.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00006D6C File Offset: 0x00004F6C
		public override string Type
		{
			get
			{
				return this._browser.Type;
			}
		}

		/// <summary>Used internally to get a value that indicates whether to use an optimized cache key.</summary>
		/// <returns>true to use an optimized cache key; otherwise, false. The default is false.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00006D79 File Offset: 0x00004F79
		public override bool UseOptimizedCacheKey
		{
			get
			{
				return this._browser.UseOptimizedCacheKey;
			}
		}

		/// <summary>Gets a value that indicates whether the browser supports Visual Basic Scripting edition (VBScript).</summary>
		/// <returns>true if the browser supports VBScript; otherwise, false. The default is false.NoteThis property does not indicate whether VBScript is currently enabled in the browser, only whether the browser can support VBScript.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00006D86 File Offset: 0x00004F86
		public override bool VBScript
		{
			get
			{
				return this._browser.VBScript;
			}
		}

		/// <summary>Gets the full version number (integer and decimal) of the browser as a string.</summary>
		/// <returns>The full version number of the browser as a string.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006D93 File Offset: 0x00004F93
		public override string Version
		{
			get
			{
				return this._browser.Version;
			}
		}

		/// <summary>Gets a value that indicates whether the client is a Win16-based computer.</summary>
		/// <returns>true if the browser is running on a Win16-based computer; otherwise, false. The default is false.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public override bool Win16
		{
			get
			{
				return this._browser.Win16;
			}
		}

		/// <summary>Gets a value that indicates whether the client is a Win32-based computer.</summary>
		/// <returns>true if the client is a Win32-based computer; otherwise, false. The default is false.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006DAD File Offset: 0x00004FAD
		public override bool Win32
		{
			get
			{
				return this._browser.Win32;
			}
		}

		/// <summary>Gets the value of the specified browser capability. In C#, this property is the indexer for the class.</summary>
		/// <returns>The browser capability with the specified key name.</returns>
		/// <param name="key">The name of the browser capability to retrieve.</param>
		// Token: 0x17000119 RID: 281
		public override string this[string key]
		{
			get
			{
				return this._browser[key];
			}
		}

		/// <summary>Used internally to add an entry to the internal collection of browsers for which capabilities are recognized.</summary>
		/// <param name="browserName">The name of the browser to add.</param>
		// Token: 0x06000246 RID: 582 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public override void AddBrowser(string browserName)
		{
			this._browser.AddBrowser(browserName);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render markup to the browser.</summary>
		/// <returns>A new instance of the object.</returns>
		/// <param name="w">The object to be created.</param>
		/// <exception cref="T:System.Exception">An error occurred when creating the <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</exception>
		// Token: 0x06000247 RID: 583 RVA: 0x00006DD6 File Offset: 0x00004FD6
		public override HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
		{
			return this._browser.CreateHtmlTextWriter(w);
		}

		/// <summary>Used internally to disable use of an optimized cache key.</summary>
		// Token: 0x06000248 RID: 584 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public override void DisableOptimizedCacheKey()
		{
			this._browser.DisableOptimizedCacheKey();
		}

		/// <summary>Gets all versions of the .NET Framework common language runtime (CLR) that are installed on the client.</summary>
		/// <returns>An array of <see cref="T:System.Version" /> objects.</returns>
		// Token: 0x06000249 RID: 585 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public override Version[] GetClrVersions()
		{
			return this._browser.GetClrVersions();
		}

		/// <summary>Gets a value that indicates whether the client browser is the same as the specified browser.</summary>
		/// <returns>true if the client browser is the same as the specified browser; otherwise, false. The default is false.</returns>
		/// <param name="browserName">The specified browser.</param>
		// Token: 0x0600024A RID: 586 RVA: 0x00006DFE File Offset: 0x00004FFE
		public override bool IsBrowser(string browserName)
		{
			return this._browser.IsBrowser(browserName);
		}

		/// <summary>Used internally to compare filters.</summary>
		/// <returns>1 if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1 if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; or 0 if there is no parent-child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
		/// <param name="filter1">The first filter to compare.</param>
		/// <param name="filter2">The second filter to compare.</param>
		// Token: 0x0600024B RID: 587 RVA: 0x00006E0C File Offset: 0x0000500C
		public override int CompareFilters(string filter1, string filter2)
		{
			return ((IFilterResolutionService)this._browser).CompareFilters(filter1, filter2);
		}

		/// <summary>Used internally to evaluate a filter.</summary>
		/// <returns>true if the filter was successfully evaluated; otherwise, false.</returns>
		/// <param name="filterName">The filter to evaluate.</param>
		// Token: 0x0600024C RID: 588 RVA: 0x00006E1B File Offset: 0x0000501B
		public override bool EvaluateFilter(string filterName)
		{
			return ((IFilterResolutionService)this._browser).EvaluateFilter(filterName);
		}

		// Token: 0x04000D9B RID: 3483
		private HttpBrowserCapabilities _browser;
	}
}
