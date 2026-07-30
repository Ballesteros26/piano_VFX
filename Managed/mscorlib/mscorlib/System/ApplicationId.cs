using System;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System
{
	/// <summary>Contains information used to uniquely identify a manifest-based application. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000125 RID: 293
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationId
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00002111 File Offset: 0x00000311
		internal ApplicationId()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationId" /> class.</summary>
		/// <param name="publicKeyToken">The array of bytes representing the raw public key data. </param>
		/// <param name="name">The name of the application. </param>
		/// <param name="version">A <see cref="T:System.Version" /> object that specifies the version of the application. </param>
		/// <param name="processorArchitecture">The processor architecture of the application. </param>
		/// <param name="culture">The culture of the application. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name " />is null.-or-<paramref name="version " />is null.-or-<paramref name="publicKeyToken " />is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name " />is an empty string.</exception>
		// Token: 0x06000A3B RID: 2619 RVA: 0x000325E8 File Offset: 0x000307E8
		public ApplicationId(byte[] publicKeyToken, string name, Version version, string processorArchitecture, string culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("ApplicationId cannot have an empty string for the name."));
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (publicKeyToken == null)
			{
				throw new ArgumentNullException("publicKeyToken");
			}
			this.m_publicKeyToken = new byte[publicKeyToken.Length];
			Array.Copy(publicKeyToken, 0, this.m_publicKeyToken, 0, publicKeyToken.Length);
			this.m_name = name;
			this.m_version = version;
			this.m_processorArchitecture = processorArchitecture;
			this.m_culture = culture;
		}

		/// <summary>Gets the public key token for the application.</summary>
		/// <returns>A byte array containing the public key token for the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00032680 File Offset: 0x00030880
		public byte[] PublicKeyToken
		{
			get
			{
				byte[] array = new byte[this.m_publicKeyToken.Length];
				Array.Copy(this.m_publicKeyToken, 0, array, 0, this.m_publicKeyToken.Length);
				return array;
			}
		}

		/// <summary>Gets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x000326B2 File Offset: 0x000308B2
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the version of the application.</summary>
		/// <returns>A <see cref="T:System.Version" /> that specifies the version of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000326BA File Offset: 0x000308BA
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		/// <summary>Gets the target processor architecture for the application.</summary>
		/// <returns>The processor architecture of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000326C2 File Offset: 0x000308C2
		public string ProcessorArchitecture
		{
			get
			{
				return this.m_processorArchitecture;
			}
		}

		/// <summary>Gets a string representing the culture information for the application.</summary>
		/// <returns>The culture information for the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000326CA File Offset: 0x000308CA
		public string Culture
		{
			get
			{
				return this.m_culture;
			}
		}

		/// <summary>Creates and returns an identical copy of the current application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object that represents an exact copy of the original.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000A41 RID: 2625 RVA: 0x000326D2 File Offset: 0x000308D2
		public ApplicationId Copy()
		{
			return new ApplicationId(this.m_publicKeyToken, this.m_name, this.m_version, this.m_processorArchitecture, this.m_culture);
		}

		/// <summary>Creates and returns a string representation of the application identity.</summary>
		/// <returns>A string representation of the application identity.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000A42 RID: 2626 RVA: 0x000326F8 File Offset: 0x000308F8
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append(this.m_name);
			if (this.m_culture != null)
			{
				stringBuilder.Append(", culture=\"");
				stringBuilder.Append(this.m_culture);
				stringBuilder.Append("\"");
			}
			stringBuilder.Append(", version=\"");
			stringBuilder.Append(this.m_version.ToString());
			stringBuilder.Append("\"");
			if (this.m_publicKeyToken != null)
			{
				stringBuilder.Append(", publicKeyToken=\"");
				stringBuilder.Append(Hex.EncodeHexString(this.m_publicKeyToken));
				stringBuilder.Append("\"");
			}
			if (this.m_processorArchitecture != null)
			{
				stringBuilder.Append(", processorArchitecture =\"");
				stringBuilder.Append(this.m_processorArchitecture);
				stringBuilder.Append("\"");
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Determines whether the specified <see cref="T:System.ApplicationId" /> object is equivalent to the current <see cref="T:System.ApplicationId" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ApplicationId" /> object is equivalent to the current <see cref="T:System.ApplicationId" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.ApplicationId" /> object to compare to the current <see cref="T:System.ApplicationId" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000A43 RID: 2627 RVA: 0x000327D8 File Offset: 0x000309D8
		public override bool Equals(object o)
		{
			ApplicationId applicationId = o as ApplicationId;
			if (applicationId == null)
			{
				return false;
			}
			if (!object.Equals(this.m_name, applicationId.m_name) || !object.Equals(this.m_version, applicationId.m_version) || !object.Equals(this.m_processorArchitecture, applicationId.m_processorArchitecture) || !object.Equals(this.m_culture, applicationId.m_culture))
			{
				return false;
			}
			if (this.m_publicKeyToken.Length != applicationId.m_publicKeyToken.Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_publicKeyToken.Length; i++)
			{
				if (this.m_publicKeyToken[i] != applicationId.m_publicKeyToken[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the hash code for the current application identity.</summary>
		/// <returns>The hash code for the current application identity.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000A44 RID: 2628 RVA: 0x0003287B File Offset: 0x00030A7B
		public override int GetHashCode()
		{
			return this.m_name.GetHashCode() ^ this.m_version.GetHashCode();
		}

		// Token: 0x04000797 RID: 1943
		private string m_name;

		// Token: 0x04000798 RID: 1944
		private Version m_version;

		// Token: 0x04000799 RID: 1945
		private string m_processorArchitecture;

		// Token: 0x0400079A RID: 1946
		private string m_culture;

		// Token: 0x0400079B RID: 1947
		internal byte[] m_publicKeyToken;
	}
}
