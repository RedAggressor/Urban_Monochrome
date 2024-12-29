import { Link } from 'react-router-dom';
import cl from './Logo.module.scss';

export const Logo = () => {
  return (
    <Link to="#" className={cl.logo}>
      <img src="/pictures/logo.png" alt="logo" className={cl.logo__img} />
    </Link>
  );
};
