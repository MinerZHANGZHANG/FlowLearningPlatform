using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	/// <summary>
	/// 用户注册的第二部分，填写基本信息的表单模型
	/// </summary>
	public class RegisterSecondStep
	{
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }
        [Required(ErrorMessage = "专业不能为空")]
        public string DepartmentTypeId { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get;set; }
		[Required(ErrorMessage = "确认密码不能为空"), Compare(nameof(Password),ErrorMessage = "两次输入的密码需要一致")]
		public string ConfirmPassword { get; set; }
	}
}
