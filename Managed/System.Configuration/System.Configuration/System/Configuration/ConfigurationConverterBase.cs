using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>The base class for the configuration converter types.</summary>
	// Token: 0x02000018 RID: 24
	public abstract class ConfigurationConverterBase : TypeConverter
	{
		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <returns>true if the conversion is allowed; otherwise, false.</returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to convert from.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x000035B7 File Offset: 0x000017B7
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string) || base.CanConvertFrom(ctx, type);
		}

		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <returns>true if the conversion is allowed; otherwise, false. </returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversion.</param>
		/// <param name="type">The type to convert to.</param>
		// Token: 0x06000094 RID: 148 RVA: 0x000035D5 File Offset: 0x000017D5
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string) || base.CanConvertTo(ctx, type);
		}
	}
}
