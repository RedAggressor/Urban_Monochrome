import { ProductType } from '../../features/ProductType';
import { Container } from '../Container/Container';
import { ProductCard } from '../ProductCard/ProductCard';
import cl from './ProductList.module.scss';
import cn from 'classnames';

type Props = {
  list: ProductType[];
  isFiltersOpen: boolean;
  ref?: React.Ref<HTMLDivElement>;
  className?: string;
};

export const ProductList: React.FC<Props> = ({
  list,
  isFiltersOpen,
  ref,
  className,
}) => {
  console.log(ref);

  return (
    <Container className={className} ref={ref}>
      <ul className={cl.list}>
        {list.map((item, i) => (
          <ProductCard
            product={item}
            className={cn(cl.list__item, {
              [cl.list__item_filtersOpen]: isFiltersOpen,
            })}
            key={i}
          />
        ))}
      </ul>
    </Container>
  );
};
