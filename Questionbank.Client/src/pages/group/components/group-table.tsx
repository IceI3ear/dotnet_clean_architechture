import { FC } from 'react';
import { Space, Table, TableProps } from 'antd';
import { EditOutlined, DeleteOutlined, EyeOutlined } from '@ant-design/icons';
import { GroupDataType } from '../../../types/user';
import { useSelector } from '../../../redux/store';

interface GroupTableProps {
  data: GroupDataType[];
  handleEdit: (data: GroupDataType) => void;
  handleView: (data: GroupDataType) => void;
  handleDelete: () => void;
}

const GroupTable: FC<GroupTableProps> = (props) => {
  const { data, handleEdit, handleView, handleDelete } = props;
  const { isLoading } = useSelector((state) => state.user);

  const columns: TableProps<GroupDataType>['columns'] = [
    {
      title: 'Row',
      dataIndex: 'key',
      rowScope: 'row',
      width: '10px',
    },
    {
      title: 'Group name',
      dataIndex: 'name',
      render: (text) => <a>{text}</a>,
    },
    {
      title: 'Description',
      dataIndex: 'description',
    },
    {
      title: 'Actions',
      dataIndex: '',
      width: 215,
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

export default GroupTable;
