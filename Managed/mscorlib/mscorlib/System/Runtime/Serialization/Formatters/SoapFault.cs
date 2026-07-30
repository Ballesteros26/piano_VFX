using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Carries error and status information within a SOAP message. This class cannot be inherited.</summary>
	// Token: 0x02000701 RID: 1793
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> class with default values.</summary>
		// Token: 0x06004B35 RID: 19253 RVA: 0x00002111 File Offset: 0x00000311
		public SoapFault()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> class, setting the properties to specified values.</summary>
		/// <param name="faultCode">The fault code for the new instance of <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />. The fault code identifies the type of the fault that occurred. </param>
		/// <param name="faultString">The fault string for the new instance of <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />. The fault string provides a human readable explanation of the fault. </param>
		/// <param name="faultActor">The URI of the object that generated the fault. </param>
		/// <param name="serverFault">The description of a common language runtime exception. This information is also present in the <see cref="P:System.Runtime.Serialization.Formatters.SoapFault.Detail" /> property. </param>
		// Token: 0x06004B36 RID: 19254 RVA: 0x0010C846 File Offset: 0x0010AA46
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultActor = faultActor;
			this.detail = serverFault;
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x0010C86C File Offset: 0x0010AA6C
		internal SoapFault(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				object value = enumerator.Value;
				if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
				{
					int num = ((string)value).IndexOf(':');
					if (num > -1)
					{
						this.faultCode = ((string)value).Substring(num + 1);
					}
					else
					{
						this.faultCode = (string)value;
					}
				}
				else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultString = (string)value;
				}
				else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultActor = (string)value;
				}
				else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
				{
					this.detail = value;
				}
			}
		}

		/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data to serialize the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for the current serialization. </param>
		// Token: 0x06004B38 RID: 19256 RVA: 0x0010C94C File Offset: 0x0010AB4C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", "SOAP-ENV:" + this.faultCode);
			info.AddValue("faultstring", this.faultString);
			if (this.faultActor != null)
			{
				info.AddValue("faultactor", this.faultActor);
			}
			info.AddValue("detail", this.detail, typeof(object));
		}

		/// <summary>Gets or sets the fault code for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault code for this <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x0010C9B9 File Offset: 0x0010ABB9
		// (set) Token: 0x06004B3A RID: 19258 RVA: 0x0010C9C1 File Offset: 0x0010ABC1
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		/// <summary>Gets or sets the fault message for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault message for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x0010C9CA File Offset: 0x0010ABCA
		// (set) Token: 0x06004B3C RID: 19260 RVA: 0x0010C9D2 File Offset: 0x0010ABD2
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		/// <summary>Gets or sets the fault actor for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault actor for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x0010C9DB File Offset: 0x0010ABDB
		// (set) Token: 0x06004B3E RID: 19262 RVA: 0x0010C9E3 File Offset: 0x0010ABE3
		public string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		/// <summary>Gets or sets additional information required for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>Additional information required for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004B3F RID: 19263 RVA: 0x0010C9EC File Offset: 0x0010ABEC
		// (set) Token: 0x06004B40 RID: 19264 RVA: 0x0010C9F4 File Offset: 0x0010ABF4
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		// Token: 0x04002740 RID: 10048
		private string faultCode;

		// Token: 0x04002741 RID: 10049
		private string faultString;

		// Token: 0x04002742 RID: 10050
		private string faultActor;

		// Token: 0x04002743 RID: 10051
		[SoapField(Embedded = true)]
		private object detail;
	}
}
