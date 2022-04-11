using System;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000004 RID: 4
	public struct EmbedFooter
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000208A File Offset: 0x0000028A
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x04000012 RID: 18
		public string Text;

		// Token: 0x04000013 RID: 19
		public string IconUrl;

		// Token: 0x04000014 RID: 20
		public string ProxyIconUrl;
	}
}
