using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000049 RID: 73
	internal class MatchMember
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00006D10 File Offset: 0x00004F10
		internal void Match(object target, string text)
		{
			if (this.memberInfo is FieldInfo)
			{
				((FieldInfo)this.memberInfo).SetValue(target, (this.matchType == null) ? this.MatchString(text) : this.MatchClass(text));
				return;
			}
			if (this.memberInfo is PropertyInfo)
			{
				((PropertyInfo)this.memberInfo).SetValue(target, (this.matchType == null) ? this.MatchString(text) : this.MatchClass(text), new object[0]);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006D90 File Offset: 0x00004F90
		private object MatchString(string text)
		{
			Match match = this.regex.Match(text);
			if (((this.memberInfo is FieldInfo) ? ((FieldInfo)this.memberInfo).FieldType : ((PropertyInfo)this.memberInfo).PropertyType).IsArray)
			{
				ArrayList arrayList = new ArrayList();
				int num = 0;
				while (match.Success && num < this.maxRepeats)
				{
					if (match.Groups.Count <= this.group)
					{
						throw MatchMember.BadGroupIndexException(this.group, this.memberInfo.Name, match.Groups.Count - 1);
					}
					foreach (object obj in match.Groups[this.group].Captures)
					{
						Capture capture = (Capture)obj;
						arrayList.Add(text.Substring(capture.Index, capture.Length));
					}
					match = match.NextMatch();
					num++;
				}
				return arrayList.ToArray(typeof(string));
			}
			if (match.Success)
			{
				if (match.Groups.Count <= this.group)
				{
					throw MatchMember.BadGroupIndexException(this.group, this.memberInfo.Name, match.Groups.Count - 1);
				}
				Group group = match.Groups[this.group];
				if (group.Captures.Count > 0)
				{
					if (group.Captures.Count <= this.capture)
					{
						throw MatchMember.BadCaptureIndexException(this.capture, this.memberInfo.Name, group.Captures.Count - 1);
					}
					Capture capture2 = group.Captures[this.capture];
					return text.Substring(capture2.Index, capture2.Length);
				}
			}
			return null;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006F94 File Offset: 0x00005194
		private object MatchClass(string text)
		{
			Match match = this.regex.Match(text);
			if (((this.memberInfo is FieldInfo) ? ((FieldInfo)this.memberInfo).FieldType : ((PropertyInfo)this.memberInfo).PropertyType).IsArray)
			{
				ArrayList arrayList = new ArrayList();
				int num = 0;
				while (match.Success && num < this.maxRepeats)
				{
					if (match.Groups.Count <= this.group)
					{
						throw MatchMember.BadGroupIndexException(this.group, this.memberInfo.Name, match.Groups.Count - 1);
					}
					foreach (object obj in match.Groups[this.group].Captures)
					{
						Capture capture = (Capture)obj;
						arrayList.Add(this.matchType.Match(text.Substring(capture.Index, capture.Length)));
					}
					match = match.NextMatch();
					num++;
				}
				return arrayList.ToArray(this.matchType.Type);
			}
			if (match.Success)
			{
				if (match.Groups.Count <= this.group)
				{
					throw MatchMember.BadGroupIndexException(this.group, this.memberInfo.Name, match.Groups.Count - 1);
				}
				Group group = match.Groups[this.group];
				if (group.Captures.Count > 0)
				{
					if (group.Captures.Count <= this.capture)
					{
						throw MatchMember.BadCaptureIndexException(this.capture, this.memberInfo.Name, group.Captures.Count - 1);
					}
					Capture capture2 = group.Captures[this.capture];
					return this.matchType.Match(text.Substring(capture2.Index, capture2.Length));
				}
			}
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000071B0 File Offset: 0x000053B0
		private static Exception BadCaptureIndexException(int index, string matchName, int highestIndex)
		{
			return new Exception(Res.GetString("WebTextMatchBadCaptureIndex", new object[] { index, matchName, highestIndex }));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000071DD File Offset: 0x000053DD
		private static Exception BadGroupIndexException(int index, string matchName, int highestIndex)
		{
			return new Exception(Res.GetString("WebTextMatchBadGroupIndex", new object[] { index, matchName, highestIndex }));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000720C File Offset: 0x0000540C
		internal static MatchMember Reflect(MemberInfo memberInfo)
		{
			Type type = null;
			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				if (!propertyInfo.CanRead)
				{
					return null;
				}
				if (!propertyInfo.CanWrite)
				{
					return null;
				}
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				if (getMethod.IsStatic)
				{
					return null;
				}
				if (getMethod.GetParameters().Length != 0)
				{
					return null;
				}
				type = propertyInfo.PropertyType;
			}
			if (memberInfo is FieldInfo)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				if (!fieldInfo.IsPublic)
				{
					return null;
				}
				if (fieldInfo.IsStatic)
				{
					return null;
				}
				if (fieldInfo.IsSpecialName)
				{
					return null;
				}
				type = fieldInfo.FieldType;
			}
			object[] customAttributes = memberInfo.GetCustomAttributes(typeof(MatchAttribute), false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			MatchAttribute matchAttribute = (MatchAttribute)customAttributes[0];
			MatchMember matchMember = new MatchMember();
			matchMember.regex = new Regex(matchAttribute.Pattern, RegexOptions.Singleline | (matchAttribute.IgnoreCase ? (RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) : RegexOptions.None));
			matchMember.group = matchAttribute.Group;
			matchMember.capture = matchAttribute.Capture;
			matchMember.maxRepeats = matchAttribute.MaxRepeats;
			matchMember.memberInfo = memberInfo;
			if (matchMember.maxRepeats < 0)
			{
				matchMember.maxRepeats = (type.IsArray ? int.MaxValue : 1);
			}
			if (type.IsArray)
			{
				type = type.GetElementType();
			}
			if (type != typeof(string))
			{
				matchMember.matchType = MatchType.Reflect(type);
			}
			return matchMember;
		}

		// Token: 0x04000219 RID: 537
		private MemberInfo memberInfo;

		// Token: 0x0400021A RID: 538
		private Regex regex;

		// Token: 0x0400021B RID: 539
		private int group;

		// Token: 0x0400021C RID: 540
		private int capture;

		// Token: 0x0400021D RID: 541
		private int maxRepeats;

		// Token: 0x0400021E RID: 542
		private MatchType matchType;
	}
}
