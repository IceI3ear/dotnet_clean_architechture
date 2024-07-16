import React from 'react';
import { Button, Drawer, Form, Input } from 'antd';
import { Controller } from 'react-hook-form';
import DrawerTitle from '../../../components/drawer-title';
import useFormSubject from '../hooks/use-form-subject';
import { ModeType, SubjectDataType } from '../../../types/user';

interface SubjectFormDrawerProps {
  open: boolean;
  data: SubjectDataType | null;
  mode: ModeType;
  onClose: () => void;
}

const SubjectFormDrawer: React.FC<SubjectFormDrawerProps> = ({
  open,
  mode,
  data,
  onClose,
}) => {
  const {
    formState: { errors },
    func: { control, onSubmit },
  } = useFormSubject(mode, data);

  return (
    <Drawer
      title={
        <DrawerTitle
          title={
            mode === 'new'
              ? 'Create subject'
              : mode === 'edit'
                ? 'Update subject'
                : 'View subject detail'
          }
          onClose={onClose}
        />
      }
      placement="right"
      closable={false}
      onClose={onClose}
      open={open}
      key="create-subject-drawer"
      width={600}
    >
      <Form
        layout="vertical"
        onFinish={onSubmit}
      >
        <Form.Item
          label={
            <>
              Subject name <span style={{ color: 'red' }}>*</span>
            </>
          }
          validateStatus={errors.name ? 'error' : ''}
          help={errors.name?.message}
        >
          <Controller
            disabled={mode === 'view'}
            name="name"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                placeholder="Subject name"
              />
            )}
          />
        </Form.Item>

        <Form.Item>
          <Button
            disabled={mode === 'view'}
            type="primary"
            htmlType="submit"
          >
            {mode === 'new' ? 'Submit' : 'Update'}
          </Button>
        </Form.Item>
      </Form>
    </Drawer>
  );
};

export default SubjectFormDrawer;
