namespace ToDoList.Api.Domain.Common
{
    public class BaseEntity<T>
    {
        public T? Id { get; set; }
    }
}
