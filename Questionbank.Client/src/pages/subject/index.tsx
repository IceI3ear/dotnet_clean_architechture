import { FC } from 'react';
import { Seo } from '../../components/seo';
import { useSelector } from '../../redux/store';
import { Button, Flex } from 'antd';
import SubjectFormDrawer from '././components/user-form-drawer';
import { PlusOutlined } from '@ant-design/icons';
import SubjectTable from './components/user-table';
import useSubject from './hooks/use-subject';

const SubjectPage: FC = () => {
  const { subjectsData } = useSelector((state) => state.user);
  const {
    mode,
    currentData,
    handleOpen,
    handleClose,
    handleEdit,
    handleView,
    handleDelete,
  } = useSubject();

  return (
    <>
      <Seo title="Subject" />

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
          Create subject
        </Button>
      </Flex>
      <SubjectTable
        data={subjectsData || []}
        handleEdit={handleEdit}
        handleView={handleView}
        handleDelete={handleDelete}
      />

      <SubjectFormDrawer
        open={mode !== ''}
        mode={mode !== '' ? mode : 'new'}
        data={currentData || null}
        onClose={handleClose}
      />
    </>
  );
};

export default SubjectPage;
