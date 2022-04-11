using System;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000008 RID: 8
	public struct EmbedField
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x0400001F RID: 31
		public string Name;

		// Token: 0x04000020 RID: 32
		public string Value;

		// Token: 0x04000021 RID: 33
		public bool InLine;
	}
}
