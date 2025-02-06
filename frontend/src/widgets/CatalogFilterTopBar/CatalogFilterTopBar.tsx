import cl from './CatalogFilterTopBar.module.scss';
import cn from 'classnames';
import React, { Dispatch, SetStateAction, useMemo, useState } from 'react';
import { CatalogSortOptions } from '../../pages/CatalogPage/CatalogPage';
import { Container } from '../../shared/Container/Container';

type Props = {
  setSelectedSorting: Dispatch<SetStateAction<CatalogSortOptions>>;
  isFiltersVisible: boolean;
  setIsFiltersVisible: Dispatch<SetStateAction<boolean>>;
};

export const CatalogFilterTopBar: React.FC<Props> = ({
  setSelectedSorting,
  isFiltersVisible,
  setIsFiltersVisible,
}) => {
  const [isOptionsVisible, setIsOptionsVisible] = useState(false);
  // закешував бо це значення треба знайти лише раз і більше не переобчислювати
  const optionsList = useMemo(() => Object.values(CatalogSortOptions), []);

  return (
    <Container className={cl.container}>
      <nav className={cl.filters}>
        <button
          className={cl.textIconButton}
          onClick={() => setIsFiltersVisible(!isFiltersVisible)}
        >
          {isFiltersVisible ? 'Hide filters' : 'Show filters'}
          <svg
            className={`${cl.textIconButton__icon} ${cl.icon_toggleFilters}`}
          />
        </button>
        <div className={cl.rightButtons}>
          <button
            className={`${cl.textIconButton} ${cl.sortByButton}`}
            onClick={() => setIsOptionsVisible(!isOptionsVisible)}
          >
            <span className={cl.sortByButton__text}>Sort by</span>
            <svg
              className={`${cl.textIconButton__icon} ${cl.icon_arrowDown}`}
            />
            <ul
              className={cn(cl.sortByOptions, {
                [cl.sortByOptions_hidden]: !isOptionsVisible,
              })}
              // щоб клік по цьому елементу не активував подію батьківського(закривання меню)
              onClick={ev => ev.stopPropagation()}
            >
              {optionsList.map(opt => (
                <li key={opt} className={cl.sortByOptions__option}>
                  <input
                    type="radio"
                    name="sortOptions"
                    id={opt}
                    value={opt}
                    className={cl.sortByOptions__radio}
                    onChange={() => setSelectedSorting(opt)}
                  />
                  <label
                    htmlFor={opt}
                    key={opt}
                    className={cl.sortByOptions__label}
                  >
                    {opt}
                  </label>
                </li>
              ))}
            </ul>
          </button>

          <button className={cl.textIconButton}>
            <svg
              className={`${cl.textIconButton__icon} ${cl.icon_displayList}`}
            />
          </button>

          <button className={cl.textIconButton}>
            <svg
              className={`${cl.textIconButton__icon} ${cl.icon_displayGrid}`}
            />
          </button>
        </div>
      </nav>
    </Container>
  );
};
