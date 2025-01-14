import { Link } from 'react-router-dom';
import cn from 'classnames';
import cl from './Icons.module.scss';

export enum IconType {
  Account = 'account',
  Cart = 'cart',
  Heart = 'heart',
  Menu = 'menu',
  Search = 'search',
  Email = 'email',
  Facebook = 'facebook',
  Instagram = 'instagram',
  MailArrow = 'mailArrow',
  Phone = 'phone',
  Pinterest = 'pinterest',
  Twitter = 'twitter',
  Close = 'close',
  BankCard = 'bankCard',
  PayPal = 'payPal',
  GooglePay = 'googlePay',
  ApplePay = 'applePay',
}

type Props = {
  type: IconType;
  size?: 'small' | 'large';
  to?: string;
  onClick?: () => void;
  className?: string;
};

// міні компонент. оскільки цей блок коду використовується пару раз в основному компоненті
const IconSvg = ({ type }: { type: string }) => {
  return (
    <div
      // це в нас просто основна іконка
      className={cl.icon__svg}
      style={{ backgroundImage: `url(/pictures/icons/${type}.svg)` }}
    >
      {' '}
      <div
        // а це та сама іконка з іншим кольором яка з'являється при наведенні і перекриває основну
        className={cl.icon__svgHover}
        style={{ backgroundImage: `url(/pictures/icons/${type}_hover.svg)` }}
      />{' '}
    </div>
  );
};

export const Icon: React.FC<Props> = ({
  type,
  to,
  onClick,
  size = 'large',
  className,
}) => {
  // ці іконки повинні бути кнопками з якимсь функціоналом по клікові
  if (
    type === IconType.Search ||
    type === IconType.Menu ||
    type === IconType.MailArrow ||
    type === IconType.Close
  ) {
    return (
      <button className={`${cl.icon} ${className}`} onClick={onClick}>
        <IconSvg type={type} />
      </button>
    );
  }

  // всі остальні будуть лінками
  return (
    <Link
      to={to || '/'}
      className={cn(cl.icon, className, {
        [cl.iconSmall]: size === 'small',
      })}
    >
      <IconSvg type={type} />
    </Link>
  );
};
