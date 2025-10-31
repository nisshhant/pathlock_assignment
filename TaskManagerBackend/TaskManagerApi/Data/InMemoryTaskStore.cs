using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerApi.Models;

namespace TaskManagerApi.Data
{
    public static class InMemoryTaskStore
    {
        // Thread-unsafe for simplicity: OK for homework / single-user dev.
        private static readonly List<TaskItem> _tasks = new()
        {
            new TaskItem { Description = "Sample task 1", IsCompleted = false },
            new TaskItem { Description = "Sample task 2", IsCompleted = true }
        };

        public static List<TaskItem> GetAll() => _tasks;

        public static TaskItem? Get(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);

        public static TaskItem Add(TaskItem item)
        {
            _tasks.Add(item);
            return item;
        }

        public static bool Remove(Guid id)
        {
            var existing = Get(id);
            if (existing == null) return false;
            _tasks.Remove(existing);
            return true;
        }

        public static bool Update(Guid id, TaskItem updated)
        {
            var existing = Get(id);
            if (existing == null) return false;
            existing.Description = updated.Description;
            existing.IsCompleted = updated.IsCompleted;
            return true;
        }
    }
}
