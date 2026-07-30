using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000AC RID: 172
	public class SearchResultEventArgs : LdapEventArgs
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00014531 File Offset: 0x00012731
		public SearchResultEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, aType)
		{
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001453C File Offset: 0x0001273C
		public LdapEntry Entry
		{
			get
			{
				return ((LdapSearchResult)this.ldap_message).Entry;
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014550 File Offset: 0x00012750
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.getChangeTypeString());
			stringBuilder.AppendFormat("(EventInformation:{0})", this.getStringRepresentaionOfEventInformation());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000145C0 File Offset: 0x000127C0
		private string getStringRepresentaionOfEventInformation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			LdapSearchResult ldapSearchResult = (LdapSearchResult)this.ldap_message;
			stringBuilder.AppendFormat("(Entry={0})", ldapSearchResult.Entry);
			LdapControl[] controls = ldapSearchResult.Controls;
			if (controls != null)
			{
				stringBuilder.Append("(Controls=");
				int num = 0;
				foreach (LdapControl ldapControl in controls)
				{
					stringBuilder.AppendFormat("(Control{0}={1})", ++num, ldapControl.ToString());
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014658 File Offset: 0x00012858
		private string getChangeTypeString()
		{
			LdapEventType eType = this.eType;
			switch (eType)
			{
			case LdapEventType.LDAP_PSEARCH_ADD:
				return "ADD";
			case LdapEventType.LDAP_PSEARCH_DELETE:
				return "DELETE";
			case (LdapEventType)3:
				break;
			case LdapEventType.LDAP_PSEARCH_MODIFY:
				return "MODIFY";
			default:
				if (eType == LdapEventType.LDAP_PSEARCH_MODDN)
				{
					return "MODDN";
				}
				break;
			}
			return "No change type: " + this.eType;
		}
	}
}
