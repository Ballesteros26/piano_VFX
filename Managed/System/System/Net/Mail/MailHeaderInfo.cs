using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x02000573 RID: 1395
	internal static class MailHeaderInfo
	{
		// Token: 0x06002B4E RID: 11086 RVA: 0x000A994C File Offset: 0x000A7B4C
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.m_HeaderInfo.Length; i++)
			{
				MailHeaderInfo.m_HeaderDictionary.Add(MailHeaderInfo.m_HeaderInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000A9C88 File Offset: 0x000A7E88
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.m_HeaderInfo[(int)id].NormalizedName;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000A9CA8 File Offset: 0x000A7EA8
		internal static MailHeaderID GetID(string name)
		{
			int num;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num))
			{
				return (MailHeaderID)num;
			}
			return MailHeaderID.Unknown;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000A9CC8 File Offset: 0x000A7EC8
		internal static bool IsWellKnown(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000A9CE4 File Offset: 0x000A7EE4
		internal static bool IsUserSettable(string name)
		{
			int num;
			return !MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) || MailHeaderInfo.m_HeaderInfo[num].IsUserSettable;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000A9D14 File Offset: 0x000A7F14
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && MailHeaderInfo.m_HeaderInfo[num].IsSingleton;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000A9D44 File Offset: 0x000A7F44
		internal static string NormalizeCase(string name)
		{
			int num;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num))
			{
				return MailHeaderInfo.m_HeaderInfo[num].NormalizedName;
			}
			return name;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000A9D74 File Offset: 0x000A7F74
		internal static bool IsMatch(string name, MailHeaderID header)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && num == (int)header;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000A9D98 File Offset: 0x000A7F98
		internal static bool AllowsUnicode(string name)
		{
			int num;
			return !MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) || MailHeaderInfo.m_HeaderInfo[num].AllowsUnicode;
		}

		// Token: 0x04002450 RID: 9296
		private static readonly MailHeaderInfo.HeaderInfo[] m_HeaderInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true, true, true)
		};

		// Token: 0x04002451 RID: 9297
		private static readonly Dictionary<string, int> m_HeaderDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000574 RID: 1396
		private struct HeaderInfo
		{
			// Token: 0x06002B57 RID: 11095 RVA: 0x000A9DC6 File Offset: 0x000A7FC6
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable, bool allowsUnicode)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
				this.IsUserSettable = isUserSettable;
				this.AllowsUnicode = allowsUnicode;
			}

			// Token: 0x04002452 RID: 9298
			public readonly string NormalizedName;

			// Token: 0x04002453 RID: 9299
			public readonly bool IsSingleton;

			// Token: 0x04002454 RID: 9300
			public readonly MailHeaderID ID;

			// Token: 0x04002455 RID: 9301
			public readonly bool IsUserSettable;

			// Token: 0x04002456 RID: 9302
			public readonly bool AllowsUnicode;
		}
	}
}
