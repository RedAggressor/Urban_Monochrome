import React from 'react';
import { Container } from '../../shared/Container/Container';
import cl from './Pagination.module.scss';
import cn from 'classnames';
import { useAppSelector } from '../../shared/hooks';

type Props = {
  totalPages: number;
  currentPage: number;
  onArrowLeftClick: () => void;
  onPageClick: (ev: React.MouseEvent<HTMLLIElement>) => void;
  onArrowRightClick: () => void;
  className?: string;
};

export const Pagination: React.FC<Props> = ({
  totalPages,
  currentPage,
  onArrowLeftClick,
  onPageClick,
  onArrowRightClick,
  className,
}) => {
  const { screenWidth } = useAppSelector(st => st.global);

  function getVisiblePages(current: number, total: number) {
    const pages = [];

    if (total <= 5) {
      for (let i = 1; i <= total; i++) {
        pages.push(i);
      }
    } else {
      // дуже закручена логіка але так треба щоб пагінація вміщалась при різній кількості сторінок нормально на всіх екранах
      if (screenWidth <= 430) {
        if (current <= 2) {
          pages.push(1, 2, 3, '...', total);
        } else if (current >= total - 1) {
          pages.push(1, '...', total - 2, total - 1, total);
        } else {
          pages.push(1, '...', current, '...', total);
        }
      } else {
        if (current <= 2) {
          pages.push(1, 2, 3, '...', total);
        } else if (current >= total - 1) {
          pages.push(1, '...', total - 2, total - 1, total);
        } else {
          pages.push(1, '...', current - 1, current, current + 1, '...', total);
        }
      }
    }

    return pages;
  }

  const visiblePages = getVisiblePages(currentPage, totalPages);
  return (
    <Container className={`${className}`}>
      <nav className={cl.nav}>
        <button
          className={cn(cl.arrowButton, cl.arrowButton__left, {
            [cl.arrowButton_disabled]: currentPage === 1,
          })}
          onClick={onArrowLeftClick}
        >
          <svg className={cl.arrowButton_iconLeft} />
        </button>

        <ol className={cl.pagesList}>
          {visiblePages.map((page, index) => (
            <li
              key={index}
              className={cn(cl.pagesList__page, {
                [cl.pagesList__page_active]: currentPage === page,
              })}
              // міняти сторінку лише якщо вона не '...'
              onClick={page === '...' ? () => {} : onPageClick}
            >
              {page}
            </li>
          ))}
        </ol>

        <button
          className={cn(cl.arrowButton, cl.arrowButton__right, {
            [cl.arrowButton_disabled]: currentPage === totalPages,
          })}
          onClick={onArrowRightClick}
        >
          <svg className={cl.arrowButton_iconRight} />
        </button>
      </nav>
    </Container>
  );
};
