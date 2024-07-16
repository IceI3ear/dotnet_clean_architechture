import { lazy, Suspense } from 'react';
import type { RouteObject } from 'react-router';
import { Outlet } from 'react-router-dom';
import DashBoardLayout from '../layouts/dashboard/index';
import AuthGuard from '../guards/auth-guard';
import { paths } from '../path';
import RoleBasedGuard from '../guards/role-based-guard';

const UserPage = lazy(() => import('../pages/user'));
const GroupPage = lazy(() => import('../pages/group'));
const SubjectPage = lazy(() => import('../pages/subject'));
const ReminderPage = lazy(() => import('../pages/reminder'));

export const dashboardRoutes: RouteObject[] = [
  {
    element: (
      <AuthGuard>
        <DashBoardLayout>
          <Suspense>
            <Outlet />
          </Suspense>
        </DashBoardLayout>
      </AuthGuard>
    ),
    children: [
      {
        index: true,
        element: (
          <RoleBasedGuard accessibleRoles={['Admin']}>
            <UserPage />
          </RoleBasedGuard>
        ),
      },
      {
        path: paths.group,
        element: (
          <RoleBasedGuard accessibleRoles={['Admin']}>
            <GroupPage />,
          </RoleBasedGuard>
        ),
      },
      {
        path: paths.subjects,
        element: (
          <RoleBasedGuard accessibleRoles={['Admin']}>
            <SubjectPage />,
          </RoleBasedGuard>
        ),
      },
      {
        path: paths.reminder,
        element: (
          <RoleBasedGuard accessibleRoles={['Admin']}>
            <ReminderPage />
          </RoleBasedGuard>
        ),
      },
      {
        path: paths.questions,
        element: (
          <RoleBasedGuard accessibleRoles={['Admin']}>
            <UserPage />
          </RoleBasedGuard>
        ),
      },
      {
        path: paths.taskAssignment,
        element: (
          <RoleBasedGuard accessibleRoles={['Admin']}>
            <UserPage />
          </RoleBasedGuard>
        ),
      },
    ],
  },
];

// https://fullstack.edu.vn/blog/authentication-authorization-trong-reactjs.htmls
