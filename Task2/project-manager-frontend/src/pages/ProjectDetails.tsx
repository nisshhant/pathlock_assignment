import { useParams } from "react-router-dom";
import Navbar from "../components/Navbar";
import TaskList from "../components/TaskList";
import api from "../api/axios";
import { useState } from "react";

export default function ProjectDetails() {
  const { id } = useParams<{ id: string }>();
  const [schedule, setSchedule] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  const generateSchedule = async () => {
    setLoading(true);
    try {
      const res = await api.post(`/projects/${id}/schedule`, {
        startDate: new Date().toISOString(),
        workHoursPerDay: 6,
      });
      setSchedule(res.data.schedule);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <Navbar />
      <div className="p-6">
        <h2 className="text-2xl font-bold mb-3">Project #{id}</h2>
        <button
          onClick={generateSchedule}
          className="bg-purple-600 text-white px-4 py-2 rounded hover:bg-purple-700"
          disabled={loading}
        >
          {loading ? "Generating..." : "Generate Smart Schedule"}
        </button>

        {schedule.length > 0 && (
          <div className="mt-4">
            <h3 className="text-xl font-semibold mb-2">Suggested Schedule</h3>
            <ul className="space-y-2">
              {schedule.map((item) => (
                <li
                  key={item.taskId}
                  className="border p-2 rounded flex justify-between"
                >
                  <span>{item.taskTitle}</span>
                  <span className="text-gray-500">{item.scheduledDate}</span>
                </li>
              ))}
            </ul>
          </div>
        )}

        <TaskList projectId={parseInt(id!)} />
      </div>
    </div>
  );
}
