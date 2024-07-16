import { Layout } from 'antd';
import { Content, Footer } from 'antd/es/layout/layout';
import { FC, ReactNode } from 'react';

interface AuthLayoutProps {
  children: ReactNode;
}

const AuthLayout: FC<AuthLayoutProps> = (props) => {
  const { children } = props;
  return (
    <Layout
      style={{
        display: 'flex',
        justifyContent: 'flex-start',
        alignItems: 'center',
        width: '100wh',
        height: '100vh',
        backgroundImage:
          'url(https://gw.alipayobjects.com/zos/rmsportal/TVYTbAXWheQpRcWDaDMu.svg)',
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'center 110px',
        backgroundSize: '100%',
      }}
    >
      <Content>{children}</Content>

      <Footer
        style={{
          textAlign: 'center',
        }}
      >
        Cyberschool Question Bank ©{new Date().getFullYear()} Created by BRSS
      </Footer>
    </Layout>
  );
};

export default AuthLayout;
