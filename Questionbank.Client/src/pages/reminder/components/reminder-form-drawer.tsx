import React from 'react';
import { Button, Drawer, Form, Input } from 'antd';
import { Controller } from 'react-hook-form';
import DrawerTitle from '../../../components/drawer-title';
import useFormReminder from '../hooks/use-form-reminder';
import { ModeType, ReminderDataType } from '../../../types/user';

interface ReminderFormDrawerProps {
  open: boolean;
  data: ReminderDataType | null;
  mode: ModeType;
  onClose: () => void;
}

const ReminderFormDrawer: React.FC<ReminderFormDrawerProps> = ({
  open,
  mode,
  data,
  onClose,
}) => {
  const {
    formState: { errors },
    func: { control, onSubmit },
  } = useFormReminder(mode, data);

  return (
    <Drawer
      title={
        <DrawerTitle
          title={
            mode === 'new'
              ? 'Create reminder'
              : mode === 'edit'
                ? 'Update reminder'
                : 'View reminder detail'
          }
          onClose={onClose}
        />
      }
      placement="right"
      closable={false}
      onClose={onClose}
      open={open}
      key="create-reminder-drawer"
      width={600}
    >
      <Form
        layout="vertical"
        onFinish={onSubmit}
      >
        <Form.Item
          label={
            <>
              Name <span style={{ color: 'red' }}>*</span>
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
                placeholder="Reminder name"
              />
            )}
          />
        </Form.Item>

        <Form.Item
          label="Description"
          validateStatus={errors.description ? 'error' : ''}
          help={errors.description?.message}
        >
          <Controller
            disabled={mode === 'view'}
            name="description"
            control={control}
            render={({ field }) => (
              <Input.TextArea
                {...field}
                placeholder="Description"
                rows={6}
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

export default ReminderFormDrawer;
