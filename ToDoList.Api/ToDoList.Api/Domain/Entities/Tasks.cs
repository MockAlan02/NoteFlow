using ToDoList.Api.Domain.Common;

namespace ToDoList.Api.Domain.Entities
{
    public class Tasks : BaseEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
