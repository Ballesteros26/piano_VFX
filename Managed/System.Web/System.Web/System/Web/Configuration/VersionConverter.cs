using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x020005EB RID: 1515
	internal sealed class VersionConverter : ConfigurationConverterBase
	{
		// Token: 0x060041B5 RID: 16821 RVA: 0x000A6860 File Offset: 0x000A4A60
		public VersionConverter()
		{
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000ABC9F File Offset: 0x000A9E9F
		public VersionConverter(int minMajor, int minMinor, string exceptionText = null)
		{
			this.minVersion = new Version(minMajor, minMinor);
			this.exceptionText = exceptionText;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000ABCBC File Offset: 0x000A9EBC
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			string text = data as string;
			if (string.IsNullOrEmpty(text))
			{
				throw new ConfigurationErrorsException("The input string is too short or null.");
			}
			Version version;
			if (!Version.TryParse(text, out version))
			{
				throw new ConfigurationErrorsException("The input string wasn't in correct format.");
			}
			if (this.minVersion != null && version < this.minVersion)
			{
				throw new ConfigurationErrorsException(string.Format(this.exceptionText, version, this.minVersion));
			}
			return version;
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000ABD2C File Offset: 0x000A9F2C
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			Version version = value as Version;
			if (version == null)
			{
				throw new ArgumentException("Is not an instance of the Version type", "value");
			}
			if (type == typeof(string))
			{
				return version.ToString();
			}
			if (type == typeof(Version))
			{
				return version.Clone();
			}
			throw new ConfigurationErrorsException("Conversion to type '" + type + "' is not supported.");
		}

		// Token: 0x04002345 RID: 9029
		private Version minVersion;

		// Token: 0x04002346 RID: 9030
		private string exceptionText;
	}
}
