import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useMemo } from 'react';
import { ModeType, ReminderDataType } from '../../../types/user';

interface FormValues {
  name: string;
  description?: string;
}

const validationSchema = yup.object().shape({
  name: yup.string().required('Name is required'),
  description: yup.string(),
});

const useFormReminder = (
  mode: ModeType,
  currentData: ReminderDataType | null,
) => {
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
    if ((mode === 'view' || mode === 'edit') && currentData) {
      reset(defaultValues);
    }
    if (!(mode === 'new')) {
      reset(defaultValues);
    }
  }, [mode, currentData]);

  const onSubmit = (data: FormValues) => {
    console.log(data);
  };

  return {
    formState: {
      errors,
    },
    func: {
      control,
      onSubmit: handleSubmit(onSubmit),
    },
  };
};

export default useFormReminder;
