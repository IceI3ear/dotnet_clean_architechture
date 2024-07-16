import { FC } from 'react';
import { Space, Table, TableProps } from 'antd';
import { EditOutlined, DeleteOutlined, EyeOutlined } from '@ant-design/icons';
import { ReminderDataType } from '../../../types/user';
import { useSelector } from '../../../redux/store';

interface ReminderTableProps {
  data: ReminderDataType[];
  handleEdit: (data: ReminderDataType) => void;
  handleView: (data: ReminderDataType) => void;
  handleDelete: () => void;
}

const ReminderTable: FC<ReminderTableProps> = (props) => {
  const { data, handleEdit, handleView, handleDelete } = props;
  const { isLoading } = useSelector((state) => state.user);

  const columns: TableProps<any>['columns'] = [
    {
      title: 'Row',
      dataIndex: 'id',
      rowScope: 'row',
      width: '10px',
    },
    {
      title: 'Name',
      dataIndex: 'name',
      width: 150,
    },
    {
      title: 'Description',
      dataIndex: 'description',
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

export default ReminderTable;
