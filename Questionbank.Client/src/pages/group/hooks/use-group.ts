import { useEffect, useState } from 'react';
import { useDispatch } from '../../../redux/store';
import { getGroupsData } from '../../../redux/slice/userSlice';
import { GroupDataType, ModeType } from '../../../types/user';

const useGroup = () => {
  const dispatch = useDispatch();
  const [mode, setMode] = useState<ModeType | ''>('');
  const [currentData, setCurrentData] = useState<GroupDataType | null>(null);

  const handleOpen = (value: ModeType) => {
    setMode(value);
  };

  const handleClose = () => {
    setMode('');
  };

  useEffect(() => {
    dispatch(getGroupsData());
  }, []);

  const handleEdit = (value: GroupDataType) => {
    setMode('edit');
    setCurrentData(value);
  };

  const handleView = (value: GroupDataType) => {
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

export default useGroup;
