using System.ComponentModel.DataAnnotations.Schema;

namespace InsaneIO.Insane.EntityFrameworkCore
{
    public interface IEntity
    {
        [NotMapped]
        public string UniqueId { get; set; }
    }
}
