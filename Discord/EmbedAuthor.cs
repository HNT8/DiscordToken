using System;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000007 RID: 7
	public struct EmbedAuthor
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E1 File Offset: 0x000002E1
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x0400001B RID: 27
		public string Name;

		// Token: 0x0400001C RID: 28
		public string Url;

		// Token: 0x0400001D RID: 29
		public string IconUrl;

		// Token: 0x0400001E RID: 30
		public string ProxyIconUrl;
	}
}
