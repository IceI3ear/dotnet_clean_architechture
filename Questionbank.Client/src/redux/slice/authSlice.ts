import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthResponse, AuthState, User } from '../../types/user';
import { jwtDecode } from 'jwt-decode';
import axiosInstance from '../../utils/axios-interceptors';
import { STORAGE_KEY } from '../../constants/user';

const initialState: AuthState = {
  isAuthenticated: false,
  isInittialized: false,
  user: null,
};

type AuthActionPayload = PayloadAction<AuthState>;
type SignInActionPayload = PayloadAction<{ user: User }>;

const reducers = {
  initialize: (state: AuthState, action: AuthActionPayload) => {
    state.isInittialized = action.payload.isInittialized;
    (state.isAuthenticated = action.payload.isAuthenticated),
      (state.user = action.payload.user);
  },
  signIn: (state: AuthState, action: SignInActionPayload) => {
    state.isAuthenticated = true;
    state.user = action.payload.user;
  },
  signOut: (state: AuthState) => {
    state.isAuthenticated = false;
    state.user = null;
  },
};

const authSlice = createSlice({
  name: 'auth',
  initialState: initialState,
  reducers,
});

export const { initialize, signIn, signOut } = authSlice.actions;

export const initializeState = () => {
  return async (dispatch: any) => {
    try {
      const accessToken = localStorage.getItem(STORAGE_KEY);

      if (accessToken) {
        const user = {
          id: 1,
          userName: null,
          email: 'admin@gmail.com',
          phoneNumber: null,
          avatar: null,
          roles: ['Admin', 'ProofReader', 'QuestionCreator'],
        };

        const decodedToken: any = jwtDecode(accessToken);

        const currentTime = Date.now() / 1000;

        if (decodedToken.exp < currentTime) {
          // Token has expired
          dispatch(
            initialize({
              isInittialized: true,
              isAuthenticated: false,
              user: null,
            }),
          );
        } else {
          dispatch(
            initialize({
              isInittialized: true,
              isAuthenticated: true,
              user,
            }),
          );
        }
      } else {
        dispatch(
          initialize({
            isInittialized: true,
            isAuthenticated: false,
            user: null,
          }),
        );
      }
    } catch (err) {
      console.error(err);
      dispatch(
        initialize({
          isInittialized: true,
          isAuthenticated: false,
          user: null,
        }),
      );
    }
  };
};

export const loginUser =
  (email: string, password: string) => async (dispatch: any) => {
    try {
      const data = {
        email,
        password,
      };
      // Perform login API call and get JWT token
      const response = await axiosInstance.post('/Authentication/login', data);

      const result: AuthResponse = response.data;

      // // Decode JWT token to get user information
      // const decodedToken = jwtDecode(data.token);

      if (result) {
        localStorage.setItem(STORAGE_KEY, result.token);
        // Dispatch action to update Redux store
        dispatch(signIn({ user: result.user }));
      }
    } catch (error) {
      console.error('Login failed:', error);
    }
  };

export const logoutUser = () => (dispatch: any) => {
  localStorage.removeItem(STORAGE_KEY);

  dispatch(signOut());
};

export default authSlice.reducer;
