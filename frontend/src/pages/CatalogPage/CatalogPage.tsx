import { useEffect, useState } from 'react';
import { CatalogBanner } from '../../widgets/CatalogBanner/CatalogBanner';
import { CatalogFilterTopBar } from '../../widgets/CatalogFilterTopBar/CatalogFilterTopBar';
import { DetailedFilters } from '../../widgets/DetailedFilters/DetailedFilters';
import cl from './CatalogPage.module.scss';

export enum CatalogSortOptions {
  priceIncrease = 'Price increase',
  priceReduction = 'Price reduction',
  oldest = 'From the oldest',
  default = 'Default',
}

export const CatalogPage = () => {
  const [selectedSorting, setSelectedSorting] = useState(
    CatalogSortOptions.default,
  );
  const [isFiltersVisible, setIsFiltersVisible] = useState(false);

  const [selectedGenders, setSelectedGenders] = useState<string[]>([]);
  const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
  const [selectedCollections, setSelectedCollections] = useState<string[]>([]);
  const [selectedHotItems, setSelectedHotItems] = useState<string[]>([]);
  const [selectedSizes, setSelectedSizes] = useState<string[]>([]);
  const [minPrice, setMinPrice] = useState(10);
  const [maxPrice, setMaxPrice] = useState(250);

  useEffect(() => {
    console.log('selected', selectedCategories);
  }, [selectedCategories]);

  return (
    <div className={cl['catalog-main']}>
      <CatalogBanner className={cl['catalog-banner']} />
      <CatalogFilterTopBar
        setSelectedSorting={setSelectedSorting}
        isFiltersVisible={isFiltersVisible}
        setIsFiltersVisible={setIsFiltersVisible}
      />

      <DetailedFilters
        isFiltersVisible={isFiltersVisible}
        selectedGenders={selectedGenders}
        setSelectedGenders={setSelectedGenders}
        selectedCategories={selectedCategories}
        setSelectedCategories={setSelectedCategories}
        selectedCollections={selectedCollections}
        setSelectedCollections={setSelectedCollections}
        selectedHotItems={selectedHotItems}
        setSelectedHotItems={setSelectedHotItems}
        selectedSizes={selectedSizes}
        setSelectedSizes={setSelectedSizes}
        minPrice={minPrice}
        setMinPrice={setMinPrice}
        maxPrice={maxPrice}
        setMaxPrice={setMaxPrice}
      />
    </div>
  );
};
