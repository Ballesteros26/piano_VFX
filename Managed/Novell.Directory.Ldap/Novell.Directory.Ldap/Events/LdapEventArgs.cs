using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A6 RID: 166
	public class LdapEventArgs : DirectoryEventArgs
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00013FB9 File Offset: 0x000121B9
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00013FC1 File Offset: 0x000121C1
		public LdapEventType EventType
		{
			get
			{
				return this.eType;
			}
			set
			{
				this.eType = value;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013FCA File Offset: 0x000121CA
		public LdapEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, aClassification)
		{
			this.eType = aType;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013FDC File Offset: 0x000121DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.AppendFormat("{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.eType);
			stringBuilder.AppendFormat("(EventInformation:{0})", this.ldap_message);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000305 RID: 773
		protected LdapEventType eType;
	}
}
