import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useMemo } from 'react';
import { ModeType, SubjectDataType } from '../../../types/user';

interface FormValues {
  name: string;
}

const validationSchema = yup.object().shape({
  name: yup.string().required('Subject name is required'),
});

const useFormSubject = (
  mode: ModeType,
  currentData: SubjectDataType | null,
) => {
  const defaultValues = useMemo(
    () => ({
      name: currentData?.name || '',
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

export default useFormSubject;
