using System;
using System.Collections;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000657 RID: 1623
	internal class GlobalAsaxCompiler : BaseCompiler
	{
		// Token: 0x060045A2 RID: 17826 RVA: 0x000BED5D File Offset: 0x000BCF5D
		public GlobalAsaxCompiler(ApplicationFileParser parser)
			: base(parser)
		{
			GlobalAsaxCompiler.applicationObjectTags.Clear();
			GlobalAsaxCompiler.sessionObjectTags.Clear();
			this.parser = parser;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x0003B1E5 File Offset: 0x000393E5
		public static Type CompileApplicationType(ApplicationFileParser parser)
		{
			return new AspGenerator(parser).GetCompiledType();
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000BED81 File Offset: 0x000BCF81
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			base.CreateProfileProperty();
			this.ProcessObjects(this.parser.RootBuilder);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x000BEDA0 File Offset: 0x000BCFA0
		private void ProcessObjects(ControlBuilder builder)
		{
			if (builder.Children == null)
			{
				return;
			}
			foreach (object obj in builder.Children)
			{
				if (obj is ObjectTagBuilder)
				{
					ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)obj;
					if (objectTagBuilder.Scope == null)
					{
						string text = base.CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
						base.CreatePropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, text, true);
					}
					else if (string.Compare(objectTagBuilder.Scope, "session", true, Helpers.InvariantCulture) == 0)
					{
						GlobalAsaxCompiler.sessionObjectTags.Add(objectTagBuilder);
						base.CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, false, false);
					}
					else
					{
						if (string.Compare(objectTagBuilder.Scope, "application", true, Helpers.InvariantCulture) != 0)
						{
							throw new ParseException(objectTagBuilder.Location, "Invalid scope: " + objectTagBuilder.Scope);
						}
						GlobalAsaxCompiler.applicationObjectTags.Add(objectTagBuilder);
						base.CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
						base.CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, true, false);
					}
				}
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x000BEEF4 File Offset: 0x000BD0F4
		internal static ArrayList ApplicationObjects
		{
			get
			{
				return GlobalAsaxCompiler.applicationObjectTags;
			}
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x000BEEFB File Offset: 0x000BD0FB
		internal static ArrayList SessionObjects
		{
			get
			{
				return GlobalAsaxCompiler.sessionObjectTags;
			}
		}

		// Token: 0x04002500 RID: 9472
		private ApplicationFileParser parser;

		// Token: 0x04002501 RID: 9473
		private static ArrayList applicationObjectTags = new ArrayList(1);

		// Token: 0x04002502 RID: 9474
		private static ArrayList sessionObjectTags = new ArrayList(1);
	}
}
