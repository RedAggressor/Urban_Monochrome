import { Dispatch, SetStateAction, useEffect, useRef, useState } from 'react';
import { Container } from '../../shared/Container/Container';
import cl from './DetailedFilters.module.scss';
import cn from 'classnames';
import { useAppSelector } from '../../shared/hooks';

const checkboxSections = [
  {
    name: 'Gender',
    options: ['Men', 'Women', 'Unisex'],
  },
  {
    name: 'Categories',
    options: [
      'Dresses',
      'Hoodies',
      'Jeans',
      'Outerwear',
      'Pants',
      'Sweaters',
      'Sweatshirts',
      'T-shirts',
      'Tops',
    ],
  },
  {
    name: 'Collections',
    options: ['Dark Impulse', 'Duality', 'Pure Essence', 'Total Black'],
  },
  {
    name: 'Hot Items',
    options: ['Best Sellers', 'New', 'Sale'],
  },
];

const sizeOptions = ['XS', 'S', 'M', 'L', 'XL', 'XXL'];

const colorOptions = ['Black', 'White'];

type Props = {
  isFiltersVisible: boolean;
  minHeightTablet: number;
  minHeightDesk: number;
  setIsFiltersVisible: Dispatch<SetStateAction<boolean>>;
  selectedGenders: string[];
  setSelectedGenders: Dispatch<SetStateAction<string[]>>;
  selectedCategories: string[];
  setSelectedCategories: Dispatch<SetStateAction<string[]>>;
  selectedCollections: string[];
  setSelectedCollections: Dispatch<SetStateAction<string[]>>;
  selectedHotItems: string[];
  setSelectedHotItems: Dispatch<SetStateAction<string[]>>;
  selectedSizes: string[];
  setSelectedSizes: Dispatch<SetStateAction<string[]>>;
  minPrice: number;
  setMinPrice: Dispatch<SetStateAction<number>>;
  maxPrice: number;
  setMaxPrice: Dispatch<SetStateAction<number>>;
  selectedColors: string[];
  setSelectedColors: Dispatch<SetStateAction<string[]>>;
  setAppliedFilters: Dispatch<SetStateAction<object>>;
  className?: string;
};

