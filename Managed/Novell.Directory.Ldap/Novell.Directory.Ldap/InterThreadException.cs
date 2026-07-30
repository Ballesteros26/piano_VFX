using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000009 RID: 9
	public class InterThreadException : LdapException
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003574 File Offset: 0x00001774
		internal virtual int MessageID
		{
			get
			{
				if (this.request == null)
				{
					return -1;
				}
				return this.request.MessageID;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000358C File Offset: 0x0000178C
		internal virtual int ReplyType
		{
			get
			{
				if (this.request == null)
				{
					return -1;
				}
				int messageType = this.request.MessageType;
				int num = -1;
				switch (messageType)
				{
				case 0:
					num = 1;
					break;
				case 1:
				case 4:
				case 5:
				case 7:
				case 9:
				case 11:
				case 13:
				case 15:
					break;
				case 2:
					num = -1;
					break;
				case 3:
					num = 5;
					break;
				case 6:
					num = 7;
					break;
				case 8:
					num = 9;
					break;
				case 10:
					num = 11;
					break;
				case 12:
					num = 13;
					break;
				case 14:
					num = 15;
					break;
				case 16:
					num = -1;
					break;
				default:
					if (messageType == 23)
					{
						num = 24;
					}
					break;
				}
				return num;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000362E File Offset: 0x0000182E
		internal InterThreadException(string message, object[] arguments, int resultCode, Exception rootException, Message request)
			: base(message, arguments, resultCode, null, rootException)
		{
			this.request = request;
		}

		// Token: 0x0400005B RID: 91
		private Message request;
	}
}
