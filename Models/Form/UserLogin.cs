using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlowLearningPlatform.Models.Form
{
    public class UserLogin
    {
        [Required, DisplayName("学号")]
        public string StudentNumber { get; set; }
        [Required, DisplayName("密码")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}
