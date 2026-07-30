using System;
using System.Collections;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x02000267 RID: 615
	internal class ListSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x060019DA RID: 6618 RVA: 0x0002A3D0 File Offset: 0x000285D0
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IList list = (IList)obj;
			info.AddValue("_size", list.Count);
			info.AddValue("_items", ListSerializationSurrogate.ArrayFromGenericList(list));
			info.AddValue("_version", 0);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0002A418 File Offset: 0x00028618
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			int @int = info.GetInt32("_size");
			bool flag = @int == 0;
			object obj2;
			if (flag)
			{
				obj2 = list;
			}
			else
			{
				IEnumerator enumerator = ((IEnumerable)info.GetValue("_items", typeof(IEnumerable))).GetEnumerator();
				for (int i = 0; i < @int; i++)
				{
					bool flag2 = !enumerator.MoveNext();
					if (flag2)
					{
						throw new InvalidOperationException();
					}
					list.Add(enumerator.Current);
				}
				obj2 = list;
			}
			return obj2;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0002A4B4 File Offset: 0x000286B4
		private static Array ArrayFromGenericList(IList list)
		{
			Array array = Array.CreateInstance(list.GetType().GetGenericArguments()[0], list.Count);
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040007EF RID: 2031
		public static readonly ISerializationSurrogate Default = new ListSerializationSurrogate();
	}
}
