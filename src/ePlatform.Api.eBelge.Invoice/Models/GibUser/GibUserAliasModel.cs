
namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class GibUserAliasModel
    {
        public string Alias { get; set; }
        public int? AppType { get; set; }
        public GibAliasType GibAliasType { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public int GibUserType { get; set; }

        public System.DateTime FirstCreationTime { get; set; }

        public System.DateTime AliasCreationTime { get; set; }

        public bool IsActive { get; set; }
    }
}
