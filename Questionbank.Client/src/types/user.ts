export interface AuthState {
  isAuthenticated: boolean;
  isInittialized: boolean;
  user: User | null;
}

export interface AuthResponse {
  token: string;
  user: User;
}

export interface User {
  id: number;
  avatar: string | null;
  email: string;
  userName: string | null;
  phoneNumber: string | null;
  roles: string[];
}

export type UserState = {
  isLoading: boolean;
  usersData: UserDataType[] | null;
  groupsData: GroupDataType[] | null;
  subjectsData: SubjectDataType[] | null;
  remindersData: ReminderDataType[] | null;
};

export type UserDataType = {
  firstName: string;
  lastName: string;
  email: string;
  groups?: string;
  phoneNumber?: string;
  birthday?: string;
  age?: string;
  gender?: number;
  isActive?: boolean;
  role: string[];
};

export type GroupDataType = {
  id: string;
  name: string;
  description: string;
};

export type GroupDataRequest = {
  name: string;
  description?: string;
};

export type SubjectDataType = {
  id: string;
  name: string;
};

export type SubjectDataRequest = {
  name: string;
};

export type ReminderDataType = {
  id: string;
  name: string;
  description: string;
};

export type ReminderDataRequest = {
  name: string;
  description?: string;
};

export type ModeType = 'edit' | 'new' | 'view';
