import * as yup from 'yup';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useMemo } from 'react';
import { GroupDataType, ModeType } from '../../../types/user';
import { useDispatch } from '../../../redux/store';
import { createGroup, getGroupsData } from '../../../redux/slice/userSlice';

interface FormValues {
  name: string;
  description?: string;
}

const validationSchema = yup.object().shape({
  name: yup.string().required('Name is required'),
  description: yup.string(),
});

const useFormGroup = (
  mode: ModeType,
  currentData: GroupDataType | null,
  onClose: () => void,
) => {
  const dispatch = useDispatch();

  const defaultValues = useMemo(
    () => ({
      name: currentData?.name || '',
      description: currentData?.description || '',
    }),
    [currentData],
  );

  const methods = useForm<FormValues>({
    resolver: yupResolver(validationSchema),
    defaultValues,
  });

  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = methods;

  useEffect(() => {
    if (mode !== 'new' && currentData) {
      reset(defaultValues);
    }
  }, [mode, currentData, reset, defaultValues]);

  const onSubmit: SubmitHandler<FormValues> = (data) => {
    onClose();
    dispatch(createGroup(data));
    dispatch(getGroupsData());
  };

  return {
    formState: { errors },
    func: {
      control,
      onSubmit: handleSubmit(onSubmit),
    },
  };
};

export default useFormGroup;
