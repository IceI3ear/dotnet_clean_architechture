import { combineReducers } from 'redux';
import authReducer from '../slice/authSlice';
import userSlice from '../slice/userSlice';

const rootReducer = combineReducers({
  auth: authReducer,
  user: userSlice,
});

export default rootReducer;
