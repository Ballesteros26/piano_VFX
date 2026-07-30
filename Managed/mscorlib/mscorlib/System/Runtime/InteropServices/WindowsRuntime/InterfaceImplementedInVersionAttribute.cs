using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the version of the target type that first implemented the specified interface.</summary>
	// Token: 0x0200095A RID: 2394
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
	public sealed class InterfaceImplementedInVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute" /> class, specifying the interface that the target type implements and the version in which that interface was first implemented. </summary>
		/// <param name="interfaceType">The interface that was first implemented in the specified version of the target type. </param>
		/// <param name="majorVersion">The major component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="minorVersion">The minor component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="buildVersion">The build component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		/// <param name="revisionVersion">The revision component of the version of the target type that first implemented <paramref name="interfaceType" />.</param>
		// Token: 0x0600592A RID: 22826 RVA: 0x0012AB66 File Offset: 0x00128D66
		public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
		{
			this.m_interfaceType = interfaceType;
			this.m_majorVersion = majorVersion;
			this.m_minorVersion = minorVersion;
			this.m_buildVersion = buildVersion;
			this.m_revisionVersion = revisionVersion;
		}

		/// <summary>Gets the type of the interface that the target type implements. </summary>
		/// <returns>The type of the interface. </returns>
		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x0600592B RID: 22827 RVA: 0x0012AB93 File Offset: 0x00128D93
		public Type InterfaceType
		{
			get
			{
				return this.m_interfaceType;
			}
		}

		/// <summary>Gets the major component of the version of the target type that first implemented the interface. </summary>
		/// <returns>The major component of the version.</returns>
		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x0012AB9B File Offset: 0x00128D9B
		public byte MajorVersion
		{
			get
			{
				return this.m_majorVersion;
			}
		}

		/// <summary>Gets the minor component of the version of the target type that first implemented the interface. </summary>
		/// <returns>The minor component of the version. </returns>
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x0600592D RID: 22829 RVA: 0x0012ABA3 File Offset: 0x00128DA3
		public byte MinorVersion
		{
			get
			{
				return this.m_minorVersion;
			}
		}

		/// <summary>Gets the build component of the version of the target type that first implemented the interface. </summary>
		/// <returns>The build component of the version.</returns>
		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x0600592E RID: 22830 RVA: 0x0012ABAB File Offset: 0x00128DAB
		public byte BuildVersion
		{
			get
			{
				return this.m_buildVersion;
			}
		}

		/// <summary>Gets the revision component of the version of the target type that first implemented the interface. </summary>
		/// <returns>The revision component of the version.</returns>
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x0600592F RID: 22831 RVA: 0x0012ABB3 File Offset: 0x00128DB3
		public byte RevisionVersion
		{
			get
			{
				return this.m_revisionVersion;
			}
		}

		// Token: 0x04002E04 RID: 11780
		private Type m_interfaceType;

		// Token: 0x04002E05 RID: 11781
		private byte m_majorVersion;

		// Token: 0x04002E06 RID: 11782
		private byte m_minorVersion;

		// Token: 0x04002E07 RID: 11783
		private byte m_buildVersion;

		// Token: 0x04002E08 RID: 11784
		private byte m_revisionVersion;
	}
}
