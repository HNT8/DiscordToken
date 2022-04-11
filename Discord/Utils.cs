using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Discord
{
	// Token: 0x02000009 RID: 9
	public static class Utils
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public static int ColorToHex(Color color)
		{
			return int.Parse(color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"), NumberStyles.HexNumber);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002174 File Offset: 0x00000374
		internal static JObject StructToJson(object @struct)
		{
			Type type = @struct.GetType();
			JObject jobject = new JObject();
			foreach (FieldInfo fieldInfo in type.GetFields())
			{
				string propertyName = Utils.FieldNameToJsonName(fieldInfo.Name);
				object value = fieldInfo.GetValue(@struct);
				if (value != null)
				{
					if (value is bool)
					{
						jobject.Add(propertyName, (bool)value);
					}
					else if (value is int)
					{
						jobject.Add(propertyName, (int)value);
					}
					else if (value is Color)
					{
						jobject.Add(propertyName, Utils.ColorToHex((Color)value));
					}
					else if (value is string)
					{
						jobject.Add(propertyName, value as string);
					}
					else if (value is DateTime)
					{
						jobject.Add(propertyName, ((DateTime)value).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"));
					}
					else if (value is IList && value.GetType().IsGenericType)
					{
						JArray jarray = new JArray();
						foreach (object struct2 in (value as IList))
						{
							jarray.Add(Utils.StructToJson(struct2));
						}
						jobject.Add(propertyName, jarray);
					}
					else
					{
						jobject.Add(propertyName, Utils.StructToJson(value));
					}
				}
			}
			return jobject;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000230C File Offset: 0x0000050C
		internal static string FieldNameToJsonName(string name)
		{
			if (Utils.ignore.ToList<string>().Contains(name))
			{
				return name.ToLower();
			}
			List<char> list = new List<char>();
			if (Utils.IsFullUpper(name))
			{
				list.AddRange(name.ToLower().ToCharArray());
			}
			else
			{
				for (int i = 0; i < name.Length; i++)
				{
					if (i > 0 && char.IsUpper(name[i]))
					{
						list.AddRange(new char[]
						{
							'_',
							char.ToLower(name[i])
						});
					}
					else
					{
						list.Add(char.ToLower(name[i]));
					}
				}
			}
			return string.Join<char>("", list);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023B8 File Offset: 0x000005B8
		internal static bool IsFullUpper(string str)
		{
			bool result = true;
			for (int i = 0; i < str.Length; i++)
			{
				if (!char.IsUpper(str[i]))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023EC File Offset: 0x000005EC
		public static string Decode(Stream source)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(source))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002424 File Offset: 0x00000624
		public static byte[] Encode(string source, string encoding = "utf-8")
		{
			return Encoding.GetEncoding(encoding).GetBytes(source);
		}

		// Token: 0x04000022 RID: 34
		private static string[] ignore = new string[]
		{
			"InLine"
		};
	}
}
