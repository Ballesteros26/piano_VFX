using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the network resource access that is granted to code.</summary>
	// Token: 0x0200055D RID: 1373
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.CodeConnectAccess" /> class. </summary>
		/// <param name="allowScheme">The URI scheme represented by the current instance.</param>
		/// <param name="allowPort">The port represented by the current instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowScheme" /> is null.-or-<paramref name="allowScheme" /> is an empty string ("").-or-<paramref name="allowScheme" /> contains characters that are not permitted in schemes.-or-<paramref name="allowPort" /> is less than 0.-or-<paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000DD684 File Offset: 0x000DB884
		[MonoTODO("(2.0) validations incomplete")]
		public CodeConnectAccess(string allowScheme, int allowPort)
		{
			if (allowScheme == null || allowScheme.Length == 0)
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			if (allowPort < 0 || allowPort > 65535)
			{
				throw new ArgumentOutOfRangeException("allowPort");
			}
			this._scheme = allowScheme;
			this._port = allowPort;
		}

		/// <summary>Gets the port represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that identifies a computer port used in conjunction with the <see cref="P:System.Security.Policy.CodeConnectAccess.Scheme" /> property.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x000DD6D2 File Offset: 0x000DB8D2
		public int Port
		{
			get
			{
				return this._port;
			}
		}

		/// <summary>Gets the URI scheme represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that identifies a URI scheme, converted to lowercase.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x000DD6DA File Offset: 0x000DB8DA
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Security.Policy.CodeConnectAccess" /> objects represent the same scheme and port.</summary>
		/// <returns>true if the two objects represent the same scheme and port; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.CodeConnectAccess" /> object.</param>
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000DD6E4 File Offset: 0x000DB8E4
		public override bool Equals(object o)
		{
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this._scheme == codeConnectAccess._scheme && this._port == codeConnectAccess._port;
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000DD720 File Offset: 0x000DB920
		public override int GetHashCode()
		{
			return this._scheme.GetHashCode() ^ this._port;
		}

		/// <summary>Returns a <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance that represents access to the specified port using any scheme.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance for the specified port.</returns>
		/// <param name="allowPort">The port represented by the returned instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowPort" /> is less than 0.-or-<paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06003DB6 RID: 15798 RVA: 0x000DD734 File Offset: 0x000DB934
		public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
		{
			return new CodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
		}

		/// <summary>Returns a <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance that represents access to the specified port using the code's scheme of origin.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance for the specified port.</returns>
		/// <param name="allowPort">The port represented by the returned instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowPort" /> is less than 0.-or-<paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000DD741 File Offset: 0x000DB941
		public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
		{
			return new CodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
		}

		/// <summary>Contains the string value that represents the scheme wildcard.</summary>
		// Token: 0x04001FA6 RID: 8102
		public static readonly string AnyScheme = "*";

		/// <summary>Contains the value used to represent the default port.</summary>
		// Token: 0x04001FA7 RID: 8103
		public static readonly int DefaultPort = -3;

		/// <summary>Contains the value used to represent the port value in the URI where code originated.</summary>
		// Token: 0x04001FA8 RID: 8104
		public static readonly int OriginPort = -4;

		/// <summary>Contains the value used to represent the scheme in the URL where the code originated.</summary>
		// Token: 0x04001FA9 RID: 8105
		public static readonly string OriginScheme = "$origin";

		// Token: 0x04001FAA RID: 8106
		private string _scheme;

		// Token: 0x04001FAB RID: 8107
		private int _port;
	}
}
