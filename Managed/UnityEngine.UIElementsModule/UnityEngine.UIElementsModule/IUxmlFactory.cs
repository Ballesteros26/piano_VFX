using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FD RID: 509
	public interface IUxmlFactory
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F80 RID: 3968
		string uxmlName { get; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F81 RID: 3969
		string uxmlNamespace { get; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F82 RID: 3970
		string uxmlQualifiedName { get; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F83 RID: 3971
		bool canHaveAnyAttribute { get; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F84 RID: 3972
		IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription { get; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F85 RID: 3973
		IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F86 RID: 3974
		string substituteForTypeName { get; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F87 RID: 3975
		string substituteForTypeNamespace { get; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F88 RID: 3976
		string substituteForTypeQualifiedName { get; }

		// Token: 0x06000F89 RID: 3977
		bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x06000F8A RID: 3978
		VisualElement Create(IUxmlAttributes bag, CreationContext cc);
	}
}
