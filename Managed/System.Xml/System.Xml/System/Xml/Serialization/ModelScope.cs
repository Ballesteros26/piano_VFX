using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002F5 RID: 757
	internal class ModelScope
	{
		// Token: 0x06001C66 RID: 7270 RVA: 0x0009B51D File Offset: 0x0009971D
		internal ModelScope(TypeScope typeScope)
		{
			this.typeScope = typeScope;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0009B542 File Offset: 0x00099742
		internal TypeScope TypeScope
		{
			get
			{
				return this.typeScope;
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0009B54A File Offset: 0x0009974A
		internal TypeModel GetTypeModel(Type type)
		{
			return this.GetTypeModel(type, true);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0009B554 File Offset: 0x00099754
		internal TypeModel GetTypeModel(Type type, bool directReference)
		{
			TypeModel typeModel = (TypeModel)this.models[type];
			if (typeModel != null)
			{
				return typeModel;
			}
			TypeDesc typeDesc = this.typeScope.GetTypeDesc(type, null, directReference);
			switch (typeDesc.Kind)
			{
			case TypeKind.Root:
			case TypeKind.Struct:
			case TypeKind.Class:
				typeModel = new StructModel(type, typeDesc, this);
				break;
			case TypeKind.Primitive:
				typeModel = new PrimitiveModel(type, typeDesc, this);
				break;
			case TypeKind.Enum:
				typeModel = new EnumModel(type, typeDesc, this);
				break;
			case TypeKind.Array:
			case TypeKind.Collection:
			case TypeKind.Enumerable:
				typeModel = new ArrayModel(type, typeDesc, this);
				break;
			default:
				if (!typeDesc.IsSpecial)
				{
					throw new NotSupportedException(Res.GetString("The type {0} may not be serialized.", new object[] { type.FullName }));
				}
				typeModel = new SpecialModel(type, typeDesc, this);
				break;
			}
			this.models.Add(type, typeModel);
			return typeModel;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0009B620 File Offset: 0x00099820
		internal ArrayModel GetArrayModel(Type type)
		{
			TypeModel typeModel = (TypeModel)this.arrayModels[type];
			if (typeModel == null)
			{
				typeModel = this.GetTypeModel(type);
				if (!(typeModel is ArrayModel))
				{
					TypeDesc arrayTypeDesc = this.typeScope.GetArrayTypeDesc(type);
					typeModel = new ArrayModel(type, arrayTypeDesc, this);
				}
				this.arrayModels.Add(type, typeModel);
			}
			return (ArrayModel)typeModel;
		}

		// Token: 0x0400164B RID: 5707
		private TypeScope typeScope;

		// Token: 0x0400164C RID: 5708
		private Hashtable models = new Hashtable();

		// Token: 0x0400164D RID: 5709
		private Hashtable arrayModels = new Hashtable();
	}
}
