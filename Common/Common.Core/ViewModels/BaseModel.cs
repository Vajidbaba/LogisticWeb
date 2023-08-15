

namespace Common.Core.ViewModels
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
    }
}
