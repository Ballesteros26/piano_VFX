using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that enable the server to gather information about the capabilities of the browser that made the current request.</summary>
	// Token: 0x02000034 RID: 52
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpBrowserCapabilitiesBase : IFilterResolutionService
	{
		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is capable of supporting ActiveX controls.</summary>
		/// <returns>true if the browser can support ActiveX controls; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool ActiveXControls
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of available control adapters.</summary>
		/// <returns>The registered control adapters for the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IDictionary Adapters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the client is an America Online (AOL) browser.</summary>
		/// <returns>true if the browser is an AOL browser; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool AOL
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports playing background sounds by using the bgsounds HTML element.</summary>
		/// <returns>true if the browser supports playing background sounds; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool BackgroundSounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is a beta version.</summary>
		/// <returns>true if the browser is a beta version; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Beta
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the browser string (if any) that was sent by the browser in the User-Agent request header.</summary>
		/// <returns>The contents of the User-Agent request header that was sent by the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Browser
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of browsers for which capabilities are recognized.</summary>
		/// <returns>The browsers for which capabilities are recognized.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual ArrayList Browsers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports decks that contain multiple forms, such as separate cards.</summary>
		/// <returns>true if the browser supports decks that contain multiple forms; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanCombineFormsInDeck
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser device is capable of initiating a voice call.</summary>
		/// <returns>true if the browser device is capable of initiating a voice call; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanInitiateVoiceCall
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports page content that follows WML select or input elements.</summary>
		/// <returns>true if the browser supports page content that follows HTML select or input elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderAfterInputOrSelectElement
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports empty HTML select elements.</summary>
		/// <returns>true if the browser supports empty HTML select elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderEmptySelects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML input and select elements together in the same card.</summary>
		/// <returns>true if the browser supports WML input and select elements together; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderInputAndSelectElementsTogether
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML option elements that specify both onpick and value attributes.</summary>
		/// <returns>true if the browser supports WML option elements that specify both onpick and value attributes; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderMixedSelects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML onevent and prev elements in the same card.</summary>
		/// <returns>true if the browser supports WML onevent and prev elements in the same WML card; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderOneventAndPrevElementsTogether
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML cards for postback.</summary>
		/// <returns>true if the browser supports WML cards for postback; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderPostBackCards
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML setvar elements that have a value attribute of 0.</summary>
		/// <returns>true if the browser supports WML setvar elements that have a value attribute of 0; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanRenderSetvarZeroWithMultiSelectionList
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports sending e-mail messages by using the HTML mailto scheme.</summary>
		/// <returns>true if the browser supports sending e-mail message by using the HTML mailto scheme; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CanSendMail
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, used internally to get the defined capabilities of the browser.</summary>
		/// <returns>The defined capabilities of the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IDictionary Capabilities
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports Channel Definition Format (CDF) for webcasting.</summary>
		/// <returns>true if the browser supports CDF; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool CDF
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the version of the .NET Framework that is installed on the client.</summary>
		/// <returns>The common language runtime (CLR) version.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Version ClrVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is capable of supporting cookies.</summary>
		/// <returns>true if the browser can support cookies; otherwise, false.NoteThis property does not indicate whether cookies are currently enabled in the browser, only whether the browser can support cookies.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is a search-engine Web crawler.</summary>
		/// <returns>true if the browser is a search-engine crawler; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Crawler
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the maximum number of submit buttons that are allowed for a form.</summary>
		/// <returns>The maximum number of submit buttons that are allowed for a form.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int DefaultSubmitButtonLimit
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the version number of ECMAScript (JavaScript) that the browser supports.</summary>
		/// <returns>The version number of ECMAScript (JavaScript) that the browser supports.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Version EcmaScriptVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML frames.</summary>
		/// <returns>true if the browser supports frames; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Frames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the major version number of the wireless gateway that is used to access the server, if known. </summary>
		/// <returns>The major version number of the wireless gateway that is used to access the server, if known.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int GatewayMajorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the minor version number of the wireless gateway that is used to access the server, if known. </summary>
		/// <returns>The minor version number of the wireless gateway that is used to access the server, if known.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual double GatewayMinorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the version of the wireless gateway that is used to access the server, if known.</summary>
		/// <returns>The version number of the wireless gateway that is used to access the server, if known.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string GatewayVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser has a dedicated Back button.</summary>
		/// <returns>true if the browser has a dedicated Back button; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool HasBackButton
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the scrollbar of an HTML select multiple element that has an align attribute value of right is obscured upon rendering.</summary>
		/// <returns>true if the scrollbar of an HTML select multiple element that has an align attribute value of right is obscured upon rendering; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool HidesRightAlignedMultiselectScrollbars
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</summary>
		/// <returns>The fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string HtmlTextWriter
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the internal identifier of the browser as specified in the browser definition file.</summary>
		/// <returns>The internal identifier of the browser as specified in the browser definition file.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the type of input that is supported by the browser.</summary>
		/// <returns>The type of input supported by the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string InputType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser has a color display.</summary>
		/// <returns>true if the browser has a color display; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is a recognized mobile device.</summary>
		/// <returns>true if the browser is a recognized mobile device; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsMobileDevice
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports Java.</summary>
		/// <returns>true if the browser supports Java; otherwise, false.NoteThis property does not indicate whether Java is currently enabled in the browser, only whether the browser can support Java.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool JavaApplets
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the JScript version that the browser supports.</summary>
		/// <returns>The version of JScript that the browser supports.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Version JScriptVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the major (integer) version number of the browser.</summary>
		/// <returns>The major version number of the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int MajorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the maximum length in characters for the href attribute of an HTML a (anchor) element.</summary>
		/// <returns>The maximum length in characters for the href attribute of an HTML a (anchor) element.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int MaximumHrefLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the maximum length of the page, in bytes, that the browser can display. </summary>
		/// <returns>The maximum length of the page, in bytes, that the browser can display.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int MaximumRenderedPageSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the maximum length of the text that a soft-key label can display.</summary>
		/// <returns>The maximum length of the text that a soft-key label can display.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int MaximumSoftkeyLabelLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the minor (decimal) version number of the browser.</summary>
		/// <returns>The minor version number of the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual double MinorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the minor (decimal) version number of the browser as a string.</summary>
		/// <returns>A string that represents the minor version number of the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MinorVersionString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the manufacturer of a mobile device, if known.</summary>
		/// <returns>The name of the manufacturer of a mobile device, if known.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MobileDeviceManufacturer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the model name of a mobile device, if known.</summary>
		/// <returns>The model name of a mobile device, if known.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MobileDeviceModel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the version of the Microsoft HTML (MSHTML) Document Object Model (DOM) that the browser supports.</summary>
		/// <returns>The MSHTML DOM version that the browser supports.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Version MSDomVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the number of softkeys on a mobile device.</summary>
		/// <returns>The number of softkeys supported on a mobile device.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int NumberOfSoftkeys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the operating system that the client is using, if known.</summary>
		/// <returns>The operating system that the client is using, if known.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Platform
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the MIME type of the type of image content that the browser typically prefers.</summary>
		/// <returns>The MIME type of the type of image content that the browser typically prefers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PreferredImageMime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the MIME type of the type of content that the browser typically prefers.</summary>
		/// <returns>The MIME type of the type of content that the browser typically prefers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PreferredRenderingMime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the general name for the type of content that the browser prefers.</summary>
		/// <returns>The values "html32" or "chtml10".</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PreferredRenderingType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the request encoding that the browser prefers.</summary>
		/// <returns>The request encoding that the browser prefers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PreferredRequestEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the response encoding that the browser prefers.</summary>
		/// <returns>The response encoding that the browser prefers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PreferredResponseEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break before WML select or input elements.</summary>
		/// <returns>true if the browser renders a line break before select or input elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RendersBreakBeforeWmlSelectAndInput
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break after list-item elements.</summary>
		/// <returns>true if the browser renders a line break after list-item elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RendersBreaksAfterHtmlLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break after a standalone WML a (anchor) element.</summary>
		/// <returns>true if the browser renders a line break after a standalone WML a (anchor) element; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RendersBreaksAfterWmlAnchor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break after a WML input element.</summary>
		/// <returns>true if the browser renders a line break after a WML input element; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RendersBreaksAfterWmlInput
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the mobile-device browser renders a WML do form accept construct as an inline button instead of as a softkey.</summary>
		/// <returns>true if the mobile-device browser renders a WML do form-accept construct as an inline button; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RendersWmlDoAcceptsInline
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders WML select elements as menu cards, instead of as a combo box.</summary>
		/// <returns>true if the browser renders WML select elements as menu cards; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RendersWmlSelectsAsMenuCards
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, used internally to produce a meta-tag that is required by some browsers.</summary>
		/// <returns>A meta-tag that is required by some browsers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string RequiredMetaTagNameValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires colons in element attribute values to be replaced with a different character.</summary>
		/// <returns>true if the browser requires colons in element attribute values to be replaced with a different character; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresAttributeColonSubstitution
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires an HTML meta element for which the content-type attribute is specified.</summary>
		/// <returns>true if the browser requires an HTML meta element for which the content-type attribute is specified; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresContentTypeMetaTag
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires control state to be maintained in sessions.</summary>
		/// <returns>true if the browser requires control state to be maintained in sessions; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresControlStateInSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires a double-byte character set.</summary>
		/// <returns>true if the browser requires a double-byte character set; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresDBCSCharacter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires nonstandard error messages.</summary>
		/// <returns>true if the browser requires nonstandard error messages; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresHtmlAdaptiveErrorReporting
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires the first element in the body of a Web page to be an HTML br element.</summary>
		/// <returns>true if the browser requires the first element in the body of a Web page to be an HTML br element; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresLeadingPageBreak
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser does not support HTML br elements to format line breaks.</summary>
		/// <returns>true if the browser does not support HTML br elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresNoBreakInFormatting
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires pages to contain a size-optimized form of markup language tags.</summary>
		/// <returns>true if the browser requires pages to contain a size-optimized form of markup language tags; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresOutputOptimization
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports telephone dialing based on plain text, or whether it requires special markup.</summary>
		/// <returns>true if the browser supports telephone dialing based on plain text; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresPhoneNumbersAsPlainText
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires view-state values to be specially encoded.</summary>
		/// <returns>true if the browser requires view-state values to be specially encoded; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresSpecialViewStateEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires unique form-action URLs.</summary>
		/// <returns>true if the browser requires unique form-action URLs; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresUniqueFilePathSuffix
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires unique name attribute values for multiple HTML input type="checkbox" elements.</summary>
		/// <returns>true if the browser requires unique name attribute values for multiple HTML input type="checkbox" elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresUniqueHtmlCheckboxNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires unique name attribute values for multiple HTML input elements.</summary>
		/// <returns>true if the browser requires unique name attribute values for multiple HTML input elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresUniqueHtmlInputNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether postback data that is sent by the browser will be URL-encoded.</summary>
		/// <returns>true if postback data that is sent by the browser will be URL-encoded; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool RequiresUrlEncodedPostfieldValues
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the depth of the display, in bits per pixel.</summary>
		/// <returns>The depth of the display, in bits per pixel.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ScreenBitDepth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the approximate height of the display, in character lines.</summary>
		/// <returns>The approximate height of the display, in character lines.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ScreenCharactersHeight
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the approximate width of the display, in characters.</summary>
		/// <returns>The approximate width of the display, in characters.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ScreenCharactersWidth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the approximate height of the display, in pixels.</summary>
		/// <returns>The approximate height of the display, in pixels.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ScreenPixelsHeight
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the approximate width of the display, in pixels.</summary>
		/// <returns>The approximate width of the display, in pixels.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ScreenPixelsWidth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the accesskey attribute of HTML a (anchor) and input elements.</summary>
		/// <returns>true if the browser supports the accesskey attribute of HTML a (anchor) and input elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsAccesskeyAttribute
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the bgcolor attribute of the HTML body element.</summary>
		/// <returns>true if the browser supports the bgcolor attribute of the HTML body element; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsBodyColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML b elements to format bold text.</summary>
		/// <returns>true if the browser supports HTML b elements to format bold text; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsBold
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the cache-control value for the http-equiv attribute of HTML meta elements.</summary>
		/// <returns>true if the browser supports the cache-control value for the http-equiv attribute of HTML meta elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsCacheControlMetaTag
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports callback scripts.</summary>
		/// <returns>true if the browser supports callback scripts; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsCallback
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports cascading style sheets (CSS).</summary>
		/// <returns>true if the browser supports CSS; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsCss
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the align attribute of HTML div elements.</summary>
		/// <returns>true if the browser supports the align attribute of HTML div elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsDivAlign
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the nowrap attribute of HTML div elements.</summary>
		/// <returns>true if the browser supports the nowrap HTML div elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsDivNoWrap
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports empty strings in cookie values.</summary>
		/// <returns>true if the browser supports empty strings in cookie values; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsEmptyStringInCookieValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the color attribute of HTML font elements.</summary>
		/// <returns>true if the browser supports the color attribute of HTML font elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsFontColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the name attribute of HTML font elements.</summary>
		/// <returns>true if the browser supports the name attribute of HTML font elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsFontName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the size attribute of HTML font elements.</summary>
		/// <returns>true if the browser supports the size attribute of HTML font elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsFontSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the use of a custom image in place of a standard form submit button.</summary>
		/// <returns>true if the browser supports the use of a custom image in place of a standard form submit button; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsImageSubmit
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports i-mode symbols.</summary>
		/// <returns>true if the browser supports i-mode symbols; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsIModeSymbols
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the istyle attribute of HTML input elements.</summary>
		/// <returns>true if the browser supports the istyle attribute of HTML input elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsInputIStyle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the mode attribute of HTML input elements.</summary>
		/// <returns>true if the browser supports the mode attribute of HTML input elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsInputMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML i elements to format italic text.</summary>
		/// <returns>true if the browser supports HTML i elements to format italic text; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsItalic
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports J-Phone multimedia attributes.</summary>
		/// <returns>true if the browser supports J-Phone multimedia attributes; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsJPhoneMultiMediaAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports J-Phone–specific picture symbols.</summary>
		/// <returns>true if the browser supports J-Phone–specific picture symbols; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsJPhoneSymbols
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports a query string in the action attribute value of HTML form elements.</summary>
		/// <returns>true if the browser supports a query string in the action attribute value of HTML form elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsQueryStringInFormAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports cookies on redirection.</summary>
		/// <returns>true if the browser supports cookies on redirection; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsRedirectWithCookie
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the multiple attribute of HTML select elements.</summary>
		/// <returns>true if the browser supports the multiple attribute of HTML select elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsSelectMultiple
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether clearing a checked HTML input type="checkbox" element is reflected in postback data.</summary>
		/// <returns>true if clearing a checked HTML input type="checkbox" element is reflected in postback data; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsUncheck
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports receiving XML over HTTP.</summary>
		/// <returns>true if the browser supports receiving XML over HTTP; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsXmlHttp
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML table elements.</summary>
		/// <returns>true if the browser supports HTML table elements; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Tables
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, used internally to get the type of the object that is used to write tags for the browser.</summary>
		/// <returns>The type of the object that is used to write tags for the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Type TagWriter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the name and major (integer) version number of the browser.</summary>
		/// <returns>The name and major version number of the browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Type
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, used internally to get a value that indicates whether to use an optimized cache key.</summary>
		/// <returns>true to use an optimized cache key; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool UseOptimizedCacheKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports Visual Basic Scripting edition (VBScript).</summary>
		/// <returns>true if the browser supports VBScript; otherwise, false.NoteThis property does not indicate whether VBScript is currently enabled in the browser, only whether the browser can support VBScript.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool VBScript
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the full version number (integer and decimal) of the browser as a string.</summary>
		/// <returns>The full version number of the browser as a string.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Version
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the version of the World Wide Web Consortium (W3C) XML Document Object Model (DOM) that the browser supports.</summary>
		/// <returns>The number of the W3C XML DOM version number that the browser supports.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Version W3CDomVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the client is a Win16-based computer.</summary>
		/// <returns>true if the browser is running on a Win16-based computer; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Win16
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the client is a Win32-based computer.</summary>
		/// <returns>true if the client is a Win32-based computer; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Win32
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the value of the specified browser capability. In C#, this property is the indexer for the class.</summary>
		/// <returns>The browser capability with the specified key name.</returns>
		/// <param name="key">The name of the browser capability to retrieve.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170000A8 RID: 168
		public virtual string this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, used internally to add an entry to the internal collection of browsers for which capabilities are recognized.</summary>
		/// <param name="browserName">The name of the browser to add.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001CA RID: 458 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddBrowser(string browserName)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render markup to the browser.</summary>
		/// <returns>A new instance of the object.</returns>
		/// <param name="w">The object to be created.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001CB RID: 459 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, used internally to disable use of an optimized cache key.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001CC RID: 460 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void DisableOptimizedCacheKey()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets all versions of the .NET Framework common language runtime (CLR) that are installed on the client.</summary>
		/// <returns>An array of <see cref="T:System.Version" /> objects.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001CD RID: 461 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Version[] GetClrVersions()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the client browser is the same as the specified browser.</summary>
		/// <returns>true if the client browser is the same as the specified browser; otherwise, false.</returns>
		/// <param name="browserName">The specified browser.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001CE RID: 462 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsBrowser(string browserName)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, used internally to compare filters.</summary>
		/// <returns>1 if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1 if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; or 0 if there is no parent-child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
		/// <param name="filter1">The first filter to compare.</param>
		/// <param name="filter2">The second filter to compare.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001CF RID: 463 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int CompareFilters(string filter1, string filter2)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, used internally to evaluate a filter.</summary>
		/// <returns>true if the filter was successfully evaluated; otherwise, false.</returns>
		/// <param name="filterName">The filter to evaluate.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060001D0 RID: 464 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool EvaluateFilter(string filterName)
		{
			throw new NotImplementedException();
		}
	}
}
