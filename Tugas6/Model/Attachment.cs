using System.ComponentModel.DataAnnotations.Schema;

namespace Tugas6.Model
{
    [Table("Attachments")]
    public class Attachment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("todo_id")]
        public int TodoId { get; set; }
        [Column("url")]
        public string Url { get; set; }
    }
}
