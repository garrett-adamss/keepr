
namespace keepr.Models
{
    public class VaultKeep : RepoItem<int>
    {
        public string creatorId{ get; set;}
        public int vaultId {get; set;}
        public int keepId { get; set;}
        public Profile Creator{ get; set;}
    }
}
