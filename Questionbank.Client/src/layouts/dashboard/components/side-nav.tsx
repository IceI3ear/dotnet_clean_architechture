import React, { useEffect, useState } from 'react';
import {
  UserOutlined,
  TeamOutlined,
  ProductOutlined,
  QuestionOutlined,
  UserAddOutlined,
  NotificationOutlined,
} from '@ant-design/icons';
import { Image, Menu } from 'antd';
import type { MenuProps } from 'antd';
import preNewcollegeLogo from '../../../assets/pre-newcollege-logo.png';
import { paths } from '../../../path';
import { useLocation, useNavigate } from 'react-router-dom';
import { useSelector } from '../../../redux/store';

type MenuItem = Required<MenuProps>['items'][number];

interface CustomMenuItem {
  key: string;
  label: string;
  icon: React.ReactNode;
  roles: string[];
}

const menuItems: CustomMenuItem[] = [
  {
    key: paths.user,
    label: 'User',
    icon: <UserOutlined />,
    roles: ['Admin', 'ProofReader', 'QuestionCreator'],
  },
  {
    key: paths.group,
    label: 'Group',
    icon: <TeamOutlined />,
    roles: ['Admin', 'ProofReader', 'QuestionCreator'],
  },
  {
    key: paths.subjects,
    label: 'Subjects',
    icon: <ProductOutlined />,
    roles: ['Admin', 'ProofReader', 'QuestionCreator'],
  },
  {
    key: paths.reminder,
    label: 'Reminder',
    icon: <NotificationOutlined />,
    roles: ['Admin', 'ProofReader', 'QuestionCreator'],
  },
  {
    key: paths.questions,
    label: 'Questions',
    icon: <QuestionOutlined />,
    roles: ['Admin', 'ProofReader', 'QuestionCreator'],
  },
  {
    key: paths.taskAssignment,
    label: 'Task Assignment',
    icon: <UserAddOutlined />,
    roles: ['Admin', 'ProofReader', 'QuestionCreator'],
  },
];

const buildMenuItems = (
  items: CustomMenuItem[],
  userRoles: string[],
): MenuItem[] => {
  return items
    .filter((item) => item.roles.some((role) => userRoles.includes(role)))
    .map((item) => ({
      key: item.key,
      label: item.label,
      icon: item.icon,
    }));
};

const SideNav: React.FC = () => {
  const [current, setCurrent] = useState('');
  const navigate = useNavigate();
  const location = useLocation();
  const { user } = useSelector((state) => state.auth); // Giả sử bạn có user trong state

  const onClick: MenuProps['onClick'] = (e) => {
    if (e.key !== current) {
      setCurrent(e.key);
    }
  };

  useEffect(() => {
    if (current !== '') {
      navigate(`${current}`);
    }
  }, [current]);

  useEffect(() => {
    setCurrent(location.pathname);
  }, []);

  const filteredItems = user ? buildMenuItems(menuItems, user.roles) : [];

  return (
    <>
      <div style={{ padding: '1rem' }}>
        <Image src={preNewcollegeLogo} />
      </div>
      <Menu
        defaultSelectedKeys={['1']}
        defaultOpenKeys={['sub1']}
        onClick={onClick}
        selectedKeys={[current]}
        mode="inline"
        theme="dark"
        items={filteredItems} // Sử dụng danh sách items đã được lọc
      />
    </>
  );
};

export default SideNav;
