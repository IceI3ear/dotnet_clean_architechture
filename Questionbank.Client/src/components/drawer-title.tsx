import { FC } from 'react';
import { Button } from 'antd';
import { CloseOutlined } from '@ant-design/icons';

interface DrawerTitleProps {
  title: string;
  onClose: () => void;
}

const DrawerTitle: FC<DrawerTitleProps> = (props) => {
  const { title, onClose } = props;
  return (
    <div
      style={{
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        width: '100%',
      }}
    >
      <div>{title}</div>
      <Button
        type="text"
        icon={<CloseOutlined />}
        onClick={onClose}
      />
    </div>
  );
};

export default DrawerTitle;
