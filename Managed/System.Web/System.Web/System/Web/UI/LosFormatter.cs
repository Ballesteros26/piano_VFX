using System;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Serializes the view state for a Web Forms page. This class cannot be inherited.</summary>
	// Token: 0x020001E6 RID: 486
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class LosFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using default values.</summary>
		// Token: 0x060013A6 RID: 5030 RVA: 0x0003540D File Offset: 0x0003360D
		public LosFormatter()
		{
			this.osf = new ObjectStateFormatter();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using the specified enable flag and machine authentication code (MAC) key modifier.</summary>
		/// <param name="enableMac">true to enable view-state MAC; otherwise, false. </param>
		/// <param name="macKeyModifier">The modifier for the MAC key. </param>
		// Token: 0x060013A7 RID: 5031 RVA: 0x00035420 File Offset: 0x00033620
		public LosFormatter(bool enableMac, string macKeyModifier)
			: this(enableMac, string.IsNullOrEmpty(macKeyModifier) ? null : Encoding.ASCII.GetBytes(macKeyModifier))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using the specified enable flag and machine authentication code (MAC) key modifier.</summary>
		/// <param name="enableMac">true to enable view-state MAC; otherwise, false.</param>
		/// <param name="macKeyModifier">The modifier for the MAC key.</param>
		// Token: 0x060013A8 RID: 5032 RVA: 0x0003543F File Offset: 0x0003363F
		public LosFormatter(bool enableMac, byte[] macKeyModifier)
		{
			this.osf = new ObjectStateFormatter();
			if (enableMac && macKeyModifier != null)
			{
				this.SetMacKey(macKeyModifier);
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00035460 File Offset: 0x00033660
		private void SetMacKey(byte[] macKeyModifier)
		{
			try
			{
				this.osf.Section.ValidationKey = MachineKeySectionUtils.GetHexString(macKeyModifier);
			}
			catch (ArgumentException)
			{
			}
			catch (ConfigurationErrorsException)
			{
			}
		}

		/// <summary>Transforms the view-state value contained in a <see cref="T:System.IO.Stream" /> object to a limited object serialization (LOS)-formatted object.</summary>
		/// <returns>A LOS-formatted object.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the view-state value to transform. </param>
		// Token: 0x060013AA RID: 5034 RVA: 0x000354A8 File Offset: 0x000336A8
		public object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			object obj;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				obj = this.Deserialize(streamReader.ReadToEnd());
			}
			return obj;
		}

		/// <summary>Transforms the view-state value contained in a <see cref="T:System.IO.TextReader" /> object to a limited object serialization (LOS)-formatted object.</summary>
		/// <returns>A LOS-formatted object.</returns>
		/// <param name="input">A <see cref="T:System.IO.TextReader" /> that contains the view-state value to transform. </param>
		// Token: 0x060013AB RID: 5035 RVA: 0x000354F4 File Offset: 0x000336F4
		public object Deserialize(TextReader input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Deserialize(input.ReadToEnd());
		}

		/// <summary>Transforms the specified view-state value to a limited object serialization (LOS)-formatted object.</summary>
		/// <returns>A LOS-formatted object.</returns>
		/// <param name="input">The view-state value to transform. </param>
		/// <exception cref="T:System.Web.HttpException">The view state is invalid. </exception>
		// Token: 0x060013AC RID: 5036 RVA: 0x00035510 File Offset: 0x00033710
		public object Deserialize(string input)
		{
			if (input == null)
			{
				return null;
			}
			return this.osf.Deserialize(input);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00035523 File Offset: 0x00033723
		internal string SerializeToBase64(object value)
		{
			return this.osf.Serialize(value);
		}

		/// <summary>Transforms a limited object serialization (LOS)-formatted object into a view-state value and places the results into a <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the transformed value. </param>
		/// <param name="value">The LOS-formatted object to transform into a view-state value. </param>
		// Token: 0x060013AE RID: 5038 RVA: 0x00035534 File Offset: 0x00033734
		public void Serialize(Stream stream, object value)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new NotSupportedException();
			}
			string text = this.SerializeToBase64(value);
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Transforms a limited object serialization (LOS)-formatted object into a view-state value and places the results into a <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to receive the transformed value. </param>
		/// <param name="value">The LOS-formatted object to transform into a view-state value. </param>
		// Token: 0x060013AF RID: 5039 RVA: 0x0003557C File Offset: 0x0003377C
		public void Serialize(TextWriter output, object value)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write(this.SerializeToBase64(value));
		}

		// Token: 0x04001475 RID: 5237
		private ObjectStateFormatter osf;
	}
}
