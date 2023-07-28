using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models
{
    public class Board
    {
        [Key]
        public Guid BoardId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreateTime { get;set; }
        public DateTime UpdateTime { get;set; }
        public string Text { get;set; }

    }
}
