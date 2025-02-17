import { Link } from 'react-router-dom';
import cl from './navLinks.module.scss';
import cn from 'classnames';

export enum NavLinksOrigin {
  Header = 'header',
  Footer = 'footer',
}

interface Props {
  className?: string;
  origin: NavLinksOrigin;
}

export const NavLinks: React.FC<Props> = ({ className, origin }) => {
  return (
    <nav
      className={cn(className, cl.nav, {
        [cl.nav__header]: origin === NavLinksOrigin.Header,
        [cl.nav__footer]: origin === NavLinksOrigin.Footer,
      })}
    >
      <Link to="catalog" className={cl.nav_link}>
        Shop
      </Link>
      <Link to="#" className={cl.nav_link}>
        New
      </Link>
      <Link to="#" className={cl.nav_link}>
        Sale
      </Link>
    </nav>
  );
};
