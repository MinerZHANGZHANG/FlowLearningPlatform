using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class Announcement
    {
        [Key]
        [Column(Order = 1)]
        public Guid BoardId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreateTime { get;set; }
        [Column(Order = 0)]
        public DateTime UpdateTime { get;set; }
        public string HtmlText { get;set; }
        public string Icon { get; set; } = string.Empty;
    }
}
