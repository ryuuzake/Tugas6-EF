using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tugas6.Model
{
    [Table("Todos")]
    public class Todo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("body")]
        public string Body { get; set; }

        public List<Attachment> Attachments { get; } = new List<Attachment>();
    }
}
