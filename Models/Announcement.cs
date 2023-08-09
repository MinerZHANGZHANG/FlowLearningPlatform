using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class Announcement
    {
        [Key]
        [Column(Order = 1)]
        public Guid AnnouncementId { get; set; }
        public string Title { get; set; } = string.Empty;
		public string SubTitle { get; set; } = string.Empty;
		public DateTime CreateTime { get;set; }
        [Column(Order = 0)]
        public DateTime UpdateTime { get;set; }
        public string HtmlText { get;set; }=string.Empty;
        public string Icon { get; set; } = string.Empty;
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
