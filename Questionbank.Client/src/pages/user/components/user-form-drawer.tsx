import React from 'react';
import {
  Button,
  Col,
  DatePicker,
  Drawer,
  Form,
  Input,
  Radio,
  Row,
  Select,
} from 'antd';
import { Controller } from 'react-hook-form';
import dayjs from 'dayjs';
import DrawerTitle from '../../../components/drawer-title';
import useFormUser from '../hooks/use-form-user';
import { ModeType, UserDataType } from '../../../types/user';

const { Option } = Select;

interface UserFormDrawerProps {
  open: boolean;
  data: UserDataType | null;
  mode: ModeType;
  onClose: () => void;
}

const UserFormDrawer: React.FC<UserFormDrawerProps> = ({
  open,
  mode,
  data,
  onClose,
}) => {
  const {
    formState: { errors },
    func: { control, onSubmit },
  } = useFormUser(mode, data);

  return (
    <Drawer
      title={
        <DrawerTitle
          title={
            mode === 'new'
              ? 'Create user'
              : mode === 'edit'
                ? 'Update user'
                : 'View user detail'
          }
          onClose={onClose}
        />
      }
      placement="right"
      closable={false}
      onClose={onClose}
      open={open}
      key="create-user-drawer"
      width={600}
    >
      <Form
        layout="vertical"
        onFinish={onSubmit}
      >
        <Row gutter={16}>
          <Col
            xs={24}
            md={12}
          >
            <Form.Item
              label={
                <>
                  First name <span style={{ color: 'red' }}>*</span>
                </>
              }
              validateStatus={errors.firstName ? 'error' : ''}
              help={errors.firstName?.message}
            >
              <Controller
                disabled={mode === 'view'}
                name="firstName"
                control={control}
                render={({ field }) => (
                  <Input
                    {...field}
                    placeholder="First name"
                  />
                )}
              />
            </Form.Item>
          </Col>

          <Col
            xs={24}
            md={12}
          >
            <Form.Item
              label={
                <>
                  Last name <span style={{ color: 'red' }}>*</span>
                </>
              }
              validateStatus={errors.lastName ? 'error' : ''}
              help={errors.lastName?.message}
            >
              <Controller
                disabled={mode === 'view'}
                name="lastName"
                control={control}
                render={({ field }) => (
                  <Input
                    {...field}
                    placeholder="Last name"
                  />
                )}
              />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item
          label={
            <>
              Email <span style={{ color: 'red' }}>*</span>
            </>
          }
          validateStatus={errors.email ? 'error' : ''}
          help={errors.email?.message}
        >
          <Controller
            disabled={mode === 'view'}
            name="email"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                placeholder="Email"
                type="email"
              />
            )}
          />
        </Form.Item>

        <Form.Item
          label="Groups"
          validateStatus={errors.groups ? 'error' : ''}
          help={errors.groups?.message}
        >
          <Controller
            disabled={mode === 'view'}
            name="groups"
            control={control}
            render={({ field }) => (
              <Select
                {...field}
                showSearch
                placeholder="Search to Select"
                optionFilterProp="label"
                filterSort={(optionA, optionB) =>
                  (optionA?.label ?? '')
                    .toLowerCase()
                    .localeCompare((optionB?.label ?? '').toLowerCase())
                }
                options={[
                  { value: '1', label: 'Not Identified' },
                  { value: '2', label: 'Closed' },
                  { value: '3', label: 'Communicated' },
                  { value: '4', label: 'Identified' },
                  { value: '5', label: 'Resolved' },
                  { value: '6', label: 'Cancelled' },
                ]}
              />
            )}
          />
        </Form.Item>

        <Row gutter={16}>
          <Col
            xs={24}
            md={12}
          >
            <Form.Item
              label="Phone number"
              validateStatus={errors.phoneNumber ? 'error' : ''}
              help={errors.phoneNumber?.message}
            >
              <Controller
                disabled={mode === 'view'}
                name="phoneNumber"
                control={control}
                render={({ field }) => (
                  <Input
                    {...field}
                    placeholder="Phone number"
                  />
                )}
              />
            </Form.Item>
          </Col>

          <Col
            xs={24}
            md={12}
          >
            <Form.Item label="Birthday">
              <Controller
                disabled={mode === 'view'}
                name="birthday"
                control={control}
                render={({ field }) => (
                  <DatePicker
                    {...field}
                    style={{ width: '100%' }}
                    value={field.value ? dayjs(field.value) : null}
                    onChange={(date) =>
                      field.onChange(date ? date.toDate() : null)
                    }
                  />
                )}
              />
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col
            xs={24}
            md={12}
          >
            <Form.Item
              label="Age"
              validateStatus={errors.age ? 'error' : ''}
              help={errors.age?.message}
            >
              <Controller
                disabled={mode === 'view'}
                name="age"
                control={control}
                render={({ field }) => (
                  <Input
                    {...field}
                    placeholder="Age"
                  />
                )}
              />
            </Form.Item>
          </Col>

          <Col
            xs={24}
            md={12}
          >
            <Form.Item
              label="Gender"
              validateStatus={errors.gender ? 'error' : ''}
              help={errors.gender?.message}
            >
              <Controller
                disabled={mode === 'view'}
                name="gender"
                control={control}
                render={({ field }) => (
                  <Select
                    {...field}
                    placeholder="Gender"
                    allowClear
                  >
                    <Option value={0}>Male</Option>
                    <Option value={1}>Female</Option>
                    <Option value={2}>Other</Option>
                  </Select>
                )}
              />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item label="Status">
          <Controller
            disabled={mode === 'view'}
            name="isActive"
            control={control}
            render={({ field }) => (
              <Radio.Group {...field}>
                <Radio value={true}>Active</Radio>
                <Radio value={false}>Inactive</Radio>
              </Radio.Group>
            )}
          />
        </Form.Item>

        <Form.Item
          label={
            <>
              Role <span style={{ color: 'red' }}>*</span>
            </>
          }
          validateStatus={errors.role ? 'error' : ''}
          help={errors.role?.message}
        >
          <Controller
            disabled={mode === 'view'}
            name="role"
            control={control}
            render={({ field }) => (
              <Select
                {...field}
                mode="multiple"
                allowClear
                style={{ width: '100%' }}
                placeholder="Please select"
                onChange={(value) => field.onChange(value)}
                options={[
                  { label: 'Admin', value: 'admin' },
                  { label: 'Question creator', value: 'questionCreator' },
                  { label: 'Proofreader', value: 'proofreader' },
                ]}
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

export default UserFormDrawer;
