import { FC, useEffect } from 'react';
import { useSelector } from '../redux/store';
import { useNavigate } from 'react-router-dom';
import { paths } from '../path';
import { Flex, Spin } from 'antd';

interface ChildrenProps {
  children?: React.ReactNode;
}

const GuestGuard: FC<ChildrenProps> = ({ children }) => {
  const navigate = useNavigate();
  const { isInittialized, isAuthenticated } = useSelector(
    (state) => state.auth,
  );

  useEffect(() => {
    if (isAuthenticated) {
      navigate(`${paths.user}`);
    }
  }, [isAuthenticated, navigate]);

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

  return <>{children}</>;
};

export default GuestGuard;
