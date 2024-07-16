import { FC } from 'react';
import { Space, Table, TableProps } from 'antd';
import { EditOutlined, DeleteOutlined, EyeOutlined } from '@ant-design/icons';
import { UserDataType } from '../../../types/user';
import { useSelector } from '../../../redux/store';

interface UserTableProps {
  data: UserDataType[];
  handleEdit: (data: UserDataType) => void;
  handleView: (data: UserDataType) => void;
  handleDelete: () => void;
}

const UserTable: FC<UserTableProps> = (props) => {
  const { data, handleEdit, handleView, handleDelete } = props;
  const { isLoading } = useSelector((state) => state.user);

  const columns: TableProps<any>['columns'] = [
    {
      title: 'Row',
      dataIndex: 'key',
      rowScope: 'row',
      width: '10px',
    },
    {
      title: 'Name',
      dataIndex: 'name',
      width: 150,
      render: (_, record) => (
        <>
          {record.firstName} {record.lastName}
        </>
      ),
    },
    {
      title: 'Email',
      dataIndex: 'email',
    },
    {
      title: 'Phone number',
      dataIndex: 'phoneNumber',
      width: 160,
    },
    {
      title: 'Roles',
      dataIndex: 'role',
      width: 160,
    },
    {
      title: 'Gender',
      dataIndex: 'gender',
    },
    {
      title: 'Status',
      dataIndex: 'status',
    },
    {
      title: 'Groups',
      dataIndex: 'group',
      width: 195,
    },
    {
      title: 'Actions',
      dataIndex: '',
      width: 395,
      render: (_, record) => (
        <Space size="middle">
          <a onClick={() => handleView(record)}>
            <EyeOutlined /> View
          </a>
          <a onClick={() => handleEdit(record)}>
            <EditOutlined /> Edit
          </a>
          <a
            style={{ color: 'red' }}
            onClick={() => handleDelete()}
          >
            <DeleteOutlined /> Delete
          </a>
        </Space>
      ),
    },
  ];

  return (
    <Table
      columns={columns}
      dataSource={data}
      bordered
      loading={isLoading}
      scroll={{ x: 800 }}
    />
  );
};

export default UserTable;
