import { useEffect, useState } from 'react';
import { useDispatch } from '../../../redux/store';
import { getSubjectData } from '../../../redux/slice/userSlice';
import { ModeType, SubjectDataType } from '../../../types/user';

const useSubject = () => {
  const dispatch = useDispatch();
  const [mode, setMode] = useState<ModeType | ''>('');
  const [currentData, setCurrentData] = useState<SubjectDataType | null>(null);

  const handleOpen = (value: ModeType) => {
    setMode(value);
  };

  const handleClose = () => {
    setMode('');
  };

  useEffect(() => {
    dispatch(getSubjectData());
  }, []);

  const handleEdit = (value: SubjectDataType) => {
    setMode('edit');
    setCurrentData(value);
  };

  const handleView = (value: SubjectDataType) => {
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

export default useSubject;
