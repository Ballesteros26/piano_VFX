using System;

namespace System.Globalization
{
	/// <summary>Provides information about the version of Unicode used to compare and order strings.</summary>
	// Token: 0x02000423 RID: 1059
	[Serializable]
	public sealed class SortVersion : IEquatable<SortVersion>
	{
		/// <summary>Gets the full version number of the <see cref="T:System.Globalization.SortVersion" /> object.</summary>
		/// <returns>The version number of this <see cref="T:System.Globalization.SortVersion" /> object.</returns>
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000B5F2E File Offset: 0x000B412E
		public int FullVersion
		{
			get
			{
				return this.m_NlsVersion;
			}
		}

		/// <summary>Gets a globally unique identifier for this <see cref="T:System.Globalization.SortVersion" /> object.</summary>
		/// <returns>A globally unique identifier for this <see cref="T:System.Globalization.SortVersion" /> object.</returns>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000B5F36 File Offset: 0x000B4136
		public Guid SortId
		{
			get
			{
				return this.m_SortId;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Globalization.SortVersion" /> class.</summary>
		/// <param name="fullVersion">A version number.</param>
		/// <param name="sortId">A sort ID.</param>
		// Token: 0x060032BA RID: 12986 RVA: 0x000B5F3E File Offset: 0x000B413E
		public SortVersion(int fullVersion, Guid sortId)
		{
			this.m_SortId = sortId;
			this.m_NlsVersion = fullVersion;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000B5F54 File Offset: 0x000B4154
		internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
		{
			this.m_NlsVersion = nlsVersion;
			if (customVersion == Guid.Empty)
			{
				byte b = (byte)((uint)effectiveId >> 24);
				byte b2 = (byte)((effectiveId & 16711680) >> 16);
				byte b3 = (byte)((effectiveId & 65280) >> 8);
				byte b4 = (byte)(effectiveId & 255);
				customVersion = new Guid(0, 0, 0, 0, 0, 0, 0, b, b2, b3, b4);
			}
			this.m_SortId = customVersion;
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Globalization.SortVersion" /> instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Globalization.SortVersion" /> object that represents the same version as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance.</param>
		// Token: 0x060032BC RID: 12988 RVA: 0x000B5FBC File Offset: 0x000B41BC
		public override bool Equals(object obj)
		{
			SortVersion sortVersion = obj as SortVersion;
			return sortVersion != null && this.Equals(sortVersion);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Globalization.SortVersion" /> instance is equal to a specified <see cref="T:System.Globalization.SortVersion" /> object.</summary>
		/// <returns>true if <paramref name="other" /> represents the same version as this instance; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		// Token: 0x060032BD RID: 12989 RVA: 0x000B5FE2 File Offset: 0x000B41E2
		public bool Equals(SortVersion other)
		{
			return !(other == null) && this.m_NlsVersion == other.m_NlsVersion && this.m_SortId == other.m_SortId;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032BE RID: 12990 RVA: 0x000B6010 File Offset: 0x000B4210
		public override int GetHashCode()
		{
			return (this.m_NlsVersion * 7) | this.m_SortId.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Globalization.SortVersion" /> instances are equal.</summary>
		/// <returns>true if the values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		// Token: 0x060032BF RID: 12991 RVA: 0x000B602C File Offset: 0x000B422C
		public static bool operator ==(SortVersion left, SortVersion right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null || right.Equals(left);
		}

		/// <summary>Indicates whether two <see cref="T:System.Globalization.SortVersion" /> instances are not equal.</summary>
		/// <returns>true if the values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		// Token: 0x060032C0 RID: 12992 RVA: 0x000B6045 File Offset: 0x000B4245
		public static bool operator !=(SortVersion left, SortVersion right)
		{
			return !(left == right);
		}

		// Token: 0x04001AEC RID: 6892
		private int m_NlsVersion;

		// Token: 0x04001AED RID: 6893
		private Guid m_SortId;
	}
}
