namespace TaskManagmentApi.Models
{
    public class Response
    {
        public Response(dynamic? statusCode, dynamic? responseMessage, dynamic? data)
        {
            this.statusCode = statusCode;
            this.Message = responseMessage;
            this.Data = data;
        }
        public dynamic statusCode { get; set; }
        public dynamic Message { get; set; }
        public dynamic Data { get; set; }
    }
}
