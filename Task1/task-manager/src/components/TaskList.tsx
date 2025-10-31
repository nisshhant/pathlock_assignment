import { useEffect, useState } from "react";
import api from "../api/axios";
import type { Task } from "../types";

export default function TaskList() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTask, setNewTask] = useState("");

  // Fetch tasks on load
  const fetchTasks = async () => {
    const res = await api.get("/tasks");
    setTasks(res.data);
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const addTask = async () => {
    if (!newTask.trim()) return;
    await api.post("/tasks", { description: newTask });
    setNewTask("");
    fetchTasks();
  };

  const toggleTask = async (id: number) => {
    await api.put(`/tasks/${id}`);
    fetchTasks();
  };

  const deleteTask = async (id: number) => {
    await api.delete(`/tasks/${id}`);
    fetchTasks();
  };

  return (
    <div className="max-w-md mx-auto p-4 bg-gray-100 rounded-2xl shadow">
      <h1 className="text-2xl font-bold text-center mb-4">📝 Task Manager</h1>

      <div className="flex gap-2 mb-4">
        <input
          value={newTask}
          onChange={(e) => setNewTask(e.target.value)}
          placeholder="Enter new task..."
          className="flex-1 border rounded p-2"
        />
        <button
          onClick={addTask}
          className="bg-blue-500 text-white px-4 rounded hover:bg-blue-600"
        >
          Add
        </button>
      </div>

      <ul>
        {tasks.map((t) => (
          <li
            key={t.id}
            className="flex justify-between items-center mb-2 bg-white rounded-lg p-2 shadow-sm"
          >
            <span
              onClick={() => toggleTask(t.id)}
              className={`cursor-pointer ${
                t.isCompleted ? "line-through text-gray-500" : ""
              }`}
            >
              {t.description}
            </span>
            <button
              onClick={() => deleteTask(t.id)}
              className="text-red-500 hover:text-red-700"
            >
              ✖
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
