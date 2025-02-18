import { Link } from 'react-router-dom';
import cl from './ProductCard.module.scss';
import { ProductType } from '../../features/ProductType';

type Props = {
  product: ProductType;
  className?: string;
};

const LeftRedBracket = () => <span className={cl.redText}>{`[`}</span>;
const RightRedBracket = () => <span className={cl.redText}>{`]`}</span>;

export const ProductCard: React.FC<Props> = ({ product, className }) => {
  return (
    <article className={`${cl.card} ${className}`}>
      <Link to="" className={cl.imgLink}>
        <img
          src={product.imgUrl}
          alt={product.name}
          className={cl.imgLink__img}
        />
      </Link>

      <div className={cl.textContainer}>
        <Link to="">
          <h3 className={cl.textContainer__title}>
            {product.name} <br /> <LeftRedBracket />
            {product.collection}
            <RightRedBracket />
          </h3>
        </Link>
        <span
          itemProp="price"
          content={`${product.price}`}
          className={cl.textContainer__price}
        >
          {`$${product.price}`}
        </span>
      </div>
    </article>
  );
};
