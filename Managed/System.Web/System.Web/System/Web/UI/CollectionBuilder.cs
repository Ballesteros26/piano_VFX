using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x020001B1 RID: 433
	internal sealed class CollectionBuilder : ControlBuilder
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0002B246 File Offset: 0x00029446
		internal CollectionBuilder()
		{
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0002E130 File Offset: 0x0002C330
		public override void AppendLiteralString(string s)
		{
			if (s != null && s.Trim().Length > 0)
			{
				throw new HttpException("Literal content not allowed for " + base.ControlType);
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0002E15C File Offset: 0x0002C35C
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			Type childControlType = base.Root.GetChildControlType(tagName, attribs);
			if (this.possibleElementTypes != null)
			{
				bool flag = false;
				int num = 0;
				while (num < this.possibleElementTypes.Length && !flag)
				{
					flag = this.possibleElementTypes[num].IsAssignableFrom(childControlType);
					num++;
				}
				if (!flag)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < this.possibleElementTypes.Length; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(this.possibleElementTypes[i]);
					}
					throw new HttpException(string.Concat(new object[] { "Cannot add a ", childControlType, " to ", stringBuilder }));
				}
			}
			return childControlType;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0002E214 File Offset: 0x0002C414
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, id, attribs);
			PropertyInfo property = parentBuilder.ControlType.GetProperty(tagName, ControlBuilder.FlagsNoCase);
			base.SetControlType(property.PropertyType);
			MemberInfo[] member = base.ControlType.GetMember("Item", MemberTypes.Property, ControlBuilder.FlagsNoCase & ~BindingFlags.IgnoreCase);
			if (member.Length != 0)
			{
				this.possibleElementTypes = new Type[member.Length];
				for (int i = 0; i < member.Length; i++)
				{
					this.possibleElementTypes[i] = ((PropertyInfo)member[i]).PropertyType;
				}
				return;
			}
			throw new HttpException("Collection of type '" + base.ControlType + "' does not have an indexer.");
		}

		// Token: 0x04001398 RID: 5016
		private Type[] possibleElementTypes;
	}
}
