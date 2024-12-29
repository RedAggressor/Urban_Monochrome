import cl from './navLinks.module.scss';

export enum NavLinksOrigin {
  Header = 'header',
  Footer = 'footer',
}

interface Props {
  className: string;
  origin: NavLinksOrigin;
}

export const NavLinks: React.FC<Props> = ({ className, origin }) => {
  return (
    <nav
      className={` ${className} ${origin === NavLinksOrigin.Header ? cl.header__nav : cl.footer__nav}`}
    >
      <a href="#" className={cl.nav_link}>
        Shop
      </a>
      <a href="#" className={cl.nav_link}>
        New
      </a>
      <a href="#" className={cl.nav_link}>
        Sale
      </a>
    </nav>
  );
};
