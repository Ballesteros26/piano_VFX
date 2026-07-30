using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>Represents the version number of an assembly, operating system, or the common language runtime. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001EB RID: 491
	[ComVisible(true)]
	[Serializable]
	public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class with the specified major, minor, build, and revision numbers.</summary>
		/// <param name="major">The major version number. </param>
		/// <param name="minor">The minor version number. </param>
		/// <param name="build">The build number. </param>
		/// <param name="revision">The revision number. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, <paramref name="build" />, or <paramref name="revision" /> is less than zero. </exception>
		// Token: 0x06001683 RID: 5763 RVA: 0x000597BC File Offset: 0x000579BC
		public Version(int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = revision;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major, minor, and build values.</summary>
		/// <param name="major">The major version number. </param>
		/// <param name="minor">The minor version number. </param>
		/// <param name="build">The build number. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, or <paramref name="build" /> is less than zero. </exception>
		// Token: 0x06001684 RID: 5764 RVA: 0x00059860 File Offset: 0x00057A60
		public Version(int major, int minor, int build)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major and minor values.</summary>
		/// <param name="major">The major version number. </param>
		/// <param name="minor">The minor version number. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" /> or <paramref name="minor" /> is less than zero. </exception>
		// Token: 0x06001685 RID: 5765 RVA: 0x000598E4 File Offset: 0x00057AE4
		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
			}
			this._Major = major;
			this._Minor = minor;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified string.</summary>
		/// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.'). </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> has fewer than two components or more than four components. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A major, minor, build, or revision component is less than zero. </exception>
		/// <exception cref="T:System.FormatException">At least one component of <paramref name="version" /> does not parse to an integer. </exception>
		/// <exception cref="T:System.OverflowException">At least one component of <paramref name="version" /> represents a number greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001686 RID: 5766 RVA: 0x00059948 File Offset: 0x00057B48
		public Version(string version)
		{
			Version version2 = Version.Parse(version);
			this._Major = version2.Major;
			this._Minor = version2.Minor;
			this._Build = version2.Build;
			this._Revision = version2.Revision;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class.</summary>
		// Token: 0x06001687 RID: 5767 RVA: 0x000599A0 File Offset: 0x00057BA0
		public Version()
		{
			this._Major = 0;
			this._Minor = 0;
		}

		/// <summary>Gets the value of the major component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The major version number.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x000599C4 File Offset: 0x00057BC4
		public int Major
		{
			get
			{
				return this._Major;
			}
		}

		/// <summary>Gets the value of the minor component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The minor version number.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x000599CC File Offset: 0x00057BCC
		public int Minor
		{
			get
			{
				return this._Minor;
			}
		}

		/// <summary>Gets the value of the build component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The build number, or -1 if the build number is undefined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x000599D4 File Offset: 0x00057BD4
		public int Build
		{
			get
			{
				return this._Build;
			}
		}

		/// <summary>Gets the value of the revision component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The revision number, or -1 if the revision number is undefined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x000599DC File Offset: 0x00057BDC
		public int Revision
		{
			get
			{
				return this._Revision;
			}
		}

		/// <summary>Gets the high 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x000599E4 File Offset: 0x00057BE4
		public short MajorRevision
		{
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		/// <summary>Gets the low 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x000599F0 File Offset: 0x00057BF0
		public short MinorRevision
		{
			get
			{
				return (short)(this._Revision & 65535);
			}
		}

		/// <summary>Returns a new <see cref="T:System.Version" /> object whose value is the same as the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A new <see cref="T:System.Object" /> whose values are a copy of the current <see cref="T:System.Version" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600168E RID: 5774 RVA: 0x000599FF File Offset: 0x00057BFF
		public object Clone()
		{
			return new Version
			{
				_Major = this._Major,
				_Minor = this._Minor,
				_Build = this._Build,
				_Revision = this._Revision
			};
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative values of the two objects, as shown in the following table.Return value Meaning Less than zero The current <see cref="T:System.Version" /> object is a version before <paramref name="version" />. Zero The current <see cref="T:System.Version" /> object is the same version as <paramref name="version" />. Greater than zero The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="version" />.-or- <paramref name="version" /> is null. </returns>
		/// <param name="version">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> is not of type <see cref="T:System.Version" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600168F RID: 5775 RVA: 0x00059A38 File Offset: 0x00057C38
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			Version version2 = version as Version;
			if (version2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Version."));
			}
			if (this._Major != version2._Major)
			{
				if (this._Major > version2._Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Minor != version2._Minor)
			{
				if (this._Minor > version2._Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Build != version2._Build)
			{
				if (this._Build > version2._Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this._Revision == version2._Revision)
				{
					return 0;
				}
				if (this._Revision > version2._Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified <see cref="T:System.Version" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative values of the two objects, as shown in the following table.Return value Meaning Less than zero The current <see cref="T:System.Version" /> object is a version before <paramref name="value" />. Zero The current <see cref="T:System.Version" /> object is the same version as <paramref name="value" />. Greater than zero The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="value" />. -or-<paramref name="value" /> is null.</returns>
		/// <param name="value">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or null.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001690 RID: 5776 RVA: 0x00059AEC File Offset: 0x00057CEC
		public int CompareTo(Version value)
		{
			if (value == null)
			{
				return 1;
			}
			if (this._Major != value._Major)
			{
				if (this._Major > value._Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Minor != value._Minor)
			{
				if (this._Minor > value._Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Build != value._Build)
			{
				if (this._Build > value._Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this._Revision == value._Revision)
				{
					return 0;
				}
				if (this._Revision > value._Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object is equal to a specified object.</summary>
		/// <returns>true if the current <see cref="T:System.Version" /> object and <paramref name="obj" /> are both <see cref="T:System.Version" /> objects, and every component of the current <see cref="T:System.Version" /> object matches the corresponding component of <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">An object to compare with the current <see cref="T:System.Version" /> object, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001691 RID: 5777 RVA: 0x00059B88 File Offset: 0x00057D88
		public override bool Equals(object obj)
		{
			Version version = obj as Version;
			return !(version == null) && this._Major == version._Major && this._Minor == version._Minor && this._Build == version._Build && this._Revision == version._Revision;
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object and a specified <see cref="T:System.Version" /> object represent the same value.</summary>
		/// <returns>true if every component of the current <see cref="T:System.Version" /> object matches the corresponding component of the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or null.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001692 RID: 5778 RVA: 0x00059BE4 File Offset: 0x00057DE4
		public bool Equals(Version obj)
		{
			return !(obj == null) && this._Major == obj._Major && this._Minor == obj._Minor && this._Build == obj._Build && this._Revision == obj._Revision;
		}

		/// <summary>Returns a hash code for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001693 RID: 5779 RVA: 0x00059C37 File Offset: 0x00057E37
		public override int GetHashCode()
		{
			return 0 | ((this._Major & 15) << 28) | ((this._Minor & 255) << 20) | ((this._Build & 255) << 12) | (this._Revision & 4095);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, as depicted in the following format. Each component is separated by a period character ('.'). Square brackets ('[' and ']') indicate a component that will not appear in the return value if the component is not defined: major.minor[.build[.revision]] For example, if you create a <see cref="T:System.Version" /> object using the constructor Version(1,1), the returned string is "1.1". If you create a <see cref="T:System.Version" /> object using the constructor Version(1,3,4,2), the returned string is "1.3.4.2".</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001694 RID: 5780 RVA: 0x00059C74 File Offset: 0x00057E74
		public override string ToString()
		{
			if (this._Build == -1)
			{
				return this.ToString(2);
			}
			if (this._Revision == -1)
			{
				return this.ToString(3);
			}
			return this.ToString(4);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation. A specified count indicates the number of components to return.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, each separated by a period character ('.'). The <paramref name="fieldCount" /> parameter determines how many components are returned.fieldCount Return Value 0 An empty string (""). 1 major 2 major.minor 3 major.minor.build 4 major.minor.build.revision For example, if you create <see cref="T:System.Version" /> object using the constructor Version(1,3,5), ToString(2) returns "1.3" and ToString(4) throws an exception.</returns>
		/// <param name="fieldCount">The number of components to return. The <paramref name="fieldCount" /> ranges from 0 to 4. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fieldCount" /> is less than 0, or more than 4.-or- <paramref name="fieldCount" /> is more than the number of components defined in the current <see cref="T:System.Version" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001695 RID: 5781 RVA: 0x00059CA0 File Offset: 0x00057EA0
		public string ToString(int fieldCount)
		{
			switch (fieldCount)
			{
			case 0:
				return string.Empty;
			case 1:
				return this._Major.ToString();
			case 2:
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				Version.AppendPositiveNumber(this._Major, stringBuilder);
				stringBuilder.Append('.');
				Version.AppendPositiveNumber(this._Minor, stringBuilder);
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
			default:
				if (this._Build == -1)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument must be between {0} and {1}.", new object[] { "0", "2" }), "fieldCount");
				}
				if (fieldCount == 3)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					Version.AppendPositiveNumber(this._Major, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Minor, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Build, stringBuilder);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				if (this._Revision == -1)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument must be between {0} and {1}.", new object[] { "0", "3" }), "fieldCount");
				}
				if (fieldCount == 4)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					Version.AppendPositiveNumber(this._Major, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Minor, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Build, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Revision, stringBuilder);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				throw new ArgumentException(Environment.GetResourceString("Argument must be between {0} and {1}.", new object[] { "0", "4" }), "fieldCount");
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00059E3C File Offset: 0x0005803C
		private static void AppendPositiveNumber(int num, StringBuilder sb)
		{
			int length = sb.Length;
			do
			{
				int num2 = num % 10;
				num /= 10;
				sb.Insert(length, (char)(48 + num2));
			}
			while (num > 0);
		}

		/// <summary>Converts the string representation of a version number to an equivalent <see cref="T:System.Version" /> object.</summary>
		/// <returns>An object that is equivalent to the version number specified in the <paramref name="input" /> parameter.</returns>
		/// <param name="input">A string that contains a version number to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="input" /> has fewer than two or more than four version components.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">At least one component in <paramref name="input" /> is less than zero.</exception>
		/// <exception cref="T:System.FormatException">At least one component in <paramref name="input" /> is not an integer.</exception>
		/// <exception cref="T:System.OverflowException">At least one component in <paramref name="input" /> represents a number that is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001697 RID: 5783 RVA: 0x00059E6C File Offset: 0x0005806C
		public static Version Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Version.VersionResult versionResult = default(Version.VersionResult);
			versionResult.Init("input", true);
			if (!Version.TryParseVersion(input, ref versionResult))
			{
				throw versionResult.GetVersionParseException();
			}
			return versionResult.m_parsedVersion;
		}

		/// <summary>Tries to convert the string representation of a version number to an equivalent <see cref="T:System.Version" /> object, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="input" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="input">A string that contains a version number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Version" /> equivalent of the number that is contained in <paramref name="input" />, if the conversion succeeded, or a <see cref="T:System.Version" /> object whose major and minor version numbers are 0 if the conversion failed.</param>
		// Token: 0x06001698 RID: 5784 RVA: 0x00059EB4 File Offset: 0x000580B4
		public static bool TryParse(string input, out Version result)
		{
			Version.VersionResult versionResult = default(Version.VersionResult);
			versionResult.Init("input", false);
			bool flag = Version.TryParseVersion(input, ref versionResult);
			result = versionResult.m_parsedVersion;
			return flag;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00059EE8 File Offset: 0x000580E8
		private static bool TryParseVersion(string version, ref Version.VersionResult result)
		{
			if (version == null)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentNullException);
				return false;
			}
			string[] array = version.Split(Version.SeparatorsArray);
			int num = array.Length;
			if (num < 2 || num > 4)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentException);
				return false;
			}
			int num2;
			if (!Version.TryParseComponent(array[0], "version", ref result, out num2))
			{
				return false;
			}
			int num3;
			if (!Version.TryParseComponent(array[1], "version", ref result, out num3))
			{
				return false;
			}
			num -= 2;
			if (num > 0)
			{
				int num4;
				if (!Version.TryParseComponent(array[2], "build", ref result, out num4))
				{
					return false;
				}
				num--;
				if (num > 0)
				{
					int num5;
					if (!Version.TryParseComponent(array[3], "revision", ref result, out num5))
					{
						return false;
					}
					result.m_parsedVersion = new Version(num2, num3, num4, num5);
				}
				else
				{
					result.m_parsedVersion = new Version(num2, num3, num4);
				}
			}
			else
			{
				result.m_parsedVersion = new Version(num2, num3);
			}
			return true;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00059FC0 File Offset: 0x000581C0
		private static bool TryParseComponent(string component, string componentName, ref Version.VersionResult result, out int parsedComponent)
		{
			if (!int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent))
			{
				result.SetFailure(Version.ParseFailureKind.FormatException, component);
				return false;
			}
			if (parsedComponent < 0)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentOutOfRangeException, componentName);
				return false;
			}
			return true;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are equal.</summary>
		/// <returns>true if <paramref name="v1" /> equals <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600169B RID: 5787 RVA: 0x00059FEB File Offset: 0x000581EB
		public static bool operator ==(Version v1, Version v2)
		{
			if (v1 == null)
			{
				return v2 == null;
			}
			return v1.Equals(v2);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="v1" /> does not equal <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600169C RID: 5788 RVA: 0x00059FFC File Offset: 0x000581FC
		public static bool operator !=(Version v1, Version v2)
		{
			return !(v1 == v2);
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is less than <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is null. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600169D RID: 5789 RVA: 0x0005A008 File Offset: 0x00058208
		public static bool operator <(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) < 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than or equal to the second <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is less than or equal to <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is null. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600169E RID: 5790 RVA: 0x0005A022 File Offset: 0x00058222
		public static bool operator <=(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) <= 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is greater than <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600169F RID: 5791 RVA: 0x0005A03F File Offset: 0x0005823F
		public static bool operator >(Version v1, Version v2)
		{
			return v2 < v1;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than or equal to the second specified <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is greater than or equal to <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060016A0 RID: 5792 RVA: 0x0005A048 File Offset: 0x00058248
		public static bool operator >=(Version v1, Version v2)
		{
			return v2 <= v1;
		}

		// Token: 0x04000BDB RID: 3035
		private int _Major;

		// Token: 0x04000BDC RID: 3036
		private int _Minor;

		// Token: 0x04000BDD RID: 3037
		private int _Build = -1;

		// Token: 0x04000BDE RID: 3038
		private int _Revision = -1;

		// Token: 0x04000BDF RID: 3039
		private static readonly char[] SeparatorsArray = new char[] { '.' };

		// Token: 0x04000BE0 RID: 3040
		private const int ZERO_CHAR_VALUE = 48;

		// Token: 0x020001EC RID: 492
		internal enum ParseFailureKind
		{
			// Token: 0x04000BE2 RID: 3042
			ArgumentNullException,
			// Token: 0x04000BE3 RID: 3043
			ArgumentException,
			// Token: 0x04000BE4 RID: 3044
			ArgumentOutOfRangeException,
			// Token: 0x04000BE5 RID: 3045
			FormatException
		}

		// Token: 0x020001ED RID: 493
		internal struct VersionResult
		{
			// Token: 0x060016A2 RID: 5794 RVA: 0x0005A063 File Offset: 0x00058263
			internal void Init(string argumentName, bool canThrow)
			{
				this.m_canThrow = canThrow;
				this.m_argumentName = argumentName;
			}

			// Token: 0x060016A3 RID: 5795 RVA: 0x0005A073 File Offset: 0x00058273
			internal void SetFailure(Version.ParseFailureKind failure)
			{
				this.SetFailure(failure, string.Empty);
			}

			// Token: 0x060016A4 RID: 5796 RVA: 0x0005A081 File Offset: 0x00058281
			internal void SetFailure(Version.ParseFailureKind failure, string argument)
			{
				this.m_failure = failure;
				this.m_exceptionArgument = argument;
				if (this.m_canThrow)
				{
					throw this.GetVersionParseException();
				}
			}

			// Token: 0x060016A5 RID: 5797 RVA: 0x0005A0A0 File Offset: 0x000582A0
			internal Exception GetVersionParseException()
			{
				switch (this.m_failure)
				{
				case Version.ParseFailureKind.ArgumentNullException:
					return new ArgumentNullException(this.m_argumentName);
				case Version.ParseFailureKind.ArgumentException:
					return new ArgumentException(Environment.GetResourceString("Version string portion was too short or too long."));
				case Version.ParseFailureKind.ArgumentOutOfRangeException:
					return new ArgumentOutOfRangeException(this.m_exceptionArgument, Environment.GetResourceString("Version's parameters must be greater than or equal to zero."));
				case Version.ParseFailureKind.FormatException:
					try
					{
						int.Parse(this.m_exceptionArgument, CultureInfo.InvariantCulture);
					}
					catch (FormatException ex)
					{
						return ex;
					}
					catch (OverflowException ex)
					{
						return ex;
					}
					return new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
				default:
					return new ArgumentException(Environment.GetResourceString("Version string portion was too short or too long."));
				}
			}

			// Token: 0x04000BE6 RID: 3046
			internal Version m_parsedVersion;

			// Token: 0x04000BE7 RID: 3047
			internal Version.ParseFailureKind m_failure;

			// Token: 0x04000BE8 RID: 3048
			internal string m_exceptionArgument;

			// Token: 0x04000BE9 RID: 3049
			internal string m_argumentName;

			// Token: 0x04000BEA RID: 3050
			internal bool m_canThrow;
		}
	}
}
