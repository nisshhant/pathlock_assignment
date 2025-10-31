import { useState, useEffect } from "react";
import api from "../api/axios";
import type { Task } from "../types";
import "./TaskList.css";

export default function TaskList({ projectId }: { projectId: number }) {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [title, setTitle] = useState("");
  const [schedule, setSchedule] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  // Fetch tasks
  const fetchTasks = async () => {
    try {
      const res = await api.get<Task[]>(`/projects/${projectId}/tasks`);
      setTasks(res.data);
    } catch (err) {
      console.error("Error fetching tasks:", err);
    }
  };

  // Add new task
  const addTask = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim()) return;
    try {
      await api.post(`/projects/${projectId}/tasks`, { title });
      setTitle("");
      fetchTasks();
    } catch (err) {
      console.error("Error adding task:", err);
    }
  };

  // Toggle task completion
  const toggleTask = async (task: Task) => {
    try {
      await api.put(`/tasks/${task.id}`, {
        ...task,
        isCompleted: !task.isCompleted,
      });
      fetchTasks();
    } catch (err) {
      console.error("Error toggling task:", err);
    }
  };

  // Delete a task
  const deleteTask = async (id: number) => {
    try {
      await api.delete(`/tasks/${id}`);
      fetchTasks();
    } catch (err) {
      console.error("Error deleting task:", err);
    }
  };

  // Generate Smart Schedule
  const generateSchedule = async () => {
    setLoading(true);
    try {
      const res = await api.post(`/projects/${projectId}/schedule`, {
        startDate: new Date().toISOString(),
        workHoursPerDay: 6,
      });
      setSchedule(res.data.schedule || []);
    } catch (err: any) {
      console.error("Scheduler Error:", err.response?.data || err.message);
      alert(err.response?.data?.message || "Error generating schedule");
    } finally {
      setLoading(false);
    }
  };

  // Fetch tasks on load
  useEffect(() => {
    fetchTasks();
  }, [projectId]);

  return (
    <div className="task-list-container">
      <h3 className="task-list-header">Tasks</h3>

      {/* Add Task Form */}
      <form onSubmit={addTask} className="task-form">
        <input
          placeholder="Task title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <button type="submit">Add</button>
      </form>

      {/* Task List */}
      <ul className="task-list">
        {tasks.map((t) => (
          <li key={t.id} className="task-item">
            <span
              onClick={() => toggleTask(t)}
              className={`task-title ${t.isCompleted ? "completed" : ""}`}
            >
              {t.title}
            </span>
            <button
              onClick={() => deleteTask(t.id)}
              className="task-delete-btn"
            >
              Delete
            </button>
          </li>
        ))}
      </ul>

      {/* Smart Schedule Section */}
      <button
        onClick={generateSchedule}
        className="smart-schedule-btn"
        disabled={loading}
      >
        {loading ? "Generating..." : "Generate Smart Schedule"}
      </button>

      {schedule.length > 0 && (
        <div className="schedule-section">
          <h3>Suggested Schedule</h3>
          <ul className="schedule-list">
            {schedule.map((item, index) => (
              <li key={item.taskId} className="schedule-item">
                <span>
                  <strong>Day {index + 1}:</strong> {item.taskTitle}
                </span>
                <span className="schedule-date">{item.scheduledDate}</span>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
