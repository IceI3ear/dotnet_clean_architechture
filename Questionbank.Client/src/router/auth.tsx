import { lazy } from 'react';
import type { RouteObject } from 'react-router';
import { Outlet } from 'react-router-dom';
import AuthLayout from '../layouts/auth';
import GuestGuard from '../guards/guest-guard';
// JWT
const LoginPage = lazy(() => import('../pages/login'));

export const authRoutes: RouteObject[] = [
  {
    element: (
      <GuestGuard>
        <AuthLayout>
          <Outlet />
        </AuthLayout>
      </GuestGuard>
    ),
    children: [
      {
        path: 'login',
        element: <LoginPage />,
      },
      // {
      //   path: 'register',
      //   element: <JwtRegisterPage />,
      // },
    ],
  },
];
