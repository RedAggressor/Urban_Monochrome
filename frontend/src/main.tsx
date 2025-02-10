import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { App } from './app/App';
import store from './app/store';
import './app/styles/main.scss';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement,
);

root.render(
  <Provider store={store}>
    {/* бо стікі хедер сам по собі не займає місця на сторінці, в класах я цьому елементу задав висоту 154пкс */}
    <div className="header-placeholder" />
    <App />
  </Provider>,
);
