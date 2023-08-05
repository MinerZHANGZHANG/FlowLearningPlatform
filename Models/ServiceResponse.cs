namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 包装返回信息
    /// </summary>
    /// <typeparam name="T">返回的数据类型</typeparam>
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

    }
}
