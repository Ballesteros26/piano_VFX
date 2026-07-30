using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x0200009C RID: 156
	internal class DnsResponse : DnsPacket
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000ADA5 File Offset: 0x00008FA5
		public DnsResponse(byte[] buffer, int length)
			: base(buffer, length)
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public void Reset()
		{
			this.question = null;
			this.answer = null;
			this.authority = null;
			this.additional = null;
			for (int i = 0; i < this.packet.Length; i++)
			{
				this.packet[i] = 0;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000AE00 File Offset: 0x00009000
		private ReadOnlyCollection<DnsResourceRecord> GetRRs(int count)
		{
			if (count <= 0)
			{
				return DnsResponse.EmptyRR;
			}
			List<DnsResourceRecord> list = new List<DnsResourceRecord>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(DnsResourceRecord.CreateFromBuffer(this, this.position, ref this.offset));
			}
			return list.AsReadOnly();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000AE48 File Offset: 0x00009048
		private ReadOnlyCollection<DnsQuestion> GetQuestions(int count)
		{
			if (count <= 0)
			{
				return DnsResponse.EmptyQS;
			}
			List<DnsQuestion> list = new List<DnsQuestion>(count);
			for (int i = 0; i < count; i++)
			{
				DnsQuestion dnsQuestion = new DnsQuestion();
				this.offset = dnsQuestion.Init(this, this.offset);
				list.Add(dnsQuestion);
			}
			return list.AsReadOnly();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000AE98 File Offset: 0x00009098
		public ReadOnlyCollection<DnsQuestion> GetQuestions()
		{
			if (this.question == null)
			{
				this.question = this.GetQuestions((int)base.Header.QuestionCount);
			}
			return this.question;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AEBF File Offset: 0x000090BF
		public ReadOnlyCollection<DnsResourceRecord> GetAnswers()
		{
			if (this.answer == null)
			{
				this.GetQuestions();
				this.answer = this.GetRRs((int)base.Header.AnswerCount);
			}
			return this.answer;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000AEED File Offset: 0x000090ED
		public ReadOnlyCollection<DnsResourceRecord> GetAuthority()
		{
			if (this.authority == null)
			{
				this.GetQuestions();
				this.GetAnswers();
				this.authority = this.GetRRs((int)base.Header.AuthorityCount);
			}
			return this.authority;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000AF22 File Offset: 0x00009122
		public ReadOnlyCollection<DnsResourceRecord> GetAdditional()
		{
			if (this.additional == null)
			{
				this.GetQuestions();
				this.GetAnswers();
				this.GetAuthority();
				this.additional = this.GetRRs((int)base.Header.AdditionalCount);
			}
			return this.additional;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000AF60 File Offset: 0x00009160
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Header);
			stringBuilder.Append("Question:\r\n");
			foreach (DnsQuestion dnsQuestion in this.GetQuestions())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsQuestion);
			}
			stringBuilder.Append("Answer(s):\r\n");
			foreach (DnsResourceRecord dnsResourceRecord in this.GetAnswers())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsResourceRecord);
			}
			stringBuilder.Append("Authority:\r\n");
			foreach (DnsResourceRecord dnsResourceRecord2 in this.GetAuthority())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsResourceRecord2);
			}
			stringBuilder.Append("Additional:\r\n");
			foreach (DnsResourceRecord dnsResourceRecord3 in this.GetAdditional())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsResourceRecord3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040008A6 RID: 2214
		private static readonly ReadOnlyCollection<DnsResourceRecord> EmptyRR = new ReadOnlyCollection<DnsResourceRecord>(new DnsResourceRecord[0]);

		// Token: 0x040008A7 RID: 2215
		private static readonly ReadOnlyCollection<DnsQuestion> EmptyQS = new ReadOnlyCollection<DnsQuestion>(new DnsQuestion[0]);

		// Token: 0x040008A8 RID: 2216
		private ReadOnlyCollection<DnsQuestion> question;

		// Token: 0x040008A9 RID: 2217
		private ReadOnlyCollection<DnsResourceRecord> answer;

		// Token: 0x040008AA RID: 2218
		private ReadOnlyCollection<DnsResourceRecord> authority;

		// Token: 0x040008AB RID: 2219
		private ReadOnlyCollection<DnsResourceRecord> additional;

		// Token: 0x040008AC RID: 2220
		private int offset = 12;
	}
}
