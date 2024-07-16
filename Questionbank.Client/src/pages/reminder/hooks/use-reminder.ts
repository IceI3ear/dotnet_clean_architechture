import { useEffect, useState } from 'react';
import { useDispatch } from '../../../redux/store';
import { getUsersData } from '../../../redux/slice/userSlice';
import { ModeType, ReminderDataType } from '../../../types/user';

const useReminder = () => {
  const dispatch = useDispatch();
  const [mode, setMode] = useState<ModeType | ''>('');
  const [currentData, setCurrentData] = useState<ReminderDataType | null>(null);

  const handleOpen = (value: ModeType) => {
    setMode(value);
  };

  const handleClose = () => {
    setMode('');
  };

  useEffect(() => {
    dispatch(getUsersData());
  }, []);

  const handleEdit = (value: ReminderDataType) => {
    setMode('edit');
    setCurrentData(value);
  };

  const handleView = (value: ReminderDataType) => {
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

export default useReminder;
