import { FC } from 'react';
import { Seo } from '../../components/seo';
import { useSelector } from '../../redux/store';
import { Button, Flex } from 'antd';
import ReminderFormDrawer from './components/reminder-form-drawer';
import { PlusOutlined } from '@ant-design/icons';
import ReminderTable from './components/reminder-table';
import useReminder from './hooks/use-reminder';

const ReminderPage: FC = () => {
  const { remindersData } = useSelector((state) => state.user);
  const {
    mode,
    currentData,
    handleOpen,
    handleClose,
    handleEdit,
    handleView,
    handleDelete,
  } = useReminder();

  return (
    <>
      <Seo title="Reminder" />

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
          Create reminder
        </Button>
      </Flex>
      <ReminderTable
        data={remindersData || []}
        handleEdit={handleEdit}
        handleView={handleView}
        handleDelete={handleDelete}
      />

      <ReminderFormDrawer
        open={mode !== ''}
        mode={mode !== '' ? mode : 'new'}
        data={currentData || null}
        onClose={handleClose}
      />
    </>
  );
};

export default ReminderPage;
