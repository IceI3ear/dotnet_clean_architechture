import React, { ReactNode } from 'react';
import { Grid, Layout, theme } from 'antd';
import SideNav from './components/side-nav';
import TopNav from './components/top-nav';
const { useBreakpoint } = Grid;

const { Header, Content, Footer, Sider } = Layout;

interface DashBoardLayoutProps {
  children: ReactNode;
}

const DashBoardLayout: React.FC<DashBoardLayoutProps> = (props) => {
  const { children } = props;
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  const screens = useBreakpoint();

  const widthPx = screens.lg ? '256px' : '80px';

  return (
    <Layout hasSider>
      <Sider
        width={widthPx}
        breakpoint="lg"
        collapsedWidth={screens.lg ? 0 : 80}
        style={{
          overflow: 'auto',
          minHeight: '100vh',
          position: 'fixed',
          left: 0,
          top: 0,
          bottom: 0,
        }}
        className="hidden-scrollbar"
      >
        <SideNav />
      </Sider>
      <Layout
        style={{
          marginLeft: widthPx,
        }}
      >
        <Header
          style={{
            padding: '8px 15px',
            background: colorBgContainer,
            height: 'auto',
          }}
        >
          <TopNav />
        </Header>
        <Content
          style={{
            margin: '24px 16px 0',
            overflow: 'initial',
          }}
        >
          <div
            style={{
              padding: 24,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
              minHeight: 'calc(100vh - 150px)',
            }}
          >
            {children}
          </div>
        </Content>
        <Footer
          style={{
            textAlign: 'center',
          }}
        >
          Cyberschool Question Bank ©{new Date().getFullYear()} Created by BRSS
        </Footer>
      </Layout>
    </Layout>
  );
};

export default DashBoardLayout;
