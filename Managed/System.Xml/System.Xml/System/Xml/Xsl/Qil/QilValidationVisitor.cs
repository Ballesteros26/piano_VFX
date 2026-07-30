using System;
using System.Diagnostics;
using System.Xml.Utils;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000648 RID: 1608
	internal class QilValidationVisitor : QilScopedVisitor
	{
		// Token: 0x06004086 RID: 16518 RVA: 0x00159C8B File Offset: 0x00157E8B
		[Conditional("DEBUG")]
		public static void Validate(QilNode node)
		{
			new QilValidationVisitor().VisitAssumeReference(node);
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x00159C99 File Offset: 0x00157E99
		protected QilValidationVisitor()
		{
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x00159CB8 File Offset: 0x00157EB8
		[Conditional("DEBUG")]
		internal static void SetError(QilNode n, string message)
		{
			message = Res.GetString("QIL Validation Error! '{0}'.", new object[] { message });
			string text = n.Annotation as string;
			if (text != null)
			{
				message = text + "\n" + message;
			}
			n.Annotation = message;
		}

		// Token: 0x040028CC RID: 10444
		private SubstitutionList subs = new SubstitutionList();

		// Token: 0x040028CD RID: 10445
		private QilTypeChecker typeCheck = new QilTypeChecker();
	}
}
