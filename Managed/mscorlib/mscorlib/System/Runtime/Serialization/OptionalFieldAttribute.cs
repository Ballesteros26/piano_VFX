using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that a field can be missing from a serialization stream so that the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> and the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> does not throw an exception. </summary>
	// Token: 0x020006E5 RID: 1765
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		/// <summary>This property is unused and is reserved.</summary>
		/// <returns>This property is reserved.</returns>
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0010B0A7 File Offset: 0x001092A7
		// (set) Token: 0x06004A92 RID: 19090 RVA: 0x0010B0AF File Offset: 0x001092AF
		public int VersionAdded
		{
			get
			{
				return this.versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Version value must be positive."));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x040026FD RID: 9981
		private int versionAdded = 1;
	}
}
