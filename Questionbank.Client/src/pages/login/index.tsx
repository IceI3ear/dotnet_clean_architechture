import { Controller } from 'react-hook-form';
import { Form, Input, Button, Checkbox, Typography, Flex, Grid } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';

const { Text } = Typography;
const { useBreakpoint } = Grid;

import preUniLogo from '../../assets/pre-uni-logo.png';
import useLogin from './hooks/use-login';

const LoginPage = () => {
  const {
    formState: { errors },
    func: { control, onSubmit },
  } = useLogin();

  const screens = useBreakpoint();

  const styles = {
    defaultWith: {
      width: '95%',
      maxWidth: screens.md ? '400px' : '328px',
      margin: '0 auto',
    },
    input: {
      padding: '8px 12px',
      fontSize: '16px',
    },
  };

  return (
    <>
      <Flex
        vertical={true}
        justify="center"
        align="center"
        style={{ margin: '32px 0' }}
      >
        <img
          src={preUniLogo}
          alt=""
          style={styles.defaultWith}
        />
        <Text style={{ fontSize: '16px', marginBottom: '30px' }}>
          Welcome to Cyberschool
        </Text>
      </Flex>
      <div style={styles.defaultWith}>
        <Form
          onFinish={onSubmit}
          className="login-form"
        >
          <Form.Item
            validateStatus={errors.email ? 'error' : ''}
            help={errors.email ? errors.email.message : ''}
          >
            <Controller
              name="email"
              control={control}
              defaultValue=""
              render={({ field }) => (
                <Input
                  {...field}
                  prefix={<UserOutlined style={{ fontSize: 16 }} />}
                  placeholder="Email"
                  style={styles.input}
                />
              )}
            />
          </Form.Item>
          <Form.Item
            validateStatus={errors.password ? 'error' : ''}
            help={errors.password ? errors.password.message : ''}
          >
            <Controller
              name="password"
              control={control}
              defaultValue=""
              render={({ field }) => (
                <Input.Password
                  {...field}
                  prefix={<LockOutlined style={{ fontSize: 16 }} />}
                  type="password"
                  placeholder="Password"
                  style={styles.input}
                />
              )}
            />
          </Form.Item>
          <Form.Item>
            <Controller
              name="remember"
              control={control}
              defaultValue={true}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value}
                  style={{ fontSize: 16 }}
                >
                  Remember me
                </Checkbox>
              )}
            />
            <a
              style={{ float: 'right', fontSize: 16, marginTop: '5px' }}
              href=""
            >
              Forgot password
            </a>
          </Form.Item>

          <Button
            type="primary"
            htmlType="submit"
            className="login-form-button"
            style={{
              width: '100%',
              padding: '8px 15px',
              height: '40px',
              fontSize: '16px',
            }}
          >
            Log in
          </Button>
          {/* Or <a href="">register now!</a> */}
        </Form>
      </div>
    </>
  );
};

export default LoginPage;
