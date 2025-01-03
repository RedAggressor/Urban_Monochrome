import '../ButtonShopNow/ButtonShopNow.scss';

interface Props {
  className?: string;
}

export const ButtonShopNow: React.FC<Props> = ({ className }) => {
  return (
    <button className={`${className} button-shop-now`}>Shop Now</button>
  )
}
