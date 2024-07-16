import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useMemo } from 'react';
import { ModeType, UserDataType } from '../../../types/user';

interface FormValues {
  firstName: string;
  lastName: string;
  email: string;
  groups?: string;
  phoneNumber?: string;
  birthday?: Date | any;
  age?: string;
  gender?: number;
  isActive?: boolean;
  role: string[];
}

const validationSchema = yup.object().shape({
  firstName: yup.string().required('First name is required'),
  lastName: yup.string().required('Last name is required'),
  email: yup
    .string()
    .email('Invalid email format')
    .required('Email is required'),
  groups: yup.string(),
  phoneNumber: yup.string(),
  birthday: yup.date(),
  age: yup.string(),
  gender: yup.number(),
  isActive: yup.boolean(),
  role: yup
    .array()
    .of(yup.string().required())
    .min(1, 'At least one role is required')
    .required('Role is required'),
});

const useFormUser = (mode: ModeType, currentUser: UserDataType | null) => {
  const defaultValues = useMemo(
    () => ({
      firstName: currentUser?.firstName || '',
      lastName: currentUser?.lastName || '',
      email: currentUser?.email || '',
      groups: currentUser?.groups || '',
      phoneNumber: currentUser?.phoneNumber || '',
      birthday: currentUser?.birthday || undefined,
      age: currentUser?.age || '',
      gender: currentUser?.gender || 2,
      isActive: currentUser?.isActive || false,
      role: currentUser?.role || [],
    }),
    [currentUser],
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
    if ((mode === 'view' || mode === 'edit') && currentUser) {
      reset(defaultValues);
    }
    if (!(mode === 'new')) {
      reset(defaultValues);
    }
  }, [mode, currentUser]);

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

export default useFormUser;
