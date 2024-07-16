import { FC } from 'react';
import { useSelector } from '../redux/store';
import { useNavigate } from 'react-router-dom';
import { paths } from '../path';
import { Flex, Spin } from 'antd';

interface ChildrenProps {
  children?: React.ReactNode;
}

const AuthGuard: FC<ChildrenProps> = ({ children }) => {
  const { isInittialized, isAuthenticated } = useSelector(
    (state) => state.auth,
  );
  const navigate = useNavigate();

  if (!isInittialized) {
    return (
      <Flex
        justify="center"
        align="center"
        vertical={true}
        gap={100}
        style={{ height: '100vh', width: '100wh' }}
      >
        <Spin size="large" />
      </Flex>
    );
  }

  if (!isAuthenticated) {
    navigate(`${paths.auth.login}`);
  }

  return <>{children}</>;
};

export default AuthGuard;
