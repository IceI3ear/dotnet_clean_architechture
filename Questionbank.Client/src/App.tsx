import { useEffect, useState } from 'react';
import { useRoutes } from 'react-router-dom';
import { useDispatch } from './redux/store';
import { initializeState } from './redux/slice/authSlice';
import { routes } from './router';
import { Flex, Spin } from 'antd';
import preUniLogo from './assets/pre-uni-logo.png';

export default function App() {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const dispatch = useDispatch();

  useEffect(() => {
    setIsLoading(true);
    dispatch(initializeState());
    setTimeout(() => {
      setIsLoading(false);
    }, 1000);
  }, [dispatch]);

  const element = useRoutes(routes);

  return (
    <>
      {isLoading ? (
        <Flex
          justify="center"
          align="center"
          vertical={true}
          gap={100}
          style={{ height: '100vh', width: '100wh' }}
        >
          <img
            src={preUniLogo}
            width="320px"
            alt=""
          />
          <Spin size="large" />
        </Flex>
      ) : (
        element
      )}
    </>
  );
}