export const DetailedFilters: React.FC<Props> = ({
  isFiltersVisible,
  minHeightTablet,
  minHeightDesk,
  setIsFiltersVisible,
  selectedGenders,
  setSelectedGenders,
  selectedCategories,
  setSelectedCategories,
  selectedCollections,
  setSelectedCollections,
  selectedHotItems,
  setSelectedHotItems,
  selectedSizes,
  setSelectedSizes,
  minPrice,
  setMinPrice,
  maxPrice,
  setMaxPrice,
  selectedColors,
  setSelectedColors,
  setAppliedFilters,
  className,
}) => {
  const { screenWidth } = useAppSelector(st => st.global);

  //#region булеві стейти
  const [isGenderListVisible, setIsGenderListVisible] = useState(true);
  const [isCategoriesListVisible, setIsCategoriesListVisible] = useState(true);
  const [isCollectionsListVisible, setIsCollectionsListVisible] =
    useState(true);
  const [isHotItemsListVisible, setIsHotItemsListVisible] = useState(true);
  const [isSizeListVisible, setIsSizeListVisible] = useState(true);
  const [isPriceListVisible, setIsPriceListVisible] = useState(true);
  const [isColorListVisible, setIsColorListVisible] = useState(true);
  //#endregion

  //#region refs
  const genderListRef = useRef<HTMLUListElement>(null);
  const categoriesListRef = useRef<HTMLUListElement>(null);
  const collectionsListRef = useRef<HTMLUListElement>(null);
  const hotItemsListRef = useRef<HTMLUListElement>(null);
  const sizeSectionRef = useRef<HTMLUListElement>(null);
  const priceSectionRef = useRef<HTMLDivElement>(null);
  const colorSectionRef = useRef<HTMLUListElement>(null);
  //#endregion

  //#region функціїі об'єкти для роботи з якимсь конкретним елементом списку
  const refsList = {
    Gender: genderListRef,
    Categories: categoriesListRef,
    Collections: collectionsListRef,
    'Hot Items': hotItemsListRef,
    Size: sizeSectionRef,
    Price: priceSectionRef,
    Color: colorSectionRef,
  };

  const selectedFiltersStateList = {
    Gender: selectedGenders,
    Categories: selectedCategories,
    Collections: selectedCollections,
    'Hot Items': selectedHotItems,
  };

  const booleanStatesList = {
    Gender: isGenderListVisible,
    Categories: isCategoriesListVisible,
    Collections: isCollectionsListVisible,
    'Hot Items': isHotItemsListVisible,
  };

  function toggleListVisibility(listName: string) {
    switch (listName) {
      case 'Gender':
        setIsGenderListVisible(!isGenderListVisible);
        break;
      case 'Categories':
        setIsCategoriesListVisible(!isCategoriesListVisible);
        break;
      case 'Collections':
        setIsCollectionsListVisible(!isCollectionsListVisible);
        break;
      case 'Hot Items':
        setIsHotItemsListVisible(!isHotItemsListVisible);
        break;
      case 'Size':
        setIsSizeListVisible(!isSizeListVisible);
        break;
      case 'Price':
        setIsPriceListVisible(!isPriceListVisible);
        break;
      case 'Color':
        setIsColorListVisible(!isColorListVisible);
        break;
      default:
        break;
    }
  }
  //#endregion

  function toggleFilterOptionsState(listName: string, option: string) {
    switch (listName) {
      case 'Gender':
        setSelectedGenders(prev =>
          prev.includes(option)
            ? prev.filter(i => i !== option)
            : [...prev, option],
        );
        break;
      case 'Categories':
        setSelectedCategories(prev =>
          prev.includes(option)
            ? prev.filter(i => i !== option)
            : [...prev, option],
        );
        break;
      case 'Collections':
        setSelectedCollections(prev =>
          prev.includes(option)
            ? prev.filter(i => i !== option)
            : [...prev, option],
        );
        break;
      case 'Hot Items':
        setSelectedHotItems(prev =>
          prev.includes(option)
            ? prev.filter(i => i !== option)
            : [...prev, option],
        );
        break;
      case 'Size':
        setSelectedSizes(prev =>
          prev.includes(option)
            ? prev.filter(i => i !== option)
            : [...prev, option],
        );
        break;
      case 'Color':
        setSelectedColors(prev =>
          prev.includes(option)
            ? prev.filter(i => i !== option)
            : [...prev, option],
        );
        break;
      default:
        break;
    }
  }

  function resetFilters() {
    setSelectedGenders([]);
    setSelectedCategories([]);
    setSelectedCollections([]);
    setSelectedHotItems([]);
    setSelectedSizes([]);
    setMinPrice(10);
    setMaxPrice(250);
  }

  function applyFilters() {
    const filters = {
      genders: selectedGenders,
      categories: selectedCategories,
      collections: selectedCollections,
      hotItems: selectedHotItems,
      sizes: selectedSizes,
      priceRange: { min: minPrice, max: maxPrice },
      colors: selectedColors,
    };
    setAppliedFilters({ ...filters });
    setIsFiltersVisible(false);
  }

  useEffect(() => {
    // заборона прокрутки всієї сторінки лише на моб екранах
    isFiltersVisible && screenWidth < 567
      ? (document.body.style.overflowY = 'hidden')
      : (document.body.style.overflowY = 'auto');
  }, [isFiltersVisible]);

  function getElementMinHeight() {
    if (screenWidth < 567) {
      return 'none';
    } else if (screenWidth > 567 && screenWidth < 1024) {
      return minHeightTablet + 'px';
    }
    // на декстопі мін висота менша на 40пкс щоб компенсувати падінги контейнера і нижній край фільтрів був вирівняний з пагінацією
    return minHeightDesk - 40 + 'px';
  }
  const filtersMenuMinHeight = getElementMinHeight();

  return (
    <Container
      className={`${cl.sectionsWrapper} ${className}`}
      style={{
        transform: `${isFiltersVisible ? 'translateX(0)' : 'translateX(-100%)'}`,
        transition: 'transform 0.3s ease-in-out',
        minHeight: filtersMenuMinHeight,
      }}
    >
      {checkboxSections.map(section => (
        <section key={section.name} className={cl.checkboxSection}>
          <div className={cl.checkboxSection__titleButtonWrapper}>
            <h4 className={cl.checkboxSection__title}>{section.name}</h4>
            <button
              className={cn(cl.checkboxSection__hideButton, {
                [cl.checkboxSection__showButton]:
                  // @ts-expect-error - TS не любить чогось коли я беру відповідні властивості з об'єкта
                  !booleanStatesList[section.name],
              })}
              onClick={() => toggleListVisibility(section.name)}
            />
          </div>

          <ul
            className={cn(cl.checkboxSection__list, {
              // @ts-expect-error - аналогічно
              [cl.listHidden]: !booleanStatesList[section.name],
            })}
            // @ts-expect-error - аналогічно
            ref={refsList[section.name]}
            style={{
              // @ts-expect-error - аналогічно
              height: refsList[section.name]?.current?.scrollHeight,
            }}
          >
            {section.options.map(option => (
              <li key={option} className={cl.checkboxSection__checkboxItem}>
                <input
                  type="checkbox"
                  id={section.name + option}
                  className={cn(cl.checkboxSection__checkbox, {
                    [cl.checkboxSection__checkbox_checked]:
                      // @ts-expect-error - аналогічно
                      selectedFiltersStateList[section.name].includes(option),
                  })}
                  onChange={() =>
                    toggleFilterOptionsState(section.name, option)
                  }
                />
                <label
                  htmlFor={section.name + option}
                  className={cl.checkboxSection__label}
                >
                  {option}
                </label>
              </li>
            ))}
          </ul>
        </section>
      ))}

      <section className={cl.checkboxSection}>
        <div className={cl.checkboxSection__titleButtonWrapper}>
          <h4
            className={`${cl.checkboxSection__title} ${cl.checkboxSection__title_size}`}
          >
            Size
          </h4>
          <button
            className={cn(cl.checkboxSection__hideButton, {
              [cl.checkboxSection__showButton]: !isSizeListVisible,
            })}
            onClick={() => toggleListVisibility('Size')}
          />
        </div>

        <ul
          className={cn(cl.sizeList, {
            [cl.listHidden]: !isSizeListVisible,
          })}
          ref={sizeSectionRef}
          style={{ height: sizeSectionRef.current?.scrollHeight }}
        >
          {sizeOptions.map(size => (
            <li
              className={cn(cl.sizeList__item, {
                [cl.sizeList__item_selected]: selectedSizes.includes(size),
              })}
              onClick={() => toggleFilterOptionsState('Size', size)}
              key={size}
            >
              {size}
            </li>
          ))}
        </ul>
      </section>

      <section className={cl.checkboxSection}>
        <div className={cl.checkboxSection__titleButtonWrapper}>
          <h4 className={cl.checkboxSection__title}>Price</h4>
          <button
            className={cn(cl.checkboxSection__hideButton, {
              [cl.checkboxSection__showButton]: !isPriceListVisible,
            })}
            onClick={() => toggleListVisibility('Price')}
          />
        </div>

        <div
          className={cn(cl.priceContent, {
            [cl.listHidden]: !isPriceListVisible,
          })}
          ref={priceSectionRef}
          style={{ height: priceSectionRef.current?.scrollHeight }}
        >
          <div className={cl.priceContent__inputs}>
            <input
              type="number"
              className={cl.priceContent__input}
              value={minPrice}
              onChange={ev => setMinPrice(+ev.target.value)}
            />
            <span>&mdash;</span>
            <input
              type="number"
              className={cl.priceContent__input}
              value={maxPrice}
              onChange={ev => setMaxPrice(+ev.target.value)}
            />
          </div>
          {/* заюзати пізніше біблу react-range */}
          <input
            type="range"
            min={10}
            max={250}
            className={cl.priceContent__range}
          />
        </div>
      </section>

      <section className={cl.checkboxSection}>
        <div className={cl.checkboxSection__titleButtonWrapper}>
          <h4 className={cl.checkboxSection__title}>Color</h4>
          <button
            className={cn(cl.checkboxSection__hideButton, {
              [cl.checkboxSection__showButton]: !isColorListVisible,
            })}
            onClick={() => toggleListVisibility('Color')}
          />
        </div>

        <ul
          className={cn(cl.checkboxSection__list, {
            [cl.listHidden]: !isColorListVisible,
          })}
          ref={colorSectionRef}
          style={{
            height: colorSectionRef.current?.scrollHeight,
          }}
        >
          {colorOptions.map(option => (
            <li key={option} className={cl.checkboxSection__checkboxItem}>
              <input
                type="checkbox"
                id={option}
                className={cn(cl.checkboxSection__checkbox, {
                  [cl.checkboxSection__checkbox_checked]:
                    selectedColors.includes(option),
                })}
                onChange={() => toggleFilterOptionsState('Color', option)}
              />
              <label htmlFor={option} className={cl.checkboxSection__label}>
                {option}
              </label>
            </li>
          ))}
        </ul>
      </section>

      <div className={cl.bottomButtons}>
        <button
          className={cl.bottomButtons__whiteButton}
          onClick={applyFilters}
        >
          Apply changes
        </button>
        <button
          className={cl.bottomButtons__whiteButton}
          onClick={resetFilters}
        >
          Reset filters
        </button>
      </div>
    </Container>
  );
};
