import { FC } from 'react';
import { Seo } from '../../components/seo';
import { useSelector } from '../../redux/store';
import { Button, Flex } from 'antd';
import UserFormDrawer from '././components/user-form-drawer';
import { PlusOutlined } from '@ant-design/icons';
import UserTable from './components/user-table';
import useUser from './hooks/use-user';

const UserPage: FC = () => {
  const { usersData } = useSelector((state) => state.user);
  const {
    mode,
    currentData,
    handleOpen,
    handleClose,
    handleEdit,
    handleView,
    handleDelete,
  } = useUser();

  return (
    <>
      <Seo title="User" />

      <Flex
        align="center"
        justify="end"
        gap={20}
        style={{ marginBottom: '24px' }}
      >
        <Button
          onClick={() => handleOpen('new')}
          type="primary"
          icon={<PlusOutlined />}
          style={{ padding: '8px 12px', height: '40px', fontSize: '16px' }}
        >
          Create user
        </Button>
      </Flex>
      <UserTable
        data={usersData || []}
        handleEdit={handleEdit}
        handleView={handleView}
        handleDelete={handleDelete}
      />

      <UserFormDrawer
        open={mode !== ''}
        mode={mode !== '' ? mode : 'new'}
        data={currentData || null}
        onClose={handleClose}
      />
    </>
  );
};

export default UserPage;
