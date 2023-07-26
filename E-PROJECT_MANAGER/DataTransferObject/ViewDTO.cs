namespace E_PROJECT_MANAGER.DataTransferObject
{
    public class ViewDTO<T> where T : class
    {
        public int totalRows { get; set; } = 0;
        public int StatusCode { get; set; } = 500;
        public List<T> DataRows { get; set; } = new List<T>();
        public string Message { get; set; } = "Không thành công!";
        public List<T> Data { get; set; } = new List<T>();

    }
}
