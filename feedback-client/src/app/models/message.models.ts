export interface Theme {
  id: number;
  name: string;
}

export interface FeedbackRequest {
  name: string;
  email: string;
  phone: string;
  themeId: number;
  content: string;
}

export interface FeedbackResponse {
  messageId: number;
  text: string;
  themeName: string;
  createdAt: string;
  contactName: string;
  contactEmail: string;
  contactPhone: string;
}
