using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
	/// <summary>Provides an object representation of a uniform resource identifier (URI) and easy access to the parts of the URI.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F6 RID: 246
	[TypeConverter(typeof(UriTypeConverter))]
	[Serializable]
	public class Uri : ISerializable
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001B661 File Offset: 0x00019861
		private bool IsImplicitFile
		{
			get
			{
				return (this.m_Flags & Uri.Flags.ImplicitFile) > Uri.Flags.Zero;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001B674 File Offset: 0x00019874
		private bool IsUncOrDosPath
		{
			get
			{
				return (this.m_Flags & (Uri.Flags.DosPath | Uri.Flags.UncPath)) > Uri.Flags.Zero;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001B687 File Offset: 0x00019887
		private bool IsDosPath
		{
			get
			{
				return (this.m_Flags & Uri.Flags.DosPath) > Uri.Flags.Zero;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001B69A File Offset: 0x0001989A
		private bool IsUncPath
		{
			get
			{
				return (this.m_Flags & Uri.Flags.UncPath) > Uri.Flags.Zero;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001B6AD File Offset: 0x000198AD
		private Uri.Flags HostType
		{
			get
			{
				return this.m_Flags & Uri.Flags.HostTypeMask;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001B6BC File Offset: 0x000198BC
		private UriParser Syntax
		{
			get
			{
				return this.m_Syntax;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001B6C4 File Offset: 0x000198C4
		private bool IsNotAbsoluteUri
		{
			get
			{
				return this.m_Syntax == null;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001B6CF File Offset: 0x000198CF
		internal static bool IriParsingStatic(UriParser syntax)
		{
			return Uri.s_IriParsing && ((syntax != null && syntax.InFact(UriSyntaxFlags.AllowIriParsing)) || syntax == null);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001B6F4 File Offset: 0x000198F4
		private bool AllowIdn
		{
			get
			{
				return this.m_Syntax != null && (this.m_Syntax.Flags & UriSyntaxFlags.AllowIdn) != UriSyntaxFlags.None && (Uri.s_IdnScope == UriIdnScope.All || (Uri.s_IdnScope == UriIdnScope.AllExceptIntranet && this.NotAny(Uri.Flags.IntranetUri)));
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001B745 File Offset: 0x00019945
		private bool AllowIdnStatic(UriParser syntax, Uri.Flags flags)
		{
			return syntax != null && (syntax.Flags & UriSyntaxFlags.AllowIdn) != UriSyntaxFlags.None && (Uri.s_IdnScope == UriIdnScope.All || (Uri.s_IdnScope == UriIdnScope.AllExceptIntranet && Uri.StaticNotAny(flags, Uri.Flags.IntranetUri)));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00004240 File Offset: 0x00002440
		private bool IsIntranet(string schemeHost)
		{
			return false;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001B781 File Offset: 0x00019981
		internal bool UserDrivenParsing
		{
			get
			{
				return (this.m_Flags & Uri.Flags.UserDrivenParsing) > Uri.Flags.Zero;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001B794 File Offset: 0x00019994
		private void SetUserDrivenParsing()
		{
			this.m_Flags = Uri.Flags.UserDrivenParsing | (this.m_Flags & Uri.Flags.UserEscaped);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001B7B0 File Offset: 0x000199B0
		private ushort SecuredPathIndex
		{
			get
			{
				if (this.IsDosPath)
				{
					char c = this.m_String[(int)this.m_Info.Offset.Path];
					return (c == '/' || c == '\\') ? 3 : 2;
				}
				return 0;
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001B7F2 File Offset: 0x000199F2
		private bool NotAny(Uri.Flags flags)
		{
			return (this.m_Flags & flags) == Uri.Flags.Zero;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001B800 File Offset: 0x00019A00
		private bool InFact(Uri.Flags flags)
		{
			return (this.m_Flags & flags) > Uri.Flags.Zero;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B80E File Offset: 0x00019A0E
		private static bool StaticNotAny(Uri.Flags allFlags, Uri.Flags checkFlags)
		{
			return (allFlags & checkFlags) == Uri.Flags.Zero;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B817 File Offset: 0x00019A17
		private static bool StaticInFact(Uri.Flags allFlags, Uri.Flags checkFlags)
		{
			return (allFlags & checkFlags) > Uri.Flags.Zero;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001B820 File Offset: 0x00019A20
		private Uri.UriInfo EnsureUriInfo()
		{
			Uri.Flags flags = this.m_Flags;
			if ((this.m_Flags & Uri.Flags.MinimalUriInfoSet) == Uri.Flags.Zero)
			{
				this.CreateUriInfo(flags);
			}
			return this.m_Info;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001B850 File Offset: 0x00019A50
		private void EnsureParseRemaining()
		{
			if ((this.m_Flags & (Uri.Flags)(-2147483648)) == Uri.Flags.Zero)
			{
				this.ParseRemaining();
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001B867 File Offset: 0x00019A67
		private void EnsureHostString(bool allowDnsOptimization)
		{
			this.EnsureUriInfo();
			if (this.m_Info.Host == null)
			{
				if (allowDnsOptimization && this.InFact(Uri.Flags.CanonicalDnsHost))
				{
					return;
				}
				this.CreateHostString();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class with the specified URI.</summary>
		/// <param name="uriString">A URI. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x060006BA RID: 1722 RVA: 0x0001B895 File Offset: 0x00019A95
		public Uri(string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(uriString, false, UriKind.Absolute);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class with the specified URI, with explicit control of character escaping.</summary>
		/// <param name="uriString">The URI. </param>
		/// <param name="dontEscape">true if <paramref name="uriString" /> is completely escaped; otherwise, false. See Remarks. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="uriString" /> is empty or contains only spaces.-or- The scheme specified in <paramref name="uriString" /> is not valid.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x060006BB RID: 1723 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		[Obsolete("The constructor has been deprecated. Please use new Uri(string). The dontEscape parameter is deprecated and is always false. http://go.microsoft.com/fwlink/?linkid=14202")]
		public Uri(string uriString, bool dontEscape)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(uriString, dontEscape, UriKind.Absolute);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class based on the specified base and relative URIs, with explicit control of character escaping.</summary>
		/// <param name="baseUri">The base URI. </param>
		/// <param name="relativeUri">The relative URI to add to the base URI. </param>
		/// <param name="dontEscape">true if <paramref name="uriString" /> is completely escaped; otherwise, false. See Remarks. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseUri" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is empty or contains only spaces.-or- The scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> contains too many slashes.-or- The password specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The file name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid. -or- The user name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host or authority name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> cannot be terminated by backslashes.-or- The port number specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid or cannot be parsed.-or- The length of the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 65519 characters.-or- The length of the scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 1023 characters.-or- There is an invalid character sequence in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x060006BC RID: 1724 RVA: 0x0001B8D3 File Offset: 0x00019AD3
		[Obsolete("The constructor has been deprecated. Please new Uri(Uri, string). The dontEscape parameter is deprecated and is always false. http://go.microsoft.com/fwlink/?linkid=14202")]
		public Uri(Uri baseUri, string relativeUri, bool dontEscape)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			this.CreateUri(baseUri, relativeUri, dontEscape);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class with the specified URI. This constructor allows you to specify if the URI string is a relative URI, absolute URI, or is indeterminate.</summary>
		/// <param name="uriString">A string that identifies the resource to be represented by the <see cref="T:System.Uri" /> instance.</param>
		/// <param name="uriKind">Specifies whether the URI string is a relative URI, absolute URI, or is indeterminate.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uriKind" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> contains a relative URI and <paramref name="uriKind" /> is <see cref="F:System.UriKind.Absolute" />.or<paramref name="uriString" /> contains an absolute URI and <paramref name="uriKind" /> is <see cref="F:System.UriKind.Relative" />.or<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x060006BD RID: 1725 RVA: 0x0001B905 File Offset: 0x00019B05
		public Uri(string uriString, UriKind uriKind)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(uriString, false, uriKind);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class based on the specified base URI and relative URI string.</summary>
		/// <param name="baseUri">The base URI. </param>
		/// <param name="relativeUri">The relative URI to add to the base URI. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseUri" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is empty or contains only spaces.-or- The scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> contains too many slashes.-or- The password specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The file name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid. -or- The user name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host or authority name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> cannot be terminated by backslashes.-or- The port number specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid or cannot be parsed.-or- The length of the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 65519 characters.-or- The length of the scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 1023 characters.-or- There is an invalid character sequence in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x060006BE RID: 1726 RVA: 0x0001B924 File Offset: 0x00019B24
		public Uri(Uri baseUri, string relativeUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			this.CreateUri(baseUri, relativeUri, false);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001B958 File Offset: 0x00019B58
		private void CreateUri(Uri baseUri, string relativeUri, bool dontEscape)
		{
			this.CreateThis(relativeUri, dontEscape, (UriKind)300);
			if (baseUri.Syntax.IsSimple)
			{
				UriFormatException ex;
				Uri uri = Uri.ResolveHelper(baseUri, this, ref relativeUri, ref dontEscape, out ex);
				if (ex != null)
				{
					throw ex;
				}
				if (uri != null)
				{
					if (uri != this)
					{
						this.CreateThisFromUri(uri);
					}
					return;
				}
			}
			else
			{
				dontEscape = false;
				UriFormatException ex;
				relativeUri = baseUri.Syntax.InternalResolve(baseUri, this, out ex);
				if (ex != null)
				{
					throw ex;
				}
			}
			this.m_Flags = Uri.Flags.Zero;
			this.m_Info = null;
			this.m_Syntax = null;
			this.CreateThis(relativeUri, dontEscape, UriKind.Absolute);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class based on the combination of a specified base <see cref="T:System.Uri" /> instance and a relative <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="baseUri">An absolute <see cref="T:System.Uri" /> that is the base for the new <see cref="T:System.Uri" /> instance. </param>
		/// <param name="relativeUri">A relative <see cref="T:System.Uri" /> instance that is combined with <paramref name="baseUri" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseUri" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is empty or contains only spaces.-or- The scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> contains too many slashes.-or- The password specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The file name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid. -or- The user name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host or authority name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> cannot be terminated by backslashes.-or- The port number specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid or cannot be parsed.-or- The length of the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 65519 characters.-or- The length of the scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 1023 characters.-or- There is an invalid character sequence in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x060006C0 RID: 1728 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		public Uri(Uri baseUri, Uri relativeUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			this.CreateThisFromUri(relativeUri);
			string text = null;
			bool flag;
			if (baseUri.Syntax.IsSimple)
			{
				flag = this.InFact(Uri.Flags.UserEscaped);
				UriFormatException ex;
				relativeUri = Uri.ResolveHelper(baseUri, this, ref text, ref flag, out ex);
				if (ex != null)
				{
					throw ex;
				}
				if (relativeUri != null)
				{
					if (relativeUri != this)
					{
						this.CreateThisFromUri(relativeUri);
					}
					return;
				}
			}
			else
			{
				flag = false;
				UriFormatException ex;
				text = baseUri.Syntax.InternalResolve(baseUri, this, out ex);
				if (ex != null)
				{
					throw ex;
				}
			}
			this.m_Flags = Uri.Flags.Zero;
			this.m_Info = null;
			this.m_Syntax = null;
			this.CreateThis(text, flag, UriKind.Absolute);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001BA98 File Offset: 0x00019C98
		private unsafe static ParsingError GetCombinedString(Uri baseUri, string relativeStr, bool dontEscape, ref string result)
		{
			int num = 0;
			while (num < relativeStr.Length && relativeStr[num] != '/' && relativeStr[num] != '\\' && relativeStr[num] != '?' && relativeStr[num] != '#')
			{
				if (relativeStr[num] == ':')
				{
					if (num >= 2)
					{
						string text = relativeStr.Substring(0, num);
						fixed (string text2 = text)
						{
							char* ptr = text2;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							UriParser uriParser = null;
							if (Uri.CheckSchemeSyntax(ptr, (ushort)text.Length, ref uriParser) == ParsingError.None)
							{
								if (baseUri.Syntax != uriParser)
								{
									result = relativeStr;
									return ParsingError.None;
								}
								if (num + 1 < relativeStr.Length)
								{
									relativeStr = relativeStr.Substring(num + 1);
								}
								else
								{
									relativeStr = string.Empty;
								}
							}
						}
						break;
					}
					break;
				}
				else
				{
					num++;
				}
			}
			if (relativeStr.Length == 0)
			{
				result = baseUri.OriginalString;
				return ParsingError.None;
			}
			result = Uri.CombineUri(baseUri, relativeStr, dontEscape ? UriFormat.UriEscaped : UriFormat.SafeUnescaped);
			return ParsingError.None;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001BB88 File Offset: 0x00019D88
		private static UriFormatException GetException(ParsingError err)
		{
			switch (err)
			{
			case ParsingError.None:
				return null;
			case ParsingError.BadFormat:
				return new UriFormatException(global::SR.GetString("Invalid URI: The format of the URI could not be determined."));
			case ParsingError.BadScheme:
				return new UriFormatException(global::SR.GetString("Invalid URI: The URI scheme is not valid."));
			case ParsingError.BadAuthority:
				return new UriFormatException(global::SR.GetString("Invalid URI: The Authority/Host could not be parsed."));
			case ParsingError.EmptyUriString:
				return new UriFormatException(global::SR.GetString("Invalid URI: The URI is empty."));
			case ParsingError.SchemeLimit:
				return new UriFormatException(global::SR.GetString("Invalid URI: The Uri scheme is too long."));
			case ParsingError.SizeLimit:
				return new UriFormatException(global::SR.GetString("Invalid URI: The Uri string is too long."));
			case ParsingError.MustRootedPath:
				return new UriFormatException(global::SR.GetString("Invalid URI: A Dos path must be rooted, for example, 'c:\\\\'."));
			case ParsingError.BadHostName:
				return new UriFormatException(global::SR.GetString("Invalid URI: The hostname could not be parsed."));
			case ParsingError.NonEmptyHost:
				return new UriFormatException(global::SR.GetString("Invalid URI: The format of the URI could not be determined."));
			case ParsingError.BadPort:
				return new UriFormatException(global::SR.GetString("Invalid URI: Invalid port specified."));
			case ParsingError.BadAuthorityTerminator:
				return new UriFormatException(global::SR.GetString("Invalid URI: The Authority/Host cannot end with a backslash character ('\\\\')."));
			case ParsingError.CannotCreateRelative:
				return new UriFormatException(global::SR.GetString("A relative URI cannot be created because the 'uriString' parameter represents an absolute URI."));
			default:
				return new UriFormatException(global::SR.GetString("Invalid URI: The format of the URI could not be determined."));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">An instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class containing the information required to serialize the new <see cref="T:System.Uri" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class containing the source of the serialized stream associated with the new <see cref="T:System.Uri" /> instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationInfo" /> parameter contains a null URI. </exception>
		/// <exception cref="T:System.UriFormatException">The <paramref name="serializationInfo" /> parameter contains a URI that is empty.-or- The scheme specified is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- The URI contains too many slashes.-or- The password specified in the URI is not valid.-or- The host name specified in URI is not valid.-or- The file name specified in the URI is not valid. -or- The user name specified in the URI is not valid.-or- The host or authority name specified in the URI cannot be terminated by backslashes.-or- The port number specified in the URI is not valid or cannot be parsed.-or- The length of URI exceeds 65519 characters.-or- The length of the scheme specified in the URI exceeds 1023 characters.-or- There is an invalid character sequence in the URI.-or- The MS-DOS path specified in the URI must start with c:\\.</exception>
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		protected Uri(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			string text = serializationInfo.GetString("AbsoluteUri");
			if (text.Length != 0)
			{
				this.CreateThis(text, false, UriKind.Absolute);
				return;
			}
			text = serializationInfo.GetString("RelativeUri");
			if (text == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(text, false, UriKind.Relative);
		}

		/// <summary>Returns the data needed to serialize the current instance.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Uri" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Uri" />.</param>
		// Token: 0x060006C4 RID: 1732 RVA: 0x0001BCFC File Offset: 0x00019EFC
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Returns the data needed to serialize the current instance.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Uri" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Uri" />.</param>
		// Token: 0x060006C5 RID: 1733 RVA: 0x0001BD08 File Offset: 0x00019F08
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (this.IsAbsoluteUri)
			{
				serializationInfo.AddValue("AbsoluteUri", this.GetParts(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
				return;
			}
			serializationInfo.AddValue("AbsoluteUri", string.Empty);
			serializationInfo.AddValue("RelativeUri", this.GetParts(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
		}

		/// <summary>Gets the absolute path of the URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the absolute path to the resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001BD5C File Offset: 0x00019F5C
		public string AbsolutePath
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				string text = this.PrivateAbsolutePath;
				if (this.IsDosPath && text[0] == '/')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		private string PrivateAbsolutePath
		{
			get
			{
				Uri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new Uri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.Path;
				if (text == null)
				{
					text = this.GetParts(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
					uriInfo.MoreInfo.Path = text;
				}
				return text;
			}
		}

		/// <summary>Gets the absolute URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the entire URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public string AbsoluteUri
		{
			get
			{
				if (this.m_Syntax == null)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				Uri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new Uri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.AbsoluteUri;
				if (text == null)
				{
					text = this.GetParts(UriComponents.AbsoluteUri, UriFormat.UriEscaped);
					uriInfo.MoreInfo.AbsoluteUri = text;
				}
				return text;
			}
		}

		/// <summary>Gets a local operating-system representation of a file name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local operating-system representation of a file name.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001BE59 File Offset: 0x0001A059
		public string LocalPath
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.GetLocalPath();
			}
		}

		/// <summary>Gets the Domain Name System (DNS) host name or IP address and the port number for a server.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the authority component of the URI represented by this instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001BE79 File Offset: 0x0001A079
		public string Authority
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.GetParts(UriComponents.Host | UriComponents.Port, UriFormat.UriEscaped);
			}
		}

		/// <summary>Gets the type of the host name specified in the URI.</summary>
		/// <returns>A member of the <see cref="T:System.UriHostNameType" /> enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001BE9C File Offset: 0x0001A09C
		public UriHostNameType HostNameType
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				if (this.m_Syntax.IsSimple)
				{
					this.EnsureUriInfo();
				}
				else
				{
					this.EnsureHostString(false);
				}
				Uri.Flags hostType = this.HostType;
				if (hostType <= Uri.Flags.DnsHostType)
				{
					if (hostType == Uri.Flags.IPv6HostType)
					{
						return UriHostNameType.IPv6;
					}
					if (hostType == Uri.Flags.IPv4HostType)
					{
						return UriHostNameType.IPv4;
					}
					if (hostType == Uri.Flags.DnsHostType)
					{
						return UriHostNameType.Dns;
					}
				}
				else
				{
					if (hostType == Uri.Flags.UncHostType)
					{
						return UriHostNameType.Basic;
					}
					if (hostType == Uri.Flags.BasicHostType)
					{
						return UriHostNameType.Basic;
					}
					if (hostType == Uri.Flags.HostTypeMask)
					{
						return UriHostNameType.Unknown;
					}
				}
				return UriHostNameType.Unknown;
			}
		}

		/// <summary>Gets whether the port value of the URI is the default for this scheme.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the value in the <see cref="P:System.Uri.Port" /> property is the default port for this scheme; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001BF38 File Offset: 0x0001A138
		public bool IsDefaultPort
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				if (this.m_Syntax.IsSimple)
				{
					this.EnsureUriInfo();
				}
				else
				{
					this.EnsureHostString(false);
				}
				return this.NotAny(Uri.Flags.NotDefaultPort);
			}
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.Uri" /> is a file URI.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> is a file URI; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001BF86 File Offset: 0x0001A186
		public bool IsFile
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.m_Syntax.SchemeName == Uri.UriSchemeFile;
			}
		}

		/// <summary>Gets whether the specified <see cref="T:System.Uri" /> references the local host.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if this <see cref="T:System.Uri" /> references the local host; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001BFB2 File Offset: 0x0001A1B2
		public bool IsLoopback
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				this.EnsureHostString(false);
				return this.InFact(Uri.Flags.LoopbackHost);
			}
		}

		/// <summary>Gets the <see cref="P:System.Uri.AbsolutePath" /> and <see cref="P:System.Uri.Query" /> properties separated by a question mark (?).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the <see cref="P:System.Uri.AbsolutePath" /> and <see cref="P:System.Uri.Query" /> properties separated by a question mark (?).</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		public string PathAndQuery
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				string text = this.GetParts(UriComponents.PathAndQuery, UriFormat.UriEscaped);
				if (this.IsDosPath && text[0] == '/')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		/// <summary>Gets an array containing the path segments that make up the specified URI.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the path segments that make up the specified URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001C02C File Offset: 0x0001A22C
		public string[] Segments
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				string[] array = null;
				if (array == null)
				{
					string privateAbsolutePath = this.PrivateAbsolutePath;
					if (privateAbsolutePath.Length == 0)
					{
						array = new string[0];
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						int num;
						for (int i = 0; i < privateAbsolutePath.Length; i = num + 1)
						{
							num = privateAbsolutePath.IndexOf('/', i);
							if (num == -1)
							{
								num = privateAbsolutePath.Length - 1;
							}
							arrayList.Add(privateAbsolutePath.Substring(i, num - i + 1));
						}
						array = (string[])arrayList.ToArray(typeof(string));
					}
				}
				return array;
			}
		}

		/// <summary>Gets whether the specified <see cref="T:System.Uri" /> is a universal naming convention (UNC) path.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> is a UNC path; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		public bool IsUnc
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.IsUncPath;
			}
		}

		/// <summary>Gets the host component of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host name. This is usually the DNS host name or IP address of the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001C0EB File Offset: 0x0001A2EB
		public string Host
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.GetParts(UriComponents.Host, UriFormat.UriEscaped);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001C10D File Offset: 0x0001A30D
		private static bool StaticIsFile(UriParser syntax)
		{
			return syntax.InFact(UriSyntaxFlags.FileLikeUri);
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001C11C File Offset: 0x0001A31C
		private static object InitializeLock
		{
			get
			{
				if (Uri.s_initLock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref Uri.s_initLock, obj, null);
				}
				return Uri.s_initLock;
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001C148 File Offset: 0x0001A348
		private static void InitializeUriConfig()
		{
			if (!Uri.s_ConfigInitialized)
			{
				object initializeLock = Uri.InitializeLock;
				lock (initializeLock)
				{
					if (!Uri.s_ConfigInitialized && !Uri.s_ConfigInitializing)
					{
						Uri.s_ConfigInitialized = true;
						Uri.s_ConfigInitializing = false;
					}
				}
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		private string GetLocalPath()
		{
			this.EnsureParseRemaining();
			bool flag = this.m_Info.Offset.Host != this.m_Info.Offset.Path && this.IsFile && this.OriginalString.StartsWith("file://", StringComparison.Ordinal) && !this.IsLoopback;
			if (flag)
			{
				flag = false;
				for (int i = (int)this.m_Info.Offset.Host; i < (int)this.m_Info.Offset.Path; i++)
				{
					if (this.OriginalString[i] != '/')
					{
						flag = true;
						break;
					}
				}
			}
			bool flag2 = this.IsUncPath || flag;
			if ((!this.IsUncOrDosPath || (!Uri.IsWindowsFileSystem && this.IsUncPath)) && !flag)
			{
				return this.GetUnescapedParts(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
			}
			this.EnsureHostString(false);
			int num;
			if (this.NotAny(Uri.Flags.HostNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.ShouldBeCompressed) && !flag)
			{
				num = (int)(this.IsUncPath ? (this.m_Info.Offset.Host - 2) : this.m_Info.Offset.Path);
				string text = ((this.IsImplicitFile && this.m_Info.Offset.Host == (this.IsDosPath ? 0 : 2) && this.m_Info.Offset.Query == this.m_Info.Offset.End) ? this.m_String : ((this.IsDosPath && (this.m_String[num] == '/' || this.m_String[num] == '\\')) ? this.m_String.Substring(num + 1, (int)this.m_Info.Offset.Query - num - 1) : this.m_String.Substring(num, (int)this.m_Info.Offset.Query - num)));
				if (this.IsDosPath && text[1] == '|')
				{
					text = text.Remove(1, 1);
					text = text.Insert(1, ":");
				}
				for (int j = 0; j < text.Length; j++)
				{
					if (text[j] == '/')
					{
						text = text.Replace('/', '\\');
						break;
					}
				}
				return text;
			}
			int num2 = 0;
			num = (int)this.m_Info.Offset.Path;
			string host = this.m_Info.Host;
			char[] array = new char[host.Length + 3 + (int)this.m_Info.Offset.Fragment - (int)this.m_Info.Offset.Path];
			if (flag2)
			{
				array[0] = '\\';
				array[1] = '\\';
				num2 = 2;
				UriHelper.UnescapeString(host, 0, host.Length, array, ref num2, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
			}
			else if (this.m_String[num] == '/' || this.m_String[num] == '\\')
			{
				num++;
			}
			ushort num3 = (ushort)num2;
			UnescapeMode unescapeMode = ((this.InFact(Uri.Flags.PathNotCanonical) && !this.IsImplicitFile) ? (UnescapeMode.Unescape | UnescapeMode.UnescapeAll) : UnescapeMode.CopyOnly);
			UriHelper.UnescapeString(this.m_String, num, (int)this.m_Info.Offset.Query, array, ref num2, char.MaxValue, char.MaxValue, char.MaxValue, unescapeMode, this.m_Syntax, true);
			if (array[1] == '|')
			{
				array[1] = ':';
			}
			if (this.InFact(Uri.Flags.ShouldBeCompressed))
			{
				array = Uri.Compress(array, this.IsDosPath ? (num3 + 2) : num3, ref num2, this.m_Syntax);
			}
			for (ushort num4 = 0; num4 < (ushort)num2; num4 += 1)
			{
				if (array[(int)num4] == '/')
				{
					array[(int)num4] = '\\';
				}
			}
			return new string(array, 0, num2);
		}

		/// <summary>Gets the port number of this URI.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the port number for this URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001C56C File Offset: 0x0001A76C
		public int Port
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				if (this.m_Syntax.IsSimple)
				{
					this.EnsureUriInfo();
				}
				else
				{
					this.EnsureHostString(false);
				}
				if (this.InFact(Uri.Flags.NotDefaultPort))
				{
					return (int)this.m_Info.Offset.PortValue;
				}
				return this.m_Syntax.DefaultPort;
			}
		}

		/// <summary>Gets any query information included in the specified URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains any query information included in the specified URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		public string Query
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				Uri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new Uri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.Query;
				if (text == null)
				{
					text = this.GetParts(UriComponents.Query | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
					uriInfo.MoreInfo.Query = text;
				}
				return text;
			}
		}

		/// <summary>Gets the escaped URI fragment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains any URI fragment information.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001C640 File Offset: 0x0001A840
		public string Fragment
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				Uri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new Uri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.Fragment;
				if (text == null)
				{
					text = this.GetParts(UriComponents.Fragment | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
					uriInfo.MoreInfo.Fragment = text;
				}
				return text;
			}
		}

		/// <summary>Gets the scheme name for this URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the scheme for this URI, converted to lowercase.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		public string Scheme
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.m_Syntax.SchemeName;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
		private bool OriginalStringSwitched
		{
			get
			{
				return (this.m_iriParsing && this.InFact(Uri.Flags.HasUnicode)) || (this.AllowIdn && (this.InFact(Uri.Flags.IdnHost) || this.InFact(Uri.Flags.UnicodeHost)));
			}
		}

		/// <summary>Gets the original URI string that was passed to the <see cref="T:System.Uri" /> constructor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the exact URI specified when this instance was constructed; otherwise, <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001C724 File Offset: 0x0001A924
		public string OriginalString
		{
			get
			{
				if (!this.OriginalStringSwitched)
				{
					return this.m_String;
				}
				return this.m_originalUnicodeString;
			}
		}

		/// <summary>Gets an unescaped host name that is safe to use for DNS resolution.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the unescaped host part of the URI that is suitable for DNS resolution; or the original unescaped host string, if it is already suitable for resolution.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001C73C File Offset: 0x0001A93C
		public string DnsSafeHost
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				if (this.AllowIdn && ((this.m_Flags & Uri.Flags.IdnHost) != Uri.Flags.Zero || (this.m_Flags & Uri.Flags.UnicodeHost) != Uri.Flags.Zero))
				{
					this.EnsureUriInfo();
					return this.m_Info.DnsSafeHost;
				}
				this.EnsureHostString(false);
				if (!string.IsNullOrEmpty(this.m_Info.DnsSafeHost))
				{
					return this.m_Info.DnsSafeHost;
				}
				if (this.m_Info.Host.Length == 0)
				{
					return string.Empty;
				}
				string text = this.m_Info.Host;
				if (this.HostType == Uri.Flags.IPv6HostType)
				{
					text = text.Substring(1, text.Length - 2);
					if (this.m_Info.ScopeId != null)
					{
						text += this.m_Info.ScopeId;
					}
				}
				else if (this.HostType == Uri.Flags.BasicHostType && this.InFact(Uri.Flags.HostNotCanonical | Uri.Flags.E_HostNotCanonical))
				{
					char[] array = new char[text.Length];
					int num = 0;
					UriHelper.UnescapeString(text, 0, text.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, false);
					text = new string(array, 0, num);
				}
				this.m_Info.DnsSafeHost = text;
				return text;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001C894 File Offset: 0x0001AA94
		public string IdnHost
		{
			get
			{
				string text = this.DnsSafeHost;
				if (this.HostType == Uri.Flags.DnsHostType)
				{
					text = DomainNameHelper.IdnEquivalent(text);
				}
				return text;
			}
		}

		/// <summary>Gets whether the <see cref="T:System.Uri" /> instance is absolute.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> instance is absolute; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001C8BE File Offset: 0x0001AABE
		public bool IsAbsoluteUri
		{
			get
			{
				return this.m_Syntax != null;
			}
		}

		/// <summary>Indicates that the URI string was completely escaped before the <see cref="T:System.Uri" /> instance was created.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <paramref name="dontEscape" /> parameter was set to true when the <see cref="T:System.Uri" /> instance was created; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001C8C9 File Offset: 0x0001AAC9
		public bool UserEscaped
		{
			get
			{
				return this.InFact(Uri.Flags.UserEscaped);
			}
		}

		/// <summary>Gets the user name, password, or other user-specific information associated with the specified URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user information associated with the URI. The returned value does not include the '@' character reserved for delimiting the user information part of the URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001C8D7 File Offset: 0x0001AAD7
		public string UserInfo
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
				}
				return this.GetParts(UriComponents.UserInfo, UriFormat.UriEscaped);
			}
		}

		/// <summary>Determines whether the specified host name is a valid DNS name.</summary>
		/// <returns>A <see cref="T:System.UriHostNameType" /> that indicates the type of the host name. If the type of the host name cannot be determined or if the host name is null or a zero-length string, this method returns <see cref="F:System.UriHostNameType.Unknown" />.</returns>
		/// <param name="name">The host name to validate. This can be an IPv4 or IPv6 address or an Internet host name. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public unsafe static UriHostNameType CheckHostName(string name)
		{
			if (name == null || name.Length == 0 || name.Length > 32767)
			{
				return UriHostNameType.Unknown;
			}
			int num = name.Length;
			fixed (string text = name)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (name[0] == '[' && name[name.Length - 1] == ']' && IPv6AddressHelper.IsValid(ptr, 1, ref num) && num == name.Length)
				{
					return UriHostNameType.IPv6;
				}
				num = name.Length;
				if (IPv4AddressHelper.IsValid(ptr, 0, ref num, false, false, false) && num == name.Length)
				{
					return UriHostNameType.IPv4;
				}
				num = name.Length;
				bool flag = false;
				if (DomainNameHelper.IsValid(ptr, 0, ref num, ref flag, false) && num == name.Length)
				{
					return UriHostNameType.Dns;
				}
				num = name.Length;
				flag = false;
				if (DomainNameHelper.IsValidByIri(ptr, 0, ref num, ref flag, false) && num == name.Length)
				{
					return UriHostNameType.Dns;
				}
			}
			num = name.Length + 2;
			name = "[" + name + "]";
			fixed (string text = name)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (IPv6AddressHelper.IsValid(ptr2, 1, ref num) && num == name.Length)
				{
					return UriHostNameType.IPv6;
				}
			}
			return UriHostNameType.Unknown;
		}

		/// <summary>Gets the specified portion of a <see cref="T:System.Uri" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the specified portion of the <see cref="T:System.Uri" /> instance.</returns>
		/// <param name="part">One of the <see cref="T:System.UriPartial" /> values that specifies the end of the URI portion to return. </param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance. </exception>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="part" /> is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		public string GetLeftPart(UriPartial part)
		{
			if (this.IsNotAbsoluteUri)
			{
				throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
			}
			this.EnsureUriInfo();
			switch (part)
			{
			case UriPartial.Scheme:
				return this.GetParts(UriComponents.Scheme | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
			case UriPartial.Authority:
				if (this.NotAny(Uri.Flags.AuthorityFound) || this.IsDosPath)
				{
					return string.Empty;
				}
				return this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port, UriFormat.UriEscaped);
			case UriPartial.Path:
				return this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
			case UriPartial.Query:
				return this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.UriEscaped);
			default:
				throw new ArgumentException("part");
			}
		}

		/// <summary>Converts a specified character into its hexadecimal equivalent.</summary>
		/// <returns>The hexadecimal representation of the specified character.</returns>
		/// <param name="character">The character to convert to hexadecimal representation. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="character" /> is greater than 255. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			char[] array = new char[3];
			int num = 0;
			UriHelper.EscapeAsciiChar(character, array, ref num);
			return new string(array);
		}

		/// <summary>Converts a specified hexadecimal representation of a character to the character.</summary>
		/// <returns>The character represented by the hexadecimal encoding at position <paramref name="index" />. If the character at <paramref name="index" /> is not hexadecimal encoded, the character at <paramref name="index" /> is returned. The value of <paramref name="index" /> is incremented to point to the character following the one returned.</returns>
		/// <param name="pattern">The hexadecimal representation of a character. </param>
		/// <param name="index">The location in <paramref name="pattern" /> where the hexadecimal representation of a character begins. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to the number of characters in <paramref name="pattern" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		public static char HexUnescape(string pattern, ref int index)
		{
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (pattern[index] == '%' && pattern.Length - index >= 3)
			{
				char c = UriHelper.EscapedAscii(pattern[index + 1], pattern[index + 2]);
				if (c != '\uffff')
				{
					index += 3;
					return c;
				}
			}
			int num = index;
			index = num + 1;
			return pattern[num];
		}

		/// <summary>Determines whether a character in a string is hexadecimal encoded.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if <paramref name="pattern" /> is hexadecimal encoded at the specified location; otherwise, false.</returns>
		/// <param name="pattern">The string to check. </param>
		/// <param name="index">The location in <paramref name="pattern" /> to check for hexadecimal encoding. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public static bool IsHexEncoding(string pattern, int index)
		{
			return pattern.Length - index >= 3 && (pattern[index] == '%' && UriHelper.EscapedAscii(pattern[index + 1], pattern[index + 2]) != char.MaxValue);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001CB9F File Offset: 0x0001AD9F
		internal static bool IsGenDelim(char ch)
		{
			return ch == ':' || ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']' || ch == '@';
		}

		/// <summary>Determines whether the specified scheme name is valid.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the scheme name is valid; otherwise, false.</returns>
		/// <param name="schemeName">The scheme name to validate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		public static bool CheckSchemeName(string schemeName)
		{
			if (schemeName == null || schemeName.Length == 0 || !Uri.IsAsciiLetter(schemeName[0]))
			{
				return false;
			}
			for (int i = schemeName.Length - 1; i > 0; i--)
			{
				if (!Uri.IsAsciiLetterOrDigit(schemeName[i]) && schemeName[i] != '+' && schemeName[i] != '-' && schemeName[i] != '.')
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a specified character is a valid hexadecimal digit.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the character is a valid hexadecimal digit; otherwise false.</returns>
		/// <param name="character">The character to validate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001CC35 File Offset: 0x0001AE35
		public static bool IsHexDigit(char character)
		{
			return (character >= '0' && character <= '9') || (character >= 'A' && character <= 'F') || (character >= 'a' && character <= 'f');
		}

		/// <summary>Gets the decimal value of a hexadecimal digit.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a number from 0 to 15 that corresponds to the specified hexadecimal digit.</returns>
		/// <param name="digit">The hexadecimal digit (0-9, a-f, A-F) to convert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="digit" /> is not a valid hexadecimal digit (0-9, a-f, A-F). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		public static int FromHex(char digit)
		{
			if ((digit < '0' || digit > '9') && (digit < 'A' || digit > 'F') && (digit < 'a' || digit > 'f'))
			{
				throw new ArgumentException("digit");
			}
			if (digit > '9')
			{
				return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
			}
			return (int)(digit - '0');
		}

		/// <summary>Gets the hash code for the URI.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the hash value generated for this URI.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override int GetHashCode()
		{
			if (this.IsNotAbsoluteUri)
			{
				return Uri.CalculateCaseInsensitiveHashCode(this.OriginalString);
			}
			Uri.UriInfo uriInfo = this.EnsureUriInfo();
			if (uriInfo.MoreInfo == null)
			{
				uriInfo.MoreInfo = new Uri.MoreInfo();
			}
			int num = uriInfo.MoreInfo.Hash;
			if (num == 0)
			{
				string text = uriInfo.MoreInfo.RemoteUrl;
				if (text == null)
				{
					text = this.GetParts(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
				}
				num = Uri.CalculateCaseInsensitiveHashCode(text);
				if (num == 0)
				{
					num = 16777216;
				}
				uriInfo.MoreInfo.Hash = num;
			}
			return num;
		}

		/// <summary>Gets a canonical string representation for the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the unescaped canonical representation of the <see cref="T:System.Uri" /> instance. All characters are unescaped except #, ?, and %.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001CD30 File Offset: 0x0001AF30
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override string ToString()
		{
			if (this.m_Syntax != null)
			{
				this.EnsureUriInfo();
				if (this.m_Info.String == null)
				{
					if (this.Syntax.IsSimple)
					{
						this.m_Info.String = this.GetComponentsHelper(UriComponents.AbsoluteUri, (UriFormat)32767);
					}
					else
					{
						this.m_Info.String = this.GetParts(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
					}
				}
				return this.m_Info.String;
			}
			if (!this.m_iriParsing || !this.InFact(Uri.Flags.HasUnicode))
			{
				return this.OriginalString;
			}
			return this.m_String;
		}

		/// <summary>Determines whether two <see cref="T:System.Uri" /> instances have the same value.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> instances are equivalent; otherwise, false.</returns>
		/// <param name="uri1">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri2" />. </param>
		/// <param name="uri2">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri1" />. </param>
		/// <filterpriority>3</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001CDC6 File Offset: 0x0001AFC6
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool operator ==(Uri uri1, Uri uri2)
		{
			return uri1 == uri2 || (uri1 != null && uri2 != null && uri2.Equals(uri1));
		}

		/// <summary>Determines whether two <see cref="T:System.Uri" /> instances do not have the same value.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the two <see cref="T:System.Uri" /> instances are not equal; otherwise, false. If either parameter is null, this method returns true.</returns>
		/// <param name="uri1">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri2" />. </param>
		/// <param name="uri2">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri1" />. </param>
		/// <filterpriority>3</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001CDDD File Offset: 0x0001AFDD
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool operator !=(Uri uri1, Uri uri2)
		{
			return uri1 != uri2 && (uri1 == null || uri2 == null || !uri2.Equals(uri1));
		}

		/// <summary>Compares two <see cref="T:System.Uri" /> instances for equality.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the two instances represent the same URI; otherwise, false.</returns>
		/// <param name="comparand">The <see cref="T:System.Uri" /> instance or a URI identifier to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public unsafe override bool Equals(object comparand)
		{
			if (comparand == null)
			{
				return false;
			}
			if (this == comparand)
			{
				return true;
			}
			Uri uri = comparand as Uri;
			if (uri == null)
			{
				string text = comparand as string;
				if (text == null)
				{
					return false;
				}
				if (!Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri))
				{
					return false;
				}
			}
			if (this.m_String == uri.m_String)
			{
				return true;
			}
			if (this.IsAbsoluteUri != uri.IsAbsoluteUri)
			{
				return false;
			}
			if (this.IsNotAbsoluteUri)
			{
				return this.OriginalString.Equals(uri.OriginalString);
			}
			if (this.NotAny((Uri.Flags)(-2147483648)) || uri.NotAny((Uri.Flags)(-2147483648)))
			{
				if (!this.IsUncOrDosPath)
				{
					if (this.m_String.Length == uri.m_String.Length)
					{
						fixed (string text2 = this.m_String)
						{
							char* ptr = text2;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text3 = uri.m_String)
							{
								char* ptr2 = text3;
								if (ptr2 != null)
								{
									ptr2 += RuntimeHelpers.OffsetToStringData / 2;
								}
								int num = this.m_String.Length - 1;
								while (num >= 0 && ptr[num] == ptr2[num])
								{
									num--;
								}
								if (num == -1)
								{
									return true;
								}
							}
						}
					}
				}
				else if (string.Compare(this.m_String, uri.m_String, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			this.EnsureUriInfo();
			uri.EnsureUriInfo();
			if (!this.UserDrivenParsing && !uri.UserDrivenParsing && this.Syntax.IsSimple && uri.Syntax.IsSimple)
			{
				if (this.InFact(Uri.Flags.CanonicalDnsHost) && uri.InFact(Uri.Flags.CanonicalDnsHost))
				{
					ushort num2 = this.m_Info.Offset.Host;
					ushort num3 = this.m_Info.Offset.Path;
					ushort num4 = uri.m_Info.Offset.Host;
					ushort path = uri.m_Info.Offset.Path;
					string @string = uri.m_String;
					if (num3 - num2 > path - num4)
					{
						num3 = num2 + path - num4;
					}
					while (num2 < num3)
					{
						if (this.m_String[(int)num2] != @string[(int)num4])
						{
							return false;
						}
						if (@string[(int)num4] == ':')
						{
							break;
						}
						num2 += 1;
						num4 += 1;
					}
					if (num2 < this.m_Info.Offset.Path && this.m_String[(int)num2] != ':')
					{
						return false;
					}
					if (num4 < path && @string[(int)num4] != ':')
					{
						return false;
					}
				}
				else
				{
					this.EnsureHostString(false);
					uri.EnsureHostString(false);
					if (!this.m_Info.Host.Equals(uri.m_Info.Host))
					{
						return false;
					}
				}
				if (this.Port != uri.Port)
				{
					return false;
				}
			}
			Uri.UriInfo info = this.m_Info;
			Uri.UriInfo info2 = uri.m_Info;
			if (info.MoreInfo == null)
			{
				info.MoreInfo = new Uri.MoreInfo();
			}
			if (info2.MoreInfo == null)
			{
				info2.MoreInfo = new Uri.MoreInfo();
			}
			string text4 = info.MoreInfo.RemoteUrl;
			if (text4 == null)
			{
				text4 = this.GetParts(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
				info.MoreInfo.RemoteUrl = text4;
			}
			string text5 = info2.MoreInfo.RemoteUrl;
			if (text5 == null)
			{
				text5 = uri.GetParts(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
				info2.MoreInfo.RemoteUrl = text5;
			}
			if (this.IsUncOrDosPath)
			{
				return string.Compare(info.MoreInfo.RemoteUrl, info2.MoreInfo.RemoteUrl, this.IsUncOrDosPath ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
			}
			if (text4.Length != text5.Length)
			{
				return false;
			}
			fixed (string text2 = text4)
			{
				char* ptr3 = text2;
				if (ptr3 != null)
				{
					ptr3 += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text5)
				{
					char* ptr4 = text3;
					if (ptr4 != null)
					{
						ptr4 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr5 = ptr3 + text4.Length;
					char* ptr6 = ptr4 + text4.Length;
					while (ptr5 != ptr3)
					{
						if (*(--ptr5) != *(--ptr6))
						{
							return false;
						}
					}
					return true;
				}
			}
		}

		/// <summary>Determines the difference between two <see cref="T:System.Uri" /> instances.</summary>
		/// <returns>If the hostname and scheme of this URI instance and <paramref name="uri" /> are the same, then this method returns a relative <see cref="T:System.Uri" /> that, when appended to the current URI instance, yields <paramref name="uri" />.If the hostname or scheme is different, then this method returns a <see cref="T:System.Uri" />  that represents the <paramref name="uri" /> parameter.</returns>
		/// <param name="uri">The URI to compare to the current URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
		public Uri MakeRelativeUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (this.IsNotAbsoluteUri || uri.IsNotAbsoluteUri)
			{
				throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
			}
			if (this.Scheme == uri.Scheme && this.Host == uri.Host && this.Port == uri.Port)
			{
				string absolutePath = uri.AbsolutePath;
				string text = Uri.PathDifference(this.AbsolutePath, absolutePath, !this.IsUncOrDosPath);
				if (Uri.CheckForColonInFirstPathSegment(text) && (!uri.IsDosPath || !absolutePath.Equals(text, StringComparison.Ordinal)))
				{
					text = "./" + text;
				}
				text += uri.GetParts(UriComponents.Query | UriComponents.Fragment, UriFormat.UriEscaped);
				return new Uri(text, UriKind.Relative);
			}
			return uri;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		private static bool CheckForColonInFirstPathSegment(string uriString)
		{
			char[] array = new char[] { ':', '\\', '/', '?', '#' };
			int num = uriString.IndexOfAny(array);
			return num >= 0 && uriString[num] == ':';
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		internal static string InternalEscapeString(string rawString)
		{
			if (rawString == null)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = UriHelper.EscapeString(rawString, 0, rawString.Length, null, ref num, true, '?', '#', '%');
			if (array == null)
			{
				return rawString;
			}
			return new string(array, 0, num);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001D334 File Offset: 0x0001B534
		private unsafe static ParsingError ParseScheme(string uriString, ref Uri.Flags flags, ref UriParser syntax)
		{
			int length = uriString.Length;
			if (length == 0)
			{
				return ParsingError.EmptyUriString;
			}
			if (length >= 65520)
			{
				return ParsingError.SizeLimit;
			}
			fixed (string text = uriString)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				ParsingError parsingError = ParsingError.None;
				ushort num = Uri.ParseSchemeCheckImplicitFile(ptr, (ushort)length, ref parsingError, ref flags, ref syntax);
				if (parsingError != ParsingError.None)
				{
					return parsingError;
				}
				flags |= (Uri.Flags)num;
			}
			return ParsingError.None;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001D388 File Offset: 0x0001B588
		internal UriFormatException ParseMinimal()
		{
			ParsingError parsingError = this.PrivateParseMinimal();
			if (parsingError == ParsingError.None)
			{
				return null;
			}
			this.m_Flags |= Uri.Flags.ErrorOrParsingRecursion;
			return Uri.GetException(parsingError);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		private unsafe ParsingError PrivateParseMinimal()
		{
			ushort num = (ushort)(this.m_Flags & Uri.Flags.IndexMask);
			ushort num2 = (ushort)this.m_String.Length;
			string text = null;
			this.m_Flags &= ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath | Uri.Flags.UserDrivenParsing);
			fixed (string text2 = ((this.m_iriParsing && (this.m_Flags & Uri.Flags.HasUnicode) != Uri.Flags.Zero && (this.m_Flags & Uri.Flags.HostUnicodeNormalized) == Uri.Flags.Zero) ? this.m_originalUnicodeString : this.m_String))
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (num2 > num && Uri.IsLWS(ptr[num2 - 1]))
				{
					num2 -= 1;
					while (num2 != num && Uri.IsLWS(ptr[(IntPtr)(num2 -= 1) * 2]))
					{
					}
					num2 += 1;
				}
				if (this.m_Syntax.IsAllSet(UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowDOSPath) && this.NotAny(Uri.Flags.ImplicitFile) && num + 1 < num2)
				{
					ushort num3 = num;
					char c;
					while (num3 < num2 && ((c = ptr[num3]) == '\\' || c == '/'))
					{
						num3 += 1;
					}
					if (this.m_Syntax.InFact(UriSyntaxFlags.FileLikeUri) || num3 - num <= 3)
					{
						if (num3 - num >= 2)
						{
							this.m_Flags |= Uri.Flags.AuthorityFound;
						}
						if (num3 + 1 < num2 && ((c = ptr[num3 + 1]) == ':' || c == '|') && Uri.IsAsciiLetter(ptr[num3]))
						{
							if (num3 + 2 >= num2 || ((c = ptr[num3 + 2]) != '\\' && c != '/'))
							{
								if (this.m_Syntax.InFact(UriSyntaxFlags.FileLikeUri))
								{
									return ParsingError.MustRootedPath;
								}
							}
							else
							{
								this.m_Flags |= Uri.Flags.DosPath;
								if (this.m_Syntax.InFact(UriSyntaxFlags.MustHaveAuthority))
								{
									this.m_Flags |= Uri.Flags.AuthorityFound;
								}
								if (num3 != num && num3 - num != 2)
								{
									num = num3 - 1;
								}
								else
								{
									num = num3;
								}
							}
						}
						else if (this.m_Syntax.InFact(UriSyntaxFlags.FileLikeUri) && num3 - num >= 2 && num3 - num != 3 && num3 < num2 && ptr[num3] != '?' && ptr[num3] != '#')
						{
							if (!Uri.IsWindowsFileSystem)
							{
								if (num3 - num > 3)
								{
									this.m_Flags |= Uri.Flags.CompressedSlashes;
									num = num3;
								}
							}
							else
							{
								this.m_Flags |= Uri.Flags.UncPath;
								num = num3;
							}
						}
					}
				}
				if ((this.m_Flags & (Uri.Flags.DosPath | Uri.Flags.UncPath)) == Uri.Flags.Zero && (Uri.IsWindowsFileSystem || (this.m_Flags & (Uri.Flags.ImplicitFile | Uri.Flags.CompressedSlashes)) == Uri.Flags.Zero))
				{
					if (num + 2 <= num2)
					{
						char c2 = ptr[num];
						char c3 = ptr[num + 1];
						if (this.m_Syntax.InFact(UriSyntaxFlags.MustHaveAuthority))
						{
							if ((!Uri.IsWindowsFileSystem || (c2 != '/' && c2 != '\\') || (c3 != '/' && c3 != '\\')) && (Uri.IsWindowsFileSystem || c2 != '/' || c3 != '/'))
							{
								return ParsingError.BadAuthority;
							}
							this.m_Flags |= Uri.Flags.AuthorityFound;
							num += 2;
						}
						else if (this.m_Syntax.InFact(UriSyntaxFlags.OptionalAuthority) && (this.InFact(Uri.Flags.AuthorityFound) || (c2 == '/' && c3 == '/')))
						{
							this.m_Flags |= Uri.Flags.AuthorityFound;
							num += 2;
						}
						else if (this.m_Syntax.NotAny(UriSyntaxFlags.MailToLikeUri))
						{
							this.m_Flags |= (Uri.Flags)num | Uri.Flags.HostTypeMask;
							return ParsingError.None;
						}
					}
					else
					{
						if (this.m_Syntax.InFact(UriSyntaxFlags.MustHaveAuthority))
						{
							return ParsingError.BadAuthority;
						}
						if (this.m_Syntax.NotAny(UriSyntaxFlags.MailToLikeUri))
						{
							this.m_Flags |= (Uri.Flags)num | Uri.Flags.HostTypeMask;
							return ParsingError.None;
						}
					}
				}
				if (this.InFact(Uri.Flags.DosPath))
				{
					this.m_Flags |= (((this.m_Flags & Uri.Flags.AuthorityFound) != Uri.Flags.Zero) ? Uri.Flags.BasicHostType : Uri.Flags.HostTypeMask);
					this.m_Flags |= (Uri.Flags)num;
					return ParsingError.None;
				}
				ParsingError parsingError = ParsingError.None;
				num = this.CheckAuthorityHelper(ptr, num, num2, ref parsingError, ref this.m_Flags, this.m_Syntax, ref text);
				if (parsingError != ParsingError.None)
				{
					return parsingError;
				}
				if (num < num2 && ptr[num] == '\\' && this.NotAny(Uri.Flags.ImplicitFile) && this.m_Syntax.NotAny(UriSyntaxFlags.AllowDOSPath))
				{
					return ParsingError.BadAuthorityTerminator;
				}
				this.m_Flags |= (Uri.Flags)num;
			}
			if (Uri.s_IdnScope != UriIdnScope.None || this.m_iriParsing)
			{
				this.PrivateParseMinimalIri(text, num);
			}
			return ParsingError.None;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001D858 File Offset: 0x0001BA58
		private void PrivateParseMinimalIri(string newHost, ushort idx)
		{
			if (newHost != null)
			{
				this.m_String = newHost;
			}
			if ((!this.m_iriParsing && this.AllowIdn && ((this.m_Flags & Uri.Flags.IdnHost) != Uri.Flags.Zero || (this.m_Flags & Uri.Flags.UnicodeHost) != Uri.Flags.Zero)) || (this.m_iriParsing && (this.m_Flags & Uri.Flags.HasUnicode) == Uri.Flags.Zero && this.AllowIdn && (this.m_Flags & Uri.Flags.IdnHost) != Uri.Flags.Zero))
			{
				this.m_Flags &= ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath);
				this.m_Flags |= (Uri.Flags)((long)this.m_String.Length);
				this.m_String += this.m_originalUnicodeString.Substring((int)idx, this.m_originalUnicodeString.Length - (int)idx);
			}
			if (this.m_iriParsing && (this.m_Flags & Uri.Flags.HasUnicode) != Uri.Flags.Zero)
			{
				this.m_Flags |= Uri.Flags.UseOrigUncdStrOffset;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001D960 File Offset: 0x0001BB60
		private unsafe void CreateUriInfo(Uri.Flags cF)
		{
			Uri.UriInfo uriInfo = new Uri.UriInfo();
			uriInfo.Offset.End = (ushort)this.m_String.Length;
			if (!this.UserDrivenParsing)
			{
				bool flag = false;
				ushort num;
				if ((cF & Uri.Flags.ImplicitFile) != Uri.Flags.Zero)
				{
					num = 0;
					while (num < uriInfo.Offset.End && Uri.IsLWS(this.m_String[(int)num]))
					{
						num += 1;
						Uri.UriInfo uriInfo2 = uriInfo;
						uriInfo2.Offset.Scheme = uriInfo2.Offset.Scheme + 1;
					}
					if (Uri.StaticInFact(cF, Uri.Flags.UncPath))
					{
						for (num += 2; num < (ushort)(cF & Uri.Flags.IndexMask); num += 1)
						{
							if (this.m_String[(int)num] != '/' && this.m_String[(int)num] != '\\')
							{
								break;
							}
						}
					}
				}
				else
				{
					num = (ushort)this.m_Syntax.SchemeName.Length;
					for (;;)
					{
						string @string = this.m_String;
						ushort num2 = num;
						num = num2 + 1;
						if (@string[(int)num2] == ':')
						{
							break;
						}
						Uri.UriInfo uriInfo3 = uriInfo;
						uriInfo3.Offset.Scheme = uriInfo3.Offset.Scheme + 1;
					}
					if ((cF & Uri.Flags.AuthorityFound) != Uri.Flags.Zero)
					{
						if (this.m_String[(int)num] == '\\' || this.m_String[(int)(num + 1)] == '\\')
						{
							flag = true;
						}
						num += 2;
						if ((cF & (Uri.Flags.DosPath | Uri.Flags.UncPath | Uri.Flags.CompressedSlashes)) != Uri.Flags.Zero)
						{
							while (num < (ushort)(cF & Uri.Flags.IndexMask) && (this.m_String[(int)num] == '/' || this.m_String[(int)num] == '\\'))
							{
								flag = true;
								num += 1;
							}
						}
					}
				}
				if (this.m_Syntax.DefaultPort != -1)
				{
					uriInfo.Offset.PortValue = (ushort)this.m_Syntax.DefaultPort;
				}
				if ((cF & Uri.Flags.HostTypeMask) == Uri.Flags.HostTypeMask || Uri.StaticInFact(cF, Uri.Flags.DosPath))
				{
					uriInfo.Offset.User = (ushort)(cF & Uri.Flags.IndexMask);
					uriInfo.Offset.Host = uriInfo.Offset.User;
					uriInfo.Offset.Path = uriInfo.Offset.User;
					cF &= ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath);
					if (flag)
					{
						cF |= Uri.Flags.SchemeNotCanonical;
					}
				}
				else
				{
					uriInfo.Offset.User = num;
					if (this.HostType == Uri.Flags.BasicHostType)
					{
						uriInfo.Offset.Host = num;
						uriInfo.Offset.Path = (ushort)(cF & Uri.Flags.IndexMask);
						cF &= ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath);
					}
					else
					{
						if ((cF & Uri.Flags.HasUserInfo) != Uri.Flags.Zero)
						{
							while (this.m_String[(int)num] != '@')
							{
								num += 1;
							}
							num += 1;
							uriInfo.Offset.Host = num;
						}
						else
						{
							uriInfo.Offset.Host = num;
						}
						num = (ushort)(cF & Uri.Flags.IndexMask);
						cF &= ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath);
						if (flag)
						{
							cF |= Uri.Flags.SchemeNotCanonical;
						}
						uriInfo.Offset.Path = num;
						bool flag2 = false;
						bool flag3 = (cF & Uri.Flags.UseOrigUncdStrOffset) > Uri.Flags.Zero;
						cF &= ~Uri.Flags.UseOrigUncdStrOffset;
						if (flag3)
						{
							uriInfo.Offset.End = (ushort)this.m_originalUnicodeString.Length;
						}
						if (num < uriInfo.Offset.End)
						{
							fixed (string text = (flag3 ? this.m_originalUnicodeString : this.m_String))
							{
								char* ptr = text;
								if (ptr != null)
								{
									ptr += RuntimeHelpers.OffsetToStringData / 2;
								}
								if (ptr[num] == ':')
								{
									int num3 = 0;
									if ((num += 1) < uriInfo.Offset.End)
									{
										num3 = (int)((ushort)(ptr[num] - '0'));
										if (num3 != 65535 && num3 != 15 && num3 != 65523)
										{
											flag2 = true;
											if (num3 == 0)
											{
												cF |= Uri.Flags.PortNotCanonical | Uri.Flags.E_PortNotCanonical;
											}
											for (num += 1; num < uriInfo.Offset.End; num += 1)
											{
												ushort num4 = (ushort)(ptr[num] - '0');
												if (num4 == 65535 || num4 == 15 || num4 == 65523)
												{
													break;
												}
												num3 = num3 * 10 + (int)num4;
											}
										}
									}
									if (flag2 && uriInfo.Offset.PortValue != (ushort)num3)
									{
										uriInfo.Offset.PortValue = (ushort)num3;
										cF |= Uri.Flags.NotDefaultPort;
									}
									else
									{
										cF |= Uri.Flags.PortNotCanonical | Uri.Flags.E_PortNotCanonical;
									}
									uriInfo.Offset.Path = num;
								}
							}
						}
					}
				}
			}
			cF |= Uri.Flags.MinimalUriInfoSet;
			uriInfo.DnsSafeHost = this.m_DnsSafeHost;
			string string2 = this.m_String;
			lock (string2)
			{
				if ((this.m_Flags & Uri.Flags.MinimalUriInfoSet) == Uri.Flags.Zero)
				{
					this.m_Info = uriInfo;
					this.m_Flags = (this.m_Flags & ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath)) | cF;
				}
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001DE04 File Offset: 0x0001C004
		private unsafe void CreateHostString()
		{
			Uri.UriInfo uriInfo;
			if (!this.m_Syntax.IsSimple)
			{
				uriInfo = this.m_Info;
				lock (uriInfo)
				{
					if (this.NotAny(Uri.Flags.ErrorOrParsingRecursion))
					{
						this.m_Flags |= Uri.Flags.ErrorOrParsingRecursion;
						this.GetHostViaCustomSyntax();
						this.m_Flags &= ~Uri.Flags.ErrorOrParsingRecursion;
						return;
					}
				}
			}
			Uri.Flags flags = this.m_Flags;
			string text = Uri.CreateHostStringHelper(this.m_String, this.m_Info.Offset.Host, this.m_Info.Offset.Path, ref flags, ref this.m_Info.ScopeId);
			if (text.Length != 0)
			{
				if (this.HostType == Uri.Flags.BasicHostType)
				{
					ushort num = 0;
					Uri.Check check;
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						check = this.CheckCanonical(ptr, ref num, (ushort)text.Length, char.MaxValue);
					}
					if ((check & Uri.Check.DisplayCanonical) == Uri.Check.None && (this.NotAny(Uri.Flags.ImplicitFile) || (check & Uri.Check.ReservedFound) != Uri.Check.None))
					{
						flags |= Uri.Flags.HostNotCanonical;
					}
					if (this.InFact(Uri.Flags.ImplicitFile) && (check & (Uri.Check.EscapedCanonical | Uri.Check.ReservedFound)) != Uri.Check.None)
					{
						check &= ~Uri.Check.EscapedCanonical;
					}
					if ((check & (Uri.Check.EscapedCanonical | Uri.Check.BackslashInPath)) != Uri.Check.EscapedCanonical)
					{
						flags |= Uri.Flags.E_HostNotCanonical;
						if (this.NotAny(Uri.Flags.UserEscaped))
						{
							int num2 = 0;
							char[] array = UriHelper.EscapeString(text, 0, text.Length, null, ref num2, true, '?', '#', this.IsImplicitFile ? char.MaxValue : '%');
							if (array != null)
							{
								text = new string(array, 0, num2);
							}
						}
					}
				}
				else if (this.NotAny(Uri.Flags.CanonicalDnsHost))
				{
					if (this.m_Info.ScopeId != null)
					{
						flags |= Uri.Flags.HostNotCanonical | Uri.Flags.E_HostNotCanonical;
					}
					else
					{
						ushort num3 = 0;
						while ((int)num3 < text.Length)
						{
							if (this.m_Info.Offset.Host + num3 >= this.m_Info.Offset.End || text[(int)num3] != this.m_String[(int)(this.m_Info.Offset.Host + num3)])
							{
								flags |= Uri.Flags.HostNotCanonical | Uri.Flags.E_HostNotCanonical;
								break;
							}
							num3 += 1;
						}
					}
				}
			}
			this.m_Info.Host = text;
			uriInfo = this.m_Info;
			lock (uriInfo)
			{
				this.m_Flags |= flags;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001E098 File Offset: 0x0001C298
		private static string CreateHostStringHelper(string str, ushort idx, ushort end, ref Uri.Flags flags, ref string scopeId)
		{
			bool flag = false;
			Uri.Flags flags2 = flags & Uri.Flags.HostTypeMask;
			string text;
			if (flags2 <= Uri.Flags.DnsHostType)
			{
				if (flags2 == Uri.Flags.IPv6HostType)
				{
					text = IPv6AddressHelper.ParseCanonicalName(str, (int)idx, ref flag, ref scopeId);
					goto IL_00C4;
				}
				if (flags2 == Uri.Flags.IPv4HostType)
				{
					text = IPv4AddressHelper.ParseCanonicalName(str, (int)idx, (int)end, ref flag);
					goto IL_00C4;
				}
				if (flags2 == Uri.Flags.DnsHostType)
				{
					text = DomainNameHelper.ParseCanonicalName(str, (int)idx, (int)end, ref flag);
					goto IL_00C4;
				}
			}
			else
			{
				if (flags2 == Uri.Flags.UncHostType)
				{
					text = UncNameHelper.ParseCanonicalName(str, (int)idx, (int)end, ref flag);
					goto IL_00C4;
				}
				if (flags2 != Uri.Flags.BasicHostType)
				{
					if (flags2 == Uri.Flags.HostTypeMask)
					{
						text = string.Empty;
						goto IL_00C4;
					}
				}
				else
				{
					if (Uri.StaticInFact(flags, Uri.Flags.DosPath))
					{
						text = string.Empty;
					}
					else
					{
						text = str.Substring((int)idx, (int)(end - idx));
					}
					if (text.Length == 0)
					{
						flag = true;
						goto IL_00C4;
					}
					goto IL_00C4;
				}
			}
			throw Uri.GetException(ParsingError.BadHostName);
			IL_00C4:
			if (flag)
			{
				flags |= Uri.Flags.LoopbackHost;
			}
			return text;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001E178 File Offset: 0x0001C378
		private unsafe void GetHostViaCustomSyntax()
		{
			if (this.m_Info.Host != null)
			{
				return;
			}
			string text = this.m_Syntax.InternalGetComponents(this, UriComponents.Host, UriFormat.UriEscaped);
			if (this.m_Info.Host == null)
			{
				if (text.Length >= 65520)
				{
					throw Uri.GetException(ParsingError.SizeLimit);
				}
				ParsingError parsingError = ParsingError.None;
				Uri.Flags flags = this.m_Flags & ~Uri.Flags.HostTypeMask;
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					string text3 = null;
					if (this.CheckAuthorityHelper(ptr, 0, (ushort)text.Length, ref parsingError, ref flags, this.m_Syntax, ref text3) != (ushort)text.Length)
					{
						flags &= ~Uri.Flags.HostTypeMask;
						flags |= Uri.Flags.HostTypeMask;
					}
				}
				if (parsingError != ParsingError.None || (flags & Uri.Flags.HostTypeMask) == Uri.Flags.HostTypeMask)
				{
					this.m_Flags = (this.m_Flags & ~Uri.Flags.HostTypeMask) | Uri.Flags.BasicHostType;
				}
				else
				{
					text = Uri.CreateHostStringHelper(text, 0, (ushort)text.Length, ref flags, ref this.m_Info.ScopeId);
					ushort num = 0;
					while ((int)num < text.Length)
					{
						if (this.m_Info.Offset.Host + num >= this.m_Info.Offset.End || text[(int)num] != this.m_String[(int)(this.m_Info.Offset.Host + num)])
						{
							this.m_Flags |= Uri.Flags.HostNotCanonical | Uri.Flags.E_HostNotCanonical;
							break;
						}
						num += 1;
					}
					this.m_Flags = (this.m_Flags & ~Uri.Flags.HostTypeMask) | (flags & Uri.Flags.HostTypeMask);
				}
			}
			string text4 = this.m_Syntax.InternalGetComponents(this, UriComponents.StrongPort, UriFormat.UriEscaped);
			int num2 = 0;
			if (text4 == null || text4.Length == 0)
			{
				this.m_Flags &= ~Uri.Flags.NotDefaultPort;
				this.m_Flags |= Uri.Flags.PortNotCanonical | Uri.Flags.E_PortNotCanonical;
				this.m_Info.Offset.PortValue = 0;
			}
			else
			{
				for (int i = 0; i < text4.Length; i++)
				{
					int num3 = (int)(text4[i] - '0');
					if (num3 < 0 || num3 > 9 || (num2 = num2 * 10 + num3) > 65535)
					{
						throw new UriFormatException(global::SR.GetString("A derived type '{0}' has reported an invalid value for the Uri port '{1}'.", new object[]
						{
							this.m_Syntax.GetType().FullName,
							text4
						}));
					}
				}
				if (num2 != (int)this.m_Info.Offset.PortValue)
				{
					if (num2 == this.m_Syntax.DefaultPort)
					{
						this.m_Flags &= ~Uri.Flags.NotDefaultPort;
					}
					else
					{
						this.m_Flags |= Uri.Flags.NotDefaultPort;
					}
					this.m_Flags |= Uri.Flags.PortNotCanonical | Uri.Flags.E_PortNotCanonical;
					this.m_Info.Offset.PortValue = (ushort)num2;
				}
			}
			this.m_Info.Host = text;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001E452 File Offset: 0x0001C652
		internal string GetParts(UriComponents uriParts, UriFormat formatAs)
		{
			return this.GetComponents(uriParts, formatAs);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001E45C File Offset: 0x0001C65C
		private string GetEscapedParts(UriComponents uriParts)
		{
			ushort num = (ushort)(((ushort)this.m_Flags & 16256) >> 6);
			if (this.InFact(Uri.Flags.SchemeNotCanonical))
			{
				num |= 1;
			}
			if ((uriParts & UriComponents.Path) != (UriComponents)0)
			{
				if (this.InFact(Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath))
				{
					num |= 16;
				}
				else if (this.IsDosPath && this.m_String[(int)(this.m_Info.Offset.Path + this.SecuredPathIndex - 1)] == '|')
				{
					num |= 16;
				}
			}
			if (((ushort)uriParts & num) == 0)
			{
				string uriPartsFromUserString = this.GetUriPartsFromUserString(uriParts);
				if (uriPartsFromUserString != null)
				{
					return uriPartsFromUserString;
				}
			}
			return this.ReCreateParts(uriParts, num, UriFormat.UriEscaped);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		private string GetUnescapedParts(UriComponents uriParts, UriFormat formatAs)
		{
			ushort num = (ushort)this.m_Flags & 127;
			if ((uriParts & UriComponents.Path) != (UriComponents)0)
			{
				if ((this.m_Flags & (Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath)) != Uri.Flags.Zero)
				{
					num |= 16;
				}
				else if (this.IsDosPath && this.m_String[(int)(this.m_Info.Offset.Path + this.SecuredPathIndex - 1)] == '|')
				{
					num |= 16;
				}
			}
			if (((ushort)uriParts & num) == 0)
			{
				string uriPartsFromUserString = this.GetUriPartsFromUserString(uriParts);
				if (uriPartsFromUserString != null)
				{
					return uriPartsFromUserString;
				}
			}
			return this.ReCreateParts(uriParts, num, formatAs);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001E580 File Offset: 0x0001C780
		private unsafe string ReCreateParts(UriComponents parts, ushort nonCanonical, UriFormat formatAs)
		{
			this.EnsureHostString(false);
			string text = (((parts & UriComponents.Host) == (UriComponents)0) ? string.Empty : this.m_Info.Host);
			int num = (int)((this.m_Info.Offset.End - this.m_Info.Offset.User) * ((formatAs == UriFormat.UriEscaped) ? 12 : 1));
			char[] array = new char[text.Length + num + this.m_Syntax.SchemeName.Length + 3 + 1];
			num = 0;
			if ((parts & UriComponents.Scheme) != (UriComponents)0)
			{
				this.m_Syntax.SchemeName.CopyTo(0, array, num, this.m_Syntax.SchemeName.Length);
				num += this.m_Syntax.SchemeName.Length;
				if (parts != UriComponents.Scheme)
				{
					array[num++] = ':';
					if (this.InFact(Uri.Flags.AuthorityFound))
					{
						array[num++] = '/';
						array[num++] = '/';
					}
				}
			}
			if ((parts & UriComponents.UserInfo) != (UriComponents)0 && this.InFact(Uri.Flags.HasUserInfo))
			{
				if ((nonCanonical & 2) != 0)
				{
					switch (formatAs)
					{
					case UriFormat.UriEscaped:
						if (this.NotAny(Uri.Flags.UserEscaped))
						{
							array = UriHelper.EscapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, true, '?', '#', '%');
						}
						else
						{
							this.InFact(Uri.Flags.E_UserNotCanonical);
							this.m_String.CopyTo((int)this.m_Info.Offset.User, array, num, (int)(this.m_Info.Offset.Host - this.m_Info.Offset.User));
							num += (int)(this.m_Info.Offset.Host - this.m_Info.Offset.User);
						}
						break;
					case UriFormat.Unescaped:
						array = UriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, false);
						break;
					case UriFormat.SafeUnescaped:
						array = UriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)(this.m_Info.Offset.Host - 1), array, ref num, '@', '/', '\\', this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape, this.m_Syntax, false);
						array[num++] = '@';
						break;
					default:
						array = UriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
						break;
					}
				}
				else
				{
					UriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
				}
				if (parts == UriComponents.UserInfo)
				{
					num--;
				}
			}
			if ((parts & UriComponents.Host) != (UriComponents)0 && text.Length != 0)
			{
				UnescapeMode unescapeMode;
				if (formatAs != UriFormat.UriEscaped && this.HostType == Uri.Flags.BasicHostType && (nonCanonical & 4) != 0)
				{
					unescapeMode = ((formatAs == UriFormat.Unescaped) ? (UnescapeMode.Unescape | UnescapeMode.UnescapeAll) : (this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape));
				}
				else
				{
					unescapeMode = UnescapeMode.CopyOnly;
				}
				if ((parts & UriComponents.NormalizedHost) != (UriComponents)0)
				{
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						bool flag = false;
						bool flag2 = false;
						try
						{
							text = DomainNameHelper.UnicodeEquivalent(ptr, 0, text.Length, ref flag, ref flag2);
						}
						catch (UriFormatException)
						{
						}
					}
				}
				array = UriHelper.UnescapeString(text, 0, text.Length, array, ref num, '/', '?', '#', unescapeMode, this.m_Syntax, false);
				if ((parts & UriComponents.SerializationInfoString) != (UriComponents)0 && this.HostType == Uri.Flags.IPv6HostType && this.m_Info.ScopeId != null)
				{
					this.m_Info.ScopeId.CopyTo(0, array, num - 1, this.m_Info.ScopeId.Length);
					num += this.m_Info.ScopeId.Length;
					array[num - 1] = ']';
				}
			}
			if ((parts & UriComponents.Port) != (UriComponents)0)
			{
				if ((nonCanonical & 8) == 0)
				{
					if (this.InFact(Uri.Flags.NotDefaultPort))
					{
						ushort num2 = this.m_Info.Offset.Path;
						while (this.m_String[(int)(num2 -= 1)] != ':')
						{
						}
						this.m_String.CopyTo((int)num2, array, num, (int)(this.m_Info.Offset.Path - num2));
						num += (int)(this.m_Info.Offset.Path - num2);
					}
					else if ((parts & UriComponents.StrongPort) != (UriComponents)0 && this.m_Syntax.DefaultPort != -1)
					{
						array[num++] = ':';
						text = this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
						text.CopyTo(0, array, num, text.Length);
						num += text.Length;
					}
				}
				else if (this.InFact(Uri.Flags.NotDefaultPort) || ((parts & UriComponents.StrongPort) != (UriComponents)0 && this.m_Syntax.DefaultPort != -1))
				{
					array[num++] = ':';
					text = this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
					text.CopyTo(0, array, num, text.Length);
					num += text.Length;
				}
			}
			if ((parts & UriComponents.Path) != (UriComponents)0)
			{
				array = this.GetCanonicalPath(array, ref num, formatAs);
				if (parts == UriComponents.Path)
				{
					ushort num3;
					if (this.InFact(Uri.Flags.AuthorityFound) && num != 0 && array[0] == '/')
					{
						num3 = 1;
						num--;
					}
					else
					{
						num3 = 0;
					}
					if (num != 0)
					{
						return new string(array, (int)num3, num);
					}
					return string.Empty;
				}
			}
			if ((parts & UriComponents.Query) != (UriComponents)0 && this.m_Info.Offset.Query < this.m_Info.Offset.Fragment)
			{
				ushort num3 = this.m_Info.Offset.Query + 1;
				if (parts != UriComponents.Query)
				{
					array[num++] = '?';
				}
				if ((nonCanonical & 32) != 0)
				{
					if (formatAs != UriFormat.UriEscaped)
					{
						if (formatAs != UriFormat.Unescaped)
						{
							if (formatAs != (UriFormat)32767)
							{
								array = UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, '#', char.MaxValue, char.MaxValue, this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape, this.m_Syntax, true);
							}
							else
							{
								array = UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, '#', char.MaxValue, char.MaxValue, (this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape) | UnescapeMode.V1ToStringFlag, this.m_Syntax, true);
							}
						}
						else
						{
							array = UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, '#', char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, true);
						}
					}
					else if (this.NotAny(Uri.Flags.UserEscaped))
					{
						array = UriHelper.EscapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, true, '#', char.MaxValue, '%');
					}
					else
					{
						UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, true);
					}
				}
				else
				{
					UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, true);
				}
			}
			if ((parts & UriComponents.Fragment) != (UriComponents)0 && this.m_Info.Offset.Fragment < this.m_Info.Offset.End)
			{
				ushort num3 = this.m_Info.Offset.Fragment + 1;
				if (parts != UriComponents.Fragment)
				{
					array[num++] = '#';
				}
				if ((nonCanonical & 64) != 0)
				{
					if (formatAs != UriFormat.UriEscaped)
					{
						if (formatAs != UriFormat.Unescaped)
						{
							if (formatAs != (UriFormat)32767)
							{
								array = UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, '#', char.MaxValue, char.MaxValue, this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape, this.m_Syntax, false);
							}
							else
							{
								array = UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, '#', char.MaxValue, char.MaxValue, (this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape) | UnescapeMode.V1ToStringFlag, this.m_Syntax, false);
							}
						}
						else
						{
							array = UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, '#', char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, false);
						}
					}
					else if (this.NotAny(Uri.Flags.UserEscaped))
					{
						array = UriHelper.EscapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, true, UriParser.ShouldUseLegacyV2Quirks ? '#' : char.MaxValue, char.MaxValue, '%');
					}
					else
					{
						UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
					}
				}
				else
				{
					UriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001EF34 File Offset: 0x0001D134
		private string GetUriPartsFromUserString(UriComponents uriParts)
		{
			UriComponents uriComponents = uriParts & ~UriComponents.KeepDelimiter;
			if (uriComponents <= UriComponents.Fragment)
			{
				if (uriComponents <= UriComponents.Path)
				{
					switch (uriComponents)
					{
					case UriComponents.Scheme:
						if (uriParts != UriComponents.Scheme)
						{
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme));
						}
						return this.m_Syntax.SchemeName;
					case UriComponents.UserInfo:
					{
						if (this.NotAny(Uri.Flags.HasUserInfo))
						{
							return string.Empty;
						}
						ushort num;
						if (uriParts == UriComponents.UserInfo)
						{
							num = this.m_Info.Offset.Host - 1;
						}
						else
						{
							num = this.m_Info.Offset.Host;
						}
						if (this.m_Info.Offset.User >= num)
						{
							return string.Empty;
						}
						return this.m_String.Substring((int)this.m_Info.Offset.User, (int)(num - this.m_Info.Offset.User));
					}
					case UriComponents.Scheme | UriComponents.UserInfo:
						goto IL_0992;
					case UriComponents.Host:
					{
						ushort num2 = this.m_Info.Offset.Path;
						if (this.InFact(Uri.Flags.PortNotCanonical | Uri.Flags.NotDefaultPort))
						{
							while (this.m_String[(int)(num2 -= 1)] != ':')
							{
							}
						}
						if (num2 - this.m_Info.Offset.Host != 0)
						{
							return this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(num2 - this.m_Info.Offset.Host));
						}
						return string.Empty;
					}
					default:
						switch (uriComponents)
						{
						case UriComponents.SchemeAndServer:
							if (!this.InFact(Uri.Flags.HasUserInfo))
							{
								return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Scheme));
							}
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme)) + this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Host));
						case UriComponents.UserInfo | UriComponents.Host | UriComponents.Port:
							break;
						case UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port:
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Scheme));
						case UriComponents.Path:
						{
							ushort num;
							if (uriParts == UriComponents.Path && this.InFact(Uri.Flags.AuthorityFound) && this.m_Info.Offset.End > this.m_Info.Offset.Path && this.m_String[(int)this.m_Info.Offset.Path] == '/')
							{
								num = this.m_Info.Offset.Path + 1;
							}
							else
							{
								num = this.m_Info.Offset.Path;
							}
							if (num >= this.m_Info.Offset.Query)
							{
								return string.Empty;
							}
							return this.m_String.Substring((int)num, (int)(this.m_Info.Offset.Query - num));
						}
						default:
							goto IL_0992;
						}
						break;
					}
				}
				else if (uriComponents != UriComponents.Query)
				{
					if (uriComponents == UriComponents.PathAndQuery)
					{
						return this.m_String.Substring((int)this.m_Info.Offset.Path, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Path));
					}
					switch (uriComponents)
					{
					case UriComponents.HttpRequestUrl:
						if (this.InFact(Uri.Flags.HasUserInfo))
						{
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme)) + this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Host));
						}
						if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.Fragment == this.m_String.Length)
						{
							return this.m_String;
						}
						return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Scheme));
					case UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query:
						goto IL_0992;
					case UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query:
						if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.Fragment == this.m_String.Length)
						{
							return this.m_String;
						}
						return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Scheme));
					case UriComponents.Fragment:
					{
						ushort num;
						if (uriParts == UriComponents.Fragment)
						{
							num = this.m_Info.Offset.Fragment + 1;
						}
						else
						{
							num = this.m_Info.Offset.Fragment;
						}
						if (num >= this.m_Info.Offset.End)
						{
							return string.Empty;
						}
						return this.m_String.Substring((int)num, (int)(this.m_Info.Offset.End - num));
					}
					default:
						goto IL_0992;
					}
				}
				else
				{
					ushort num;
					if (uriParts == UriComponents.Query)
					{
						num = this.m_Info.Offset.Query + 1;
					}
					else
					{
						num = this.m_Info.Offset.Query;
					}
					if (num >= this.m_Info.Offset.Fragment)
					{
						return string.Empty;
					}
					return this.m_String.Substring((int)num, (int)(this.m_Info.Offset.Fragment - num));
				}
			}
			else if (uriComponents <= (UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query | UriComponents.Fragment))
			{
				if (uriComponents == (UriComponents.Path | UriComponents.Query | UriComponents.Fragment))
				{
					return this.m_String.Substring((int)this.m_Info.Offset.Path, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Path));
				}
				if (uriComponents != (UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query | UriComponents.Fragment))
				{
					goto IL_0992;
				}
				if (this.InFact(Uri.Flags.HasUserInfo))
				{
					return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme)) + this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Host));
				}
				if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.End == this.m_String.Length)
				{
					return this.m_String;
				}
				return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Scheme));
			}
			else if (uriComponents != UriComponents.AbsoluteUri)
			{
				if (uriComponents != UriComponents.HostAndPort)
				{
					if (uriComponents != UriComponents.StrongAuthority)
					{
						goto IL_0992;
					}
				}
				else if (this.InFact(Uri.Flags.HasUserInfo))
				{
					if (this.InFact(Uri.Flags.NotDefaultPort) || this.m_Syntax.DefaultPort == -1)
					{
						return this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Host));
					}
					return this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Host)) + ":" + this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
				}
				if (!this.InFact(Uri.Flags.NotDefaultPort) && this.m_Syntax.DefaultPort != -1)
				{
					return this.m_String.Substring((int)this.m_Info.Offset.User, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.User)) + ":" + this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
				}
			}
			else
			{
				if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.End == this.m_String.Length)
				{
					return this.m_String;
				}
				return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Scheme));
			}
			if (this.m_Info.Offset.Path - this.m_Info.Offset.User != 0)
			{
				return this.m_String.Substring((int)this.m_Info.Offset.User, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.User));
			}
			return string.Empty;
			IL_0992:
			return null;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
		private unsafe void ParseRemaining()
		{
			this.EnsureUriInfo();
			Uri.Flags flags = Uri.Flags.Zero;
			if (!this.UserDrivenParsing)
			{
				bool flag = this.m_iriParsing && (this.m_Flags & Uri.Flags.HasUnicode) != Uri.Flags.Zero && (this.m_Flags & Uri.Flags.RestUnicodeNormalized) == Uri.Flags.Zero;
				ushort num = this.m_Info.Offset.Scheme;
				ushort num2 = (ushort)this.m_String.Length;
				UriSyntaxFlags flags2 = this.m_Syntax.Flags;
				Uri.Check check;
				fixed (string text = this.m_String)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (num2 > num && Uri.IsLWS(ptr[num2 - 1]))
					{
						num2 -= 1;
						while (num2 != num && Uri.IsLWS(ptr[(IntPtr)(num2 -= 1) * 2]))
						{
						}
						num2 += 1;
					}
					if (this.IsImplicitFile)
					{
						flags |= Uri.Flags.SchemeNotCanonical;
					}
					else
					{
						ushort num3 = 0;
						ushort num4 = (ushort)this.m_Syntax.SchemeName.Length;
						while (num3 < num4)
						{
							if (this.m_Syntax.SchemeName[(int)num3] != ptr[num + num3])
							{
								flags |= Uri.Flags.SchemeNotCanonical;
							}
							num3 += 1;
						}
						if ((this.m_Flags & Uri.Flags.AuthorityFound) != Uri.Flags.Zero && (num + num3 + 3 >= num2 || ptr[num + num3 + 1] != '/' || ptr[num + num3 + 2] != '/'))
						{
							flags |= Uri.Flags.SchemeNotCanonical;
						}
					}
					if ((this.m_Flags & Uri.Flags.HasUserInfo) != Uri.Flags.Zero)
					{
						num = this.m_Info.Offset.User;
						check = this.CheckCanonical(ptr, ref num, this.m_Info.Offset.Host, '@');
						if ((check & Uri.Check.DisplayCanonical) == Uri.Check.None)
						{
							flags |= Uri.Flags.UserNotCanonical;
						}
						if ((check & (Uri.Check.EscapedCanonical | Uri.Check.BackslashInPath)) != Uri.Check.EscapedCanonical)
						{
							flags |= Uri.Flags.E_UserNotCanonical;
						}
						if (this.m_iriParsing && (check & (Uri.Check.EscapedCanonical | Uri.Check.DisplayCanonical | Uri.Check.BackslashInPath | Uri.Check.NotIriCanonical | Uri.Check.FoundNonAscii)) == (Uri.Check.DisplayCanonical | Uri.Check.FoundNonAscii))
						{
							flags |= Uri.Flags.UserIriCanonical;
						}
					}
				}
				num = this.m_Info.Offset.Path;
				ushort num5 = this.m_Info.Offset.Path;
				if (flag)
				{
					if (this.IsDosPath)
					{
						if (this.IsImplicitFile)
						{
							this.m_String = string.Empty;
						}
						else
						{
							this.m_String = this.m_Syntax.SchemeName + Uri.SchemeDelimiter;
						}
					}
					this.m_Info.Offset.Path = (ushort)this.m_String.Length;
					num = this.m_Info.Offset.Path;
					ushort num6 = num5;
					if (this.IsImplicitFile || (flags2 & (UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment)) == UriSyntaxFlags.None)
					{
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, char.MaxValue);
					}
					else
					{
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, this.m_Syntax.InFact(UriSyntaxFlags.MayHaveQuery) ? '?' : (this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment) ? '#' : '\ufffe'));
					}
					string text2 = this.EscapeUnescapeIri(this.m_originalUnicodeString, (int)num6, (int)num5, UriComponents.Path);
					try
					{
						if (UriParser.ShouldUseLegacyV2Quirks)
						{
							this.m_String += text2.Normalize(NormalizationForm.FormC);
						}
						else
						{
							this.m_String += text2;
						}
					}
					catch (ArgumentException)
					{
						throw Uri.GetException(ParsingError.BadFormat);
					}
					num2 = (ushort)this.m_String.Length;
				}
				fixed (string text = this.m_String)
				{
					char* ptr2 = text;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (this.IsImplicitFile || (flags2 & (UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment)) == UriSyntaxFlags.None)
					{
						check = this.CheckCanonical(ptr2, ref num, num2, char.MaxValue);
					}
					else
					{
						check = this.CheckCanonical(ptr2, ref num, num2, ((flags2 & UriSyntaxFlags.MayHaveQuery) != UriSyntaxFlags.None) ? '?' : (this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment) ? '#' : '\ufffe'));
					}
					if ((this.m_Flags & Uri.Flags.AuthorityFound) != Uri.Flags.Zero && (flags2 & UriSyntaxFlags.PathIsRooted) != UriSyntaxFlags.None && (this.m_Info.Offset.Path == num2 || (ptr2[this.m_Info.Offset.Path] != '/' && ptr2[this.m_Info.Offset.Path] != '\\')))
					{
						flags |= Uri.Flags.FirstSlashAbsent;
					}
				}
				bool flag2 = false;
				if (this.IsDosPath || ((this.m_Flags & Uri.Flags.AuthorityFound) != Uri.Flags.Zero && ((flags2 & (UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath)) != UriSyntaxFlags.None || this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes))))
				{
					if ((check & Uri.Check.DotSlashEscaped) != Uri.Check.None && this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes))
					{
						flags |= Uri.Flags.PathNotCanonical | Uri.Flags.E_PathNotCanonical;
						flag2 = true;
					}
					if ((flags2 & UriSyntaxFlags.ConvertPathSlashes) != UriSyntaxFlags.None && (check & Uri.Check.BackslashInPath) != Uri.Check.None)
					{
						flags |= Uri.Flags.PathNotCanonical | Uri.Flags.E_PathNotCanonical;
						flag2 = true;
					}
					if ((flags2 & UriSyntaxFlags.CompressPath) != UriSyntaxFlags.None && ((flags & Uri.Flags.E_PathNotCanonical) != Uri.Flags.Zero || (check & Uri.Check.DotSlashAttn) != Uri.Check.None))
					{
						flags |= Uri.Flags.ShouldBeCompressed;
					}
					if ((check & Uri.Check.BackslashInPath) != Uri.Check.None)
					{
						flags |= Uri.Flags.BackslashInPath;
					}
				}
				else if ((check & Uri.Check.BackslashInPath) != Uri.Check.None)
				{
					flags |= Uri.Flags.E_PathNotCanonical;
					flag2 = true;
				}
				if ((check & Uri.Check.DisplayCanonical) == Uri.Check.None && ((this.m_Flags & Uri.Flags.ImplicitFile) == Uri.Flags.Zero || (this.m_Flags & Uri.Flags.UserEscaped) != Uri.Flags.Zero || (check & Uri.Check.ReservedFound) != Uri.Check.None))
				{
					flags |= Uri.Flags.PathNotCanonical;
					flag2 = true;
				}
				if ((this.m_Flags & Uri.Flags.ImplicitFile) != Uri.Flags.Zero && (check & (Uri.Check.EscapedCanonical | Uri.Check.ReservedFound)) != Uri.Check.None)
				{
					check &= ~Uri.Check.EscapedCanonical;
				}
				if ((check & Uri.Check.EscapedCanonical) == Uri.Check.None)
				{
					flags |= Uri.Flags.E_PathNotCanonical;
				}
				if (this.m_iriParsing && (!flag2 & ((check & (Uri.Check.EscapedCanonical | Uri.Check.DisplayCanonical | Uri.Check.NotIriCanonical | Uri.Check.FoundNonAscii)) == (Uri.Check.DisplayCanonical | Uri.Check.FoundNonAscii))))
				{
					flags |= Uri.Flags.PathIriCanonical;
				}
				if (flag)
				{
					ushort num7 = num5;
					if ((int)num5 < this.m_originalUnicodeString.Length && this.m_originalUnicodeString[(int)num5] == '?')
					{
						num5 += 1;
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, ((flags2 & UriSyntaxFlags.MayHaveFragment) != UriSyntaxFlags.None) ? '#' : '\ufffe');
						string text3 = this.EscapeUnescapeIri(this.m_originalUnicodeString, (int)num7, (int)num5, UriComponents.Query);
						try
						{
							if (UriParser.ShouldUseLegacyV2Quirks)
							{
								this.m_String += text3.Normalize(NormalizationForm.FormC);
							}
							else
							{
								this.m_String += text3;
							}
						}
						catch (ArgumentException)
						{
							throw Uri.GetException(ParsingError.BadFormat);
						}
						num2 = (ushort)this.m_String.Length;
					}
				}
				this.m_Info.Offset.Query = num;
				fixed (string text = this.m_String)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (num < num2 && ptr3[num] == '?')
					{
						num += 1;
						check = this.CheckCanonical(ptr3, ref num, num2, ((flags2 & UriSyntaxFlags.MayHaveFragment) != UriSyntaxFlags.None) ? '#' : '\ufffe');
						if ((check & Uri.Check.DisplayCanonical) == Uri.Check.None)
						{
							flags |= Uri.Flags.QueryNotCanonical;
						}
						if ((check & (Uri.Check.EscapedCanonical | Uri.Check.BackslashInPath)) != Uri.Check.EscapedCanonical)
						{
							flags |= Uri.Flags.E_QueryNotCanonical;
						}
						if (this.m_iriParsing && (check & (Uri.Check.EscapedCanonical | Uri.Check.DisplayCanonical | Uri.Check.BackslashInPath | Uri.Check.NotIriCanonical | Uri.Check.FoundNonAscii)) == (Uri.Check.DisplayCanonical | Uri.Check.FoundNonAscii))
						{
							flags |= Uri.Flags.QueryIriCanonical;
						}
					}
				}
				if (flag)
				{
					ushort num8 = num5;
					if ((int)num5 < this.m_originalUnicodeString.Length && this.m_originalUnicodeString[(int)num5] == '#')
					{
						num5 += 1;
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, '\ufffe');
						string text4 = this.EscapeUnescapeIri(this.m_originalUnicodeString, (int)num8, (int)num5, UriComponents.Fragment);
						try
						{
							if (UriParser.ShouldUseLegacyV2Quirks)
							{
								this.m_String += text4.Normalize(NormalizationForm.FormC);
							}
							else
							{
								this.m_String += text4;
							}
						}
						catch (ArgumentException)
						{
							throw Uri.GetException(ParsingError.BadFormat);
						}
						num2 = (ushort)this.m_String.Length;
					}
				}
				this.m_Info.Offset.Fragment = num;
				fixed (string text = this.m_String)
				{
					char* ptr4 = text;
					if (ptr4 != null)
					{
						ptr4 += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (num < num2 && ptr4[num] == '#')
					{
						num += 1;
						check = this.CheckCanonical(ptr4, ref num, num2, '\ufffe');
						if ((check & Uri.Check.DisplayCanonical) == Uri.Check.None)
						{
							flags |= Uri.Flags.FragmentNotCanonical;
						}
						if ((check & (Uri.Check.EscapedCanonical | Uri.Check.BackslashInPath)) != Uri.Check.EscapedCanonical)
						{
							flags |= Uri.Flags.E_FragmentNotCanonical;
						}
						if (this.m_iriParsing && (check & (Uri.Check.EscapedCanonical | Uri.Check.DisplayCanonical | Uri.Check.BackslashInPath | Uri.Check.NotIriCanonical | Uri.Check.FoundNonAscii)) == (Uri.Check.DisplayCanonical | Uri.Check.FoundNonAscii))
						{
							flags |= Uri.Flags.FragmentIriCanonical;
						}
					}
				}
				this.m_Info.Offset.End = num;
			}
			flags |= (Uri.Flags)int.MinValue;
			Uri.UriInfo info = this.m_Info;
			lock (info)
			{
				this.m_Flags |= flags;
			}
			this.m_Flags |= Uri.Flags.RestUnicodeNormalized;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00020188 File Offset: 0x0001E388
		private unsafe static ushort ParseSchemeCheckImplicitFile(char* uriString, ushort length, ref ParsingError err, ref Uri.Flags flags, ref UriParser syntax)
		{
			ushort num = 0;
			while (num < length && Uri.IsLWS(uriString[num]))
			{
				num += 1;
			}
			ushort num2 = num;
			while (num2 < length && uriString[num2] != ':')
			{
				num2 += 1;
			}
			if (IntPtr.Size == 4 && num2 != length && num2 >= num + 2 && Uri.CheckKnownSchemes((long*)(uriString + num), num2 - num, ref syntax))
			{
				return num2 + 1;
			}
			if (num + 2 >= length || num2 == num)
			{
				err = ParsingError.BadFormat;
				return 0;
			}
			char c;
			if ((c = uriString[num + 1]) == ':' || c == '|')
			{
				if (!Uri.IsAsciiLetter(uriString[num]))
				{
					if (c == ':')
					{
						err = ParsingError.BadScheme;
					}
					else
					{
						err = ParsingError.BadFormat;
					}
					return 0;
				}
				if ((c = uriString[num + 2]) == '\\' || c == '/')
				{
					flags |= Uri.Flags.AuthorityFound | Uri.Flags.DosPath | Uri.Flags.ImplicitFile;
					syntax = UriParser.FileUri;
					return num;
				}
				err = ParsingError.MustRootedPath;
				return 0;
			}
			else if (((c = uriString[num]) == '/' && Uri.IsWindowsFileSystem) || c == '\\')
			{
				if ((c = uriString[num + 1]) == '\\' || c == '/')
				{
					flags |= Uri.Flags.AuthorityFound | Uri.Flags.UncPath | Uri.Flags.ImplicitFile;
					syntax = UriParser.FileUri;
					num += 2;
					while (num < length && ((c = uriString[num]) == '/' || c == '\\'))
					{
						num += 1;
					}
					return num;
				}
				err = ParsingError.BadFormat;
				return 0;
			}
			else
			{
				if (uriString[num] == '/')
				{
					if (num == 0 || uriString[num - 1] != ':')
					{
						flags |= Uri.Flags.AuthorityFound | Uri.Flags.ImplicitFile;
						syntax = UriParser.FileUri;
						return num;
					}
					if (uriString[num + 1] == '/' && uriString[num + 2] == '/')
					{
						flags |= Uri.Flags.AuthorityFound | Uri.Flags.ImplicitFile;
						syntax = UriParser.FileUri;
						return num + 2;
					}
				}
				else if (uriString[num] == '\\')
				{
					err = ParsingError.BadFormat;
					return 0;
				}
				if (num2 == length)
				{
					err = ParsingError.BadFormat;
					return 0;
				}
				if (num2 - num > 1024)
				{
					err = ParsingError.SchemeLimit;
					return 0;
				}
				char* ptr;
				checked
				{
					ptr = stackalloc char[unchecked((UIntPtr)(num2 - num)) * 2];
					length = 0;
				}
				while (num < num2)
				{
					ref short ptr2 = ref *(short*)ptr;
					ushort num3 = length;
					length = num3 + 1;
					*((ref ptr2) + (IntPtr)num3 * 2) = (short)uriString[num];
					num += 1;
				}
				err = Uri.CheckSchemeSyntax(ptr, length, ref syntax);
				if (err != ParsingError.None)
				{
					return 0;
				}
				return num2 + 1;
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00020398 File Offset: 0x0001E598
		private unsafe static bool CheckKnownSchemes(long* lptr, ushort nChars, ref UriParser syntax)
		{
			if (nChars != 2)
			{
				long num = *lptr | 9007336695791648L;
				if (num <= 29273878621519975L)
				{
					if (num <= 16326042577993847L)
					{
						if (num != 12948347151515758L)
						{
							if (num != 16326029693157478L)
							{
								if (num == 16326042577993847L)
								{
									if (nChars == 3)
									{
										syntax = UriParser.WssUri;
										return true;
									}
								}
							}
							else if (nChars == 3)
							{
								syntax = UriParser.FtpUri;
								return true;
							}
						}
						else
						{
							if (nChars == 8 && (lptr[1] | 9007336695791648L) == 28429453690994800L)
							{
								syntax = UriParser.NetPipeUri;
								return true;
							}
							if (nChars == 7 && (lptr[1] | 9007336695791648L) == 16326029692043380L)
							{
								syntax = UriParser.NetTcpUri;
								return true;
							}
						}
					}
					else if (num != 28147948650299509L)
					{
						if (num != 28429436511125606L)
						{
							if (num == 29273878621519975L)
							{
								if (nChars == 6 && (*(int*)(lptr + 1) | 2097184) == 7471205)
								{
									syntax = UriParser.GopherUri;
									return true;
								}
							}
						}
						else if (nChars == 4)
						{
							syntax = UriParser.FileUri;
							return true;
						}
					}
					else if (nChars == 4)
					{
						syntax = UriParser.UuidUri;
						return true;
					}
				}
				else if (num <= 31525614009974892L)
				{
					if (num != 30399748462674029L)
					{
						if (num != 30962711301259380L)
						{
							if (num == 31525614009974892L)
							{
								if (nChars == 4)
								{
									syntax = UriParser.LdapUri;
									return true;
								}
							}
						}
						else if (nChars == 6 && (*(int*)(lptr + 1) | 2097184) == 7602277)
						{
							syntax = UriParser.TelnetUri;
							return true;
						}
					}
					else if (nChars == 6 && (*(int*)(lptr + 1) | 2097184) == 7274612)
					{
						syntax = UriParser.MailToUri;
						return true;
					}
				}
				else if (num != 31525695615008878L)
				{
					if (num != 31525695615402088L)
					{
						if (num == 32370133429452910L)
						{
							if (nChars == 4)
							{
								syntax = UriParser.NewsUri;
								return true;
							}
						}
					}
					else
					{
						if (nChars == 4)
						{
							syntax = UriParser.HttpUri;
							return true;
						}
						if (nChars == 5 && (*(ushort*)(lptr + 1) | 32) == 115)
						{
							syntax = UriParser.HttpsUri;
							return true;
						}
					}
				}
				else if (nChars == 4)
				{
					syntax = UriParser.NntpUri;
					return true;
				}
				return false;
			}
			if (((int)(*lptr) | 2097184) == 7536759)
			{
				syntax = UriParser.WsUri;
				return true;
			}
			return false;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00020604 File Offset: 0x0001E804
		private unsafe static ParsingError CheckSchemeSyntax(char* ptr, ushort length, ref UriParser syntax)
		{
			char c = *ptr;
			if (c < 'a' || c > 'z')
			{
				if (c < 'A' || c > 'Z')
				{
					return ParsingError.BadScheme;
				}
				*ptr = c | ' ';
			}
			for (ushort num = 1; num < length; num += 1)
			{
				char c2 = ptr[num];
				if (c2 < 'a' || c2 > 'z')
				{
					if (c2 >= 'A' && c2 <= 'Z')
					{
						ptr[num] = c2 | ' ';
					}
					else if ((c2 < '0' || c2 > '9') && c2 != '+' && c2 != '-' && c2 != '.')
					{
						return ParsingError.BadScheme;
					}
				}
			}
			string text = new string(ptr, 0, (int)length);
			syntax = UriParser.FindOrFetchAsUnknownV1Syntax(text);
			return ParsingError.None;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00020698 File Offset: 0x0001E898
		private unsafe ushort CheckAuthorityHelper(char* pString, ushort idx, ushort length, ref ParsingError err, ref Uri.Flags flags, UriParser syntax, ref string newHost)
		{
			int num = (int)length;
			int num2 = (int)idx;
			ushort num3 = idx;
			newHost = null;
			bool flag = false;
			bool flag2 = Uri.s_IriParsing && Uri.IriParsingStatic(syntax);
			bool flag3 = (flags & Uri.Flags.HasUnicode) > Uri.Flags.Zero;
			bool flag4 = (flags & Uri.Flags.HostUnicodeNormalized) == Uri.Flags.Zero;
			UriSyntaxFlags flags2 = syntax.Flags;
			if (flag3 && flag2 && flag4)
			{
				newHost = this.m_originalUnicodeString.Substring(0, num2);
			}
			char c;
			if (idx == length || ((c = pString[idx]) == '/' || (c == '\\' && Uri.StaticIsFile(syntax))) || c == '#' || c == '?')
			{
				if (syntax.InFact(UriSyntaxFlags.AllowEmptyHost))
				{
					flags &= ~Uri.Flags.UncPath;
					if (Uri.StaticInFact(flags, Uri.Flags.ImplicitFile) && (pString[idx] != '/' || Uri.IsWindowsFileSystem))
					{
						err = ParsingError.BadHostName;
					}
					else
					{
						flags |= Uri.Flags.BasicHostType;
					}
				}
				else
				{
					err = ParsingError.BadHostName;
				}
				if (flag3 && flag2 && flag4)
				{
					flags |= Uri.Flags.HostUnicodeNormalized;
				}
				return idx;
			}
			string text = null;
			if ((flags2 & UriSyntaxFlags.MayHaveUserInfo) != UriSyntaxFlags.None)
			{
				while ((int)num3 < num)
				{
					if ((int)num3 == num - 1 || pString[num3] == '?' || pString[num3] == '#' || pString[num3] == '\\' || pString[num3] == '/')
					{
						num3 = idx;
						break;
					}
					if (pString[num3] == '@')
					{
						flags |= Uri.Flags.HasUserInfo;
						if (flag2 || Uri.s_IdnScope != UriIdnScope.None)
						{
							if (flag2 && flag3 && flag4)
							{
								text = IriHelper.EscapeUnescapeIri(pString, num2, (int)(num3 + 1), UriComponents.UserInfo);
								try
								{
									if (UriParser.ShouldUseLegacyV2Quirks)
									{
										text = text.Normalize(NormalizationForm.FormC);
									}
								}
								catch (ArgumentException)
								{
									err = ParsingError.BadFormat;
									return idx;
								}
								newHost += text;
							}
							else
							{
								text = new string(pString, num2, (int)num3 - num2 + 1);
							}
						}
						num3 += 1;
						c = pString[num3];
						break;
					}
					num3 += 1;
				}
			}
			bool flag5 = (flags2 & UriSyntaxFlags.SimpleUserSyntax) == UriSyntaxFlags.None;
			if (c == '[' && syntax.InFact(UriSyntaxFlags.AllowIPv6Host) && IPv6AddressHelper.IsValid(pString, (int)(num3 + 1), ref num))
			{
				flags |= Uri.Flags.IPv6HostType;
				if (!Uri.s_ConfigInitialized)
				{
					Uri.InitializeUriConfig();
					this.m_iriParsing = Uri.s_IriParsing && Uri.IriParsingStatic(syntax);
				}
				if (flag3 && flag2 && flag4)
				{
					newHost += new string(pString, (int)num3, num - (int)num3);
					flags |= Uri.Flags.HostUnicodeNormalized;
					flag = true;
				}
			}
			else if (c <= '9' && c >= '0' && syntax.InFact(UriSyntaxFlags.AllowIPv4Host) && IPv4AddressHelper.IsValid(pString, (int)num3, ref num, false, Uri.StaticNotAny(flags, Uri.Flags.ImplicitFile), syntax.InFact(UriSyntaxFlags.V1_UnknownUri)))
			{
				flags |= Uri.Flags.IPv4HostType;
				if (flag3 && flag2 && flag4)
				{
					newHost += new string(pString, (int)num3, num - (int)num3);
					flags |= Uri.Flags.HostUnicodeNormalized;
					flag = true;
				}
			}
			else if ((flags2 & UriSyntaxFlags.AllowDnsHost) != UriSyntaxFlags.None && !flag2 && DomainNameHelper.IsValid(pString, num3, ref num, ref flag5, Uri.StaticNotAny(flags, Uri.Flags.ImplicitFile)))
			{
				flags |= Uri.Flags.DnsHostType;
				if (!flag5)
				{
					flags |= Uri.Flags.CanonicalDnsHost;
				}
				if (Uri.s_IdnScope != UriIdnScope.None)
				{
					if (Uri.s_IdnScope == UriIdnScope.AllExceptIntranet && this.IsIntranet(new string(pString, 0, num)))
					{
						flags |= Uri.Flags.IntranetUri;
					}
					if (this.AllowIdnStatic(syntax, flags))
					{
						bool flag6 = true;
						bool flag7 = false;
						string text2 = DomainNameHelper.UnicodeEquivalent(pString, (int)num3, num, ref flag6, ref flag7);
						if (flag7)
						{
							if (Uri.StaticNotAny(flags, Uri.Flags.HasUnicode))
							{
								this.m_originalUnicodeString = this.m_String;
							}
							flags |= Uri.Flags.IdnHost;
							newHost = this.m_originalUnicodeString.Substring(0, num2) + text + text2;
							flags |= Uri.Flags.CanonicalDnsHost;
							this.m_DnsSafeHost = new string(pString, (int)num3, num - (int)num3);
							flag = true;
						}
						flags |= Uri.Flags.HostUnicodeNormalized;
					}
				}
			}
			else if ((flags2 & UriSyntaxFlags.AllowDnsHost) != UriSyntaxFlags.None && ((syntax.InFact(UriSyntaxFlags.AllowIriParsing) && flag4) || syntax.InFact(UriSyntaxFlags.AllowIdn)) && DomainNameHelper.IsValidByIri(pString, num3, ref num, ref flag5, Uri.StaticNotAny(flags, Uri.Flags.ImplicitFile)))
			{
				this.CheckAuthorityHelperHandleDnsIri(pString, num3, num, num2, flag2, flag3, syntax, text, ref flags, ref flag, ref newHost, ref err);
			}
			else if ((flags2 & UriSyntaxFlags.AllowUncHost) != UriSyntaxFlags.None && UncNameHelper.IsValid(pString, num3, ref num, Uri.StaticNotAny(flags, Uri.Flags.ImplicitFile)) && num - (int)num3 <= 256)
			{
				flags |= Uri.Flags.UncHostType;
			}
			if (num < (int)length && pString[num] == '\\' && (flags & Uri.Flags.HostTypeMask) != Uri.Flags.Zero && !Uri.StaticIsFile(syntax))
			{
				if (syntax.InFact(UriSyntaxFlags.V1_UnknownUri))
				{
					err = ParsingError.BadHostName;
					flags |= Uri.Flags.HostTypeMask;
					return (ushort)num;
				}
				flags &= ~Uri.Flags.HostTypeMask;
			}
			else if (num < (int)length && pString[num] == ':')
			{
				if (syntax.InFact(UriSyntaxFlags.MayHavePort))
				{
					int num4 = 0;
					int num5 = num;
					idx = (ushort)(num + 1);
					while (idx < length)
					{
						ushort num6 = (ushort)(pString[idx] - '0');
						if (num6 >= 0 && num6 <= 9)
						{
							if ((num4 = num4 * 10 + (int)num6) > 65535)
							{
								break;
							}
							idx += 1;
						}
						else
						{
							if (num6 == 65535 || num6 == 15 || num6 == 65523)
							{
								break;
							}
							if (syntax.InFact(UriSyntaxFlags.AllowAnyOtherHost) && syntax.NotAny(UriSyntaxFlags.V1_UnknownUri))
							{
								flags &= ~Uri.Flags.HostTypeMask;
								break;
							}
							err = ParsingError.BadPort;
							return idx;
						}
					}
					if (num4 > 65535)
					{
						if (!syntax.InFact(UriSyntaxFlags.AllowAnyOtherHost))
						{
							err = ParsingError.BadPort;
							return idx;
						}
						flags &= ~Uri.Flags.HostTypeMask;
					}
					if (flag2 && flag3 && flag)
					{
						newHost += new string(pString, num5, (int)idx - num5);
					}
				}
				else
				{
					flags &= ~Uri.Flags.HostTypeMask;
				}
			}
			if ((flags & Uri.Flags.HostTypeMask) == Uri.Flags.Zero)
			{
				flags &= ~Uri.Flags.HasUserInfo;
				if (syntax.InFact(UriSyntaxFlags.AllowAnyOtherHost))
				{
					flags |= Uri.Flags.BasicHostType;
					num = (int)idx;
					while (num < (int)length && pString[num] != '/' && pString[num] != '?' && pString[num] != '#')
					{
						num++;
					}
					this.CheckAuthorityHelperHandleAnyHostIri(pString, num2, num, flag2, flag3, syntax, ref flags, ref newHost, ref err);
				}
				else if (syntax.InFact(UriSyntaxFlags.V1_UnknownUri))
				{
					bool flag8 = false;
					int num7 = (int)idx;
					num = (int)idx;
					while (num < (int)length && (!flag8 || (pString[num] != '/' && pString[num] != '?' && pString[num] != '#')))
					{
						if (num >= (int)(idx + 2) || pString[num] != '.')
						{
							err = ParsingError.BadHostName;
							flags |= Uri.Flags.HostTypeMask;
							return idx;
						}
						flag8 = true;
						num++;
					}
					flags |= Uri.Flags.BasicHostType;
					if (flag2 && flag3 && Uri.StaticNotAny(flags, Uri.Flags.HostUnicodeNormalized))
					{
						string text3 = new string(pString, num7, num - num7);
						try
						{
							newHost += text3.Normalize(NormalizationForm.FormC);
						}
						catch (ArgumentException)
						{
							err = ParsingError.BadFormat;
							return idx;
						}
						flags |= Uri.Flags.HostUnicodeNormalized;
					}
				}
				else if (syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (syntax.InFact(UriSyntaxFlags.MailToLikeUri) && !UriParser.ShouldUseLegacyV2Quirks))
				{
					err = ParsingError.BadHostName;
					flags |= Uri.Flags.HostTypeMask;
					return idx;
				}
			}
			return (ushort)num;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00020E84 File Offset: 0x0001F084
		private unsafe void CheckAuthorityHelperHandleDnsIri(char* pString, ushort start, int end, int startInput, bool iriParsing, bool hasUnicode, UriParser syntax, string userInfoString, ref Uri.Flags flags, ref bool justNormalized, ref string newHost, ref ParsingError err)
		{
			flags |= Uri.Flags.DnsHostType;
			if (Uri.s_IdnScope == UriIdnScope.AllExceptIntranet && this.IsIntranet(new string(pString, 0, end)))
			{
				flags |= Uri.Flags.IntranetUri;
			}
			if (this.AllowIdnStatic(syntax, flags))
			{
				bool flag = true;
				bool flag2 = false;
				string text = DomainNameHelper.IdnEquivalent(pString, (int)start, end, ref flag, ref flag2);
				string text2 = DomainNameHelper.UnicodeEquivalent(text, pString, (int)start, end);
				if (!flag)
				{
					flags |= Uri.Flags.UnicodeHost;
				}
				if (flag2)
				{
					flags |= Uri.Flags.IdnHost;
				}
				if (flag && flag2 && Uri.StaticNotAny(flags, Uri.Flags.HasUnicode))
				{
					this.m_originalUnicodeString = this.m_String;
					newHost = this.m_originalUnicodeString.Substring(0, startInput) + (Uri.StaticInFact(flags, Uri.Flags.HasUserInfo) ? userInfoString : null);
					justNormalized = true;
				}
				else if (!iriParsing && (Uri.StaticInFact(flags, Uri.Flags.UnicodeHost) || Uri.StaticInFact(flags, Uri.Flags.IdnHost)))
				{
					this.m_originalUnicodeString = this.m_String;
					newHost = this.m_originalUnicodeString.Substring(0, startInput) + (Uri.StaticInFact(flags, Uri.Flags.HasUserInfo) ? userInfoString : null);
					justNormalized = true;
				}
				if (!flag || flag2)
				{
					this.m_DnsSafeHost = text;
					newHost += text2;
					justNormalized = true;
				}
				else if (flag && !flag2 && iriParsing && hasUnicode)
				{
					newHost += text2;
					justNormalized = true;
				}
			}
			else if (hasUnicode)
			{
				string text3 = Uri.StripBidiControlCharacter(pString, (int)start, end - (int)start);
				try
				{
					newHost += ((text3 != null) ? text3.Normalize(NormalizationForm.FormC) : null);
				}
				catch (ArgumentException)
				{
					err = ParsingError.BadHostName;
				}
				justNormalized = true;
			}
			flags |= Uri.Flags.HostUnicodeNormalized;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00021070 File Offset: 0x0001F270
		private unsafe void CheckAuthorityHelperHandleAnyHostIri(char* pString, int startInput, int end, bool iriParsing, bool hasUnicode, UriParser syntax, ref Uri.Flags flags, ref string newHost, ref ParsingError err)
		{
			if (Uri.StaticNotAny(flags, Uri.Flags.HostUnicodeNormalized) && (this.AllowIdnStatic(syntax, flags) || (iriParsing && hasUnicode)))
			{
				string text = new string(pString, startInput, end - startInput);
				if (this.AllowIdnStatic(syntax, flags))
				{
					bool flag = true;
					bool flag2 = false;
					string text2 = DomainNameHelper.UnicodeEquivalent(pString, startInput, end, ref flag, ref flag2);
					if (((flag && flag2) || !flag) && (!iriParsing || !hasUnicode))
					{
						this.m_originalUnicodeString = this.m_String;
						newHost = this.m_originalUnicodeString.Substring(0, startInput);
						flags |= Uri.Flags.HasUnicode;
					}
					if (flag2 || !flag)
					{
						newHost += text2;
						string text3 = null;
						this.m_DnsSafeHost = DomainNameHelper.IdnEquivalent(pString, startInput, end, ref flag, ref text3);
						if (flag2)
						{
							flags |= Uri.Flags.IdnHost;
						}
						if (!flag)
						{
							flags |= Uri.Flags.UnicodeHost;
						}
					}
					else if (iriParsing && hasUnicode)
					{
						newHost += text;
					}
				}
				else
				{
					try
					{
						newHost += text.Normalize(NormalizationForm.FormC);
					}
					catch (ArgumentException)
					{
						err = ParsingError.BadHostName;
					}
				}
				flags |= Uri.Flags.HostUnicodeNormalized;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000211B4 File Offset: 0x0001F3B4
		private unsafe void FindEndOfComponent(string input, ref ushort idx, ushort end, char delim)
		{
			fixed (string text = input)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.FindEndOfComponent(ptr, ref idx, end, delim);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000211E0 File Offset: 0x0001F3E0
		private unsafe void FindEndOfComponent(char* str, ref ushort idx, ushort end, char delim)
		{
			ushort num;
			for (num = idx; num < end; num += 1)
			{
				char c = str[num];
				if (c == delim || (delim == '?' && c == '#' && this.m_Syntax != null && this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment)))
				{
					break;
				}
			}
			idx = num;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00021234 File Offset: 0x0001F434
		private unsafe Uri.Check CheckCanonical(char* str, ref ushort idx, ushort end, char delim)
		{
			Uri.Check check = Uri.Check.None;
			bool flag = false;
			bool flag2 = false;
			ushort num;
			for (num = idx; num < end; num += 1)
			{
				char c = str[num];
				if (c <= '\u001f' || (c >= '\u007f' && c <= '\u009f'))
				{
					flag = true;
					flag2 = true;
					check |= Uri.Check.ReservedFound;
				}
				else if (c > 'z' && c != '~')
				{
					if (this.m_iriParsing)
					{
						bool flag3 = false;
						check |= Uri.Check.FoundNonAscii;
						if (char.IsHighSurrogate(c))
						{
							if (num + 1 < end)
							{
								bool flag4 = false;
								flag3 = IriHelper.CheckIriUnicodeRange(c, str[num + 1], ref flag4, true);
							}
						}
						else
						{
							flag3 = IriHelper.CheckIriUnicodeRange(c, true);
						}
						if (!flag3)
						{
							check |= Uri.Check.NotIriCanonical;
						}
					}
					if (!flag)
					{
						flag = true;
					}
				}
				else
				{
					if (c == delim || (delim == '?' && c == '#' && this.m_Syntax != null && this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment)))
					{
						break;
					}
					if (c == '?')
					{
						if (this.IsImplicitFile || (this.m_Syntax != null && !this.m_Syntax.InFact(UriSyntaxFlags.MayHaveQuery) && delim != '\ufffe'))
						{
							check |= Uri.Check.ReservedFound;
							flag2 = true;
							flag = true;
						}
					}
					else if (c == '#')
					{
						flag = true;
						if (this.IsImplicitFile || (this.m_Syntax != null && !this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment)))
						{
							check |= Uri.Check.ReservedFound;
							flag2 = true;
						}
					}
					else if (c == '/' || c == '\\')
					{
						if ((check & Uri.Check.BackslashInPath) == Uri.Check.None && c == '\\')
						{
							check |= Uri.Check.BackslashInPath;
						}
						if ((check & Uri.Check.DotSlashAttn) == Uri.Check.None && num + 1 != end && (str[num + 1] == '/' || str[num + 1] == '\\'))
						{
							check |= Uri.Check.DotSlashAttn;
						}
					}
					else if (c == '.')
					{
						if (((check & Uri.Check.DotSlashAttn) == Uri.Check.None && num + 1 == end) || str[num + 1] == '.' || str[num + 1] == '/' || str[num + 1] == '\\' || str[num + 1] == '?' || str[num + 1] == '#')
						{
							check |= Uri.Check.DotSlashAttn;
						}
					}
					else if (!flag && ((c <= '"' && c != '!') || (c >= '[' && c <= '^') || c == '>' || c == '<' || c == '`'))
					{
						flag = true;
					}
					else if (c == '%')
					{
						if (!flag2)
						{
							flag2 = true;
						}
						if (num + 2 < end && (c = UriHelper.EscapedAscii(str[num + 1], str[num + 2])) != '\uffff')
						{
							if (c == '.' || c == '/' || c == '\\')
							{
								check |= Uri.Check.DotSlashEscaped;
							}
							num += 2;
						}
						else if (!flag)
						{
							flag = true;
						}
					}
				}
			}
			if (flag2)
			{
				if (!flag)
				{
					check |= Uri.Check.EscapedCanonical;
				}
			}
			else
			{
				check |= Uri.Check.DisplayCanonical;
				if (!flag)
				{
					check |= Uri.Check.EscapedCanonical;
				}
			}
			idx = num;
			return check;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000214F0 File Offset: 0x0001F6F0
		private unsafe char[] GetCanonicalPath(char[] dest, ref int pos, UriFormat formatAs)
		{
			if (this.InFact(Uri.Flags.FirstSlashAbsent))
			{
				char[] array = dest;
				int num = pos;
				pos = num + 1;
				array[num] = 47;
			}
			if (this.m_Info.Offset.Path == this.m_Info.Offset.Query)
			{
				return dest;
			}
			int num2 = pos;
			int securedPathIndex = (int)this.SecuredPathIndex;
			if (formatAs == UriFormat.UriEscaped)
			{
				if (this.InFact(Uri.Flags.ShouldBeCompressed))
				{
					this.m_String.CopyTo((int)this.m_Info.Offset.Path, dest, num2, (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path));
					num2 += (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path);
					if (this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes) && this.InFact(Uri.Flags.PathNotCanonical) && !this.IsImplicitFile)
					{
						char[] array2;
						char* ptr;
						if ((array2 = dest) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						Uri.UnescapeOnly(ptr, pos, ref num2, '.', '/', this.m_Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes) ? '\\' : char.MaxValue);
						array2 = null;
					}
				}
				else if (this.InFact(Uri.Flags.E_PathNotCanonical) && this.NotAny(Uri.Flags.UserEscaped))
				{
					string text = this.m_String;
					if (securedPathIndex != 0 && text[securedPathIndex + (int)this.m_Info.Offset.Path - 1] == '|')
					{
						text = text.Remove(securedPathIndex + (int)this.m_Info.Offset.Path - 1, 1);
						text = text.Insert(securedPathIndex + (int)this.m_Info.Offset.Path - 1, ":");
					}
					dest = UriHelper.EscapeString(text, (int)this.m_Info.Offset.Path, (int)this.m_Info.Offset.Query, dest, ref num2, true, '?', '#', this.IsImplicitFile ? char.MaxValue : '%');
				}
				else
				{
					this.m_String.CopyTo((int)this.m_Info.Offset.Path, dest, num2, (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path));
					num2 += (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path);
				}
			}
			else
			{
				this.m_String.CopyTo((int)this.m_Info.Offset.Path, dest, num2, (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path));
				num2 += (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path);
				if (this.InFact(Uri.Flags.ShouldBeCompressed) && this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes) && this.InFact(Uri.Flags.PathNotCanonical) && !this.IsImplicitFile)
				{
					char[] array2;
					char* ptr2;
					if ((array2 = dest) == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					Uri.UnescapeOnly(ptr2, pos, ref num2, '.', '/', this.m_Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes) ? '\\' : char.MaxValue);
					array2 = null;
				}
			}
			if (securedPathIndex != 0 && dest[securedPathIndex + pos - 1] == '|')
			{
				dest[securedPathIndex + pos - 1] = ':';
			}
			if (this.InFact(Uri.Flags.ShouldBeCompressed))
			{
				dest = Uri.Compress(dest, (ushort)(pos + securedPathIndex), ref num2, this.m_Syntax);
				if (dest[pos] == '\\')
				{
					dest[pos] = '/';
				}
				if (formatAs == UriFormat.UriEscaped && this.NotAny(Uri.Flags.UserEscaped) && this.InFact(Uri.Flags.E_PathNotCanonical))
				{
					dest = UriHelper.EscapeString(new string(dest, pos, num2 - pos), 0, num2 - pos, dest, ref pos, true, '?', '#', this.IsImplicitFile ? char.MaxValue : '%');
					num2 = pos;
				}
			}
			else if (this.m_Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes) && this.InFact(Uri.Flags.BackslashInPath))
			{
				for (int i = pos; i < num2; i++)
				{
					if (dest[i] == '\\')
					{
						dest[i] = '/';
					}
				}
			}
			if (formatAs != UriFormat.UriEscaped && this.InFact(Uri.Flags.PathNotCanonical))
			{
				UnescapeMode unescapeMode;
				if (this.InFact(Uri.Flags.PathNotCanonical))
				{
					if (formatAs != UriFormat.Unescaped)
					{
						if (formatAs == (UriFormat)32767)
						{
							unescapeMode = (this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape) | UnescapeMode.V1ToStringFlag;
							if (this.IsImplicitFile)
							{
								unescapeMode &= ~UnescapeMode.Unescape;
							}
						}
						else
						{
							unescapeMode = (this.InFact(Uri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape);
							if (this.IsImplicitFile)
							{
								unescapeMode &= ~UnescapeMode.Unescape;
							}
						}
					}
					else
					{
						unescapeMode = (this.IsImplicitFile ? UnescapeMode.CopyOnly : (UnescapeMode.Unescape | UnescapeMode.UnescapeAll));
					}
				}
				else
				{
					unescapeMode = UnescapeMode.CopyOnly;
				}
				char[] array3 = new char[dest.Length];
				Buffer.BlockCopy(dest, 0, array3, 0, num2 << 1);
				char[] array2;
				char* ptr3;
				if ((array2 = array3) == null || array2.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &array2[0];
				}
				dest = UriHelper.UnescapeString(ptr3, pos, num2, dest, ref pos, '?', '#', char.MaxValue, unescapeMode, this.m_Syntax, false);
				array2 = null;
			}
			else
			{
				pos = num2;
			}
			return dest;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00021A24 File Offset: 0x0001FC24
		private unsafe static void UnescapeOnly(char* pch, int start, ref int end, char ch1, char ch2, char ch3)
		{
			if (end - start < 3)
			{
				return;
			}
			char* ptr = pch + end - 2;
			pch += start;
			char* ptr2 = null;
			while (pch < ptr)
			{
				if (*(pch++) == '%')
				{
					char c = UriHelper.EscapedAscii(*(pch++), *(pch++));
					if (c == ch1 || c == ch2 || c == ch3)
					{
						ptr2 = pch - 2;
						*(ptr2 - 1) = c;
						while (pch < ptr)
						{
							if ((*(ptr2++) = *(pch++)) == '%')
							{
								c = UriHelper.EscapedAscii(*(ptr2++) = *(pch++), *(ptr2++) = *(pch++));
								if (c == ch1 || c == ch2 || c == ch3)
								{
									ptr2 -= 2;
									*(ptr2 - 1) = c;
								}
							}
						}
						break;
					}
				}
			}
			ptr += 2;
			if (ptr2 == null)
			{
				return;
			}
			if (pch == ptr)
			{
				end -= (int)((long)(pch - ptr2));
				return;
			}
			*(ptr2++) = *(pch++);
			if (pch == ptr)
			{
				end -= (int)((long)(pch - ptr2));
				return;
			}
			*(ptr2++) = *(pch++);
			end -= (int)((long)(pch - ptr2));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00021B40 File Offset: 0x0001FD40
		private static char[] Compress(char[] dest, ushort start, ref int destLength, UriParser syntax)
		{
			ushort num = 0;
			ushort num2 = 0;
			ushort num3 = 0;
			ushort num4 = 0;
			ushort num5 = (ushort)destLength - 1;
			start -= 1;
			while (num5 != start)
			{
				char c = dest[(int)num5];
				if (c == '\\' && syntax.InFact(UriSyntaxFlags.ConvertPathSlashes))
				{
					c = (dest[(int)num5] = '/');
				}
				if (c == '/')
				{
					num += 1;
				}
				else
				{
					if (num > 1)
					{
						num2 = num5 + 1;
					}
					num = 0;
				}
				if (c == '.')
				{
					num3 += 1;
				}
				else
				{
					if (num3 != 0)
					{
						bool flag = syntax.NotAny(UriSyntaxFlags.CanonicalizeAsFilePath) && (num3 > 2 || c != '/' || num5 == start);
						if (!flag && c == '/')
						{
							if ((num2 == num5 + num3 + 1 || (num2 == 0 && (int)(num5 + num3 + 1) == destLength)) && (UriParser.ShouldUseLegacyV2Quirks || num3 <= 2))
							{
								num2 = num5 + 1 + num3 + ((num2 == 0) ? 0 : 1);
								Buffer.BlockCopy(dest, (int)num2 << 1, dest, (int)(num5 + 1) << 1, destLength - (int)num2 << 1);
								destLength -= (int)(num2 - num5 - 1);
								num2 = num5;
								if (num3 == 2)
								{
									num4 += 1;
								}
								num3 = 0;
								goto IL_0194;
							}
						}
						else if (UriParser.ShouldUseLegacyV2Quirks && !flag && num4 == 0 && (num2 == num5 + num3 + 1 || (num2 == 0 && (int)(num5 + num3 + 1) == destLength)))
						{
							num3 = num5 + 1 + num3;
							Buffer.BlockCopy(dest, (int)num3 << 1, dest, (int)(num5 + 1) << 1, destLength - (int)num3 << 1);
							destLength -= (int)(num3 - num5 - 1);
							num2 = 0;
							num3 = 0;
							goto IL_0194;
						}
						num3 = 0;
					}
					if (c == '/')
					{
						if (num4 != 0)
						{
							num4 -= 1;
							num2 += 1;
							Buffer.BlockCopy(dest, (int)num2 << 1, dest, (int)(num5 + 1) << 1, destLength - (int)num2 << 1);
							destLength -= (int)(num2 - num5 - 1);
						}
						num2 = num5;
					}
				}
				IL_0194:
				num5 -= 1;
			}
			start += 1;
			if ((ushort)destLength > start && syntax.InFact(UriSyntaxFlags.CanonicalizeAsFilePath) && num <= 1)
			{
				if (num4 != 0 && dest[(int)start] != '/')
				{
					num2 += 1;
					Buffer.BlockCopy(dest, (int)num2 << 1, dest, (int)start << 1, destLength - (int)num2 << 1);
					destLength -= (int)num2;
				}
				else if (num3 != 0 && (num2 == num3 + 1 || (num2 == 0 && (int)(num3 + 1) == destLength)))
				{
					num3 += ((num2 == 0) ? 0 : 1);
					Buffer.BlockCopy(dest, (int)num3 << 1, dest, (int)start << 1, destLength - (int)num3 << 1);
					destLength -= (int)num3;
				}
			}
			return dest;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00021D6F File Offset: 0x0001FF6F
		internal static int CalculateCaseInsensitiveHashCode(string text)
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(text);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00021D7C File Offset: 0x0001FF7C
		private static string CombineUri(Uri basePart, string relativePart, UriFormat uriFormat)
		{
			char c = relativePart[0];
			if (basePart.IsDosPath && (c == '/' || c == '\\') && (relativePart.Length == 1 || (relativePart[1] != '/' && relativePart[1] != '\\')))
			{
				int num = basePart.OriginalString.IndexOf(':');
				if (basePart.IsImplicitFile)
				{
					return basePart.OriginalString.Substring(0, num + 1) + relativePart;
				}
				num = basePart.OriginalString.IndexOf(':', num + 1);
				return basePart.OriginalString.Substring(0, num + 1) + relativePart;
			}
			else if (Uri.StaticIsFile(basePart.Syntax) && (c == '\\' || c == '/'))
			{
				if (relativePart.Length >= 2 && (relativePart[1] == '\\' || relativePart[1] == '/'))
				{
					if (!basePart.IsImplicitFile)
					{
						return "file:" + relativePart;
					}
					return relativePart;
				}
				else
				{
					if (!basePart.IsUnc)
					{
						return "file://" + relativePart;
					}
					string text = basePart.GetParts(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
					for (int i = 1; i < text.Length; i++)
					{
						if (text[i] == '/')
						{
							text = text.Substring(0, i);
							break;
						}
					}
					if (basePart.IsImplicitFile)
					{
						return "\\\\" + basePart.GetParts(UriComponents.Host, UriFormat.Unescaped) + text + relativePart;
					}
					return "file://" + basePart.GetParts(UriComponents.Host, uriFormat) + text + relativePart;
				}
			}
			else
			{
				bool flag = basePart.Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes);
				string text2;
				if (c != '/' && (c != '\\' || !flag))
				{
					text2 = basePart.GetParts(UriComponents.Path | UriComponents.KeepDelimiter, basePart.IsImplicitFile ? UriFormat.Unescaped : uriFormat);
					int j = text2.Length;
					char[] array = new char[j + relativePart.Length];
					if (j > 0)
					{
						text2.CopyTo(0, array, 0, j);
						while (j > 0)
						{
							if (array[--j] == '/')
							{
								j++;
								break;
							}
						}
					}
					relativePart.CopyTo(0, array, j, relativePart.Length);
					c = (basePart.Syntax.InFact(UriSyntaxFlags.MayHaveQuery) ? '?' : char.MaxValue);
					char c2 = ((!basePart.IsImplicitFile && basePart.Syntax.InFact(UriSyntaxFlags.MayHaveFragment)) ? '#' : char.MaxValue);
					string text3 = string.Empty;
					if (c != '\uffff' || c2 != '\uffff')
					{
						int num2 = 0;
						while (num2 < relativePart.Length && array[j + num2] != c && array[j + num2] != c2)
						{
							num2++;
						}
						if (num2 == 0)
						{
							text3 = relativePart;
						}
						else if (num2 < relativePart.Length)
						{
							text3 = relativePart.Substring(num2);
						}
						j += num2;
					}
					else
					{
						j += relativePart.Length;
					}
					if (basePart.HostType == Uri.Flags.IPv6HostType)
					{
						if (basePart.IsImplicitFile)
						{
							text2 = "\\\\[" + basePart.DnsSafeHost + "]";
						}
						else
						{
							text2 = string.Concat(new string[]
							{
								basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo, uriFormat),
								"[",
								basePart.DnsSafeHost,
								"]",
								basePart.GetParts(UriComponents.Port | UriComponents.KeepDelimiter, uriFormat)
							});
						}
					}
					else if (basePart.IsImplicitFile)
					{
						if (Uri.IsWindowsFileSystem)
						{
							if (basePart.IsDosPath)
							{
								array = Uri.Compress(array, 3, ref j, basePart.Syntax);
								return new string(array, 1, j - 1) + text3;
							}
							text2 = "\\\\" + basePart.GetParts(UriComponents.Host, UriFormat.Unescaped);
						}
						else
						{
							text2 = basePart.GetParts(UriComponents.Host, UriFormat.Unescaped);
						}
					}
					else
					{
						text2 = basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port, uriFormat);
					}
					array = Uri.Compress(array, basePart.SecuredPathIndex, ref j, basePart.Syntax);
					return text2 + new string(array, 0, j) + text3;
				}
				if (relativePart.Length >= 2 && relativePart[1] == '/')
				{
					return basePart.Scheme + ":" + relativePart;
				}
				if (basePart.HostType == Uri.Flags.IPv6HostType)
				{
					text2 = string.Concat(new string[]
					{
						basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo, uriFormat),
						"[",
						basePart.DnsSafeHost,
						"]",
						basePart.GetParts(UriComponents.Port | UriComponents.KeepDelimiter, uriFormat)
					});
				}
				else
				{
					text2 = basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port, uriFormat);
				}
				if (flag && c == '\\')
				{
					relativePart = "/" + relativePart.Substring(1);
				}
				return text2 + relativePart;
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000221CC File Offset: 0x000203CC
		private static string PathDifference(string path1, string path2, bool compareCase)
		{
			int num = -1;
			int i = 0;
			while (i < path1.Length && i < path2.Length && (path1[i] == path2[i] || (!compareCase && char.ToLower(path1[i], CultureInfo.InvariantCulture) == char.ToLower(path2[i], CultureInfo.InvariantCulture))))
			{
				if (path1[i] == '/')
				{
					num = i;
				}
				i++;
			}
			if (i == 0)
			{
				return path2;
			}
			if (i == path1.Length && i == path2.Length)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (i < path1.Length)
			{
				if (path1[i] == '/')
				{
					stringBuilder.Append("../");
				}
				i++;
			}
			if (stringBuilder.Length == 0 && path2.Length - 1 == num)
			{
				return "./";
			}
			return stringBuilder.ToString() + path2.Substring(num + 1);
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000222AF File Offset: 0x000204AF
		internal bool HasAuthority
		{
			get
			{
				return this.InFact(Uri.Flags.AuthorityFound);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000222BD File Offset: 0x000204BD
		private static bool IsLWS(char ch)
		{
			return ch <= ' ' && (ch == ' ' || ch == '\n' || ch == '\r' || ch == '\t');
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000222DC File Offset: 0x000204DC
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000222F9 File Offset: 0x000204F9
		internal static bool IsAsciiLetterOrDigit(char character)
		{
			return Uri.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00022314 File Offset: 0x00020514
		internal static bool IsBidiControlCharacter(char ch)
		{
			return ch == '\u200e' || ch == '\u200f' || ch == '\u202a' || ch == '\u202b' || ch == '\u202c' || ch == '\u202d' || ch == '\u202e';
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00022350 File Offset: 0x00020550
		internal unsafe static string StripBidiControlCharacter(char* strToClean, int start, int length)
		{
			if (length <= 0)
			{
				return "";
			}
			char[] array = new char[length];
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = strToClean[start + i];
				if (c < '\u200e' || c > '\u202e' || !Uri.IsBidiControlCharacter(c))
				{
					array[num++] = c;
				}
			}
			return new string(array, 0, num);
		}

		/// <summary>Determines the difference between two <see cref="T:System.Uri" /> instances.</summary>
		/// <returns>If the hostname and scheme of this URI instance and <paramref name="toUri" /> are the same, then this method returns a <see cref="T:System.String" /> that represents a relative URI that, when appended to the current URI instance, yields the <paramref name="toUri" /> parameter.If the hostname or scheme is different, then this method returns a <see cref="T:System.String" /> that represents the <paramref name="toUri" /> parameter.</returns>
		/// <param name="toUri">The URI to compare to the current URI. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toUri" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this method is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000716 RID: 1814 RVA: 0x000223B0 File Offset: 0x000205B0
		[Obsolete("The method has been deprecated. Please use MakeRelativeUri(Uri uri). http://go.microsoft.com/fwlink/?linkid=14202")]
		public string MakeRelative(Uri toUri)
		{
			if (toUri == null)
			{
				throw new ArgumentNullException("toUri");
			}
			if (this.IsNotAbsoluteUri || toUri.IsNotAbsoluteUri)
			{
				throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
			}
			if (this.Scheme == toUri.Scheme && this.Host == toUri.Host && this.Port == toUri.Port)
			{
				return Uri.PathDifference(this.AbsolutePath, toUri.AbsolutePath, !this.IsUncOrDosPath);
			}
			return toUri.ToString();
		}

		/// <summary>Parses the URI of the current instance to ensure it contains all the parts required for a valid URI.</summary>
		/// <exception cref="T:System.UriFormatException">The Uri passed from the constructor is invalid. </exception>
		// Token: 0x06000717 RID: 1815 RVA: 0x000027E8 File Offset: 0x000009E8
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void Parse()
		{
		}

		/// <summary>Converts the internally stored URI to canonical form.</summary>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this method is valid only for absolute URIs. </exception>
		/// <exception cref="T:System.UriFormatException">The URI is incorrectly formed.</exception>
		// Token: 0x06000718 RID: 1816 RVA: 0x000027E8 File Offset: 0x000009E8
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void Canonicalize()
		{
		}

		/// <summary>Converts any unsafe or reserved characters in the path component to their hexadecimal character representations.</summary>
		/// <exception cref="T:System.UriFormatException">The URI passed from the constructor is invalid. This exception can occur if a URI has too many characters or the URI is relative.</exception>
		// Token: 0x06000719 RID: 1817 RVA: 0x000027E8 File Offset: 0x000009E8
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void Escape()
		{
		}

		/// <summary>Converts the specified string by replacing any escape sequences with their unescaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the unescaped value of the <paramref name="path" /> parameter.</returns>
		/// <param name="path">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x0600071A RID: 1818 RVA: 0x00022440 File Offset: 0x00020640
		[Obsolete("The method has been deprecated. Please use GetComponents() or static UnescapeDataString() to unescape a Uri component or a string. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual string Unescape(string path)
		{
			char[] array = new char[path.Length];
			int num = 0;
			array = UriHelper.UnescapeString(path, 0, path.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, null, false);
			return new string(array, 0, num);
		}

		/// <summary>Converts a string to its escaped representation.</summary>
		/// <returns>The escaped representation of the string.</returns>
		/// <param name="str">The string to transform to its escaped representation. </param>
		// Token: 0x0600071B RID: 1819 RVA: 0x00022488 File Offset: 0x00020688
		[Obsolete("The method has been deprecated. Please use GetComponents() or static EscapeUriString() to escape a Uri component or a string. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected static string EscapeString(string str)
		{
			if (str == null)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = UriHelper.EscapeString(str, 0, str.Length, null, ref num, true, '?', '#', '%');
			if (array == null)
			{
				return str;
			}
			return new string(array, 0, num);
		}

		/// <summary>Calling this method has no effect.</summary>
		// Token: 0x0600071C RID: 1820 RVA: 0x000224C5 File Offset: 0x000206C5
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void CheckSecurity()
		{
			this.Scheme == "telnet";
		}

		/// <summary>Gets whether the specified character is a reserved character.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the specified character is a reserved character otherwise, false.</returns>
		/// <param name="character">The <see cref="T:System.Char" /> to test. </param>
		// Token: 0x0600071D RID: 1821 RVA: 0x000224D8 File Offset: 0x000206D8
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual bool IsReservedCharacter(char character)
		{
			return character == ';' || character == '/' || character == ':' || character == '@' || character == '&' || character == '=' || character == '+' || character == '$' || character == ',';
		}

		/// <summary>Gets whether the specified character should be escaped.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the specified character should be escaped; otherwise, false.</returns>
		/// <param name="character">The <see cref="T:System.Char" /> to test. </param>
		// Token: 0x0600071E RID: 1822 RVA: 0x0002250C File Offset: 0x0002070C
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected static bool IsExcludedCharacter(char character)
		{
			return character <= ' ' || character >= '\u007f' || character == '<' || character == '>' || character == '#' || character == '%' || character == '"' || character == '{' || character == '}' || character == '|' || character == '\\' || character == '^' || character == '[' || character == ']' || character == '`';
		}

		/// <summary>Gets whether a character is invalid in a file system name.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the specified character is invalid; otherwise false.</returns>
		/// <param name="character">The <see cref="T:System.Char" /> to test. </param>
		// Token: 0x0600071F RID: 1823 RVA: 0x00022568 File Offset: 0x00020768
		[Obsolete("The method has been deprecated. It is not used by the system. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual bool IsBadFileSystemCharacter(char character)
		{
			return character < ' ' || character == ';' || character == '/' || character == '?' || character == ':' || character == '&' || character == '=' || character == ',' || character == '*' || character == '<' || character == '>' || character == '"' || character == '|' || character == '\\' || character == '^';
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000225C4 File Offset: 0x000207C4
		private void CreateThis(string uri, bool dontEscape, UriKind uriKind)
		{
			if ((uriKind < UriKind.RelativeOrAbsolute || uriKind > UriKind.Relative) && uriKind != (UriKind)300)
			{
				throw new ArgumentException(global::SR.GetString("The value '{0}' passed for the UriKind parameter is invalid.", new object[] { uriKind }));
			}
			this.m_String = ((uri == null) ? string.Empty : uri);
			if (dontEscape)
			{
				this.m_Flags |= Uri.Flags.UserEscaped;
			}
			ParsingError parsingError = Uri.ParseScheme(this.m_String, ref this.m_Flags, ref this.m_Syntax);
			UriFormatException ex;
			this.InitializeUri(parsingError, uriKind, out ex);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00022650 File Offset: 0x00020850
		private void InitializeUri(ParsingError err, UriKind uriKind, out UriFormatException e)
		{
			if (err == ParsingError.None)
			{
				if (this.IsImplicitFile && (uriKind != UriKind.RelativeOrAbsolute || this.m_String.Length <= 0 || this.m_String[0] != '/' || Uri.useDotNetRelativeOrAbsolute))
				{
					if (this.NotAny(Uri.Flags.DosPath) && uriKind != UriKind.Absolute && (uriKind == UriKind.Relative || (this.m_String.Length >= 2 && (this.m_String[0] != '\\' || this.m_String[1] != '\\'))))
					{
						this.m_Syntax = null;
						this.m_Flags &= Uri.Flags.UserEscaped;
						e = null;
						return;
					}
					if (uriKind == UriKind.Relative && this.InFact(Uri.Flags.DosPath))
					{
						this.m_Syntax = null;
						this.m_Flags &= Uri.Flags.UserEscaped;
						e = null;
						return;
					}
				}
			}
			else if (err > ParsingError.EmptyUriString)
			{
				this.m_String = null;
				e = Uri.GetException(err);
				return;
			}
			bool flag = false;
			if (!Uri.s_ConfigInitialized && this.CheckForConfigLoad(this.m_String))
			{
				Uri.InitializeUriConfig();
			}
			this.m_iriParsing = Uri.s_IriParsing && (this.m_Syntax == null || this.m_Syntax.InFact(UriSyntaxFlags.AllowIriParsing));
			if (this.m_iriParsing && (this.CheckForUnicode(this.m_String) || this.CheckForEscapedUnreserved(this.m_String)))
			{
				this.m_Flags |= Uri.Flags.HasUnicode;
				flag = true;
				this.m_originalUnicodeString = this.m_String;
			}
			if (this.m_Syntax != null)
			{
				if (this.m_Syntax.IsSimple)
				{
					if ((err = this.PrivateParseMinimal()) != ParsingError.None)
					{
						if (uriKind != UriKind.Absolute && err <= ParsingError.EmptyUriString)
						{
							this.m_Syntax = null;
							e = null;
							this.m_Flags &= Uri.Flags.UserEscaped;
						}
						else
						{
							e = Uri.GetException(err);
						}
					}
					else if (uriKind == UriKind.Relative)
					{
						e = Uri.GetException(ParsingError.CannotCreateRelative);
					}
					else
					{
						e = null;
					}
					if (this.m_iriParsing && flag)
					{
						this.EnsureParseRemaining();
						return;
					}
				}
				else
				{
					this.m_Syntax = this.m_Syntax.InternalOnNewUri();
					this.m_Flags |= Uri.Flags.UserDrivenParsing;
					this.m_Syntax.InternalValidate(this, out e);
					if (e != null)
					{
						if (uriKind != UriKind.Absolute && err != ParsingError.None && err <= ParsingError.EmptyUriString)
						{
							this.m_Syntax = null;
							e = null;
							this.m_Flags &= Uri.Flags.UserEscaped;
							return;
						}
					}
					else
					{
						if (err != ParsingError.None || this.InFact(Uri.Flags.ErrorOrParsingRecursion))
						{
							this.SetUserDrivenParsing();
						}
						else if (uriKind == UriKind.Relative)
						{
							e = Uri.GetException(ParsingError.CannotCreateRelative);
						}
						if (this.m_iriParsing && flag)
						{
							this.EnsureParseRemaining();
							return;
						}
					}
				}
			}
			else
			{
				if (err != ParsingError.None && uriKind != UriKind.Absolute && err <= ParsingError.EmptyUriString)
				{
					e = null;
					this.m_Flags &= Uri.Flags.UserEscaped | Uri.Flags.HasUnicode;
					if (!this.m_iriParsing || !flag)
					{
						return;
					}
					this.m_String = this.EscapeUnescapeIri(this.m_originalUnicodeString, 0, this.m_originalUnicodeString.Length, (UriComponents)0);
					try
					{
						if (UriParser.ShouldUseLegacyV2Quirks)
						{
							this.m_String = this.m_String.Normalize(NormalizationForm.FormC);
						}
						return;
					}
					catch (ArgumentException)
					{
						e = Uri.GetException(ParsingError.BadFormat);
						return;
					}
				}
				this.m_String = null;
				e = Uri.GetException(err);
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00022980 File Offset: 0x00020B80
		private unsafe bool CheckForConfigLoad(string data)
		{
			bool flag = false;
			int length = data.Length;
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (int i = 0; i < length; i++)
				{
					if (ptr[i] > '\u007f' || ptr[i] == '%' || (ptr[i] == 'x' && i + 3 < length && ptr[i + 1] == 'n' && ptr[i + 2] == '-' && ptr[i + 3] == '-'))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00022A14 File Offset: 0x00020C14
		private bool CheckForUnicode(string data)
		{
			bool flag = false;
			char[] array = new char[data.Length];
			int num = 0;
			array = UriHelper.UnescapeString(data, 0, data.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, null, false);
			for (int i = 0; i < num; i++)
			{
				if (array[i] > '\u007f')
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00022A70 File Offset: 0x00020C70
		private unsafe bool CheckForEscapedUnreserved(string data)
		{
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (int i = 0; i < data.Length - 2; i++)
				{
					if (ptr[i] == '%' && Uri.IsHexDigit(ptr[i + 1]) && Uri.IsHexDigit(ptr[i + 2]) && ptr[i + 1] >= '0' && ptr[i + 1] <= '7')
					{
						char c = UriHelper.EscapedAscii(ptr[i + 1], ptr[i + 2]);
						if (c != '\uffff' && UriHelper.Is3986Unreserved(c))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>Creates a new <see cref="T:System.Uri" /> using the specified <see cref="T:System.String" /> instance and a <see cref="T:System.UriKind" />.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> was successfully created; otherwise, false.</returns>
		/// <param name="uriString">The <see cref="T:System.String" /> representing the <see cref="T:System.Uri" />.</param>
		/// <param name="uriKind">The type of the Uri.</param>
		/// <param name="result">When this method returns, contains the constructed <see cref="T:System.Uri" />.</param>
		// Token: 0x06000725 RID: 1829 RVA: 0x00022B14 File Offset: 0x00020D14
		public static bool TryCreate(string uriString, UriKind uriKind, out Uri result)
		{
			if (uriString == null)
			{
				result = null;
				return false;
			}
			UriFormatException ex = null;
			result = Uri.CreateHelper(uriString, false, uriKind, ref ex);
			return ex == null && result != null;
		}

		/// <summary>Creates a new <see cref="T:System.Uri" /> using the specified base and relative <see cref="T:System.String" /> instances.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> was successfully created; otherwise, false.</returns>
		/// <param name="baseUri">The base <see cref="T:System.Uri" />.</param>
		/// <param name="relativeUri">The relative <see cref="T:System.Uri" />, represented as a <see cref="T:System.String" />, to add to the base <see cref="T:System.Uri" />.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Uri" /> constructed from <paramref name="baseUri" /> and <paramref name="relativeUri" />. This parameter is passed uninitialized.</param>
		// Token: 0x06000726 RID: 1830 RVA: 0x00022B44 File Offset: 0x00020D44
		public static bool TryCreate(Uri baseUri, string relativeUri, out Uri result)
		{
			Uri uri;
			if (!Uri.TryCreate(relativeUri, (UriKind)300, out uri))
			{
				result = null;
				return false;
			}
			if (!uri.IsAbsoluteUri)
			{
				return Uri.TryCreate(baseUri, uri, out result);
			}
			result = uri;
			return true;
		}

		/// <summary>Creates a new <see cref="T:System.Uri" /> using the specified base and relative <see cref="T:System.Uri" /> instances.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> was successfully created; otherwise, false.</returns>
		/// <param name="baseUri">The base <see cref="T:System.Uri" />. </param>
		/// <param name="relativeUri">The relative <see cref="T:System.Uri" /> to add to the base <see cref="T:System.Uri" />. </param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Uri" /> constructed from <paramref name="baseUri" /> and <paramref name="relativeUri" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseUri" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000727 RID: 1831 RVA: 0x00022B7C File Offset: 0x00020D7C
		public static bool TryCreate(Uri baseUri, Uri relativeUri, out Uri result)
		{
			result = null;
			if (baseUri == null || relativeUri == null)
			{
				return false;
			}
			if (baseUri.IsNotAbsoluteUri)
			{
				return false;
			}
			string text = null;
			bool flag;
			UriFormatException ex;
			if (baseUri.Syntax.IsSimple)
			{
				flag = relativeUri.UserEscaped;
				result = Uri.ResolveHelper(baseUri, relativeUri, ref text, ref flag, out ex);
			}
			else
			{
				flag = false;
				text = baseUri.Syntax.InternalResolve(baseUri, relativeUri, out ex);
			}
			if (ex != null)
			{
				return false;
			}
			if (result == null)
			{
				result = Uri.CreateHelper(text, flag, UriKind.Absolute, ref ex);
			}
			return ex == null && result != null && result.IsAbsoluteUri;
		}

		/// <summary>Gets the specified components of the current instance using the specified escaping for special characters.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the components.</returns>
		/// <param name="components">A bitwise combination of the <see cref="T:System.UriComponents" /> values that specifies which parts of the current instance to return to the caller.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000728 RID: 1832 RVA: 0x00022C04 File Offset: 0x00020E04
		public string GetComponents(UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, global::SR.GetString("UriComponents.SerializationInfoString must not be combined with other UriComponents."));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (this.IsNotAbsoluteUri)
			{
				if (components == UriComponents.SerializationInfoString)
				{
					return this.GetRelativeSerializationString(format);
				}
				throw new InvalidOperationException(global::SR.GetString("This operation is not supported for a relative URI."));
			}
			else
			{
				if (this.Syntax.IsSimple)
				{
					return this.GetComponentsHelper(components, format);
				}
				return this.Syntax.InternalGetComponents(this, components, format);
			}
		}

		/// <summary>Compares the specified parts of two URIs using the specified comparison rules.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that indicates the lexical relationship between the compared <see cref="T:System.Uri" /> components.ValueMeaningLess than zero<paramref name="uri1" /> is less than <paramref name="uri2" />.Zero<paramref name="uri1" /> equals <paramref name="uri2" />.Greater than zero<paramref name="uri1" /> is greater than <paramref name="uri2" />.</returns>
		/// <param name="uri1">The first <see cref="T:System.Uri" />.</param>
		/// <param name="uri2">The second <see cref="T:System.Uri" />.</param>
		/// <param name="partsToCompare">A bitwise combination of the <see cref="T:System.UriComponents" /> values that specifies the parts of <paramref name="uri1" /> and <paramref name="uri2" /> to compare.</param>
		/// <param name="compareFormat">One of the <see cref="T:System.UriFormat" /> values that specifies the character escaping used when the URI components are compared.</param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000729 RID: 1833 RVA: 0x00022C9C File Offset: 0x00020E9C
		public static int Compare(Uri uri1, Uri uri2, UriComponents partsToCompare, UriFormat compareFormat, StringComparison comparisonType)
		{
			if (uri1 == null)
			{
				if (uri2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (uri2 == null)
				{
					return 1;
				}
				if (uri1.IsAbsoluteUri && uri2.IsAbsoluteUri)
				{
					return string.Compare(uri1.GetParts(partsToCompare, compareFormat), uri2.GetParts(partsToCompare, compareFormat), comparisonType);
				}
				if (uri1.IsAbsoluteUri)
				{
					return 1;
				}
				if (!uri2.IsAbsoluteUri)
				{
					return string.Compare(uri1.OriginalString, uri2.OriginalString, comparisonType);
				}
				return -1;
			}
		}

		/// <summary>Indicates whether the string used to construct this <see cref="T:System.Uri" /> was well-formed and is not required to be further escaped.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the string was well-formed; else false.</returns>
		// Token: 0x0600072A RID: 1834 RVA: 0x00022D0D File Offset: 0x00020F0D
		public bool IsWellFormedOriginalString()
		{
			if (this.IsNotAbsoluteUri || this.Syntax.IsSimple)
			{
				return this.InternalIsWellFormedOriginalString();
			}
			return this.Syntax.InternalIsWellFormedOriginalString(this);
		}

		/// <summary>Indicates whether the string is well-formed by attempting to construct a URI with the string and ensures that the string does not require further escaping.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the string was well-formed; else false.</returns>
		/// <param name="uriString">The string used to attempt to construct a <see cref="T:System.Uri" />.</param>
		/// <param name="uriKind">The type of the <see cref="T:System.Uri" /> in <paramref name="uriString" />.</param>
		// Token: 0x0600072B RID: 1835 RVA: 0x00022D38 File Offset: 0x00020F38
		public static bool IsWellFormedUriString(string uriString, UriKind uriKind)
		{
			if (uriKind == UriKind.RelativeOrAbsolute)
			{
				uriKind = (UriKind)300;
			}
			Uri uri;
			return Uri.TryCreate(uriString, uriKind, out uri) && uri.IsWellFormedOriginalString();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00022D64 File Offset: 0x00020F64
		internal unsafe bool InternalIsWellFormedOriginalString()
		{
			if (this.UserDrivenParsing)
			{
				throw new InvalidOperationException(global::SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[] { base.GetType().FullName }));
			}
			fixed (string @string = this.m_String)
			{
				char* ptr = @string;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				ushort num = 0;
				if (!this.IsAbsoluteUri)
				{
					return (UriParser.ShouldUseLegacyV2Quirks || !Uri.CheckForColonInFirstPathSegment(this.m_String)) && (this.CheckCanonical(ptr, ref num, (ushort)this.m_String.Length, '\ufffe') & (Uri.Check.EscapedCanonical | Uri.Check.BackslashInPath)) == Uri.Check.EscapedCanonical;
				}
				if (this.IsImplicitFile)
				{
					return false;
				}
				this.EnsureParseRemaining();
				Uri.Flags flags = this.m_Flags & (Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.UserIriCanonical | Uri.Flags.PathIriCanonical | Uri.Flags.QueryIriCanonical | Uri.Flags.FragmentIriCanonical);
				if ((flags & Uri.Flags.E_CannotDisplayCanonical & (Uri.Flags.E_UserNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical)) != Uri.Flags.Zero && (!this.m_iriParsing || (this.m_iriParsing && ((flags & Uri.Flags.E_UserNotCanonical) == Uri.Flags.Zero || (flags & Uri.Flags.UserIriCanonical) == Uri.Flags.Zero) && ((flags & Uri.Flags.E_PathNotCanonical) == Uri.Flags.Zero || (flags & Uri.Flags.PathIriCanonical) == Uri.Flags.Zero) && ((flags & Uri.Flags.E_QueryNotCanonical) == Uri.Flags.Zero || (flags & Uri.Flags.QueryIriCanonical) == Uri.Flags.Zero) && ((flags & Uri.Flags.E_FragmentNotCanonical) == Uri.Flags.Zero || (flags & Uri.Flags.FragmentIriCanonical) == Uri.Flags.Zero))))
				{
					return false;
				}
				if (this.InFact(Uri.Flags.AuthorityFound))
				{
					num = (ushort)((int)this.m_Info.Offset.Scheme + this.m_Syntax.SchemeName.Length + 2);
					if (num >= this.m_Info.Offset.User || this.m_String[(int)(num - 1)] == '\\' || this.m_String[(int)num] == '\\')
					{
						return false;
					}
					if (this.InFact(Uri.Flags.DosPath | Uri.Flags.UncPath) && (num += 1) < this.m_Info.Offset.User && (this.m_String[(int)num] == '/' || this.m_String[(int)num] == '\\'))
					{
						return false;
					}
				}
				if (this.InFact(Uri.Flags.FirstSlashAbsent) && this.m_Info.Offset.Query > this.m_Info.Offset.Path)
				{
					return false;
				}
				if (this.InFact(Uri.Flags.BackslashInPath))
				{
					return false;
				}
				if (this.IsDosPath && this.m_String[(int)(this.m_Info.Offset.Path + this.SecuredPathIndex - 1)] == '|')
				{
					return false;
				}
				if ((this.m_Flags & Uri.Flags.CanonicalDnsHost) == Uri.Flags.Zero && this.HostType != Uri.Flags.IPv6HostType)
				{
					num = this.m_Info.Offset.User;
					Uri.Check check = this.CheckCanonical(ptr, ref num, this.m_Info.Offset.Path, '/');
					if ((check & (Uri.Check.EscapedCanonical | Uri.Check.BackslashInPath | Uri.Check.ReservedFound)) != Uri.Check.EscapedCanonical && (!this.m_iriParsing || (this.m_iriParsing && (check & (Uri.Check.DisplayCanonical | Uri.Check.NotIriCanonical | Uri.Check.FoundNonAscii)) != (Uri.Check.DisplayCanonical | Uri.Check.FoundNonAscii))))
					{
						return false;
					}
				}
				if ((this.m_Flags & (Uri.Flags.SchemeNotCanonical | Uri.Flags.AuthorityFound)) == (Uri.Flags.SchemeNotCanonical | Uri.Flags.AuthorityFound))
				{
					num = (ushort)this.m_Syntax.SchemeName.Length;
					IntPtr intPtr;
					ushort num2;
					do
					{
						intPtr = ptr;
						num2 = num;
						num = num2 + 1;
					}
					while (*(intPtr + (IntPtr)num2 * 2) != 58);
					if ((int)(num + 1) >= this.m_String.Length || ptr[num] != '/' || ptr[num + 1] != '/')
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Converts a string to its unescaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the unescaped representation of <paramref name="stringToUnescape" />. </returns>
		/// <param name="stringToUnescape">The string to unescape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stringToUnescape" /> is null. </exception>
		// Token: 0x0600072D RID: 1837 RVA: 0x00023090 File Offset: 0x00021290
		public unsafe static string UnescapeDataString(string stringToUnescape)
		{
			if (stringToUnescape == null)
			{
				throw new ArgumentNullException("stringToUnescape");
			}
			if (stringToUnescape.Length == 0)
			{
				return string.Empty;
			}
			char* ptr = stringToUnescape;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 0;
			while (num < stringToUnescape.Length && ptr[num] != '%')
			{
				num++;
			}
			if (num == stringToUnescape.Length)
			{
				return stringToUnescape;
			}
			UnescapeMode unescapeMode = UnescapeMode.Unescape | UnescapeMode.UnescapeAll;
			num = 0;
			char[] array = new char[stringToUnescape.Length];
			array = UriHelper.UnescapeString(stringToUnescape, 0, stringToUnescape.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, unescapeMode, null, false);
			return new string(array, 0, num);
		}

		/// <summary>Converts a URI string to its escaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the escaped representation of <paramref name="stringToEscape" />.</returns>
		/// <param name="stringToEscape">The string to escape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stringToEscape" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The length of <paramref name="stringToEscape" /> exceeds 32766 characters.</exception>
		// Token: 0x0600072E RID: 1838 RVA: 0x00023130 File Offset: 0x00021330
		public static string EscapeUriString(string stringToEscape)
		{
			if (stringToEscape == null)
			{
				throw new ArgumentNullException("stringToEscape");
			}
			if (stringToEscape.Length == 0)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = UriHelper.EscapeString(stringToEscape, 0, stringToEscape.Length, null, ref num, true, char.MaxValue, char.MaxValue, char.MaxValue);
			if (array == null)
			{
				return stringToEscape;
			}
			return new string(array, 0, num);
		}

		/// <summary>Converts a string to its escaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the escaped representation of <paramref name="stringToEscape" />.</returns>
		/// <param name="stringToEscape">The string to escape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stringToEscape" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The length of <paramref name="stringToEscape" /> exceeds 32766 characters.</exception>
		// Token: 0x0600072F RID: 1839 RVA: 0x0002318C File Offset: 0x0002138C
		public static string EscapeDataString(string stringToEscape)
		{
			if (stringToEscape == null)
			{
				throw new ArgumentNullException("stringToEscape");
			}
			if (stringToEscape.Length == 0)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = UriHelper.EscapeString(stringToEscape, 0, stringToEscape.Length, null, ref num, false, char.MaxValue, char.MaxValue, char.MaxValue);
			if (array == null)
			{
				return stringToEscape;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000231E8 File Offset: 0x000213E8
		internal unsafe string EscapeUnescapeIri(string input, int start, int end, UriComponents component)
		{
			char* ptr = input;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return IriHelper.EscapeUnescapeIri(ptr, start, end, component);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0002320F File Offset: 0x0002140F
		private Uri(Uri.Flags flags, UriParser uriParser, string uri)
		{
			this.m_Flags = flags;
			this.m_Syntax = uriParser;
			this.m_String = uri;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002322C File Offset: 0x0002142C
		internal static Uri CreateHelper(string uriString, bool dontEscape, UriKind uriKind, ref UriFormatException e)
		{
			if ((uriKind < UriKind.RelativeOrAbsolute || uriKind > UriKind.Relative) && uriKind != (UriKind)300)
			{
				throw new ArgumentException(global::SR.GetString("The value '{0}' passed for the UriKind parameter is invalid.", new object[] { uriKind }));
			}
			UriParser uriParser = null;
			Uri.Flags flags = Uri.Flags.Zero;
			ParsingError parsingError = Uri.ParseScheme(uriString, ref flags, ref uriParser);
			if (dontEscape)
			{
				flags |= Uri.Flags.UserEscaped;
			}
			if (parsingError == ParsingError.None)
			{
				Uri uri = new Uri(flags, uriParser, uriString);
				Uri uri2;
				try
				{
					uri.InitializeUri(parsingError, uriKind, out e);
					if (e == null)
					{
						uri2 = uri;
					}
					else
					{
						uri2 = null;
					}
				}
				catch (UriFormatException ex)
				{
					e = ex;
					uri2 = null;
				}
				return uri2;
			}
			if (uriKind != UriKind.Absolute && parsingError <= ParsingError.EmptyUriString)
			{
				return new Uri(flags & Uri.Flags.UserEscaped, null, uriString);
			}
			return null;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000232E0 File Offset: 0x000214E0
		internal static Uri ResolveHelper(Uri baseUri, Uri relativeUri, ref string newUriString, ref bool userEscaped, out UriFormatException e)
		{
			e = null;
			string text = string.Empty;
			if (relativeUri != null)
			{
				if (relativeUri.IsAbsoluteUri && (Uri.IsWindowsFileSystem || relativeUri.OriginalString[0] != '/' || !relativeUri.IsImplicitFile))
				{
					return relativeUri;
				}
				text = relativeUri.OriginalString;
				userEscaped = relativeUri.UserEscaped;
			}
			else
			{
				text = string.Empty;
			}
			if (text.Length > 0 && (Uri.IsLWS(text[0]) || Uri.IsLWS(text[text.Length - 1])))
			{
				text = text.Trim(Uri._WSchars);
			}
			if (text.Length == 0)
			{
				newUriString = baseUri.GetParts(UriComponents.AbsoluteUri, baseUri.UserEscaped ? UriFormat.UriEscaped : UriFormat.SafeUnescaped);
				return null;
			}
			if (text[0] == '#' && !baseUri.IsImplicitFile && baseUri.Syntax.InFact(UriSyntaxFlags.MayHaveFragment))
			{
				newUriString = baseUri.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.UriEscaped) + text;
				return null;
			}
			if (text[0] == '?' && !baseUri.IsImplicitFile && baseUri.Syntax.InFact(UriSyntaxFlags.MayHaveQuery))
			{
				newUriString = baseUri.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped) + text;
				return null;
			}
			if (text.Length >= 3 && (text[1] == ':' || text[1] == '|') && Uri.IsAsciiLetter(text[0]) && (text[2] == '\\' || text[2] == '/'))
			{
				if (baseUri.IsImplicitFile)
				{
					newUriString = text;
					return null;
				}
				if (baseUri.Syntax.InFact(UriSyntaxFlags.AllowDOSPath))
				{
					string text2;
					if (baseUri.InFact(Uri.Flags.AuthorityFound))
					{
						text2 = (baseUri.Syntax.InFact(UriSyntaxFlags.PathIsRooted) ? ":///" : "://");
					}
					else
					{
						text2 = (baseUri.Syntax.InFact(UriSyntaxFlags.PathIsRooted) ? ":/" : ":");
					}
					newUriString = baseUri.Scheme + text2 + text;
					return null;
				}
			}
			ParsingError combinedString = Uri.GetCombinedString(baseUri, text, userEscaped, ref newUriString);
			if (combinedString != ParsingError.None)
			{
				e = Uri.GetException(combinedString);
				return null;
			}
			if (newUriString == baseUri.m_String)
			{
				return baseUri;
			}
			return null;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000234EC File Offset: 0x000216EC
		private string GetRelativeSerializationString(UriFormat format)
		{
			if (format == UriFormat.UriEscaped)
			{
				if (this.m_String.Length == 0)
				{
					return string.Empty;
				}
				int num = 0;
				char[] array = UriHelper.EscapeString(this.m_String, 0, this.m_String.Length, null, ref num, true, char.MaxValue, char.MaxValue, '%');
				if (array == null)
				{
					return this.m_String;
				}
				return new string(array, 0, num);
			}
			else
			{
				if (format == UriFormat.Unescaped)
				{
					return Uri.UnescapeDataString(this.m_String);
				}
				if (format != UriFormat.SafeUnescaped)
				{
					throw new ArgumentOutOfRangeException("format");
				}
				if (this.m_String.Length == 0)
				{
					return string.Empty;
				}
				char[] array2 = new char[this.m_String.Length];
				int num2 = 0;
				array2 = UriHelper.UnescapeString(this.m_String, 0, this.m_String.Length, array2, ref num2, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.EscapeUnescape, null, false);
				return new string(array2, 0, num2);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000235C8 File Offset: 0x000217C8
		internal string GetComponentsHelper(UriComponents uriComponents, UriFormat uriFormat)
		{
			if (uriComponents == UriComponents.Scheme)
			{
				return this.m_Syntax.SchemeName;
			}
			if ((uriComponents & UriComponents.SerializationInfoString) != (UriComponents)0)
			{
				uriComponents |= UriComponents.AbsoluteUri;
			}
			this.EnsureParseRemaining();
			if ((uriComponents & UriComponents.NormalizedHost) != (UriComponents)0)
			{
				uriComponents |= UriComponents.Host;
			}
			if ((uriComponents & UriComponents.Host) != (UriComponents)0)
			{
				this.EnsureHostString(true);
			}
			if (uriComponents == UriComponents.Port || uriComponents == UriComponents.StrongPort)
			{
				if ((this.m_Flags & Uri.Flags.NotDefaultPort) != Uri.Flags.Zero || (uriComponents == UriComponents.StrongPort && this.m_Syntax.DefaultPort != -1))
				{
					return this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if ((uriComponents & UriComponents.StrongPort) != (UriComponents)0)
				{
					uriComponents |= UriComponents.Port;
				}
				if (uriComponents == UriComponents.Host && (uriFormat == UriFormat.UriEscaped || (this.m_Flags & (Uri.Flags.HostNotCanonical | Uri.Flags.E_HostNotCanonical)) == Uri.Flags.Zero))
				{
					this.EnsureHostString(false);
					return this.m_Info.Host;
				}
				if (uriFormat == UriFormat.UriEscaped)
				{
					return this.GetEscapedParts(uriComponents);
				}
				if (uriFormat - UriFormat.Unescaped > 1 && uriFormat != (UriFormat)32767)
				{
					throw new ArgumentOutOfRangeException("uriFormat");
				}
				return this.GetUnescapedParts(uriComponents, uriFormat);
			}
		}

		/// <summary>Determines whether the current <see cref="T:System.Uri" /> instance is a base of the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <returns>true if the current <see cref="T:System.Uri" /> instance is a base of <paramref name="uri" />; otherwise, false.</returns>
		/// <param name="uri">The specified <see cref="T:System.Uri" /> instance to test. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000736 RID: 1846 RVA: 0x000236CD File Offset: 0x000218CD
		public bool IsBaseOf(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!this.IsAbsoluteUri)
			{
				return false;
			}
			if (this.Syntax.IsSimple)
			{
				return this.IsBaseOfHelper(uri);
			}
			return this.Syntax.InternalIsBaseOf(this, uri);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0002370C File Offset: 0x0002190C
		internal unsafe bool IsBaseOfHelper(Uri uriLink)
		{
			if (!this.IsAbsoluteUri || this.UserDrivenParsing)
			{
				return false;
			}
			if (!uriLink.IsAbsoluteUri)
			{
				string text = null;
				bool flag = false;
				UriFormatException ex;
				uriLink = Uri.ResolveHelper(this, uriLink, ref text, ref flag, out ex);
				if (ex != null)
				{
					return false;
				}
				if (uriLink == null)
				{
					uriLink = Uri.CreateHelper(text, flag, UriKind.Absolute, ref ex);
				}
				if (ex != null)
				{
					return false;
				}
			}
			if (this.Syntax.SchemeName != uriLink.Syntax.SchemeName)
			{
				return false;
			}
			string parts = this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.SafeUnescaped);
			string parts2 = uriLink.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.SafeUnescaped);
			fixed (string text2 = parts)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = parts2)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					return UriHelper.TestForSubPath(ptr, (ushort)parts.Length, ptr2, (ushort)parts2.Length, this.IsUncOrDosPath || uriLink.IsUncOrDosPath);
				}
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000237E8 File Offset: 0x000219E8
		private void CreateThisFromUri(Uri otherUri)
		{
			this.m_Info = null;
			this.m_Flags = otherUri.m_Flags;
			if (this.InFact(Uri.Flags.MinimalUriInfoSet))
			{
				this.m_Flags &= ~(Uri.Flags.SchemeNotCanonical | Uri.Flags.UserNotCanonical | Uri.Flags.HostNotCanonical | Uri.Flags.PortNotCanonical | Uri.Flags.PathNotCanonical | Uri.Flags.QueryNotCanonical | Uri.Flags.FragmentNotCanonical | Uri.Flags.E_UserNotCanonical | Uri.Flags.E_HostNotCanonical | Uri.Flags.E_PortNotCanonical | Uri.Flags.E_PathNotCanonical | Uri.Flags.E_QueryNotCanonical | Uri.Flags.E_FragmentNotCanonical | Uri.Flags.ShouldBeCompressed | Uri.Flags.FirstSlashAbsent | Uri.Flags.BackslashInPath | Uri.Flags.MinimalUriInfoSet | Uri.Flags.AllUriInfoSet);
				int num = (int)otherUri.m_Info.Offset.Path;
				if (this.InFact(Uri.Flags.NotDefaultPort))
				{
					while (otherUri.m_String[num] != ':' && num > (int)otherUri.m_Info.Offset.Host)
					{
						num--;
					}
					if (otherUri.m_String[num] != ':')
					{
						num = (int)otherUri.m_Info.Offset.Path;
					}
				}
				this.m_Flags |= (Uri.Flags)((long)num);
			}
			this.m_Syntax = otherUri.m_Syntax;
			this.m_String = otherUri.m_String;
			this.m_iriParsing = otherUri.m_iriParsing;
			if (otherUri.OriginalStringSwitched)
			{
				this.m_originalUnicodeString = otherUri.m_originalUnicodeString;
			}
			if (otherUri.AllowIdn && (otherUri.InFact(Uri.Flags.IdnHost) || otherUri.InFact(Uri.Flags.UnicodeHost)))
			{
				this.m_DnsSafeHost = otherUri.m_DnsSafeHost;
			}
		}

		/// <summary>Specifies that the URI is a pointer to a file. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C3C RID: 3132
		public static readonly string UriSchemeFile = UriParser.FileUri.SchemeName;

		/// <summary>Specifies that the URI is accessed through the File Transfer Protocol (FTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C3D RID: 3133
		public static readonly string UriSchemeFtp = UriParser.FtpUri.SchemeName;

		/// <summary>Specifies that the URI is accessed through the Gopher protocol. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C3E RID: 3134
		public static readonly string UriSchemeGopher = UriParser.GopherUri.SchemeName;

		/// <summary>Specifies that the URI is accessed through the Hypertext Transfer Protocol (HTTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C3F RID: 3135
		public static readonly string UriSchemeHttp = UriParser.HttpUri.SchemeName;

		/// <summary>Specifies that the URI is accessed through the Secure Hypertext Transfer Protocol (HTTPS). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C40 RID: 3136
		public static readonly string UriSchemeHttps = UriParser.HttpsUri.SchemeName;

		// Token: 0x04000C41 RID: 3137
		internal static readonly string UriSchemeWs = UriParser.WsUri.SchemeName;

		// Token: 0x04000C42 RID: 3138
		internal static readonly string UriSchemeWss = UriParser.WssUri.SchemeName;

		/// <summary>Specifies that the URI is an e-mail address and is accessed through the Simple Mail Transport Protocol (SMTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C43 RID: 3139
		public static readonly string UriSchemeMailto = UriParser.MailToUri.SchemeName;

		/// <summary>Specifies that the URI is an Internet news group and is accessed through the Network News Transport Protocol (NNTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C44 RID: 3140
		public static readonly string UriSchemeNews = UriParser.NewsUri.SchemeName;

		/// <summary>Specifies that the URI is an Internet news group and is accessed through the Network News Transport Protocol (NNTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C45 RID: 3141
		public static readonly string UriSchemeNntp = UriParser.NntpUri.SchemeName;

		/// <summary>Specifies that the URI is accessed through the NetTcp scheme used by Windows Communication Foundation (WCF). This field is read-only.</summary>
		// Token: 0x04000C46 RID: 3142
		public static readonly string UriSchemeNetTcp = UriParser.NetTcpUri.SchemeName;

		/// <summary>Specifies that the URI is accessed through the NetPipe scheme used by Windows Communication Foundation (WCF). This field is read-only.</summary>
		// Token: 0x04000C47 RID: 3143
		public static readonly string UriSchemeNetPipe = UriParser.NetPipeUri.SchemeName;

		/// <summary>Specifies the characters that separate the communication protocol scheme from the address portion of the URI. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C48 RID: 3144
		public static readonly string SchemeDelimiter = "://";

		// Token: 0x04000C49 RID: 3145
		private const int c_Max16BitUtf8SequenceLength = 12;

		// Token: 0x04000C4A RID: 3146
		internal const int c_MaxUriBufferSize = 65520;

		// Token: 0x04000C4B RID: 3147
		private const int c_MaxUriSchemeName = 1024;

		// Token: 0x04000C4C RID: 3148
		private string m_String;

		// Token: 0x04000C4D RID: 3149
		private string m_originalUnicodeString;

		// Token: 0x04000C4E RID: 3150
		private UriParser m_Syntax;

		// Token: 0x04000C4F RID: 3151
		private string m_DnsSafeHost;

		// Token: 0x04000C50 RID: 3152
		private Uri.Flags m_Flags;

		// Token: 0x04000C51 RID: 3153
		private Uri.UriInfo m_Info;

		// Token: 0x04000C52 RID: 3154
		private bool m_iriParsing;

		// Token: 0x04000C53 RID: 3155
		private static volatile bool s_ConfigInitialized;

		// Token: 0x04000C54 RID: 3156
		private static volatile bool s_ConfigInitializing;

		// Token: 0x04000C55 RID: 3157
		private static volatile UriIdnScope s_IdnScope = UriIdnScope.None;

		// Token: 0x04000C56 RID: 3158
		private static volatile bool s_IriParsing = !(Environment.GetEnvironmentVariable("MONO_URI_IRIPARSING") == "false");

		// Token: 0x04000C57 RID: 3159
		private static bool useDotNetRelativeOrAbsolute = Environment.GetEnvironmentVariable("MONO_URI_DOTNETRELATIVEORABSOLUTE") == "true";

		// Token: 0x04000C58 RID: 3160
		private const UriKind DotNetRelativeOrAbsolute = (UriKind)300;

		// Token: 0x04000C59 RID: 3161
		internal static readonly bool IsWindowsFileSystem = Path.DirectorySeparatorChar == '\\';

		// Token: 0x04000C5A RID: 3162
		private static object s_initLock;

		// Token: 0x04000C5B RID: 3163
		private const UriFormat V1ToStringUnescape = (UriFormat)32767;

		// Token: 0x04000C5C RID: 3164
		internal const char c_DummyChar = '\uffff';

		// Token: 0x04000C5D RID: 3165
		internal const char c_EOL = '\ufffe';

		// Token: 0x04000C5E RID: 3166
		internal static readonly char[] HexLowerChars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f'
		};

		// Token: 0x04000C5F RID: 3167
		private static readonly char[] _WSchars = new char[] { ' ', '\n', '\r', '\t' };

		// Token: 0x020000F7 RID: 247
		[Flags]
		private enum Flags : ulong
		{
			// Token: 0x04000C61 RID: 3169
			Zero = 0UL,
			// Token: 0x04000C62 RID: 3170
			SchemeNotCanonical = 1UL,
			// Token: 0x04000C63 RID: 3171
			UserNotCanonical = 2UL,
			// Token: 0x04000C64 RID: 3172
			HostNotCanonical = 4UL,
			// Token: 0x04000C65 RID: 3173
			PortNotCanonical = 8UL,
			// Token: 0x04000C66 RID: 3174
			PathNotCanonical = 16UL,
			// Token: 0x04000C67 RID: 3175
			QueryNotCanonical = 32UL,
			// Token: 0x04000C68 RID: 3176
			FragmentNotCanonical = 64UL,
			// Token: 0x04000C69 RID: 3177
			CannotDisplayCanonical = 127UL,
			// Token: 0x04000C6A RID: 3178
			E_UserNotCanonical = 128UL,
			// Token: 0x04000C6B RID: 3179
			E_HostNotCanonical = 256UL,
			// Token: 0x04000C6C RID: 3180
			E_PortNotCanonical = 512UL,
			// Token: 0x04000C6D RID: 3181
			E_PathNotCanonical = 1024UL,
			// Token: 0x04000C6E RID: 3182
			E_QueryNotCanonical = 2048UL,
			// Token: 0x04000C6F RID: 3183
			E_FragmentNotCanonical = 4096UL,
			// Token: 0x04000C70 RID: 3184
			E_CannotDisplayCanonical = 8064UL,
			// Token: 0x04000C71 RID: 3185
			ShouldBeCompressed = 8192UL,
			// Token: 0x04000C72 RID: 3186
			FirstSlashAbsent = 16384UL,
			// Token: 0x04000C73 RID: 3187
			BackslashInPath = 32768UL,
			// Token: 0x04000C74 RID: 3188
			IndexMask = 65535UL,
			// Token: 0x04000C75 RID: 3189
			HostTypeMask = 458752UL,
			// Token: 0x04000C76 RID: 3190
			HostNotParsed = 0UL,
			// Token: 0x04000C77 RID: 3191
			IPv6HostType = 65536UL,
			// Token: 0x04000C78 RID: 3192
			IPv4HostType = 131072UL,
			// Token: 0x04000C79 RID: 3193
			DnsHostType = 196608UL,
			// Token: 0x04000C7A RID: 3194
			UncHostType = 262144UL,
			// Token: 0x04000C7B RID: 3195
			BasicHostType = 327680UL,
			// Token: 0x04000C7C RID: 3196
			UnusedHostType = 393216UL,
			// Token: 0x04000C7D RID: 3197
			UnknownHostType = 458752UL,
			// Token: 0x04000C7E RID: 3198
			UserEscaped = 524288UL,
			// Token: 0x04000C7F RID: 3199
			AuthorityFound = 1048576UL,
			// Token: 0x04000C80 RID: 3200
			HasUserInfo = 2097152UL,
			// Token: 0x04000C81 RID: 3201
			LoopbackHost = 4194304UL,
			// Token: 0x04000C82 RID: 3202
			NotDefaultPort = 8388608UL,
			// Token: 0x04000C83 RID: 3203
			UserDrivenParsing = 16777216UL,
			// Token: 0x04000C84 RID: 3204
			CanonicalDnsHost = 33554432UL,
			// Token: 0x04000C85 RID: 3205
			ErrorOrParsingRecursion = 67108864UL,
			// Token: 0x04000C86 RID: 3206
			DosPath = 134217728UL,
			// Token: 0x04000C87 RID: 3207
			UncPath = 268435456UL,
			// Token: 0x04000C88 RID: 3208
			ImplicitFile = 536870912UL,
			// Token: 0x04000C89 RID: 3209
			MinimalUriInfoSet = 1073741824UL,
			// Token: 0x04000C8A RID: 3210
			AllUriInfoSet = 2147483648UL,
			// Token: 0x04000C8B RID: 3211
			IdnHost = 4294967296UL,
			// Token: 0x04000C8C RID: 3212
			HasUnicode = 8589934592UL,
			// Token: 0x04000C8D RID: 3213
			HostUnicodeNormalized = 17179869184UL,
			// Token: 0x04000C8E RID: 3214
			RestUnicodeNormalized = 34359738368UL,
			// Token: 0x04000C8F RID: 3215
			UnicodeHost = 68719476736UL,
			// Token: 0x04000C90 RID: 3216
			IntranetUri = 137438953472UL,
			// Token: 0x04000C91 RID: 3217
			UseOrigUncdStrOffset = 274877906944UL,
			// Token: 0x04000C92 RID: 3218
			UserIriCanonical = 549755813888UL,
			// Token: 0x04000C93 RID: 3219
			PathIriCanonical = 1099511627776UL,
			// Token: 0x04000C94 RID: 3220
			QueryIriCanonical = 2199023255552UL,
			// Token: 0x04000C95 RID: 3221
			FragmentIriCanonical = 4398046511104UL,
			// Token: 0x04000C96 RID: 3222
			IriCanonical = 8246337208320UL,
			// Token: 0x04000C97 RID: 3223
			CompressedSlashes = 17592186044416UL
		}

		// Token: 0x020000F8 RID: 248
		private class UriInfo
		{
			// Token: 0x04000C98 RID: 3224
			public string Host;

			// Token: 0x04000C99 RID: 3225
			public string ScopeId;

			// Token: 0x04000C9A RID: 3226
			public string String;

			// Token: 0x04000C9B RID: 3227
			public Uri.Offset Offset;

			// Token: 0x04000C9C RID: 3228
			public string DnsSafeHost;

			// Token: 0x04000C9D RID: 3229
			public Uri.MoreInfo MoreInfo;
		}

		// Token: 0x020000F9 RID: 249
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct Offset
		{
			// Token: 0x04000C9E RID: 3230
			public ushort Scheme;

			// Token: 0x04000C9F RID: 3231
			public ushort User;

			// Token: 0x04000CA0 RID: 3232
			public ushort Host;

			// Token: 0x04000CA1 RID: 3233
			public ushort PortValue;

			// Token: 0x04000CA2 RID: 3234
			public ushort Path;

			// Token: 0x04000CA3 RID: 3235
			public ushort Query;

			// Token: 0x04000CA4 RID: 3236
			public ushort Fragment;

			// Token: 0x04000CA5 RID: 3237
			public ushort End;
		}

		// Token: 0x020000FA RID: 250
		private class MoreInfo
		{
			// Token: 0x04000CA6 RID: 3238
			public string Path;

			// Token: 0x04000CA7 RID: 3239
			public string Query;

			// Token: 0x04000CA8 RID: 3240
			public string Fragment;

			// Token: 0x04000CA9 RID: 3241
			public string AbsoluteUri;

			// Token: 0x04000CAA RID: 3242
			public int Hash;

			// Token: 0x04000CAB RID: 3243
			public string RemoteUrl;
		}

		// Token: 0x020000FB RID: 251
		[Flags]
		private enum Check
		{
			// Token: 0x04000CAD RID: 3245
			None = 0,
			// Token: 0x04000CAE RID: 3246
			EscapedCanonical = 1,
			// Token: 0x04000CAF RID: 3247
			DisplayCanonical = 2,
			// Token: 0x04000CB0 RID: 3248
			DotSlashAttn = 4,
			// Token: 0x04000CB1 RID: 3249
			DotSlashEscaped = 128,
			// Token: 0x04000CB2 RID: 3250
			BackslashInPath = 16,
			// Token: 0x04000CB3 RID: 3251
			ReservedFound = 32,
			// Token: 0x04000CB4 RID: 3252
			NotIriCanonical = 64,
			// Token: 0x04000CB5 RID: 3253
			FoundNonAscii = 8
		}
	}
}
