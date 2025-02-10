import { useEffect, useState } from 'react';
import { CatalogBanner } from '../../widgets/CatalogBanner/CatalogBanner';
import { CatalogFilterTopBar } from '../../widgets/CatalogFilterTopBar/CatalogFilterTopBar';
import { DetailedFilters } from '../../widgets/DetailedFilters/DetailedFilters';
import cl from './CatalogPage.module.scss';
import { Pagination } from '../../widgets/Pagination/Pagination';
import { useSearchParams } from 'react-router-dom';

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

  //#region стейти фільтрів
  const [selectedGenders, setSelectedGenders] = useState<string[]>([]);
  const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
  const [selectedCollections, setSelectedCollections] = useState<string[]>([]);
  const [selectedHotItems, setSelectedHotItems] = useState<string[]>([]);
  const [selectedSizes, setSelectedSizes] = useState<string[]>([]);
  const [minPrice, setMinPrice] = useState(10);
  const [maxPrice, setMaxPrice] = useState(250);
  const [selectedColors, setSelectedColors] = useState<string[]>([]);

  const [appliedFilters, setAppliedFilters] = useState({});
  //#endregion

  //#region параметри пошуку і застосування їх
  const [searchParams, setSearchParams] = useSearchParams();

  const sortBy = searchParams.get('sortBy') || 'Default';

  function handleSortOptionChange(value: string) {
    const params = new URLSearchParams(searchParams);

    params.set('sortBy', value || '');
    setSearchParams(params);
  }
  //#endregion
  return (
    <div className={cl['catalog-main']}>
      <CatalogBanner className={cl['catalog-banner']} />
      <CatalogFilterTopBar
        currentSort={sortBy}
        onSortOptionChange={handleSortOptionChange}
        isFiltersVisible={isFiltersVisible}
        setIsFiltersVisible={setIsFiltersVisible}
      />

      <div className={cl['catalog-gridContainer']}>
        <DetailedFilters
          isFiltersVisible={isFiltersVisible}
          setIsFiltersVisible={setIsFiltersVisible}
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
          selectedColors={selectedColors}
          setSelectedColors={setSelectedColors}
          setAppliedFilters={setAppliedFilters}
        />
        <Pagination
          totalPages={8}
          currentPage={4}
          onPageClick={() => {}}
          onArrowLeftClick={() => {}}
          onArrowRightClick={() => {}}
        />
      </div>
    </div>
  );
};
