import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useDispatch } from '../../../redux/store';
import { loginUser } from '../../../redux/slice/authSlice';
import { paths } from '../../../path';

interface FormValuesLogin {
  email: string;
  password: string;
  remember?: boolean;
}

const validationSchema = yup.object().shape({
  email: yup
    .string()
    .email('You have entered an invalid email address. Please try again.')
    .required('Email is required.'),
  password: yup.string().required('Password is required.'),
  remember: yup.boolean(),
});

const useLogin = () => {
  const navigate = useNavigate();

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValuesLogin>({ resolver: yupResolver(validationSchema) });

  const dispatch = useDispatch();

  const handleLogin = async (loginInfor: FormValuesLogin) => {
    await dispatch(loginUser(loginInfor.email, loginInfor.password));
    navigate(`${paths.user}`);
  };

  useEffect(() => {
    const enterKeyListener = (e: KeyboardEvent) => {
      if (e.code === 'Enter' || e.code === 'NumpadEnter') {
        handleSubmit(handleLogin);
      }
    };
    document.addEventListener('keydown', enterKeyListener);

    return () => {
      document.removeEventListener('keydown', enterKeyListener);
    };
  }, []);

  return {
    formState: {
      errors,
    },
    func: {
      control,
      handleSubmit,
      onSubmit: handleSubmit(handleLogin),
    },
  };
};

export default useLogin;
