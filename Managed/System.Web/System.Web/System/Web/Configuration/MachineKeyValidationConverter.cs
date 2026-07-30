using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	/// <summary>Provides methods for converting <see cref="T:System.Web.Configuration.MachineKeyValidation" /> objects to and from strings.</summary>
	// Token: 0x020005BA RID: 1466
	public sealed class MachineKeyValidationConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a string to the equivalent <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value.</summary>
		/// <returns>The equivalent <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value.</returns>
		/// <param name="ctx">This parameter is not used.</param>
		/// <param name="ci">This parameter is not used.</param>
		/// <param name="data">The string to convert.</param>
		/// <exception cref="T:System.ArgumentException">The data is not one of the expected strings.</exception>
		// Token: 0x06003EDF RID: 16095 RVA: 0x000A6868 File Offset: 0x000A4A68
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			string text = (string)data;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1416239282U)
			{
				if (num != 415037492U)
				{
					if (num != 957249328U)
					{
						if (num == 1416239282U)
						{
							if (text == "3DES")
							{
								return MachineKeyValidation.TripleDES;
							}
						}
					}
					else if (text == "HMACSHA512")
					{
						return MachineKeyValidation.HMACSHA512;
					}
				}
				else if (text == "SHA1")
				{
					return MachineKeyValidation.SHA1;
				}
			}
			else if (num <= 2012598173U)
			{
				if (num != 1935726387U)
				{
					if (num == 2012598173U)
					{
						if (text == "HMACSHA384")
						{
							return MachineKeyValidation.HMACSHA384;
						}
					}
				}
				else if (text == "MD5")
				{
					return MachineKeyValidation.MD5;
				}
			}
			else if (num != 2018892245U)
			{
				if (num == 2893537640U)
				{
					if (text == "AES")
					{
						return MachineKeyValidation.AES;
					}
				}
			}
			else if (text == "HMACSHA256")
			{
				return MachineKeyValidation.HMACSHA256;
			}
			throw new ArgumentException("The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512.");
		}

		/// <summary>Converts a <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value to the string representation of that value.</summary>
		/// <returns>A string representing a <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value.</returns>
		/// <param name="ctx">This parameter is not used.</param>
		/// <param name="ci">This parameter is not used.</param>
		/// <param name="value">The <see cref="T:System.Web.Configuration.MachineKeyValidation" /> to be converted.</param>
		/// <param name="type">This parameter is not used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="value" /> parameter is not one of the expected enumerated values.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Web.Configuration.MachineKeyValidation" /> object.</exception>
		// Token: 0x06003EE0 RID: 16096 RVA: 0x000A6984 File Offset: 0x000A4B84
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null || value.GetType() != typeof(MachineKeyValidation))
			{
				throw new ArgumentException("The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512.");
			}
			switch ((MachineKeyValidation)value)
			{
			case MachineKeyValidation.MD5:
				return "MD5";
			case MachineKeyValidation.SHA1:
				return "SHA1";
			case MachineKeyValidation.TripleDES:
				return "3DES";
			case MachineKeyValidation.AES:
				return "AES";
			case MachineKeyValidation.HMACSHA256:
				return "HMACSHA256";
			case MachineKeyValidation.HMACSHA384:
				return "HMACSHA384";
			case MachineKeyValidation.HMACSHA512:
				return "HMACSHA512";
			default:
				throw new ArgumentException("The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512.");
			}
		}

		// Token: 0x04002254 RID: 8788
		private const string InvalidValue = "The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512.";
	}
}
