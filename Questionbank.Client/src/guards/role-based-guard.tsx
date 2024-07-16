import { FC, ReactNode } from 'react';
import { useSelector } from '../redux/store';
import { Layout, Typography } from 'antd';

export interface RoleBasedGuardProps {
  accessibleRoles: string[];
  children: ReactNode;
}
const RoleBasedGuard: FC<RoleBasedGuardProps> = ({
  children,
  accessibleRoles,
}) => {
  const { user } = useSelector((state) => state.auth);

  const hasAccess = user?.roles.some((role) => accessibleRoles.includes(role));

  if (!hasAccess) {
    return (
      <Layout
        style={{
          height: '100vh',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
        }}
      >
        <Typography>Permission Denied</Typography>
        <Typography>You do not have permission to access this page</Typography>
      </Layout>
    );
  }

  return <>{children}</>;
};
export default RoleBasedGuard;
