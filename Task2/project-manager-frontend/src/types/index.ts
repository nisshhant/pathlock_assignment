export interface Project {
  id: number;
  title: string;
  description?: string;
  createdAt: string;
}

export interface Task {
  id: number;
  title: string;
  dueDate?: string;
  isCompleted: boolean;
  projectId: number;
}
