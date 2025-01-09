import { useEffect } from 'react';
import { RouterProvider } from 'react-router-dom';
import { debounce } from 'lodash';
import { router } from './router/router';
import { useAppDispatch } from '../shared/hooks';
import { setScreenWidth } from '../features/globalSlice';

export const App = () => {
  const dispatch = useAppDispatch();
  // переобчислення розміру вікна кожен раз коли воно змінюється
  useEffect(() => {
    // дебаунс робить затримки (200 мс) перед кожним викликом функції, щоб не перегружати обчислення
    const handleResize = debounce(() => {
      dispatch(setScreenWidth(window.innerWidth));
    }, 200);

    window.addEventListener('resize', handleResize);

    return () => window.removeEventListener('resize', handleResize);
  }, [dispatch]);

  return <RouterProvider router={router} />;
};
