import { useEffect, useState } from 'react';
import { useDispatch } from '../../../redux/store';
import { getUsersData } from '../../../redux/slice/userSlice';
import { ModeType, UserDataType } from '../../../types/user';

const useUser = () => {
  const dispatch = useDispatch();
  const [mode, setMode] = useState<ModeType | ''>('');
  const [currentData, setCurrentData] = useState<UserDataType | null>(null);

  const handleOpen = (value: ModeType) => {
    setMode(value);
  };

  const handleClose = () => {
    setMode('');
  };

  useEffect(() => {
    dispatch(getUsersData());
  }, []);

  const handleEdit = (value: UserDataType) => {
    setMode('edit');
    setCurrentData(value);
  };

  const handleView = (value: UserDataType) => {
    setMode('view');
    setCurrentData(value);
  };

  const handleDelete = () => {};

  return {
    mode,
    handleOpen,
    handleClose,
    currentData,
    handleEdit,
    handleView,
    handleDelete,
  };
};

export default useUser;
