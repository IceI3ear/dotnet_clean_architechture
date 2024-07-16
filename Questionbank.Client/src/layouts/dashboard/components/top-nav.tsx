import React from 'react';
import { UserOutlined, LogoutOutlined } from '@ant-design/icons';
import { Avatar, Button, Divider, Flex, Popover, Typography } from 'antd';
import { useDispatch, useSelector } from '../../../redux/store';
import { logoutUser } from '../../../redux/slice/authSlice';
import { useNavigate } from 'react-router-dom';
import { paths } from '../../../path';

const TopNav: React.FC = () => {
  const { user } = useSelector((state) => state.auth);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleLogOut = () => {
    dispatch(logoutUser());
    navigate(`${paths.auth.login}`);
  };

  return (
    <Flex
      gap="middle"
      align="end"
      vertical
    >
      <Popover
        content={() =>
          user && (
            <div>
              {user.userName && (
                <Typography style={{ fontSize: '1rem' }}>
                  {user.userName}
                </Typography>
              )}
              <Typography style={{ fontSize: '1rem' }}>{user.email}</Typography>
              <Divider style={{ margin: '8px 0' }} />
              <Flex
                justify="space-between"
                align="center"
                vertical={true}
              >
                <Button
                  type="text"
                  block
                  icon={<LogoutOutlined />}
                  style={{ fontSize: '1rem' }}
                  onClick={handleLogOut}
                >
                  Log out
                </Button>
              </Flex>
            </div>
          )
        }
        trigger="click"
      >
        <Avatar
          size="large"
          icon={<UserOutlined />}
          style={{ cursor: 'pointer' }}
        />
      </Popover>
    </Flex>
  );
};

export default TopNav;
