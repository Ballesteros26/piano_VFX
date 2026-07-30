using System;
using System.Text;

namespace System.Runtime.Versioning
{
	/// <summary>Represents the name of a version of the .NET Framework.</summary>
	// Token: 0x0200035C RID: 860
	[Serializable]
	public sealed class FrameworkName : IEquatable<FrameworkName>
	{
		/// <summary>Gets the identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0006B7F3 File Offset: 0x000699F3
		public string Identifier
		{
			get
			{
				return this.m_identifier;
			}
		}

		/// <summary>Gets the version of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>An object that contains version information about this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0006B7FB File Offset: 0x000699FB
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		/// <summary>Gets the profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0006B803 File Offset: 0x00069A03
		public string Profile
		{
			get
			{
				return this.m_profile;
			}
		}

		/// <summary>Gets the full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0006B80C File Offset: 0x00069A0C
		public string FullName
		{
			get
			{
				if (this.m_fullName == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.Identifier);
					stringBuilder.Append(',');
					stringBuilder.Append("Version").Append('=');
					stringBuilder.Append('v');
					stringBuilder.Append(this.Version);
					if (!string.IsNullOrEmpty(this.Profile))
					{
						stringBuilder.Append(',');
						stringBuilder.Append("Profile").Append('=');
						stringBuilder.Append(this.Profile);
					}
					this.m_fullName = stringBuilder.ToString();
				}
				return this.m_fullName;
			}
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified object.</summary>
		/// <returns>true if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		// Token: 0x06001AB0 RID: 6832 RVA: 0x0006B8B1 File Offset: 0x00069AB1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FrameworkName);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance.</summary>
		/// <returns>true if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The object to compare to the current instance.</param>
		// Token: 0x06001AB1 RID: 6833 RVA: 0x0006B8BF File Offset: 0x00069ABF
		public bool Equals(FrameworkName other)
		{
			return other != null && (this.Identifier == other.Identifier && this.Version == other.Version) && this.Profile == other.Profile;
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A 32-bit signed integer that represents the hash code of this instance.</returns>
		// Token: 0x06001AB2 RID: 6834 RVA: 0x0006B8FF File Offset: 0x00069AFF
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode() ^ this.Version.GetHashCode() ^ this.Profile.GetHashCode();
		}

		/// <summary>Returns the string representation of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x06001AB3 RID: 6835 RVA: 0x0006B924 File Offset: 0x00069B24
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string and a <see cref="T:System.Version" /> object that identify a .NET Framework version.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version. </param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is null.-or-<paramref name="version" /> is null.</exception>
		// Token: 0x06001AB4 RID: 6836 RVA: 0x0006B92C File Offset: 0x00069B2C
		public FrameworkName(string identifier, Version version)
			: this(identifier, version, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string, a <see cref="T:System.Version" /> object that identifies a .NET Framework version, and a profile name.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version.</param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <param name="profile">A profile name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is null.-or-<paramref name="version" /> is null.</exception>
		// Token: 0x06001AB5 RID: 6837 RVA: 0x0006B938 File Offset: 0x00069B38
		public FrameworkName(string identifier, Version version, string profile)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (identifier.Trim().Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "identifier" }), "identifier");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.m_identifier = identifier.Trim();
			this.m_version = (Version)version.Clone();
			this.m_profile = ((profile == null) ? string.Empty : profile.Trim());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string that contains information about a version of the .NET Framework.</summary>
		/// <param name="frameworkName">A string that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="frameworkName" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="frameworkName" /> has fewer than two components or more than three components.-or-<paramref name="frameworkName" /> does not include a major and minor version number.-or-<paramref name="frameworkName " />does not include a valid version number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is null.</exception>
		// Token: 0x06001AB6 RID: 6838 RVA: 0x0006B9D0 File Offset: 0x00069BD0
		public FrameworkName(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "frameworkName" }), "frameworkName");
			}
			string[] array = frameworkName.Split(new char[] { ',' });
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(global::SR.GetString("FrameworkName cannot have less than two components or more than three components."), "frameworkName");
			}
			this.m_identifier = array[0].Trim();
			if (this.m_identifier.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("FrameworkName is invalid."), "frameworkName");
			}
			bool flag = false;
			this.m_profile = string.Empty;
			int i = 1;
			while (i < array.Length)
			{
				string[] array2 = array[i].Split(new char[] { '=' });
				if (array2.Length != 2)
				{
					throw new ArgumentException(global::SR.GetString("FrameworkName is invalid."), "frameworkName");
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					try
					{
						this.m_version = new Version(text2);
						goto IL_0191;
					}
					catch (Exception ex)
					{
						throw new ArgumentException(global::SR.GetString("FrameworkName version component is invalid."), "frameworkName", ex);
					}
					goto IL_015B;
				}
				goto IL_015B;
				IL_0191:
				i++;
				continue;
				IL_015B:
				if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(global::SR.GetString("FrameworkName is invalid."), "frameworkName");
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_profile = text2;
					goto IL_0191;
				}
				goto IL_0191;
			}
			if (!flag)
			{
				throw new ArgumentException(global::SR.GetString("FrameworkName version component is missing."), "frameworkName");
			}
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent the same .NET Framework version.</summary>
		/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters represent the same .NET Framework version; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06001AB7 RID: 6839 RVA: 0x0006BBA4 File Offset: 0x00069DA4
		public static bool operator ==(FrameworkName left, FrameworkName right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent different .NET Framework versions.</summary>
		/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters represent different .NET Framework versions; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06001AB8 RID: 6840 RVA: 0x0006BBB5 File Offset: 0x00069DB5
		public static bool operator !=(FrameworkName left, FrameworkName right)
		{
			return !(left == right);
		}

		// Token: 0x04001849 RID: 6217
		private readonly string m_identifier;

		// Token: 0x0400184A RID: 6218
		private readonly Version m_version;

		// Token: 0x0400184B RID: 6219
		private readonly string m_profile;

		// Token: 0x0400184C RID: 6220
		private string m_fullName;

		// Token: 0x0400184D RID: 6221
		private const char c_componentSeparator = ',';

		// Token: 0x0400184E RID: 6222
		private const char c_keyValueSeparator = '=';

		// Token: 0x0400184F RID: 6223
		private const char c_versionValuePrefix = 'v';

		// Token: 0x04001850 RID: 6224
		private const string c_versionKey = "Version";

		// Token: 0x04001851 RID: 6225
		private const string c_profileKey = "Profile";
	}
}
