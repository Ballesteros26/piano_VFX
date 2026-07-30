using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000537 RID: 1335
	[Serializable]
	internal class HostingEnvironmentException : Exception
	{
		// Token: 0x06003A66 RID: 14950 RVA: 0x0009DA8A File Offset: 0x0009BC8A
		protected HostingEnvironmentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._details = info.GetString("_details");
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x0009DAA5 File Offset: 0x0009BCA5
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_details", this._details);
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x0009DAC0 File Offset: 0x0009BCC0
		internal HostingEnvironmentException(string message, string details)
			: base(message)
		{
			this._details = details;
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x0009DAD0 File Offset: 0x0009BCD0
		internal string Details
		{
			get
			{
				if (this._details == null)
				{
					return string.Empty;
				}
				return this._details;
			}
		}

		// Token: 0x04001FD1 RID: 8145
		private string _details;
	}
}
