using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200014A RID: 330
	internal class CodeDomSerializationProvider : IDesignerSerializationProvider
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x000125CC File Offset: 0x000107CC
		public static CodeDomSerializationProvider Instance
		{
			get
			{
				if (CodeDomSerializationProvider._instance == null)
				{
					CodeDomSerializationProvider._instance = new CodeDomSerializationProvider();
				}
				return CodeDomSerializationProvider._instance;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000125E4 File Offset: 0x000107E4
		public CodeDomSerializationProvider()
		{
			this._componentSerializer = new ComponentCodeDomSerializer();
			this._propertySerializer = new PropertyCodeDomSerializer();
			this._eventSerializer = new EventCodeDomSerializer();
			this._collectionSerializer = new CollectionCodeDomSerializer();
			this._primitiveSerializer = new PrimitiveCodeDomSerializer();
			this._rootSerializer = new RootCodeDomSerializer();
			this._enumSerializer = new EnumCodeDomSerializer();
			this._othersSerializer = new CodeDomSerializer();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00012650 File Offset: 0x00010850
		public object GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			CodeDomSerializerBase codeDomSerializerBase = null;
			if (serializerType == typeof(CodeDomSerializer))
			{
				if (objectType == null)
				{
					codeDomSerializerBase = this._primitiveSerializer;
				}
				else if (typeof(IComponent).IsAssignableFrom(objectType))
				{
					codeDomSerializerBase = this._componentSerializer;
				}
				else if (objectType.IsEnum || typeof(Enum).IsAssignableFrom(objectType))
				{
					codeDomSerializerBase = this._enumSerializer;
				}
				else if (objectType.IsPrimitive || objectType == typeof(string))
				{
					codeDomSerializerBase = this._primitiveSerializer;
				}
				else if (typeof(ICollection).IsAssignableFrom(objectType))
				{
					codeDomSerializerBase = this._collectionSerializer;
				}
				else
				{
					codeDomSerializerBase = this._othersSerializer;
				}
			}
			else if (serializerType == typeof(MemberCodeDomSerializer))
			{
				if (typeof(PropertyDescriptor).IsAssignableFrom(objectType))
				{
					codeDomSerializerBase = this._propertySerializer;
				}
				else if (typeof(EventDescriptor).IsAssignableFrom(objectType))
				{
					codeDomSerializerBase = this._eventSerializer;
				}
			}
			else if (serializerType == typeof(RootCodeDomSerializer))
			{
				codeDomSerializerBase = this._rootSerializer;
			}
			return codeDomSerializerBase;
		}

		// Token: 0x0400024A RID: 586
		private static CodeDomSerializationProvider _instance;

		// Token: 0x0400024B RID: 587
		private CodeDomSerializerBase _componentSerializer;

		// Token: 0x0400024C RID: 588
		private CodeDomSerializerBase _propertySerializer;

		// Token: 0x0400024D RID: 589
		private CodeDomSerializerBase _eventSerializer;

		// Token: 0x0400024E RID: 590
		private CodeDomSerializerBase _primitiveSerializer;

		// Token: 0x0400024F RID: 591
		private CodeDomSerializerBase _collectionSerializer;

		// Token: 0x04000250 RID: 592
		private CodeDomSerializerBase _rootSerializer;

		// Token: 0x04000251 RID: 593
		private CodeDomSerializerBase _enumSerializer;

		// Token: 0x04000252 RID: 594
		private CodeDomSerializerBase _othersSerializer;
	}
}
