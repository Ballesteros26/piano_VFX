using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Web.Services.Protocols
{
	/// <summary>Provides URL encoding functionality for writers of out-going request parameters for Web service clients implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000085 RID: 133
	public abstract class UrlEncodedParameterWriter : MimeParameterWriter
	{
		/// <summary>Gets or sets the encoding used to write parameters to the HTTP request.</summary>
		/// <returns>The encoding used to write parameters to the HTTP request.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00010E7C File Offset: 0x0000F07C
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00010E84 File Offset: 0x0000F084
		public override Encoding RequestEncoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Returns an initializer for the specified method.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the initializer for the specified method.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
		// Token: 0x06000391 RID: 913 RVA: 0x00010E8D File Offset: 0x0000F08D
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			if (!ValueCollectionParameterReader.IsSupported(methodInfo))
			{
				return null;
			}
			return methodInfo.InParameters;
		}

		/// <summary>Initializes an instance.</summary>
		/// <param name="initializer">A <see cref="T:System.Reflection.ParameterInfo" /> array obtained through the <see cref="P:System.Web.Services.Protocols.LogicalMethodInfo.InParameters" /> property of the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> class.</param>
		// Token: 0x06000392 RID: 914 RVA: 0x00010E9F File Offset: 0x0000F09F
		public override void Initialize(object initializer)
		{
			this.paramInfos = (ParameterInfo[])initializer;
		}

		/// <summary>Encodes all the parameter values for a Web method and writes them to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object that does the writing to the HTTP request.</param>
		/// <param name="values">The Web method parameter values.</param>
		// Token: 0x06000393 RID: 915 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		protected void Encode(TextWriter writer, object[] values)
		{
			this.numberEncoded = 0;
			for (int i = 0; i < this.paramInfos.Length; i++)
			{
				ParameterInfo parameterInfo = this.paramInfos[i];
				if (parameterInfo.ParameterType.IsArray)
				{
					Array array = (Array)values[i];
					for (int j = 0; j < array.Length; j++)
					{
						this.Encode(writer, parameterInfo.Name, array.GetValue(j));
					}
				}
				else
				{
					this.Encode(writer, parameterInfo.Name, values[i]);
				}
			}
		}

		/// <summary>Encodes a specified parameter value and writes it to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object that does the writing to the HTTP request.</param>
		/// <param name="name">The name of the parameter that will be encoded.</param>
		/// <param name="value">The value of the parameter that will be encoded.</param>
		// Token: 0x06000394 RID: 916 RVA: 0x00010F30 File Offset: 0x0000F130
		protected void Encode(TextWriter writer, string name, object value)
		{
			if (this.numberEncoded > 0)
			{
				writer.Write('&');
			}
			writer.Write(this.UrlEncode(name));
			writer.Write('=');
			writer.Write(this.UrlEncode(ScalarFormatter.ToString(value)));
			this.numberEncoded++;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00010F83 File Offset: 0x0000F183
		private string UrlEncode(string value)
		{
			if (this.encoding != null)
			{
				return UrlEncoder.UrlEscapeString(value, this.encoding);
			}
			return UrlEncoder.UrlEscapeStringUnicode(value);
		}

		// Token: 0x04000302 RID: 770
		private ParameterInfo[] paramInfos;

		// Token: 0x04000303 RID: 771
		private int numberEncoded;

		// Token: 0x04000304 RID: 772
		private Encoding encoding;
	}
}
