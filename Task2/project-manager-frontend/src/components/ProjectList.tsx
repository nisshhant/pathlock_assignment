import { useState, useEffect } from "react";
import api from "../api/axios";
import type { Project } from "../types";
import { Link } from "react-router-dom";
import "./ProjectList.css";

export default function ProjectList() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const fetchProjects = async () => {
    const res = await api.get<Project[]>("/projects");
    setProjects(res.data);
  };

  const addProject = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim()) return;
    await api.post("/projects", { title, description });
    setTitle("");
    setDescription("");
    fetchProjects();
  };

  const deleteProject = async (id: number) => {
    await api.delete(`/projects/${id}`);
    fetchProjects();
  };

  useEffect(() => {
    fetchProjects();
  }, []);

  return (
    <div className="project-list-container">
      <h2 className="project-list-header">Your Projects</h2>

      <form onSubmit={addProject} className="project-form">
        <input
          placeholder="Project Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <textarea
          placeholder="Description (optional)"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <button type="submit">Add Project</button>
      </form>

      <ul className="project-list">
        {projects.map((p) => (
          <li key={p.id} className="project-item">
            <div>
              <Link to={`/projects/${p.id}`} className="project-title">
                {p.title}
              </Link>
              <p className="project-description">{p.description}</p>
            </div>
            <button
              onClick={() => deleteProject(p.id)}
              className="delete-btn"
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
