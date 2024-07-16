import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
  GroupDataRequest,
  GroupDataType,
  ReminderDataType,
  SubjectDataType,
  UserDataType,
  UserState,
} from '../../types/user';
import axiosInstance from '../../utils/axios-interceptors';

const initialState: UserState = {
  isLoading: false,
  usersData: null,
  groupsData: null,
  subjectsData: null,
  remindersData: null,
};

type getUsersDataAction = PayloadAction<UserDataType[]>;
type getGroupsDataAction = PayloadAction<GroupDataType[]>;
type getSubjectsDataAction = PayloadAction<SubjectDataType[]>;
type getRemindersDataAction = PayloadAction<ReminderDataType[]>;

const reducers = {
  getUsersDataRequest: (state: UserState) => {
    state.isLoading = true;
  },
  getUsersDataSuccess: (state: UserState, action: getUsersDataAction) => {
    state.isLoading = false;
    state.usersData = action.payload;
  },

  getGroupsDataRequest: (state: UserState) => {
    state.isLoading = true;
  },
  getGroupsDataSuccess: (state: UserState, action: getGroupsDataAction) => {
    state.isLoading = false;
    state.groupsData = action.payload;
  },

  getSubjectsDataRequest: (state: UserState) => {
    state.isLoading = true;
  },
  getSubjectsDataSuccess: (state: UserState, action: getSubjectsDataAction) => {
    state.isLoading = false;
    state.subjectsData = action.payload;
  },

  getRemindersDataRequest: (state: UserState) => {
    state.isLoading = true;
  },
  getRemindersDataSuccess: (
    state: UserState,
    action: getRemindersDataAction,
  ) => {
    state.isLoading = false;
    state.remindersData = action.payload;
  },
};

const userSlice = createSlice({
  name: 'user',
  initialState: initialState,
  reducers,
});

export const {
  getUsersDataRequest,
  getUsersDataSuccess,
  getGroupsDataRequest,
  getGroupsDataSuccess,
  getSubjectsDataRequest,
  getSubjectsDataSuccess,
  getRemindersDataRequest,
  getRemindersDataSuccess,
} = userSlice.actions;

export const getUsersData = () => async (dispatch: any) => {
  try {
    dispatch(getUsersDataRequest());

    const response = await axiosInstance.get('/User/GetList');
    const result = response.data;

    if (result) {
      dispatch(getUsersDataSuccess(result.data));
    }
  } catch (error) {
    console.error('Login failed:', error);
  }
};

export const createGroup = (data: GroupDataRequest) => async () => {
  try {
    const response = await axiosInstance.post('/Group/Create', data);

    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export const getGroupsData = () => async (dispatch: any) => {
  try {
    dispatch(getGroupsDataRequest());

    const response = await axiosInstance.get('/Group/GetList');
    const result = response.data;

    if (result) {
      dispatch(getGroupsDataSuccess(result.data));
    }
  } catch (error) {
    console.error('Login failed:', error);
  }
};

export const getSubjectData = () => async (dispatch: any) => {
  try {
    dispatch(getSubjectsDataRequest());

    const response = await axiosInstance.get('/Subject/GetList');
    const result = response.data;

    if (result) {
      dispatch(getSubjectsDataSuccess(result.data));
    }
  } catch (error) {
    console.error('Login failed:', error);
  }
};

export const getReminderData = () => async (dispatch: any) => {
  try {
    dispatch(getRemindersDataRequest());

    const response = await axiosInstance.get('/Reminder/GetList');
    const result = response.data;

    if (result) {
      dispatch(getRemindersDataSuccess(result.data));
    }
  } catch (error) {
    console.error('Login failed:', error);
  }
};

export default userSlice.reducer;
