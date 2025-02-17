import { createBrowserRouter } from 'react-router-dom';
import { HomePage } from '../../pages/HomePage/HomePage';
import { BaseLayout } from '../../shared/ui-kit/Layouts/BaseLayout';
import { Header } from '../../widgets/Header/Header';
import { Footer } from '../../widgets/Footer/Footer';
import { ROUTES } from '../../shared/ui-kit/Layouts/routes/routes';
import { CatalogPage } from '../../pages/CatalogPage/CatalogPage';

export const router = createBrowserRouter([
  {
    element: <BaseLayout headerSlot={<Header />} footerSlot={<Footer />} />,
    children: [
      {
        path: ROUTES.ROOT,
        element: <HomePage />,
      },
      {
        path: ROUTES.CATALOG,
        element: <CatalogPage />,
      },
    ],
  },
]);
