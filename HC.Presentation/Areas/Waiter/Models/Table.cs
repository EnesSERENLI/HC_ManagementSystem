using HC.Domain.Enums;

namespace HC.Presentation.Areas.Waiter.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public Status Status { get; set; }
    }
}
