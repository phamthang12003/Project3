namespace E_PROJECT_MANAGER.DataTransferObject
{
    public class ViewDTO<T> where T : class
    {
        public int StatusCode { get; set; } = 500;
        public string Message { get; set; } = "Không thành công!";
        public List<T> Data { get; set; } = new List<T>();
    }
}
