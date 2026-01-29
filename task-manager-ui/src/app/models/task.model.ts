export interface TaskAdditionalData {
  priority?: string;
  estimatedEndDate?: string;
  tags?: string[];
}

export interface TaskItem {
  id: number;
  title: string;
  description?: string;
  status: number;
  userId: number;
  createdAt: string;
  additionalData?: TaskAdditionalData;
}
