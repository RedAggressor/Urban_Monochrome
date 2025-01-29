import { CatalogBanner } from '../../widgets/CatalogBanner/CatalogBanner';
import { CatalogFilterTopBar } from '../../widgets/CatalogFilterTopBar/CatalogFilterTopBar';
import cl from './CatalogPage.module.scss';

export const CatalogPage = () => {
  return (
    <div className={cl['catalog-main']}>
      <CatalogBanner className={cl['catalog-banner']} />
      <CatalogFilterTopBar />
    </div>
  );
};
