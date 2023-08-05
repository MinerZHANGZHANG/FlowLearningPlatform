using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlowLearningPlatform.Models.Form
{
    public class UserLogin
    {
        [Required(ErrorMessage = "学号不能为空"), DisplayName("学号")]
        public string StudentNumber { get; set; }
        [Required(ErrorMessage = "密码不能为空"), DisplayName("密码")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}
