using System;
using System.Collections;
using System.Reflection;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000048 RID: 72
	internal class MatchType
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006C60 File Offset: 0x00004E60
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006C68 File Offset: 0x00004E68
		internal static MatchType Reflect(Type type)
		{
			MatchType matchType = new MatchType();
			matchType.type = type;
			MemberInfo[] members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < members.Length; i++)
			{
				MatchMember matchMember = MatchMember.Reflect(members[i]);
				if (matchMember != null)
				{
					arrayList.Add(matchMember);
				}
			}
			matchType.fields = (MatchMember[])arrayList.ToArray(typeof(MatchMember));
			return matchType;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006CD4 File Offset: 0x00004ED4
		internal object Match(string text)
		{
			object obj = Activator.CreateInstance(this.type);
			for (int i = 0; i < this.fields.Length; i++)
			{
				this.fields[i].Match(obj, text);
			}
			return obj;
		}

		// Token: 0x04000217 RID: 535
		private Type type;

		// Token: 0x04000218 RID: 536
		private MatchMember[] fields;
	}
}
