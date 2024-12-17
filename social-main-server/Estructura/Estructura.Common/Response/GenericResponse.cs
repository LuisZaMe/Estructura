namespace Estructura.Common.Response
{
    public class GenericResponse<T> : ResponseBaseModel
    {
        public T Response { get; set; }
    }
}
