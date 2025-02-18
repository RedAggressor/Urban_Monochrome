import { useEffect, useRef, useState } from 'react';
import { CatalogBanner } from '../../widgets/CatalogBanner/CatalogBanner';
import { CatalogFilterTopBar } from '../../widgets/CatalogFilterTopBar/CatalogFilterTopBar';
import { DetailedFilters } from '../../widgets/DetailedFilters/DetailedFilters';
import cl from './CatalogPage.module.scss';
import cn from 'classnames';
import { Pagination } from '../../widgets/Pagination/Pagination';
import { useSearchParams } from 'react-router-dom';
import { ProductList } from '../../shared/ProductList/ProductList';

export enum CatalogSortOptions {
  priceIncrease = 'Price increase',
  priceReduction = 'Price reduction',
  oldest = 'From the oldest',
  default = 'Default',
}

const testList = [
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
  {
    id: 1,
    imgUrl: '/pictures/catalogPictures/kurtkaTest.png',
    name: 'Jacket with pockets',
    collection: 'Total Black',
    price: 114,
  },
];

export const CatalogPage = () => {
  const [selectedSorting, setSelectedSorting] = useState(
    CatalogSortOptions.default,
  );
  const [isFiltersVisible, setIsFiltersVisible] = useState(false);
  const listRef = useRef<HTMLDivElement>(null);
  const [listHeight, setListHeight] = useState(listRef.current?.clientHeight);

  useEffect(() => {
    setListHeight(listRef.current?.clientHeight);
  }, [isFiltersVisible, listRef]);
  console.log(listHeight, listRef);

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
          minHeight={listHeight || 0}
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
          className={cn(cl.detailedFilters, {
            [cl.detailedFilters_open]: isFiltersVisible,
          })}
        />
        <ProductList
          list={testList}
          isFiltersOpen={isFiltersVisible}
          className={cn(cl.prodList, {
            [cl.prodList_filtersOpen]: isFiltersVisible,
          })}
          ref={listRef}
        />
        <Pagination
          totalPages={8}
          currentPage={4}
          onPageClick={() => {}}
          onArrowLeftClick={() => {}}
          onArrowRightClick={() => {}}
          className={cn(cl.pagination, {
            [cl.pagination_filtersOpen]: isFiltersVisible,
          })}
        />
      </div>
    </div>
  );
};
