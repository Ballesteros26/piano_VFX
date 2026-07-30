using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents information about an operating system, such as the version and platform identifier. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000228 RID: 552
	[ComVisible(true)]
	[Serializable]
	public sealed class OperatingSystem : ICloneable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OperatingSystem" /> class, using the specified platform identifier value and version object.</summary>
		/// <param name="platform">One of the <see cref="T:System.PlatformID" /> values that indicates the operating system platform. </param>
		/// <param name="version">A <see cref="T:System.Version" /> object that indicates the version of the operating system. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="platform" /> is not a <see cref="T:System.PlatformID" /> enumeration value.</exception>
		// Token: 0x06001A52 RID: 6738 RVA: 0x000636E0 File Offset: 0x000618E0
		public OperatingSystem(PlatformID platform, Version version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = version;
			if (platform == PlatformID.Win32NT && version.Revision != 0)
			{
				this._servicePack = "Service Pack " + (version.Revision >> 16);
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0006374C File Offset: 0x0006194C
		private OperatingSystem(SerializationInfo information, StreamingContext context)
		{
			this._platform = (PlatformID)information.GetValue("_platform", typeof(PlatformID));
			this._version = (Version)information.GetValue("_version", typeof(Version));
			this._servicePack = information.GetString("_servicePack");
		}

		/// <summary>Gets a <see cref="T:System.PlatformID" /> enumeration value that identifies the operating system platform.</summary>
		/// <returns>One of the <see cref="T:System.PlatformID" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000637BB File Offset: 0x000619BB
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		/// <summary>Gets a <see cref="T:System.Version" /> object that identifies the operating system.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that describes the major version, minor version, build, and revision numbers for the operating system.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x000637C3 File Offset: 0x000619C3
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>Gets the service pack version represented by this <see cref="T:System.OperatingSystem" /> object.</summary>
		/// <returns>The service pack version, if service packs are supported and at least one is installed; otherwise, an empty string (""). </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000637CB File Offset: 0x000619CB
		public string ServicePack
		{
			get
			{
				return this._servicePack;
			}
		}

		/// <summary>Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system. </summary>
		/// <returns>The string representation of the values returned by the <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" />, and <see cref="P:System.OperatingSystem.ServicePack" /> properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0003D958 File Offset: 0x0003BB58
		public string VersionString
		{
			get
			{
				return this.ToString();
			}
		}

		/// <summary>Creates an <see cref="T:System.OperatingSystem" /> object that is identical to this instance.</summary>
		/// <returns>An <see cref="T:System.OperatingSystem" /> object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A58 RID: 6744 RVA: 0x000637D3 File Offset: 0x000619D3
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize this instance.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A59 RID: 6745 RVA: 0x000637E6 File Offset: 0x000619E6
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_platform", this._platform);
			info.AddValue("_version", this._version);
			info.AddValue("_servicePack", this._servicePack);
		}

		/// <summary>Converts the value of this <see cref="T:System.OperatingSystem" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the values returned by the <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" />, and <see cref="P:System.OperatingSystem.ServicePack" /> properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A5A RID: 6746 RVA: 0x00063820 File Offset: 0x00061A20
		public override string ToString()
		{
			int platform = (int)this._platform;
			string text;
			switch (platform)
			{
			case 0:
				text = "Microsoft Win32S";
				goto IL_0076;
			case 1:
				text = "Microsoft Windows 98";
				goto IL_0076;
			case 2:
				text = "Microsoft Windows NT";
				goto IL_0076;
			case 3:
				text = "Microsoft Windows CE";
				goto IL_0076;
			case 4:
				break;
			case 5:
				text = "XBox";
				goto IL_0076;
			case 6:
				text = "OSX";
				goto IL_0076;
			default:
				if (platform != 128)
				{
					text = Locale.GetText("<unknown>");
					goto IL_0076;
				}
				break;
			}
			text = "Unix";
			IL_0076:
			string text2 = "";
			if (this.ServicePack != string.Empty)
			{
				text2 = " " + this.ServicePack;
			}
			return text + " " + this._version.ToString() + text2;
		}

		// Token: 0x04000D0F RID: 3343
		private PlatformID _platform;

		// Token: 0x04000D10 RID: 3344
		private Version _version;

		// Token: 0x04000D11 RID: 3345
		private string _servicePack = string.Empty;
	}
}
