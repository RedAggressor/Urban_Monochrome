import cl from './ButtonShopNow.module.scss';

interface Props {
  className?: string;
  onClick: () => void;
}

export const ButtonShopNow: React.FC<Props> = ({ className, onClick }) => {
  return (
    <button className={`${className} ${cl.buttonShopNow}`} onClick={onClick}>
      Shop Now
    </button>
  );
};
