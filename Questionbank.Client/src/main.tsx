import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { HelmetProvider } from 'react-helmet-async';
import { Provider as ReduxProvider } from 'react-redux';

import './index.css';
import App from './App.tsx';
import { Suspense } from 'react';
import { store } from './redux/store/index.ts';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <HelmetProvider>
    <ReduxProvider store={store}>
      <BrowserRouter>
        <Suspense>
          <App />
        </Suspense>
      </BrowserRouter>
    </ReduxProvider>
  </HelmetProvider>,
);
