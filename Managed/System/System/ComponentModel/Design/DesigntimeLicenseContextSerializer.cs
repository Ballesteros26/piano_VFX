using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for design-time license context serialization.</summary>
	// Token: 0x0200031D RID: 797
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesigntimeLicenseContextSerializer
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x000020EB File Offset: 0x000002EB
		private DesigntimeLicenseContextSerializer()
		{
		}

		/// <summary>Serializes the licenses within the specified design-time license context using the specified key and output stream.</summary>
		/// <param name="o">The stream to output to. </param>
		/// <param name="cryptoKey">The key to use for encryption. </param>
		/// <param name="context">A <see cref="T:System.ComponentModel.Design.DesigntimeLicenseContext" /> indicating the license context. </param>
		// Token: 0x06001967 RID: 6503 RVA: 0x0006A20A File Offset: 0x0006840A
		public static void Serialize(Stream o, string cryptoKey, DesigntimeLicenseContext context)
		{
			((IFormatter)new BinaryFormatter()).Serialize(o, new object[] { cryptoKey, context.savedLicenseKeys });
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006A22C File Offset: 0x0006842C
		internal static void Deserialize(Stream o, string cryptoKey, RuntimeLicenseContext context)
		{
			object obj = ((IFormatter)new BinaryFormatter()).Deserialize(o);
			if (obj is object[])
			{
				object[] array = (object[])obj;
				if (array[0] is string && (string)array[0] == cryptoKey)
				{
					context.savedLicenseKeys = (Hashtable)array[1];
				}
			}
		}
	}
}
