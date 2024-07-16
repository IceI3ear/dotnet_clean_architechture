import { FC } from 'react';
import { Seo } from '../../components/seo';
import { useSelector } from '../../redux/store';
import { Button, Flex } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import useGroup from './hooks/use-group';
import GroupFormDrawer from './components/group-form-drawer';
import GroupTable from './components/group-table';

const UserPage: FC = () => {
  const { groupsData } = useSelector((state) => state.user);
  const {
    mode,
    currentData,
    handleOpen,
    handleClose,
    handleEdit,
    handleView,
    handleDelete,
  } = useGroup();

  return (
    <>
      <Seo title="Group" />

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
          Create group
        </Button>
      </Flex>
      <GroupTable
        data={groupsData || []}
        handleEdit={handleEdit}
        handleView={handleView}
        handleDelete={handleDelete}
      />

      <GroupFormDrawer
        open={mode !== ''}
        mode={mode || 'new'}
        data={currentData || null}
        onClose={handleClose}
      />
    </>
  );
};

export default UserPage;
