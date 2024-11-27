namespace Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }


        public Response() { }

        //Respuesta cuando no tuvo éxito
        public Response(string message) 
        {
            Succeeded = false;
            Message = message;
        }

        //Respuesta cuando tuvo éxito
        public Response(T data, string message = null) 
        {
            Succeeded = true;
            Data = data;
            Message = message;
        }

    }
}
