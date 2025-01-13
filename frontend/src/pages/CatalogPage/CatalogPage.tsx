import { CatalogBanner } from '../../widgets/CatalogBanner/CatalogBanner';
import cl from './CatalogPage.module.scss';

export const CatalogPage = () => {
  return (
    <div className={cl['catalog-main']}>
      <CatalogBanner />
    </div>
  );
};
